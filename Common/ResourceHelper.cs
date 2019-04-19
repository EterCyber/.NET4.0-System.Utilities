namespace System.Utilities.Common
{
    using System;
    using System.IO;
    using System.Reflection;

    public class ResourceHelper
    {
        public static bool ExtractEmbeddedResourceToFile(string resourceName, string filename)
        {
            bool flag = false;
            try
            {
                using (Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        using (FileStream stream2 = new FileStream(filename, FileMode.Create))
                        {
                            byte[] buffer = new byte[stream.Length];
                            stream.Read(buffer, 0, buffer.Length);
                            stream2.Write(buffer, 0, buffer.Length);
                            flag = true;
                        }
                    }
                    return flag;
                }
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }
    }
}

