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
    public class CalentamientoDAO
    {
        public List<Calentamiento> Listar()
        {
            List<Calentamiento> lista = new List<Calentamiento>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        select IdCalentamiento, IdMaquina, DescripcionCalentamiento 
                        from Calentamiento
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Calentamiento unCalentamiento = new Calentamiento();

                            unCalentamiento.IdCalentamiento = Convert.ToInt32(dr["IdCalentamiento"]);
                            unCalentamiento.DescripcionCalentamiento = dr["DescripcionCalentamiento"].ToString();

                            if (dr["IdMaquina"] != DBNull.Value)
                            {
                                unCalentamiento.MaquinaTipoCardio = new Maquina
                                {
                                    IdElemento = Convert.ToInt32(dr["IdMaquina"])
                                };
                            }
                            else
                            {
                                unCalentamiento.MaquinaTipoCardio = null;
                            }

                            lista.Add(unCalentamiento);
                        }

                    }
                }
                catch (Exception)
                {
                    lista = new List<Calentamiento>();
                }
            }
            return lista;
        }

        public bool Registrar(Calentamiento obj, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    string query = @"
                        INSERT INTO Calentamiento (IdMaquina, DescripcionCalentamiento)
                        VALUES (@IdMaquina, @DescripcionCalentamiento);
                        SELECT SCOPE_IDENTITY();
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@IdMaquina",
                        obj.MaquinaTipoCardio != null ? obj.MaquinaTipoCardio.IdElemento : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DescripcionCalentamiento", obj.DescripcionCalentamiento);

                    oconexion.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int idGenerado))
                    {
                        respuesta = idGenerado > 0;
                        mensaje = "Calentamiento registrado exitosamente";
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar el calentamiento: " + ex.Message;
            }

            return respuesta;
        }


        public bool Editar(Calentamiento obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    string query = @"
                        UPDATE Calentamiento
                        SET IdMaquina = @IdMaquina,
                            DescripcionCalentamiento = @DescripcionCalentamiento
                        WHERE IdCalentamiento = @IdCalentamiento
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@IdCalentamiento", obj.IdCalentamiento);
                    cmd.Parameters.AddWithValue("@IdMaquina", obj.MaquinaTipoCardio != null ? obj.MaquinaTipoCardio.IdElemento : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DescripcionCalentamiento", obj.DescripcionCalentamiento);

                    oconexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    resultado = filasAfectadas > 0;
                    mensaje = "Calentamiento actualizado exitosamente";
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = "Error al editar el calentamiento: " + ex.Message;
            }
            return resultado;
        }

        public bool Eliminar(int idCalentamiento, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    string query = "DELETE FROM Calentamiento WHERE IdCalentamiento = @IdCalentamiento";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IdCalentamiento", idCalentamiento);

                    oconexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    respuesta = filasAfectadas > 0;
                    mensaje = "Calentamiento eliminado exitosamente";
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al eliminar el calentamiento: " + ex.Message;
            }

            return respuesta;
        }
    }
}
