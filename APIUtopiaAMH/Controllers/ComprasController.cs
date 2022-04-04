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
    public class ComprasController : ControllerBase
    {
        private RepositoryUtopia repo;

        public ComprasController(RepositoryUtopia repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("[action]/{idusuario}")]
        public ActionResult <List<Compra>> BuscarComprasUsuario(int idusuario)
        {
            List<Compra> compras = this.repo.BuscarCompras(idusuario);
            return compras;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult CreateCompra(Compra compra)
        {
            this.repo.CreateCompras(compra);
            return Ok();
        }
    }
}
