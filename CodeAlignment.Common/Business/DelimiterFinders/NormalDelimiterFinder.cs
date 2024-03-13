using System;
using System.Linq;

namespace CMcG.CodeAlignment.Business
{
    public class NormalDelimiterFinder : IDelimiterFinder
    {
        public virtual DelimiterResult GetIndex(String source, String delimiter, Int32 minIndex, Int32 tabSize)
        {
            minIndex = this.TabbifyIndex(source, minIndex, tabSize);

            Int32 result = source.Length >= minIndex ? source.IndexOf(delimiter, minIndex) : -1;
            return DelimiterResult.Create(result);
        }

        public Int32 TabbifyIndex(String source, Int32 minIndex, Int32 tabSize)
        {
            Int32 adjustment = 0;
            Int32 index      = source.IndexOf('\t');

            while (index >= 0 && index < minIndex)
            {
                Int32 padding = tabSize - (index % tabSize);
                if (index + padding - 1 <= minIndex)
                {
                    adjustment += padding - 1;
                }

                source = source.Remove(index, 1).Insert(index, String.Empty.PadLeft(padding));
                index  = source.IndexOf('\t');
            }

            return minIndex - adjustment;
        }
    }
}