using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class HistorialCalentamientoDAO
    {
        public List<RutinaCalentamiento> ListarCalentamientosPorHistorialRutina(int idRutina)
        {
            List<RutinaCalentamiento> lista = new List<RutinaCalentamiento>();

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cadena))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT IdCalentamiento, Duracion 
                        FROM Historial_Calentamiento 
                        WHERE IdHistorial = @IdRutina", conn);

                    cmd.Parameters.AddWithValue("@IdRutina", idRutina);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new RutinaCalentamiento
                            {
                                IdRutina = idRutina,
                                IdCalentamiento = Convert.ToInt32(reader["IdCalentamiento"]),
                                Minutos = Convert.ToInt32(reader["Duracion"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Podés loguear o retornar una lista vacía, según tu necesidad
                lista = new List<RutinaCalentamiento>();
            }

            return lista;
        }
    }
}
