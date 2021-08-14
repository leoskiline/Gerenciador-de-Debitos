using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos.Model
{
    public class StatusPagamentoParcela
    {
        private int idStatusPagamentoParcela;
        private string status;

        public StatusPagamentoParcela(int idStatusPagamentoParcela, string status)
        {
            this.IdStatusPagamentoParcela = idStatusPagamentoParcela;
            this.Status = status;
        }

        public StatusPagamentoParcela()
        {
            this.IdStatusPagamentoParcela = 0;
            this.Status = "";
        }

        public int IdStatusPagamentoParcela { get => idStatusPagamentoParcela; set => idStatusPagamentoParcela = value; }
        public string Status { get => status; set => status = value; }
    }
}
