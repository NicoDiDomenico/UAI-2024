using pjGestionEmpleados.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pjGestionEmpleados.Datos
{
    public class D_Empleados
    {
        // Método público que devuelve un DataTable con la lista de empleados
        public DataTable Listar_Empleados(string cBusqueda)
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

                // Crea un comando para ejecutar el procedimiento almacenado "SP_LISTAR_EMPLEADOS"
                SqlCommand comando = new SqlCommand("SP_LISTAR_EMPLEADOS", SqlCon);
                comando.CommandType = CommandType.StoredProcedure; // Especifica que el comando es un procedimiento almacenado

                // Agrega un parámetro al comando para enviar el texto de búsqueda
                comando.Parameters.Add("@cBusqueda", SqlDbType.VarChar).Value = cBusqueda;

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
        public string Guardar_Empleado(E_Empleado Empleado)
        {
            string respuesta = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = Conexion.crearInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("SP_GUARDAR_EMPLEADOS", SqlCon);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@cNombre", SqlDbType.VarChar).Value = Empleado.Nombre_Empleado;
                comando.Parameters.Add("@cDireccion", SqlDbType.VarChar).Value = Empleado.Direccion_Empleado;
                comando.Parameters.Add("@dFechaNacimiento", SqlDbType.Date).Value = Empleado.Fecha_Nacimiento_Empleado;
                comando.Parameters.Add("@cTelefono", SqlDbType.VarChar).Value = Empleado.Telefono_Empleado;
                comando.Parameters.Add("@nSalario", SqlDbType.Money).Value = Empleado.Salario_Empleado;
                comando.Parameters.Add("@nIdDepartamento", SqlDbType.Int).Value = Empleado.ID_Departamento;
                comando.Parameters.Add("@nIdCargo", SqlDbType.Int).Value = Empleado.ID_Cargo;

                SqlCon.Open();

                respuesta = comando.ExecuteNonQuery() >= 1 ? "OK" : "Los Datos No Se Pudieron Registrar";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }

            return respuesta;
        }
        public string Actualizar_Empleado(E_Empleado Empleado)
        {
            string respuesta = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = Conexion.crearInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("SP_ACTUALIZAR_EMPLEADOS", SqlCon);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@nIdEmpleado", SqlDbType.Int).Value = Empleado.ID_Empleado;
                comando.Parameters.Add("@cNombre", SqlDbType.VarChar).Value = Empleado.Nombre_Empleado;
                comando.Parameters.Add("@cDireccion", SqlDbType.VarChar).Value = Empleado.Direccion_Empleado;
                comando.Parameters.Add("@dFechaNacimiento", SqlDbType.Date).Value = Empleado.Fecha_Nacimiento_Empleado;
                comando.Parameters.Add("@cTelefono", SqlDbType.VarChar).Value = Empleado.Telefono_Empleado;
                comando.Parameters.Add("@nSalario", SqlDbType.Money).Value = Empleado.Salario_Empleado;
                comando.Parameters.Add("@nIdDepartamento", SqlDbType.Int).Value = Empleado.ID_Departamento;
                comando.Parameters.Add("@nIdCargo", SqlDbType.Int).Value = Empleado.ID_Cargo;

                SqlCon.Open();

                respuesta = comando.ExecuteNonQuery() >= 1 ? "OK" : "Los Datos No Se Pudieron Actualizar";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }

            return respuesta;
        }
        public string Borrar_Empleado(int iCodigoEmpleado)
        {
            string respuesta = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = Conexion.crearInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("SP_DESACTIVAR_EMPLEADOS", SqlCon);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@nIdEmpleado", SqlDbType.Int).Value = iCodigoEmpleado;

                SqlCon.Open();

                respuesta = comando.ExecuteNonQuery() >= 1 ? "OK" : "Los Datos No Se Pudieron Borrar";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }

            return respuesta;
        }
    }
}

