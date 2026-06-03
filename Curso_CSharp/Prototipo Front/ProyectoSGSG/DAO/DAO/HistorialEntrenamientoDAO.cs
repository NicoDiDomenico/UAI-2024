using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class HistorialEntrenamientoDAO
    {
        public List<Entrenamiento> ListarEntrenamientoPorHistorialRutina(int idRutina)
        {
            List<Entrenamiento> lista = new List<Entrenamiento>();

            using (SqlConnection conn = new SqlConnection(Conexion.cadena))
            {
                string query = @"
                    SELECT he.IdHistorialEntrenamiento, he.IdHistorial, he.Series, he.Repeticiones, he.Peso,
                           eg.IdElemento, eg.NombreElemento
                    FROM Historial_Entrenamiento he
                    INNER JOIN ElementoGimnasio eg ON eg.IdElemento = he.IdElementoGimnasio
                    WHERE he.IdHistorial = @IdRutina";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdRutina", idRutina);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Entrenamiento
                    {
                        IdEntrenamiento = Convert.ToInt32(reader["IdHistorialEntrenamiento"]),
                        IdRutina = Convert.ToInt32(reader["IdHistorial"]),
                        Series = Convert.ToInt32(reader["Series"]),
                        Repeticiones = Convert.ToInt32(reader["Repeticiones"]),
                        Peso = Convert.ToInt32(reader["Peso"]),
                        ElementoGimnasio = new ElementoGimnasio
                        {
                            IdElemento = Convert.ToInt32(reader["IdElemento"]),
                            NombreElemento = reader["NombreElemento"].ToString()
                        }
                    });
                }
            }
            return lista;
        }
    }
}
