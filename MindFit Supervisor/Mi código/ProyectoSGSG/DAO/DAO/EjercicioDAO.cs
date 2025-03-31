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
    public class EjercicioDAO
    {
        public List<Ejercicio> Listar()
        {
            List<Ejercicio> lista = new List<Ejercicio>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT 
                            eg.IdElemento,
                            eg.NombreElemento,
                            e.Descripcion
                        FROM Ejercicio e
                        INNER JOIN ElementoGimnasio eg ON e.IdElemento = eg.IdElemento
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Ejercicio ejercicio = new Ejercicio
                            {
                                IdElemento = Convert.ToInt32(dr["IdElemento"]),
                                NombreElemento = dr["NombreElemento"].ToString(),
                                Descripcion = dr["Descripcion"].ToString()
                            };

                            lista.Add(ejercicio);
                        }
                    }
                }
                catch
                {
                    lista = new List<Ejercicio>();
                }
            }

            return lista;
        }

        public bool Registrar(Ejercicio obj, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();

                    // Insertar en ElementoGimnasio
                    string queryElemento = @"
                        INSERT INTO ElementoGimnasio (NombreElemento)
                        VALUES (@NombreElemento);
                        SELECT SCOPE_IDENTITY();
                    ";

                    SqlCommand cmdElemento = new SqlCommand(queryElemento, oconexion);
                    cmdElemento.Parameters.AddWithValue("@NombreElemento", obj.NombreElemento);

                    object result = cmdElemento.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int nuevoIdElemento))
                    {
                        // Insertar en Ejercicio
                        string queryEjercicio = @"
                            INSERT INTO Ejercicio (IdElemento, Descripcion)
                            VALUES (@IdElemento, @Descripcion);
                        ";

                        SqlCommand cmdEjercicio = new SqlCommand(queryEjercicio, oconexion);
                        cmdEjercicio.Parameters.AddWithValue("@IdElemento", nuevoIdElemento);
                        cmdEjercicio.Parameters.AddWithValue("@Descripcion", obj.Descripcion);

                        int filas = cmdEjercicio.ExecuteNonQuery();
                        respuesta = filas > 0;
                        mensaje = respuesta ? "Ejercicio registrado correctamente." : "No se pudo registrar el ejercicio.";
                    }
                    else
                    {
                        mensaje = "No se pudo registrar el elemento del gimnasio.";
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar el ejercicio: " + ex.Message;
            }

            return respuesta;
        }

        public bool Editar(Ejercicio obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();

                    string queryElemento = @"
                        UPDATE ElementoGimnasio
                        SET NombreElemento = @NombreElemento
                        WHERE IdElemento = @IdElemento;
                    ";

                    SqlCommand cmdElemento = new SqlCommand(queryElemento, oconexion);
                    cmdElemento.Parameters.AddWithValue("@NombreElemento", obj.NombreElemento);
                    cmdElemento.Parameters.AddWithValue("@IdElemento", obj.IdElemento);
                    cmdElemento.ExecuteNonQuery();

                    string queryEjercicio = @"
                        UPDATE Ejercicio
                        SET Descripcion = @Descripcion
                        WHERE IdElemento = @IdElemento;
                    ";

                    SqlCommand cmdEjercicio = new SqlCommand(queryEjercicio, oconexion);
                    cmdEjercicio.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmdEjercicio.Parameters.AddWithValue("@IdElemento", obj.IdElemento);

                    int filas = cmdEjercicio.ExecuteNonQuery();
                    resultado = filas > 0;

                    mensaje = resultado ? "Ejercicio actualizado correctamente." : "No se pudo actualizar el ejercicio.";
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = "Error al editar el ejercicio: " + ex.Message;
            }

            return resultado;
        }

        public bool Eliminar(int idElemento, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();

                    SqlTransaction transaccion = oconexion.BeginTransaction();

                    try
                    {
                        // 1. Eliminar de Ejercicio (tabla hija)
                        string queryEjercicio = "DELETE FROM Ejercicio WHERE IdElemento = @IdElemento";
                        SqlCommand cmdEjercicio = new SqlCommand(queryEjercicio, oconexion, transaccion);
                        cmdEjercicio.Parameters.AddWithValue("@IdElemento", idElemento);
                        cmdEjercicio.ExecuteNonQuery();

                        // 2. Eliminar de ElementoGimnasio (tabla padre)
                        string queryElemento = "DELETE FROM ElementoGimnasio WHERE IdElemento = @IdElemento";
                        SqlCommand cmdElemento = new SqlCommand(queryElemento, oconexion, transaccion);
                        cmdElemento.Parameters.AddWithValue("@IdElemento", idElemento);
                        int filasAfectadas = cmdElemento.ExecuteNonQuery();

                        transaccion.Commit();

                        respuesta = filasAfectadas > 0;
                        mensaje = respuesta ? "Ejercicio y elemento eliminados correctamente." : "No se encontró el elemento para eliminar.";
                    }
                    catch (Exception ex2)
                    {
                        transaccion.Rollback();
                        mensaje = "Error durante la transacción: " + ex2.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al conectar con la base de datos: " + ex.Message;
            }

            return respuesta;
        }
    }
}
