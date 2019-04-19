namespace System.Utilities.Common
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class ExceptionHelper
    {
        public static string Builder_Auxiliary_ParamName_Message(this Exception ex, string auxiliaryText, Func<string, string> messageHanlder)
        {
            StringBuilder builder = new StringBuilder();
            if (ex is ArgumentException)
            {
                ArgumentException exception = (ArgumentException) ex;
                builder.AppendFormat("{0}{1} {2}", auxiliaryText, exception.ParamName, messageHanlder(exception.Message));
            }
            else
            {
                builder.AppendFormat("{0} {1}", auxiliaryText, messageHanlder(ex.Message));
            }
            return builder.ToString();
        }

        public static string FormatMessage(this Exception ex, bool isHideStackTrace)
        {
            StringBuilder builder = new StringBuilder();
            int num = 0;
            string str = string.Empty;
            while (ex != null)
            {
                if (num > 0)
                {
                    str = str + "  ";
                }
                builder.AppendLine(string.Format("{0}异常消息：{1}", str, ex.Message));
                builder.AppendLine(string.Format("{0}异常类型：{1}", str, ex.GetType().FullName));
                builder.AppendLine(string.Format("{0}异常方法：{1}", str, (ex.TargetSite == null) ? null : ex.TargetSite.Name));
                builder.AppendLine(string.Format("{0}异常源：{1}", str, ex.Source));
                if (!isHideStackTrace && (ex.StackTrace != null))
                {
                    builder.AppendLine(string.Format("{0}异常堆栈：{1}", str, ex.StackTrace));
                }
                if (ex.InnerException != null)
                {
                    builder.AppendLine(string.Format("{0}内部异常：", str));
                    num++;
                }
                ex = ex.InnerException;
            }
            return builder.ToString();
        }
    }
}

