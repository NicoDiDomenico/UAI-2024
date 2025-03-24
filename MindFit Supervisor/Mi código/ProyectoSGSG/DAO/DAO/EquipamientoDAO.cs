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
    public class EquipamientoDAO
    {
        public List<Equipamiento> Listar()
        {
            List<Equipamiento> lista = new List<Equipamiento>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT 
                            eg.IdElemento,
                            eg.NombreElemento,
                            e.Precio
                        FROM Equipamiento e
                        INNER JOIN ElementoGimnasio eg ON e.IdElemento = eg.IdElemento
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Equipamiento equipamiento = new Equipamiento
                            {
                                IdElemento = Convert.ToInt32(dr["IdElemento"]),
                                NombreElemento = dr["NombreElemento"].ToString(),
                                Precio = Convert.ToSingle(dr["Precio"])
                            };

                            lista.Add(equipamiento);
                        }
                    }
                }
                catch
                {
                    lista = new List<Equipamiento>();
                }
            }

            return lista;
        }

        public bool Registrar(Equipamiento obj, out string mensaje)
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
                        // Insertar en Equipamiento
                        string queryEquipamiento = @"
                            INSERT INTO Equipamiento (IdElemento, Precio)
                            VALUES (@IdElemento, @Precio);
                        ";

                        SqlCommand cmdEquipamiento = new SqlCommand(queryEquipamiento, oconexion);
                        cmdEquipamiento.Parameters.AddWithValue("@IdElemento", nuevoIdElemento);
                        cmdEquipamiento.Parameters.AddWithValue("@Precio", obj.Precio);

                        int filas = cmdEquipamiento.ExecuteNonQuery();
                        respuesta = filas > 0;
                        mensaje = respuesta ? "Equipamiento registrado correctamente." : "No se pudo registrar el equipamiento.";
                    }
                    else
                    {
                        mensaje = "No se pudo registrar el elemento del gimnasio.";
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar el equipamiento: " + ex.Message;
            }

            return respuesta;
        }

        public bool Editar(Equipamiento obj, out string mensaje)
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

                    string queryEquipamiento = @"
                        UPDATE Equipamiento
                        SET Precio = @Precio
                        WHERE IdElemento = @IdElemento;
                    ";

                    SqlCommand cmdEquipamiento = new SqlCommand(queryEquipamiento, oconexion);
                    cmdEquipamiento.Parameters.AddWithValue("@Precio", obj.Precio);
                    cmdEquipamiento.Parameters.AddWithValue("@IdElemento", obj.IdElemento);

                    int filas = cmdEquipamiento.ExecuteNonQuery();
                    resultado = filas > 0;

                    mensaje = resultado ? "Equipamiento actualizado correctamente." : "No se pudo actualizar el equipamiento.";
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = "Error al editar el equipamiento: " + ex.Message;
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

                    string query = "DELETE FROM Equipamiento WHERE IdElemento = @IdElemento";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@IdElemento", idElemento);

                    int filas = cmd.ExecuteNonQuery();
                    respuesta = filas > 0;

                    mensaje = respuesta ? "Equipamiento eliminado correctamente." : "No se encontró el equipamiento para eliminar.";
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al eliminar el equipamiento: " + ex.Message;
            }

            return respuesta;
        }
    }
}
