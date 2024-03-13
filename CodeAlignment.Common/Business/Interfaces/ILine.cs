using System;

namespace CMcG.CodeAlignment.Business
{
    public interface ILine
    {
        Int32  Position { get; }
        String Text     { get; }
    }
}