namespace System.Utilities.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class TypeHelper
    {
        private static Type FindGenericType(this Type definition, Type type)
        {
            while ((type != null) && (type != typeof(object)))
            {
                if (type.IsGenericType && (type.GetGenericTypeDefinition() == definition))
                {
                    return type;
                }
                if (definition.IsInterface)
                {
                    foreach (Type type2 in type.GetInterfaces())
                    {
                        Type type3 = definition.FindGenericType(type2);
                        if (type3 != null)
                        {
                            return type3;
                        }
                    }
                }
                type = type.BaseType;
            }
            return null;
        }

        public static Type GetElementType(this Type enumerableType)
        {
            Type type = typeof(IEnumerable<>).FindGenericType(enumerableType);
            if (type != null)
            {
                return type.GetGenericArguments()[0];
            }
            return enumerableType;
        }

        public static Type GetNonNullableType(this Type type)
        {
            return Nullable.GetUnderlyingType(type);
        }

        public static bool IsEnumerableType(this Type enumerableType)
        {
            return (typeof(IEnumerable<>).FindGenericType(enumerableType) != null);
        }

        public static bool IsKindOfGeneric(this Type type, Type definition)
        {
            return (definition.FindGenericType(type) != null);
        }

        public static bool IsNullableType(this Type type)
        {
            return (((type != null) && type.IsGenericType) && (type.GetGenericTypeDefinition() == typeof(Nullable<>)));
        }
    }
}

