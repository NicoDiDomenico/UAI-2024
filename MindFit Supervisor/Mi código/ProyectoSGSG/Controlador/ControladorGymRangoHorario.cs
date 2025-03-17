using DAO;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ControladorGymRangoHorario
    {
        private RangoHorarioDAO objcd_RangoHorario = new RangoHorarioDAO();

        public List<RangoHorario> Listar()
        {
            return objcd_RangoHorario.Listar();
        }
        public List<RangoHorario> ListarTodo()
        {
            return objcd_RangoHorario.ListarTodo();
        }
        public List<RangoHorario> ListarParaTurno()
        {
            return objcd_RangoHorario.ListarParaTurno();
        }
        public List<RangoHorario> ListarEntrenadoresDisponibles(int id, DateTime FechaTurno)
        {
            return objcd_RangoHorario.ListarEntrenadoresDisponibles(id, FechaTurno);
        }

        public bool Registrar(int idRH, int idU, out string mensaje)
        {
            return objcd_RangoHorario.Registrar(idRH, idU, out mensaje);
        }
        public bool ActualizarCupo(int idRangoHorario, int nuevoCupo, out string mensaje)
        {
            return objcd_RangoHorario.ActualizarCupo(idRangoHorario, nuevoCupo, out mensaje);
        }

        public bool EliminarRelacion(int idRangoHorario, int idUsuario, out string mensaje)
        {
            return objcd_RangoHorario.EliminarRelacion(idRangoHorario, idUsuario, out mensaje);
        }
        
    }
}
