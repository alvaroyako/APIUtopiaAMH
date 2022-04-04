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
    public class JuegosController : ControllerBase
    {
        private RepositoryUtopia repo;

        public JuegosController(RepositoryUtopia repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult<List<Juego>> GetJuegos()
        {
            List<Juego> juegos = this.repo.GetJuegos();
            return juegos;
        }

        [HttpGet]
        [Route("[action]/{idjuego}")]
        public ActionResult<Juego> GetJuegoId(int idjuego)
        {
            return this.repo.FindJuego(idjuego);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult CrearJuego(Juego juego)
        {
            this.repo.CrearJuego(juego);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public ActionResult UpdateJuego(Juego juego)
        {
            this.repo.EditarJuego(
                juego.IdJuego,
                juego.Nombre,
                juego.Descripcion,
                juego.Categoria,
                juego.Precio,
                juego.Foto
                );
            return Ok();
        }

        [HttpDelete]
        [Route("[action]/{idjuego}")]
        public void DeleteJuego(int idjuego)
        {
            this.repo.DeleteJuego(idjuego);
        }
    }
}
