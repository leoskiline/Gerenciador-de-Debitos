using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos.Model
{
    public class Usuario
    {
        private int idUsuario;
        private string email;
        private string senha;
        private string nome;

        public Usuario(int idUsuario, string email, string senha, string nome)
        {
            this.IdUsuario = idUsuario;
            this.Email = email;
            this.Senha = senha;
            this.Nome = nome;
        }

        public Usuario()
        {
            this.IdUsuario = 0;
            this.Email = "";
            this.Senha = "";
            this.Nome = "";
        }

        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string Email { get => email; set => email = value; }
        public string Senha { get => senha; set => senha = value; }
        public string Nome { get => nome; set => nome = value; }
    }
}
