using DAO;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ControladorGymMaquina
    {
        private MaquinaDAO objcd_Maquina = new MaquinaDAO();

        public List<Maquina> Listar()
        {
            return objcd_Maquina.Listar();
        }

        public List<Maquina> Registrar()
        {
            return objcd_Maquina.Listar();
        }
        
        public List<Maquina> Editar()
        {
            return objcd_Maquina.Listar();
        }
    }
}
