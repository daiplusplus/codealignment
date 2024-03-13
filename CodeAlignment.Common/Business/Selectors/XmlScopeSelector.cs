using System;
using System.Linq;
using System.Collections.Generic;

namespace CMcG.CodeAlignment.Business
{
    public class XmlScopeSelector : IScopeSelector
    {
        public Int32? Start { get; set; }
        public Int32? End   { get; set; }

        public IEnumerable<ILine> GetLinesToAlign(IDocument view)
        {
            Int32 start = this.Start ?? view.StartSelectionLineNumber;
            Int32 end   = this.End   ?? view.EndSelectionLineNumber;

            if (start == end)
            {
                String line = view.GetLineFromLineNumber(start).Text.ReplaceTabs(view.TabSize);
                Boolean isMulti = this.IsMultiLineTag(view, line);

                IEnumerable<Int32> blanks = isMulti ?
                    (start + 0).DownTo(0).Where(x => this.IsMultiLineStart(view, x)) :
                    (start + 1).DownTo(1).Where(x => this.IsNotSameScope(view, x - 1, line));

                start  = blanks.Any() ? blanks.First() : 0;

                blanks = isMulti ? (end - 0).UpTo(view.LineCount - 1).Where(x => this.IsMultiLineEnd(view, x + 0))
                                 : (end - 1).UpTo(view.LineCount - 2).Where(x => this.IsNotSameScope(view, x + 1, line));

                end    = blanks.Any() ? blanks.First() : view.LineCount - 1;
            }

            return start.UpTo(end).Select(view.GetLineFromLineNumber);
        }

        Boolean IsMultiLineTag(IDocument view, String line)
        {
            line = line.Trim();
            return !line.StartsWith("<") || !line.Contains(">");
        }

        Boolean IsMultiLineStart(IDocument view, Int32 lineNo)
        {
            String[] blankStrings = new[] { String.Empty,  };
            String line = view.GetLineFromLineNumber(lineNo).Text.Trim();
            return line == String.Empty || line.StartsWith("<");
        }

        Boolean IsMultiLineEnd(IDocument view, Int32 lineNo)
        {
            String[] blankStrings = new[] { String.Empty,  };
            String line = view.GetLineFromLineNumber(lineNo).Text.Trim();
            return line == String.Empty || line.Contains(">");
        }

        Boolean IsNotSameScope(IDocument view, Int32 lineNo, String original)
        {
            String line           = view.GetLineFromLineNumber(lineNo).Text.ReplaceTabs(view.TabSize);
            Int32  lineIndent     = line    .Length - line    .TrimStart().Length;
            Int32  originalIndent = original.Length - original.TrimStart().Length;
            return line.Trim() == String.Empty || lineIndent != originalIndent;
        }
    }
}