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
    public class RangoHorarioDAO
    {
        public List<RangoHorario> Listar()
        {
            List<RangoHorario> lista = new List<RangoHorario>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        select IdRangoHorario, HoraDesde, HoraHasta, CupoMaximo 
                        from RangoHorario
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            RangoHorario unRangoHorario = new RangoHorario();

                            unRangoHorario.IdRangoHorario = Convert.ToInt32(dr["IdRangoHorario"]);
                            unRangoHorario.HoraDesde = (TimeSpan)dr["HoraDesde"];
                            unRangoHorario.HoraHasta = (TimeSpan)dr["HoraHasta"];
                            unRangoHorario.CupoMaximo = Convert.ToInt32(dr["CupoMaximo"]);
                            /*
                            unRangoHorario.UnUsuario = new Usuario();
                            unRangoHorario.UnUsuario.NombreYApellido = Convert.ToString(dr["NombreYApellido"]);
                            unRangoHorario.UnUsuario.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                            */

                            lista.Add(unRangoHorario);
                        }
                    }
                }
                catch (Exception)
                {
                    lista = new List<RangoHorario>();
                }
            }
            return lista;
        }

        public List<RangoHorario> ListarTodo()
        {
            List<RangoHorario> lista = new List<RangoHorario>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        select rh.IdRangoHorario, rh.HoraDesde, rh.HoraHasta, u.NombreYApellido, u.IdUsuario from RangoHorario rh
                        inner join RangoHorario_Usuario rh_u
                        on rh.IdRangoHorario = rh_u.IdRangoHorario
                        inner join Usuario u 
                        on rh_u.IdUsuario = u.IdRol
                    ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            RangoHorario unRangoHorario = new RangoHorario();

                            unRangoHorario.IdRangoHorario = Convert.ToInt32(dr["IdRangoHorario"]);
                            unRangoHorario.HoraDesde = (TimeSpan)dr["HoraDesde"];
                            unRangoHorario.HoraHasta = (TimeSpan)dr["HoraHasta"];
                            
                            unRangoHorario.UnUsuario = new Usuario();
                            unRangoHorario.UnUsuario.NombreYApellido = Convert.ToString(dr["NombreYApellido"]);
                            unRangoHorario.UnUsuario.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                            

                            lista.Add(unRangoHorario);
                        }
                    }
                }
                catch (Exception)
                {
                    lista = new List<RangoHorario>();
                }
            }
            return lista;
        }

        public bool Registrar(int IdRangoHorario, int IdUsuario, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_RANGOHORARIO_USUARIO", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdRangoHorario", IdRangoHorario);
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                    resultado = false;
                }
            }

            return resultado;
        }
        public bool ActualizarCupo(int idRangoHorario, int nuevoCupo, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("UPDATE RangoHorario SET CupoMaximo = @Cupo WHERE IdRangoHorario = @IdRango", conexion);
                    cmd.Parameters.AddWithValue("@Cupo", nuevoCupo);
                    cmd.Parameters.AddWithValue("@IdRango", idRangoHorario);

                    conexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                    resultado = false;
                }
            }
            return resultado;
        }

        public bool EliminarRelacion(int idRangoHorario, int idUsuario, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM RangoHorario_Usuario WHERE IdRangoHorario = @IdRangoHorario AND IdUsuario = @IdUsuario", conexion);
                    cmd.Parameters.AddWithValue("@IdRangoHorario", idRangoHorario);
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    conexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                    resultado = false;
                }
            }
            return resultado;
        }
    }
}
