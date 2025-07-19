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

using Controlador;

using Modelo;
using Vista;

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        #region "Metodos"
        // Este metodo no pertenece al formulario actual, sino que es un metodo cuyo objetivo es asignarle un evento al formulario de inicio
        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            txtNombreUsuario.Clear();
            txtClave.Clear();

            this.Show();
        }

        private void ingresoUsuario()
        {
            if (txtNombreUsuario.Text.Trim() != "" && txtClave.Text.Trim() != "")
            {
                Usuario unUsuario = (((new ControladorGymUsuario()).Listar()).Where(u => u.NombreUsuario == (txtNombreUsuario.Text).Trim() && u.Clave == (txtClave.Text).Trim())).FirstOrDefault();

                if (unUsuario != null)
                {
                    Inicio form = new Inicio(unUsuario);

                    form.Show();
                    this.Hide();

                    form.FormClosing += frm_closing; // Cada vez que el formulario Inicio se esté por cerrar (cuando el usuario haga clic en la "X" o cierre la ventana), se ejecutará el método Login_FormClosing.
                }
                else
                {
                    MessageBox.Show("No se encontró el usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            } else
            {
                MessageBox.Show("Ingrese un usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

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
            ingresoUsuario();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void txtClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Verifica si se presionó Enter
            {
                ingresoUsuario();
            }
        }

        private void txtNombreUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Verifica si se presionó Enter
            {
                ingresoUsuario();
            }
        }

        private void liblRecuperar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRecuperar form = new frmRecuperar();

            form.Show();
            this.Hide();

            form.FormClosing += frm_closing;
        }

        private void txtNombreUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtClave_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
