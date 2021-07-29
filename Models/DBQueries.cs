using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace MPSVJson.Models
{
    class DBQueries
    {    
        //SELECT
        public string Select(string selectWhat,string selectFromTable, string selectWhere,string orderby)
        {
            string select = "SELECT " + selectWhat + " FROM " + selectFromTable + " WHERE " + selectWhere + " ORDER BY " + orderby;
            string response = "";

            using (SqlCommand SQLComm = new SqlCommand(select, DBConn.Conn))
            {
                using (SqlDataReader dataReader = SQLComm.ExecuteReader())
                {

                    while (dataReader.Read()) // dokud neprojdeme všechny záznamy
                    {

                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            response += dataReader.GetName(i) + " = " + dataReader[i] + "|";
                        }
                        response += "\n";
                    }
                }
            }

            return response;
        }
        public string Select(string selectWhat, string selectFromTable, string selectWhere)
        {
            string select = "SELECT " + selectWhat + " FROM " + selectFromTable + " WHERE " + selectWhere;
            string response = "";

            using (SqlCommand SQLComm = new SqlCommand(select, DBConn.Conn))
            {
                using (SqlDataReader dataReader = SQLComm.ExecuteReader())
                {

                    while (dataReader.Read()) // dokud neprojdeme všechny záznamy
                    {

                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            response += dataReader.GetName(i) + " = " + dataReader[i] + "|";
                        }
                        response += "\n";
                    }
                }
            }

            return response;
        }
        public Dictionary<string,string> SelectDict(string selectWhat, string selectFromTable, string selectWhere)
        {
            string select = "SELECT " + selectWhat + " FROM " + selectFromTable + " WHERE " + selectWhere;
            Dictionary<string, string> response = new Dictionary<string, string>();

            using (SqlCommand SQLComm = new SqlCommand(select, DBConn.Conn))
            {
                using (SqlDataReader dataReader = SQLComm.ExecuteReader())
                {

                    while (dataReader.Read()) // dokud neprojdeme všechny záznamy
                    {
                        string key;
                        string value;
                        key = (string)dataReader[0];
                        value = (string)dataReader[1];
                        if(!response.ContainsKey(key))
                            response.Add(key, value);
                    }
                }
            }

            return response;
        }
        public List<Dictionary<string, object>> SelectAll(string selectWhat, string selectFromTable ,string where)
        {
            string select = "SELECT " + selectWhat + " FROM " + selectFromTable + where;

            List<Dictionary<string, object>> response = new List<Dictionary<string, object>>();

            using (SqlCommand SQLComm = new SqlCommand(select, DBConn.Conn))
            {
                using (SqlDataReader dataReader = SQLComm.ExecuteReader())
                {
                    int index = 0;
                    while (dataReader.Read()) 
                    {
                        Dictionary<string, object> next = new Dictionary<string, object>();
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            
                            string key;
                            object value;
                            
                            key = dataReader.GetName(i);                            
                            value = dataReader[i];

                            if (value != null)
                            {
                                next.Add(key, value);
                                response.Add(next);
                                index++;
                            }
                            
                        }
                        

                    }
                }
            }

            return response;
        }
        public int[] SelectInts(string selectWhat, string selectFromTable, string selectWhere)
        {
            string select = "SELECT " + selectWhat + " FROM " + selectFromTable + " WHERE " + selectWhere; ;
            List<int> response = new List<int>();

            using (SqlCommand SQLComm = new SqlCommand(select, DBConn.Conn))
            {
                using (SqlDataReader dataReader = SQLComm.ExecuteReader())
                {

                    while (dataReader.Read()) // dokud neprojdeme všechny záznamy
                    {

                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            response.Add((int)dataReader[i]);
                        }
                    }
                }
            }

            return response.ToArray();
        }
        public int[] SelectInts(string selectWhat, string selectFromTable)
        {
            string select = "SELECT " + selectWhat + " FROM " + selectFromTable;
            List<int> response = new List<int>();

            using (SqlCommand SQLComm = new SqlCommand(select, DBConn.Conn))
            {
                using (SqlDataReader dataReader = SQLComm.ExecuteReader())
                {

                    while (dataReader.Read()) // dokud neprojdeme všechny záznamy
                    {

                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            if(dataReader[i] != DBNull.Value)
                                response.Add((int)dataReader[i]);
                        }                        
                    }
                }
            }

            return response.ToArray();
        }
        
        
        public string Select(string selectWhat, string selectFromTable)
        {
            string select = "SELECT " + selectWhat + " FROM " + selectFromTable;
            string response = "";

            using (SqlCommand SQLComm = new SqlCommand(select, DBConn.Conn))
            {
                using (SqlDataReader dataReader = SQLComm.ExecuteReader())
                {

                    while (dataReader.Read()) // dokud neprojdeme všechny záznamy
                    {

                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            response += dataReader.GetName(i) + " = " + dataReader[i] + "|";
                        }
                        response += "\n";
                    }
                }
            }
            return response;
        }

        //INSERT       
        public void Insert(string insertToTable, string columnNames ,string values)
        {
            string insert = "INSERT INTO " + insertToTable +"("+columnNames+") VALUES(" + values + ")";
            using(SqlCommand SQLComm = new SqlCommand(insert,DBConn.Conn))
            {
                SQLComm.ExecuteNonQuery();
            }
        }
        

        //UPDATE
        public void Update(string updateTable, string updateColumnValue, string updateWhere)
        {
            string update = "UPDATE  " + updateTable + " SET " + updateColumnValue + " WHERE " + updateWhere;
            using (SqlCommand SQLComm = new SqlCommand(update, DBConn.Conn))
            {
                SQLComm.ExecuteNonQuery();
            }
        }        

        //DELETE
        public void Delete(string deleteFromTable, string deleteWhere)
        {
            string delete = "DELETE FROM " + deleteFromTable + " WHERE " + deleteWhere;
            using (SqlCommand SQLComm = new SqlCommand(delete, DBConn.Conn))
            {
                SQLComm.ExecuteNonQuery();
            }
        }       
    }
}
