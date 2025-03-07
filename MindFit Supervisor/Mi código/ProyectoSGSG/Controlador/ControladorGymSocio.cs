using DAO;
using Modelo;
using System;
using System.Collections.Generic;

namespace Controlador
{
    public class ControladorGymSocio
    {
        private SocioDAO objcd_Socio = new SocioDAO();

        public List<Socio> Listar()
        {
            return objcd_Socio.Listar();
        }

        public int Registrar(Socio unSocio, out string mensaje)
        {
            mensaje = string.Empty;

            // Asignar la fecha de inicio de actividades (RN01)
            unSocio.FechaInicioActividades = DateTime.Now.Date;

            // Aplicar RN02: Calcular FechaFinActividades según el plan seleccionado
            if (unSocio.Plan == "Mensual")
            {
                unSocio.FechaFinActividades = unSocio.FechaInicioActividades.Value.AddDays(30); // Suma 30 días
                unSocio.FechaNotificacion = unSocio.FechaInicioActividades.Value.AddDays(29);
            }
            else if (unSocio.Plan == "Anual")
            {
                unSocio.FechaFinActividades = unSocio.FechaInicioActividades.Value.AddDays(365); // Suma 365 días
                unSocio.FechaNotificacion = unSocio.FechaInicioActividades.Value.AddDays(364); // Suma 365 días
            }
            else
            {
                mensaje = "El plan seleccionado no es válido.";
                return 0; // Indica error
            }

            // RN3 --> Nuevo    
            unSocio.EstadoSocio = "Nuevo";

            // Aquí se debería llamar a la Capa de Datos para registrar el socio
            return objcd_Socio.Registrar(unSocio, out mensaje);
        }
    }
}

