using DAO;
using Modelo;
using System.Collections.Generic;

namespace Controlador
{
    public class ControladorGymAuditoriaTurno
    {
        private AuditoriaTurnoDAO dao = new AuditoriaTurnoDAO();

        public List<AuditoriaTurno> Listar()
        {
            return dao.Listar();
        }
    }
}
