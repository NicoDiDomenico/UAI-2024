using DAO;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ControladorGymEquipamiento
    {
        private EquipamientoDAO objcd_Equipamiento = new EquipamientoDAO();

        public List<Equipamiento> Listar()
        {
            return objcd_Equipamiento.Listar();
        }

        public bool Registrar(Equipamiento Equipamiento, out string mensaje)
        {
            return objcd_Equipamiento.Registrar(Equipamiento, out mensaje);
        }
        public bool Editar(Equipamiento Equipamiento, out string mensaje)
        {
            return objcd_Equipamiento.Editar(Equipamiento, out mensaje);
        }
        public bool Eliminar(int IdElemento, out string mensaje)
        {
            return objcd_Equipamiento.Eliminar(IdElemento, out mensaje);
        }
    }
}
