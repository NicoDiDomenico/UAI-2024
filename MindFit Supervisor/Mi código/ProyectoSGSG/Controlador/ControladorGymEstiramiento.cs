using DAO;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ControladorGymEstiramiento
    {
        private EstiramientoDAO objcd_Estiramiento = new EstiramientoDAO();

        public List<Estiramiento> Listar()
        {
            return objcd_Estiramiento.Listar();
        }
        public bool Registrar(Estiramiento estiramiento, out string mensaje)
        {
            return objcd_Estiramiento.Registrar(estiramiento, out mensaje);
        }
        public bool Editar(Estiramiento estiramiento, out string mensaje)
        {
            return objcd_Estiramiento.Editar(estiramiento, out mensaje);
        }

        public bool Eliminar(int idEstiramiento, out string mensaje)
        {
            return objcd_Estiramiento.Eliminar(idEstiramiento, out mensaje);
        }

        public List<RutinaEstiramiento> ListarEstiramientosPorRutina(int IdRutina)
        {
            return objcd_Estiramiento.ListarEstiramientosPorRutina(IdRutina);
        }
    }
}
