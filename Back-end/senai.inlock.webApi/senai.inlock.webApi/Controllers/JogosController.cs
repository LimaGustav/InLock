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
    [Authorize]
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
                JogoDomain jogoBuscado = _jogoRepository.BuscarPorId(id);


                if (jogoBuscado == null)
                {
                    return NotFound(
                            new
                            {
                                mensagem = "Usuário não encontrado"
                            }
                        );
                }

                return Ok(jogoBuscado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        // Somento Administrador poderá cadastrar um novo jogo
        [Authorize(Roles = "ADMINISTRADOR")]
        /// <summary>
        /// Cadastra um novo jogo
        /// </summary>
        /// <param name="novoJogo">Objeto jogo a ser cadastrado</param>
        /// <returns></returns>
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

        /// <summary>
        /// Atualiza jogo Através do id
        /// </summary>
        /// <param name="id">Id do jogo a ser atualizado </param>
        /// <param name="jogoAtualizado">Objeto jogo com dados atualizados</param>
        /// <returns></returns>
        [Authorize(Roles ="ADMINISTRADOR")]
        [HttpPut("{id}")]
        public IActionResult UpdateByUrl(int id, JogoDomain jogoAtualizado)
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

            JogoDomain jogoBuscado = _jogoRepository.BuscarPorId(id);

            if (jogoBuscado == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Jogo não encontrado"
                        }
                    );
            }

            try
            {
                if (jogoAtualizado.nomeJogo == null) jogoAtualizado.nomeJogo = jogoBuscado.nomeJogo;

                if (jogoAtualizado.descricao == null) jogoAtualizado.descricao = jogoBuscado.descricao;

                if (jogoAtualizado.dataLancamento == null) jogoAtualizado.dataLancamento = jogoBuscado.dataLancamento;

                if (jogoAtualizado.valor <= 0) jogoAtualizado.valor = jogoBuscado.valor;

                if (jogoAtualizado.idEstudio <= 0) jogoAtualizado.idEstudio = jogoBuscado.idEstudio;

                _jogoRepository.AtualizarIdUrl(id, jogoAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }
        /// <summary>
        /// Deleta um jogo cadastrado
        /// </summary>
        /// <param name="id">Id do jogo a ser deletado</param>
        /// <returns></returns>
        // Somente administrador poderá deletar um jogo cadastrado
        [Authorize(Roles = "ADMINISTRADOR")]
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
