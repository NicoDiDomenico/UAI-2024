using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

using Modelo;

namespace Controlador
{
    public class ControladorGymCalentamiento
    {
        private CalentamientoDAO objcd_Calentamiento = new CalentamientoDAO();

        public List<Calentamiento> Listar()
        {
            return objcd_Calentamiento.Listar();
        }
        public bool Registrar(Calentamiento calentamiento, out string mensaje)
        {
            return objcd_Calentamiento.Registrar(calentamiento, out mensaje);
        }
        public bool Editar(Calentamiento calentamiento, out string mensaje)
        {
            return objcd_Calentamiento.Editar(calentamiento, out mensaje);
        }

        public bool Eliminar(int idCalentamiento, out string mensaje)
        {
            return objcd_Calentamiento.Eliminar(idCalentamiento, out mensaje);
        }

        public List<RutinaCalentamiento> ListarCalentamientosPorRutina(int IdRutina)
        {
            return objcd_Calentamiento.ListarCalentamientosPorRutina(IdRutina);
        }
    }
}
