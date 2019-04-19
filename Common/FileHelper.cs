namespace System.Utilities.Common
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Security.AccessControl;
    using System.Text.RegularExpressions;

    public static class FileHelper
    {
        private const string PATH_SPLIT_CHAR = @"\";

        public static void CopyFiles(string sourceDir, string targetDir, bool overWrite)
        {
            CopyFiles(sourceDir, targetDir, overWrite, false);
        }

        public static void CopyFiles(string sourceDir, string targetDir, bool overWrite, bool copySubDir)
        {
            foreach (string str in Directory.GetFiles(sourceDir))
            {
                string path = Path.Combine(targetDir, str.Substring(str.LastIndexOf(@"\") + 1));
                if (File.Exists(path))
                {
                    if (overWrite)
                    {
                        File.SetAttributes(path, FileAttributes.Normal);
                        File.Copy(str, path, overWrite);
                    }
                }
                else
                {
                    File.Copy(str, path, overWrite);
                }
            }
            if (copySubDir)
            {
                foreach (string str3 in Directory.GetDirectories(sourceDir))
                {
                    string str4 = Path.Combine(targetDir, str3.Substring(str3.LastIndexOf(@"\") + 1));
                    if (!Directory.Exists(str4))
                    {
                        Directory.CreateDirectory(str4);
                    }
                    CopyFiles(str3, str4, overWrite, true);
                }
            }
        }

        public static void CopyFiles(string sourceDir, string targetDir, bool overWrite, bool copySubDir, string backDir)
        {
            if (!Directory.Exists(backDir))
            {
                Directory.CreateDirectory(backDir);
            }
            foreach (string str in Directory.GetFiles(sourceDir))
            {
                string path = Path.Combine(targetDir, str.Substring(str.LastIndexOf(@"\") + 1));
                if (File.Exists(path))
                {
                    if (overWrite)
                    {
                        File.SetAttributes(path, FileAttributes.Normal);
                        string destFileName = Path.Combine(backDir, str.Substring(str.LastIndexOf(@"\") + 1));
                        File.Copy(path, destFileName, true);
                        File.Copy(str, path, overWrite);
                    }
                }
                else
                {
                    File.Copy(str, path, overWrite);
                }
            }
            if (copySubDir)
            {
                foreach (string str4 in Directory.GetDirectories(sourceDir))
                {
                    string str5 = Path.Combine(targetDir, str4.Substring(str4.LastIndexOf(@"\") + 1));
                    if (!Directory.Exists(str5))
                    {
                        Directory.CreateDirectory(str5);
                    }
                    string str6 = Path.Combine(backDir, str5.Substring(str5.LastIndexOf(@"\") + 1));
                    if (!Directory.Exists(str6))
                    {
                        Directory.CreateDirectory(str6);
                    }
                    CopyFiles(str4, str5, overWrite, true, str6);
                }
            }
        }

        public static bool CopyLocalBigFile(string fromPath, string toPath, int eachReadLength)
        {
            bool flag = true;
            FileStream stream = new FileStream(fromPath, FileMode.Open, FileAccess.Read);
            FileStream stream2 = new FileStream(toPath, FileMode.Append, FileAccess.Write);
            try
            {
                int num = 0;
                if (eachReadLength < stream.Length)
                {
                    byte[] buffer = new byte[eachReadLength];
                    long num2 = 0L;
                    while (num2 <= (stream.Length - eachReadLength))
                    {
                        num = stream.Read(buffer, 0, eachReadLength);
                        stream.Flush();
                        stream2.Write(buffer, 0, eachReadLength);
                        stream2.Flush();
                        stream2.Position = stream.Position;
                        num2 += num;
                    }
                    int count = (int) (stream.Length - num2);
                    num = stream.Read(buffer, 0, count);
                    stream.Flush();
                    stream2.Write(buffer, 0, count);
                    stream2.Flush();
                    return flag;
                }
                byte[] buffer2 = new byte[stream.Length];
                stream.Read(buffer2, 0, buffer2.Length);
                stream.Flush();
                stream2.Write(buffer2, 0, buffer2.Length);
                stream2.Flush();
                return flag;
            }
            catch (Exception)
            {
                flag = false;
            }
            finally
            {
                stream.Close();
                stream2.Close();
            }
            return flag;
        }

        public static bool CopyToBak(string filePath)
        {
            try
            {
                int num = 0;
                string path = "";
                do
                {
                    num++;
                    path = string.Format("{0}.{1}.bak", filePath, num);
                }
                while (File.Exists(path));
                File.Copy(filePath, path);
                File.Delete(filePath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void CreateDirectory(string targetDir)
        {
            DirectoryInfo info = new DirectoryInfo(targetDir);
            if (!info.Exists)
            {
                info.Create();
            }
        }

        public static void CreateDirectory(string parentDir, string subDirName)
        {
            CreateDirectory(parentDir + @"\" + subDirName);
        }

        public static bool CreatePath(string path)
        {
            bool flag = true;
            if (!string.IsNullOrEmpty(path) && !File.Exists(path))
            {
                try
                {
                    CreateDirectory(GetExceptName(path));
                    File.Create(path).Close();
                }
                catch (Exception)
                {
                    flag = false;
                }
            }
            return flag;
        }

        public static string CreateTempPath(string extension)
        {
            return Path.ChangeExtension(Path.GetTempFileName(), extension);
        }

        public static void DeleteDirectory(string targetDir)
        {
            DirectoryInfo info = new DirectoryInfo(targetDir);
            if (info.Exists)
            {
                DeleteFiles(targetDir, true);
                info.Delete(true);
            }
        }

        public static void DeleteFiles(string targetDir)
        {
            DeleteFiles(targetDir, false);
        }

        public static void DeleteFiles(string targetDir, bool delSubDir)
        {
            foreach (string str in Directory.GetFiles(targetDir))
            {
                File.SetAttributes(str, FileAttributes.Normal);
                File.Delete(str);
            }
            if (delSubDir)
            {
                DirectoryInfo info = new DirectoryInfo(targetDir);
                foreach (DirectoryInfo info2 in info.GetDirectories())
                {
                    DeleteFiles(info2.FullName, true);
                    info2.Delete();
                }
            }
        }

        public static void DeleteSubDirectory(string targetDir)
        {
            foreach (string str in Directory.GetDirectories(targetDir))
            {
                DeleteDirectory(str);
            }
        }

        public static string GetExceptEx(string path)
        {
            Match result = null;
            string str = string.Empty;
            if (!string.IsNullOrEmpty(path) && RegexHelper.IsMatch(path, @"^(?<fpath>([a-zA-Z]:\\)([\s\.\-\w]+\\)*)(?<fname>[\w]+)(?<namext>(\.[\w]+)*)(?<suffix>\.[\w]+)", out result))
            {
                str = result.Result("${fpath}") + result.Result("${fname}") + result.Result("${namext}");
            }
            return str;
        }

        public static string GetExceptName(string path)
        {
            Match result = null;
            string str = string.Empty;
            if (!string.IsNullOrEmpty(path) && RegexHelper.IsMatch(path, @"^(?<fpath>([a-zA-Z]:\\)([\s\.\-\w]+\\)*)(?<fname>[\w]+)(?<namext>(\.[\w]+)*)(?<suffix>\.[\w]+)", out result))
            {
                str = result.Result("${fpath}");
            }
            return str;
        }

        public static string GetFileEx(string path)
        {
            Match result = null;
            string str = string.Empty;
            if (!string.IsNullOrEmpty(path) && RegexHelper.IsMatch(path, @"^(?<fpath>([a-zA-Z]:\\)([\s\.\-\w]+\\)*)(?<fname>[\w]+)(?<namext>(\.[\w]+)*)(?<suffix>\.[\w]+)", out result))
            {
                str = result.Result("${suffix}");
            }
            return str;
        }

        public static string GetFileName(string path)
        {
            Match result = null;
            string str = string.Empty;
            if (!string.IsNullOrEmpty(path) && RegexHelper.IsMatch(path, @"^(?<fpath>([a-zA-Z]:\\)([\s\.\-\w]+\\)*)(?<fname>[\w]+)(?<namext>(\.[\w]+)*)(?<suffix>\.[\w]+)", out result))
            {
                str = result.Result("${fname}") + result.Result("${namext}") + result.Result("${suffix}");
            }
            return str;
        }

        public static string GetFileNameOnly(string path)
        {
            Match result = null;
            string str = string.Empty;
            if (!string.IsNullOrEmpty(path) && RegexHelper.IsMatch(path, @"^(?<fpath>([a-zA-Z]:\\)([\s\.\-\w]+\\)*)(?<fname>[\w]+)(?<namext>(\.[\w]+)*)(?<suffix>\.[\w]+)", out result))
            {
                str = result.Result("${fname}") + result.Result("${namext}");
            }
            return str;
        }

        public static double GetKBSize(string filePath)
        {
            double num = 0.0;
            long size = GetSize(filePath);
            if (size != 0L)
            {
                num = ((double) size) / 1024.0;
            }
            return num;
        }

        public static double GetMBSize(string filePath)
        {
            double num = 0.0;
            long size = GetSize(filePath);
            if (size != 0L)
            {
                num = ((double) size) / 1048576.0;
            }
            return num;
        }

        public static long GetSize(string filePath)
        {
            long length = 0L;
            try
            {
                if (File.Exists(filePath))
                {
                    FileStream stream = new FileStream(filePath, FileMode.Open);
                    length = stream.Length;
                    stream.Close();
                    stream.Dispose();
                }
            }
            catch (Exception)
            {
                length = 0L;
            }
            return length;
        }

        public static bool InvalidFileNameChars(this string fileName)
        {
            bool flag = true;
            if (!string.IsNullOrEmpty(fileName))
            {
                string str = new string(Path.GetInvalidFileNameChars());
                Regex regex = new Regex("[" + Regex.Escape(str) + "]");
                flag = !regex.IsMatch(fileName);
            }
            return flag;
        }

        public static void LoopFolder(string pathName, Action<FileInfo> fileRule)
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(pathName);
            while (queue.Count > 0)
            {
                string name = queue.Dequeue();
                DirectorySecurity security = new DirectorySecurity(name, AccessControlSections.Access);
                if (!security.AreAccessRulesProtected)
                {
                    DirectoryInfo info = new DirectoryInfo(name);
                    foreach (DirectoryInfo info2 in info.GetDirectories())
                    {
                        queue.Enqueue(info2.FullName);
                    }
                    foreach (FileInfo info3 in info.GetFiles())
                    {
                        fileRule(info3);
                    }
                }
            }
        }

        public static void MoveFiles(string sourceDir, string targetDir, bool overWrite)
        {
            MoveFiles(sourceDir, targetDir, overWrite, false);
        }

        public static void MoveFiles(string sourceDir, string targetDir, bool overWrite, bool moveSubDir)
        {
            foreach (string str in Directory.GetFiles(sourceDir))
            {
                string path = Path.Combine(targetDir, str.Substring(str.LastIndexOf(@"\") + 1));
                if (File.Exists(path))
                {
                    if (overWrite)
                    {
                        File.SetAttributes(path, FileAttributes.Normal);
                        File.Delete(path);
                        File.Move(str, path);
                    }
                }
                else
                {
                    File.Move(str, path);
                }
            }
            if (moveSubDir)
            {
                foreach (string str3 in Directory.GetDirectories(sourceDir))
                {
                    string str4 = Path.Combine(targetDir, str3.Substring(str3.LastIndexOf(@"\") + 1));
                    if (!Directory.Exists(str4))
                    {
                        Directory.CreateDirectory(str4);
                    }
                    MoveFiles(str3, str4, overWrite, true);
                    Directory.Delete(str3);
                }
            }
        }

        public static void OpenFolderAndFile(string path)
        {
            if (File.Exists(path) || Directory.Exists(path))
            {
                Process.Start(path);
            }
        }

        public static void StartupSet(string path, string keyName, bool set)
        {
            RegistryKey localMachine = Registry.LocalMachine;
            try
            {
                RegistryKey key2 = localMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                if (set)
                {
                    key2.SetValue(keyName, path);
                }
                else if (key2.GetValue(keyName) != null)
                {
                    key2.DeleteValue(keyName);
                }
            }
            finally
            {
                localMachine.Close();
            }
        }

        public static byte[] ToBytes(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, (int) stream.Length);
                    return buffer;
                }
            }
            return null;
        }

        public static void ToFile(byte[] bytes, string filePath)
        {
            File.WriteAllBytes(filePath, bytes);
        }
    }
}

