using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        [Authorize("Autorizacao")]
        public IActionResult CadastrarConta() // Feito por Pedro
        {
            string descricao = Request.Form["descricao"].ToString();
            double valor = Convert.ToDouble(Request.Form["valor"]);
            DateTime data = Convert.ToDateTime(Request.Form["data"]);

            this.conn.AbrirConexao();
            Debito debito = new Debito(this.conn);
            Usuario usuario = new Usuario(Convert.ToInt32(HttpContext.User.Claims.Where(w => w.Type == "idUsuario").First().Value),
            User.Claims.Where(w => w.Type == "Email").First().Value, "",
            User.Claims.Where(w => w.Type == "Nome").First().Value,
            User.Claims.Where(w => w.Type == "Nivel").First().Value,
            this.conn);

            bool duplicado = false;
            string msg = "";

            // Validar se o usuário já possui uma descrição e data iguais.
            // Por exemplo: pode sim haver duas contas de água, mas não com a data igual !
            if (descricao != null)
            {
                foreach (var item in debito.obterDebitosPorNome(usuario, descricao))
                {
                    if (descricao.ToUpper() == item.Descricao.ToUpper())
                    {
                        if (data == item.Data)
                        {
                            duplicado = true;
                        }
                    }
                }
            }
            else
            {
                msg = "Item digitado não pode ser nulo!";
            }

            // Gravar Dados
            bool cadastrado = false;
            
            if (!duplicado)
            {
                debito.Descricao = descricao;
                debito.Data = data;
                debito.Valor = valor;
                debito.Usuario = usuario;
                cadastrado = debito.Cadastrar();
            }
            this.conn.FecharConexao();
            var retorno = new
            {
                msg,
                duplicado,
                cadastrado
            };
            return Json(retorno);
        }

        [HttpGet]
        [Authorize("Autorizacao")]
        public IActionResult filtrarDebitos()
        {
            string fdescricao = Request.Form["fdescricao"].ToString();
            double fvalor = Convert.ToDouble(Request.Form["fvalor"]);
            DateTime fdata = Convert.ToDateTime(Request.Form["fdata"]);

            this.conn.AbrirConexao();
            Debito debito = new Debito(this.conn);
            Usuario usuario = new Usuario(Convert.ToInt32(HttpContext.User.Claims.Where(w => w.Type == "idUsuario").First().Value),
            User.Claims.Where(w => w.Type == "Email").First().Value, "",
            User.Claims.Where(w => w.Type == "Nome").First().Value,
            User.Claims.Where(w => w.Type == "Nivel").First().Value,
            this.conn);
            List<Debito> debitos = debito.filtroDebitos(usuario);
            this.conn.FecharConexao();
            return Json(debitos);
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
