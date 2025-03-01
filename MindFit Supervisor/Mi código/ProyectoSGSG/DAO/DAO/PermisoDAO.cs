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
            // Se crea una lista vacía para almacenar los usuarios que se obtendrán de la base de datos
            List<Permiso> lista = new List<Permiso>();

            // Se establece la conexión a la base de datos utilizando la cadena de conexión definida en 'Conexion.cadena'
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT p.IdRol, p.NombreMenu 
                        FROM PERMISO p
                        INNER JOIN ROL r ON r.IdRol = p.IdRol
                        INNER JOIN USUARIO u ON u.IdRol = r.IdRol
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
                            lista.Add(new Permiso()
                            {
                                Rol = new Rol { IdRol = Convert.ToInt32(dr["IdRol"]) },
                                NombreMenu = dr["NombreMenu"].ToString()
                            });
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
        public List<Permiso> ListarTodos()
        {
            List<Permiso> lista = new List<Permiso>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        select NombreMenu, Descripcion
                        from Permiso
                        group by NombreMenu, Descripcion
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Permiso()
                            {
                                NombreMenu = dr["NombreMenu"].ToString(),
                                Descripcion = dr["Descripcion"].ToString()
                            });
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
            // Se crea una lista para almacenar los detalles de la compra
            List<Permiso> oLista = new List<Permiso>();

            try
            {
                // Se establece la conexión con la base de datos
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open(); // Se abre la conexión

                    string query = @"
                        SELECT p.NombreMenu, p.Descripcion from Permiso p
                        inner join Rol r
                        on p.IdRol = r.IdRol
                        Where p.IdRol = @IdRol
                    ";

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);

                    cmd.Parameters.AddWithValue("@IdRol", IdRol);

                    cmd.CommandType = CommandType.Text;

                    // Se ejecuta la consulta y se obtiene el resultado
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Se recorren los registros obtenidos
                        while (dr.Read())
                        {
                            // Se agrega cada detalle de compra a la lista
                            oLista.Add(new Permiso()
                            {
                                NombreMenu = dr["NombreMenu"].ToString(),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oLista = new List<Permiso>();
            }

            // Se retorna la lista con los detalles de la compra
            return oLista;
        }
    }
}
