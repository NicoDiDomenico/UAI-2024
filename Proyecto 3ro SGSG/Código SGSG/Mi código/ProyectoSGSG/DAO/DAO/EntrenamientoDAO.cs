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
    public class EntrenamientoDAO
    {
        public List<Entrenamiento> ListarPorRutina(int idRutina)
        {
            List<Entrenamiento> lista = new List<Entrenamiento>();

            using (SqlConnection conn = new SqlConnection(Conexion.cadena))
            {
                string query = @"
                    SELECT e.IdEntrenamiento, e.IdRutina, e.Series, e.Repeticiones, e.Peso,
                           eg.IdElemento, eg.NombreElemento
                    FROM Entrenamiento e
                    INNER JOIN ElementoGimnasio eg ON eg.IdElemento = e.IdElementoGimnasio
                    WHERE e.IdRutina = @IdRutina";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdRutina", idRutina);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Entrenamiento
                    {
                        IdEntrenamiento = Convert.ToInt32(reader["IdEntrenamiento"]),
                        IdRutina = Convert.ToInt32(reader["IdRutina"]),
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
