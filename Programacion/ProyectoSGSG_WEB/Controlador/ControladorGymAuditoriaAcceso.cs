using DAO;
using Modelo;
using System.Collections.Generic;

namespace Controlador
{
    public class ControladorGymAuditoriaAcceso
    {
        private AuditoriaAccesoDAO dao = new AuditoriaAccesoDAO();

        public List<AuditoriaAcceso> Listar()
        {
            return dao.Listar();
        }
    }
}
