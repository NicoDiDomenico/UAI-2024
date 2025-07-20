using Controlador.State.Turno;
using DAO;
using Modelo;
using System;

namespace Controlador.State.Turno
{
    public class EstadoCancelado : IEstadoTurno
    {
        public string Nombre => "Cancelado";

        public bool ActualizarEstadoTurno(int idTurno, TurnoDAO daoTurno, int idRangoHorario, DateTime fechaTurno)
        {
            return daoTurno.ModificarEstadoTurno(idTurno, "Cancelado", fechaTurno);
        }

        public void Evaluar(Modelo.Turno turno, TurnoDAO dao)
        {
            // Ya está cancelado, no hace nada
        }

        public int Registrar(Modelo.Turno obj, TurnoDAO daoTurno, out string mensaje)
        {
            throw new System.NotImplementedException();
        }
    }
}
