using System;

using CMcG.CodeAlignment.Business;

using Microsoft.VisualStudio.Text;

namespace CMcG.CodeAlignment.Implementations
{
    internal class Line : ILine
    {
        private readonly ITextSnapshotLine m_line;

        public Line(ITextSnapshotLine line)
        {
            this.m_line = line;
        }

        public Int32 Position
        {
            get { return this.m_line.Start.Position; }
        }

        public String Text
        {
            get { return this.m_line.GetText(); }
        }
    }
}