using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
