using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gerenciador_de_Debitos
{
    public class UsuarioAutenticado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Nivel { get; set; }
        public bool Autenticado { get; set; }

        public UsuarioAutenticado(IHttpContextAccessor accessor)
        {
            Autenticado = false;
            if (accessor.HttpContext.User.Claims.Count() > 0)
            {
                try
                {
                    Id = Convert.ToInt32(accessor.HttpContext.User.Claims.Where(w => w.Type == "idUsuario").First().Value);
                    Nome = accessor.HttpContext.User.Claims.Where(w => w.Type == "Nome").First().Value;
                    Email = accessor.HttpContext.User.Claims.Where(w => w.Type == "Email").First().Value;
                    Nivel = accessor.HttpContext.User.Claims.Where(w => w.Type == "Nivel").First().Value;
                    Autenticado = true;
                }
                catch
                {

                }
            }
        }
    }
}
