using DAO;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Controlador.State.Turno; // arriba del archivo

namespace Controlador
{
    public class ControladorGymTurno
    {
        private TurnoDAO daoTurno = new TurnoDAO();
       
        public List<Turno> Listar(int idSocioSeleccionado)
        {
            List<Turno> turnos = daoTurno.Listar(idSocioSeleccionado);

            foreach (Turno turno in turnos)
            {
                var estado = EstadoFactoryTurno.ObtenerEstado(turno.EstadoTurno);
                estado?.Evaluar(turno, daoTurno);
            }

            return turnos;
        }

        public int Registrar(Turno obj, out string mensaje)
        {
            /*
            obj.CodigoIngreso = GenerarCodigoIngreso();
            obj.EstadoTurno = "En Curso"; // RF21
            return daoTurno.Registrar(obj, out mensaje);
            */
            obj.EstadoTurno = "En Curso"; // RF21
            var estado = EstadoFactoryTurno.ObtenerEstado(obj.EstadoTurno);

            if (estado == null)
            {
                mensaje = "El estado del turno no es válido.";
                return -1;
            }

            return estado.Registrar(obj, daoTurno, out mensaje);
        }

        public bool Eliminar(int idTurno, int horarioId, DateTime fechaTurno, out string mensaje)
        {
            // En el SP se resta el cupo si el turno que se quiere eliminar tiene el estado 'En Curso'
            return daoTurno.Eliminar(idTurno, horarioId, fechaTurno, out mensaje);
        }

        public bool ValidarCodigoIngreso(string codigo, out int idTurno, out int idRangoHorario, out DateTime fechaTurno, out string mensaje)
        {
            return daoTurno.ValidarCodigoIngreso(codigo, out idTurno, out idRangoHorario, out fechaTurno, out mensaje);
        }
        public bool ActualizarEstadoTurno(int idTurno, int idRangoHorario, DateTime fechaTurno)
        {

            var estado = EstadoFactoryTurno.ObtenerEstado("Finalizado");
            if (estado == null) return false;
            return estado.ActualizarEstadoTurno(idTurno, daoTurno, idRangoHorario, fechaTurno);
        }

        public List<Turno> ListarTurnosHorarioActual()
        {
            return daoTurno.ListarTurnosHorarioActual();
        }
    }
}
