namespace System.Utilities.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Text;

    public class OLEDBExcelHelper
    {
        private string _ExcelConnectString = string.Empty;
        private string _ExcelExtension = string.Empty;
        private string _ExcelPath = string.Empty;
        private static bool _X64Version = false;
        private static readonly string xls = ".xls";
        private static readonly string xlsx = ".xlsx";

        public OLEDBExcelHelper(string excelPath, bool x64Version)
        {
            this._ExcelExtension = Path.GetExtension(excelPath).ToLower();
            this._ExcelPath = excelPath;
            _X64Version = x64Version;
            this._ExcelConnectString = this.BuilderConnectionString();
        }

        private string BuilderConnectionString()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (!this._ExcelExtension.Equals(xlsx) && !this._ExcelExtension.Equals(xls))
            {
                throw new ArgumentException("excelPath");
            }
            if (!_X64Version)
            {
                if (this._ExcelExtension.Equals(xlsx))
                {
                    dictionary["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
                    dictionary["Extended Properties"] = "'Excel 12.0 XML;IMEX=1'";
                }
                else if (this._ExcelExtension.Equals(xls))
                {
                    dictionary["Provider"] = "Microsoft.Jet.OLEDB.4.0";
                    dictionary["Extended Properties"] = "'Excel 8.0;IMEX=1'";
                }
            }
            else
            {
                dictionary["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
                dictionary["Extended Properties"] = "'Excel 12.0 XML;IMEX=1'";
            }
            dictionary["Data Source"] = this._ExcelPath;
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in dictionary)
            {
                builder.Append(pair.Key);
                builder.Append('=');
                builder.Append(pair.Value);
                builder.Append(';');
            }
            return builder.ToString();
        }

        public DataSet ExecuteDataSet()
        {
            DataSet set = null;
            OleDbConnection connection = new OleDbConnection(this._ExcelConnectString);
            try
            {
                connection.Open();
                DataTable oleDbSchemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (oleDbSchemaTable != null)
                {
                    int num = 0;
                    set = new DataSet();
                    foreach (DataRow row in oleDbSchemaTable.Rows)
                    {
                        string str = row["TABLE_NAME"].ToString().Trim();
                        using (OleDbCommand command = new OleDbCommand(string.Format("select * from [{0}]", str), connection))
                        {
                            using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                            {
                                DataTable dataTable = new DataTable {
                                    TableName = str
                                };
                                adapter.Fill(dataTable);
                                set.Tables.Add(dataTable);
                            }
                        }
                        num++;
                    }
                }
                return set;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }
        }

        public DataTable ExecuteDataTable(string sql)
        {
            DataTable table2;
            OleDbConnection connection = new OleDbConnection(this._ExcelConnectString);
            try
            {
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        table2 = dataTable;
                    }
                }
            }
            catch (Exception)
            {
                table2 = null;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }
            return table2;
        }

        public int ExecuteNonQuery(string sql)
        {
            OleDbConnection connection = new OleDbConnection(this._ExcelConnectString);
            try
            {
                connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }
        }

        public string[] GetExcelSheetNames()
        {
            DataTable oleDbSchemaTable = null;
            string[] strArray2;
            using (OleDbConnection connection = new OleDbConnection(this._ExcelConnectString))
            {
                try
                {
                    connection.Open();
                    oleDbSchemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string[] strArray = new string[oleDbSchemaTable.Rows.Count];
                    int index = 0;
                    foreach (DataRow row in oleDbSchemaTable.Rows)
                    {
                        strArray[index] = row["TABLE_NAME"].ToString().Trim();
                        index++;
                    }
                    strArray2 = strArray;
                }
                catch (Exception)
                {
                    strArray2 = null;
                }
                finally
                {
                    if (oleDbSchemaTable != null)
                    {
                        oleDbSchemaTable.Dispose();
                    }
                }
            }
            return strArray2;
        }
    }
}

