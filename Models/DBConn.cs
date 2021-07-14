using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPSVJson.Models
{
    public static class DBConn
    {
        private static SqlConnection conn;
        public static SqlConnection Conn 
        {
            get
            {
                return conn;
            }
            private set
            {
                conn = value;
            }
        }

        private static string conn_s;
        public static string Conn_s
        {
            get
            {
                return conn_s;
            }
            private set
            {
                conn_s = value;
            }
        }

        public static void Connect(string initialCatalog)
        {
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            //csb.DataSource = @"DESKTOP-J11IP9D\SQLEXPRESS";
            csb.DataSource = @"REGIONYSERVER\SQLEXPRESS";
            csb.InitialCatalog = initialCatalog;
            csb.IntegratedSecurity = true;
            Conn_s = csb.ConnectionString;

            Conn = new SqlConnection(Conn_s);
            Conn.Open();
            Console.WriteLine("Připojení k "+initialCatalog+"...OK");

            

        }
        public static void Disconnect()
        {
            conn.Close();                        
        }        
    }
}
