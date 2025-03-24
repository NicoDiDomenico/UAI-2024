using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAO
{
    public class MaquinaDAO
    {
        public List<Maquina> Listar()
        {
            List<Maquina> lista = new List<Maquina>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT 
                            eg.IdElemento,
                            eg.NombreElemento,
                            m.FechaFabricacion,
                            m.FechaCompra,
                            m.Precio,
                            m.Peso,
                            m.EsElectrica
                        FROM Maquina m
                        INNER JOIN ElementoGimnasio eg ON m.IdElemento = eg.IdElemento
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Maquina unaMaquina = new Maquina
                            {
                                IdElemento = Convert.ToInt32(dr["IdElemento"]),
                                NombreElemento = dr["NombreElemento"].ToString(),
                                FechaFabricacion = Convert.ToDateTime(dr["FechaFabricacion"]),
                                FechaCompra = Convert.ToDateTime(dr["FechaCompra"]),
                                Precio = Convert.ToSingle(dr["Precio"]),
                                Peso = Convert.ToInt32(dr["Peso"]),
                                TipoMaquina = dr["TipoMaquina"].ToString(),
                                EsElectrica = Convert.ToBoolean(dr["EsElectrica"])
                            };

                            lista.Add(unaMaquina);
                        }
                    }
                }
                catch (Exception mensaje)
                {
                    lista = new List<Maquina>(); // Devuelve lista vacía en caso de error
                    //MessageBox.Show(mensaje.Message);
                }
            }

            return lista;
        }
    }
}
