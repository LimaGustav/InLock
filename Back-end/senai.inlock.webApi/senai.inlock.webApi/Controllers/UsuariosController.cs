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

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Id inválido",
                            erro = true
                        }
                    );
            }

            try
            {
                UsuarioDomain usuarioBuscado = _usuarioRepository.BuscarPorId(id);

                if (usuarioBuscado == null)
                {
                    return NotFound(
                            new
                            {
                                mensagem = "Usuário não encontrado"
                            }
                        );
                }

                return Ok(usuarioBuscado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateByUrl(int id, UsuarioDomain usuarioAtualizado)
        {
            if (id <= 0)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Id inválido",
                            erro = true
                        }
                    );
            }

            UsuarioDomain usuarioBuscado = _usuarioRepository.BuscarPorId(id);

            if (usuarioBuscado == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Usuário não encontrado"
                        }
                    );
            }

            try
            {
                if (usuarioAtualizado.senha == null) usuarioAtualizado.senha = usuarioBuscado.senha;

                if (usuarioAtualizado.idTipoUsuario == 0) usuarioAtualizado.idTipoUsuario = usuarioBuscado.idTipoUsuario;

                _usuarioRepository.AtualizarIdUrl(id, usuarioAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Id inválido",
                            erro = true
                        }
                    );
            }

            try
            {
                _usuarioRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }
            
    }
}