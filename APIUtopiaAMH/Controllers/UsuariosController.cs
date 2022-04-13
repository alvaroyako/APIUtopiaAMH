using APIUtopiaAMH.Repositories;
using Microsoft.AspNetCore.Authorization;
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
            bool respuesta=this.repo.RegistrarUsuario(
                usuario.Nombre,
                usuario.Email,
                usuario.PasswordString,
                usuario.Imagen,
                "cliente"
                );
            if (respuesta == false)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
            
        }

        [HttpGet]
        [Route("[action]/{idusuario}")]
        [Authorize]
        public ActionResult<Usuario> FindUsuario(int idusuario)
        {
            return this.repo.FindUsuario(idusuario);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<Usuario> ExisteUsuario(string email, string password)
        {
            Usuario usu = this.repo.ExisteUsuario(email, password);
            if (usu !=null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
