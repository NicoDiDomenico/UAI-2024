using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    // Esta clase conexion se encargará de enviar a las demás clases la cadena de conexion que tenemos almacenada en App.config
    public class Conexion 
    {
        public static string cadena = ConfigurationManager.ConnectionStrings["cadena_conexion"].ToString();
    }
}
