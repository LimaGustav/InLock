using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repository
{
    public class EstudioRepository : IEstudioRepository
    {

        private string stringConexao = "Data Source=DESKTOP-L3Q203S\\SQLEXPRESS; initial catalog=inlock_games_tarde; user Id=sa; pwd=senai@132";
        public void AtualizarIdUrl(int id, EstudioDomain estudioAtualizado)
        {
            throw new NotImplementedException();
        }

        public EstudioDomain BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(EstudioDomain novoEstudio)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public List<EstudioDomain> ListarEmpresasEJogos()
        {
            List<EstudioDomain> listaEstudio = new List<EstudioDomain>();


            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelect = @"SELECT ESTUDIO.idEstudio, nomeEstudio, ISNULL(descricao, 'Não Cadastrado') descricao, 
                                        ISNULL(nomeJogo, 'Não cadastrado') nomeJogo, ISNULL(idJogo, 0) idJogo, ISNULL(valor, 0) valor, 
                                        ISNULL(dataLancamento, '') dataLancamento FROM ESTUDIO LEFT JOIN JOGO ON JOGO.idEstudio = ESTUDIO.idEstudio";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        EstudioDomain ESTUDIO = new EstudioDomain()
                        {
                            idEstudio = Convert.ToInt32(rdr["idEstudio"]),
                            nomeEstudio = rdr["nomeEstudio"].ToString(),
                            Jogo = new JogoDomain()
                            {
                                idEstudio = Convert.ToInt32(rdr["idEstudio"]),
                                idJogo = Convert.ToInt32(rdr["idJogo"]),
                                nomeJogo = rdr["nomeJogo"].ToString(),
                                descricao = rdr["descricao"].ToString(),
                                valor = Convert.ToSingle(rdr["valor"]),
                                dataLancamento = Convert.ToDateTime(rdr["dataLancamento"])
                            }
                        };

                        listaEstudio.Add(ESTUDIO);
                    }
                }
            }

            return listaEstudio;
        }


        public List<EstudioDomain> ListarTodos()
        {
            List<EstudioDomain> listaEstudio = new List<EstudioDomain>();


            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelect = "SELECT idEstudio, nomeEstudio FROM ESTUDIO";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        EstudioDomain ESTUDIO = new EstudioDomain()
                        {
                            idEstudio = Convert.ToInt32(rdr[0]),
                            nomeEstudio = rdr[1].ToString()
                        };

                        listaEstudio.Add(ESTUDIO);
                    }
                }
            }

            return listaEstudio;
        }
    }
}



