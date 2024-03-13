using System;
using System.Linq;
using System.Windows.Forms;
using CMcG.CodeAlignment.Properties;

namespace CMcG.CodeAlignment
{
    public partial class FormCodeAlignment : Controls.BaseForm, Interactions.IAlignmentDetails
    {
        public FormCodeAlignment()
        {
            this.InitializeComponent();
            this.UseRegex = Settings.Default.UseRegex;
            if (Settings.Default.Delimiters == null)
            {
                Settings.Default.Delimiters = new System.Collections.Specialized.StringCollection();
                Settings.Default.Save();
            }

            foreach (String delimiter in Settings.Default.Delimiters)
            {
                this.cboDelimiter.Items.Add(delimiter);
            }

            if (this.cboDelimiter.Items.Count > 0)
            {
                this.cboDelimiter.SelectedIndex = 0;
            }
        }

        void Ok(Object sender = null, EventArgs e = null)
        {
            Settings.Default.Delimiters.Remove(this.cboDelimiter.Text);
            Settings.Default.Delimiters.Insert(0, this.cboDelimiter.Text);
            Settings.Default.UseRegex = this.UseRegex;
            Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        void Cancel(Object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        void ShowHelp(Object sender, System.ComponentModel.CancelEventArgs e)
        {
            new FormAbout().ShowDialog();
            e.Cancel = true;
        }

        void ShowOptions(Object sender, EventArgs e)
        {
            new Options.FormOptions().ShowDialog(this);
        }

        public String Delimiter
        {
            get { return this.cboDelimiter.Text; }
        }

        public Boolean AlignFromCaret
        {
            get { return this.chkAlignFromCaret.Checked || (Control.ModifierKeys == Keys.Shift); }
            set { this.chkAlignFromCaret.Checked = value; }
        }

        public Boolean UseRegex
        {
            get { return this.chkUseRegex.Checked; }
            set { this.chkUseRegex.Checked = value; }
        }
    }
}