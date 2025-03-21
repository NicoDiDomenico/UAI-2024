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
    public class RutinaDAO
    {
        public List<Rutina> Listar(int idSocioSeleccionado)
        {
            List<Rutina> lista = new List<Rutina>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        select r.Dia, r.IdRutina 
                        from Rutina r
                        inner join Socio s
                        on r.IdSocio = s.IdSocio
                        where s.IdSocio = @IdSocio
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@idSocio", idSocioSeleccionado);

                        oconexion.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Rutina unaRutina = new Rutina
                                {
                                    Dia = Convert.ToString(dr["Dia"]),
                                    IdRutina = dr["IdRutina"] != DBNull.Value ? Convert.ToInt32(dr["IdRutina"]) : 0,
                                };

                                lista.Add(unaRutina);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Rutina>();
                    Console.WriteLine($"Error al listar rutinas: {ex.Message}");
                }
            }
            return lista;
        }
    }
}
