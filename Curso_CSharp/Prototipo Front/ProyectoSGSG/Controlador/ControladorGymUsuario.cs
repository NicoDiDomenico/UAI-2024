using Modelo;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ControladorGymUsuario
    {
        private UsuarioDAO objcd_usuario = new UsuarioDAO();

        public List<Usuario> Listar()
        {
            return objcd_usuario.Listar();
        }

        public List<Usuario> ListarPorCorreo(string correo)
        {
            return objcd_usuario.ListarPorCorreo(correo);
        }
        public bool CambiarClave(int idUsuario, string nuevaClave)
        {
            return objcd_usuario.CambiarClave(idUsuario, nuevaClave);
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.NombreUsuario == "")
            {
                Mensaje += "Es necesario el Nombre de Usuario para el usuario\n";
            }

            if (obj.NombreYApellido == "")
            {
                Mensaje += "Es necesario el nombre completo del usuario\n";
            }

            if (obj.Clave == "")
            {
                Mensaje += "Es necesario la clave del usuario\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_usuario.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            List<Usuario> entrenadores = objcd_usuario.ListarUsuariosPorRangoHorario();

            foreach (Usuario ent in entrenadores)
            {
                if (obj.IdUsuario == ent.IdUsuario)
                {
                    Usuario unU = objcd_usuario.getUsuario(obj.IdUsuario);
                    // Mensaje += "No se puede editar un Entrenador que está asignado a un Rango Horario.\n";
                    if (obj.Rol.IdRol != unU.Rol.IdRol)
                    {
                        Mensaje += "No se puede cambiar el rol de un Entrenador que está asignado a un Rango Horario.\n";
                    }
                    if (obj.Estado != unU.Estado)
                    {
                        Mensaje += "No se puede cambiar el estado de un Entrenador que está asignado a un Rango Horario.\n";
                    }
                }
            }

            if (obj.NombreUsuario == "")
            {
                Mensaje += "Es necesario el Nombre de Usuario\n";
            }

            if (obj.NombreYApellido == "")
            {
                Mensaje += "Es necesario el nombre completo del usuario\n";
            }

            if (obj.Clave == "")
            {
                Mensaje += "Es necesario la clave del usuario\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_usuario.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            return objcd_usuario.Eliminar(obj, out Mensaje);
        }

        public List<Usuario> ObtenerPorRangoHorario(int idRangoHorario)
        {
            return objcd_usuario.ObtenerPorRangoHorario(idRangoHorario);
        }
    }
}
