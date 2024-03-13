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

        public Int32 Exec(ref Guid cmdGroup, UInt32 cmdId, UInt32 options, IntPtr inArg, IntPtr outArg)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (cmdGroup != this.CommandGuid)
            {
                return this.Next.Exec(ref cmdGroup, cmdId, options, inArg, outArg);
            }

            this.Execute(cmdId);

            return VSConstants.S_OK;
        }

        public Int32 QueryStatus(ref Guid cmdGroup, UInt32 cmdCount, OLECMD[] cmds, IntPtr cmdText)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (cmdGroup != this.CommandGuid)
            {
                return this.Next.QueryStatus(ref cmdGroup, cmdCount, cmds, cmdText);
            }

            foreach (OLECMD cmd in cmds)
            {
                cmds[0].cmdf = (UInt32)this.CanExecuteResult(cmd.cmdID);
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
            if (ErrorHandler.Succeeded(textViewAdapter.AddCommandFilter(filter, out IOleCommandTarget next)))
            {
                filter.Next = next;
            }
        }
    }
}