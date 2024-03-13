using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using CMcG.CodeAlignment.Business;

namespace CMcG.CodeAlignment.Test.Business.Mocks
{
    public class MockDocuments : IDocument
    {
        MockLine[] m_lines;
        public String[] Lines
        {
            get { return this.m_lines.Select(x => x.Text).ToArray(); }
            set { this.m_lines = value.Select(x => new MockLine { Text = x }).ToArray(); }
        }

        public Int32 LineCount
        {
            get { return this.m_lines.Length; }
        }

        public Int32 StartSelectionLineNumber { get; set; }
        public Int32 EndSelectionLineNumber   { get; set; }
        public Int32 CaretColumn              { get; set; }
        public Boolean ConvertTabsToSpaces      { get; set; }
        public Int32 TabSize                  { get; set; }
        public String FileType                 { get; set; }

        public ILine GetLineFromLineNumber(Int32 lineNo)
        {
            return this.m_lines[lineNo];
        }

        public IEdit StartEdit()
        {
            return new MockEdit();
        }

        public void Refresh()
        {
        }
    }
}
