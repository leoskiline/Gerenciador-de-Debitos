using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos.Model
{
    public class Usuario
    {
        private int idUsuario;
        private string email;
        private string senha;
        private string nome;
        private string nivel;
        private Connection conexao;

        public Usuario(int idUsuario, string email, string senha, string nome, string nivel, Connection conn)
        {
            this.conexao = conn;
            this.IdUsuario = idUsuario;
            this.Email = email;
            this.Senha = senha;
            this.Nome = nome;
            this.Nivel = nivel;
        }

        public Usuario(Connection conn)
        {
            this.conexao = conn;
            this.IdUsuario = -1;
            this.Email = null;
            this.Senha = null;
            this.Nome = null;
            this.nivel = "COMUM";
        }

        public Usuario()
        {

        }

        public NameValueCollection registrarUsuario()
        {
            NameValueCollection ret = new NameValueCollection()
            {
                {"icon","info" },
                {"message","Opss... Algo deu errado ao registrar Usuario." }
            };
            try
            {
                if (this.verificarEmailRegistrado())
                {
                    ret["icon"] = "warning";
                    ret["message"] = $"E-mail não esta disponivel.";
                }else
                {
                    string sql = "INSERT INTO usuario (email,senha,nome) VALUES(@email,@senha,@nome)";
                    this.conexao.LimparParametros();
                    this.conexao.AdicionarParametro("@email", this.Email);
                    this.conexao.AdicionarParametro("@senha", this.Senha);
                    this.conexao.AdicionarParametro("@nome", this.Nome);
                    if(this.conexao.ExecutarNonQuery(sql) > 0)
                    {
                        ret["icon"] = "success";
                        ret["message"] = "E-mail registrado com Sucesso!";
                    }
                }
            }catch(Exception e)
            {
                throw new Exception("Ocorreu um Erro:" + e.ToString());
            }
            return ret;
        }

        private bool verificarEmailRegistrado()
        {
            bool ret = false;
            try
            {
                string sql = "SELECT email FROM usuario WHERE email = @email";
                this.conexao.LimparParametros();
                this.conexao.AdicionarParametro("@email", this.email);
                DataTable dt = this.conexao.ExecutarSelect(sql);
                if(dt.Rows.Count > 0)
                {
                    ret = true;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um Erro:" + e.ToString());
            }
            return ret;
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
                    this.Nivel = dt.Rows[0]["nivel"].ToString();
                    ret = true;
                }
                return ret;
            }
            catch(Exception e)
            {
                throw new Exception("Ocorreu um Erro:" + e.ToString());
            }
        }


        private static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public Usuario obterUsuarioByID(int idUsuario)
        {
            this.conexao.LimparParametros();
            this.conexao.AdicionarParametro("@idUsuario", idUsuario.ToString());
            DataTable dt = this.conexao.ExecutarSelect("SELECT * FROM debitos.usuario WHERE idUsuario = @idUsuario");
            if (dt.Rows.Count > 0)
            {
                this.IdUsuario = Convert.ToInt32(dt.Rows[0]["idUsuario"]);
                this.Email = dt.Rows[0]["email"].ToString();
                this.Senha = GetHashString(dt.Rows[0]["senha"].ToString());
                this.Nome = dt.Rows[0]["nome"].ToString();
                this.Nivel = dt.Rows[0]["nivel"].ToString();
            }
            return this;
        }

        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string Email { get => email; set => email = value; }
        public string Senha { get => senha; set => senha = value; }
        public string Nome { get => nome; set => nome = value; }

        public string Nivel { get => nivel; set => nivel = value; }
    }
}
