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
    public class ElementoGimnasioDAO
    {
        public List<ElementoGimnasio> Listar()
        {
            List<ElementoGimnasio> lista = new List<ElementoGimnasio>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        select IdElemento, NombreElemento
                        from ElementoGimnasio
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ElementoGimnasio unElementoGimnasio = new ElementoGimnasio();

                            unElementoGimnasio.IdElemento = Convert.ToInt32(dr["IdElemento"]);
                            unElementoGimnasio.NombreElemento = dr["NombreElemento"].ToString();

                            lista.Add(unElementoGimnasio);
                        }

                    }
                }
                catch (Exception)
                {
                    lista = new List<ElementoGimnasio>();
                }
            }
            return lista;
        }
    }
}
