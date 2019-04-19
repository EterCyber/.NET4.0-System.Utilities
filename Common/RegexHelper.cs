namespace System.Utilities.Common
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    public class RegexHelper
    {
        public static bool IsMatch(string input, string pattern)
        {
            return IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        public static bool IsMatch(string input, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }

        public static bool IsMatch(string input, string pattern, out Match result)
        {
            bool success = false;
            result = null;
            if (!string.IsNullOrEmpty(input))
            {
                result = new Regex(pattern).Match(input);
                success = result.Success;
            }
            return success;
        }
    }
}

