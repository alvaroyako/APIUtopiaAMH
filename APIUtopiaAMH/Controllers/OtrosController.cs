﻿using APIUtopiaAMH.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIUtopiaAMH.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OtrosController : ControllerBase
    {
        private RepositoryUtopia repo;

        public OtrosController(RepositoryUtopia repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public ActionResult<int> GetMaxIdUsuarios()
        {
            int idusuario = this.repo.GetMaxIdUsuario();
            return idusuario;
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public ActionResult<int> GetMaxIdCompras()
        {
            int idcompra = this.repo.GetMaxIdCompra();
            return idcompra;
        }
    }
}
