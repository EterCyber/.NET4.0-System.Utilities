namespace System.Utilities.Base
{
    using System.Utilities.Enums;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Text;

    [Serializable]
    public class Warning : Exception
    {
        private readonly string _message;

        public Warning(Exception exception) : this("", "", LogLevel.Notice, exception)
        {
        }

        public Warning(string message) : this(message, "")
        {
        }

        public Warning(string message, string code) : this(message, code, LogLevel.Notice)
        {
        }

        public Warning(string message, string code, LogLevel level) : this(message, code, level, null)
        {
        }

        public Warning(string message, string code, Exception exception) : this(message, code, LogLevel.Notice, exception)
        {
        }

        public Warning(string message, string code, LogLevel level, Exception exception) : base(message ?? "", exception)
        {
            this.Code = code;
            this.Level = level;
            this._message = this.GetMessage();
            this.CustomeMessage = message;
        }

        private void AppendInnerMessage(StringBuilder result, Exception exception)
        {
            if (exception != null)
            {
                if (exception is Warning)
                {
                    result.AppendLine(exception.Message);
                }
                else
                {
                    result.AppendLine(exception.Message);
                    result.Append(this.GetData(exception));
                    this.AppendInnerMessage(result, exception.InnerException);
                }
            }
        }

        private void AppendSelfMessage(StringBuilder result)
        {
            if (!string.IsNullOrEmpty(base.Message))
            {
                result.AppendLine(base.Message);
            }
        }

        private string GetData(Exception ex)
        {
            StringBuilder builder = new StringBuilder();
            foreach (DictionaryEntry entry in ex.Data)
            {
                builder.AppendFormat("{0}：{1}{2}", entry.Key, entry.Value, Environment.NewLine);
            }
            return builder.ToString();
        }

        private string GetMessage()
        {
            StringBuilder result = new StringBuilder();
            this.AppendSelfMessage(result);
            this.AppendInnerMessage(result, base.InnerException);
            return result.ToString().TrimEnd(Environment.NewLine.ToCharArray());
        }

        public string Code { get; set; }

        public string CustomeMessage { get; set; }

        public LogLevel Level { get; set; }

        public override string Message
        {
            get
            {
                if (this.Data.Count == 0)
                {
                    return this._message;
                }
                return (this._message + Environment.NewLine + this.GetData(this));
            }
        }

        public string Message_Code
        {
            get
            {
                return string.Format("{0}{1}错误代码：{2}", this.CustomeMessage, Environment.NewLine, this.Code);
            }
        }

        public override string StackTrace
        {
            get
            {
                if (!string.IsNullOrEmpty(base.StackTrace))
                {
                    return base.StackTrace;
                }
                if (base.InnerException == null)
                {
                    return string.Empty;
                }
                return base.InnerException.StackTrace;
            }
        }

        public string TraceId { get; set; }
    }
}

