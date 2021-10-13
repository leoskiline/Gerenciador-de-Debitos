using Gerenciador_de_Debitos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos.Models
{
    public class Credito : TemplateMethodBaseClass
    {
     
        public Credito(int idCredito, string descricao, DateTime data, double valor, Usuario usuario, StatusPagamento statusPagamento, Connection conn, List<IObservador> listaObservadores)
        {
            IdCredito = idCredito;
            Descricao = descricao;
            Data = data;
            Valor = valor;
            Usuario = usuario;
            StatusPagamento = statusPagamento;
            Conn = conn;
            this.listaObservadores = listaObservadores;
        }

        public Credito(Connection conn)
        {
            this.conn = conn;
        }

    }
}
