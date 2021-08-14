using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos.Model
{
    public class TipoDebito
    {
        private int idTipoDebito;
        private string descricao;

        public TipoDebito(int idTipoDebito, string descricao)
        {
            this.IdTipoDebito = idTipoDebito;
            this.Descricao = descricao;
        }

        public TipoDebito()
        {
            this.IdTipoDebito = 0;
            this.Descricao = "";
        }

        public int IdTipoDebito { get => idTipoDebito; set => idTipoDebito = value; }
        public string Descricao { get => descricao; set => descricao = value; }
    }
}
