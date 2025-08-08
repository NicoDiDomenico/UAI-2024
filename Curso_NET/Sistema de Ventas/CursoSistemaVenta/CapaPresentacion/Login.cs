using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaNegocio;
using CapaEntidad;

namespace CapaPresentacion
{
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Crear una instancia de CN_Usuario para acceder a la lista de usuarios
            /*
            //// Forma Completa:
            CN_Usuario cnUsuario = new CN_Usuario();

            // Obtener la lista de usuarios desde la base de datos
            List<Usuario> listaUsuarios = cnUsuario.Listar();

            // FILTRADO - Filtrar la lista para encontrar el usuario que coincide con el documento y la clave ingresados
            Usuario ousuario = listaUsuarios
                .Where(u => u.Documento == txtDocumento.Text && u.Clave == txtClave.Text) // El método .Where() filtra la lista de usuarios según una condición.
                .FirstOrDefault(); // FirstOrDefault() (Obtener el Primer Resultado)
            */
            //// Forma Resumida: 
            Usuario ousuario = (((new CN_Usuario()).Listar()).Where(u => u.Documento == txtDocumento.Text && u.Clave == txtClave.Text)).FirstOrDefault();
                      
            if (ousuario != null)
            {
                Inicio form = new Inicio(ousuario);

                form.Show();
                this.Hide();

                form.FormClosing += frm_closing; // Cada vez que el formulario Inicio se esté por cerrar (cuando el usuario haga clic en la "X" o cierre la ventana), se ejecutará el método Login_FormClosing.
            }
            else
            {
                MessageBox.Show("no se encontro el usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // Este metodo no pertenece al formulario actual, sino que es un metodo cuyo objetivo es asignarle un evento al formulario de inicio
        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            txtDocumento.Clear();
            txtClave.Clear();

            this.Show();
        }
    }
}
