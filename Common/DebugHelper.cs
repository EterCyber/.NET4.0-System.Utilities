namespace System.Utilities.Common
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public class DebugHelper
    {
        public static void ConsoleOutput()
        {
            TextWriterTraceListener listener = new TextWriterTraceListener(Console.Out);
            Debug.Listeners.Add(listener);
        }

        public static void FileOutput()
        {
            FileOutput(string.Empty);
        }

        public static void FileOutput(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = DateTime.Now.ToString("yyyyMMdd") + ".log";
            }
            TextWriterTraceListener listener = new TextWriterTraceListener(File.CreateText(path));
            Debug.Listeners.Add(listener);
        }
    }
}

