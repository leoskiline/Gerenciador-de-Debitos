using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Gerenciador_de_Debitos.Model
{
    public class Debito
    {
        private int idDebito;
        private string descricao;
        private DateTime data;
        private double valor;
        private Usuario usuario;
        private Connection conn;

        public Debito(int idDebito, string descricao, DateTime data, double valor, Usuario usuario,Connection conn)
        {
            this.IdDebito = idDebito;
            this.Descricao = descricao;
            this.Data = data;
            this.Valor = valor;
            this.Usuario = usuario;
            this.conn = conn;
        }

        public Debito(Connection conn)
        {
            this.conn = conn;
        }

        public List<Debito> obterDebitosBanco(Usuario usuario)
        {
            List<Debito> debitos = new List<Debito>();
            
            try
            {
                conn.LimparParametros();
                conn.AdicionarParametro("@idUsuario", usuario.IdUsuario.ToString());
                DataTable dt = conn.ExecutarSelect("SELECT * FROM debitos.debito where idUsuario = @idUsuario");
                if(dt.Rows.Count>0)
                {           
                    foreach (DataRow row in dt.Rows)
                    {
                        this.idDebito = Convert.ToInt32(row["idDebito"]);
                        this.Descricao = row["descricao"].ToString();
                        this.Data = Convert.ToDateTime(row["data"]);
                        this.Valor = Convert.ToDouble(row["valor"]);
                        this.Usuario = usuario;
                        debitos.Add(this);
                    }
                }
            }catch(Exception e)
            {

            }
            return debitos;
        }

        public int IdDebito { get => idDebito; set => idDebito = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public DateTime Data { get => data; set => data = value; }
        public double Valor { get => valor; set => valor = value; }
        public Usuario Usuario { get => usuario; set => usuario = value; }
    }
}
