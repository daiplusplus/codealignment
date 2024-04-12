using System;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextManager.Interop;

namespace CMcG.CodeAlignment
{
    public abstract class CommandFilter : IOleCommandTarget
    {
        protected abstract Guid CommandGuid { get; }

        public IOleCommandTarget Next { get; set; }

        public Int32 Exec(ref Guid pguidCmdGroup, UInt32 nCmdID, UInt32 nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (pguidCmdGroup != this.CommandGuid)
            {
                return this.Next.Exec(ref pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn, pvaOut);
            }

            this.Execute(nCmdID);

            return VSConstants.S_OK;
        }

        public Int32 QueryStatus(ref Guid pguidCmdGroup, UInt32 cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
        {
            if (prgCmds is null) throw new ArgumentNullException(nameof(prgCmds));
            ThreadHelper.ThrowIfNotOnUIThread();

            if (pguidCmdGroup != this.CommandGuid)
            {
                return this.Next.QueryStatus(ref pguidCmdGroup, cCmds, prgCmds, pCmdText);
            }

            foreach (OLECMD cmd in prgCmds)
            {
                prgCmds[0].cmdf = (UInt32)this.CanExecuteResult(cmd.cmdID);
            }

            return VSConstants.S_OK;
        }

        public OLECMDF CanExecuteResult(UInt32 cmdId)
        {
            return this.CanExecute(cmdId) ?
                (OLECMDF.OLECMDF_ENABLED | OLECMDF.OLECMDF_SUPPORTED) :
                (OLECMDF.OLECMDF_SUPPORTED);
        }

        public virtual Boolean CanExecute(UInt32 cmdId) => true;

        public abstract void Execute(UInt32 cmdId);

        public static void Register(IVsTextView textViewAdapter, CommandFilter filter)
        {
            if (textViewAdapter is null) throw new ArgumentNullException(nameof(textViewAdapter));
            if (filter          is null) throw new ArgumentNullException(nameof(filter));

            if (ErrorHandler.Succeeded(textViewAdapter.AddCommandFilter(filter, out IOleCommandTarget next)))
            {
                filter.Next = next;
            }
        }
    }
}
