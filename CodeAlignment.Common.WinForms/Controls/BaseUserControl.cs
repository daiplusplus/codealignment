using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace CMcG.CodeAlignment.Controls
{
    public partial class BaseUserControl : UserControl
    {
        public BaseUserControl()
        {
            this.Font = new Font(SystemFonts.MessageBoxFont.FontFamily, 9.0F);

            this.InitializeComponent();

            this.AutoScaleMode = AutoScaleMode.Font;
        }
    }
}
