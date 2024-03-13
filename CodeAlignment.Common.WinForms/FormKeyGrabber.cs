using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using CMcG.CodeAlignment.Business;

namespace CMcG.CodeAlignment
{
    public partial class FormKeyGrabber : Controls.BaseForm, Interactions.IKeyGrabber
    {
        Boolean m_isChained;
        public AlignmentViewModel ViewModel { get; set; }

        public FormKeyGrabber()
        {
            this.InitializeComponent();
        }

        void OnKeyDown(Object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                Disposed += (s, a) => this.ViewModel.AlignFromPosition();
                this.Close();
                return;
            }

            Key? key = this.GetKey(e);
            if (key == null)
            {
                return;
            }

            this.ViewModel.PerformAlign((Key)key, e.Shift);

            if (!e.Control)
            {
                this.Close();
            }
            else
            {
                this.m_isChained = true;
                this.lblDescription.Text = "Release Ctrl key to finish or press another shortcut.";
            }
        }

        Key? GetKey(KeyEventArgs e)
        {
            if (Enum.IsDefined(typeof(Key), (Int32)e.KeyCode))
            {
                return (Key)e.KeyCode;
            }

            if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                return (Key)(e.KeyCode - Keys.NumPad0 + Keys.D0);
            }

            if (e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.ControlKey)
            {
                return null;
            }
            else
            {
                this.Close();
                return null;
            }
        }

        const Int32  KEY_UP        = 257;
        const Int64 LEFT_CONTROL  = 3223126017,
                   RIGHT_CONTROL = 3239903233;

        protected override void WndProc(ref Message m)
        {
            if (this.m_isChained && m.Msg == KEY_UP)
            {
                Int64 value = m.LParam.ToInt64();
                if (value < 0)
                {
                    value = this.ToUnsigned(value);
                }

                if (value == LEFT_CONTROL || value == RIGHT_CONTROL)
                {
                    this.Close();
                }
            }

            base.WndProc(ref m);
        }

        Int64 ToUnsigned(Int64 intValue ) => intValue & 0xffffffffL;

        public void Display() => this.ShowDialog();

        public void SetBounds(Rectangle bounds)
        {
            this.Location = bounds.Location;
            this.Size     = bounds.Size;
        }
    }
}