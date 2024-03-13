using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CMcG.CodeAlignment.Business
{
    public class GeneralScopeSelector : IScopeSelector
    {
        public String ScopeSelectorRegex { get; set; }
        public Int32? Start              { get; set; }
        public Int32? End                { get; set; }

        public IEnumerable<ILine> GetLinesToAlign(IDocument view)
        {
            Int32 start = this.Start ?? view.StartSelectionLineNumber;
            Int32 end   = this.End   ?? view.EndSelectionLineNumber;

            if (start == end)
            {
                IEnumerable<Int32> blanks = start.DownTo(0).Where(x => this.IsLineBlank(view, x));
                start      = blanks.Any() ? blanks.First() + 1 : 0;

                blanks     = end.UpTo(view.LineCount - 1).Where(x => this.IsLineBlank(view, x));
                end        = blanks.Any() ? blanks.First() - 1 : view.LineCount -1;
            }

            return start.UpTo(end).Select(x => view.GetLineFromLineNumber(x));
        }

        Boolean IsLineBlank(IDocument view, Int32 lineNo)
        {
            return Regex.IsMatch(view.GetLineFromLineNumber(lineNo).Text, this.ScopeSelectorRegex);
        }
    }
}