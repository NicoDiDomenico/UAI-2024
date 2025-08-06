using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAO
{
    public class AuditoriaAccesoDAO
    {
        public List<AuditoriaAcceso> Listar()
        {
            List<AuditoriaAcceso> lista = new List<AuditoriaAcceso>();

            using (SqlConnection con = new SqlConnection(Conexion.cadena))
            {
                string query = @"SELECT aa.IdAuditoria, aa.IdUsuario, aa.FechaHora, aa.TipoEvento, u.NombreYApellido
                FROM AuditoriaAccesos aa
                INNER JOIN Usuario u
                ON u.IdUsuario = aa.IdUsuario
                ORDER BY FechaHora DESC
                ";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AuditoriaAcceso acceso = new AuditoriaAcceso
                        {
                            IdAuditoria = Convert.ToInt32(dr["IdAuditoria"]),
                            usuario = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                NombreYApellido = Convert.ToString(dr["NombreYApellido"])
                            },
                            FechaHora = Convert.ToDateTime(dr["FechaHora"]),
                            TipoEvento = dr["TipoEvento"].ToString()
                        };

                        lista.Add(acceso);
                    }
                }
            }

            return lista;
        }
    }
}
