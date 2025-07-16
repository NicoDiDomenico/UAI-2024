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
    public class PermisoDAO
    {
        public List<Permiso> Listar(int idUsuario)
        {
            List<Permiso> lista = new List<Permiso>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT 
                            p.IdPermiso, 
                            p.IdRol, 
                            COALESCE(g.NombreMenu, am.NombreMenu) AS NombreMenu, 
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
                    cmd.Parameters.AddWithValue("@idusuario", idUsuario);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Permiso permiso = new Permiso
                            {
                                IdPermiso = Convert.ToInt32(dr["IdPermiso"]),
                                Rol = dr["IdRol"] != DBNull.Value ? new Rol { IdRol = Convert.ToInt32(dr["IdRol"]) } : null,
                                Grupo = dr["NombreMenu"] != DBNull.Value ? new Grupo { NombreMenu = dr["NombreMenu"].ToString() } : null,
                                Accion = dr["NombreAccion"] != DBNull.Value ? new Accion { NombreAccion = dr["NombreAccion"].ToString() } : null
                            };

                            lista.Add(permiso);
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Permiso>();
                }
            }
            return lista;
        }

        public List<Permiso> ObtenerPermisosRol(int IdRol)
        {
            List<Permiso> lista = new List<Permiso>();

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("SP_OBTENER_PERMISOS_POR_ROL", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdRol", IdRol);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    List<Permiso> permisosGrupo = new List<Permiso>();
                    List<Permiso> permisosAccion = new List<Permiso>();

                    while (dr.Read())
                    {
                        string tipo = dr["TipoPermiso"].ToString();

                        Permiso p = new Permiso
                        {
                            TipoPermiso = tipo,
                            Grupo = new Grupo
                            {
                                IdGrupo = Convert.ToInt32(dr["IdGrupo"]),
                                NombreMenu = dr["NombreMenu"].ToString(),
                                Descripcion = dr["Descripcion"].ToString()
                            }
                        };

                        if (tipo == "Accion")
                        {
                            p.Accion = new Accion
                            {
                                IdAccion = Convert.ToInt32(dr["IdAccion"]),
                                NombreAccion = dr["NombreAccion"].ToString(),
                                Descripcion = dr["DescAccion"].ToString()
                            };
                            permisosAccion.Add(p);
                        }
                        else if (tipo == "Grupo")
                        {
                            permisosGrupo.Add(p);
                        }
                    }

                    // Agregar todos los permisos de acción
                    lista.AddRange(permisosAccion);

                    // Agregar permisos de grupo solo si no hay acción asociada
                    foreach (var permisoGrupo in permisosGrupo)
                    {
                        bool yaExisteAccion = permisosAccion.Any(pa => pa.Grupo.IdGrupo == permisoGrupo.Grupo.IdGrupo);
                        if (!yaExisteAccion)
                        {
                            lista.Add(permisoGrupo);
                        }
                    }
                }
            }

            return lista;
        }
    }
}
