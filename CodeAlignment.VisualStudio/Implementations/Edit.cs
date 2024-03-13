using CMcG.CodeAlignment.Business;

using Microsoft.VisualStudio.Text;

namespace CMcG.CodeAlignment.Implementations
{
    internal class Edit : IEdit
    {
        private readonly ITextEdit m_edit;

        public Edit(ITextEdit edit)
        {
            this.m_edit = edit;
        }

        public System.Boolean Insert(ILine line, System.Int32 position, System.String text)
        {
            return this.m_edit.Insert(line.Position + position, text);
        }

        public void Commit()
        {
            _ = this.m_edit.Apply();
        }

        public void Dispose()
        {
            this.m_edit.Dispose();
        }
    }
}