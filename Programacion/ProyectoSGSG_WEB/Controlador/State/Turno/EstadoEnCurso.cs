using System;
using System.Collections.Generic;
using System.Linq;
using Controlador.State.Turno;
using DAO;
using Modelo;

namespace Controlador.State.Turno
{
    public class EstadoEnCurso : IEstadoTurno
    {
        public string Nombre => "En Curso";

        private string GenerarCodigoIngreso(TurnoDAO daoTurno)
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

        public void Evaluar(Modelo.Turno turno, TurnoDAO dao)
        {
            if (turno.FechaTurno < DateTime.Today)
            {
                turno.EstadoTurno = "Cancelado";
                dao.ModificarEstadoTurno(turno.IdTurno, "Cancelado", turno.FechaTurno);
            }
        }

        public int Registrar(Modelo.Turno obj, TurnoDAO daoTurno, out string mensaje)
        {
            obj.CodigoIngreso = GenerarCodigoIngreso(daoTurno);
            return daoTurno.Registrar(obj, out mensaje);
        }

        public bool ActualizarEstadoTurno(int idTurno, TurnoDAO daoTurno, int idRangoHorario, DateTime fechaTurno)
        {
            throw new NotImplementedException();
        }
    }
}
