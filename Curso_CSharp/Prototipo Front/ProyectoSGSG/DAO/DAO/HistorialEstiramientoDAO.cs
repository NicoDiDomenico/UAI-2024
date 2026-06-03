using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class HistorialEstiramientoDAO
    {
        public List<RutinaEstiramiento> ListarEstiramientosPorHistorialRutina(int idRutina)
        {
            List<RutinaEstiramiento> lista = new List<RutinaEstiramiento>();

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cadena))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT IdEstiramiento, Duracion 
                        FROM Historial_Estiramiento 
                        WHERE IdHistorial = @IdRutina", conn);

                    cmd.Parameters.AddWithValue("@IdRutina", idRutina);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new RutinaEstiramiento
                            {
                                IdRutina = idRutina,
                                IdEstiramiento = Convert.ToInt32(reader["IdEstiramiento"]),
                                Minutos = Convert.ToInt32(reader["Duracion"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Podés loguear o retornar una lista vacía, según tu necesidad
                lista = new List<RutinaEstiramiento>();
            }
            return lista;
        }
    }
}
