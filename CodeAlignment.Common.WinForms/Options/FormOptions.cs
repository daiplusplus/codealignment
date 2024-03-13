using System;
using System.Windows.Forms;
using CMcG.CodeAlignment.Business;

namespace CMcG.CodeAlignment.Options
{
    public partial class FormOptions : Controls.BaseForm
    {
        Business.CodeAlignmentOptions m_options = new Business.CodeAlignmentOptions();
        public FormOptions()
        {
            this.InitializeComponent();
        }

        void ShowScreen(Object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Name)
            {
                case "nodeGeneral"       :
                this.DisplayScreen(new ScreenGeneral  (this.m_options )); break;
                case "nodeShortcuts"     :
                this.DisplayScreen(new ScreenShortcuts(this.m_options )); break;
                case "nodeAutoSelection" :
                this.DisplayScreen(new ScreenSelectors(this.m_options )); break;
                default                  : break;
            }
        }

        void DisplayScreen(Control ctl)
        {
            foreach (Control control in this.pnlMain.Controls)
            {
                control.Dispose();
            }

            this.pnlMain.Controls.Clear();
            ctl.Dock = DockStyle.Fill;
            this.pnlMain.Controls.Add(ctl);
        }

        void Cancel(Object sender, EventArgs e)
        {
            this.Close();
        }

        void Ok(Object sender, EventArgs e)
        {
            this.m_options.Save();
            this.Close();
        }
    }
}