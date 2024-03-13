using System;
using System.Linq;
using System.Drawing;
using CMcG.CodeAlignment.Business;

namespace CMcG.CodeAlignment
{
    public class AlignmentViewModel
    {
        Business.CodeAlignmentOptions m_options = new Business.CodeAlignmentOptions();
        Alignment m_alignment;
        AlignFunctions m_functions;

        Int32 m_lastAlignment = -1;

        public AlignmentViewModel(AlignFunctions functions, Alignment alignment)
        {
            this.m_alignment = alignment;
            this.m_functions = functions;
        }

        public void AlignFromPosition()
        {
            this.m_functions.AlignByDialog(alignFromCaret:true);
        }

        public Int32 PerformAlign(Key key, Boolean forceFromCaret )
        {
            this.m_alignment.View.Refresh();
            KeyShortcut shortcut = this.m_options.GetShortcut(key, this.m_functions.Document.FileType);

            if (shortcut != null && !String.IsNullOrEmpty(shortcut.Alignment))
            {
                this.m_alignment.Finder = this.GetFinder(shortcut);
                Int32 minIndex       = this.GetMinIndex(forceFromCaret, shortcut);
                this.m_lastAlignment    = this.m_alignment.PerformAlignment(shortcut.Alignment, minIndex, shortcut.AddSpace);
                return this.m_lastAlignment;
            }

            return -1;
        }

        IDelimiterFinder GetFinder(KeyShortcut shortcut)
        {
            return shortcut.UseRegex ? new RegexDelimiterFinder() : new NormalDelimiterFinder();
        }

        Int32 GetMinIndex(Boolean forceFromCaret, KeyShortcut shortcut)
        {
            if (this.m_lastAlignment != -1)
            {
                return this.m_lastAlignment + 1;
            }

            if (forceFromCaret || shortcut.AlignFromCaret)
            {
                return this.m_functions.Document.CaretColumn;
            }

            return 0;
        }
    }
}