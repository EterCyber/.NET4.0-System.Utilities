namespace System.Utilities.Models
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct TipMessage
    {
        public TipMessage(string caption, string message)
        {
            this = new TipMessage();
            this.Caption = caption;
            this.Message = message;
        }

        public string Caption { get; set; }
        public string Message { get; set; }
    }
}

