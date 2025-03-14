using System;
using Modelo;
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
    public partial class frmAcciones : Form
    {
        // Propiedad pública para almacenar las acciones seleccionadas
        public List<Accion> AccionesSeleccionadas { get; set; } // No inicializamos aquí
        private int ultimoUsuarioId;

        public frmAcciones(int id, List<Accion> accionesPrevias)
        {
            ultimoUsuarioId = id;
            AccionesSeleccionadas = accionesPrevias ?? new List<Accion>(); // Si es null, inicializamos una nueva lista
            InitializeComponent();
        }

        private void frmAcciones_Load(object sender, EventArgs e)
        {
            if (ultimoUsuarioId == 0 && AccionesSeleccionadas != null && AccionesSeleccionadas.Count > 0)
            {
                foreach (Accion accion in AccionesSeleccionadas)
                {
                    if (accion.NombreAccion == btnMenuAgregar.Name) btnMenuAgregar.Checked = true;
                    if (accion.NombreAccion == btnMenuConsultar.Name) btnMenuConsultar.Checked = true;
                    if (accion.NombreAccion == btnMenuEliminar.Name) btnMenuEliminar.Checked = true;
                    if (accion.NombreAccion == btnMenuTurno.Name) btnMenuTurno.Checked = true;

                    if (accion.NombreAccion == menuUsuarios.Name) menuUsuarios.Checked = true;
                    if (accion.NombreAccion == menuRoles.Name) menuRoles.Checked = true;
                    if (accion.NombreAccion == menuMaquinas.Name) menuMaquinas.Checked = true;
                    if (accion.NombreAccion == menuEjercicios.Name) menuEjercicios.Checked = true;
                    if (accion.NombreAccion == menuEquipamiento.Name) menuEquipamiento.Checked = true;
                    if (accion.NombreAccion == menuRangosHorarios.Name) menuRangosHorarios.Checked = true;
                    if (accion.NombreAccion == menuNegocio.Name) menuNegocio.Checked = true;
                }
            }
            else
            {
                // traer de la bd las acciones del usuario con id ultimoUsuarioId
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            List<Accion> listaTemporal = new List<Accion>();

            if (btnMenuAgregar.Checked == true) // Si está marcado
            {
                listaTemporal.Add(new Accion { NombreAccion = btnMenuAgregar.Name });
            }
            if (btnMenuConsultar.Checked == true) // Si está marcado
            {
                listaTemporal.Add(new Accion { NombreAccion = btnMenuConsultar.Name });
            }
            if (btnMenuEliminar.Checked == true) // Si está marcado
            {
                listaTemporal.Add(new Accion { NombreAccion = btnMenuEliminar.Name });
            }
            if (btnMenuTurno.Checked == true) // Si está marcado
            {
                listaTemporal.Add(new Accion { NombreAccion = btnMenuTurno.Name });
            }
            
            if (menuUsuarios.Checked == true) // Si está marcado
            {
                listaTemporal.Add(new Accion { NombreAccion = menuUsuarios.Name });
            }
            if (menuRoles.Checked == true) // Si está marcado
            {
                listaTemporal.Add(new Accion { NombreAccion = menuRoles.Name });
            }
            if (menuMaquinas.Checked == true) // Si está marcado
            {
                listaTemporal.Add(new Accion { NombreAccion = menuMaquinas.Name });
            }
            if (menuEjercicios.Checked == true) // Si está marcado
            {
                listaTemporal.Add(new Accion { NombreAccion = menuEjercicios.Name });
            }
            if (menuEquipamiento.Checked == true) // Si está marcado
            {
                listaTemporal.Add(new Accion { NombreAccion = menuEquipamiento.Name });
            }
            if (menuRangosHorarios.Checked == true) // Si está marcado
            {
                listaTemporal.Add(new Accion { NombreAccion = menuRangosHorarios.Name });
            }
            if (menuNegocio.Checked == true) // Si está marcado
            {
                listaTemporal.Add(new Accion { NombreAccion = menuNegocio.Name });
            }

            AccionesSeleccionadas = listaTemporal.Any() ? listaTemporal : new List<Accion>();

            // Ahora sí, mostrar el mensaje con seguridad
            // MessageBox.Show($"Se seleccionaron {AccionesSeleccionadas.Count} acciones.");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
