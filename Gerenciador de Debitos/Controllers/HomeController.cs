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
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {

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
    }
}
