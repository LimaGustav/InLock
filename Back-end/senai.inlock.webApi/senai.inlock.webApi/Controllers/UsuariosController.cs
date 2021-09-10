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
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }


        public UsuariosController()
        {
            _usuarioRepository = new UsuarioRepository();

        }

        [HttpGet]
        public IActionResult Get()
        {
            List<UsuarioDomain> listaUsuarios = _usuarioRepository.ListarTodos();

            return Ok(listaUsuarios);
        }

        [HttpPost]
        public IActionResult Post(UsuarioDomain novoUsuario)
        {
            if (novoUsuario.email == null || novoUsuario.senha == null || novoUsuario.idTipoUsuario <= 0)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Dados incompletos",
                            erro = true
                        }
                    );
            }

            try
            {
                _usuarioRepository.Cadastrar(novoUsuario);

                return StatusCode(201);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }
    }
}
