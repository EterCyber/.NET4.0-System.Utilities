namespace System.Utilities.Common
{
    using System;
    using System.IO.Ports;
    using System.Runtime.CompilerServices;

    public static class SerialPortHelper
    {
        private static string[] _baudRate;
        private static string[] _dataBits;
        private static string[] _parity;
        private static string[] _stopBits;

        public static void Close(this SerialPort serialPort, string portName)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        public static void Open(this SerialPort serialPort, string portName, string dateBits, string bondRate, string parity, string stopBit)
        {
            int result = -1;
            int num2 = -1;
            if (!int.TryParse(bondRate, out result))
            {
                throw new ArgumentException("bondRate");
            }
            if (!int.TryParse(dateBits, out num2))
            {
                throw new ArgumentException("dateBits");
            }
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            serialPort.PortName = portName;
            serialPort.BaudRate = result;
            serialPort.DataBits = num2;
            switch (stopBit)
            {
                case "1":
                    serialPort.StopBits = System.IO.Ports.StopBits.One;
                    break;

                case "1.5":
                    serialPort.StopBits = System.IO.Ports.StopBits.OnePointFive;
                    break;

                case "2":
                    serialPort.StopBits = System.IO.Ports.StopBits.Two;
                    break;

                default:
                    serialPort.StopBits = System.IO.Ports.StopBits.None;
                    break;
            }
            string str2 = parity;
            if (str2 != null)
            {
                switch (str2)
                {
                    case "偶":
                        serialPort.Parity = Parity.Even;
                        goto Label_011F;

                    case "奇":
                        serialPort.Parity = Parity.Odd;
                        goto Label_011F;

                    case "空格":
                        serialPort.Parity = Parity.Space;
                        goto Label_011F;

                    case "标志":
                        serialPort.Parity = Parity.Mark;
                        goto Label_011F;

                    case "无":
                        serialPort.Parity = Parity.None;
                        goto Label_011F;
                }
            }
            serialPort.Parity = Parity.None;
        Label_011F:
            serialPort.Open();
        }

        public static string[] BaudRates
        {
            get
            {
                if (_baudRate == null)
                {
                    _baudRate = new string[] { "600", "1200", "1800", "2400", "4800", "7200", "9600", "14400", "19200", "38400", "57600", "115200", "128000" };
                }
                return _baudRate;
            }
        }

        public static string[] DataBits
        {
            get
            {
                if (_dataBits == null)
                {
                    _dataBits = new string[] { "4", "5", "6", "7", "8" };
                }
                return _dataBits;
            }
        }

        public static string[] Paritys
        {
            get
            {
                return Enum.GetNames(typeof(Parity));
            }
        }

        public static string[] Paritys_CH
        {
            get
            {
                if (_parity == null)
                {
                    string[] names = Enum.GetNames(typeof(Parity));
                    _parity = new string[names.Length];
                    int index = 0;
                    foreach (string str in names)
                    {
                        if (string.Compare(str, "None", true) == 0)
                        {
                            _parity[index] = "无";
                        }
                        else if (string.Compare(str, "Odd", true) == 0)
                        {
                            _parity[index] = "奇";
                        }
                        else if (string.Compare(str, "Even", true) == 0)
                        {
                            _parity[index] = "偶";
                        }
                        else if (string.Compare(str, "Mark", true) == 0)
                        {
                            _parity[index] = "标志";
                        }
                        else if (string.Compare(str, "Space", true) == 0)
                        {
                            _parity[index] = "空格";
                        }
                        else
                        {
                            _parity[index] = str;
                        }
                        index++;
                    }
                }
                return _parity;
            }
        }

        public static string[] PortNames
        {
            get
            {
                return SerialPort.GetPortNames();
            }
        }

        public static string[] StopBits
        {
            get
            {
                return Enum.GetNames(typeof(System.IO.Ports.StopBits));
            }
        }

        public static string[] StopBits_CH
        {
            get
            {
                if (_stopBits == null)
                {
                    string[] names = Enum.GetNames(typeof(System.IO.Ports.StopBits));
                    int index = 0;
                    _stopBits = new string[names.Length - 1];
                    foreach (string str in names)
                    {
                        if (string.Compare(str, "None", true) != 0)
                        {
                            if (string.Compare(str, "One", true) == 0)
                            {
                                _stopBits[index] = "1";
                            }
                            else if (string.Compare(str, "Two", true) == 0)
                            {
                                _stopBits[index] = "2";
                            }
                            else if (string.Compare(str, "OnePointFive", true) == 0)
                            {
                                _stopBits[index] = "1.5";
                            }
                            else
                            {
                                _stopBits[index] = str;
                            }
                            index++;
                        }
                    }
                }
                return _stopBits;
            }
        }
    }
}

