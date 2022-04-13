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
    public class PlatosController : ControllerBase
    {
        private RepositoryUtopia repo;

        public PlatosController(RepositoryUtopia repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult <List<Plato>> GetPlatos()
        {
            List<Plato> platos = this.repo.GetPlatos();
            return platos;
        }

        [HttpGet]
        [Route("[action]/{idplato}")]
        public ActionResult<Plato> FindPlato(int idplato)
        {
            return this.repo.FindPlato(idplato);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public ActionResult CrearPlato(Plato plato)
        {
            this.repo.CrearPlato(plato);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        [Authorize]
        public ActionResult UpdatePlato(Plato plato)
        {
            this.repo.EditarPlato(
                plato.IdPlato,
                plato.Nombre,
                plato.Descripcion,
                plato.Categoria,
                plato.Precio,
                plato.Foto
                );
            return Ok();
        }

        [HttpDelete]
        [Route("[action]/{idplato}")]
        [Authorize]
        public void DeletePlato(int idplato)
        {
            this.repo.DeletePlato(idplato);
        }
    }
}
