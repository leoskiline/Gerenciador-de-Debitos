using Gerenciador_de_Debitos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos.Models
{
    public class Credito : TemplateMethodBaseClass
    {
        private int idCredito;
        private string descricao;
        private DateTime data;
        private double valor;
        private Usuario usuario;
        private StatusPagamento statusPagamento;
        private Connection conn;
        private List<IObservador> listaObservadores;

        public Credito(int idCredito, string descricao, DateTime data, double valor, Usuario usuario, StatusPagamento statusPagamento, Connection conn, List<IObservador> listaObservadores)
        {
            this.idCredito = idCredito;
            this.descricao = descricao;
            this.data = data;
            this.valor = valor;
            this.usuario = usuario;
            this.statusPagamento = statusPagamento;
            this.conn = conn;
            this.listaObservadores = listaObservadores;
        }

        public int IdCredito { get => idCredito; set => idCredito = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public DateTime Data { get => data; set => data = value; }
        public double Valor { get => valor; set => valor = value; }
        public Usuario Usuario { get => usuario; set => usuario = value; }
        public StatusPagamento StatusPagamento { get => statusPagamento; set => statusPagamento = value; }
        public Connection Conn { get => conn; set => conn = value; }
        public List<IObservador> ListaObservadores { get => listaObservadores; set => listaObservadores = value; }
       
        public bool Cadastrar() 
        {
            int linhasAfetadas = 0;
            try
            {
                this.conn.LimparParametros();
                StateContext ctx = new StateContext(); // Inicia no estado padrao Pendente.
                ctx.setState(new Pendente()); // Para deixar claro que mudou de estado para Pendente.
                this.statusPagamento = ctx.getState().setState(ctx);
                this.conn.AdicionarParametro("@descricao", this.Descricao);
                this.conn.AdicionarParametro("@data", this.Data.ToString("yyyy-MM-dd"));
                this.conn.AdicionarParametro("@valor", this.Valor.ToString().Replace(",", "."));
                this.conn.AdicionarParametro("@idUsuario", this.Usuario.IdUsuario.ToString());
                this.conn.AdicionarParametro("@idStatusPagamento", this.statusPagamento.IdStatusPagamento.ToString());
                string sql = "INSERT INTO debitos.debito (descricao, data, valor,idStatusPagamento, idUsuario) " +
                                "VALUES (@descricao,@data,@valor,@idStatusPagamento,@idUsuario)";
                linhasAfetadas = conn.ExecutarNonQuery(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return linhasAfetadas > 0;
        }
        override
        public bool AlterarConta() 
        {
            int linhasAfetadas = 0;
            try
            {
                string valor = this.valor.ToString().Replace(",", ".");
                string data = this.data.ToString("yyyy-MM-dd");
                conn.LimparParametros();
                //conn.AdicionarParametro("@idUsuario", this.usuario.IdUsuario.ToString());
                conn.AdicionarParametro("@descricao", this.descricao);
                conn.AdicionarParametro("@valor", valor);
                conn.AdicionarParametro("@idDebito", this.idCredito.ToString());
                conn.AdicionarParametro("@data", data);

                string sql = "UPDATE debitos.debito SET descricao = @descricao, valor = @valor, data = @data " +
                    "WHERE idDebito = @idDebito";

                linhasAfetadas = conn.ExecutarNonQuery(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return linhasAfetadas > 0;
        }
        override
        public bool DeletarPorID()
        {
            bool sucesso = false;
            try
            {
                this.conn.LimparParametros();
                this.conn.AdicionarParametro("@idDebito", this.IdCredito.ToString());
                int rows = this.conn.ExecutarNonQuery("DELETE FROM debitos.debito WHERE idDebito = @idDebito");
                if (rows > 0)
                {
                    sucesso = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return sucesso;
        }
    }
}
