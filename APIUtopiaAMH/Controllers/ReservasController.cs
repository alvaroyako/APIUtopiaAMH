using APIUtopiaAMH.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NugetUtopia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        [Authorize]
        public ActionResult<List<Reserva>> GetReservas()
        {
            List<Reserva> reservas = this.repo.GetReservas();
            return reservas;
        }

        [HttpGet]
        [Route("[action]/{nombre}")]
        public ActionResult<Reserva> FindReserva(string nombre)
        {
            return this.repo.FindReserva(nombre);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public ActionResult CrearReserva(string nombre, string telefono, string email, int personas, DateTime fecha)
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            string json = claims.SingleOrDefault(x => x.Type == "UserData").Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(json);
            if (usuario.Rol == "admin")
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
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        [Route("[action]")]
        [Authorize]
        public ActionResult UpdateReserva(Reserva reserva)
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            string json = claims.SingleOrDefault(x => x.Type == "UserData").Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(json);
            if (usuario.Rol == "admin")
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
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete]
        [Route("[action]/{nombre}")]
        [Authorize]
        public ActionResult DeleteReserva(string nombre)
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            string json = claims.SingleOrDefault(x => x.Type == "UserData").Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(json);
            if (usuario.Rol == "admin")
            {
                this.repo.DeleteReserva(nombre);
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
