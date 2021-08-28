using System;
using System.Collections.Generic;
using System.Data;
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
        private Connection conexao;

        public Usuario(int idUsuario, string email, string senha, string nome,Connection conn)
        {
            this.conexao = conn;
            this.IdUsuario = idUsuario;
            this.Email = email;
            this.Senha = senha;
            this.Nome = nome;
        }

        public Usuario(Connection conn)
        {
            this.conexao = conn;
            this.IdUsuario = 0;
            this.Email = "";
            this.Senha = "";
            this.Nome = "";
        }

        public Usuario()
        {

        }

        public bool autenticarUsuario()
        {
            bool ret = false;
            try
            {
                string sql = "SELECT * FROM usuario WHERE email = @email AND senha = @senha";
                this.conexao.LimparParametros();
                this.conexao.AdicionarParametro("@email", this.email);
                this.conexao.AdicionarParametro("@senha", this.senha);
                DataTable dt = this.conexao.ExecutarSelect(sql);
                if(dt.Rows.Count > 0)
                {
                    this.IdUsuario = (int)dt.Rows[0]["idUsuario"];
                    this.Email = dt.Rows[0]["email"].ToString();
                    this.Nome = dt.Rows[0]["nome"].ToString();
                    this.Senha = dt.Rows[0]["senha"].ToString();
                    ret = true;
                }
                return ret;
            }
            catch(Exception e)
            {
                throw new Exception("Ocorreu um Erro:" + e.ToString());
            }
            
        }

        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string Email { get => email; set => email = value; }
        public string Senha { get => senha; set => senha = value; }
        public string Nome { get => nome; set => nome = value; }
    }
}
