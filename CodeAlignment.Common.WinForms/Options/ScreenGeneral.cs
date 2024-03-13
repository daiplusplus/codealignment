using System;
using System.Linq;
using System.Windows.Forms;
using CMcG.CodeAlignment.Properties;

namespace CMcG.CodeAlignment.Options
{
    public partial class ScreenGeneral : Controls.BaseUserControl
    {
        Business.CodeAlignmentOptions m_options;
        public ScreenGeneral(Business.CodeAlignmentOptions options)
        {
            this.InitializeComponent();
            this.m_options              = options;
            this.bindOptions.DataSource = this.m_options;
        }

        void ResetMruList(Object sender, EventArgs e)
        {
            Settings.Default.Delimiters = new System.Collections.Specialized.StringCollection();
            Settings.Default.Save();
        }

        void BackupSettings(Object sender, EventArgs e)
        {
            if (this.dlgSave.ShowDialog() == DialogResult.OK)
            {
                this.m_options.SaveAs(this.dlgSave.FileName);
            }
        }

        void RestoreSettings(Object sender, EventArgs e)
        {
            if (this.dlgLoad.ShowDialog() == DialogResult.OK)
            {
                this.m_options.LoadFrom(this.dlgLoad.FileName);
            }
        }
    }
}
