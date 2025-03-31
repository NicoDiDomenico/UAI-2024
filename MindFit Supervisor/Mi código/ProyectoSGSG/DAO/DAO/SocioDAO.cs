using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Modelo;

namespace DAO
{
    public class SocioDAO
    {
        public List<Socio> Listar()
        {
            List<Socio> lista = new List<Socio>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                    SELECT IdSocio, NombreYApellido, Email, Telefono, Direccion, Ciudad, 
                           NroDocumento, Genero, FechaNacimiento, ObraSocial, [Plan], 
                           EstadoSocio, FechaInicioActividades, FechaFinActividades, 
                           FechaNotificacion, RespuestaNotificacion
                    FROM Socio
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Socio unSocio = new Socio
                            {
                                IdSocio = Convert.ToInt32(dr["IdSocio"]),
                                NombreYApellido = dr["NombreYApellido"].ToString(),
                                Email = dr["Email"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Ciudad = dr["Ciudad"].ToString(),
                                NroDocumento = Convert.ToInt32(dr["NroDocumento"]),
                                Genero = dr["Genero"].ToString(),
                                FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                                ObraSocial = dr["ObraSocial"] != DBNull.Value ? dr["ObraSocial"].ToString() : null,
                                Plan = dr["Plan"] != DBNull.Value ? dr["Plan"].ToString() : null,
                                EstadoSocio = dr["EstadoSocio"].ToString(),
                                FechaInicioActividades = dr["FechaInicioActividades"] != DBNull.Value ? Convert.ToDateTime(dr["FechaInicioActividades"]) : (DateTime?)null,
                                FechaFinActividades = dr["FechaFinActividades"] != DBNull.Value ? Convert.ToDateTime(dr["FechaFinActividades"]) : (DateTime?)null,
                                FechaNotificacion = dr["FechaNotificacion"] != DBNull.Value ? Convert.ToDateTime(dr["FechaNotificacion"]) : (DateTime?)null,
                                RespuestaNotificacion = dr["RespuestaNotificacion"] != DBNull.Value ? Convert.ToBoolean(dr["RespuestaNotificacion"]) : (bool?)null
                            };

                            lista.Add(unSocio);
                        }
                    }
                }
                catch (Exception)
                {
                    lista = new List<Socio>();
                }
            }
            return lista;
        }

        public Socio GetSocio(int IdSocio)
        {
            Socio unSocio = null; // Se inicializa como null para validar si se encontró un socio

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT IdSocio, NombreYApellido, Email, Telefono, Direccion, Ciudad, 
                               NroDocumento, Genero, FechaNacimiento, ObraSocial, [Plan], 
                               EstadoSocio, FechaInicioActividades, FechaFinActividades, 
                               FechaNotificacion, RespuestaNotificacion
                        FROM Socio
                        WHERE IdSocio = @IdSocio;

                        SELECT IdRutina, IdSocio, FechaModificacion, Dia 
                        FROM Rutina
                        WHERE IdSocio = @IdSocio AND Activa = 1;
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IdSocio", IdSocio); // Se agrega el parámetro

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Leer datos del socio
                        if (dr.Read()) // Verifica si encontró un socio
                        {
                            unSocio = new Socio
                            {
                                IdSocio = Convert.ToInt32(dr["IdSocio"]),
                                NombreYApellido = dr["NombreYApellido"].ToString(),
                                Email = dr["Email"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Ciudad = dr["Ciudad"].ToString(),
                                NroDocumento = Convert.ToInt32(dr["NroDocumento"]),
                                Genero = dr["Genero"].ToString(),
                                FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                                ObraSocial = dr["ObraSocial"] != DBNull.Value ? dr["ObraSocial"].ToString() : null,
                                Plan = dr["Plan"] != DBNull.Value ? dr["Plan"].ToString() : null,
                                EstadoSocio = dr["EstadoSocio"].ToString(),
                                FechaInicioActividades = dr["FechaInicioActividades"] != DBNull.Value ? Convert.ToDateTime(dr["FechaInicioActividades"]) : (DateTime?)null,
                                FechaFinActividades = dr["FechaFinActividades"] != DBNull.Value ? Convert.ToDateTime(dr["FechaFinActividades"]) : (DateTime?)null,
                                FechaNotificacion = dr["FechaNotificacion"] != DBNull.Value ? Convert.ToDateTime(dr["FechaNotificacion"]) : (DateTime?)null,
                                RespuestaNotificacion = dr["RespuestaNotificacion"] != DBNull.Value ? Convert.ToBoolean(dr["RespuestaNotificacion"]) : (bool?)null,
                                Rutinas = new List<Rutina>() // Inicializar la lista de rutinas
                            };
                        }

                        // Pasar a la segunda consulta (Rutinas)
                        if (unSocio != null && dr.NextResult())
                        {
                            while (dr.Read()) // Leer las rutinas asociadas
                            {
                                Rutina rutina = new Rutina
                                {
                                    IdRutina = Convert.ToInt32(dr["IdRutina"]),
                                    FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"]),
                                    Dia = dr["Dia"].ToString(),
                                    Socio = new Socio
                                    {
                                        IdSocio = Convert.ToInt32(dr["IdSocio"]) // Aquí se asigna el ID al objeto Socio
                                    }
                                };
                                unSocio.Rutinas.Add(rutina);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en GetSocio: {ex.Message}");
                    unSocio = null; // En caso de error, se devuelve null
                }
            }
            return unSocio; // Retorna el socio encontrado con sus rutinas o null si no existe
        }

        public int Registrar(Socio unSocio, out string mensaje)
        {
            int idSocioGenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_RegistrarSocio", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros del socio
                    cmd.Parameters.AddWithValue("@NombreYApellido", unSocio.NombreYApellido);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", unSocio.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Genero", unSocio.Genero);
                    cmd.Parameters.AddWithValue("@NroDocumento", unSocio.NroDocumento);
                    cmd.Parameters.AddWithValue("@Ciudad", unSocio.Ciudad);
                    cmd.Parameters.AddWithValue("@Direccion", unSocio.Direccion);
                    cmd.Parameters.AddWithValue("@Telefono", unSocio.Telefono);
                    cmd.Parameters.AddWithValue("@Email", unSocio.Email);
                    cmd.Parameters.AddWithValue("@ObraSocial", (object)unSocio.ObraSocial ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Plan", (object)unSocio.Plan ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EstadoSocio", unSocio.EstadoSocio);
                    cmd.Parameters.AddWithValue("@FechaInicioActividades", unSocio.FechaInicioActividades);
                    cmd.Parameters.AddWithValue("@FechaFinActividades", unSocio.FechaFinActividades);
                    cmd.Parameters.AddWithValue("@FechaNotificacion", (object)unSocio.FechaNotificacion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@RespuestaNotificacion", (object)unSocio.RespuestaNotificacion ?? DBNull.Value);

                    // Parámetro de salida
                    SqlParameter pIdSocio = new SqlParameter("@IdSocio", SqlDbType.Int);
                    pIdSocio.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pIdSocio);

                    SqlParameter pMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                    pMensaje.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pMensaje);

                    //// En otros casos lo hice en el front ahora lo hice en el back
                    // Enviar lista de rutinas como DataTable con FechaModificacion
                    DataTable tablaRutinas = new DataTable();
                    tablaRutinas.Columns.Add("FechaModificacion", typeof(DateTime));
                    tablaRutinas.Columns.Add("Dia", typeof(string));

                    foreach (var rutina in unSocio.Rutinas)
                    {
                        tablaRutinas.Rows.Add(DateTime.Now, rutina.Dia);
                    }
                    ////

                    SqlParameter pRutinas = cmd.Parameters.AddWithValue("@Rutinas", tablaRutinas);
                    pRutinas.SqlDbType = SqlDbType.Structured;

                    // Ejecutar consulta
                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    idSocioGenerado = Convert.ToInt32(cmd.Parameters["@IdSocio"].Value);
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar el socio: " + ex.Message;
            }

            return idSocioGenerado;
        }
        public Boolean Actualizar(Socio unSocio, out string mensaje)
        {
            Boolean rta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ActualizarSocio", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros del socio
                    cmd.Parameters.AddWithValue("@IdSocio", unSocio.IdSocio);
                    cmd.Parameters.AddWithValue("@NombreYApellido", unSocio.NombreYApellido);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", unSocio.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Genero", unSocio.Genero);
                    cmd.Parameters.AddWithValue("@NroDocumento", unSocio.NroDocumento);
                    cmd.Parameters.AddWithValue("@Ciudad", unSocio.Ciudad);
                    cmd.Parameters.AddWithValue("@Direccion", unSocio.Direccion);
                    cmd.Parameters.AddWithValue("@Telefono", unSocio.Telefono);
                    cmd.Parameters.AddWithValue("@Email", unSocio.Email);
                    cmd.Parameters.AddWithValue("@ObraSocial", (object)unSocio.ObraSocial ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Plan", (object)unSocio.Plan ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EstadoSocio", unSocio.EstadoSocio);
                    cmd.Parameters.AddWithValue("@FechaInicioActividades", (object)unSocio.FechaInicioActividades ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FechaFinActividades", (object)unSocio.FechaFinActividades ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FechaNotificacion", (object)unSocio.FechaNotificacion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@RespuestaNotificacion", (object)unSocio.RespuestaNotificacion ?? DBNull.Value);

                    SqlParameter pMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                    pMensaje.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pMensaje);

                    // **Actualizar rutinas sin borrar todas**
                    DataTable tablaRutinas = new DataTable();
                    tablaRutinas.Columns.Add("FechaModificacion", typeof(DateTime));
                    tablaRutinas.Columns.Add("Dia", typeof(string));

                    foreach (var rutina in unSocio.Rutinas)
                    {
                        tablaRutinas.Rows.Add(DateTime.Now, rutina.Dia);
                    }

                    SqlParameter pRutinas = cmd.Parameters.AddWithValue("@Rutinas", tablaRutinas);
                    pRutinas.SqlDbType = SqlDbType.Structured;

                    // Ejecutar consulta
                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    rta = true;
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al actualizar el socio: " + ex.Message;
            }

            return rta;
        }

        public bool Eliminar(Socio obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARSOCIO", oconexion);
                    cmd.Parameters.AddWithValue("@IdSocio", obj.IdSocio);
                    cmd.Parameters.Add("@Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["@Respuesta"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            return respuesta;
        }

        public List<Socio> ListarSociosActuales(int IdEntrenador, int idRangoHorarioActual)
        {
            List<Socio> lista = new List<Socio>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT s.IdSocio, s.NombreYApellido
                        FROM Socio s
                        INNER JOIN Turno t ON s.IdSocio = t.IdSocio
                        INNER JOIN Usuario u ON t.IdUsuario = u.IdUsuario
                        INNER JOIN RangoHorario rh ON t.IdRangoHorario = rh.IdRangoHorario
                        WHERE u.IdUsuario = @IdEntrenador 
                        AND t.IdRangoHorario = @idRangoHorarioActual 
                        AND t.EstadoTurno = 'En Curso'
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        // 🔹 Corrección del parámetro
                        cmd.Parameters.AddWithValue("@IdEntrenador", IdEntrenador);
                        cmd.Parameters.AddWithValue("@idRangoHorarioActual", idRangoHorarioActual);

                        oconexion.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Socio unSocio = new Socio
                                {
                                    IdSocio = Convert.ToInt32(dr["IdSocio"]),
                                    NombreYApellido = dr["NombreYApellido"].ToString()
                                };

                                lista.Add(unSocio);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error en ListarSociosActuales: " + ex.Message);
                    lista = new List<Socio>();
                }
            }
            return lista;
        }

        public bool ActualizarEstadoSocio(int idSocio, string nuevoEstado)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cadena))
                {
                    string query = "UPDATE Socio SET EstadoSocio = @estado WHERE IdSocio = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@estado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@id", idSocio);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
