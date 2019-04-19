namespace System.Utilities.Common
{
    using System.Utilities.Base;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public static class SerializeHelper
    {
        public static IList<T> BinaryToCollection<T>(string path)
        {
            IFormatter formatter = new BinaryFormatter {
                Binder = new UBinder()
            };
            using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return (IList<T>) formatter.Deserialize(stream);
            }
        }

        public static DataTable BinaryToDataTable(string path)
        {
            IFormatter formatter = new BinaryFormatter {
                Binder = new UBinder()
            };
            using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return (DataTable) formatter.Deserialize(stream);
            }
        }

        public static T FromBinary<T>(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (T) formatter.Deserialize(stream);
            }
        }

        public static T FromBinaryFile<T>(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (T) formatter.Deserialize(stream);
            }
        }

        public static T FromXml<T>(string xml)
        {
            Encoding.Default.GetBytes(xml);
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T) serializer.Deserialize(stream);
            }
        }

        public static T FromXmlFile<T>(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T) serializer.Deserialize(stream);
            }
        }

        public static string SerializeString(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            StringBuilder builder = new StringBuilder();
            foreach (FieldInfo info in fields)
            {
                object obj2 = info.GetValue(obj);
                builder.Append(string.Concat(new object[] { info.Name, ":", obj2, ";" }));
            }
            return builder.ToString();
        }

        public static byte[] ToBinary(object data)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, data);
                stream.Seek(0L, SeekOrigin.Begin);
                return stream.ToArray();
            }
        }

        public static void ToBinary<T>(string savePath, IList<T> collection)
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, collection);
            }
        }

        public static void ToBinary(string savePath, DataTable datatable)
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, datatable);
            }
        }

        public static void ToBinaryFile(object data, string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                new BinaryFormatter().Serialize(stream, data);
            }
        }

        public static string ToXml(object data)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                new XmlSerializer(data.GetType()).Serialize((Stream) stream, data);
                stream.Seek(0L, SeekOrigin.Begin);
                return Encoding.Default.GetString(stream.ToArray());
            }
        }

        public static void ToXML<T>(string savePath, IList<T> collection)
        {
            using (Stream stream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
            {
                XmlTextWriter writer = new XmlTextWriter(stream, new UTF8Encoding(false)) {
                    Formatting = Formatting.Indented
                };
                new XmlSerializer(collection.GetType()).Serialize((XmlWriter) writer, collection);
                writer.Flush();
                writer.Close();
            }
        }

        public static void ToXML<T>(string savePath, T t)
        {
            using (Stream stream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
            {
                XmlTextWriter writer = new XmlTextWriter(stream, new UTF8Encoding(false)) {
                    Formatting = Formatting.Indented
                };
                new XmlSerializer(t.GetType()).Serialize((XmlWriter) writer, t);
                writer.Flush();
                writer.Close();
            }
        }

        public static void ToXmlFile(object data, string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                new XmlSerializer(data.GetType()).Serialize((Stream) stream, data);
            }
        }

        public static IList<T> XMLToCollection<T>(string path)
        {
            using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                return (IList<T>) serializer.Deserialize(stream);
            }
        }

        public static T XMLToObject<T>(string path) where T: class
        {
            using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T) serializer.Deserialize(stream);
            }
        }

        public static string XMLToString(string path)
        {
            XmlDocument document = new XmlDocument();
            document.Load(path);
            StringBuilder sb = new StringBuilder();
            StringWriter w = new StringWriter(sb);
            XmlTextWriter writer2 = new XmlTextWriter(w) {
                Formatting = Formatting.Indented
            };
            document.WriteContentTo(writer2);
            return sb.ToString();
        }
    }
}

