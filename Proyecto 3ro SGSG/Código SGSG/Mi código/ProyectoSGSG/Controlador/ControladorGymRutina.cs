using DAO;
using Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ControladorGymRutina
    {
        private RutinaDAO daoRutina = new RutinaDAO();
        public List<Rutina> Listar(int idSocioSeleccionado)
        {
            List<Rutina> rutinas = daoRutina.Listar(idSocioSeleccionado);

            return rutinas;
        }

        public bool GuardarCalentamientos(List<RutinaCalentamiento> lista, out string mensaje)
        {
            mensaje = "";

            try
            {
                return new RutinaDAO().GuardarCalentamientos(lista, out mensaje);
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado: " + ex.Message;
                return false;
            }
        }

        public bool GuardarEntrenamientos(List<Entrenamiento> lista, out string mensaje)
        {
            mensaje = "";

            try
            {
                return new RutinaDAO().GuardarEntrenamientos(lista, out mensaje);
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado: " + ex.Message;
                return false;
            }
        }

        public bool GuardarEstiramientos(List<RutinaEstiramiento> lista, out string mensaje)
        {
            mensaje = "";

            try
            {
                return new RutinaDAO().GuardarEstiramientos(lista, out mensaje);
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado: " + ex.Message;
                return false;
            }
        }

        public bool CambiarEstadoRutina(int idRutina)
        {
            try
            {
                return new RutinaDAO().ActualizarFechaModificacion(idRutina);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DesactivarRutina(int idRutina)
        {
            try
            {
                return new RutinaDAO().DesactivarRutina(idRutina);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TieneRutinaActivaEnDia(int idSocio, string dia)
        {
            return daoRutina.TieneRutinaActivaEnDia(idSocio, dia);
        }
    }
}
