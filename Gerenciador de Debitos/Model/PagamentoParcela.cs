using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos.Model
{
    public class PagamentoParcela
    {
        private int idPagamentoParcela;
        private double valor;
        private DateTime dataHoraPagamento;
        private StatusPagamentoParcela statusPagamentoParcela;

        public PagamentoParcela(int idPagamentoParcela, double valor, DateTime dataHoraPagamento, StatusPagamentoParcela statusPagamentoParcela)
        {
            this.IdPagamentoParcela = idPagamentoParcela;
            this.Valor = valor;
            this.DataHoraPagamento = dataHoraPagamento;
            this.StatusPagamentoParcela = statusPagamentoParcela;
        }

        public PagamentoParcela()
        {
            this.IdPagamentoParcela = 0;
            this.Valor = 0.0;
            this.DataHoraPagamento = new DateTime();
            this.StatusPagamentoParcela = new StatusPagamentoParcela();
        }

        public int IdPagamentoParcela { get => idPagamentoParcela; set => idPagamentoParcela = value; }
        public double Valor { get => valor; set => valor = value; }
        public DateTime DataHoraPagamento { get => dataHoraPagamento; set => dataHoraPagamento = value; }
        public StatusPagamentoParcela StatusPagamentoParcela { get => statusPagamentoParcela; set => statusPagamentoParcela = value; }
    }
}
