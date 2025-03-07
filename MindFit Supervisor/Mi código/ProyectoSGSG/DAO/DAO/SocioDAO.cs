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
    }
}
