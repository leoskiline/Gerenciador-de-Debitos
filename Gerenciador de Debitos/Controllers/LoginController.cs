using Gerenciador_de_Debitos.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Claims;

namespace Gerenciador_de_Debitos.Controller
{
    public class LoginController : Microsoft.AspNetCore.Mvc.Controller
    {
        private Connection conexao;
        private UsuarioAutenticado _ua;

        public LoginController(UsuarioAutenticado ua)
        {
            _ua = ua;
            if(conexao == null)
            {
                conexao = new Connection();
            }
            
        }

        public IActionResult Index()
        {
            if(_ua.Autenticado)
            {
                return Redirect("Views/Debito/index.cshtml");
            }
            return View();
        }


        public IActionResult Sair()
        {
            Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignOutAsync(HttpContext);
            return View("Views/Login/Index.cshtml");
        }

        [HttpPost]
        public ActionResult Registrar()
        {
            this.conexao.AbrirConexao();
            Usuario user = new Usuario(this.conexao);
            user.Nome = Request.Form["nomeRegister"].ToString();
            user.Email = Request.Form["emailRegister"].ToString();
            user.Senha = Request.Form["senhaRegister"].ToString();
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

        [HttpPost]
        public ActionResult Logar()
        {
            this.conexao.AbrirConexao();
            string icon = "error";
            string message = "Usuario ou Senha Invalida.";
            Usuario user = new Usuario(this.conexao);
            user.Email = Request.Form["email"].ToString();
            user.Senha = Request.Form["senha"].ToString();
            if(user.autenticarUsuario())
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
