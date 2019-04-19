namespace System.Utilities.Common
{
    using System.Utilities.Enums;
    using System;
    using System.Text.RegularExpressions;

    public static class HtmlHelper
    {
        private static Regex htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        public static string StripTags(string data, StripHtmlType type)
        {
            if (!string.IsNullOrEmpty(data))
            {
                switch (type)
                {
                    case StripHtmlType.CharArray:
                        data = StripTagsCharArray(data);
                        return data;

                    case StripHtmlType.RegexCompiled:
                        data = StripTagsRegexCompiled(data);
                        return data;

                    case StripHtmlType.Regex:
                        data = StripTagsRegex(data);
                        return data;
                }
            }
            return data;
        }

        private static string StripTagsCharArray(string data)
        {
            char[] chArray = new char[data.Length];
            int index = 0;
            bool flag = false;
            for (int i = 0; i < data.Length; i++)
            {
                char ch = data[i];
                switch (ch)
                {
                    case '<':
                        flag = true;
                        break;

                    case '>':
                        flag = false;
                        break;

                    default:
                        if (!flag)
                        {
                            chArray[index] = ch;
                            index++;
                        }
                        break;
                }
            }
            return new string(chArray, 0, index);
        }

        private static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        private static string StripTagsRegexCompiled(string source)
        {
            return htmlRegex.Replace(source, string.Empty);
        }
    }
}

