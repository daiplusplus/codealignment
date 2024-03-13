using System;
using System.Linq;
using System.Drawing;

using CMcG.CodeAlignment.Business;
using CMcG.CodeAlignment.Interactions;

namespace CMcG.CodeAlignment
{
    public class AlignFunctions
    {
        Business.CodeAlignmentOptions m_options       = new Business.CodeAlignmentOptions();
        Point            m_keyGrabOffset = new Point(10, -50);

        public IUIManager UIManager { get; set; }
        public IDocument  Document  { get; set; }
        public IntPtr     Handle    { get; set; }

        public Point KeyGrabOffset
        {
            get { return this.m_keyGrabOffset; }
            set { this.m_keyGrabOffset = value; }
        }

        public void AlignBy(String alignDelimiter, Boolean alignFromCaret = false, Boolean useRegex = false, Boolean addSpace = false)
        {
            if (!String.IsNullOrEmpty(alignDelimiter))
            {
                this.CreateAlignment(useRegex).PerformAlignment(alignDelimiter, alignFromCaret ? this.Document.CaretColumn : 0, addSpace);
            }
        }

        public void AlignBy(Key key, Boolean forceFromCaret = false)
        {
            KeyShortcut shortcut = this.m_options.GetShortcut(key, this.Document.FileType);
            if (shortcut != null)
            {
                this.AlignBy(shortcut.Alignment, forceFromCaret || shortcut.AlignFromCaret, shortcut.UseRegex, shortcut.AddSpace);
            }
        }

        public void AlignByDialog(Boolean alignFromCaret = false)
        {
            IAlignmentDetails result = this.UIManager.PromptForAlignment(alignFromCaret);
            if (result != null)
            {
                this.AlignBy(result.Delimiter, result.AlignFromCaret, useRegex:result.UseRegex);
            }
        }

        private Alignment CreateAlignment(Boolean useRegex = false)
        {
            Alignment alignment = new Alignment
            {
                View = this.Document,
                UseIdeTabSettings = this.m_options.UseIdeTabSettings
            };

            if (this.m_options.XmlTypes.Contains(this.Document.FileType))
            {
                alignment.Selector = new XmlScopeSelector
                {
                    Start              = this.Document.StartSelectionLineNumber,
                    End                = this.Document.EndSelectionLineNumber
                };
            }
            else
            {
                alignment.Selector = new GeneralScopeSelector
                {
                    ScopeSelectorRegex = this.m_options.ScopeSelectorRegex,
                    Start              = this.Document.StartSelectionLineNumber,
                    End                = this.Document.EndSelectionLineNumber
                };
            }

            if (useRegex)
            {
                alignment.Finder = new RegexDelimiterFinder();
            }

            return alignment;
        }

        public void AlignByKey()
        {
            AlignmentViewModel viewModel = new AlignmentViewModel(this, this.CreateAlignment());
            Rectangle bounds = new LocationCalculator().CalculateBounds(this.Handle, this.KeyGrabOffset);

            using (IKeyGrabber grabber = this.UIManager.GetKeyGrabber(viewModel))
            {
                grabber.SetBounds(bounds);
                grabber.Display();
            }
        }
    }
}