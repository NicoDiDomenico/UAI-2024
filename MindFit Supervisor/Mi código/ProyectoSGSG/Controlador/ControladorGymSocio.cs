using DAO;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Controlador
{
    public class ControladorGymSocio
    {
        private SocioDAO objcd_Socio = new SocioDAO();

        public List<Socio> Listar()
        {
            List<Socio> socios = objcd_Socio.Listar();

            foreach (Socio socio in socios)
            {
                // 👉 Evitar forzar el estado si ya es Eliminado
                if (socio.EstadoSocio == "Eliminado")
                    continue;

                if (socio.FechaFinActividades <= DateTime.Now.Date)
                {
                    if (socio.FechaFinActividades.Value.AddDays(30) <= DateTime.Now.Date)
                    {
                        socio.EstadoSocio = "Eliminado";
                        objcd_Socio.ActualizarEstadoSocio(socio.IdSocio, socio.EstadoSocio);
                    }
                    else
                    {
                        socio.EstadoSocio = "Suspendido";
                        objcd_Socio.ActualizarEstadoSocio(socio.IdSocio, socio.EstadoSocio);
                    }
                }
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

        public bool RevertirAEstadoSuspendido(Socio socio, out string mensaje)
        {
            mensaje = "";

            // Si ya está eliminado, verificar si se puede volver a suspender
            if (socio.EstadoSocio == "Eliminado")
            {
                if (socio.FechaFinActividades.HasValue && socio.FechaFinActividades.Value.AddDays(30) > DateTime.Now.Date)
                {
                    // Todavía no pasaron 30 días desde el vencimiento → se puede suspender
                    socio.EstadoSocio = "Suspendido";
                    bool actualizado = new SocioDAO().ActualizarEstadoSocio(socio.IdSocio, "Suspendido");
                    mensaje = actualizado ? "Estado cambiado a Suspendido." : "No se pudo actualizar el estado.";
                    return actualizado;
                }
                else
                {
                    // Pasaron más de 30 días → no se puede volver a suspender
                    mensaje = "No se puede cambiar a estado Suspendido porque pasaron más de 30 días desde el vencimiento. El socio debe renovar la cuota.";
                    return false;
                }
            }

            // Si no está eliminado, no hace falta revertir
            mensaje = "El socio no está en estado Eliminado, no es necesario revertir.";
            return false;
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

        public bool ActualizarEstadoSocio(Socio unSocio, string nuevoEstado)
        {
            return objcd_Socio.ActualizarEstadoSocio(unSocio.IdSocio, nuevoEstado);
        }

        public List<Socio> ListarSociosInactivos(int dias)
        {
            return objcd_Socio.ListarSociosInactivos(dias);
        }

        public Dictionary<string, int> ContarSociosPorEstado()
        {
            return Listar()
                .GroupBy(s => s.EstadoSocio)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}

