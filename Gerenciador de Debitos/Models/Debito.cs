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
        private DateTime data;
        private double valor;
        private Usuario usuario;

        public Debito(int idDebito, string descricao, DateTime data, double valor, Usuario usuario)
        {
            this.IdDebito = idDebito;
            this.Descricao = descricao;
            this.Data = data;
            this.Valor = valor;
            this.Usuario = usuario;
        }

        public int IdDebito { get => idDebito; set => idDebito = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public DateTime Data { get => data; set => data = value; }
        public double Valor { get => valor; set => valor = value; }
        public Usuario Usuario { get => usuario; set => usuario = value; }
    }
}
