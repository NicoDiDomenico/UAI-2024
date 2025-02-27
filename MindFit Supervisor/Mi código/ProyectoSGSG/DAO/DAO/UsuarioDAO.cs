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
                        select u.IdUsuario, u.NombreUsuario, NombreYApellido, u.Email, u.Telefono, u.Direccion, u.Ciudad, u.NroDocumento, u.Genero, FechaNacimiento, u.Clave, r.IdRol, r.Descripcion, u.Estado
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
                            
                            // Para el Login 
                            unUsuario.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                            unUsuario.NombreUsuario = dr["NombreUsuario"].ToString();
                            unUsuario.NombreYApellido = dr["NombreYApellido"].ToString();
                            unUsuario.Clave = dr["Clave"].ToString();
                            // Para el ABM
                            // ...

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
        // Falta ABM
    }
}