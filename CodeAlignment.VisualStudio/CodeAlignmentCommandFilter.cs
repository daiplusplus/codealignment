using System;

using CMcG.CodeAlignment.Business;
using CMcG.CodeAlignment.Implementations;

using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;

namespace CMcG.CodeAlignment
{
    sealed class CodeAlignmentCommandFilter : CommandFilter
    {
        private readonly IWpfTextView m_view;
        private readonly IVsTextView  m_textViewAdapter;

        public CodeAlignmentCommandFilter(IVsTextView textViewAdapter, IWpfTextView view)
        {
            this.m_view            = view            ?? throw new ArgumentNullException( nameof(view) );
            this.m_textViewAdapter = textViewAdapter ?? throw new ArgumentNullException( nameof(textViewAdapter) );
        }

        protected override Guid CommandGuid => GuidList.CmdSetGuid;

        public override void Execute(UInt32 cmdId)
        {
            AlignFunctions functions = new AlignFunctions // <-- QUESTION: Should this be instantiated on every call?
            {
                UIManager     = new UIManager(),
                Document      = new Document(doc: this.m_view),
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
                case Commands.AlignByColon        : functions.AlignBy(":");                       break;
                case Commands.AlignByComma        : functions.AlignBy(",");                       break;
#if DEBUG
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(cmdId), actualValue: cmdId, message: "Unrecognized value.");
#endif
            }
        }
    }
}
