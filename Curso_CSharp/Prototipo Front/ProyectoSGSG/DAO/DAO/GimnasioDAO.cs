using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;

namespace DAO
{
    public class GimnasioDAO
    {
        public Gimnasio ObtenerDatos()
        {
            Gimnasio obj = new Gimnasio();

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    string query = @"
                        SELECT IdGimnasio, NombreGimnasio, Direccion, Telefono,
                               Email,
                               HoraAperturaLaV, HoraCierreLaV,
                               HoraAperturaSabado, HoraCierreSabado
                        FROM Gimnasio
                        WHERE IdGimnasio = 1
                    ";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new Gimnasio()
                            {
                                IdGimnasio = Convert.ToInt32(dr["IdGimnasio"]),
                                NombreGimnasio = dr["NombreGimnasio"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Email = dr["Email"].ToString(), // <--- AGREGADO
                                HoraAperturaLaV = dr["HoraAperturaLaV"] != DBNull.Value ? (TimeSpan)dr["HoraAperturaLaV"] : TimeSpan.Zero,
                                HoraCierreLaV = dr["HoraCierreLaV"] != DBNull.Value ? (TimeSpan)dr["HoraCierreLaV"] : TimeSpan.Zero,
                                HoraAperturaSabado = dr["HoraAperturaSabado"] != DBNull.Value ? (TimeSpan)dr["HoraAperturaSabado"] : TimeSpan.Zero,
                                HoraCierreSabado = dr["HoraCierreSabado"] != DBNull.Value ? (TimeSpan)dr["HoraCierreSabado"] : TimeSpan.Zero
                            };
                        }
                    }
                }
            }
            catch
            {
                obj = new Gimnasio(); // Retornar objeto vacío en caso de error
            }

            return obj;
        }

        public bool GuardarDatos(Gimnasio objeto, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("UPDATE Gimnasio SET");
                    query.AppendLine("NombreGimnasio = @nombre,");
                    query.AppendLine("Telefono = @telefono,");
                    query.AppendLine("Direccion = @direccion,");
                    query.AppendLine("Email = @correo,"); // <-- AGREGADO
                    query.AppendLine("HoraAperturaLaV = @horaAperturaLaV,");
                    query.AppendLine("HoraCierreLaV = @horaCierreLaV,");
                    query.AppendLine("HoraAperturaSabado = @horaAperturaSabado,");
                    query.AppendLine("HoraCierreSabado = @horaCierreSabado");
                    query.AppendLine("WHERE IdGimnasio = 1;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@nombre", objeto.NombreGimnasio);
                    cmd.Parameters.AddWithValue("@telefono", objeto.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", objeto.Direccion);
                    cmd.Parameters.AddWithValue("@correo", objeto.Email); // <-- AGREGADO
                    cmd.Parameters.AddWithValue("@horaAperturaLaV", objeto.HoraAperturaLaV);
                    cmd.Parameters.AddWithValue("@horaCierreLaV", objeto.HoraCierreLaV);
                    cmd.Parameters.AddWithValue("@horaAperturaSabado", objeto.HoraAperturaSabado);
                    cmd.Parameters.AddWithValue("@horaCierreSabado", objeto.HoraCierreSabado);
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
                    string query = "select Logo from Gimnasio where IdGimnasio = 1";
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
                    query.AppendLine("update Gimnasio set Logo = @imagen");
                    query.AppendLine("where IdGimnasio = 1;");

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
