using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.TextFormatting;

using Modelo;
using Controlador;

namespace Vista
{
    public partial class frmMenuNegocio : Form
    {
        #region "Métodos"
        // Método que convierte un array de bytes en una imagen
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
        public frmMenuNegocio()
        {
            InitializeComponent();
        }

        private void frmMenuNegocio_Load(object sender, EventArgs e)
        {
            bool obtenido = true;

            byte[] byteimage = new ControladorGymGimnasio().ObtenerLogo(out obtenido);

            if (obtenido)
            {
                picLogo.Image = ByteToImage(byteimage);
            }

            Gimnasio datos = new ControladorGymGimnasio().ObtenerDatos();

            txtNombre.Text = datos.NombreGimnasio;
            txtTelefono.Text = datos.Telefono;
            txtDireccion.Text = datos.Direccion;
        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            OpenFileDialog oOpenFileDialog = new OpenFileDialog();

            oOpenFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png";

            if (oOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] byteimage = File.ReadAllBytes(oOpenFileDialog.FileName);

                bool respuesta = new ControladorGymGimnasio().ActualizarLogo(byteimage, out mensaje);

                if (respuesta)
                    picLogo.Image = ByteToImage(byteimage);
                else
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Gimnasio obj = new Gimnasio()
            {
                NombreGimnasio = txtNombre.Text, 
                Telefono = txtTelefono.Text,      
                Direccion = txtDireccion.Text 
            };

            bool respuesta = new ControladorGymGimnasio().GuardarDatos(obj, out mensaje);

            if (respuesta)
                MessageBox.Show("Los cambios fueron guardados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo guardar los cambios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
