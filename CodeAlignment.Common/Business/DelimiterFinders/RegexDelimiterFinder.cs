using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CMcG.CodeAlignment.Business
{
    public class RegexDelimiterFinder : NormalDelimiterFinder
    {
        public override DelimiterResult GetIndex(String source, String delimiter, Int32 minIndex, Int32 tabSize)
        {
            minIndex = this.TabbifyIndex(source, minIndex, tabSize);

            if (source.Length < minIndex)
            {
                return DelimiterResult.Create(-1);
            }

            Match match = Regex.Match(source.Substring(minIndex), delimiter);
            if (!match.Success)
            {
                return DelimiterResult.Create(-1);
            }

            return new DelimiterResult
            {
                CompareIndex = minIndex + this.GetGroupIndex(match, "compare", "x"),
                InsertIndex  = minIndex + this.GetGroupIndex(match, "insert", "compare", "x")
            };
        }

        Int32 GetGroupIndex(Match match, params String[] keys)
        {
            foreach (String key in keys)
            {
                Group group = match.Groups[key];
                if (group.Success)
                {
                    return group.Index;
                }
            }

            return match.Index;
        }
    }
}
