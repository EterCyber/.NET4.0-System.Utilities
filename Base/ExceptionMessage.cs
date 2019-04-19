namespace System.Utilities.Base
{
    using System.Utilities.Common;
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class ExceptionMessage
    {
        public ExceptionMessage(Exception ex, string userMessage, bool isHideStackTrace)
        {
            this.UserMessage = string.IsNullOrEmpty(userMessage) ? ex.Message : userMessage;
            StringBuilder builder = new StringBuilder();
            this.ExMessage = string.Empty;
            int num = 0;
            string str = string.Empty;
            while (ex != null)
            {
                if (num > 0)
                {
                    str = str + "    ";
                }
                this.ExMessage = ex.Message;
                builder.AppendLine(str + "消息： " + ex.Message);
                builder.AppendLine(str + "类型： " + ex.GetType().FullName);
                builder.AppendLine(str + "方法： " + ((ex.TargetSite == null) ? null : ex.TargetSite.Name));
                builder.AppendLine(str + "代码： " + ex.Source);
                if (!isHideStackTrace && (ex.StackTrace != null))
                {
                    builder.AppendLine(str + "堆栈： " + ex.StackTrace);
                }
                if (ex.InnerException != null)
                {
                    builder.AppendLine(str + "内部异常： ");
                    num++;
                }
                ex = ex.InnerException;
            }
            this.ErrorDetails = builder.ToString();
            builder.Clear();
        }

        public override string ToString()
        {
            return this.ErrorDetails;
        }

        public string ErrorDetails { get; private set; }

        public string ExMessage { get; private set; }

        public string UserMessage { get; set; }
    }
}

