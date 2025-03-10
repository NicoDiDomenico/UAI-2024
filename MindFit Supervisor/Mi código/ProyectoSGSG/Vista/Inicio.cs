using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Modelo;
using Controlador;
using System.IO;
using System.Drawing.Drawing2D;

namespace Vista
{
    public partial class Inicio : Form
    {
        #region "Variables"
        private static Usuario usuarioActual; // La variable usuarioActual es compartida por todas las instancias de la clase Inicio.
        private static IconMenuItem MenuActivo = null;
        private static Form formularioActivo = null;
        #endregion

        #region "Métodos"
        private void PersonalizarFormulario(Form formulario)
        {
            formulario.TopLevel = false; // Indica que el formulario no es una ventana de nivel superior. Esto es necesario para poder insertar el formulario dentro de otro contenedor (como un panel).
            formulario.FormBorderStyle = FormBorderStyle.None; // Quita el borde del formulario para que se vea integrado en el contenedor sin la barra de título ni los bordes.
            formulario.Dock = DockStyle.Fill; // Hace que el formulario ocupe todo el espacio disponible en el contenedor.
            formulario.BackColor = Color.SteelBlue; // Cambia el color de fondo del formulario al color SteelBlue.
        }

        private void AbrirFormulario(IconMenuItem menu, Form formulario)
        {
            if (MenuActivo != null)
            {
                MenuActivo.BackColor = ColorTranslator.FromHtml("#2C4C7F");
            }

            menu.BackColor = Color.SteelBlue;
            menu.ForeColor = ColorTranslator.FromHtml("#FFFFFF");

            MenuActivo = menu;

            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }

            formularioActivo = formulario;

            PersonalizarFormulario(formulario);
            
            botones.Visible = false;
            contenedor.Controls.Add(formulario);

            formulario.Show();
        }

        private void validarPermisos(List<Permiso> listaPermisos)
        {
            // 🔹 Deshabilitar y cambiar color en el MenuStrip (botones.Items)
            foreach (IconMenuItem iconMenu in botones.Items)
            {
                bool encontrado = listaPermisos.Any(m => m.NombreMenu == iconMenu.Name);

                if (encontrado == false)
                {
                    iconMenu.Enabled = false;
                    iconMenu.BackColor = Color.Gainsboro;
                }
            }

            // 🔹 Deshabilitar y cambiar color en el otro MenuStrip (botonesTop.Items)
            foreach (IconMenuItem iconMenu in botonesTop.Items)
            {
                // 🔥 Eliminar "Top" del nombre antes de comparar
                string nombreNormalizado = iconMenu.Name.Replace("Top", "");

                bool encontrado = listaPermisos.Any(m => m.NombreMenu == nombreNormalizado);

                if (!encontrado)
                {
                    iconMenu.Enabled = false;
                    iconMenu.BackColor = Color.Gainsboro;
                }
            }
        }
        public Image ByteToImage(byte[] imageBytes)
        {
            // Crea un flujo de memoria
            MemoryStream ms = new MemoryStream();

            // Escribe los bytes de la imagen en el flujo de memoria
            ms.Write(imageBytes, 0, imageBytes.Length);

            // Crea un objeto Bitmap a partir del flujo de memoria
            Image image = new Bitmap(ms);

            // Retorna la imagen convertida
            return image;
        }
        #endregion

        public Inicio(Usuario ousuario)
        {
            usuarioActual = ousuario;
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            // Variable para verificar si se obtuvo correctamente el logo
            bool obtenido = true;

            // Llama al método ObtenerLogo de la capa de negocio para recuperar el logo de la base de datos
            byte[] byteimage = new ControladorGymGimnasio().ObtenerLogo(out obtenido);

            // Si el logo se obtuvo correctamente, lo convierte a imagen y lo muestra en el PictureBox
            if (obtenido)
            {
                picLogoInicio.Image = ByteToImage(byteimage);
            }

            Gimnasio gym = new ControladorGymGimnasio().ObtenerDatos();

            lblLogo.Text = gym.NombreGimnasio;
            List<Permiso> listaPermisos = new ControladorGymPermiso().Listar(usuarioActual.IdUsuario);

            validarPermisos(listaPermisos);
            lblUsuario.Text = usuarioActual.NombreYApellido;
        }
        // Gestionar Rutinas
        private void menuTopGestionarRutinas_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmGestionarRutinas());
        }

        private void menuGestionarRutinas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuTopGestionarRutinas, new frmGestionarRutinas());
        }

        // Ver Socios
        private void menuTopSocios_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmSocios());
        }

        private void menuSocios_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuTopSocios, new frmSocios());
        }

        // Gestionar Gimnasio
        private void menuTopGestionarGimnasio_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmGestionarGimnasio(usuarioActual));
        }

        private void menuGestionarGimnasio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuTopGestionarGimnasio, new frmGestionarGimnasio(usuarioActual));
        }

        private void picLogo_Click(object sender, EventArgs e)
        {
            if (formularioActivo != null)
            {
                formularioActivo.Close();
                botones.Visible = true;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            frmValidarIngreso formulario = new frmValidarIngreso();
            formulario.ShowDialog(); // Muestra el formulario en modo modal
        }
    }
}
