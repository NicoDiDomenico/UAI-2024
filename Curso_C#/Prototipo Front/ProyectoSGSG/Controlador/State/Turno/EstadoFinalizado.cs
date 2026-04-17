using Controlador.State.Turno;
using DAO;
using Modelo;
using System;

namespace Controlador.State.Turno
{
    public class EstadoFinalizado : IEstadoTurno
    {
        public string Nombre => "Finalizado";

        public bool ActualizarEstadoTurno(int idTurno, TurnoDAO daoTurno, int idRangoHorario, DateTime fechaTurno)
        {
            return daoTurno.ActualizarEstadoTurno(idTurno, idRangoHorario, fechaTurno);
        }

        public void Evaluar(Modelo.Turno turno, TurnoDAO dao)
        {
            // Ya está finalizado, no hace nada
        }

        public int Registrar(Modelo.Turno obj, TurnoDAO daoTurno, out string mensaje)
        {
            throw new System.NotImplementedException();
        }
    }
}
