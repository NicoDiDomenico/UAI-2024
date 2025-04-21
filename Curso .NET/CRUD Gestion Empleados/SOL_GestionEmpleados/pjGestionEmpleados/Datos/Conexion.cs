using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pjGestionEmpleados.Datos
{
    public class Conexion
    {
        private string Base;
        private string Servidor;
        private string Usuario;
        private string Clave;
        private static Conexion Con = null;

        // Constructor de la clase
        private Conexion()
        {
            this.Servidor = "DiDomenicoPC\\SQLEXPRESS";
            this.Base = "bd_gestion_empleados";
            this.Usuario = "nicolas_didomenico";
            this.Clave = "0045981746";
        }

        public SqlConnection CrearConexion()
        {
            SqlConnection cadena = new SqlConnection();

            try
            {
                cadena.ConnectionString = "Server=" + this.Servidor +
                                          "; Database=" + this.Base +
                                          "; User Id=" + this.Usuario +
                                          "; Password=" + this.Clave;
            }
            catch (Exception ex)
            {
                cadena = null;
                throw ex;
            }

            return cadena;
        }

        // Si no hago el siguiente metodo de instanciacion no puedo usar el metodo CrearConexion()
        public static Conexion crearInstancia()
        {
            // Implementa el patrón Singleton
            if (Con == null)
            {
                Con = new Conexion(); // Se crea una sola instancia
            }
            return Con; // Se reutiliza la misma instancia
        }
    }
}
