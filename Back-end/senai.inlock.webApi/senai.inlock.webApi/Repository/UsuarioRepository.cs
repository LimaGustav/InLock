﻿using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {

        // Gustavo
        private string stringConexao = "Data Source=DESKTOP-8VJGUSR\\SQLEXPRESS; initial catalog=inlock_games_tarde; user Id=sa; pwd=senai@132";
        public void AtualizarIdUrl(int id, UsuarioDomain usuarioAtualizado)
        {
            throw new NotImplementedException();
        }

        public UsuarioDomain BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(UsuarioDomain novoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO USUARIO (email, senha, idTipoUsuario) VALUES (@email, @senha, @idTipoUsuario)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@email", novoUsuario.email);
                    cmd.Parameters.AddWithValue("@senha", novoUsuario.senha);
                    cmd.Parameters.AddWithValue("@idTipoUsuario", novoUsuario.idTipoUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM USUARIO WHERE idUsuario = @idUsuario";
            }
        }

        public List<UsuarioDomain> ListarTodos()
        {
            List<UsuarioDomain> listaUsuarios = new List<UsuarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = @"SELECT	idUsuario, 
		                                            email, 
		                                            U.idTipoUsuario, 
		                                            titulo 
                                            FROM USUARIO U
                                            LEFT JOIN TIPOUSUARIO T
                                            ON U.idTipoUsuario = T.idTipoUsuario";

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    con.Open();

                    SqlDataReader rdr;

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        UsuarioDomain usuario = new UsuarioDomain()
                        {
                            idUsuario = Convert.ToInt32(rdr["idUsuario"]),
                            email = rdr["email"].ToString(),
                            idTipoUsuario = Convert.ToInt32(rdr["idTipoUsuario"]),
                            TipoUsuario = new TipoUsuarioDomain()
                            {
                                idTipoUsuario = Convert.ToInt32(rdr["idTipoUsuario"]),
                                titulo = rdr["titulo"].ToString()
                            }
                        };
                        listaUsuarios.Add(usuario);
                    }
                    return listaUsuarios;
                }
            }
        }
    }
}
