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
    public class CD_Rol
    {
        public List<Rol> Listar()
        {
            List<Rol> lista = new List<Rol>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    ////
                    // Forma 1 - Misma logica que CA_Usuario:

                    string query = "SELECT IdRol, Descripcion FROM ROL";

                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read()) // dr.Read() avanza a la siguiente fila de los resultados, al inicio está en una fila anterior a la primera, pero al ejecutarse por 1ra vez en el while pasar a posicionarse en la primera fila.
                        {
                            lista.Add(new Rol()
                            {
                                IdRol = Convert.ToInt32(dr["idRol"]),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Rol>();
                }
            }
            return lista;
        }
    }   
}
