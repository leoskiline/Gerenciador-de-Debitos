using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Gerenciador_de_Debitos.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_de_Debitos.Controller
{
    public class DebitoController : Microsoft.AspNetCore.Mvc.Controller
    {
        private Connection conn;

        public DebitoController()
        {
            if (this.conn == null)
                conn = new Connection();
        }

        [Authorize("Autorizacao")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize("Autorizacao")]
        public IActionResult obterUsuario()
        {
            return Ok(User.Claims.Select(x => new { Type = x.Type, Value = x.Value}));
        }
        [HttpPost]

        public IActionResult CadastrarConta()
        {

            string descricao = Request.Form["descricao"].ToString();



            /*string descricao = dados.GetProperty("descricao").ToString();
            string valor = dados.GetProperty("valor").ToString();
            string data = dados.GetProperty("data").ToString();*/


       /*     this.conn.AbrirConexao();
            Usuario usuario = new Usuario(Convert.ToInt32(HttpContext.User.Claims.Where(w => w.Type == "idUsuario").First().Value),
            User.Claims.Where(w => w.Type == "Email").First().Value, "",
            User.Claims.Where(w => w.Type == "Nome").First().Value,
            User.Claims.Where(w => w.Type == "Nivel").First().Value,
            this.conn);*/

            //Debito debito = new Debito(

            return Json("OK");
            //    );

        }

        [HttpGet]
        [Authorize("Autorizacao")]
        public IActionResult obterDebitos()
        {
            this.conn.AbrirConexao();
            Debito debito = new Debito(this.conn);
            Usuario usuario = new Usuario(Convert.ToInt32(HttpContext.User.Claims.Where(w => w.Type == "idUsuario").First().Value),
            User.Claims.Where(w => w.Type == "Email").First().Value,"",
            User.Claims.Where(w => w.Type == "Nome").First().Value,
            User.Claims.Where(w => w.Type == "Nivel").First().Value,
            this.conn);
            List<Debito> debitos = debito.obterDebitosBanco(usuario);
            this.conn.FecharConexao();
            return Json(debitos);   
        }
    }
}
