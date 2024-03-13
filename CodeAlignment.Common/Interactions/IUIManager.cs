using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CMcG.CodeAlignment.Interactions
{
    public interface IUIManager
    {
        IKeyGrabber GetKeyGrabber(AlignmentViewModel viewModel);

        IAlignmentDetails PromptForAlignment(Boolean alignFromCaret);
    }

    public interface IKeyGrabber : IDisposable
    {
        AlignmentViewModel ViewModel { get; set; }

        void Display();
        void SetBounds(Rectangle bounds);
    }

    public interface IAlignmentDetails
    {
        String  Delimiter      { get; }
        Boolean AlignFromCaret { get; set; }
        Boolean UseRegex       { get; set; }
    }
}
