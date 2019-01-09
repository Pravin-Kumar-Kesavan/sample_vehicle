using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Trimble_Transportation.Models
{
    public class AddData
    {
        static AddData db = null;
        private NpgsqlConnection conn = null;
        private string connstring = String.Format("Server=127.0.0.1;Port=5432;" +
                    "User Id=postgres;Password=pravin9398;Database=postgres;");
        private NpgsqlDataAdapter da = null;
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        string sql;
        private AddData()
        {
            try
            {
                conn = new NpgsqlConnection(connstring);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public static AddData getInstance()
        {
            if (db == null)
            {
                db = new AddData();
                return db;
            }
            else
            {
                return db;
            }
        }
        public string addData(string v)
        {

            try
            {
                conn.Open();
                Console.WriteLine("data=" + v);
                sql = "insert into sample(vin) values('"+v+"')";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                conn.Close();
                return "inserted";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "not inserted";
            }
        }
    }
}
