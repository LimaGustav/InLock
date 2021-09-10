using senai.inlock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Interfaces
{
    interface IUsuarioRepository
    {
        /// <summary>
        /// Listar todos os usuarios
        /// </summary>
        /// <returns>Uma lista de jogos</returns>
        List<UsuarioDomain> ListarTodos();

        /// <summary>
        /// Busca um usuario atraves do id
        /// </summary>
        /// <param name="id"> Id do jogo a ser buscado</param>
        /// <returns> O jogo buscado</returns>
        UsuarioDomain BuscarPorId(int id);

        /// <summary>
        /// Cadastra um novo usuario
        /// </summary>
        /// <param name="novoUsuario">Usuario a ser cadastrado</param>
        void Cadastrar(UsuarioDomain novoUsuario);


        /// <summary>
        /// Atualizar um Usuario atraves do id 
        /// </summary>
        /// <param name="id"> O id do Usuario a ser atualizado</param>
        /// <param name="usuarioAtualizado">Usuario com os dados atualizado</param>
        void AtualizarIdUrl(int id, UsuarioDomain usuarioAtualizado);


        /// <summary>
        /// Deleta um usuario existente
        /// </summary>
        /// <param name="id">id do Usuario a ser deletado</param>
        void Deletar(int id);
    }
}
