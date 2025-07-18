using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Modelo;

namespace Controlador.Observer
{
    public class EmailObserver : ICuotaObserver
    {
        public void NotificarRenovacion(Socio socio)
        {
            string asunto = "Renovación de Cuota";
            string cuerpo = $"Hola {socio.NombreYApellido},\n\nTu cuota ha sido renovada exitosamente. Tu nuevo vencimiento es el día {socio.FechaFinActividades?.ToString("dd/MM/yyyy")}.\n\nGracias por seguir entrenando con nosotros.";
            string destino = socio.Email;

            EmailUtilidades.EnviarCorreo(destino, asunto, cuerpo);
        }
    }
}
