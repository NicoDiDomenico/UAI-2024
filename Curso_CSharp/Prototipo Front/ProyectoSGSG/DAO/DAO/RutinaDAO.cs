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
                        select r.IdRutina, r.IdSocio, r.FechaModificacion, r.Dia  
                        from Rutina r
                        inner join Socio s
                        on r.IdSocio = s.IdSocio
                        where s.IdSocio = @IdSocio AND Activa = 1;
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
                                    IdRutina = dr["IdRutina"] != DBNull.Value ? Convert.ToInt32(dr["IdRutina"]) : 0,
                                    Socio = new Socio
                                    {
                                        IdSocio = Convert.ToInt32(dr["IdSocio"]) // Aquí se asigna el ID al objeto Socio
                                    },
                                    FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"]),
                                    Dia = Convert.ToString(dr["Dia"])
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

        public bool GuardarCalentamientos(List<RutinaCalentamiento> lista, out string mensaje)
        {
            mensaje = "";
            bool exito = true;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                oconexion.Open();
                SqlTransaction trans = oconexion.BeginTransaction();

                try
                {
                    if (lista.Count > 0)
                    {
                        // Borrar calentamientos anteriores de esa rutina
                        string deleteQuery = "DELETE FROM Rutina_Calentamiento WHERE IdRutina = @IdRutina";
                        SqlCommand deleteCmd = new SqlCommand(deleteQuery, oconexion, trans);
                        deleteCmd.Parameters.AddWithValue("@IdRutina", lista[0].IdRutina);
                        deleteCmd.ExecuteNonQuery();
                    }

                    foreach (var item in lista)
                    {
                        string query = @"
                            INSERT INTO Rutina_Calentamiento (IdRutina, IdCalentamiento, Duracion)
                            VALUES (@IdRutina, @IdCalentamiento, @Minutos);
                        ";

                        SqlCommand cmd = new SqlCommand(query, oconexion, trans);
                        cmd.Parameters.AddWithValue("@IdRutina", item.IdRutina);
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
                    mensaje = "Error en la base de datos: " + ex.Message;
                }
            }
            return exito;
        }

        public bool GuardarEntrenamientos(List<Entrenamiento> lista, out string mensaje)
        {
            mensaje = "";
            bool exito = true;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                oconexion.Open();
                SqlTransaction trans = oconexion.BeginTransaction();

                try
                {
                    if (lista.Count > 0)
                    {
                        // Borrar entrenamientos anteriores de esa rutina
                        string deleteQuery = "DELETE FROM Entrenamiento WHERE IdRutina = @IdRutina";
                        SqlCommand deleteCmd = new SqlCommand(deleteQuery, oconexion, trans);
                        deleteCmd.Parameters.AddWithValue("@IdRutina", lista[0].IdRutina);
                        deleteCmd.ExecuteNonQuery();
                    }

                    foreach (var item in lista)
                    {
                        string query = @"
                            INSERT INTO Entrenamiento (IdRutina, IdElementoGimnasio, Series, Repeticiones, Peso)
                            VALUES (@IdRutina, @IdElementoGimnasio, @Series, @Repeticiones, @Peso);
                        ";

                        SqlCommand cmd = new SqlCommand(query, oconexion, trans);
                        cmd.Parameters.AddWithValue("@IdRutina", item.IdRutina);
                        cmd.Parameters.AddWithValue("@IdElementoGimnasio", item.ElementoGimnasio.IdElemento);
                        cmd.Parameters.AddWithValue("@Series", item.Series);
                        cmd.Parameters.AddWithValue("@Repeticiones", item.Repeticiones);
                        cmd.Parameters.AddWithValue("@Peso", item.Peso);

                        cmd.ExecuteNonQuery();
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    exito = false;
                    mensaje = "Error en la base de datos: " + ex.Message;
                }
            }

            return exito;
        }

        public bool GuardarEstiramientos(List<RutinaEstiramiento> lista, out string mensaje)
        {
            mensaje = "";
            bool exito = true;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                oconexion.Open();
                SqlTransaction trans = oconexion.BeginTransaction();

                try
                {
                    if (lista.Count > 0)
                    {
                        // Borrar estiramientos anteriores de esa rutina
                        string deleteQuery = "DELETE FROM Rutina_Estiramiento WHERE IdRutina = @IdRutina";
                        SqlCommand deleteCmd = new SqlCommand(deleteQuery, oconexion, trans);
                        deleteCmd.Parameters.AddWithValue("@IdRutina", lista[0].IdRutina);
                        deleteCmd.ExecuteNonQuery();
                    }

                    foreach (var item in lista)
                    {
                        string query = @"
                            INSERT INTO Rutina_Estiramiento (IdRutina, IdEstiramiento, Duracion)
                            VALUES (@IdRutina, @IdEstiramiento, @Minutos);
                        ";

                        SqlCommand cmd = new SqlCommand(query, oconexion, trans);
                        cmd.Parameters.AddWithValue("@IdRutina", item.IdRutina);
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
                    mensaje = "Error en la base de datos: " + ex.Message;
                }
            }
            return exito;
        }

        public bool ActualizarFechaModificacion(int idRutina)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.cadena))
            {
                string query = "UPDATE Rutina SET FechaModificacion = @Fecha WHERE IdRutina = @IdRutina";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Fecha", DateTime.Now);
                cmd.Parameters.AddWithValue("@IdRutina", idRutina);

                conn.Open();
                int filasAfectadas = cmd.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }

        public bool DesactivarRutina(int idRutina)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cadena))
                {
                    conn.Open();

                    string query = "UPDATE Rutina SET Activa = 0, FechaModificacion = GETDATE() WHERE IdRutina = @IdRutina";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdRutina", idRutina);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    resultado = rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al desactivar rutina: " + ex.Message);
                resultado = false;
            }
            return resultado;
        }

        public bool TieneRutinaActivaEnDia(int idSocio, string dia)
        {
            bool resultado = false;

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                string query = "SELECT COUNT(*) FROM Rutina WHERE IdSocio = @idSocio AND Dia = @dia AND Activa = 1";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idSocio", idSocio);
                    cmd.Parameters.AddWithValue("@dia", dia);

                    try
                    {
                        conexion.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        resultado = count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al validar la rutina: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return resultado;
        }
    }
}
