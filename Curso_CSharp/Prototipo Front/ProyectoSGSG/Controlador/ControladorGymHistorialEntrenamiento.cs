using DAO;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ControladorGymHistorialEntrenamiento
    {
        private HistorialEntrenamientoDAO objcd_HistorialEntrenamiento = new HistorialEntrenamientoDAO();

        public List<Entrenamiento> ListarEntrenamientoPorHistorialRutina(int idRutina)
        {
            return new HistorialEntrenamientoDAO().ListarEntrenamientoPorHistorialRutina(idRutina);
        }
    }
}
