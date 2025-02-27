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
    }
}
