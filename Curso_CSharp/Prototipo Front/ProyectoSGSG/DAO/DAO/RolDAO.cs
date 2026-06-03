using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAO
{
    public class RolDAO
    {
        public List<Rol> Listar()
        {
            List<Rol> lista = new List<Rol>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT IdRol, Descripcion FROM Rol
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Rol()
                            {
                                IdRol = Convert.ToInt32(dr["IdRol"]),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Rol>();
                }
            }
            return lista;
        }

        public List<Rol> ListarConAcciones()
        {
            List<Rol> lista = new List<Rol>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT r.IdRol, r.Descripcion, a.NombreAccion
                        FROM Rol r
                        INNER JOIN Permiso p ON p.IdRol = r.IdRol
                        LEFT JOIN Accion a ON a.IdAccion = p.IdAccion
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        Dictionary<int, Rol> rolesMap = new Dictionary<int, Rol>();

                        while (dr.Read())
                        {
                            int idRol = Convert.ToInt32(dr["IdRol"]);
                            string descripcion = dr["Descripcion"].ToString();
                            string nombreAccion = dr["NombreAccion"] != DBNull.Value ? dr["NombreAccion"].ToString() : null;

                            if (!rolesMap.ContainsKey(idRol))
                            {
                                rolesMap[idRol] = new Rol()
                                {
                                    IdRol = idRol,
                                    Descripcion = descripcion,
                                    Acciones = new List<string>()
                                };
                            }

                            if (!string.IsNullOrEmpty(nombreAccion))
                            {
                                rolesMap[idRol].Acciones.Add(nombreAccion);
                            }
                        }

                        lista = rolesMap.Values.ToList();
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Rol>();
                }
            }

            return lista;
        }


        public bool Registrar(string Descripcion, DataTable Permisos, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARROL", conexion);

                    cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
                    cmd.Parameters.AddWithValue("@Permisos", Permisos); // <--
                    // --> cmd.Parameters.Add("@Permisos", SqlDbType.Structured).Value = Permisos;  // Parámetro tipo tabla

                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    Resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    Resultado = false;
                    Mensaje = ex.Message;
                }
            }
            return Resultado;
        }

        public bool Actualizar(Rol unRol, DataTable Permisos, int IdGrupo, String DescripcionGrupo, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_ACTUALIZARROL", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdRol", unRol.IdRol);
                    cmd.Parameters.AddWithValue("@Descripcion", unRol.Descripcion);
                    cmd.Parameters.AddWithValue("@IdGrupo", IdGrupo);
                    cmd.Parameters.AddWithValue("@DescripcionGrupo", DescripcionGrupo);
                    cmd.Parameters.AddWithValue("@Permisos", Permisos); // este es el parámetro nuevo

                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    Resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    Resultado = false;
                    Mensaje = ex.Message;
                }
            }

            return Resultado;
        }

        public bool Eliminar(int IdRol, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARROL", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdRol", IdRol);

                    cmd.Parameters.Add("@Respuesta", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["@Respuesta"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    Respuesta = false;
                    Mensaje = ex.Message;
                }
            }

            return Respuesta;
        }
    }
}
