using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pjGestionEmpleados.Datos
{
    public class D_Cargos
    {
        public DataTable Listar_Cargos()
        {
            // Variable para leer los datos devueltos por la base de datos
            SqlDataReader resultado;

            // Objeto DataTable que almacenará los datos obtenidos
            DataTable tabla = new DataTable();

            // Objeto SqlConnection para establecer la conexión con la base de datos
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                // Obtiene una conexión a la base de datos desde la clase Conexion
                SqlCon = Conexion.crearInstancia().CrearConexion();

                // Crea un comando para ejecutar el procedimiento almacenado "SP_LISTAR_CARGOS"
                SqlCommand comando = new SqlCommand("SP_LISTAR_CARGOS", SqlCon);
                comando.CommandType = CommandType.StoredProcedure; // Especifica que el comando es un procedimiento almacenado

                // Abre la conexión con la base de datos
                SqlCon.Open();

                // Ejecuta el comando y obtiene los resultados en un SqlDataReader
                resultado = comando.ExecuteReader();

                // Carga los datos del SqlDataReader en el DataTable
                tabla.Load(resultado);

                // Devuelve el DataTable con los datos obtenidos
                return tabla;
            }
            catch (Exception ex)
            {
                // Muestra un mensaje de error si ocurre una excepción
                MessageBox.Show(ex.Message);

                // Lanza la excepción para que pueda ser manejada en otro nivel del programa
                throw ex;
            }
            finally
            {
                // Asegura que la conexión se cierre si está abierta, incluso si ocurre un error
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }
    }
}
