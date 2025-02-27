using CapaEntidad;
using CapaNegocio;
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

namespace CapaPresentacion
{
    public partial class frmNegocio : Form
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

        public frmNegocio()
        {
            InitializeComponent();
        }

        // Evento que se ejecuta cuando el formulario frmNegocio se carga
        private void frmNegocio_Load(object sender, EventArgs e)
        {
            // Variable para verificar si se obtuvo correctamente el logo
            bool obtenido = true;

            // Llama al método ObtenerLogo de la capa de negocio para recuperar el logo de la base de datos
            byte[] byteimage = new CN_Negocio().ObtenerLogo(out obtenido);

            // Si el logo se obtuvo correctamente, lo convierte a imagen y lo muestra en el PictureBox
            if (obtenido)
                picLogo.Image = ByteToImage(byteimage);

            // Crea una instancia de la clase de negocio y obtiene los datos del negocio desde la base de datos
            Negocio datos = new CN_Negocio().ObtenerDatos();

            // Asigna los valores obtenidos a los cuadros de texto correspondientes
            txtNombre.Text = datos.Nombre;     // Muestra el nombre del negocio
            txtRuc.Text = datos.RUC;           // Muestra el RUC del negocio
            txtDireccion.Text = datos.Direccion; // Muestra la dirección del negocio
        }

        // Evento que se ejecuta cuando el botón "Subir" es presionado
        private void btnSubir_Click(object sender, EventArgs e)
        {
            // Variable para almacenar el mensaje de éxito o error
            string mensaje = string.Empty;

            // Se crea un cuadro de diálogo para seleccionar un archivo
            OpenFileDialog oOpenFileDialog = new OpenFileDialog();

            // Se especifican los tipos de archivos permitidos (imágenes en formato JPG y PNG)
            oOpenFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png";

            // Se muestra el cuadro de diálogo y se verifica si el usuario seleccionó un archivo
            if (oOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Se lee el archivo seleccionado y se almacena en un array de bytes
                byte[] byteimage = File.ReadAllBytes(oOpenFileDialog.FileName);

                // Se llama a la función para actualizar el logo en la base de datos
                // Se pasa el array de bytes y se captura el mensaje de respuesta
                bool respuesta = new CN_Negocio().ActualizarLogo(byteimage, out mensaje);

                // Si la actualización fue exitosa, se convierte la imagen en bytes a una imagen visible
                if (respuesta)
                    picLogo.Image = ByteToImage(byteimage);
                else
                    // Si hubo un error, se muestra un mensaje con el contenido de "mensaje"
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Variable para almacenar el mensaje de éxito o error
            string mensaje = string.Empty;

            // Se crea un nuevo objeto de tipo "Negocio" con los valores ingresados en los campos de texto
            Negocio obj = new Negocio()
            {
                Nombre = txtNombre.Text,  // Se obtiene el texto del campo "Nombre"
                RUC = txtRuc.Text,        // Se obtiene el texto del campo "RUC"
                Direccion = txtDireccion.Text // Se obtiene el texto del campo "Dirección"
            };

            // Se llama al método "GuardarDatos" de la clase CN_Negocio para guardar los datos en la base de datos
            // Se pasa el objeto con la información y se captura un mensaje de respuesta
            bool respuesta = new CN_Negocio().GuardarDatos(obj, out mensaje);

            // Se verifica si la operación fue exitosa
            if (respuesta)
                // Se muestra un mensaje de éxito si los datos fueron guardados correctamente
                MessageBox.Show("Los cambios fueron guardados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                // Si hubo un error, se muestra un mensaje indicando que los cambios no se pudieron guardar
                MessageBox.Show("No se pudo guardar los cambios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
