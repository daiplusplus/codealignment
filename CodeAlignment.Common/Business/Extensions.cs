using System;
using System.Linq;
using System.Collections.Generic;

namespace CMcG.CodeAlignment.Business
{
    public static class Extensions
    {
        public static IEnumerable<Int32> UpTo(this Int32 start, Int32 end)
        {
            for (Int32 i = start; i <= end; i++)
            {
                yield return i;
            }
        }

        public static IEnumerable<Int32> DownTo(this Int32 start, Int32 end)
        {
            for (Int32 i = start; i >= end; i--)
            {
                yield return i;
            }
        }

        /// <summary>Replaces each tab character <c>\t</c> in <paramref name="value"/> with <paramref name="tabSize"/>-many space characters - but modulo any existing space chars.</summary>
        public static String ReplaceTabs(this String value, Int32 tabSize)
        {
            Int32 index = value.IndexOf('\t');
            while (index >= 0)
            {
                Int32  spaceChars  = tabSize - (index % tabSize);
                String replacement = String.Empty.PadLeft(spaceChars);

                value = value.Remove(index, 1).Insert(index, replacement);
                index = value.IndexOf('\t');
            }

            return value;
        }

        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource,TKey> selector)
        {
            return source.MaxItemsBy(selector).First();
        }

        public static IEnumerable<TSource> MaxItemsBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource,TKey> selector)
        {
            TKey maxValue = source.Max(selector);
            Comparer<TKey> comparer = Comparer<TKey>.Default;
            return source.Where(x => comparer.Compare(selector.Invoke(x), maxValue) == 0);
        }
    }
}