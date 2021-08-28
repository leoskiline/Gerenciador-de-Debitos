using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos.Model
{
    public class Pagamento
    {
        private int idPagamento;
        private double valorPagamento;
        private DateTime dataHoraPagamento;

        public Pagamento(int idPagamento, double valorPagamento, DateTime dataHoraPagamento)
        {
            this.IdPagamento = idPagamento;
            this.ValorPagamento = valorPagamento;
            this.DataHoraPagamento = dataHoraPagamento;
        }

        public Pagamento()
        {
            this.IdPagamento = 0;
            this.ValorPagamento = 0.0;
            this.DataHoraPagamento = new DateTime();
        }


        public int IdPagamento { get => idPagamento; set => idPagamento = value; }
        public double ValorPagamento { get => valorPagamento; set => valorPagamento = value; }
        public DateTime DataHoraPagamento { get => dataHoraPagamento; set => dataHoraPagamento = value; }
    }
}
