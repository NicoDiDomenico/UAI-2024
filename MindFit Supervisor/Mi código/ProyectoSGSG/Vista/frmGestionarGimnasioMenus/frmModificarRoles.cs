using Controlador;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class frmModificarRoles : Form
    {
        #region "Métodos"
        private void limpiarCampos()
        {/*
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtNombreYApellido.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            txtCiudad.Text = "";
            txtNroDocumento.Text = "";
            dtpFechaNacimiento.Value = DateTime.Today;
            txtNombreUsuario.Text = "";
            txtClave.Text = "";

            cboGenero.SelectedIndex = -1;
            cboRol.SelectedIndex = -1;
            cboEstado.SelectedIndex = -1;

            txtNombreYApellido.Select();
            */
        }
        private void cargarGrid()
        {
            List<Rol> roles = new ControladorGymRol().Listar();

            foreach (Rol item in roles)
            {
                dgvData.Rows.Add(new object[] {
                "",
                item.IdRol,
                item.Descripcion,
                item.FechaRegistro
                });
            }
        }
        #endregion
        public frmModificarRoles()
        {
            InitializeComponent();
        }

        private void frmModificarRoles_Load(object sender, EventArgs e)
        {
            cargarGrid();
        }
    }
}
