using System;
using System.Linq;
using System.Windows.Forms;
using Process = System.Diagnostics.Process;

namespace CMcG.CodeAlignment
{
    public partial class FormAbout : Controls.BaseForm
    {
        public FormAbout()
        {
            this.InitializeComponent();
            AboutData data = new AboutData();
            this.Text                = String.Format("About {0}",   data.AssemblyTitle);
            this.labelVersion  .Text = String.Format("Version {0}", data.AssemblyVersion);
            this.labelCopyright.Text = data.AssemblyCopyright;
            if (AboutData.Image != null)
            {
                this.ctlLogo.Image = AboutData.Image;
            }
        }

        void GoToWebsite(Object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.codealignment.com");
        }

        void Donate(Object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?hosted_button_id=AZZSHWMQ946V2&cmd=_s-xclick");
        }
    }
}