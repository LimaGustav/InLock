using senai.inlock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Interfaces
{
    interface IJogoRepository
    {

        /// <summary>
        /// Listar todos os jogos
        /// </summary>
        /// <returns>Uma lista de jogos</returns>
        List<JogoDomain> ListarTodos();

        /// <summary>
        /// Busca um jogo atraves do id
        /// </summary>
        /// <param name="id"> Id do jogo a ser buscado</param>
        /// <returns> O jogo buscado</returns>
        JogoDomain BuscarPorId(int id);

        /// <summary>
        /// Cadastra um novo jogo
        /// </summary>
        /// <param name="novoJogo"> Jogo a ser cadastrado</param>
        void Cadastrar(JogoDomain novoJogo);


        /// <summary>
        /// Atualizar um jogo atraves do id 
        /// </summary>
        /// <param name="id"> O id do jogo a ser atualizado</param>
        /// <param name="jogoAtualizado"> Jogo com os dados atualizado</param>
        void AtualizarIdUrl(int id, JogoDomain jogoAtualizado);


        /// <summary>
        /// Deleta um jogo existente
        /// </summary>
        /// <param name="id"> id do jogo a ser deletado</param>
        void Deletar(int id);
    }
}
