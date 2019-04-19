namespace System.Utilities.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class JsonHelper
    {
        private static string HanlderJsonString(string data)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                char ch = data.ToCharArray()[i];
                switch (ch)
                {
                    case '/':
                    {
                        builder.Append(@"\/");
                        continue;
                    }
                    case '\\':
                    {
                        builder.Append(@"\\");
                        continue;
                    }
                    case '\b':
                    {
                        builder.Append(@"\b");
                        continue;
                    }
                    case '\t':
                    {
                        builder.Append(@"\t");
                        continue;
                    }
                    case '\n':
                    {
                        builder.Append(@"\n");
                        continue;
                    }
                    case '\f':
                    {
                        builder.Append(@"\f");
                        continue;
                    }
                    case '\r':
                    {
                        builder.Append(@"\r");
                        continue;
                    }
                    case '"':
                    {
                        builder.Append("\\\"");
                        continue;
                    }
                }
                builder.Append(ch);
            }
            return builder.ToString();
        }

        private static string StringFormat(string data, Type type)
        {
            if (type == typeof(string))
            {
                data = HanlderJsonString(data);
                data = "\"" + data + "\"";
                return data;
            }
            if (type == typeof(DateTime))
            {
                data = "\"" + data + "\"";
                return data;
            }
            if (type == typeof(bool))
            {
                data = data.ToLower();
                return data;
            }
            if ((type != typeof(string)) && string.IsNullOrEmpty(data))
            {
                data = "\"" + data + "\"";
            }
            return data;
        }

        public static string ToJson<T>(this IList<T> list) where T: class
        {
            object obj2 = list[0];
            return list.ToJson<T>(obj2.GetType().Name);
        }

        public static string ToJson(DbDataReader dataReader)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            while (dataReader.Read())
            {
                builder.Append("{");
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    Type fieldType = dataReader.GetFieldType(i);
                    string name = dataReader.GetName(i);
                    string data = dataReader[i].ToString();
                    builder.Append("\"" + name + "\":");
                    data = StringFormat(data, fieldType);
                    if (i < (dataReader.FieldCount - 1))
                    {
                        builder.Append(data + ",");
                    }
                    else
                    {
                        builder.Append(data);
                    }
                }
                builder.Append("},");
            }
            dataReader.Close();
            builder.Remove(builder.Length - 1, 1);
            builder.Append("]");
            return builder.ToString();
        }

        public static string ToJson(this DataSet dataSet)
        {
            string str = "{";
            foreach (DataTable table in dataSet.Tables)
            {
                string str2 = str;
                str = str2 + "\"" + table.TableName + "\":" + table.ToJson() + ",";
            }
            return (str.TrimEnd(new char[] { ',' }) + "}");
        }

        public static string ToJson(this DataTable table)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            DataRowCollection rows = table.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                builder.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    string columnName = table.Columns[j].ColumnName;
                    string data = rows[i][j].ToString();
                    Type dataType = table.Columns[j].DataType;
                    builder.Append("\"" + columnName + "\":");
                    data = StringFormat(data, dataType);
                    if (j < (table.Columns.Count - 1))
                    {
                        builder.Append(data + ",");
                    }
                    else
                    {
                        builder.Append(data);
                    }
                }
                builder.Append("},");
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append("]");
            return builder.ToString();
        }

        public static string ToJson<T>(this IList<T> list, string jsonName)
        {
            StringBuilder builder = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
            {
                T local2 = list[0];
                jsonName = local2.GetType().Name;
            }
            builder.Append("{\"" + jsonName + "\":[");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    PropertyInfo[] properties = Activator.CreateInstance<T>().GetType().GetProperties();
                    builder.Append("{");
                    for (int j = 0; j < properties.Length; j++)
                    {
                        Type type = properties[j].GetValue(list[i], null).GetType();
                        builder.Append("\"" + properties[j].Name.ToString() + "\":" + StringFormat(properties[j].GetValue(list[i], null).ToString(), type));
                        if (j < (properties.Length - 1))
                        {
                            builder.Append(",");
                        }
                    }
                    builder.Append("}");
                    if (i < (list.Count - 1))
                    {
                        builder.Append(",");
                    }
                }
            }
            builder.Append("]}");
            return builder.ToString();
        }

        public static string ToJson(this DataTable dataTable, string jsonName)
        {
            StringBuilder builder = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
            {
                jsonName = dataTable.TableName;
            }
            builder.Append("{\"" + jsonName + "\":[");
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    builder.Append("{");
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        Type type = dataTable.Rows[i][j].GetType();
                        builder.Append("\"" + dataTable.Columns[j].ColumnName.ToString() + "\":" + StringFormat(dataTable.Rows[i][j].ToString(), type));
                        if (j < (dataTable.Columns.Count - 1))
                        {
                            builder.Append(",");
                        }
                    }
                    builder.Append("}");
                    if (i < (dataTable.Rows.Count - 1))
                    {
                        builder.Append(",");
                    }
                }
            }
            builder.Append("]}");
            return builder.ToString();
        }
    }
}

