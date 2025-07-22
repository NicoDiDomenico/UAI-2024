using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAO;
using System.Data;

namespace Controlador
{
    public class ControladorGymRol
    {
        private RolDAO objcd_rol = new RolDAO();

        public List<Rol> Listar()
        {
            return objcd_rol.Listar();
        }

        public List<Rol> ListarConAcciones()
        {
            return objcd_rol.ListarConAcciones();
        }

        public bool Registrar(string Descripcion, DataTable Permisos, out string Mensaje)
        {
            return objcd_rol.Registrar(Descripcion, Permisos, out Mensaje);
        }

        public bool Actualizar(Rol unRol, DataTable Permisos, int IdGrupo, String Descripcion, out string Mensaje)
        {
            return objcd_rol.Actualizar(unRol, Permisos, IdGrupo, Descripcion, out Mensaje);
        }

        public bool Eliminar(int IdRol, out string Mensaje)
        {
            return objcd_rol.Eliminar(IdRol, out Mensaje);
        }
    }
}
