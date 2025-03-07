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
    public partial class frmEditarSocio : Form
    {
        #region "Variables"
        private static int idSocioSeleccionado; // Con esto hago una consulta a la BD y cargo el Socio en los campos
        #endregion

        public frmEditarSocio(int idSocio)
        {
            idSocioSeleccionado = idSocio;
            InitializeComponent();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK; // Indica que la acción fue confirmada
            this.Close(); // Cierra el formulario
        }

        private void chkMensual_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBoxSeleccionado && checkBoxSeleccionado.Checked)
            {
                // Recorre todos los CheckBox en el mismo contenedor (Panel, GroupBox, etc.)
                foreach (Control control in checkBoxSeleccionado.Parent.Controls)
                {
                    if (control is CheckBox checkBox && checkBox != checkBoxSeleccionado)
                    {
                        checkBox.Checked = false; // Desmarca los demás
                    }
                }
            }
        }

        private void chkAnual_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBoxSeleccionado && checkBoxSeleccionado.Checked)
            {
                // Recorre todos los CheckBox en el mismo contenedor (Panel, GroupBox, etc.)
                foreach (Control control in checkBoxSeleccionado.Parent.Controls)
                {
                    if (control is CheckBox checkBox && checkBox != checkBoxSeleccionado)
                    {
                        checkBox.Checked = false; // Desmarca los demás
                    }
                }
            }
        }
    }
}
