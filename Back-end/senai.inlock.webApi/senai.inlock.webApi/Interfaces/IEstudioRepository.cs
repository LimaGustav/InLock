using senai.inlock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Interfaces
{
    interface IEstudioRepository
    {
        /// <summary>
        /// Lista todos os estudios existentes
        /// </summary>
        /// <returns> Uma lista de estudio</returns>
        List<EstudioDomain> ListarTodos();

        /// <summary>
        /// Buscar um estudio atraves do id
        /// </summary>
        /// <param name="id"> Id do estudio a ser buscado</param>
        /// <returns> O estudio buscado</returns>
        EstudioDomain BuscarPorId(int id);

        /// <summary>
        /// Cadastrar um novo estudio
        /// </summary>
        /// <param name="novoEstudio"> Estudio a ser cadastrado</param>
        void Cadastrar(EstudioDomain novoEstudio);

        /// <summary>
        /// Atualizar um estudio atraves do id 
        /// </summary>
        /// <param name="id"> o id do estudio a ser atualizado</param>
        /// <param name="estudioAtualizado"> O estudio com os dados atualizados</param>
        void AtualizarIdUrl(int id, EstudioDomain estudioAtualizado);


        /// <summary>
        /// Delestar um estudio existente
        /// </summary>
        /// <param name="id"> id do estudio a ser deletado</param>
        void Deletar(int id);

        /// <summary>
        /// Lista os estudios e suas empresas
        /// </summary>
        /// <returns>Retorna a lista de empresas e seus jogos</returns>
        List<EstudioDomain> ListarEmpresasEJogos();
    }
}
