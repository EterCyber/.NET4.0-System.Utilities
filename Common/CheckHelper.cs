namespace System.Utilities.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public static class CheckHelper
    {
        public static bool InRange(this string data, int minValue, int maxValue)
        {
            bool flag = false;
            int result = -1;
            if (int.TryParse(data, out result))
            {
                flag = (result >= minValue) && (result <= maxValue);
            }
            return flag;
        }

        public static bool IsBase64(string data)
        {
            return (((data.Length % 4) == 0) && RegexHelper.IsMatch(data, "^[A-Z0-9/+=]*$"));
        }

        public static bool IsBinaryCodedDecimal(this string data)
        {
            return RegexHelper.IsMatch(data, "^([0-9]{2})+$");
        }

        public static bool IsBool(this object data)
        {
            switch (data.ToString().Trim().ToLower())
            {
                case "0":
                    return false;

                case "1":
                    return true;

                case "是":
                    return true;

                case "否":
                    return false;

                case "yes":
                    return true;

                case "no":
                    return false;
            }
            return false;
        }

        public static bool IsChinses(this string data)
        {
            return RegexHelper.IsMatch(data, "^[0-9]+[0-9]*$");
        }

        public static bool IsDate(this string data)
        {
            if (!string.IsNullOrEmpty(data) && RegexHelper.IsMatch(data, @"\d{4}([/-年])\d{1,2}([/-月])\d{1,2}([日])\s?\d{0,2}:?\d{0,2}:?\d{0,2}"))
            {
                DateTime time;
                data = data.Replace("年", "-");
                data = data.Replace("月", "-");
                data = data.Replace("日", " ");
                data = data.Replace("  ", " ");
                return DateTime.TryParse(data, out time);
            }
            return false;
        }

        public static bool IsEmail(this string data)
        {
            return RegexHelper.IsMatch(data, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
        }

        public static bool IsFilePath(this string data)
        {
            return RegexHelper.IsMatch(data, @"^(?<fpath>([a-zA-Z]:\\)([\s\.\-\w]+\\)*)(?<fname>[\w]+)(?<namext>(\.[\w]+)*)(?<suffix>\.[\w]+)");
        }

        public static bool IsHexString(string data)
        {
            return RegexHelper.IsMatch(data, @"\A\b[0-9a-fA-F]+\b\Z");
        }

        public static bool IsIdCard(string data)
        {
            return RegexHelper.IsMatch(data, @"^(11|12|13|14|15|21|22|23|31|32|33|34|35|36|37|41|42|43|44|45|46|50|51|52|53|54|61|62|63|64|65|71|81|82|91)(\d{13}|\d{15}[\dx])$");
        }

        public static bool IsImageFormat(byte[] data)
        {
            if ((data == null) || (data.Length < 4))
            {
                return false;
            }
            int length = data.Length;
            try
            {
                switch ((data[0].ToString() + data[1].ToString()).Trim())
                {
                    case "7173":
                    case "13780":
                        return true;
                }
                byte[] buffer = new byte[] { 0xff, 0xd8, 0xff, 0xd9 };
                return (((data[0] == buffer[0]) && (data[1] == buffer[1])) && ((data[length - 2] == buffer[2]) && (data[length - 1] == buffer[3])));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsInt(this string data)
        {
            return RegexHelper.IsMatch(data, "^[0-9]+[0-9]*$");
        }

        public static bool IsIp(this string data)
        {
            return RegexHelper.IsMatch(data, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        public static bool IsLatitude(this double data)
        {
            return ((data >= -90.0) && (data <= 90.0));
        }

        public static bool IsLen7MacAddress(this string data)
        {
            return RegexHelper.IsMatch(data, "[0-9a-fA-F]{2}-[0-9a-fA-F]{2}-[0-9a-fA-F]{2}-[0-9a-fA-F]{2}-[0-9a-fA-F]{2}-[0-9a-fA-F]{2}-[0-9a-fA-F]{2}");
        }

        public static bool IsLocalIp4(this string ipAddress)
        {
            bool flag = false;
            if ((string.IsNullOrEmpty(ipAddress) || !ipAddress.IsIp()) || ((!ipAddress.StartsWith("192.168.") && !ipAddress.StartsWith("172.")) && !ipAddress.StartsWith("10.")))
            {
                return flag;
            }
            return true;
        }

        public static bool IsLongitude(this double data)
        {
            return ((data >= -180.0) && (data <= 180.0));
        }

        public static bool IsNumber(this string data)
        {
            return RegexHelper.IsMatch(data, "^[0-9]+[0-9]*[.]?[0-9]*$");
        }

        public static bool IsPoseCode(this string data)
        {
            return RegexHelper.IsMatch(data, @"^\d{6}$");
        }

        public static bool IsURL(this string data)
        {
            return RegexHelper.IsMatch(data, @"^(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?$");
        }

        public static bool IsValidPort(this string port)
        {
            bool flag = false;
            int num = 0;
            int num2 = 0xffff;
            int result = -1;
            if (int.TryParse(port, out result))
            {
                flag = (result >= num) && (result <= num2);
            }
            return flag;
        }

        public static bool NotNull(object data)
        {
            return (data != null);
        }
    }
}

