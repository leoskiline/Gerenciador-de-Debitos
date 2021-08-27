using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos
{
    public class Connection
    {
        private static string strConn = "Server=leonardocds15.sytes.net;Database=debitos;Uid=engenharia3;Pwd=bsifipp2sem2021";
        private static MySqlConnection connection;
        private static MySqlCommand command;
        private static Connection singleton;

        public MySqlConnection MySqlConnectionFactory
        {
            get { return connection; }
        }

        private Connection()
        {
        }


        public static Connection getConnection()
        {
            if (singleton == null)
            {
                singleton = new Connection();
                connection = new MySqlConnection(strConn);
                command = connection.CreateCommand();
                if (connection.State != ConnectionState.Open)
                    connection.Open();
            }
            return singleton;
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        public void ClearParameters()
        {
            command.Parameters.Clear();
        }


        public int ExecutarNonQuery(string sql, Dictionary<string,object> parameters = null)
        {
            command.CommandText = sql;
            if(parameters != null)
            {
                foreach (var item in parameters)
                {
                    command.Parameters.AddWithValue(item.Key, item.Value);
                }
            }
            int rows;
            try
            {
                rows = command.ExecuteNonQuery();
            }
            catch(MySqlException e)
            {
                rows = 0;
            }
            return rows;
        }

        public void addParameter(string param,string value)
        {
            command.Parameters.AddWithValue(param, value);
        }

        public string GetStrConn()
        {
            return strConn;
        }


        public DataTable ExecuteSelect(string sql, Dictionary<string, object> parameters = null)
        {
            DataTable tabMemoria = new DataTable();
            command.CommandText = sql;
            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    command.Parameters.AddWithValue(item.Key, item.Value);
                }
            }
            tabMemoria.Load(command.ExecuteReader());
            return tabMemoria;
        }

        public void SetStrConn(string host, string database, string username, string password)
        {
            strConn = $"Server={host};Database={database};Uid={username};Pwd={password}";
        }

       
    }
}
