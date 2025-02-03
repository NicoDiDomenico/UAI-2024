using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_PERMISO
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
                    ////
                    // Forma 1 - Misma logica que CA_Usuario:
                    
                    string query = @"
                        SELECT p.IdRol, p.NombreMenu 
                        FROM PERMISO p
                        INNER JOIN ROL r ON r.IdRol = p.IdRol
                        INNER JOIN USUARIO u ON u.IdRol = r.IdRol
                        WHERE u.IdUsuario = @idusuario
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    
                    /*////
                    // Forma 2 -- "Mas Eficiente":
                    // Se crea un StringBuilder para construir la consulta SQL de manera eficiente
                    StringBuilder query = new StringBuilder();

                    // Se añade la primera línea de la consulta que selecciona las columnas IdRol y NombreMenu de la tabla PERMISO
                    query.AppendLine("select p.IdRol, p.NombreMenu from PERMISO p");

                    // Se realiza un INNER JOIN entre la tabla PERMISO y la tabla ROL, usando el campo IdRol como clave de unión
                    query.AppendLine("inner join ROL r on r.IdRol = p.IdRol");

                    // Se realiza otro INNER JOIN entre la tabla USUARIO y la tabla ROL, también usando el campo IdRol
                    query.AppendLine("inner join USUARIO u on u.IdRol = r.IdRol");

                    // Se añade una condición WHERE para filtrar los resultados por el IdUsuario que se pasará como parámetro
                    query.AppendLine("where u.IdUsuario = @idusuario");

                    // Se crea un comando SQL utilizando la consulta construida y la conexión a la base de datos
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    ////*/
                    
                    // Se añade el parámetro @idusuario al comando, que se reemplazará con el valor de la variable 'id'
                    cmd.Parameters.AddWithValue("@idusuario", idUsuario);

                    // Se especifica que el tipo de comando es texto (consulta SQL directa)
                    cmd.CommandType = CommandType.Text;

                    // Se abre la conexión a la base de datos para ejecutar el comando
                    oconexion.Open();


                    // Se ejecuta la consulta y se obtiene un SqlDataReader para leer los datos fila por fila
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Mientras haya filas para leer
                        while (dr.Read()) // dr.Read() avanza a la siguiente fila de los resultados, al inicio está en una fila anterior a la primera, pero al ejecutarse por 1ra vez en el while pasar a posicionarse en la primera fila.
                        {
                            // Se añade un nuevo objeto Usuario a la lista, mapeando los campos de la base de datos a las propiedades del objeto
                            lista.Add(new Permiso()
                            {
                                oRol = new Rol { IdRol = Convert.ToInt32(dr["idRol"]) },
                                NombreMenu = dr["NombreMenu"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // En caso de producirse una excepción (error), se devuelve una lista vacía
                    // Aquí podrías registrar el error (log) usando ex.Message para saber qué pasó
                    lista = new List<Permiso>();
                    //throw new Exception($"Error al listar usuarios: {ex.Message}");
                }
            }

            // Se devuelve la lista de usuarios obtenida de la base de datos
            return lista;
        }
    }
}
