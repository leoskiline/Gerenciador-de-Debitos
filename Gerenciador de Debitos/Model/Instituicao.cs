using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos.Model
{
    public class Instituicao
    {
        private int idInstituicao;
        private string desricao;

        public Instituicao(int idInstituicao, string desricao)
        {
            this.IdInstituicao = idInstituicao;
            this.Desricao = desricao;
        }

        public Instituicao()
        {
            this.IdInstituicao = 0;
            this.Desricao = "";
        }

        public int IdInstituicao { get => idInstituicao; set => idInstituicao = value; }
        public string Desricao { get => desricao; set => desricao = value; }
    }
}
