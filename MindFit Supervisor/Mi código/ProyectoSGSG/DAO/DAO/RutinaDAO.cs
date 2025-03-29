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
                        select r.IdRutina, r.IdSocio, r.FechaModificacion, r.Dia  
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

    }
}
