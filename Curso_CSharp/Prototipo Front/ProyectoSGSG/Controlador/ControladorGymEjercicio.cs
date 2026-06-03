using DAO;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ControladorGymEjercicio
    {
        private EjercicioDAO objcd_Ejercicio = new EjercicioDAO();

        public List<Ejercicio> Listar()
        {
            return objcd_Ejercicio.Listar();
        }

        public bool Registrar(Ejercicio ejercicio, out string mensaje)
        {
            return objcd_Ejercicio.Registrar(ejercicio, out mensaje);
        }
        public bool Editar(Ejercicio ejercicio, out string mensaje)
        {
            return objcd_Ejercicio.Editar(ejercicio, out mensaje);
        }
        public bool Eliminar(int IdElemento, out string mensaje)
        {
            return objcd_Ejercicio.Eliminar(IdElemento, out mensaje);
        }
    }
}
