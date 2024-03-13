using System.ComponentModel.Composition;

using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;

namespace CMcG.CodeAlignment
{
    [Export(typeof(IVsTextViewCreationListener))]
    [ContentType("text")]
    [TextViewRole(PredefinedTextViewRoles.Editable)]
    class VsTextViewCreationListener : IVsTextViewCreationListener
    {
#pragma warning disable IDE0044 // Add readonly modifier // This field is set by MEF.
        [Import]
        private IVsEditorAdaptersFactoryService AdaptersFactory = null;
#pragma warning restore IDE0044

        static VsTextViewCreationListener()
        {
            AboutData.MainAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            AboutData.Image        = VSPackage.CodeAlignmentVSBanner;
        }

        public void VsTextViewCreated(IVsTextView textViewAdapter)
        {
            IWpfTextView wpfTextView = this.AdaptersFactory.GetWpfTextView(textViewAdapter);
            if (wpfTextView != null)
            {
                CommandFilter.Register(textViewAdapter, new CodeAlignmentCommandFilter(textViewAdapter, wpfTextView));
            }
        }
    }
}