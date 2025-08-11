using System.Configuration;

namespace DAO
{
    // Versión simple: no usa ConfigurationManager.
    // La cadena se establece desde la app que arranca (Web o WinForms).
    public class Conexion
    {
        public static string cadena { get; private set; } = string.Empty;

        public static void Configurar(string cadenaConexion)
        {
            if (!string.IsNullOrWhiteSpace(cadenaConexion))
                cadena = cadenaConexion;
        }
    }
}
