using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAO
{
    public class AuditoriaTurnoDAO
    {
        public List<AuditoriaTurno> Listar()
        {
            List<AuditoriaTurno> lista = new List<AuditoriaTurno>();

            using (SqlConnection con = new SqlConnection(Conexion.cadena))
            {
                string query = @"SELECT IdAuditoria, IdTurno, IdUsuario, FechaHora, Accion, DatosOriginales, DatosNuevos 
                                 FROM AuditoriaTurno ORDER BY FechaHora DESC";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AuditoriaTurno auditoria = new AuditoriaTurno
                        {
                            IdAuditoria = Convert.ToInt32(dr["IdAuditoria"]),
                            IdTurno = Convert.ToInt32(dr["IdTurno"]),
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            FechaHora = Convert.ToDateTime(dr["FechaHora"]),
                            Accion = dr["Accion"].ToString(),
                            DatosOriginales = dr["DatosOriginales"]?.ToString(),
                            DatosNuevos = dr["DatosNuevos"]?.ToString()
                        };

                        lista.Add(auditoria);
                    }
                }
            }

            return lista;
        }
    }
}
