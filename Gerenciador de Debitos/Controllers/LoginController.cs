using Gerenciador_de_Debitos.Control;
using Gerenciador_de_Debitos.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Claims;

namespace Gerenciador_de_Debitos.Controller
{
    public class LoginController : Microsoft.AspNetCore.Mvc.Controller
    {
        private LoginControl control;
        private UsuarioAutenticado _ua;

        public LoginController(UsuarioAutenticado ua)
        {
            _ua = ua;
            if(this.control == null)
            {
                this.control = new LoginControl();
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
            string nome= Request.Form["nome"].ToString();
            string email  = Request.Form["email"].ToString();
            string senha = Request.Form["senha"].ToString();
            return this.control.Registrar(nome, email, senha);
        }

        [HttpPost]
        public ActionResult Logar()
        {
            string email = Request.Form["email"].ToString();
            string senha = Request.Form["senha"].ToString();
            return this.control.Logar(email, senha, HttpContext);
        }
    }
}
