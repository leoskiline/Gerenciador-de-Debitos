using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos.Model
{
    public class StatusDebito
    {
        private int idStatusDebito;
        private string status;

        public StatusDebito(int idStatusDebito, string status)
        {
            this.IdStatusDebito = idStatusDebito;
            this.Status = status;
        }

        public StatusDebito()
        {
            this.IdStatusDebito = 0;
            this.Status = "";
        }

        public int IdStatusDebito { get => idStatusDebito; set => idStatusDebito = value; }
        public string Status { get => status; set => status = value; }
    }
}
