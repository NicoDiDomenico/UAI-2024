using DAO;
using Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        public bool Registrar(Maquina maquina, out string mensaje)
        {
            return objcd_Maquina.Registrar(maquina, out mensaje);
        }
        public bool Editar(Maquina maquina, out string mensaje)
        {
            return objcd_Maquina.Editar(maquina, out mensaje);
        }
        public bool Eliminar(int IdElemento, out string mensaje)
        {
            return objcd_Maquina.Eliminar(IdElemento, out mensaje);
        }
    }
}
