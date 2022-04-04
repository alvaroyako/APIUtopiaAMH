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
    public class ReservasController : ControllerBase
    {
        private RepositoryUtopia repo;

        public ReservasController(RepositoryUtopia repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult<List<Reserva>> GetReservas()
        {
            List<Reserva> reservas = this.repo.GetReservas();
            return reservas;
        }

        [HttpGet]
        [Route("[action]/{nombre}")]
        public ActionResult<Reserva> GetReservaNombre(string nombre)
        {
            return this.repo.FindReserva(nombre);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult CrearReserva(string nombre, string telefono, string email, int personas, DateTime fecha)
        {
            Reserva res = new Reserva();
            res.Nombre = nombre;
            res.Telefono = telefono;
            res.Email = email;
            res.Personas = personas;
            res.Fecha = fecha.ToShortDateString();
            res.Hora = fecha.ToShortTimeString();
            this.repo.CrearReserva(res);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public ActionResult UpdateReserva(Reserva reserva)
        {
            this.repo.EditarReserva(
                reserva.Nombre,
                reserva.Telefono,
                reserva.Email,
                reserva.Personas,
                reserva.Fecha,
                reserva.Hora
                );
            return Ok();
        }

        [HttpDelete]
        [Route("[action]/{nombre}")]
        public void DeleteReserva(string nombre)
        {
            this.repo.DeleteReserva(nombre);
        }
    }
}
