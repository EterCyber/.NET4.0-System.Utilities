namespace System.Utilities.Common
{
    using System;
    using System.Data;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class CSVHelper
    {
        public static DataTable ImportToTable(this DataTable table, string filePath, int startRowIndex)
        {
            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8, false))
            {
                int num = 0;
                while (reader.Peek() > -1)
                {
                    num++;
                    string str = reader.ReadLine();
                    if (num >= (startRowIndex + 1))
                    {
                        string[] strArray = str.Split(new char[] { ',' });
                        DataRow row = table.NewRow();
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            row[i] = strArray[i];
                        }
                        table.Rows.Add(row);
                    }
                }
                return table;
            }
        }

        public static bool ToCSV(this DataTable table, string filePath, string tableheader, string columname)
        {
            bool flag;
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
                    writer.WriteLine(tableheader);
                    writer.WriteLine(columname);
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            writer.Write(table.Rows[i][j].ToString());
                            writer.Write(",");
                        }
                        writer.WriteLine();
                    }
                    writer.Close();
                    flag = true;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }
    }
}

