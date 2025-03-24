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
    public class MaquinaDAO
    {
        public List<Maquina> Listar()
        {
            List<Maquina> lista = new List<Maquina>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT 
                            eg.IdElemento,
                            eg.NombreElemento,
                            m.FechaFabricacion,
                            m.FechaCompra,
                            m.Precio,
                            m.Peso,
                            m.EsElectrica
                        FROM Maquina m
                        INNER JOIN ElementoGimnasio eg ON m.IdElemento = eg.IdElemento
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Maquina unaMaquina = new Maquina
                            {
                                IdElemento = Convert.ToInt32(dr["IdElemento"]),
                                NombreElemento = dr["NombreElemento"].ToString(),
                                FechaFabricacion = Convert.ToDateTime(dr["FechaFabricacion"]),
                                FechaCompra = Convert.ToDateTime(dr["FechaCompra"]),
                                Precio = Convert.ToSingle(dr["Precio"]),
                                Peso = Convert.ToInt32(dr["Peso"]),
                                EsElectrica = Convert.ToBoolean(dr["EsElectrica"])
                            };

                            lista.Add(unaMaquina);
                        }
                    }
                }
                catch (Exception mensaje)
                {
                    lista = new List<Maquina>(); // Devuelve lista vacía en caso de error
                    //MessageBox.Show(mensaje.Message);
                }
            }

            return lista;
        }
        public bool Registrar(Maquina obj, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();

                    // 1. Insertar en ElementoGimnasio
                    string queryElemento = @"
                        INSERT INTO ElementoGimnasio (NombreElemento)
                        VALUES (@NombreElemento);
                        SELECT SCOPE_IDENTITY(); -- Obtener el nuevo IdElemento
                    ";

                    SqlCommand cmdElemento = new SqlCommand(queryElemento, oconexion);
                    cmdElemento.CommandType = CommandType.Text;
                    cmdElemento.Parameters.AddWithValue("@NombreElemento", obj.NombreElemento);

                    object result = cmdElemento.ExecuteScalar();
                    int nuevoIdElemento = 0;

                    if (result != null && int.TryParse(result.ToString(), out nuevoIdElemento))
                    {
                        // 2. Insertar en Maquina con ese IdElemento
                        string queryMaquina = @"
                            INSERT INTO Maquina (IdElemento, FechaFabricacion, FechaCompra, Precio, Peso, EsElectrica)
                            VALUES (@IdElemento, @FechaFabricacion, @FechaCompra, @Precio, @Peso, @EsElectrica);
                        ";

                        SqlCommand cmdMaquina = new SqlCommand(queryMaquina, oconexion);
                        cmdMaquina.CommandType = CommandType.Text;

                        cmdMaquina.Parameters.AddWithValue("@IdElemento", nuevoIdElemento);
                        cmdMaquina.Parameters.AddWithValue("@FechaFabricacion", obj.FechaFabricacion);
                        cmdMaquina.Parameters.AddWithValue("@FechaCompra", obj.FechaCompra);
                        cmdMaquina.Parameters.AddWithValue("@Precio", obj.Precio);
                        cmdMaquina.Parameters.AddWithValue("@Peso", obj.Peso);
                        cmdMaquina.Parameters.AddWithValue("@EsElectrica", obj.EsElectrica ? 1 : 0);

                        int filas = cmdMaquina.ExecuteNonQuery();
                        if (filas > 0)
                        {
                            respuesta = true;
                            mensaje = "Máquina registrada correctamente.";
                        }
                        else
                        {
                            mensaje = "No se pudo registrar la máquina.";
                        }
                    }
                    else
                    {
                        mensaje = "No se pudo registrar el elemento del gimnasio.";
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar la máquina: " + ex.Message;
            }

            return respuesta;
        }

        public bool Editar(Maquina obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();

                    // 1. Actualizar ElementoGimnasio
                    string queryElemento = @"
                        UPDATE ElementoGimnasio
                        SET NombreElemento = @NombreElemento
                        WHERE IdElemento = @IdElemento;
                    ";

                    SqlCommand cmdElemento = new SqlCommand(queryElemento, oconexion);
                    cmdElemento.Parameters.AddWithValue("@NombreElemento", obj.NombreElemento);
                    cmdElemento.Parameters.AddWithValue("@IdElemento", obj.IdElemento);
                    cmdElemento.ExecuteNonQuery();

                    // 2. Actualizar Maquina
                    string queryMaquina = @"
                        UPDATE Maquina
                        SET FechaFabricacion = @FechaFabricacion,
                            FechaCompra = @FechaCompra,
                            Precio = @Precio,
                            Peso = @Peso,
                            EsElectrica = @EsElectrica
                        WHERE IdElemento = @IdElemento;
                    ";

                    SqlCommand cmdMaquina = new SqlCommand(queryMaquina, oconexion);
                    cmdMaquina.Parameters.AddWithValue("@FechaFabricacion", obj.FechaFabricacion);
                    cmdMaquina.Parameters.AddWithValue("@FechaCompra", obj.FechaCompra);
                    cmdMaquina.Parameters.AddWithValue("@Precio", obj.Precio);
                    cmdMaquina.Parameters.AddWithValue("@Peso", obj.Peso);
                    cmdMaquina.Parameters.AddWithValue("@EsElectrica", obj.EsElectrica ? 1 : 0);
                    cmdMaquina.Parameters.AddWithValue("@IdElemento", obj.IdElemento);

                    int filas = cmdMaquina.ExecuteNonQuery();
                    resultado = filas > 0;

                    mensaje = resultado ? "Máquina actualizada correctamente." : "No se pudo actualizar la máquina.";
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = "Error al editar la máquina: " + ex.Message;
            }

            return resultado;
        }

        public bool Eliminar(int idMaquina, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    string query = "DELETE FROM Maquina WHERE IdMaquina = @IdMaquina";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IdMaquina", idMaquina);

                    oconexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    respuesta = filasAfectadas > 0;
                    mensaje = "Maquina eliminado exitosamente";
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al eliminar el Maquina: " + ex.Message;
            }

            return respuesta;
        }
    }
}
