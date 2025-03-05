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
    public class UsuarioDAO
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        select u.IdUsuario, NombreYApellido, u.Email, u.Telefono, u.Direccion, u.Ciudad, u.NroDocumento, u.Genero, FechaNacimiento, u.NombreUsuario, u.Clave, r.IdRol, r.Descripcion, u.Estado, u.FechaRegistro
                        from Usuario u
                        inner join Rol r
                        on r.IdRol = u.IdRol
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Usuario unUsuario = new Usuario();
                            
                            unUsuario.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                            unUsuario.NombreYApellido = dr["NombreYApellido"].ToString();
                            unUsuario.Email = dr["Email"].ToString();
                            unUsuario.Telefono = dr["Telefono"].ToString();
                            unUsuario.Direccion = dr["Direccion"].ToString();
                            unUsuario.Ciudad = dr["Ciudad"].ToString();
                            unUsuario.NroDocumento = Convert.ToInt32(dr["NroDocumento"]);
                            unUsuario.Genero = dr["Genero"].ToString();
                            unUsuario.FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]);
                            unUsuario.NombreUsuario = dr["NombreUsuario"].ToString();
                            unUsuario.Clave = dr["Clave"].ToString();
                            unUsuario.Rol = new Rol { IdRol = Convert.ToInt32(dr["IdRol"]), Descripcion = dr["Descripcion"].ToString() };
                            unUsuario.Estado = Convert.ToBoolean(dr["Estado"]);
                            unUsuario.FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);

                            lista.Add(unUsuario);
                        }
                    }
                }
                catch (Exception)
                {
                    lista = new List<Usuario>();
                }
            }
            return lista;
        }
        public int Registrar(Usuario obj, out string Mensaje)
        {
            int idusuariogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARUSUARIO", oconexion);

                    // Parámetros de entrada
                    cmd.Parameters.AddWithValue("@NombreUsuario", obj.NombreUsuario);
                    cmd.Parameters.AddWithValue("@NombreYApellido", obj.NombreYApellido);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("@Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("@Ciudad", obj.Ciudad);
                    cmd.Parameters.AddWithValue("@NroDocumento", obj.NroDocumento);
                    cmd.Parameters.AddWithValue("@Genero", obj.Genero);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", obj.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("@IdRol", obj.Rol.IdRol);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);

                    cmd.Parameters.Add("@IdUsuarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    // Se obtienen los valores de los parámetros de salida después de la ejecución del procedimiento
                    idusuariogenerado = Convert.ToInt32(cmd.Parameters["@IdUsuarioResultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
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

                    cmd.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("@NombreUsuario", obj.NombreUsuario);
                    cmd.Parameters.AddWithValue("@NombreYApellido", obj.NombreYApellido);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("@Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("@Ciudad", obj.Ciudad);
                    cmd.Parameters.AddWithValue("@NroDocumento", obj.NroDocumento);
                    cmd.Parameters.AddWithValue("@Genero", obj.Genero);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", obj.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("@IdRol", obj.Rol.IdRol);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);

                    cmd.Parameters.Add("@Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["@Respuesta"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
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
                    cmd.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                    cmd.Parameters.Add("@Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["@Respuesta"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
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