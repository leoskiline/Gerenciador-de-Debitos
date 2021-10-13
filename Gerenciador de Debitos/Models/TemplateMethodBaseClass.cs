using Gerenciador_de_Debitos.Model;
using Gerenciador_de_Debitos.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos.Models
{
    public abstract class TemplateMethodBaseClass : ClasseInutil
    {
        private int idCredito;
        private string descricao;
        private DateTime data;
        private double valor;
        private Usuario usuario;
        private StatusPagamento statusPagamento;
        private List<IObservador> listaObservadores;
        private ContaType tipoConta;
        private Connection conn;

        public int IdCredito { get => idCredito; set => idCredito = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public DateTime Data { get => data; set => data = value; }
        public double Valor { get => valor; set => valor = value; }
        public Usuario Usuario { get => usuario; set => usuario = value; }
        public StatusPagamento StatusPagamento { get => statusPagamento; set => statusPagamento = value; }
        public List<IObservador> ListaObservadores { get => listaObservadores; set => listaObservadores = value; }
        public Connection Conn { get => conn; set => conn = value; }
        public ContaType TipoConta { get => tipoConta; set => tipoConta = value; }

        public override sealed void TemplateMethod()
        {
            this.Cadastrar();
            this.AlterarConta();
            this.DeletarPorID();
        }

        public abstract bool DeletarPorID();
        public abstract bool AlterarConta();
        public bool Cadastrar() // Feito por Pedro
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
                this.conn.AdicionarParametro("@tipoConta", this.tipoConta.ToString());
                string sql = "INSERT INTO debitos.debito (descricao, data, valor,idStatusPagamento, idUsuario, tipoConta) " +
                                "VALUES (@descricao,@data,@valor,@idStatusPagamento,@idUsuario,@tipoConta)";
                linhasAfetadas = conn.ExecutarNonQuery(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return linhasAfetadas > 0;
        }
    }
}
