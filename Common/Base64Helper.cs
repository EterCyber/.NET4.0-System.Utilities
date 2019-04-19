namespace System.Utilities.Common
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class Base64Helper
    {
        public static string Base64Decode(this string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(data));
            }
            return data;
        }

        public static string Base64Encode(this string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
            }
            return data;
        }
    }
}

