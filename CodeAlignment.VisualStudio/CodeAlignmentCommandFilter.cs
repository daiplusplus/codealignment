using System;
using CMcG.CodeAlignment.Implementations;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using CMcG.CodeAlignment.Business;

namespace CMcG.CodeAlignment
{
    sealed class CodeAlignmentCommandFilter : CommandFilter
    {
        IWpfTextView m_view;
        IVsTextView  m_textViewAdapter;

        public CodeAlignmentCommandFilter(IVsTextView textViewAdapter, IWpfTextView view)
        {
            this.m_view            = view;
            this.m_textViewAdapter = textViewAdapter;
        }

        protected override Guid CommandGuid
        {
            get { return GuidList.CmdSetGuid; }
        }

        public override void Execute( UInt32 cmdId )
        {
            AlignFunctions functions = new AlignFunctions
            {
                UIManager     = new UIManager(),
                Document      = new Document(this.m_view),
                Handle        = this.m_textViewAdapter.GetWindowHandle(),
                KeyGrabOffset = new System.Drawing.Point(10, -35)
            };

            switch (cmdId)
            {
                case Commands.AlignBy             : functions.AlignByDialog();                    break;
                case Commands.AlignByKey          : functions.AlignByKey();                       break;
                case Commands.AlignByEquals       : functions.AlignBy(Key.EqualsPlus);            break;
                case Commands.AlignByEqualsEquals : functions.AlignBy("==");                      break;
                case Commands.AlignByMUnderscore  : functions.AlignBy(Key.M);                     break;
                case Commands.AlignByQuote        : functions.AlignBy(Key.Quotes);                break;
                case Commands.AlignByPeriod       : functions.AlignBy(Key.Period);                break;
                case Commands.AlignBySpace        : functions.AlignBy(Key.Space);                 break;
                case Commands.AlignFromCaret      : functions.AlignByDialog(alignFromCaret:true); break;
            }
        }
    }
}