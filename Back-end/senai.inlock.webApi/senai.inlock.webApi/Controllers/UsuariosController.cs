using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        /// <summary>
        /// Cadastra  usuário Cliente
        /// </summary>
        /// <param name="novoUsuario">Objeto usuario a ser cadastrado</param>
        /// <returns></returns>
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
                novoUsuario.idTipoUsuario = 1;

                _usuarioRepository.Cadastrar(novoUsuario);

                return StatusCode(201);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [Authorize(Roles ="ADMINISTRADOR")]
        [HttpPost("admin")]
        public IActionResult PostAdm(UsuarioDomain novoUsuario)
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
        
        [Authorize(Roles ="ADMINISTRADOR")]
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
                _usuarioRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(UsuarioDomain login)
        {
            UsuarioDomain usuarioBuscado = _usuarioRepository.BuscarPorEmailSenha(login.email,login.senha);

            if (usuarioBuscado == null)
            {
                return NotFound("E-mail ou senha inválidos!");
            }

            // return Ok(usuarioBuscado);

            //Define os dados que serão fornecidos no token - Payload
            var minhasClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.idUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuarioBuscado.TipoUsuario.titulo),
                new Claim("Claim personalizada", "Amendoin")
            };

            // Define a chave de acesso ao token
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("inLock-chave-autenticacao"));

            // Define as credenciais do token - signature
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Composição do token
            var meuToken = new JwtSecurityToken(
                    issuer: "Inlock.webAPI",                // emissor do token
                    audience: "Inlock.webAPI",              // destinatário do token
                    claims: minhasClaims,                   // dados definidos acima (linha 39)
                    expires: DateTime.Now.AddMinutes(30),   // tempo de expiração do token
                    signingCredentials: creds               // credenciais do token
                );

            return Ok(new { 
                token = new JwtSecurityTokenHandler().WriteToken(meuToken)
            });



        }

    }
}