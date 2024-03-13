using System;
using System.Linq;

namespace CMcG.CodeAlignment.Options
{
    public partial class ScreenSelectors : Controls.BaseUserControl
    {
        Business.Options m_options;
        public ScreenSelectors(Business.Options options)
        {
            this.InitializeComponent();
            this.m_options              = options;
            this.bindOptions.DataSource = this.m_options;
        }

        void RestoreDefaults(Object sender, EventArgs e)
        {
            this.m_options.ResetSelectorTypes();
            this.bindOptions.ResetBindings(false);
        }
    }
}
