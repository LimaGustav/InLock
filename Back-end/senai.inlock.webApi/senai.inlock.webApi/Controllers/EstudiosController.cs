using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiosController : ControllerBase
    {
        private IEstudioRepository _estudioRepository { get; set; }

        public EstudiosController()
        {
            _estudioRepository = new EstudioRepository();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            List<EstudioDomain> listaEstudio = _estudioRepository.ListarTodos();

            return Ok(listaEstudio);
        }

        [Authorize]
        [HttpGet("ListarEspresas")]
        public IActionResult GetEmpresa()
        {
            List<EstudioDomain> listaEstudio = _estudioRepository.ListarEmpresasEJogos();
            return Ok(listaEstudio);
        }
    }
}
