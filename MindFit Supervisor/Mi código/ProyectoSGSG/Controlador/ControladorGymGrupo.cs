using DAO;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ControladorGymGrupo
    {
        private GrupoDAO objcd_grupo = new GrupoDAO();

        public List<Grupo> Listar()
        {
            return objcd_grupo.Listar();
        }
    }
}
