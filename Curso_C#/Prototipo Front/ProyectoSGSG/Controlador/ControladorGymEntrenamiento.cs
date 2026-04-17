using DAO;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ControladorGymEntrenamiento
    {
        private EntrenamientoDAO objcd_Entrenamiento = new EntrenamientoDAO();

        public List<Entrenamiento> ListarPorRutina(int idRutina)
        {
            return new EntrenamientoDAO().ListarPorRutina(idRutina);
        }
    }
}
