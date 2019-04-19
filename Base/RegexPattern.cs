namespace System.Utilities.Base
{
    using System;

    public class RegexPattern
    {
        public const string Base64Check = "^[A-Z0-9/+=]*$";
        public const string BinaryCodedDecimal = "^([0-9]{2})+$";
        public const string CarLicenseCheck = @"^([\u4e00-\u9fa5]|[A-Z]){1,2}[A-Za-z0-9]{1,2}-[0-9A-Za-z]{5}$";
        public const string ChineseCheck = @"^[\u4e00-\u9fa5]+$";
        public const string DateCheck = @"\d{4}([/-年])\d{1,2}([/-月])\d{1,2}([日])\s?\d{0,2}:?\d{0,2}:?\d{0,2}";
        public const string DecimalCheck = "^[0-9]+(.[0-9]{2})?$";
        public const string EmailCheck = @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$";
        public const string FileCheck = @"^(?<fpath>([a-zA-Z]:\\)([\s\.\-\w]+\\)*)(?<fname>[\w]+)(?<namext>(\.[\w]+)*)(?<suffix>\.[\w]+)";
        public const string HexStringCheck = @"\A\b[0-9a-fA-F]+\b\Z";
        public const string IdCardCheck = @"^(11|12|13|14|15|21|22|23|31|32|33|34|35|36|37|41|42|43|44|45|46|50|51|52|53|54|61|62|63|64|65|71|81|82|91)(\d{13}|\d{15}[\dx])$";
        public const string IntCheck = "^[0-9]+[0-9]*$";
        public const string IpCheck = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";
        public const string Len7MacAddressCheck = "[0-9a-fA-F]{2}-[0-9a-fA-F]{2}-[0-9a-fA-F]{2}-[0-9a-fA-F]{2}-[0-9a-fA-F]{2}-[0-9a-fA-F]{2}-[0-9a-fA-F]{2}";
        public const string LetterCheck = "^[A-Za-z]+$";
        public const string NumberCheck = "^[0-9]+[0-9]*[.]?[0-9]*$";
        public const string NumberWithoutZeroCheck = "^[A-Za-z]+$";
        public const string PassWordLengthCheck = @"^\d{6,18}$";
        public const string PostCodeCheck = @"^\d{6}$";
        public const string URLCheck = @"^(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?$";
    }
}

