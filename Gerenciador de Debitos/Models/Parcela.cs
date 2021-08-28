using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos.Model
{
    public class Parcela
    {
        private int idParcela;
        private int parcelaAtual;
        private double valorParcela;
        private DateTime dataParcela;
        private Debito debito;
        private PagamentoParcela pagamentoParcela;

        public Parcela(int idParcela, int parcelaAtual, double valorParcela, DateTime dataParcela, Debito debito, PagamentoParcela pagamentoParcela)
        {
            this.IdParcela = idParcela;
            this.ParcelaAtual = parcelaAtual;
            this.ValorParcela = valorParcela;
            this.DataParcela = dataParcela;
            this.Debito = debito;
            this.PagamentoParcela = pagamentoParcela;
        }

        public Parcela()
        {
            this.IdParcela = 0;
            this.ParcelaAtual = 0;
            this.ValorParcela = 0.0;
            this.DataParcela = new DateTime();
            this.Debito = new Debito();
            this.PagamentoParcela = new PagamentoParcela();
        }

        public int IdParcela { get => idParcela; set => idParcela = value; }
        public int ParcelaAtual { get => parcelaAtual; set => parcelaAtual = value; }
        public double ValorParcela { get => valorParcela; set => valorParcela = value; }
        public DateTime DataParcela { get => dataParcela; set => dataParcela = value; }
        public Debito Debito { get => debito; set => debito = value; }
        public PagamentoParcela PagamentoParcela { get => pagamentoParcela; set => pagamentoParcela = value; }
    }
}
