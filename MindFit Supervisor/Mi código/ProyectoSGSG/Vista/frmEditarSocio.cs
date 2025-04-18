﻿using CapaPresentacion.Utilidades;
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
using System.Windows.Documents;
using System.Windows.Forms;

namespace Vista
{
    public partial class frmEditarSocio : Form
    {
        #region "Variables"
        private static int idSocioSeleccionado;
        private static string textoEstado = "";
        private static DateTime? iFechaInicioActividades; // Ahora puede ser null
        private static DateTime? iFechaFinActividades; // Ahora puede ser null
        private static DateTime? iFechaNotificacion; // Ahora puede ser null
        private static bool? iRespuestaNotificacion; // Ahora puede ser null

        #endregion
        #region "Metodos"
        private void BloquearCampos()
        {
            txtNombreYApellido.Enabled = false;
            txtNroDocumento.Enabled = false;
            txtCiudad.Enabled = false;
            txtDireccion.Enabled = false;
            txtTelefono.Enabled = false;
            txtEmail.Enabled = false;
            txtObraSocial.Enabled = false;
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

        private void MensajeEstado()
        {
            lblMensajeEstado.Text = "Vencimiento cuota: " + (iFechaFinActividades ?? DateTime.MinValue).ToString("dd-MM-yyyy");

            if (cboEstado.SelectedItem is OpcionCombo opcionSeleccionada)
            {
                switch (opcionSeleccionada.Texto)
                {
                    case "Nuevo":
                        lblMensajeEstado.ForeColor = Color.Green;
                        break;
                    case "Actualizado":
                        lblMensajeEstado.ForeColor = Color.SeaGreen;
                        break;
                    case "Suspendido":
                        lblMensajeEstado.ForeColor = Color.OrangeRed;
                        break;
                    case "Eliminado":
                        lblMensajeEstado.ForeColor = Color.Firebrick;
                        break;
                    default:
                        lblMensajeEstado.ForeColor = Color.Black; // Color por defecto
                        break;
                }
            }
        }
        #endregion
        public frmEditarSocio(int idSocio)
        {
            idSocioSeleccionado = idSocio;
            InitializeComponent();
        }
        private void frmEditarSocio_Load(object sender, EventArgs e)
        {
            BloquearCampos();

            // Para ComboBox de Genero
            cboGenero.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Femenino" });
            cboGenero.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Masculino" });

            cboGenero.DisplayMember = "Texto";
            cboGenero.ValueMember = "Valor";
            cboGenero.SelectedIndex = -1;

            // Para ComboBox de Estado
            // Agregar elementos al ComboBox:
            cboEstado.Items.Add(new OpcionCombo() { Valor = 3, Texto = "Eliminado" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 2, Texto = "Suspendido" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Actualizado" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Nuevo" });

            // Configurar qué se muestra y qué se usa como valor:
            cboEstado.DisplayMember = "Texto"; // Le dice al ComboBox que lo que debe mostrar es el valor de la propiedad Texto de OpcionCombo. Así, en la interfaz se verá "Activo" o "No Activo".
            cboEstado.ValueMember = "Valor"; // El ValueMember en un ComboBox indica qué propiedad del objeto agregado al ComboBox será considerada como el valor interno.
            cboEstado.SelectedIndex = -1; // Esto selecciona el primer elemento de la lista por defecto, es decir, "Activo".


            //// Cargar Campos
            Socio socio = new ControladorGymSocio().GetSocio(idSocioSeleccionado);

            txtId.Text = Convert.ToString(socio.IdSocio);
            txtNombreYApellido.Text = socio.NombreYApellido;
            dtpFechaNacimiento.Value = socio.FechaNacimiento;
            foreach (OpcionCombo oc in cboGenero.Items)
            {
                if (oc.Texto == socio.Genero)
                {
                    int indice_combo = cboGenero.Items.IndexOf(oc);
                    cboGenero.SelectedIndex = indice_combo;
                    break;
                }
            }
            txtNroDocumento.Text = Convert.ToString(socio.NroDocumento);
            txtCiudad.Text = socio.Ciudad;
            txtDireccion.Text = socio.Direccion;
            txtTelefono.Text = socio.Telefono;
            txtEmail.Text = socio.Email;
            txtObraSocial.Text = socio.ObraSocial;
            foreach (Rutina R in socio.Rutinas)
            {
                if (R.Dia == "Lunes") chkLunes.Checked = true;
                if (R.Dia == "Martes") chkMartes.Checked = true;
                if (R.Dia == "Miércoles") chkMiercoles.Checked = true; // <- tildes
                if (R.Dia == "Jueves") chkJueves.Checked = true;
                if (R.Dia == "Viernes") chkViernes.Checked = true;
                if (R.Dia == "Sábado") chkSabado.Checked = true;       // <- tildes
            }

            if (socio.Plan == "Mensual") chkMensual.Checked = true;
            else chkAnual.Checked = true;

            foreach (OpcionCombo oc in cboEstado.Items)
            {
                if (oc.Texto == socio.EstadoSocio)
                {
                    int indice_combo = cboEstado.Items.IndexOf(oc);
                    cboEstado.SelectedIndex = indice_combo;
                    break;
                }
            }

            iFechaInicioActividades = socio.FechaInicioActividades;
            iFechaFinActividades = socio.FechaFinActividades;
            iFechaNotificacion = socio.FechaNotificacion;
            iRespuestaNotificacion = socio.RespuestaNotificacion;

            MensajeEstado();
            btnNombreYApellido.Select();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
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

            if (cboEstado.SelectedItem is OpcionCombo opcionSeleccionada)
            {
                textoEstado = opcionSeleccionada.Texto; 
            }

            // Capturar los días seleccionados en la lista de Rutinas
            List<Rutina> rutinas = new List<Rutina>();

            if (chkLunes.Checked) rutinas.Add(new Rutina() { Dia = "Lunes" });
            if (chkMartes.Checked) rutinas.Add(new Rutina() { Dia = "Martes" });
            if (chkMiercoles.Checked) rutinas.Add(new Rutina() { Dia = "Miércoles" }); // ← con tilde
            if (chkJueves.Checked) rutinas.Add(new Rutina() { Dia = "Jueves" });
            if (chkViernes.Checked) rutinas.Add(new Rutina() { Dia = "Viernes" });
            if (chkSabado.Checked) rutinas.Add(new Rutina() { Dia = "Sábado" });       // ← con tilde

            Socio socio = new Socio()
            {
                IdSocio = idSocioSeleccionado,
                NombreYApellido = txtNombreYApellido.Text,
                FechaNacimiento = dtpFechaNacimiento.Value,
                Genero = Convert.ToInt64(((OpcionCombo)cboGenero.SelectedItem).Valor) == 1 ? "Femenino" : "Masculino",
                NroDocumento = Convert.ToInt32(txtNroDocumento.Text),
                Ciudad = txtCiudad.Text,
                Direccion = txtDireccion.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
                ObraSocial = txtObraSocial.Text,
                Plan = planElegido,
                EstadoSocio = textoEstado,
                FechaInicioActividades = iFechaInicioActividades,
                FechaFinActividades = iFechaFinActividades,
                FechaNotificacion = iFechaNotificacion,
                RespuestaNotificacion = iRespuestaNotificacion,
                Rutinas = rutinas // Se asigna la lista de rutinas aquí
            };

            string mensaje = string.Empty;

            Boolean socioActualzado = new ControladorGymSocio().Actualizar(socio, out mensaje);

            if (socioActualzado)
            {
                MessageBox.Show("Socio actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void btnNombreYApellido_Click(object sender, EventArgs e)
        {
            BloquearCampos();
            txtNombreYApellido.Enabled = true;
        }

        private void txtNombreYApellido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                BloquearCampos();
            }
        }

        private void btnNroDocumento_Click(object sender, EventArgs e)
        {
            BloquearCampos();
            txtNroDocumento.Enabled = true;
        }

        private void txtNroDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                BloquearCampos();
            }
        }

        private void btnCiudad_Click(object sender, EventArgs e)
        {
            BloquearCampos();
            txtCiudad.Enabled = true;
        }

        private void txtCiudad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                BloquearCampos();
            }
        }

        private void btnDireccion_Click(object sender, EventArgs e)
        {
            
            txtDireccion.Enabled = true;
        }

        private void txtDireccion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                BloquearCampos();
            }
        }

        private void btnTelefono_Click(object sender, EventArgs e)
        {
            BloquearCampos();
            txtTelefono.Enabled = true;
        }

        private void txtTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                BloquearCampos();
            }
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            BloquearCampos();
            txtEmail.Enabled = true;
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                BloquearCampos();
            }
        }

        private void btnObraSocial_Click(object sender, EventArgs e)
        {
            BloquearCampos();
            txtObraSocial.Enabled = true;
        }

        private void txtObraSocial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                BloquearCampos();
            }
        }

        private void btnRenovarCuota_Click(object sender, EventArgs e)
        {
            // Mostrar un cuadro de confirmación antes de proceder
            DialogResult resultado = MessageBox.Show("¿Desea renovar la cuota?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes) // Si el usuario confirma
            {
                if (iFechaFinActividades <= DateTime.Now.Date)
                {
                    iFechaFinActividades = DateTime.Now.Date; // Se establece la fecha actual como inicio

                    if (chkMensual.Checked)
                    {
                        iFechaFinActividades = iFechaFinActividades.Value.AddDays(30);
                        iFechaNotificacion = iFechaFinActividades.Value.AddDays(29);
                    }
                    else if (chkAnual.Checked) // Verificar si es anual
                    {
                        iFechaFinActividades = iFechaFinActividades.Value.AddDays(365);
                        iFechaNotificacion = iFechaFinActividades.Value.AddDays(364);
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar un plan (Mensual o Anual)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Evita que continúe el código si no hay plan seleccionado
                    }
                } else
                {
                    if (chkMensual.Checked)
                    {
                        iFechaFinActividades = iFechaFinActividades.Value.AddDays(30);
                        iFechaNotificacion = iFechaFinActividades.Value.AddDays(29);
                    }
                    else if (chkAnual.Checked) // Verificar si es anual
                    {
                        iFechaFinActividades = iFechaFinActividades.Value.AddDays(365);
                        iFechaNotificacion = iFechaFinActividades.Value.AddDays(364);
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar un plan (Mensual o Anual)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Evita que continúe el código si no hay plan seleccionado
                    }
                }
                // Actualizar el estado del socio a "Actualizado"
                foreach (OpcionCombo oc in cboEstado.Items)
                {
                    if (oc.Texto.Equals("Actualizado", StringComparison.OrdinalIgnoreCase))
                    {
                        cboEstado.SelectedItem = oc;
                        break;
                    }
                }

                MensajeEstado();

                MessageBox.Show("Cuota renovada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
