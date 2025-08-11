using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Modelo;
using DotNetEnv;

namespace Controlador.Observer
{
    public static class EmailUtilidades
    {
        public static bool EnviarCorreo(string destino, string asunto, string cuerpo)
        {
            try
            {
                // Cargar variables desde .env
                Env.Load("../../../Vista/Resources/.env");

                // Obtener correo del gimnasio desde BD
                var datosGym = new Controlador.ControladorGymGimnasio().ObtenerDatos();
                string correoOrigen = datosGym.Email;

                // Leer clave de entorno
                string claveOrigen = Environment.GetEnvironmentVariable("CLAVE_CORREO_GYM");
                if (string.IsNullOrWhiteSpace(claveOrigen))
                    throw new InvalidOperationException("La variable 'CLAVE_CORREO_GYM' no está definida en el archivo .env.");

                // Leer host SMTP
                string smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST");
                if (string.IsNullOrWhiteSpace(smtpHost))
                    throw new InvalidOperationException("La variable 'SMTP_HOST' no está definida en el archivo .env.");

                // Leer y validar puerto SMTP
                string smtpPortStr = Environment.GetEnvironmentVariable("SMTP_PORT");
                if (string.IsNullOrWhiteSpace(smtpPortStr) || !int.TryParse(smtpPortStr, out int smtpPort))
                    throw new InvalidOperationException("La variable 'SMTP_PORT' no está definida correctamente en el archivo .env.");

                // Crear mensaje
                MailMessage mensaje = new MailMessage();
                mensaje.To.Add(destino);
                mensaje.Subject = asunto;
                mensaje.Body = cuerpo;
                mensaje.From = new MailAddress(correoOrigen);
                mensaje.IsBodyHtml = false;

                // Configurar cliente SMTP
                SmtpClient smtp = new SmtpClient(smtpHost, smtpPort);
                smtp.Credentials = new NetworkCredential(correoOrigen, claveOrigen);
                smtp.EnableSsl = true;

                // Enviar mensaje
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
