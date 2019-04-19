namespace System.Utilities.Base
{
    using System;
    using System.Reflection;
    using System.Runtime.Serialization;

    public class UBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            return Assembly.GetExecutingAssembly().GetType(typeName);
        }
    }
}

