using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class TurnoDAO
    {
        public HashSet<string> ObtenerCodigosExistentes()
        {
            HashSet<string> codigos = new HashSet<string>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = "SELECT CodigoIngreso FROM Turno";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            codigos.Add(dr["CodigoIngreso"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener los códigos de ingreso: " + ex.Message);
                }
            }

            return codigos;
        }

        public List<Turno> Listar(int idSocioSeleccionado)
        {
            List<Turno> lista = new List<Turno>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT t.IdTurno, t.FechaTurno, rh.IdRangoHorario, rh.HoraDesde, rh.HoraHasta, 
                               t.EstadoTurno, t.CodigoIngreso, 
                               u.IdUsuario, u.NombreYApellido AS NombreEntrenador, 
                               s.IdSocio, s.NombreYApellido AS NombreSocio
                        FROM Turno t
                        INNER JOIN Usuario u ON t.IdUsuario = u.IdUsuario
                        INNER JOIN Socio s ON t.IdSocio = s.IdSocio
                        INNER JOIN RangoHorario rh ON t.IdRangoHorario = rh.IdRangoHorario
                        WHERE t.IdSocio = @idSocioSeleccionado
                    ";
                     
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@idSocioSeleccionado", idSocioSeleccionado);

                        oconexion.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Turno unTurno = new Turno
                                {
                                    IdTurno = dr["IdTurno"] != DBNull.Value ? Convert.ToInt32(dr["IdTurno"]) : 0,
                                    FechaTurno = dr["FechaTurno"] != DBNull.Value ? Convert.ToDateTime(dr["FechaTurno"]) : DateTime.MinValue,
                                    unRangoHorario = new RangoHorario
                                    {
                                        IdRangoHorario = dr["IdRangoHorario"] != DBNull.Value ? Convert.ToInt32(dr["IdRangoHorario"]) : 0,
                                        HoraDesde = dr["HoraDesde"] != DBNull.Value ? (TimeSpan)dr["HoraDesde"] : TimeSpan.Zero,
                                        HoraHasta = dr["HoraHasta"] != DBNull.Value ? (TimeSpan)dr["HoraHasta"] : TimeSpan.Zero
                                    },
                                    EstadoTurno = dr["EstadoTurno"]?.ToString() ?? string.Empty,
                                    CodigoIngreso = dr["CodigoIngreso"]?.ToString() ?? string.Empty,
                                    unUsuario = new Usuario
                                    {
                                        IdUsuario = dr["IdUsuario"] != DBNull.Value ? Convert.ToInt32(dr["IdUsuario"]) : 0,
                                        NombreYApellido = dr["NombreEntrenador"]?.ToString() ?? string.Empty
                                    },
                                    unSocio = new Socio
                                    {
                                        IdSocio = dr["IdSocio"] != DBNull.Value ? Convert.ToInt32(dr["IdSocio"]) : 0,
                                        NombreYApellido = dr["NombreSocio"]?.ToString() ?? string.Empty
                                    }
                                };

                                lista.Add(unTurno);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Turno>();
                    Console.WriteLine($"Error al listar turnos: {ex.Message}");
                }
            }

            return lista;
        }

        public int Registrar(Turno obj, out string Mensaje)
        {
            int idTurnoGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARTURNO", oconexion);

                    // Parámetros requeridos por la tabla Turno
                    cmd.Parameters.AddWithValue("@IdRangoHorario", obj.unRangoHorario.IdRangoHorario); // Nuevo campo
                    cmd.Parameters.AddWithValue("@IdUsuario", obj.unUsuario.IdUsuario); // Ahora parte de la clave de RangoHorario_Usuario
                    cmd.Parameters.AddWithValue("@IdSocio", obj.unSocio.IdSocio);
                    cmd.Parameters.AddWithValue("@FechaTurno", obj.FechaTurno);
                    cmd.Parameters.AddWithValue("@EstadoTurno", obj.EstadoTurno);
                    cmd.Parameters.AddWithValue("@CodigoIngreso", obj.CodigoIngreso); // Nuevo campo agregado

                    // Parámetros de salida
                    cmd.Parameters.Add("@IdTurnoResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    // Obtener valores de salida
                    idTurnoGenerado = Convert.ToInt32(cmd.Parameters["@IdTurnoResultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idTurnoGenerado = 0;
                Mensaje = ex.Message;
            }

            return idTurnoGenerado;
        }

        public bool Eliminar(int idTurno, int horarioId, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARTURNO", oconexion);
                    cmd.Parameters.AddWithValue("@IdTurno", idTurno);
                    cmd.Parameters.AddWithValue("@IdRangoHorario", horarioId);
                    cmd.Parameters.Add("@Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["@Respuesta"].Value);
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = ex.Message;
            }

            return respuesta;
        }
    }
}
