using Controlador;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class frmRecuperar : Form
    {
        #region "Variables"
        public Usuario usuarioActual = null;
        private string codigoVerificacionGenerado = "";
        private string clavePendiente = "";
        #endregion
        #region "Metodos"
        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            txtNombreUsuario.Clear();
            txtClave.Clear();

            this.Show();
        }

        private void ingresoUsuario()
        {
            string correo = txtCorreo.Text.Trim();
            if (correo != "")
            {
                Usuario unUsuario = (((new ControladorGymUsuario()).ListarPorCorreo(correo)).Where(u => u.Email == (txtCorreo.Text).Trim())).FirstOrDefault();

                if (unUsuario != null)
                {
                    txtCorreo.Enabled = false;
                    txtClave.Enabled = true;
                    txtRepetir.Enabled = true;

                    txtNombreUsuario.Text = unUsuario.NombreUsuario;
                    usuarioActual = unUsuario;

                    txtClave.Select();
                }
                else
                {
                    MessageBox.Show("No se encontró el correo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Ingrese un correo válido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public static class CorreoHelper
        {
            public static bool EnviarCorreo(string destino, string asunto, string cuerpo)
            {
                try
                {
                    // Obtener el correo del gimnasio
                    Gimnasio datosGym = new ControladorGymGimnasio().ObtenerDatos();
                    string correoOrigen = datosGym.Email;

                    // Clave SMTP fija (en código)
                    string claveOrigen = "lmbc sqfz fijj vhir"; // tu app password

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
                catch (Exception ex)
                {
                    MessageBox.Show("Error al enviar correo:\n" + ex.Message, "Error SMTP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
        #endregion

        public frmRecuperar()
        {
            InitializeComponent();
        }

        private void frmRecuperar_Load(object sender, EventArgs e)
        {
            gbCorreo.Visible = false;
            txtCorreo.Enabled = true;
            txtNombreUsuario.Enabled = false;
            txtClave.Enabled = false;
            txtRepetir.Enabled = false;
        }

        private void txtCorreo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ingresoUsuario();
                e.SuppressKeyPress = true; // (opcional) evita que se emita el sonido "ding"
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRecuperar_Click(object sender, EventArgs e)
        {
            string nuevaClave = txtClave.Text.Trim();
            string repetirClave = txtRepetir.Text.Trim();

            if (nuevaClave == "" || repetirClave == "")
            {
                MessageBox.Show("Debe completar ambos campos de contraseña", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nuevaClave != repetirClave)
            {
                MessageBox.Show("Las contraseñas no coinciden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (usuarioActual == null)
            {
                MessageBox.Show("No hay un usuario cargado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Generar código aleatorio
            Random rand = new Random();
            codigoVerificacionGenerado = rand.Next(100000, 999999).ToString(); // Ej: 6 dígitos
            clavePendiente = nuevaClave;

            // Enviar correo
            string asunto = "Verificación de cambio de contraseña";
            string cuerpo = $"Hola {usuarioActual.NombreYApellido},\n\nTu código de verificación es: {codigoVerificacionGenerado}\n\nSi no pediste este cambio, ignorá este mensaje.";

            bool enviado = CorreoHelper.EnviarCorreo(usuarioActual.Email, asunto, cuerpo); // Usá tu clase de envío

            if (enviado)
            {
                MessageBox.Show("Se ha enviado un código de verificación al correo.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                gbCorreo.Visible = true;

                txtCodigoVerificacion.Enabled = true;
                btnConfirmarCodigo.Enabled = true;
                txtCodigoVerificacion.Focus();
            }
            else
            {
                MessageBox.Show("No se pudo enviar el correo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gbCorreo.Visible = false;
            }
        }

        private void btnConfirmarCodigo_Click(object sender, EventArgs e)
        {
            string codigoIngresado = txtCodigoVerificacion.Text.Trim();

            if (codigoIngresado == "")
            {
                MessageBox.Show("Debe ingresar el código recibido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (codigoIngresado == codigoVerificacionGenerado)
            {
                bool actualizado = new ControladorGymUsuario().CambiarClave(usuarioActual.IdUsuario, clavePendiente);

                if (actualizado)
                {
                    MessageBox.Show("La contraseña ha sido actualizada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gbCorreo.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("El código ingresado es incorrecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gbCorreo.Visible = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            gbCorreo.Visible = false;
        }
    }
}
