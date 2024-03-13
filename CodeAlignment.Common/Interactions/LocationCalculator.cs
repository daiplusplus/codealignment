using System;
using System.Linq;
using System.Drawing;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace CMcG.CodeAlignment.Interactions
{
    public class LocationCalculator
    {
        public Rectangle CalculateBounds(IntPtr handle, Point offset)
        {
            Boolean ok = GetWindowRect(handle, out RECT rect);
            if (!ok) throw new Win32Exception();

            return new Rectangle(
                x      : rect.Left + offset.X,
                y      : rect.Bottom - 20 + offset.Y,
                width  : rect.Right - rect.Left - 20,
                height : 20
            );
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern Boolean GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        struct RECT
        {
            public Int32 Left;
            public Int32 Top;
            public Int32 Right;
            public Int32 Bottom;
        }
    }
}
