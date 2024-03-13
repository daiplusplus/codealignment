using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace CMcG.CodeAlignment.Business
{
    public class Alignment
    {
        public IDocument        View              { get; set; }
        public IScopeSelector   Selector          { get; set; }
        public IDelimiterFinder Finder            { get; set; }
        public Boolean UseIdeTabSettings { get; set; }

        public Alignment()
        {
            this.Finder = new NormalDelimiterFinder();
        }

        public Int32 PerformAlignment(String delimiter, Int32 minIndex = 0, Boolean addSpace = false)
        {
            IEnumerable<ILine> lines = this.Selector.GetLinesToAlign(this.View);
            LineDetails[] data = lines
                .Select(x => new LineDetails(x, this.Finder, delimiter, minIndex, this.View.TabSize))
                .Where(y => y.Index >= 0)
                .ToArray();

            if (!data.Any())
            {
                return -1;
            }

            Int32 targetPosition = data
                .MaxItemsBy(y => y.Position)
                .Max(x => x.GetPositionToAlignTo(addSpace, this.View.TabSize));

            this.CommitChanges(data, targetPosition);
            return targetPosition;
        }

        private void CommitChanges(LineDetails[] data, Int32 targetPosition)
        {
            using (IEdit edit = this.View.StartEdit())
            {
                foreach (LineDetails change in data)
                {
                    if (!edit.Insert(change.Line, change.Index, this.GetSpacesToInsert(change.Position, targetPosition)))
                    {
                        return;
                    }
                }

				#warning TODO: After inserting the spaces to align things, then remove all redundant spaces.

                edit.Commit();
            }
        }

        String GetSpacesToInsert(Int32 startIndex, Int32 endIndex)
        {
            Boolean useSpaces = this.View.ConvertTabsToSpaces;
            if (useSpaces || !this.UseIdeTabSettings)
            {
                return String.Empty.PadLeft(endIndex - startIndex);
            }

            Int32 spaces = endIndex % this.View.TabSize;
            Int32 tabs   = (Int32)Math.Ceiling((endIndex - spaces - startIndex) / (Double)this.View.TabSize);

            return (tabs == 0) ? String.Empty.PadLeft(endIndex - startIndex)
                               : String.Empty.PadLeft(tabs, '\t') + String.Empty.PadLeft(spaces);
        }
    }
}