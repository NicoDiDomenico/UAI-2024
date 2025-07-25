﻿using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Modelo;

namespace Controlador.State.Turno
{
    public interface IEstadoTurno
    {
        string Nombre { get; }
        void Evaluar(Modelo.Turno turno, TurnoDAO dao);
        int Registrar(Modelo.Turno obj, TurnoDAO daoTurno, out string mensaje);

        bool ActualizarEstadoTurno(int idTurno, TurnoDAO daoTurno, int idRangoHorario, DateTime fechaTurno);
    }
}
