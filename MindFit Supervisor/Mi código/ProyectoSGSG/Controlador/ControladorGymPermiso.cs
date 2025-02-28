using Modelo;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ControladorGymPermiso
    {
        private PermisoDAO objcd_permiso = new PermisoDAO();

        public List<Permiso> Listar(int idUsuario)
        {
            return objcd_permiso.Listar(idUsuario);
        }
        public List<Permiso> ListarTodos()
        {
            return objcd_permiso.ListarTodos();
        }
    }
}
