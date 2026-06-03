using DAO;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ControladorGymHistorialCalentamiento
    {
        private HistorialCalentamientoDAO objcd_HistorialCalentamientoDAO = new HistorialCalentamientoDAO();

        public List<RutinaCalentamiento> ListarCalentamientosPorHistorialRutina(int IdRutina)
        {
            return objcd_HistorialCalentamientoDAO.ListarCalentamientosPorHistorialRutina(IdRutina);
        }
    }
}
