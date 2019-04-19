namespace System.Utilities.Common
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    public class FontHelper
    {
        [DllImport("gdi32")]
        public static extern int AddFontResource(string lpFileName);
        public static bool InstallFont(string fontSourcePath)
        {
            string fileName = FileHelper.GetFileName(fontSourcePath);
            string path = string.Format(@"{0}\fonts\{1}", Environment.GetEnvironmentVariable("WINDIR"), fileName);
            try
            {
                string fileNameOnly = FileHelper.GetFileNameOnly(path);
                if (!File.Exists(path) && File.Exists(fontSourcePath))
                {
                    File.Copy(fontSourcePath, path);
                    AddFontResource(path);
                    WriteProfileString("fonts", fileNameOnly + "(TrueType)", fileName);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);
        [DllImport("kernel32.dll", SetLastError=true)]
        private static extern int WriteProfileString(string lpszSection, string lpszKeyName, string lpszString);
    }
}

