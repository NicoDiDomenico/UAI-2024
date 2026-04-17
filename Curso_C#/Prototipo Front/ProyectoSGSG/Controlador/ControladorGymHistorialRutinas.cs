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
    public class ControladorGymHistorialRutinas
    {
        private HistorialRutinaDAO daoHistorialRutina = new HistorialRutinaDAO();
        
        public List<Rutina> Listar(int idSocioSeleccionado, string diaActual)
        {
            List<Rutina> rutinas = daoHistorialRutina.Listar(idSocioSeleccionado, diaActual);

            return rutinas;
        }

        public int CrearHistorialRutina(int idSocio, string dia, out string mensaje)
        {
            mensaje = "";

            try
            {
                return daoHistorialRutina.CrearHistorialRutina(idSocio, dia, out mensaje);
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado: " + ex.Message;
                return 0;
            }
        }

        public bool GuardarHistorialCalentamientos(int idHistorial, List<RutinaCalentamiento> lista, out string mensaje)
        {
            mensaje = "";

            try
            {
                return daoHistorialRutina.GuardarHistorialCalentamientos(idHistorial, lista, out mensaje);
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado: " + ex.Message;
                return false;
            }
        }

        public bool GuardarHistorialEntrenamientos(int idHistorial, List<Entrenamiento> lista, out string mensaje)
        {
            mensaje = "";

            try
            {
                return daoHistorialRutina.GuardarHistorialEntrenamientos(idHistorial, lista, out mensaje);
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado: " + ex.Message;
                return false;
            }
        }

        public bool GuardarHistorialEstiramientos(int idHistorial, List<RutinaEstiramiento> lista, out string mensaje)
        {
            mensaje = "";

            try
            {
                return daoHistorialRutina.GuardarHistorialEstiramientos(idHistorial, lista, out mensaje);
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado: " + ex.Message;
                return false;
            }
        }

        public Dictionary<string, int> ObtenerCantidadModificacionesPorDia(int idSocio)
        {
            return daoHistorialRutina.ObtenerCantidadModificacionesPorDia(idSocio);
        }
    }
}
