using System;
using System.Linq;
using System.Collections;
using System.Windows.Forms;

namespace CMcG.CodeAlignment.Controls
{
    public partial class ListControl : BaseUserControl
    {
        Type  m_viewType;
        IList m_items;

        public ListControl()
        {
            this.InitializeComponent();
            this.AutoScroll = true;
        }

        public Type ViewType
        {
            get { return this.m_viewType; }
            set
            {
                this.m_viewType = value;
                this.RefreshList();
            }
        }

        public IList Items
        {
            get { return this.m_items; }
            set
            {
                this.m_items = value;
                this.RefreshList();
            }
        }

        public void RefreshList()
        {
            foreach (Control ctl in this.Controls)
            {
                ctl.Dispose();
            }

            this.Controls.Clear();

            if (this.ViewType == null)
            {
                return;
            }

            if (this.Items == null)
            {
                this.Controls.Add(this.CreateChild());
            }
            else
            {
                this.Controls.AddRange(this.Items.Cast<Object>().Select(this.CreateChild).ToArray());
            }
        }

        public void AddItem(Object context)
        {
            this.Items.Add(context);
            this.Controls.Add(this.CreateChild(context));
        }

        Control CreateChild(Object context = null)
        {
            Control item  = (Control)Activator.CreateInstance(this.ViewType);
            item.Tag  = context;
            item.Dock = DockStyle.Fill;

            BaseUserControl panel     = new BaseUserControl { Dock = DockStyle.Top, Height = item.Height };
            Button btnRemove = new Button { Text = "-", Dock = DockStyle.Right, Width = 30 };

            btnRemove.Click += (s, e) => this.RemoveItem(context);
            panel.Controls.Add(item);
            panel.Controls.Add(btnRemove);

            return panel;
        }

        void RemoveItem(Object context)
        {
            this.Items.Remove(context);
            this.RefreshList();
        }
    }
}