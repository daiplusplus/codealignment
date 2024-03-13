using System;
using System.Runtime.InteropServices;

using Microsoft.VisualStudio.Shell;

namespace CMcG.CodeAlignment
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0.0.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.PackageGuidStr)]
    public sealed class CodeAlignmentPackage : Package
    {
    }
}
