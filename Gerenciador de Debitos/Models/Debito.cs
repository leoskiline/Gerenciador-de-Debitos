using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos.Model
{
    public class Debito
    {
        private int idDebito;
        private string descricao;
        private DateTime dataHora;
        private double valorTotal;
        private int quantidadeParcelas;
        private Usuario usuario;
        private Instituicao instituicao;
        private TipoDebito tipoDebito;
        private StatusDebito statusDebito;
        private Pagamento pagamento;

        public Debito(int idDebito, string descricao, DateTime dataHora, double valorTotal, int quantidadeParcelas, Usuario usuario, Instituicao instituicao, TipoDebito tipoDebito, StatusDebito statusDebito, Pagamento pagamento)
        {
            this.IdDebito = idDebito;
            this.Descricao = descricao;
            this.DataHora = dataHora;
            this.ValorTotal = valorTotal;
            this.QuantidadeParcelas = quantidadeParcelas;
            this.Usuario = usuario;
            this.Instituicao = instituicao;
            this.TipoDebito = tipoDebito;
            this.StatusDebito = statusDebito;
            this.Pagamento = pagamento;
        }

        public Debito()
        {
            this.IdDebito = 0;
            this.Descricao = "";
            this.DataHora = new DateTime();
            this.ValorTotal = 0.0;
            this.QuantidadeParcelas = 0;
            this.Usuario = new Usuario();
            this.Instituicao = new Instituicao();
            this.TipoDebito = new TipoDebito();
            this.StatusDebito = new StatusDebito();
            this.Pagamento = new Pagamento();
        }

        public int IdDebito { get => idDebito; set => idDebito = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public DateTime DataHora { get => dataHora; set => dataHora = value; }
        public double ValorTotal { get => valorTotal; set => valorTotal = value; }
        public int QuantidadeParcelas { get => quantidadeParcelas; set => quantidadeParcelas = value; }
        public Usuario Usuario { get => usuario; set => usuario = value; }
        public Instituicao Instituicao { get => instituicao; set => instituicao = value; }
        public TipoDebito TipoDebito { get => tipoDebito; set => tipoDebito = value; }
        public StatusDebito StatusDebito { get => statusDebito; set => statusDebito = value; }
        public Pagamento Pagamento { get => pagamento; set => pagamento = value; }
    }
}
