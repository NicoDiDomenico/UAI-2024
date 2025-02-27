using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Negocio
    {
        public Negocio ObtenerDatos()
        {
            Negocio obj = new Negocio();

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    string query = "select IdNegocio, Nombre, RUC, Direccion from NEGOCIO where IdNegocio = '1'";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new Negocio()
                            {
                                IdNegocio = int.Parse(dr["IdNegocio"].ToString()),
                                Nombre = dr["Nombre"].ToString(),
                                RUC = dr["RUC"].ToString(),
                                Direccion = dr["Direccion"].ToString()
                            };
                        }
                    }
                }
            }
            catch
            {
                obj = new Negocio();
            }

            return obj;
        }

        public bool GuardarDatos(Negocio objeto, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update NEGOCIO set Nombre = @nombre,");
                    query.AppendLine("RUC = @ruc,");
                    query.AppendLine("Direccion = @direccion");
                    query.AppendLine("where IdNegocio = 1;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("@ruc", objeto.RUC);
                    cmd.Parameters.AddWithValue("@direccion", objeto.Direccion);
                    cmd.CommandType = CommandType.Text;

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudo guardar los datos";
                        respuesta = false;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                respuesta = false;
            }

            return respuesta;
        }

        public byte[] ObtenerLogo(out bool obtenido)
        {
            obtenido = true;  // Se asume inicialmente que la obtención del logo será exitosa
            byte[] LogoBytes = new byte[0];  // Se inicializa un array de bytes vacío

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena)) // Se establece la conexión con la base de datos
                {
                    conexion.Open();  // Se abre la conexión

                    // Consulta SQL para obtener el logo del negocio con IdNegocio = 1
                    string query = "select Logo from NEGOCIO where IdNegocio = 1";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = CommandType.Text;

                    // Se ejecuta la consulta y se obtiene un lector de datos
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())  // Se recorre el resultado de la consulta
                        {
                            LogoBytes = (byte[])dr["Logo"];  // Se asigna el valor del campo 'Logo' al array de bytes
                        }
                    }
                }
            }
            catch (Exception ex)  // Se captura cualquier error que ocurra durante la ejecución
            {
                obtenido = false;  // Se indica que hubo un error al obtener el logo
                LogoBytes = new byte[0];  // Se retorna un array vacío en caso de error
            }

            return LogoBytes;  // Se retorna el logo en formato de bytes
        }

        public bool ActualizarLogo(byte[] image, out string mensaje)
        {
            mensaje = string.Empty;  // Se inicializa el mensaje como vacío
            bool respuesta = true;  // Se asume que la actualización será exitosa

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena)) // Se establece la conexión con la base de datos
                {
                    conexion.Open();  // Se abre la conexión con la base de datos

                    // Se construye la consulta SQL para actualizar el logo
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update NEGOCIO set Logo = @imagen");
                    query.AppendLine("where IdNegocio = 1;");

                    // Se crea el comando SQL y se asocia a la conexión
                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@imagen", image); // Se agrega el parámetro con la imagen en formato byte[]
                    cmd.CommandType = CommandType.Text;

                    // Se ejecuta la consulta y se verifica si se realizó la actualización
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudo actualizar el logo";  // Mensaje de error si no se realizó ninguna actualización
                        respuesta = false;  // Se indica que la operación no fue exitosa
                    }
                }
            }
            catch (Exception ex)  // Se captura cualquier error ocurrido en la ejecución
            {
                mensaje = ex.Message;  // Se guarda el mensaje del error en la variable de salida
                respuesta = false;  // Se indica que la operación falló
            }

            return respuesta;  // Se retorna el resultado de la operación
        }
    }
}
