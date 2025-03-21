using CapaPresentacion.Utilidades;
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

using Vista.Utilidades;

namespace Vista
{
    public partial class frmAgregarSocio : Form
    {
        #region "Métodos"
        #endregion
        #region "Métodos"
        private void limpiarCampos()
        {
            txtNombreYApellido.Text = "";
            dtpFechaNacimiento.Value = DateTime.Today;
            cboGenero.SelectedIndex = -1;
            txtNroDocumento.Text = "";
            txtCiudad.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
            txtObraSocial.Text = "";

            chkLunes.Checked = false;
            chkMartes.Checked = false;
            chkMiercoles.Checked = false;
            chkJueves.Checked = false;
            chkViernes.Checked = false;
            chkSabado.Checked = false;

            chkMensual.Checked = false;
            chkAnual.Checked = false;

            txtNombreYApellido.Select();
        }

        private void validarCamposSocio()
        {
            // Validación de Nombre y Apellido
            if (string.IsNullOrWhiteSpace(txtNombreYApellido.Text))
            {
                MessageBox.Show("Ingrese nombre y apellido del socio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validación de Fecha de Nacimiento (no puede ser una fecha futura)
            if (dtpFechaNacimiento.Value.Date > DateTime.Today)
            {
                MessageBox.Show("La fecha de nacimiento no puede ser una fecha futura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validación de Género
            if (cboGenero.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un género.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validación de Número de Documento
            if (!int.TryParse(txtNroDocumento.Text, out int nroDocumento))
            {
                MessageBox.Show("Ingrese un número de documento válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validación de Ciudad
            if (string.IsNullOrWhiteSpace(txtCiudad.Text))
            {
                MessageBox.Show("Ingrese la ciudad del socio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validación de Dirección
            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("Ingrese la dirección del socio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validación de Teléfono (debe contener solo números)
            if (string.IsNullOrWhiteSpace(txtTelefono.Text) || !txtTelefono.Text.All(char.IsDigit))
            {
                MessageBox.Show("Ingrese un número de teléfono válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validación de Email con formato correcto
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Ingrese un correo electrónico válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        #endregion

        public frmAgregarSocio()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            validarCamposSocio();

            // Validación del plan
            string planElegido = "";

            if (chkMensual.Checked || chkAnual.Checked)
            {
                if (chkMensual.Checked)
                {
                    planElegido = chkMensual.Text;
                }
                if (chkAnual.Checked)
                {
                    planElegido = chkAnual.Text;
                }
            }
            else
            {
                MessageBox.Show("Se debe seleccionar un plan", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Detiene la ejecución
            }

            if (cboGenero.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un género.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtNroDocumento.Text, out int nroDocumento))
            {
                MessageBox.Show("Ingrese un número de documento válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Socio socio = new Socio()
            {
                NombreYApellido = txtNombreYApellido.Text,
                FechaNacimiento = dtpFechaNacimiento.Value,
                Genero = Convert.ToInt64(((OpcionCombo)cboGenero.SelectedItem).Valor) == 1 ? "Femenino" : "Masculino",
                NroDocumento = Convert.ToInt32(txtNroDocumento.Text),
                Ciudad = txtCiudad.Text,
                Direccion = txtDireccion.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
                ObraSocial = txtObraSocial.Text,
                Plan = planElegido
            };

            // Validación de días seleccionados
            List<Rutina> rutinas = new List<Rutina>();

            if (chkLunes.Checked || chkMartes.Checked || chkMiercoles.Checked || chkJueves.Checked || chkViernes.Checked || chkSabado.Checked)
            {
                if (chkLunes.Checked)
                {
                    Rutina rutinaDia = new Rutina()
                    {
                        Dia = chkLunes.Text,
                        FechaModificacion = DateTime.Now
                    };
                    rutinas.Add(rutinaDia);
                }
                if (chkMartes.Checked)
                {
                    Rutina rutinaDia = new Rutina()
                    {
                        Dia = chkMartes.Text,
                        FechaModificacion = DateTime.Now
                    };
                    rutinas.Add(rutinaDia);
                }
                if (chkMiercoles.Checked)
                {
                    Rutina rutinaDia = new Rutina()
                    {
                        Dia = chkMiercoles.Text,
                        FechaModificacion = DateTime.Now
                    };
                    rutinas.Add(rutinaDia);
                }
                if (chkJueves.Checked)
                {
                    Rutina rutinaDia = new Rutina()
                    {
                        Dia = chkJueves.Text,
                        FechaModificacion = DateTime.Now
                    };
                    rutinas.Add(rutinaDia);
                }
                if (chkViernes.Checked)
                {
                    Rutina rutinaDia = new Rutina()
                    {
                        Dia = chkViernes.Text,
                        FechaModificacion = DateTime.Now
                    };
                    rutinas.Add(rutinaDia);
                }
                if (chkSabado.Checked)
                {
                    Rutina rutinaDia = new Rutina()
                    {
                        Dia = chkSabado.Text,
                        FechaModificacion = DateTime.Now
                    };
                    rutinas.Add(rutinaDia);
                }

                socio.Rutinas = rutinas;
            } else
            {
                MessageBox.Show("Se debe seleccionar al menos un día en el que asistirá el socio al gimnasio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            string mensaje = string.Empty;

            int idUsuarioGenerado = new ControladorGymSocio().Registrar(socio, out mensaje);

            if (idUsuarioGenerado != 0)
            {
                MessageBox.Show(mensaje, "Confirmación");
                // Cerrar el form
                this.DialogResult = DialogResult.OK; // Indica que la acción fue confirmada
                this.Close(); // Cierra el formulario
            }
        }

        private void frmAgregarSocio_Load(object sender, EventArgs e)
        {
            // Para ComboBox de Genero
            cboGenero.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Femenino" });
            cboGenero.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Masculino" });

            cboGenero.DisplayMember = "Texto";
            cboGenero.ValueMember = "Valor";
            cboGenero.SelectedIndex = -1;
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
