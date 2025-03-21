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
            List <Socio> socios= objcd_Socio.Listar();

            foreach (Socio socio in socios)
            {
                if (socio.FechaFinActividades <= DateTime.Now.Date) socio.EstadoSocio = "Suspendido";
            }

            return socios;
        }
        
        public Socio GetSocio(int id)
        {
            return objcd_Socio.GetSocio(id);
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

        public Boolean Actualizar(Socio unSocio, out string mensaje)
        {
            mensaje = string.Empty;

            return objcd_Socio.Actualizar(unSocio, out mensaje);
        }

        public bool Eliminar(Socio obj, out string Mensaje)
        {
            return objcd_Socio.Eliminar(obj, out Mensaje);
        }
        
        public List<Socio> ListarSociosActuales(int id, int idRangoHorarioActual)
        {
            return objcd_Socio.ListarSociosActuales(id, idRangoHorarioActual);
        }
    }
}

