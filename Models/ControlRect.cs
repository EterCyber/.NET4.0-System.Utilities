namespace System.Utilities.Models
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ControlRect
    {
        public int Left;
        public int Top;
        public int Width;
        public int Height;
    }
}

