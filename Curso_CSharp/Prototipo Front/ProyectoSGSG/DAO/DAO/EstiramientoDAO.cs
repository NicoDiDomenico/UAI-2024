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
    public class EstiramientoDAO
    {
        public List<Estiramiento> Listar()
        {
            List<Estiramiento> lista = new List<Estiramiento>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        select IdEstiramiento, DescripcionEstiramiento 
                        from Estiramiento
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Estiramiento unEstiramiento = new Estiramiento();

                            unEstiramiento.IdEstiramiento = Convert.ToInt32(dr["IdEstiramiento"]);
                            unEstiramiento.DescripcionEstiramiento = dr["DescripcionEstiramiento"].ToString();
                            
                            lista.Add(unEstiramiento);
                        }

                    }
                }
                catch (Exception)
                {
                    lista = new List<Estiramiento>();
                }
            }
            return lista;
        }

        public bool Registrar(Estiramiento obj, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    string query = @"
                        INSERT INTO Estiramiento (DescripcionEstiramiento)
                        VALUES (@DescripcionEstiramiento);
                        SELECT SCOPE_IDENTITY();
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@DescripcionEstiramiento", obj.DescripcionEstiramiento);

                    oconexion.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int idGenerado))
                    {
                        respuesta = idGenerado > 0;
                        mensaje = "Estiramiento registrado exitosamente";
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar el Estiramiento: " + ex.Message;
            }

            return respuesta;
        }


        public bool Editar(Estiramiento obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    string query = @"
                        UPDATE Estiramiento
                        SET DescripcionEstiramiento = @DescripcionEstiramiento
                        WHERE IdEstiramiento = @IdEstiramiento
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@IdEstiramiento", obj.IdEstiramiento);
                    cmd.Parameters.AddWithValue("@DescripcionEstiramiento", obj.DescripcionEstiramiento);

                    oconexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    resultado = filasAfectadas > 0;
                    mensaje = "Estiramiento actualizado exitosamente";
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = "Error al editar el Estiramiento: " + ex.Message;
            }
            return resultado;
        }

        public bool Eliminar(int idEstiramiento, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    string query = "DELETE FROM Estiramiento WHERE IdEstiramiento = @IdEstiramiento";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IdEstiramiento", idEstiramiento);

                    oconexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    respuesta = filasAfectadas > 0;
                    mensaje = "Estiramiento eliminado exitosamente";
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al eliminar el Estiramiento: " + ex.Message;
            }

            return respuesta;
        }

        public List<RutinaEstiramiento> ListarEstiramientosPorRutina(int idRutina)
        {
            List<RutinaEstiramiento> lista = new List<RutinaEstiramiento>();

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cadena))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT IdEstiramiento, Duracion 
                        FROM Rutina_Estiramiento 
                        WHERE IdRutina = @IdRutina", conn);

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
