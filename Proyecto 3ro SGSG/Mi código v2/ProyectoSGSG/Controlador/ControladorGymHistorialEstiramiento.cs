using DAO;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ControladorGymHistorialEstiramiento
    {
        private HistorialEstiramientoDAO objcd_HistorialEstiramiento = new HistorialEstiramientoDAO();

        public List<RutinaEstiramiento> ListarEstiramientosPorHistorialRutina(int IdRutina)
        {
            return objcd_HistorialEstiramiento.ListarEstiramientosPorHistorialRutina(IdRutina);
        }
    }
}
