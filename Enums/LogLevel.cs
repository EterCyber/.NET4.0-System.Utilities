namespace System.Utilities.Enums
{
    using System;
    using System.ComponentModel;

    public enum LogLevel
    {
        [Description("后台调试")]
        BackgroundDebug = 10,
        [Description("后台错误")]
        BackgroundError = 7,
        [Description("后台致命错误")]
        BackgroundFatal = 6,
        [Description("后台信息")]
        BackgroundInformation = 9,
        [Description("后台提醒")]
        BackgroundNotice = 11,
        [Description("后台警告")]
        BackgroundWarning = 8,
        [Description("调试")]
        Debug = 4,
        [Description("错误")]
        Error = 1,
        [Description("致命错误")]
        Fatal = 0,
        [Description("信息")]
        Information = 3,
        [Description("提醒")]
        Notice = 5,
        [Description("警告")]
        Warning = 2
    }
}

