using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace CMcG.CodeAlignment.Business
{
    public class LineDetails
    {
        public LineDetails(ILine line, IDelimiterFinder finder, String delimiter, Int32 minIndex, Int32 tabSize)
        {
            String withoutTabs = line.Text.ReplaceTabs(tabSize);
            this.Line            = line;
            this.Index           = finder.GetIndex(line.Text,   delimiter, minIndex, tabSize).InsertIndex;
            this.Position        = finder.GetIndex(withoutTabs, delimiter, minIndex, tabSize).CompareIndex;
        }

        public ILine Line     { get; private set; }
        public Int32 Index    { get; private set; }
        public Int32 Position { get; private set; }

        public Int32 GetPositionToAlignTo(Boolean addSpace, Int32 tabSize)
        {
            if (addSpace && this.Position > 0 && this.Line.Text.ReplaceTabs(tabSize)[this.Position - 1] != ' ')
            {
                return this.Position + 1;
            }

            return this.Position;
        }
    }
}