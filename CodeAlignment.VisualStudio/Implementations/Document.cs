using System;

using CMcG.CodeAlignment.Business;

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace CMcG.CodeAlignment.Implementations
{
    internal class Document : IDocument
    {
        private readonly IWpfTextView  m_doc;
        private          ITextSnapshot m_snapshot;

        public Document(IWpfTextView doc)
        {
            this.m_doc      = doc;
            this.m_snapshot = doc.TextSnapshot;
        }

        public Int32 LineCount
        {
            get { return this.m_snapshot.LineCount; }
        }

        public Int32 StartSelectionLineNumber
        {
            get { return this.m_snapshot.GetLineNumberFromPosition(this.m_doc.Selection.Start.Position); }
        }

        public Int32 EndSelectionLineNumber
        {
            get { return this.m_snapshot.GetLineNumberFromPosition(this.m_doc.Selection.End.Position); }
        }

        public Int32 CaretColumn
        {
            get
            {
                SnapshotPoint caret = this.m_doc.Caret.Position.BufferPosition;
                Int32  index = this.m_doc.GetTextViewLineContainingBufferPosition(caret).Start.Difference(caret);
                String line  = this.m_snapshot.GetLineFromPosition(caret).GetText().Substring(0, index);
                return line.ReplaceTabs(this.TabSize).Length + this.m_doc.Caret.Position.VirtualSpaces;
            }
        }

        public Boolean ConvertTabsToSpaces
        {
            get { return this.m_doc.Options.GetOptionValue(DefaultOptions.ConvertTabsToSpacesOptionId); }
        }

        public Int32 TabSize
        {
            get { return this.m_doc.Options.GetOptionValue(DefaultOptions.TabSizeOptionId); }
        }

        public ILine GetLineFromLineNumber(Int32 lineNo)
        {
            return new Line(this.m_snapshot.GetLineFromLineNumber(lineNo));
        }

        public IEdit StartEdit()
        {
            return new Edit(this.m_snapshot.TextBuffer.CreateEdit());
        }

        public String FileType
        {
            get { return "." + this.m_doc.TextBuffer.ContentType.TypeName.ToLower(); }
        }

        public void Refresh()
        {
            this.m_snapshot = this.m_doc.TextSnapshot;
        }
    }
}