using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using CapaEntidad; // Si no agregaba una Referencia a la capa entidad no iba a poder importar sus clases

namespace CapaDatos
{
    // Definición de la clase que pertenece a la Capa de Datos (CD)
    public class CD_Usuario
    {

        // Método público que devuelve una lista de objetos Usuario
        public List<Usuario> Listar()
        {
            // Se crea una lista vacía para almacenar los usuarios que se obtendrán de la base de datos
            List<Usuario> lista = new List<Usuario>();

            // Se establece la conexión a la base de datos utilizando la cadena de conexión definida en 'Conexion.cadena'
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Consulta SQL para obtener los datos de la tabla 'usuario'
                    // string query = "select IdUsuario, Documento, NombreCompleto, Correo, Clave, Estado from usuario";
                    string query = @"
                        select u.IdUsuario, u.Documento, u.NombreCompleto, u.Correo, u.Clave, u.Estado, r.IdRol, r.Descripcion
                        from usuario u
                        inner join ROL r
                        on r.IdRol = u.IdRol
                    ";

                    // Se crea un comando SQL con la consulta y la conexión
                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    // Se especifica que el comando es de tipo texto (consulta SQL directa)
                    cmd.CommandType = CommandType.Text;

                    // Se abre la conexión con la base de datos
                    oconexion.Open();

                    // Se ejecuta la consulta y se obtiene un SqlDataReader para leer los datos fila por fila
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Mientras haya filas para leer
                        while (dr.Read()) // dr.Read() avanza a la siguiente fila de los resultados, al inicio está en una fila anterior a la primera, pero al ejecutarse por 1ra vez en el while pasar a posicionarse en la primera fila.
                        {
                            // Se añade un nuevo objeto Usuario a la lista, mapeando los campos de la base de datos a las propiedades del objeto
                            lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),              // Convierte el valor de 'IdUsuario' a entero
                                Documento = dr["Documento"].ToString(),                   // Convierte el valor de 'Documento' a string
                                NombreCompleto = dr["NombreCompleto"].ToString(),         // Convierte 'NombreCompleto' a string
                                Correo = dr["Correo"].ToString(),                         // Convierte 'Correo' a string
                                Clave = dr["Clave"].ToString(),                           // Convierte 'Clave' a string
                                oRol = new Rol { IdRol = Convert.ToInt32(dr["IdRol"]), Descripcion = dr["Descripcion"].ToString() }, // No lo necesito
                                Estado = Convert.ToBoolean(dr["Estado"])                  // Convierte el valor de 'Estado' a booleano (true/false)
                                // no traigo la FechaRegistro
                            });
                            /*
                            // Mi forma:
                            Usuario unUsuario = new Usuario();

                            unUsuario.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                            unUsuario.Documento = dr["Documento"].ToString();                
                            unUsuario.NombreCompleto = dr["NombreCompleto"].ToString();
                            unUsuario.Correo = dr["Correo"].ToString();
                            unUsuario.Clave = dr["Clave"].ToString();
                            unUsuario.Estado = Convert.ToBoolean(dr["Estado"]);
                            
                            lista.Add(unUsuario);
                            */
                        }
                    }
                }
                catch (Exception ex)
                {
                    // En caso de producirse una excepción (error), se devuelve una lista vacía
                    // Aquí podrías registrar el error (log) usando ex.Message para saber qué pasó
                    lista = new List<Usuario>();
                    //throw new Exception($"Error al listar usuarios: {ex.Message}");
                }
            }

            // Se devuelve la lista de usuarios obtenida de la base de datos
            return lista;
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            int idusuariogenerado = 0; // Variable para almacenar el ID del usuario generado por la BD
            Mensaje = string.Empty; // Inicializa la variable de mensaje como vacía

            try
            {
                // Se abre una conexión a la base de datos utilizando la cadena de conexión definida
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    // Se crea un objeto SqlCommand para ejecutar el procedimiento almacenado "SP_REGISTRARUSUARIO"
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARUSUARIO", oconexion);

                    // Se agregan parámetros al comando SQL con los valores del objeto 'Usuario'
                    cmd.Parameters.AddWithValue("Documento", obj.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("IdRol", obj.oRol.IdRol);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);

                    // Se definen parámetros de salida que serán retornados por el procedimiento almacenado
                    cmd.Parameters.Add("IdUsuarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    // Se indica que el tipo de comando es un procedimiento almacenado
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Se abre la conexión con la base de datos
                    oconexion.Open();

                    // Se ejecuta el procedimiento almacenado
                    cmd.ExecuteNonQuery();

                    // Se obtienen los valores de los parámetros de salida después de la ejecución del procedimiento
                    idusuariogenerado = Convert.ToInt32(cmd.Parameters["IdUsuarioResultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                // Si hay un error, se captura la excepción y se devuelve un ID de usuario 0 con el mensaje de error
                idusuariogenerado = 0;
                Mensaje = ex.Message;
            }

            return idusuariogenerado; // Retorna el ID del usuario generado o 0 si hubo error
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARUSUARIO", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("Documento", obj.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("IdRol", obj.oRol.IdRol);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;
        }

        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARUSUARIO", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;
        }

    }
}
