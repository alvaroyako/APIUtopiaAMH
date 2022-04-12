using APIUtopiaAMH.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NugetUtopia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIUtopiaAMH.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private RepositoryUtopia repo;

        public UsuariosController(RepositoryUtopia repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult RegistrarUsuario(Usuario usuario)
        {
            this.repo.RegistrarUsuario(
                usuario.Nombre,
                usuario.Email,
                usuario.Password.ToString(),
                usuario.Imagen,
                "cliente"
                );
            return Ok();
        }
    }
}
