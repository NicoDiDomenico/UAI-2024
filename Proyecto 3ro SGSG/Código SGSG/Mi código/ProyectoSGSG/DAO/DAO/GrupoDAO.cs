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
    public class GrupoDAO
    {
        public List<Grupo> Listar()
        {
            List<Grupo> lista = new List<Grupo>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        select IdGrupo, NombreMenu, Descripcion
                        from Grupo
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Grupo()
                            {
                                IdGrupo = Convert.ToInt32(dr["IdGrupo"]),
                                NombreMenu = dr["NombreMenu"].ToString(),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Grupo>();
                }
            }
            return lista;
        }
    }
}
