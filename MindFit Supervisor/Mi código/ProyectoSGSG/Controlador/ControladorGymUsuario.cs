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
    }
}
