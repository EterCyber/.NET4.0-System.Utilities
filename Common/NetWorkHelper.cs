namespace System.Utilities.Common
{
    using System;
    using System.Net;
    using System.Net.NetworkInformation;

    public class NetWorkHelper
    {
        public static string GetHostName()
        {
            return Dns.GetHostName();
        }

        public static string GetIPAddress(string ip)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(ip))
            {
                return str;
            }
            UriHostNameType type = Uri.CheckHostName(ip);
            if (type == UriHostNameType.Unknown)
            {
                Uri uri;
                if (Uri.TryCreate(string.Format("http://{0}", ip), UriKind.Absolute, out uri))
                {
                    str = IPAddressTryParse(uri.Host);
                }
                return str;
            }
            if ((type != UriHostNameType.IPv4) && (type != UriHostNameType.IPv6))
            {
                return str;
            }
            return IPAddressTryParse(ip);
        }

        public static string GetMacAddress(Func<string, string> macAddressFormatHanlder)
        {
            string str = string.Empty;
            foreach (NetworkInterface interface2 in NetworkInterface.GetAllNetworkInterfaces())
            {
                str = interface2.GetPhysicalAddress().ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    break;
                }
            }
            if (macAddressFormatHanlder != null)
            {
                str = macAddressFormatHanlder(str);
            }
            return str;
        }

        public static string GetMacAddress(NetworkInterfaceType networkType, Func<string, string> macAddressFormatHanlder)
        {
            string str = string.Empty;
            foreach (NetworkInterface interface2 in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (interface2.NetworkInterfaceType == networkType)
                {
                    str = interface2.GetPhysicalAddress().ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        break;
                    }
                }
            }
            if (macAddressFormatHanlder != null)
            {
                str = macAddressFormatHanlder(str);
            }
            return str;
        }

        public static string GetMacAddress(NetworkInterfaceType networkType, OperationalStatus status, Func<string, string> macAddressFormatHanlder)
        {
            string str = string.Empty;
            foreach (NetworkInterface interface2 in NetworkInterface.GetAllNetworkInterfaces())
            {
                if ((interface2.NetworkInterfaceType == networkType) && (interface2.OperationalStatus == status))
                {
                    str = interface2.GetPhysicalAddress().ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        break;
                    }
                }
            }
            if (macAddressFormatHanlder != null)
            {
                str = macAddressFormatHanlder(str);
            }
            return str;
        }

        private static string IPAddressTryParse(string ip)
        {
            IPAddress address;
            string str = string.Empty;
            if (IPAddress.TryParse(ip, out address))
            {
                str = address.ToString();
            }
            return str;
        }
    }
}

