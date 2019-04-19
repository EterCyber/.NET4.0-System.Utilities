namespace System.Utilities.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class DBHelper
    {
        public static DataTable AppendCSVRecord(this DataTable table, string filePath, int rowIndex)
        {
            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8, false))
            {
                int index = 0;
                int num2 = 0;
                reader.Peek();
                while (reader.Peek() > 0)
                {
                    num2++;
                    string str = reader.ReadLine();
                    if (num2 >= (rowIndex + 1))
                    {
                        string[] strArray = str.Split(new char[] { ',' });
                        DataRow row = table.NewRow();
                        for (index = 0; index < strArray.Length; index++)
                        {
                            row[index] = strArray[index];
                        }
                        table.Rows.Add(row);
                    }
                }
                return table;
            }
        }

        public static string BuilderMSSqlConnectString(string server, string database, string uid, string pwd)
        {
            return string.Format("Server={0};DataBase={1};uid={2};pwd={3}", new object[] { server, database, uid, pwd });
        }

        public static bool CheckBigint(string value, out long number)
        {
            number = -1L;
            return long.TryParse(value, out number);
        }

        public static bool CheckSmallint(string value, out short number)
        {
            number = -1;
            return short.TryParse(value, out number);
        }

        public static bool CheckTinyint(string value, out byte number)
        {
            number = 0;
            return byte.TryParse(value, out number);
        }

        private static DataTable CreateDbColumns(PropertyDescriptorCollection _props, DataTable _table)
        {
            if ((_table != null) && (_props != null))
            {
                for (int i = 0; i < _props.Count; i++)
                {
                    PropertyDescriptor descriptor = _props[i];
                    _table.Columns.Add(descriptor.Name, descriptor.PropertyType);
                }
            }
            return _table;
        }

        public static DataTable CreateTable(string columnsInfo)
        {
            DataTable table = new DataTable();
            foreach (string str3 in columnsInfo.Split(new char[] { ',' }))
            {
                string[] strArray2 = str3.Split(new char[] { '|' });
                string columnName = strArray2[0];
                if (strArray2.Length == 2)
                {
                    string columnType = strArray2[1];
                    table.Columns.Add(new DataColumn(columnName, Type.GetType(HandleColType(columnType))));
                }
                else
                {
                    table.Columns.Add(new DataColumn(columnName));
                }
            }
            return table;
        }

        private static DataTable FillDbToRows<T>(IList<T> data, PropertyDescriptorCollection _props, DataTable _table)
        {
            object[] values = new object[_props.Count];
            foreach (T local in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = _props[i].GetValue(local);
                }
                _table.Rows.Add(values);
            }
            return _table;
        }

        public static string FilterHtmlTag(this string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                data = Regex.Replace(data, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "-->", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "<!--.*", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "&(iexcl|#161);", "\x00a1", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "&(cent|#162);", "\x00a2", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "&(pound|#163);", "\x00a3", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "&(copy|#169);", "\x00a9", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, @"&#(\d+);", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "xp_cmdshell", "", RegexOptions.IgnoreCase);
            }
            return data;
        }

        public static string FilterSpecial(string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                data = data.Replace("<", "");
                data = data.Replace(">", "");
                data = data.Replace("*", "");
                data = data.Replace("-", "");
                data = data.Replace("?", "");
                data = data.Replace("'", "''");
                data = data.Replace(",", "");
                data = data.Replace("/", "");
                data = data.Replace(";", "");
                data = data.Replace("*/", "");
                data = data.Replace("\r\n", "");
            }
            return data;
        }

        public static string FilterSqlInject(string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                data = data.Replace("'", "''");
                data = data.Replace(";", "；");
                data = data.Replace("(", "（");
                data = data.Replace(")", "）");
                data = data.Replace("Exec", "");
                data = data.Replace("Execute", "");
                data = data.Replace("xp_", "x p_");
                data = data.Replace("sp_", "s p_");
                data = data.Replace("0x", "0 x");
            }
            return data;
        }

        public static string FilterSqlInjection(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace(";", "");
                str = str.Replace("'", "");
                str = str.Replace("&", "");
                str = str.Replace("%20", "");
                str = str.Replace("-", "");
                str = str.Replace("=", "");
                str = str.Replace("==", "");
                str = str.Replace("<", "");
                str = str.Replace(">", "");
                str = str.Replace("%", "");
                str = str.Replace(" or", "");
                str = str.Replace("or ", "");
                str = str.Replace(" and", "");
                str = str.Replace("and ", "");
                str = str.Replace(" not", "");
                str = str.Replace("not ", "");
                str = str.Replace("!", "");
                str = str.Replace("{", "");
                str = str.Replace("}", "");
                str = str.Replace("[", "");
                str = str.Replace("]", "");
                str = str.Replace("(", "");
                str = str.Replace(")", "");
                str = str.Replace("|", "");
                str = str.Replace("_", "");
            }
            return str;
        }

        public static string FilterString(this string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                data = data.FilterHtmlTag();
                data = data.FilterUnSafeSQL();
                data = FilterSpecial(data);
            }
            return data;
        }

        public static string FilterUnSafeSQL(this string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                data = data.Replace("'", "");
                data = data.Replace("\"", "");
                data = data.Replace("&", "&amp");
                data = data.Replace("<", "&lt");
                data = data.Replace(">", "&gt");
                data = Regex.Replace(data, "select", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "insert", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "delete from", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "count''", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "drop table", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "truncate", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "asc", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "mid", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "char", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "xp_cmdshell", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "exec master", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "net localgroup administrators", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "and", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "net user", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "or", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "net", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "-", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "delete", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "drop", "", RegexOptions.IgnoreCase);
                data = Regex.Replace(data, "script", "", RegexOptions.IgnoreCase);
            }
            return data;
        }

        public static int GetPageTotalCount(int recordCnt, int pageSize)
        {
            int num = recordCnt / pageSize;
            if ((recordCnt % pageSize) != 0)
            {
                num++;
            }
            return num;
        }

        public static object GroupByToSum(this DataTable datatable, string groupColumn, string groupValue, string sumColumn)
        {
            object obj2 = null;
            if (((datatable != null) && !string.IsNullOrEmpty(groupColumn)) && !string.IsNullOrEmpty(sumColumn))
            {
                obj2 = datatable.Compute("Sum(" + sumColumn + ")", groupColumn + "='" + groupValue + "'");
            }
            return obj2;
        }

        private static string HandleColType(string columnType)
        {
            switch (columnType.ToLower())
            {
                case "int":
                    return "System.Int32";

                case "string":
                    return "System.String";

                case "decimal":
                    return "System.Decimal";

                case "double":
                    return "System.Double";

                case "dateTime":
                    return "System.DateTime";

                case "bool":
                    return "System.Boolean";

                case "image":
                    return "System.Byte[]";

                case "object":
                    return "System.Object";
            }
            return "System.String";
        }

        public static string PageDBWithRowNumberString(string tableName, string columns, string orderColumn, int orderType, int pSize, int pIndex)
        {
            int num = (pSize * (pIndex - 1)) + 1;
            int num2 = (pSize * pIndex) + 1;
            return string.Format("select * from  (select (ROW_NUMBER() over(order by {2} {3})) as ROWNUMBER,{1}  from {0})as tp where ROWNUMBER >= {4} and ROWNUMBER< {5} ", new object[] { tableName, columns, orderColumn, (orderType == 1) ? "desc" : "asc", num, num2 });
        }

        public static string PageDBWithTopMaxString(string tableName, string columns, string orderColumn, int orderType, int pSize, int pIndex)
        {
            return string.Format("select top {4} {1} from {0} where {2}> ISNULL((select max ({2}) from (select top {5} {2} from {0} order by {2} {3}) as T),0) order by {2} {3}", new object[] { tableName, columns, orderColumn, (orderType == 1) ? "desc" : "asc", pSize, (pIndex - 1) * pSize });
        }

        public static string PageDBWithTopNotInString(string tableName, string columns, string orderColumn, int orderType, int pSize, int pIndex)
        {
            return string.Format("SELECT TOP {4} {1} FROM {0} WHERE ({2} NOT IN (SELECT TOP {5} {2} FROM {0} ORDER BY {2} {3})) ORDER BY {2} {3}", new object[] { tableName, columns, orderColumn, (orderType == 1) ? "desc" : "asc", pSize, (pIndex - 1) * pSize });
        }

        public static object Sum(this DataTable datatable, string sumColumn)
        {
            object obj2 = null;
            if ((datatable != null) && !string.IsNullOrEmpty(sumColumn))
            {
                obj2 = datatable.Compute("Sum(" + sumColumn + ")", string.Empty);
            }
            return obj2;
        }

        public static bool ToCSV(this DataTable table, string fullSavePath, string tableheader, string columname)
        {
            bool flag;
            try
            {
                string str = "";
                using (StreamWriter writer = new StreamWriter(fullSavePath, false, Encoding.UTF8))
                {
                    if (!string.IsNullOrEmpty(tableheader))
                    {
                        writer.WriteLine(tableheader);
                    }
                    if (!string.IsNullOrEmpty(columname))
                    {
                        writer.WriteLine(columname);
                    }
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        str = "";
                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            if (j > 0)
                            {
                                str = str + ",";
                            }
                            str = str + table.Rows[i][j].ToString();
                        }
                        writer.WriteLine(str);
                    }
                    flag = true;
                }
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            table = CreateDbColumns(properties, table);
            return FillDbToRows<T>(data, properties, table);
        }

        public static int ToInt(this DataRow row, int columnIndex, int failValue)
        {
            if (row != null)
            {
                int length = row.ItemArray.Length;
                if (columnIndex > length)
                {
                    throw new ArgumentException("columnIndex");
                }
                if (row.IsNull(columnIndex))
                {
                    int.TryParse(row[columnIndex].ToString(), out failValue);
                }
            }
            return failValue;
        }

        public static int ToInt(this DataRow row, string columnName, int failValue)
        {
            if (row != null)
            {
                if (!row.Table.Columns.Contains(columnName))
                {
                    throw new ArgumentException("columnName");
                }
                if (row.IsNull(columnName))
                {
                    int.TryParse(row[columnName].ToString(), out failValue);
                }
            }
            return failValue;
        }

        public static string ToString(this DataRow row, int columnIndex, string failValue)
        {
            if (row != null)
            {
                int length = row.ItemArray.Length;
                if (columnIndex > length)
                {
                    throw new ArgumentException("columnIndex");
                }
                failValue = row.IsNull(columnIndex) ? failValue : row[columnIndex].ToString().Trim();
            }
            return failValue;
        }

        public static string ToString(this DataRow row, string columnName, string failValue)
        {
            if (row != null)
            {
                failValue = row.IsNull(columnName) ? failValue : row[columnName].ToString();
            }
            return failValue;
        }
    }
}

