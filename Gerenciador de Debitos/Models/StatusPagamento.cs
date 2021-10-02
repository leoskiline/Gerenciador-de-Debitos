using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos.Models
{

    public class StatusPagamento
    {
        private int idStatusPagamento;
        private string status;
        private Connection conn;

        public StatusPagamento(int idStatusPagamento, string status)
        {
            this.idStatusPagamento = idStatusPagamento;
            this.status = status;
            this.conn = new Connection();
        }

        public StatusPagamento()
        {
            this.conn = new Connection();
        }

        public int IdStatusPagamento { get => idStatusPagamento; set => idStatusPagamento = value; }
        public string Status { get => status; set => status = value; }
        public Connection Conn { get => conn; set => conn = value; }

        public StatusPagamento obterStatusPagamentoPorID(int id)
        {
            StatusPagamento statusPagamento = null;
            try
            {
                string sql = "SELECT * FROM statuspagamento where idStatusPagamento = @id";
                this.conn.AbrirConexao();
                this.conn.AdicionarParametro("@id", id.ToString());
                DataTable dt = this.conn.ExecutarSelect(sql);
                this.conn.FecharConexao();
                if (dt.Rows.Count > 0)
                {
                     statusPagamento = new StatusPagamento(Convert.ToInt32(dt.Rows[0]["idStatusPagamento"]), dt.Rows[0]["status"].ToString());
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return statusPagamento;
        }
    }
}
