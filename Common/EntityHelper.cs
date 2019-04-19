namespace System.Utilities.Common
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class EntityHelper
    {
        public static bool CompareObject(object obj1, object obj2)
        {
            if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            if (obj1.GetType() != obj2.GetType())
            {
                return false;
            }
            return obj1.SerializeString().Equals(obj2.SerializeString());
        }

        public static T DeepCopy<T>(this T obj)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException(string.Format("该类型：{0}不支持序列化", typeof(T).FullName), "obj");
            }
            if (obj == null)
            {
                return default(T);
            }
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                stream.Seek(0L, SeekOrigin.Begin);
                return (T) formatter.Deserialize(stream);
            }
        }
    }
}

