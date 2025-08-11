﻿using Modelo;
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
                        SELECT u.IdUsuario, u.NombreYApellido, u.Email, u.Telefono, 
                               u.Direccion, u.Ciudad, u.NroDocumento, u.Genero, 
                               u.FechaNacimiento, u.NombreUsuario, u.Clave, 
                               u.IdRol, r.Descripcion AS RolDescripcion, 
                               u.Estado, u.FechaRegistro
                        FROM Usuario u
                        LEFT JOIN Rol r ON r.IdRol = u.IdRol
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Usuario unUsuario = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                NombreYApellido = dr["NombreYApellido"].ToString(),
                                Email = dr["Email"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Ciudad = dr["Ciudad"].ToString(),
                                NroDocumento = Convert.ToInt32(dr["NroDocumento"]),
                                Genero = dr["Genero"].ToString(),
                                FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                                NombreUsuario = dr["NombreUsuario"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])
                            };

                            // Si IdRol es NULL, no asignamos un rol
                            if (dr["IdRol"] != DBNull.Value)
                            {
                                unUsuario.Rol = new Rol
                                {
                                    IdRol = Convert.ToInt32(dr["IdRol"]),
                                    Descripcion = dr["RolDescripcion"].ToString()
                                };
                            }
                            else
                            {
                                unUsuario.Rol = null; // El usuario no tiene rol, pero puede tener permisos por acciones
                            }

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

        public List<Usuario> ListarPorCorreo(string correo)
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT IdUsuario, NombreYApellido, Email, Telefono, 
                               Direccion, Ciudad, NroDocumento, Genero, 
                               FechaNacimiento, NombreUsuario, Clave, 
                               Estado, FechaRegistro
                        FROM Usuario
                        WHERE Email = @correo";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@correo", correo);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Usuario unUsuario = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                NombreYApellido = dr["NombreYApellido"].ToString(),
                                Email = dr["Email"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Ciudad = dr["Ciudad"].ToString(),
                                NroDocumento = Convert.ToInt32(dr["NroDocumento"]),
                                Genero = dr["Genero"].ToString(),
                                FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                                NombreUsuario = dr["NombreUsuario"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])
                            };

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

        public bool CambiarClave(int idUsuario, string nuevaClave)
        {
            bool resultado = false;

            using (SqlConnection con = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = "UPDATE Usuario SET Clave = @clave WHERE IdUsuario = @idUsuario";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@clave", nuevaClave);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    con.Open();
                    resultado = cmd.ExecuteNonQuery() > 0;
                }
                catch
                {
                    resultado = false;
                }
            }

            return resultado;
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

                    // Si tiene un rol, se asigna el valor. Si no, se pasa NULL
                    cmd.Parameters.AddWithValue("@IdRol", obj.Rol != null ? (object)obj.Rol.IdRol : DBNull.Value);

                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                    cmd.Parameters.Add("@IdUsuarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    // Obtener el ID del usuario generado
                    idusuariogenerado = Convert.ToInt32(cmd.Parameters["@IdUsuarioResultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }

                // **Registrar acciones en Permiso si existen acciones seleccionadas**
                if (idusuariogenerado > 0 && obj.Acciones != null && obj.Acciones.Count > 0)
                {
                    using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                    {
                        oconexion.Open();

                        foreach (var accion in obj.Acciones)
                        {
                            // **Buscar el IdAccion correspondiente al NombreAccion**
                            int idAccion = 0;
                            using (SqlCommand cmdAccion = new SqlCommand("SELECT IdAccion FROM Accion WHERE NombreAccion = @NombreAccion", oconexion))
                            {
                                cmdAccion.Parameters.AddWithValue("@NombreAccion", accion.NombreAccion);
                                var resultado = cmdAccion.ExecuteScalar();
                                if (resultado != null)
                                {
                                    idAccion = Convert.ToInt32(resultado);
                                }
                            }

                            // **Si encontró un IdAccion válido, insertar en Permiso**
                            if (idAccion > 0)
                            {
                                using (SqlCommand cmdInsertPermiso = new SqlCommand("INSERT INTO Permiso (IdUsuario, IdAccion) VALUES (@IdUsuario, @IdAccion)", oconexion))
                                {
                                    cmdInsertPermiso.Parameters.AddWithValue("@IdUsuario", idusuariogenerado);
                                    cmdInsertPermiso.Parameters.AddWithValue("@IdAccion", idAccion);
                                    cmdInsertPermiso.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                idusuariogenerado = 0;
                Mensaje = ex.Message;
            }

            return idusuariogenerado;
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

                    // Error - IdRol puede ser null y no lo estoy permitiendo
                    // cmd.Parameters.AddWithValue("@IdRol", obj.Rol.IdRol);
                    // Forma correcta, mas entendible que registrar:
                    if (obj.Rol != null)
                        cmd.Parameters.AddWithValue("@IdRol", obj.Rol.IdRol);
                    else
                        cmd.Parameters.AddWithValue("@IdRol", DBNull.Value);

                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);

                    cmd.Parameters.Add("@Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["@Respuesta"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }

                // Siempre eliminar permisos anteriores al editar
                if (respuesta)
                {
                    using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                    {
                        oconexion.Open();

                        using (SqlCommand cmdDelete = new SqlCommand("DELETE FROM Permiso WHERE IdUsuario = @IdUsuario", oconexion))
                        {
                            cmdDelete.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                            cmdDelete.ExecuteNonQuery();
                        }

                        // Si tiene acciones, volver a insertar
                        if (obj.Acciones != null && obj.Acciones.Count > 0)
                        {
                            foreach (var accion in obj.Acciones)
                            {
                                int idAccion = 0;

                                using (SqlCommand cmdAccion = new SqlCommand("SELECT IdAccion FROM Accion WHERE NombreAccion = @NombreAccion", oconexion))
                                {
                                    cmdAccion.Parameters.AddWithValue("@NombreAccion", accion.NombreAccion);
                                    var resultado = cmdAccion.ExecuteScalar();
                                    if (resultado != null)
                                        idAccion = Convert.ToInt32(resultado);
                                }

                                if (idAccion > 0)
                                {
                                    using (SqlCommand cmdInsertPermiso = new SqlCommand("INSERT INTO Permiso (IdUsuario, IdAccion) VALUES (@IdUsuario, @IdAccion)", oconexion))
                                    {
                                        cmdInsertPermiso.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                                        cmdInsertPermiso.Parameters.AddWithValue("@IdAccion", idAccion);
                                        cmdInsertPermiso.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
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

        public List<Usuario> ObtenerPorRangoHorario(int IdRangoHorario)
        {
            List<Usuario> lista = new List<Usuario>();
            
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        Select u.IdUsuario, u.NombreYApellido
                        from Usuario u
                        inner join RangoHorario_Usuario rh_u
                        on u.IdUsuario = rh_u.IdUsuario
                        where rh_u.IdRangoHorario = @IdRangoHorario
                        AND u.IdRol = 3
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@IdRangoHorario", IdRangoHorario);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Usuario unUsuario = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                NombreYApellido = dr["NombreYApellido"].ToString()
                            };

                            // Despues traeré otra vez todo los usuarios para validar permisos
                            /*
                            // Si IdRol es NULL, no asignamos un rol
                            if (dr["IdRol"] != DBNull.Value)
                            {
                                unUsuario.Rol = new Rol
                                {
                                    IdRol = Convert.ToInt32(dr["IdRol"]),
                                    Descripcion = dr["RolDescripcion"].ToString()
                                };
                            }
                            else
                            {
                                unUsuario.Rol = null; // El usuario no tiene rol, pero puede tener permisos por acciones
                            }
                            */
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

        public List<Usuario> ListarUsuariosPorRangoHorario()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT DISTINCT rh_u.IdUsuario, r.Descripcion AS RolDescripcion, r.IdRol 
                        FROM RangoHorario_Usuario rh_u
                        INNER JOIN Usuario u ON u.IdUsuario = rh_u.IdUsuario
                        INNER JOIN Rol r ON r.IdRol = u.IdRol
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Usuario unUsuario = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Rol = new Rol
                                {
                                    IdRol = Convert.ToInt32(dr["IdRol"]),
                                    Descripcion = dr["RolDescripcion"].ToString()
                                }
                            };

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

        public Usuario getUsuario(int idUsuario)
        {
            Usuario unUsuario = null;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT u.IdUsuario, u.NombreYApellido, u.Email, u.Telefono, 
                               u.Direccion, u.Ciudad, u.NroDocumento, u.Genero, 
                               u.FechaNacimiento, u.NombreUsuario, u.Clave, 
                               u.IdRol, r.Descripcion AS RolDescripcion, 
                               u.Estado, u.FechaRegistro
                        FROM Usuario u
                        LEFT JOIN Rol r ON r.IdRol = u.IdRol
                        WHERE u.IdUsuario = @IdUsuario
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                        oconexion.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                unUsuario = new Usuario
                                {
                                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                    NombreYApellido = dr["NombreYApellido"].ToString(),
                                    Email = dr["Email"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    Direccion = dr["Direccion"].ToString(),
                                    Ciudad = dr["Ciudad"].ToString(),
                                    NroDocumento = Convert.ToInt32(dr["NroDocumento"]),
                                    Genero = dr["Genero"].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                                    NombreUsuario = dr["NombreUsuario"].ToString(),
                                    Clave = dr["Clave"].ToString(),
                                    Estado = Convert.ToBoolean(dr["Estado"]),
                                    FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])
                                };

                                if (dr["IdRol"] != DBNull.Value)
                                {
                                    unUsuario.Rol = new Rol
                                    {
                                        IdRol = Convert.ToInt32(dr["IdRol"]),
                                        Descripcion = dr["RolDescripcion"].ToString()
                                    };
                                }
                                else
                                {
                                    unUsuario.Rol = null;
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    unUsuario = null;
                }
            }

            return unUsuario;
        }

    }
}