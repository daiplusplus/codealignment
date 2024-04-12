using System;
using CMcG.CodeAlignment.Business;

using Microsoft.VisualStudio.Text;

namespace CMcG.CodeAlignment.Implementations
{
    internal class Edit : IEdit
    {
        private readonly ITextEdit m_edit;

        public Edit(ITextEdit edit)
        {
            this.m_edit = edit ?? throw new ArgumentNullException(paramName: nameof(edit));
        }

        public Boolean Insert(ILine line, Int32 position, String text)
        {
            if(line is null) throw new ArgumentNullException(paramName: nameof(line));
            if(text is null) throw new ArgumentNullException(paramName: nameof(text));

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
