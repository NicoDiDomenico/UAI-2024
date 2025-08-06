using DAO;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ControladorGymElementoGimnasio
    {
        private ElementoGimnasioDAO objcd_ElementoGimnasio = new ElementoGimnasioDAO();

        public List<ElementoGimnasio> Listar()
        {
            return objcd_ElementoGimnasio.Listar();
        }
    }
}
