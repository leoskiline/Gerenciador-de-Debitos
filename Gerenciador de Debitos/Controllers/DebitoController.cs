using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Gerenciador_de_Debitos.Control;
using Gerenciador_de_Debitos.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_de_Debitos.Controller
{
    public class DebitoController : Microsoft.AspNetCore.Mvc.Controller
    {
        private DebitoControl control;

        public DebitoController()
        {
            if (this.control == null)
                control = new DebitoControl();
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
            
            return this.control.obterUsuario(HttpContext);
        }


        [HttpDelete]
        [Authorize("Autorizacao")]
        public IActionResult Deletar([FromBody] JsonElement dados)
        {
            return this.control.Deletar(dados);
        }

        [HttpPut]
        [Authorize("Autorizacao")]
        public IActionResult AlterarDados([FromBody] JsonElement dados)
        {
            return this.control.AlterarDados(dados, HttpContext);
        }

        [HttpPost]
        [Authorize("Autorizacao")]
        public IActionResult CadastrarConta() // Feito por Pedro
        {
            string descricao = Request.Form["descricao"].ToString();
            double valor = Convert.ToDouble(Request.Form["valor"]);
            DateTime data = Convert.ToDateTime(Request.Form["data"]);
            return this.control.CadastrarConta(descricao,valor,data, HttpContext);
        }

        [HttpGet]
        [Authorize("Autorizacao")]
        public IActionResult filtrarDebitos()
        {
            string fdescricao = Request.Form["fdescricao"].ToString();
            double fvalor = Convert.ToDouble(Request.Form["fvalor"]);
            DateTime fdata = Convert.ToDateTime(Request.Form["fdata"]);
            return this.control.filtrarDebitos(fdescricao, fvalor, fdata, HttpContext);
        }

        [HttpGet]
        [Authorize("Autorizacao")]
        public IActionResult obterDebitos()
        {
            return this.control.obterDebitos(HttpContext);
        }
    }
}
