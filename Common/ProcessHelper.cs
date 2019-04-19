namespace System.Utilities.Common
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public class ProcessHelper
    {
        public static void ExecBatCommand(Action<Action<string>> inputAction)
        {
            Process process = null;
            try
            {
                process = new Process {
                    StartInfo = { 
                        FileName = "cmd.exe",
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    }
                };
                process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                process.ErrorDataReceived += (sender, e) => Console.WriteLine(e.Data);
                process.Start();
                Action<string> action = null;
                using (StreamWriter writer = process.StandardInput)
                {
                    writer.AutoFlush = true;
                    process.BeginOutputReadLine();
                    if (action == null)
                    {
                        action = value => writer.WriteLine(value);
                    }
                    inputAction(action);
                }
                process.WaitForExit();
            }
            finally
            {
                if ((process != null) && !process.HasExited)
                {
                    process.Kill();
                }
                if (process != null)
                {
                    process.Close();
                }
            }
        }
    }
}

