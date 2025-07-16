using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DAO
{
    public class AccionDAO
    {
        public List<Accion> ListarTodo()
        {
            List<Accion> lista = new List<Accion>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        select IdAccion, NombreAccion, Descripcion, IdGrupo
                        from Accion
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Accion Accion = new Accion
                            {
                                IdAccion = Convert.ToInt32(dr["IdAccion"]),
                                NombreAccion = dr["NombreAccion"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                IdGrupo = Convert.ToInt32(dr["IdGrupo"])
                            };

                            lista.Add(Accion);
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Accion>();
                }
            }
            return lista;
        }
        
        public List<Accion> ListarAccionesConGrupo()
        {
            List<Accion> lista = new List<Accion>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        select a.IdAccion, a.NombreAccion, a.Descripcion, g.IdGrupo, g.NombreMenu, g.Descripcion AS DescGrupo
                        from Accion a
                        inner join Grupo g
                        on g.IdGrupo = a.IdGrupo
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Accion Accion = new Accion
                            {
                                IdAccion = Convert.ToInt32(dr["IdAccion"]),
                                NombreAccion = dr["NombreAccion"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                IdGrupo = Convert.ToInt32(dr["IdGrupo"]),
                                unGrupo = new Grupo
                                {
                                    IdGrupo = Convert.ToInt32(dr["IdGrupo"]),
                                    NombreMenu = dr["NombreMenu"].ToString(),
                                    Descripcion = dr["DescGrupo"].ToString()
                                }
                            };

                            lista.Add(Accion);
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Accion>();
                }
            }
            return lista;
        }

        public List<Accion> Listar(int idusuario)
        {
            List<Accion> lista = new List<Accion>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT 
                            COALESCE(ac.NombreAccion, a.NombreAccion) AS NombreAccion
                        FROM PERMISO p
                        LEFT JOIN ROL r ON r.IdRol = p.IdRol 
                        LEFT JOIN USUARIO u ON u.IdRol = r.IdRol OR u.IdUsuario = p.IdUsuario 
                        LEFT JOIN GRUPO g ON p.IdGrupo = g.IdGrupo -- Puede ser NULL si es acción directa
                        LEFT JOIN ACCION a ON p.IdAccion = a.IdAccion -- Acciones individuales
                        LEFT JOIN ACCION ac ON g.IdGrupo = ac.IdGrupo -- Acciones que provienen de grupos
                        LEFT JOIN ( -- Subconsulta para asignar el NombreMenu a acciones individuales
                            SELECT a.IdAccion, g.NombreMenu 
                            FROM ACCION a
                            LEFT JOIN GRUPO g ON a.IdGrupo = g.IdGrupo
                        ) am ON a.IdAccion = am.IdAccion
                        WHERE u.IdUsuario = @idusuario
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@idusuario", idusuario);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Accion Accion = new Accion
                            {
                                NombreAccion = dr["NombreAccion"].ToString()
                            };

                            lista.Add(Accion);
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Accion>();
                }
            }
            return lista;
        }

        public bool Modificar(Accion unaAccion, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_MODIFICARACCION", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    cmd.Parameters.AddWithValue("@IdAccion", unaAccion.IdAccion);
                    cmd.Parameters.AddWithValue("@NombreAccion", unaAccion.NombreAccion);
                    cmd.Parameters.AddWithValue("@Descripcion", unaAccion.Descripcion);
                    cmd.Parameters.AddWithValue("@IdGrupo", unaAccion.IdGrupo);

                    // Parámetros de salida
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    // Obtener resultado del SP
                    resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }

            return resultado;
        }
    }
}
