using System;

namespace CMcG.CodeAlignment.Business
{
    public interface IDelimiterFinder
    {
        DelimiterResult GetIndex(String source, String delimiter, Int32 minIndex, Int32 tabSize);
    }
}