using System;
using System.Linq;

namespace CMcG.CodeAlignment.Business
{
    public class DelimiterResult
    {
        public Int32 CompareIndex { get; set; }
        public Int32 InsertIndex  { get; set; }

        public static DelimiterResult Create(Int32 index)
        {
            return new DelimiterResult
            {
                CompareIndex = index,
                InsertIndex  = index
            };
        }
    }
}
