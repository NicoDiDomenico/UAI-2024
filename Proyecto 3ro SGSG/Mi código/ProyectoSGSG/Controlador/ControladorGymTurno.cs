﻿using DAO;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            List<Turno> turnos = daoTurno.Listar(idSocioSeleccionado);

            foreach (Turno turno in turnos)
            {
                if (turno.FechaTurno < DateTime.Today && turno.EstadoTurno != "Finalizado" && turno.EstadoTurno != "Cancelado")// ... && turno.EstadoTurno != "Vencido") // --> No implementado "Vencido"
                {
                    turno.EstadoTurno = "Cancelado";
                    daoTurno.ModificarEstadoTurno(turno.IdTurno, turno.EstadoTurno, turno.FechaTurno);
                }
            }

            return turnos;
        }

        public int Registrar(Turno obj, out string mensaje)
        {
            obj.CodigoIngreso = GenerarCodigoIngreso();
            obj.EstadoTurno = "En Curso"; // RF21
            return daoTurno.Registrar(obj, out mensaje);
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
            return daoTurno.ActualizarEstadoTurno(idTurno, idRangoHorario, fechaTurno);
        }

        public List<Turno> ListarTurnosHorarioActual()
        {
            return daoTurno.ListarTurnosHorarioActual();
        }
    }
}
