using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
namespace Trimble_Transportation.Models
{
    public class SearchData
    {
        static SearchData db = null;
        private NpgsqlConnection conn=null;
        private string connstring = String.Format("Server=127.0.0.1;Port=5432;" +
                    "User Id=postgres;Password=pravin9398;Database=postgres;");
        private NpgsqlDataAdapter da = null;
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        string sql;
        private SearchData()
        {
            try
            {
                conn = new NpgsqlConnection(connstring);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public static SearchData getInstance()
        {
            if (db == null)
            {
                db = new SearchData();
                return db;
            }
            else
            {
                return db;
            }
        }
        public string getVehicleInfo()
        {
            try
            {
                conn.Open();
                sql = "select * from sample";
                da = new NpgsqlDataAdapter(sql, conn);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                conn.Close();
                return DataTableToJSONWithStringBuilder(dt);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public string getVehicleInfoVIN(string id)
        {
            try
            {
                // string id1 = id.ToString();
                conn.Open();
                sql = "select * from vehicle where type=" + id;
                da = new NpgsqlDataAdapter(sql, conn);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                conn.Close();
                return DataTableToJSONWithStringBuilder(dt);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }

    }
}
