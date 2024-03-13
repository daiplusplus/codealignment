using System;

namespace CMcG.CodeAlignment.Business
{
    public interface IEdit : IDisposable
    {
        Boolean Insert(ILine line, Int32 position, String text);

        void Commit();
    }
}
