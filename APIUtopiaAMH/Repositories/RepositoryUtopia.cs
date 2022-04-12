﻿using APIUtopiaAMH.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using NugetUtopia;
using Microsoft.EntityFrameworkCore;
using APIUtopiaAMH.Helpers;

namespace APIUtopiaAMH.Repositories
{
    public class RepositoryUtopia
    {
        private ContextUtopia context;
        
        public MediaTypeWithQualityHeaderValue Header { get; private set; }

        public RepositoryUtopia(ContextUtopia context)
        {
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
            this.context = context;
        }

        #region PROCEDURES
        //MOSTRAR LOS PLATOS
        //create procedure sp_all_platos
        //as
        //select * from PLATOS
        //go

        //MOSTRAR LOS JUEGOS
        //create procedure sp_all_juegos
        //as
        //select * from JUEGOS
        //go

        //CREAR RESERVA
        //create procedure sp_crear_reserva(@nombre nvarchar(50),@tlf nvarchar(50),@email nvarchar(50),@asistentes int,@fecha nvarchar(50))
        //as
        //insert into RESERVAS values(@nombre,@tlf,@email,@asistentes,@fecha)
        //go

        //MOSTRAR RESERVAS
        //CREATE PROCEDURE sp_all_reservas
        //as
        //select * from RESERVAS
        //go
        #endregion

        #region Metodos Generales
        //Obtiene el valor del ultimo id que existe en la tabla usuarios
        private int GetMaxIdUsuario()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Usuarios.Max(z => z.IdUsuario) + 1;
            }
        }
        #endregion

        #region Metodos LogIn
        public bool RegistrarUsuario(string nombre, string email, string password, string imagen, string rol)
        {
            var consulta = from datos in this.context.Usuarios
                           where datos.Nombre == nombre
                           select datos;
            if (consulta.FirstOrDefault() != null)
            {
                return false;
            }
            else
            {
                int idusuario = this.GetMaxIdUsuario();
                Usuario usuario = new Usuario();
                usuario.IdUsuario = idusuario;
                usuario.Nombre = nombre;
                usuario.Email = email;
                usuario.Salt = HelperCryptography.GenerateSalt();
                usuario.Password = HelperCryptography.EncriptarPassword(password, usuario.Salt);
                usuario.Imagen = idusuario + "_" + imagen;
                usuario.Rol = rol;
                this.context.Usuarios.Add(usuario);
                this.context.SaveChanges();
                return true;
            }
            
        }
        #endregion

        #region Metodos Platos

        //Muestra todos los platos
        public List<Plato> GetPlatos()
        {
            string sql = "sp_all_platos";
            var consulta = this.context.Platos.FromSqlRaw(sql);
            return consulta.ToList();
        }


        //Metodo para crear un plato
        public void CrearPlato(Plato plato)
        {
            this.context.Platos.Add(plato);
            this.context.SaveChanges();
        }

        //Encuentra un plato gracias a su id
        public Plato FindPlato(int idplato)
        {
            return this.context.Platos.SingleOrDefault(z => z.IdPlato == idplato);
        }

        //Borra un plato
        public void DeletePlato(int idplato)
        {
            Plato plato = this.FindPlato(idplato);
            this.context.Platos.Remove(plato);
            this.context.SaveChanges();
        }

        //Edita un plato
        public void EditarPlato(int idplato, string nombre, string descripcion, string categoria, int precio, string foto)
        {
            Plato plato = this.FindPlato(idplato);
            plato.Nombre = nombre;
            plato.Descripcion = descripcion;
            plato.Categoria = categoria;
            plato.Precio = precio;
            plato.Foto = foto;
            this.context.SaveChanges();
        }
        #endregion

        #region Metodos Juegos
        //Muestra todos los juegos
        public List<Juego> GetJuegos()
        {
            string sql = "sp_all_juegos";
            var consulta = this.context.Juegos.FromSqlRaw(sql);
            return consulta.ToList();
        }

        //Metodo para crear un juego
        public void CrearJuego(Juego juego)
        {
            this.context.Juegos.Add(juego);
            this.context.SaveChanges();
        }

        //Encuentra un juego gracias a su id
        public Juego FindJuego(int idjuego)
        {
            return this.context.Juegos.SingleOrDefault(z => z.IdJuego == idjuego);
        }

        //Borra un juego
        public void DeleteJuego(int idjuego)
        {
            Juego juego = this.FindJuego(idjuego);
            this.context.Juegos.Remove(juego);
            this.context.SaveChanges();
        }

        //Edita un juego
        public void EditarJuego(int idjuego, string nombre, string descripcion, string categoria, int precio, string foto)
        {
            Juego juego = this.FindJuego(idjuego);
            juego.Nombre = nombre;
            juego.Descripcion = descripcion;
            juego.Categoria = categoria;
            juego.Precio = precio;
            juego.Foto = foto;
            this.context.SaveChanges();
        }
        #endregion

        #region Metodos Reservas
        //Muestra todas las reservas
        public List<Reserva> GetReservas()
        {
            string sql = "sp_all_reservas";
            var consulta = this.context.Reservas.FromSqlRaw(sql);
            return consulta.ToList();
        }

        //Crea una reserva
        public void CrearReserva(Reserva reserva)
        {
            this.context.Reservas.Add(reserva);
            this.context.SaveChanges();
        }

        //Encuentra una reserva gracias a su id
        public Reserva FindReserva(string nombre)
        {
            return this.context.Reservas.FirstOrDefault(z => z.Nombre == nombre);
        }

        //Borra una reserva
        public void DeleteReserva(string nombre)
        {
            Reserva reserva = this.FindReserva(nombre);
            this.context.Reservas.Remove(reserva);
            this.context.SaveChanges();
        }

        public void EditarReserva(string nombre, string telefono, string email, int personas, string fecha, string hora)
        {
            Reserva reserva = this.FindReserva(nombre);
            reserva.Nombre = nombre;
            reserva.Telefono = telefono;
            reserva.Email = email;
            reserva.Personas = personas;
            reserva.Fecha = fecha;
            reserva.Hora = hora;
            this.context.SaveChanges();
        }
        #endregion

        #region Metodos Compras
        //Obtiene el valor del ultimo idcompra que existe en la tabla compras
        private int GetMaxIdCompra()
        {
            if (this.context.Compras.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Compras.Max(z => z.IdCompra) + 1;
            }
        }

        //Inserta en la bbdd cada una de las compras
        public void CreateCompras(Compra compra)
        {
            Compra com = new Compra();
            com.IdCompra = this.GetMaxIdCompra();
            com.IdUsuario = compra.IdUsuario;
            com.Nombre = compra.Nombre;
            this.context.Add(com);
            this.context.SaveChanges();
        }


        //Devuelve las compras de un usuario en especifico
        public List<Compra> BuscarCompras(int idusuario)
        {
            var consulta = from datos in this.context.Compras
                           where datos.IdUsuario == idusuario
                           select datos;
            return consulta.ToList();
        }
        #endregion
    }
}