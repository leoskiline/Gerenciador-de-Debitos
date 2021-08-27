using Gerenciador_de_Debitos.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos.Controller
{
    public class LoginController : Microsoft.AspNetCore.Mvc.Controller
    {
        private Connection conexao;
        public IActionResult Index()
        {
            return View();
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
