﻿using System;
using System.Windows.Forms;
using CMcG.CodeAlignment.Business;

namespace CMcG.CodeAlignment.Options
{
    public partial class ShortcutLine : Controls.BaseUserControl
    {
        public ShortcutLine()
        {
            this.InitializeComponent();
            this.bindKeyLookup.DataSource = KeyLookup.GetKeys();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.Tag != null)
            {
                this.bindShortcut.DataSource = this.Tag;
            }
        }
    }
}