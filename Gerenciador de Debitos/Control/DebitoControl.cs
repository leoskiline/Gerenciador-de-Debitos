using Gerenciador_de_Debitos.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Gerenciador_de_Debitos.Control
{
    public class DebitoControl : Microsoft.AspNetCore.Mvc.Controller
    {

        private Connection conn;

        public DebitoControl()
        {
            if(this.conn == null)
            {
                conn = new Connection();
            }
        }

        public OkObjectResult obterUsuario(HttpContext HttpContext)
        {
            return Ok(HttpContext.User.Claims.Select(x => new { Type = x.Type, Value = x.Value }));
        }

        public JsonResult Deletar(JsonElement dados)
        {
            int codigo = Convert.ToInt32(dados.GetProperty("IdDebito").ToString());
            this.conn.AbrirConexao();
            Debito debito = new Debito(codigo, this.conn);
            bool ret = debito.DeletarPorID();
            this.conn.FecharConexao();
            string msg = "";
            string icon = "";
            if (ret)
            {
                msg = "Deletado com Sucesso!";
                icon = "success";
            }
            else
            {
                msg = "Não foi possivel deletar";
                icon = "error";
            }
            var retorno = new
            {
                icon,
                msg
            };
            return Json(retorno);
        }

        public JsonResult AlterarDados(JsonElement dados, HttpContext HttpContext)
        {
            int codigo = Convert.ToInt32(dados.GetProperty("IdDebito").ToString());
            double valor = Convert.ToDouble(dados.GetProperty("Valor").ToString().Replace(",", ".")) / 100;
            string descricao = (dados.GetProperty("Descricao").ToString());
            DateTime data = Convert.ToDateTime(dados.GetProperty("Data").ToString());

            Usuario usuario = new Usuario(Convert.ToInt32(HttpContext.User.Claims.Where(w => w.Type == "idUsuario").First().Value),
            HttpContext.User.Claims.Where(w => w.Type == "Email").First().Value, "",
            HttpContext.User.Claims.Where(w => w.Type == "Nome").First().Value,
            HttpContext.User.Claims.Where(w => w.Type == "Nivel").First().Value,
            this.conn);

            // Validação
            Debito debito = new Debito(this.conn);
            bool invalido = false;
            string msg = "";
            string icon = "";

            if (descricao != "" && data.Date >= DateTime.Now.Date && valor > 0)
            {
                foreach (var item in debito.obterDebitosPorNome(usuario, descricao))
                {
                    if (descricao.ToUpper() == item.Descricao.ToUpper())
                    {
                        if (data == item.Data)
                        {
                            invalido = true;
                            msg = "Nao foi possivel adicionar, Conta Duplicada.";
                            icon = "error";
                        }
                    }
                }
            }
            else
            {
                msg = "Data nao pode ser menor que data Atual ou valores invalidos!";
                icon = "error";
                invalido = true;
            }

            if (!invalido)
            {
                this.conn.AbrirConexao();
                debito.IdDebito = codigo;
                debito.Descricao = descricao;
                debito.Data = data;
                debito.Valor = valor;
                debito.Usuario = usuario;
                if (debito.AlterarConta())
                {
                    msg = "Conta Alterada com Sucesso!";
                    icon = "success";
                }
                else
                {
                    msg = "Conta não foi alterada !";
                    icon = "error";
                }
                this.conn.FecharConexao();
            }
            var retorno = new
            {
                icon,
                msg
            };
            return Json(retorno);
        }

        public JsonResult CadastrarConta(string descricao,double valor,DateTime data, HttpContext HttpContext)
        {
            this.conn.AbrirConexao();
            Debito debito = new Debito(this.conn);
            Usuario usuario = new Usuario(Convert.ToInt32(HttpContext.User.Claims.Where(w => w.Type == "idUsuario").First().Value),
            HttpContext.User.Claims.Where(w => w.Type == "Email").First().Value, "",
            HttpContext.User.Claims.Where(w => w.Type == "Nome").First().Value,
            HttpContext.User.Claims.Where(w => w.Type == "Nivel").First().Value,
            this.conn);

            bool invalido = false;
            string msg = "";
            string icon = "";

            // Validar se o usuário já possui uma descrição e data iguais.
            // Por exemplo: pode sim haver duas contas de água, mas não com a data igual !
            if (descricao != "" && data.Date >= DateTime.Now.Date && valor > 0)
            {
                foreach (var item in debito.obterDebitosPorNome(usuario, descricao))
                {
                    if (descricao.ToUpper() == item.Descricao.ToUpper())
                    {
                        if (data == item.Data)
                        {
                            invalido = true;
                            msg = "Nao foi possivel adicionar, Conta Duplicada.";
                            icon = "error";
                        }
                    }
                }
            }
            else
            {
                msg = "Item digitado é inválido !";
                icon = "error";
                invalido = true;
            }

            // Gravar Dados
            bool cadastrado = false;

            if (!invalido)
            {
                debito.Descricao = descricao;
                debito.Data = data;
                debito.Valor = valor;
                debito.Usuario = usuario;
                cadastrado = debito.Cadastrar();
                msg = "Conta Cadastrada com Sucesso!";
                icon = "success";
            }
            this.conn.FecharConexao();
            var retorno = new
            {
                icon,
                msg
            };
            return Json(retorno);
        }

        public JsonResult filtrarDebitos(string fdescricao,double fvalor,DateTime fdata, HttpContext HttpContext)
        {
            this.conn.AbrirConexao();
            Debito debito = new Debito(this.conn);
            Usuario usuario = new Usuario(Convert.ToInt32(HttpContext.User.Claims.Where(w => w.Type == "idUsuario").First().Value),
            HttpContext.User.Claims.Where(w => w.Type == "Email").First().Value, "",
            HttpContext.User.Claims.Where(w => w.Type == "Nome").First().Value,
            HttpContext.User.Claims.Where(w => w.Type == "Nivel").First().Value,
            this.conn);
            List<Debito> debitos = debito.filtroDebitos(usuario);
            this.conn.FecharConexao();
            return Json(debitos);
        }

        public JsonResult obterDebitos(HttpContext HttpContext)
        {
            this.conn.AbrirConexao();
            Debito debito = new Debito(this.conn);
            Usuario usuario = new Usuario(Convert.ToInt32(HttpContext.User.Claims.Where(w => w.Type == "idUsuario").First().Value),
            HttpContext.User.Claims.Where(w => w.Type == "Email").First().Value, "",
            HttpContext.User.Claims.Where(w => w.Type == "Nome").First().Value,
            HttpContext.User.Claims.Where(w => w.Type == "Nivel").First().Value,
            this.conn);
            List<Debito> debitos = debito.obterDebitosBanco(usuario);
            this.conn.FecharConexao();
            return Json(debitos);
        }
    }
}
