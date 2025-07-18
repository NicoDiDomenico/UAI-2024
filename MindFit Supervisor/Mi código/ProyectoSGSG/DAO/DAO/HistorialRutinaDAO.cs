using Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class HistorialRutinaDAO
    {
        public List<Rutina> Listar(int IdSocio, string diaActual)
        {
            List<Rutina> rutinas = new List<Rutina>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT IdHistorial, IdSocio, Dia, FechaRegistro
                        FROM HistorialRutina 
                        WHERE IdSocio = @IdSocio AND Dia = @diaActual
                        ORDER BY FechaRegistro DESC
                        OFFSET 1 ROWS
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@IdSocio", IdSocio);
                        cmd.Parameters.AddWithValue("@diaActual", diaActual);

                        oconexion.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Rutina unaRutina = new Rutina
                                {
                                    IdRutina = Convert.ToInt32(dr["IdHistorial"]),
                                    Socio = new Socio
                                    {
                                        IdSocio = Convert.ToInt32(dr["IdSocio"])
                                    },
                                    Dia = dr["Dia"].ToString(),
                                    FechaModificacion = Convert.ToDateTime(dr["FechaRegistro"])
                                };

                                rutinas.Add(unaRutina);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    rutinas = new List<Rutina>();
                }
            }

            return rutinas;
        }

        public int CrearHistorialRutina(int idSocio, string dia, out string mensaje)
        {
            mensaje = "";
            int idHistorial = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        INSERT INTO HistorialRutina (IdSocio, Dia, FechaRegistro)
                        OUTPUT INSERTED.IdHistorial
                        VALUES (@IdSocio, @Dia, GETDATE());";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@IdSocio", idSocio);
                    cmd.Parameters.AddWithValue("@Dia", dia);

                    oconexion.Open();
                    idHistorial = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    mensaje = "Error en la base de datos: " + ex.Message;
                    idHistorial = 0;
                }
            }

            return idHistorial;
        }

        public bool GuardarHistorialCalentamientos(int idHistorial, List<RutinaCalentamiento> lista, out string mensaje)
        {
            mensaje = "";
            bool exito = true;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                oconexion.Open();
                SqlTransaction trans = oconexion.BeginTransaction();

                try
                {
                    foreach (var item in lista)
                    {
                        string query = @"
                        INSERT INTO Historial_Calentamiento (IdHistorial, IdCalentamiento, Duracion)
                        VALUES (@IdHistorial, @IdCalentamiento, @Minutos);";

                        SqlCommand cmd = new SqlCommand(query, oconexion, trans);
                        cmd.Parameters.AddWithValue("@IdHistorial", idHistorial);
                        cmd.Parameters.AddWithValue("@IdCalentamiento", item.IdCalentamiento);
                        cmd.Parameters.AddWithValue("@Minutos", item.Minutos);

                        cmd.ExecuteNonQuery();
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    exito = false;
                    mensaje = "Error en historial calentamientos: " + ex.Message;
                }
            }

            return exito;
        }

        public bool GuardarHistorialEntrenamientos(int idHistorial, List<Entrenamiento> lista, out string mensaje)
        {
            mensaje = "";
            bool exito = true;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                oconexion.Open();
                SqlTransaction trans = oconexion.BeginTransaction();

                try
                {
                    foreach (var item in lista)
                    {
                        string query = @"
                        INSERT INTO Historial_Entrenamiento (IdHistorial, IdElementoGimnasio, Series, Repeticiones, Peso)
                        VALUES (@IdHistorial, @IdElemento, @Series, @Reps, @Peso);";

                        SqlCommand cmd = new SqlCommand(query, oconexion, trans);
                        cmd.Parameters.AddWithValue("@IdHistorial", idHistorial);
                        cmd.Parameters.AddWithValue("@IdElemento", item.ElementoGimnasio.IdElemento);
                        cmd.Parameters.AddWithValue("@Series", item.Series);
                        cmd.Parameters.AddWithValue("@Reps", item.Repeticiones);
                        cmd.Parameters.AddWithValue("@Peso", item.Peso);

                        cmd.ExecuteNonQuery();
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    exito = false;
                    mensaje = "Error en historial entrenamientos: " + ex.Message;
                }
            }

            return exito;
        }

        public bool GuardarHistorialEstiramientos(int idHistorial, List<RutinaEstiramiento> lista, out string mensaje)
        {
            mensaje = "";
            bool exito = true;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                oconexion.Open();
                SqlTransaction trans = oconexion.BeginTransaction();

                try
                {
                    foreach (var item in lista)
                    {
                        string query = @"
                        INSERT INTO Historial_Estiramiento (IdHistorial, IdEstiramiento, Duracion)
                        VALUES (@IdHistorial, @IdEstiramiento, @Minutos);";

                        SqlCommand cmd = new SqlCommand(query, oconexion, trans);
                        cmd.Parameters.AddWithValue("@IdHistorial", idHistorial);
                        cmd.Parameters.AddWithValue("@IdEstiramiento", item.IdEstiramiento);
                        cmd.Parameters.AddWithValue("@Minutos", item.Minutos);

                        cmd.ExecuteNonQuery();
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    exito = false;
                    mensaje = "Error en historial estiramientos: " + ex.Message;
                }
            }

            return exito;
        }

        public Dictionary<string, int> ObtenerCantidadModificacionesPorDia(int idSocio)
        {
            Dictionary<string, int> resultado = new Dictionary<string, int>()
            {
                { "Lunes", 0 },
                { "Martes", 0 },
                { "Miércoles", 0 },
                { "Jueves", 0 },
                { "Viernes", 0 },
                { "Sábado", 0 }
            };

            string query = @"
                SELECT Dia, COUNT(*) as Cantidad
                FROM HistorialRutina
                WHERE IdSocio = @IdSocio
                GROUP BY Dia
            ";

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@IdSocio", idSocio);

                conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string dia = reader["Dia"].ToString();
                    int cantidad = Convert.ToInt32(reader["Cantidad"]);

                    if (resultado.ContainsKey(dia))
                        resultado[dia] = cantidad;
                }
            }

            return resultado;
        }
    }
}
