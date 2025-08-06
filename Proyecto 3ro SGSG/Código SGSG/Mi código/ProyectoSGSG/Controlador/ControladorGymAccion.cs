using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace Controlador
{
    public class ControladorGymAccion
    { 
        AccionDAO acciondao = new AccionDAO();

        public List<Accion> Listar(int id)
        {
            return acciondao.Listar(id);
        }

        public List<Accion> ListarTodo()
        {
            return acciondao.ListarTodo();
        }

        public List<Accion> ListarAccionesConGrupo()
        {
            return acciondao.ListarAccionesConGrupo();
        }

        public bool Modificar(Accion unaAccion, out string mensaje)
        {
            return acciondao.Modificar(unaAccion, out mensaje);
        }
    }
}
