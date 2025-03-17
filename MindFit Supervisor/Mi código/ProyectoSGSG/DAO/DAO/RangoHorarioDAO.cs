using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class RangoHorarioDAO
    {
        public List<RangoHorario> Listar()
        {
            List<RangoHorario> lista = new List<RangoHorario>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        select IdRangoHorario, HoraDesde, HoraHasta, CupoMaximo 
                        from RangoHorario
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            RangoHorario unRangoHorario = new RangoHorario();

                            unRangoHorario.IdRangoHorario = Convert.ToInt32(dr["IdRangoHorario"]);
                            unRangoHorario.HoraDesde = (TimeSpan)dr["HoraDesde"];
                            unRangoHorario.HoraHasta = (TimeSpan)dr["HoraHasta"];
                            unRangoHorario.CupoMaximo = Convert.ToInt32(dr["CupoMaximo"]);

                            lista.Add(unRangoHorario);
                        }
                    }
                }
                catch (Exception)
                {
                    lista = new List<RangoHorario>();
                }
            }
            return lista;
        }

        public List<RangoHorario> ListarTodo()
        {
            List<RangoHorario> lista = new List<RangoHorario>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        select rh.IdRangoHorario, rh.HoraDesde, rh.HoraHasta, u.NombreYApellido, u.IdUsuario
                        from RangoHorario rh
                        inner join RangoHorario_Usuario rh_u 
                        on rh.IdRangoHorario = rh_u.IdRangoHorario 
                        inner join Usuario u on u.IdUsuario = rh_u.IdUsuario
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            RangoHorario unRangoHorario = new RangoHorario();

                            unRangoHorario.IdRangoHorario = Convert.ToInt32(dr["IdRangoHorario"]);
                            unRangoHorario.HoraDesde = (TimeSpan)dr["HoraDesde"];
                            unRangoHorario.HoraHasta = (TimeSpan)dr["HoraHasta"];
                            
                            unRangoHorario.UnUsuario = new Usuario();
                            unRangoHorario.UnUsuario.NombreYApellido = Convert.ToString(dr["NombreYApellido"]);
                            unRangoHorario.UnUsuario.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                            

                            lista.Add(unRangoHorario);
                        }
                    }
                }
                catch (Exception)
                {
                    lista = new List<RangoHorario>();
                }
            }
            return lista;
        }

        public List<RangoHorario> ListarParaTurno()
        {
            List<RangoHorario> lista = new List<RangoHorario>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        Select rh_u.IdRangoHorario, rh_u.IdUsuario, rh.HoraDesde, rh.HoraHasta, rh.CupoMaximo, u.NombreYApellido
                        from RangoHorario rh
                        inner join RangoHorario_Usuario rh_u
                        on rh.IdRangoHorario = rh_u.IdRangoHorario
                        inner join Usuario u
                        on rh_u.IdUsuario = u.IdUsuario
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            RangoHorario unRangoHorario = new RangoHorario();

                            unRangoHorario.IdRangoHorario = Convert.ToInt32(dr["IdRangoHorario"]);
                            unRangoHorario.HoraDesde = (TimeSpan)dr["HoraDesde"];
                            unRangoHorario.HoraHasta = (TimeSpan)dr["HoraHasta"];
                            unRangoHorario.CupoMaximo = Convert.ToInt32(dr["CupoMaximo"]);
                            
                            unRangoHorario.UnUsuario = new Usuario();
                            unRangoHorario.UnUsuario.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                            unRangoHorario.UnUsuario.NombreYApellido = Convert.ToString(dr["NombreYApellido"]);
                            

                            lista.Add(unRangoHorario);
                        }
                    }
                }
                catch (Exception)
                {
                    lista = new List<RangoHorario>();
                }
            }
            return lista;
        }

        // Corregido cupo actual
        // Si anda con SP:
        public List<RangoHorario> ListarEntrenadoresDisponibles(int IdRangoHorario, DateTime FechaTurno)
        {
            List<RangoHorario> lista = new List<RangoHorario>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Llamar al Stored Procedure
                    SqlCommand cmd = new SqlCommand("SP_ListarEntrenadoresDisponibles", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Pasar parámetros al SP
                    cmd.Parameters.AddWithValue("@IdRangoHorario", IdRangoHorario);
                    cmd.Parameters.AddWithValue("@FechaTurno", FechaTurno);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new RangoHorario
                            {
                                UnUsuario = new Usuario
                                {
                                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                    NombreYApellido = dr["NombreYApellido"].ToString()
                                },
                                CupoMaximo = Convert.ToInt32(dr["CupoMaximo"]),
                                CupoActual = Convert.ToInt32(dr["CupoActual"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en ListarEntrenadoresDisponibles: {ex.Message}");
                    lista.Clear(); // Asegura que la lista no tenga valores inconsistentes en caso de error.
                }
            }
            return lista;
        }

        /*
        // No anda con query:
        public List<RangoHorario> ListarEntrenadoresDisponibles(int IdRangoHorario, DateTime FechaTurno)
        {
            List<RangoHorario> lista = new List<RangoHorario>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT DISTINCT rh_u.IdUsuario, u.NombreYApellido, rh.CupoMaximo, 
                               COALESCE(cf.CupoActual, 0) AS CupoActual -- Si no hay registros en CupoFecha, se considera como 0
                        FROM RangoHorario rh
                        INNER JOIN RangoHorario_Usuario rh_u ON rh.IdRangoHorario = rh_u.IdRangoHorario
                        INNER JOIN Usuario u ON rh_u.IdUsuario = u.IdUsuario
                        LEFT JOIN CupoFecha cf ON cf.IdRangoHorario = rh.IdRangoHorario 
                            AND cf.Fecha = @FechaTurno -- Se mueve aquí para permitir NULLs en el WHERE
                        WHERE (COALESCE(cf.CupoActual, 0) < rh.CupoMaximo) -- Si cf.CupoActual es NULL, se trata como 0
                        AND rh.IdRangoHorario = @IdRangoHorario;
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IdRangoHorario", IdRangoHorario); // 🔹 Se pasa el parámetro correctamente
                    cmd.Parameters.AddWithValue("@FechaTurno", FechaTurno); // 🔹 Se pasa el parámetro correctamente

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new RangoHorario
                            {
                                UnUsuario = new Usuario
                                {
                                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                    NombreYApellido = dr["NombreYApellido"].ToString()
                                },
                                CupoMaximo = Convert.ToInt32(dr["CupoMaximo"]),
                                CupoActual = Convert.ToInt32(dr["CupoActual"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en ListarEntrenadoresDisponibles: {ex.Message}");
                    lista.Clear(); // Asegura que la lista no tenga valores inconsistentes en caso de error.
                }
            }
            return lista;
        }
        */

        public bool Registrar(int IdRangoHorario, int IdUsuario, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_RANGOHORARIO_USUARIO", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdRangoHorario", IdRangoHorario);
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                    resultado = false;
                }
            }

            return resultado;
        }
        public bool ActualizarCupo(int idRangoHorario, int nuevoCupo, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("UPDATE RangoHorario SET CupoMaximo = @Cupo WHERE IdRangoHorario = @IdRango", conexion);
                    cmd.Parameters.AddWithValue("@Cupo", nuevoCupo);
                    cmd.Parameters.AddWithValue("@IdRango", idRangoHorario);

                    conexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                    resultado = false;
                }
            }
            return resultado;
        }

        public bool EliminarRelacion(int idRangoHorario, int idUsuario, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM RangoHorario_Usuario WHERE IdRangoHorario = @IdRangoHorario AND IdUsuario = @IdUsuario", conexion);
                    cmd.Parameters.AddWithValue("@IdRangoHorario", idRangoHorario);
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    conexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                    resultado = false;
                }
            }
            return resultado;
        }
    }
}
