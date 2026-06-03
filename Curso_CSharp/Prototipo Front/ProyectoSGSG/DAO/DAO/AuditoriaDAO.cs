using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAO
{
    public class AuditoriaDAO
    {
        public static void RegistrarEvento(int idUsuario, string tipoEvento)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    string query = "INSERT INTO AuditoriaAccesos (IdUsuario, TipoEvento) VALUES (@idUsuario, @evento)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@evento", tipoEvento);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Opcional: loguear el error si querés
                MessageBox.Show("Error al registrar auditoría: " + ex.Message);
            }
        }
    }
}
