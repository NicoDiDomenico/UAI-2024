using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Modelo;

namespace Controlador.Observer
{
    public static class EmailUtilidades
    {
        public static bool EnviarCorreo(string destino, string asunto, string cuerpo)
        {
            try
            {
                var datosGym = new Controlador.ControladorGymGimnasio().ObtenerDatos(); 
                string correoOrigen = datosGym.Email;
                string claveOrigen = "lmbc sqfz fijj vhir"; // idealmente, ocultar esto en un config seguro

                MailMessage mensaje = new MailMessage();
                mensaje.To.Add(destino);
                mensaje.Subject = asunto;
                mensaje.Body = cuerpo;
                mensaje.From = new MailAddress(correoOrigen);
                mensaje.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(correoOrigen, claveOrigen);
                smtp.EnableSsl = true;

                smtp.Send(mensaje);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
