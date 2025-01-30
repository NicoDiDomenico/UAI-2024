using pjGestionEmpleados.Datos;
using pjGestionEmpleados.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pjGestionEmpleados.Presentacion
{
    public partial class frmEmpleados : Form
    {
        public frmEmpleados()
        {
            InitializeComponent();
        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            CargarEmpleados("%");
            CargarDepartamentos();
            CargarCargos();
        }
        #region "Variables"
            int iCodigoEmpleado = 0;
        #endregion

        #region "Métodos"
        private void CargarEmpleados(string cBusqueda)
        {
            D_Empleados Datos = new D_Empleados();
            dgvLista.DataSource = Datos.Listar_Empleados(cBusqueda);

            FormatoListaEmpleados();
        }
        private void FormatoListaEmpleados()
        {
            dgvLista.Columns[0].Width = 45;
            dgvLista.Columns[1].Width = 170;
            dgvLista.Columns[2].Width = 160;
            dgvLista.Columns[4].Width = 146;
            dgvLista.Columns[5].Width = 200;
        }

        private void CargarDepartamentos()
        {
            D_Departamentos Datos = new D_Departamentos();
            cmbDepartamento.DataSource = Datos.Listar_Departamentos();
            
            cmbDepartamento.ValueMember = "id_departamento"; // Indica que el valor interno del ComboBox será id_departamento. Esto significa que cuando selecciones un departamento, su id_departamento será el valor asociado.
            // ValueMember: ¿Qué columna de la tabla será el valor interno del ComboBox? --> id_departamento
            
            cmbDepartamento.DisplayMember = "nombre_departamento"; // Define qué campo se mostrará en el ComboBox. En este caso, se mostrará el nombre_departamento en la lista desplegable.
            // DisplayMember: ¿Qué columna de la tabla se mostrará en la interfaz? --> nombre_departamento

            //cmbDepartamento.Text = " "; // Mejor asi:
            cmbDepartamento.SelectedIndex = -1;
        }

        private void CargarCargos()
        {
            D_Cargos Datos = new D_Cargos();
            cmbCargo.DataSource = Datos.Listar_Cargos();

            cmbCargo.ValueMember = "id_cargo"; // Indica que el valor interno del ComboBox será id_departamento. Esto significa que cuando selecciones un departamento, su id_departamento será el valor asociado.
                                                             // ValueMember: ¿Qué columna de la tabla será el valor interno del ComboBox? --> id_departamento

            cmbCargo.DisplayMember = "nombre_cargo"; // Define qué campo se mostrará en el ComboBox. En este caso, se mostrará el nombre_departamento en la lista desplegable.
            // DisplayMember: ¿Qué columna de la tabla se mostrará en la interfaz? --> nombre_departamento

            cmbCargo.SelectedIndex = -1;
        }

        private void ActivarTextos(bool bEstado)
        {
            txtNombre.Enabled = bEstado;
            txtDireccion.Enabled = bEstado;
            txtTelefono.Enabled = bEstado;
            txtSalario.Enabled = bEstado;
            cmbDepartamento.Enabled = bEstado;
            cmbCargo.Enabled = bEstado;
            dtpFechaNacimiento.Enabled = bEstado;

            txtBuscar.Enabled = !bEstado;
        }

        private void ActivarBotones(bool bEstado)
        {
            btnNuevo.Enabled = bEstado;
            btnActualizar.Enabled = bEstado;
            btnEliminar.Enabled = bEstado;
            btnReporte.Enabled = bEstado;

            btnGuardar.Visible = !bEstado;
            btnCancelar.Visible = !bEstado;
        }

        private void seleccionarEmpleados()
        {
            iCodigoEmpleado = Convert.ToInt32(dgvLista.CurrentRow.Cells["ID"].Value); // Nos va a servir para actualizar o eliminar

            txtNombre.Text = Convert.ToString(dgvLista.CurrentRow.Cells["Nombre"].Value);
            txtDireccion.Text = Convert.ToString(dgvLista.CurrentRow.Cells["Dirección"].Value);
            txtTelefono.Text = Convert.ToString(dgvLista.CurrentRow.Cells["Teléfono"].Value);
            txtSalario.Text = Convert.ToString(dgvLista.CurrentRow.Cells["Salario"].Value);
            cmbDepartamento.Text = Convert.ToString(dgvLista.CurrentRow.Cells["Departamento"].Value);
            cmbCargo.Text = Convert.ToString(dgvLista.CurrentRow.Cells["Cargo"].Value);
            dtpFechaNacimiento.Value = Convert.ToDateTime(dgvLista.CurrentRow.Cells["Fecha Nacimiento"].Value);
            /* Desglose paso a paso:
            1️° dgvLista.CurrentRow

            Obtiene la fila actual seleccionada en el DataGridView.
            CurrentRow representa la fila en la que el usuario ha hecho clic o seleccionado.
            
            2️° .Cells["Fecha Nacimiento"].Value

            Accede a la celda de la columna "Fecha Nacimiento" dentro de la fila seleccionada.
            Value obtiene el valor almacenado en esa celda.
            
            3️° Convert.ToDateTime(...)

            Convierte el valor de la celda a un tipo DateTime.
            Si el valor en la celda es una fecha válida, lo convierte sin problemas.
            Si la celda es null o tiene un valor incorrecto, puede lanzar una excepción.
            
            4️° dtpFechaNacimiento.Value = ...

            Asigna la fecha obtenida al DateTimePicker (dtpFechaNacimiento).
            DateTimePicker.Value debe recibir un DateTime válido, por eso usamos Convert.ToDateTime(). */
        }
        private void LimpiarCampos()
        {
            iCodigoEmpleado = 0;

            txtNombre.Text = " ";
            txtDireccion.Text = " ";
            txtTelefono.Text = " ";
            txtSalario.Clear(); // MEJOR FORMA!!!
            txtBuscar.Clear();

            cmbCargo.SelectedIndex = -1;
            cmbDepartamento.SelectedIndex = -1;

            dtpFechaNacimiento.Value = DateTime.Now;
        }

        private void GuardarEmpleados() { 
            E_Empleado Empleado = new E_Empleado();

            Empleado.Nombre_Empleado = txtNombre.Text;
            Empleado.Direccion_Empleado = txtDireccion.Text;
            Empleado.Telefono_Empleado = txtTelefono.Text;
            Empleado.Salario_Empleado = Convert.ToDecimal(txtSalario.Text);
            Empleado.Fecha_Nacimiento_Empleado = dtpFechaNacimiento.Value;
            Empleado.ID_Departamento = Convert.ToInt32(cmbDepartamento.SelectedValue);
            Empleado.ID_Cargo = Convert.ToInt32(cmbCargo.SelectedValue);

            D_Empleados Datos = new D_Empleados();
            string respuesta = Datos.Guardar_Empleado(Empleado);

            if (respuesta == "OK")
            {
                CargarEmpleados("%"); // Volver a cargar el grid
                LimpiarCampos();
                ActivarTextos(false);
                ActivarBotones(true);

                MessageBox.Show("Datos Guardados Correctamente!", "Sistema Gestión de Empleados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(respuesta, "Sistema Gestión de Empleados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool validarTextos()
        {
            bool hayTextosVacios = false;

            if (string.IsNullOrEmpty(txtNombre.Text)) hayTextosVacios = true;
            if (string.IsNullOrEmpty(txtTelefono.Text)) hayTextosVacios = true;
            if (string.IsNullOrEmpty(txtSalario.Text)) hayTextosVacios = true;

            return hayTextosVacios;
        }

        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            ActivarTextos(true);
            ActivarBotones(false);

            txtNombre.Select();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (iCodigoEmpleado != 0)
            {
                ActivarTextos(true);
                ActivarBotones(false);

                txtNombre.Select();
            } else
            {
                MessageBox.Show("Selecciona un Registro", "Sistema de Gestión de Empleados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (validarTextos())
            {
                MessageBox.Show("Hay Campos vacíos, debes llenar todos los campos (*) obligatorios",
                "Sistema Gestión de Empleados",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
            else
            {
                GuardarEmpleados();
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string campo = txtBuscar.Text;
            CargarEmpleados(campo);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // CargarEmpleados(txtBuscar.Text); // Podenos hacer lo miusmo pero en otro evento, esto seria mas practico.
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ActivarTextos(!true);
            ActivarBotones(!false);
            LimpiarCampos();
        }

        // Este evento se activa solo cuando el usuario hace clic en el contenido de la celda (por ejemplo, texto o controles como botones o enlaces dentro de la celda).
        /*private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            seleccionarEmpelados();

        }*/

        //  Este evento se activa cuando el usuario hace clic en cualquier parte de la celda (no importa si es texto, espacio vacío o un botón dentro de la celda).
        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            seleccionarEmpleados();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
