using DAO;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ControladorGymTurno
    {
        private TurnoDAO daoTurno = new TurnoDAO();
        private string GenerarCodigoIngreso()
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            string codigoGenerado;
            HashSet<string> codigosExistentes = daoTurno.ObtenerCodigosExistentes();

            do
            {
                codigoGenerado = new string(Enumerable.Repeat(caracteres, 4)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            while (codigosExistentes.Contains(codigoGenerado)); // Asegurar que sea único

            return codigoGenerado;
        }
        public List<Turno> Listar(int idSocioSeleccionado)
        {
            return daoTurno.Listar(idSocioSeleccionado);
        }

        public int Registrar(Turno obj, out string mensaje)
        {
            obj.CodigoIngreso = GenerarCodigoIngreso();
            obj.EstadoTurno = "En Curso"; // RF21
            return daoTurno.Registrar(obj, out mensaje);
        }

        public bool Eliminar(int idTurno, int horarioId, out string mensaje)
        {
            return new TurnoDAO().Eliminar(idTurno, horarioId, out mensaje);
        }
    }
}
