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
        private string strConn;
        MySqlConnection connection;
        MySqlCommand command;

        public Connection()
        {
            this.strConn = "Server=127.0.0.1;Database=debitos;Uid=root;Pwd=7;du9T6xSm;<$}4c;";
            if (this.connection == null)
            {
                this.connection = new MySqlConnection(this.strConn);
                this.command = connection.CreateCommand();
            }
        }


        public void OpenConnection()
        {
            if (this.connection.State != System.Data.ConnectionState.Open)
                this.connection.Open();
        }

        public void CloseConnection()
        {
            this.connection.Close();
        }

        public void ClearParameters()
        {
            this.command.Parameters.Clear();
        }


        public int ExecutarNonQuery(string sql, Dictionary<string,object> parameters = null)
        {
            this.command.CommandText = sql;
            if(parameters != null)
            {
                foreach (var item in parameters)
                {
                    this.command.Parameters.AddWithValue(item.Key, item.Value);
                }
            }
            int rows;
            try
            {
                rows = this.command.ExecuteNonQuery();
            }
            catch(MySqlException e)
            {
                rows = 0;
            }
            return rows;
        }

        public void addParameter(string param,string value)
        {
            this.command.Parameters.AddWithValue(param, value);
        }

        public string GetStrConn()
        {
            return this.strConn;
        }


        public DataTable ExecuteSelect(string sql, Dictionary<string, object> parameters = null)
        {
            DataTable tabMemoria = new DataTable();
            this.command.CommandText = sql;
            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    this.command.Parameters.AddWithValue(item.Key, item.Value);
                }
            }
            tabMemoria.Load(this.command.ExecuteReader());
            return tabMemoria;
        }

        public void SetStrConn(string host, string database, string username, string password)
        {
            this.strConn = $"Server={host};Database={database};Uid={username};Pwd={password}";
        }

       
    }
}
