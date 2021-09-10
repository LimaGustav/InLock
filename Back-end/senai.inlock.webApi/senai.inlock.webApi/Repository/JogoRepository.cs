using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repository
{
    public class JogoRepository : IJogoRepository
    {
        // Gustavo
        //private string stringConexao = "Data Source=DESKTOP-8VJGUSR\\SQLEXPRESS; initial catalog=inlock_games_tarde; user Id=sa; pwd=senai@132";

        //João
        private string stringConexao = "Data Source=DESKTOP-L3Q203S\\SQLEXPRESS; initial catalog=inlock_games_tarde; user Id=sa; pwd=senai@132";

        public void AtualizarIdUrl(int id, JogoDomain jogoAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdate = @"UPDATE JOGO
                                        SET 
	                                    nomeJogo = @nomeJogo, 
	                                    descricao = @descricao,
                                        dataLancamento = @dataLancamento,
                                        valor = @valor,
                                        idEstudio = @idEstudio
                                    WHERE idJogo = @idJogo";

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@nomeJogo", jogoAtualizado.nomeJogo);

                    cmd.Parameters.AddWithValue("@dataLancamento", jogoAtualizado.dataLancamento);

                    cmd.Parameters.AddWithValue("@descricao", jogoAtualizado.descricao);

                    cmd.Parameters.AddWithValue("@valor", jogoAtualizado.valor);

                    cmd.Parameters.AddWithValue("@idEstudio", jogoAtualizado.idEstudio);

                    cmd.Parameters.AddWithValue("@idJogo", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public JogoDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelect = @"SELECT	idJogo,
		                                        nomeJogo, 
		                                        descricao, 
		                                        dataLancamento, 
		                                        valor, 
		                                        J.idEstudio, 
		                                        nomeEstudio 
                                        FROM JOGO J
                                        LEFT JOIN ESTUDIO E
                                        ON J.idEstudio = E.idEstudio
                                        WHERE J.idJogo = @idJogo";

                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    cmd.Parameters.AddWithValue("@idJogo", id);

                    con.Open();

                    SqlDataReader rdr;

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        JogoDomain jogo = new JogoDomain()
                        {
                            idJogo = Convert.ToInt32(rdr["idJogo"]),
                            nomeJogo = rdr["nomeJogo"].ToString(),
                            descricao = rdr["descricao"].ToString(),
                            dataLancamento = Convert.ToDateTime(rdr["dataLancamento"]),
                            valor = Convert.ToSingle(rdr["valor"]),
                            idEstudio = Convert.ToInt32(rdr["idEstudio"]),
                            estudio = new EstudioDomain()
                            {
                                idEstudio = Convert.ToInt32(rdr["idEstudio"]),
                                nomeEstudio = rdr["nomeEstudio"].ToString()
                            }
                        };

                        return jogo;
                    }
                    return null;
                }
            }
        }

        public void Cadastrar(JogoDomain novoJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = @"INSERT INTO JOGO(nomeJogo, 
				                                        descricao, 
				                                        dataLancamento, 
				                                        valor, 
				                                        idEstudio)
                    VALUES (@nomeJogo,@descricao,@dataLancamento,@valor,@idEstudio)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@nomeJogo", novoJogo.nomeJogo);

                    cmd.Parameters.AddWithValue("@descricao", novoJogo.descricao);

                    cmd.Parameters.AddWithValue("@dataLancamento", novoJogo.dataLancamento);

                    cmd.Parameters.AddWithValue("@valor", novoJogo.valor);

                    cmd.Parameters.AddWithValue("@idEstudio", novoJogo.idEstudio);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM JOGO WHERE idJogo = @idJogo";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idJogo", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<JogoDomain> ListarTodos()
        {
            List<JogoDomain> listaJogos = new List<JogoDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelect = @"SELECT	idJogo,
		                                        nomeJogo, 
		                                        descricao, 
		                                        dataLancamento, 
		                                        valor, 
		                                        J.idEstudio, 
		                                        nomeEstudio 
                                        FROM JOGO J
                                        LEFT JOIN ESTUDIO E
                                        ON J.idEstudio = E.idEstudio";

                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    con.Open();

                    SqlDataReader rdr;

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        JogoDomain jogo = new JogoDomain()
                        {
                            idJogo = Convert.ToInt32(rdr["idJogo"]),
                            nomeJogo = rdr["nomeJogo"].ToString(),
                            descricao = rdr["descricao"].ToString(),
                            dataLancamento = Convert.ToDateTime(rdr["dataLancamento"]),
                            valor = Convert.ToSingle(rdr["valor"]),
                            idEstudio = Convert.ToInt32(rdr["idEstudio"]),
                            estudio = new EstudioDomain()
                            {
                                idEstudio = Convert.ToInt32(rdr["idEstudio"]),
                                nomeEstudio = rdr["nomeEstudio"].ToString()
                            }
                        };

                        listaJogos.Add(jogo);
                    }
                    return listaJogos;
                }
            }
        }
    }
}
