namespace System.Utilities.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    public class INIHelper
    {
        private static string FilePath = null;
        private static string ReadDefaultValue = string.Empty;

        public INIHelper(string filePath)
        {
            FilePath = filePath;
        }

        public bool Exist()
        {
            return (!string.IsNullOrEmpty(FilePath) && File.Exists(FilePath));
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public string ReadValue(string Section, string Key)
        {
            StringBuilder retVal = new StringBuilder(500);
            GetPrivateProfileString(Section, Key, ReadDefaultValue, retVal, 500, FilePath);
            return retVal.ToString();
        }

        public string ReadValue(string Section, string Key, string defaultValue)
        {
            StringBuilder retVal = new StringBuilder(500);
            GetPrivateProfileString(Section, Key, defaultValue, retVal, 500, FilePath);
            return retVal.ToString();
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        public void WriteValue<T>(string Section, T t) where T: class
        {
            foreach (KeyValuePair<string, string> pair in ReflectHelper.GetDisplayName<T>())
            {
                object obj2 = typeof(T).InvokeMember(pair.Key, BindingFlags.GetProperty, null, t, null);
                if (obj2 != null)
                {
                    this.WriteValue(Section, pair.Value, obj2.ToString());
                }
            }
        }

        public void WriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, FilePath);
        }
    }
}

