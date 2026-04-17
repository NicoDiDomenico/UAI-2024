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

        public List<PermisoPersonalizado3> ListarPermisoPersonalizado3(int idUsuario)
        {
            return objcd_permiso.ListarPermisoPersonalizado3(idUsuario);
        }

        public List<Permiso> ObtenerPermisosRol(int IdRol)
        {
            return objcd_permiso.ObtenerPermisosRol(IdRol);
        }
    }
}
