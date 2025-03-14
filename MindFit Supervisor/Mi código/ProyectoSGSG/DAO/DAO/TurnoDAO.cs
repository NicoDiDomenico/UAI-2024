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
        public bool ModificarEstadoTurno(int idTurno, string nuevoEstado)
        {
            bool respuesta = false;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    string query = @"
                        UPDATE Turno 
                        SET EstadoTurno = @EstadoTurno 
                        WHERE IdTurno = @IdTurno;

                        -- Restar en 1 el CupoActual si el turno se cancela
                        IF @EstadoTurno = 'Cancelado'
                        BEGIN
                            UPDATE RangoHorario 
                            SET CupoActual = CupoActual - 1
                            WHERE IdRangoHorario = (SELECT IdRangoHorario FROM Turno WHERE IdTurno = @IdTurno)
                            AND CupoActual > 0; -- Evita valores negativos
                        END
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@EstadoTurno", nuevoEstado);
                        cmd.Parameters.AddWithValue("@IdTurno", idTurno);

                        oconexion.Open();
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        respuesta = filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al modificar el estado del turno: {ex.Message}");
                respuesta = false;
            }

            return respuesta;
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

        public List<Turno> ListarTurnosHorarioActual()
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
                           s.IdSocio, s.NombreYApellido AS NombreSocio, rh.CupoActual, rh.CupoMaximo
                        FROM Turno t
                        INNER JOIN Usuario u ON t.IdUsuario = u.IdUsuario
                        INNER JOIN Socio s ON t.IdSocio = s.IdSocio
                        INNER JOIN RangoHorario rh ON t.IdRangoHorario = rh.IdRangoHorario
                        WHERE t.FechaTurno = CAST(GETDATE() AS DATE) -- Solo turnos de hoy
                        AND rh.HoraDesde <= CAST(GETDATE() AS TIME)  -- Horario actual o anterior
                        AND rh.HoraHasta >= CAST(GETDATE() AS TIME)  -- Todavía dentro del horario
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Turno unTurno = new Turno
                            {
                                IdTurno = Convert.ToInt32(dr["IdTurno"]),
                                FechaTurno = Convert.ToDateTime(dr["FechaTurno"]),
                                unRangoHorario = new RangoHorario
                                {
                                    IdRangoHorario = Convert.ToInt32(dr["IdRangoHorario"]),
                                    HoraDesde = (TimeSpan)dr["HoraDesde"],
                                    HoraHasta = (TimeSpan)dr["HoraHasta"],
                                    CupoActual = dr["CupoActual"] != DBNull.Value ? Convert.ToInt32(dr["CupoActual"]) : 0,
                                    CupoMaximo = dr["CupoMaximo"] != DBNull.Value ? Convert.ToInt32(dr["CupoMaximo"]) : 0
                                },
                                EstadoTurno = dr["EstadoTurno"].ToString(),
                                CodigoIngreso = dr["CodigoIngreso"].ToString(),
                                unUsuario = new Usuario
                                {
                                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                    NombreYApellido = dr["NombreEntrenador"].ToString()
                                },
                                unSocio = new Socio
                                {
                                    IdSocio = Convert.ToInt32(dr["IdSocio"]),
                                    NombreYApellido = dr["NombreSocio"].ToString()
                                }
                            };

                            lista.Add(unTurno);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error en ListarTurnosHorarioActual: " + ex.Message);
                    lista = new List<Turno>();
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

        public bool Eliminar(int idTurno, int idRangoHorario, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARTURNO", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    cmd.Parameters.AddWithValue("@IdTurno", idTurno);
                    cmd.Parameters.AddWithValue("@IdRangoHorario", idRangoHorario);

                    // Parámetros de salida
                    cmd.Parameters.Add("@Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    // Obtener valores de los parámetros de salida
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

        public bool ValidarCodigoIngreso(string codigo, out int idTurno, out int idRangoHorario, out string mensaje)
        {
            idTurno = 0;
            idRangoHorario = 0;
            mensaje = string.Empty;

            DateTime fechaActual = DateTime.Today;
            TimeSpan horaActual = DateTime.Now.TimeOfDay;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT t.IdTurno, t.IdRangoHorario, t.FechaTurno, rh.HoraDesde, rh.HoraHasta
                        FROM Turno t
                        INNER JOIN RangoHorario rh ON t.IdRangoHorario = rh.IdRangoHorario
                        WHERE t.CodigoIngreso = @CodigoIngreso
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@CodigoIngreso", codigo);

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            idTurno = Convert.ToInt32(dr["IdTurno"]);
                            idRangoHorario = Convert.ToInt32(dr["IdRangoHorario"]);
                            DateTime fechaTurno = Convert.ToDateTime(dr["FechaTurno"]);
                            TimeSpan horaDesde = (TimeSpan)dr["HoraDesde"];
                            TimeSpan horaHasta = (TimeSpan)dr["HoraHasta"];

                            // Validar la fecha del turno
                            if (fechaTurno != fechaActual)
                            {
                                mensaje = "El turno no corresponde a la fecha actual.";
                                return false;
                            }

                            // Validar que no sea demasiado temprano para ingresar
                            if (horaDesde > horaActual)
                            {
                                mensaje = "Es demasiado temprano para ingresar. El turno aún no ha comenzado.";
                                return false;
                            }

                            // Validar que no haya pasado el tiempo del turno
                            if (horaHasta < horaActual)
                            {
                                mensaje = "El turno ha expirado. No puede ingresar.";
                                return false;
                            }

                            return true; // Todas las validaciones pasaron correctamente
                        }
                        else
                        {
                            mensaje = "Código de ingreso no encontrado.";
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    mensaje = "Error en la validación del ingreso: " + ex.Message;
                    return false;
                }
            }
        }


        public bool ActualizarEstadoTurno(int idTurno, int idRangoHorario)
        {
            bool resultado = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        UPDATE Turno
                        SET EstadoTurno = 'Finalizado'
                        WHERE IdTurno = @IdTurno;

                        UPDATE RangoHorario
                        SET CupoActual = CASE 
                                            WHEN CupoActual > 0 THEN CupoActual - 1 
                                            ELSE 0 
                                         END
                        WHERE IdRangoHorario = @IdRangoHorario;
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@IdTurno", idTurno);
                    cmd.Parameters.AddWithValue("@IdRangoHorario", idRangoHorario);

                    oconexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    resultado = filasAfectadas > 0;
                }
                catch (Exception)
                {
                    resultado = false;
                }
            }

            return resultado;
        }
    }
}
