using System;

namespace CMcG.CodeAlignment.Business
{
    public interface IDocument
    {
        Int32   LineCount                { get; }
        Int32   StartSelectionLineNumber { get; }
        Int32   EndSelectionLineNumber   { get; }
        Int32   CaretColumn              { get; }
        Boolean ConvertTabsToSpaces      { get; }
        Int32   TabSize                  { get; }
        String  FileType                 { get; }

        ILine GetLineFromLineNumber(Int32 lineNo);
        IEdit StartEdit();
        void  Refresh();
    }
}