using System;
using System.Linq;
using CMcG.CodeAlignment.Business;

namespace CMcG.CodeAlignment.Options
{
    public partial class ScreenShortcuts : Controls.BaseUserControl
    {
        public ScreenShortcuts(Business.Options context)
        {
            this.InitializeComponent();
            this.DataContext        = context;
            this.listShortcut.Items = this.DataContext.Shortcuts;
        }

        Business.Options DataContext { get; set; }

        void AddShortcut(Object sender, EventArgs e)
        {
            this.listShortcut.AddItem(new KeyShortcut());
        }

        void RestoreDefaults(Object sender, EventArgs e)
        {
            this.DataContext.ResetShortcuts();
            this.listShortcut.Items = this.DataContext.Shortcuts;
        }
    }
}