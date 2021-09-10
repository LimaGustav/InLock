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
    public class JogosController : ControllerBase
    {
        private IJogoRepository _jogoRepository { get; set; }

        public JogosController()
        {
            _jogoRepository = new JogoRepository();
        }

        /// <summary>
        /// Lista todos os jogos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<JogoDomain> listaJogos = _jogoRepository.ListarTodos();

            return Ok(listaJogos);
        }

        [HttpPost]
        public IActionResult Post(JogoDomain novoJogo)
        {
            if (novoJogo.descricao == null || novoJogo.dataLancamento == null || novoJogo.idEstudio <= 0)
            {
                return NotFound(
                       new
                       {
                           mensagem = "Informações incompletas",
                           error = true
                       }
                    );
            }

            try
            {
                _jogoRepository.Cadastrar(novoJogo);

                return StatusCode(201);
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
                _jogoRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }
    }
}
