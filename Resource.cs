namespace System.Utilities
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, DebuggerNonUserCode, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    internal class Resource
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Resource()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        internal static string Filter_GroupOperateError
        {
            get
            {
                return ResourceManager.GetString("Filter_GroupOperateError", resourceCulture);
            }
        }

        internal static string Filter_RuleFieldInTypeNotFound
        {
            get
            {
                return ResourceManager.GetString("Filter_RuleFieldInTypeNotFound", resourceCulture);
            }
        }

        internal static string Logging_CreateLogInstanceReturnNull
        {
            get
            {
                return ResourceManager.GetString("Logging_CreateLogInstanceReturnNull", resourceCulture);
            }
        }

        internal static string Mef_HttpContextItems_NotFoundRequestContainer
        {
            get
            {
                return ResourceManager.GetString("Mef_HttpContextItems_NotFoundRequestContainer", resourceCulture);
            }
        }

        internal static string ObjectExtensions_PropertyNameNotExistsInType
        {
            get
            {
                return ResourceManager.GetString("ObjectExtensions_PropertyNameNotExistsInType", resourceCulture);
            }
        }

        internal static string ObjectExtensions_PropertyNameNotFixedType
        {
            get
            {
                return ResourceManager.GetString("ObjectExtensions_PropertyNameNotFixedType", resourceCulture);
            }
        }

        internal static string ParameterCheck_Between
        {
            get
            {
                return ResourceManager.GetString("ParameterCheck_Between", resourceCulture);
            }
        }

        internal static string ParameterCheck_BetweenNotEqual
        {
            get
            {
                return ResourceManager.GetString("ParameterCheck_BetweenNotEqual", resourceCulture);
            }
        }

        internal static string ParameterCheck_DirectoryNotExists
        {
            get
            {
                return ResourceManager.GetString("ParameterCheck_DirectoryNotExists", resourceCulture);
            }
        }

        internal static string ParameterCheck_FileNotExists
        {
            get
            {
                return ResourceManager.GetString("ParameterCheck_FileNotExists", resourceCulture);
            }
        }

        internal static string ParameterCheck_Ip4Address
        {
            get
            {
                return ResourceManager.GetString("ParameterCheck_Ip4Address", resourceCulture);
            }
        }

        internal static string ParameterCheck_Match
        {
            get
            {
                return ResourceManager.GetString("ParameterCheck_Match", resourceCulture);
            }
        }

        internal static string ParameterCheck_Match2
        {
            get
            {
                return ResourceManager.GetString("ParameterCheck_Match2", resourceCulture);
            }
        }

        internal static string ParameterCheck_NotEmpty_Guid
        {
            get
            {
                return ResourceManager.GetString("ParameterCheck_NotEmpty_Guid", resourceCulture);
            }
        }

        internal static string ParameterCheck_NotEqual
        {
            get
            {
                return ResourceManager.GetString("ParameterCheck_NotEqual", resourceCulture);
            }
        }

        internal static string ParameterCheck_NotGreaterThan
        {
            get
            {
                return ResourceManager.GetString("ParameterCheck_NotGreaterThan", resourceCulture);
            }
        }

        internal static string ParameterCheck_NotGreaterThanOrEqual
        {
            get
            {
                return ResourceManager.GetString("ParameterCheck_NotGreaterThanOrEqual", resourceCulture);
            }
        }

        internal static string ParameterCheck_NotLessThan
        {
            get
            {
                return ResourceManager.GetString("ParameterCheck_NotLessThan", resourceCulture);
            }
        }

        internal static string ParameterCheck_NotLessThanOrEqual
        {
            get
            {
                return ResourceManager.GetString("ParameterCheck_NotLessThanOrEqual", resourceCulture);
            }
        }

        internal static string ParameterCheck_NotNull
        {
            get
            {
                return ResourceManager.GetString("ParameterCheck_NotNull", resourceCulture);
            }
        }

        internal static string ParameterCheck_NotNullOrEmpty_Collection
        {
            get
            {
                return ResourceManager.GetString("ParameterCheck_NotNullOrEmpty_Collection", resourceCulture);
            }
        }

        internal static string ParameterCheck_NotNullOrEmpty_String
        {
            get
            {
                return ResourceManager.GetString("ParameterCheck_NotNullOrEmpty_String", resourceCulture);
            }
        }

        internal static string ParameterCheck_Port
        {
            get
            {
                return ResourceManager.GetString("ParameterCheck_Port", resourceCulture);
            }
        }

        internal static string ParameterCheck_StringLength
        {
            get
            {
                return ResourceManager.GetString("ParameterCheck_StringLength", resourceCulture);
            }
        }

        internal static string ParameterCheck_Url
        {
            get
            {
                return ResourceManager.GetString("ParameterCheck_Url", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("System.Utilities.Resource", typeof(Resource).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static string Security_DES_KeyLenght
        {
            get
            {
                return ResourceManager.GetString("Security_DES_KeyLenght", resourceCulture);
            }
        }

        internal static string Security_RSA_Sign_HashType
        {
            get
            {
                return ResourceManager.GetString("Security_RSA_Sign_HashType", resourceCulture);
            }
        }
    }
}

