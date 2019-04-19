namespace System.Utilities.Common
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class DESEncryptHelper
    {
        private string _IV;
        private string _Key;

        public DESEncryptHelper(string key, string iv)
        {
            this._Key = key;
            this._IV = iv;
        }

        public string Decrypt(string value)
        {
            return this.Decrypt(value, this._Key, this._IV);
        }

        private string Decrypt(string encryptString, string key, string iv)
        {
            string str;
            using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
            {
                byte[] buffer = new byte[encryptString.Length / 2];
                for (int i = 0; i < (encryptString.Length / 2); i++)
                {
                    int num2 = Convert.ToInt32(encryptString.Substring(i * 2, 2), 0x10);
                    buffer[i] = (byte) num2;
                }
                provider.Key = Encoding.ASCII.GetBytes(key);
                provider.IV = Encoding.ASCII.GetBytes(iv);
                using (MemoryStream stream = new MemoryStream())
                {
                    CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
                    try
                    {
                        stream2.Write(buffer, 0, buffer.Length);
                        stream2.FlushFinalBlock();
                        str = Encoding.Default.GetString(stream.ToArray());
                    }
                    catch (CryptographicException)
                    {
                        str = "N/A";
                    }
                    finally
                    {
                        if (stream2 != null)
                        {
                            stream2.Dispose();
                        }
                    }
                }
            }
            return str;
        }

        public string Encrypt(string value)
        {
            return this.Encrypt(value, this._Key, this._IV);
        }

        private string Encrypt(string encryptString, string key, string iv)
        {
            StringBuilder builder = new StringBuilder();
            using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
            {
                byte[] bytes = Encoding.Default.GetBytes(encryptString);
                provider.Key = Encoding.ASCII.GetBytes(key);
                provider.IV = Encoding.ASCII.GetBytes(iv);
                using (MemoryStream stream = new MemoryStream())
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        stream2.Write(bytes, 0, bytes.Length);
                        stream2.FlushFinalBlock();
                    }
                    foreach (byte num in stream.ToArray())
                    {
                        builder.AppendFormat("{0:X2}", num);
                    }
                }
            }
            return builder.ToString();
        }

        public bool ValidateString(string EnString, string DeString)
        {
            return this.Decrypt(EnString, this._Key, this._IV).Equals(DeString.ToString());
        }
    }
}

