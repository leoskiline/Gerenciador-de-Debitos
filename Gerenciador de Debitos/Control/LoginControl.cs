using Gerenciador_de_Debitos.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos.Control 
{
    public class LoginControl : Microsoft.AspNetCore.Mvc.Controller
    {
        private Connection conexao;

        public LoginControl()
        {
            if (conexao == null)
            {
                conexao = new Connection();
            }
        }

        public JsonResult Registrar(string nome,string email,string senha)
        {
            this.conexao.AbrirConexao();
            Usuario user = new Usuario(this.conexao);
            user.Nome = nome;
            user.Email = email;
            user.Senha = senha;
            NameValueCollection ret = user.registrarUsuario();
            string icon = ret["icon"];
            string message = ret["message"];
            var retorno = new
            {
                icon,
                message
            };
            this.conexao.FecharConexao();
            return Json(retorno);
        }

        public JsonResult Logar(string email, string senha,HttpContext HttpContext)
        {
            this.conexao.AbrirConexao();
            string icon = "error";
            string message = "Usuario ou Senha Invalida.";
            Usuario user = new Usuario(this.conexao);
            user.Email = email;
            user.Senha = senha;
            if (user.autenticarUsuario())
            {
                var userClaims = new List<Claim>()
                {
                    new Claim("Nome", user.Nome),
                    new Claim("Email", user.Email),
                    new Claim("idUsuario", user.IdUsuario.ToString()),
                    new Claim("Nivel", user.Nivel)
                };
                var identity = new ClaimsIdentity(userClaims, "Identificacao do Usuario");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                AuthenticationHttpContextExtensions.SignInAsync(HttpContext, principal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddDays(1)
                });
                icon = "success";
                message = "Autenticado com Sucesso!";
            }
            var retorno = new
            {
                icon = icon,
                message = message
            };
            this.conexao.FecharConexao();
            return Json(retorno);
        }
    }
}
