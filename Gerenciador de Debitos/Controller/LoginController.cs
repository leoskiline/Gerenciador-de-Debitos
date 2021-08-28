using Gerenciador_de_Debitos.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        }

        public IActionResult Index()
        {
            if(_ua.Autenticado)
            {
                return Redirect("Views/Home/index.cshtml");
            }
            return View();
        }


        public IActionResult Sair()
        {
            Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignOutAsync(HttpContext);
            return View("Views/Login/Index.cshtml");
        }

        [HttpPost]
        public ActionResult Logar()
        {
            this.conexao = new Connection();
            this.conexao.AbrirConexao();
            string icon = "error";
            string message = "Usuario ou Senha Invalida.";
            Usuario user = new Usuario(this.conexao);
            user.Email = Request.Form["email"].ToString();
            user.Senha = Request.Form["senha"].ToString();
            if(user.autenticarUsuario())
            {
                var  userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Nome),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Sid, user.IdUsuario.ToString())
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
