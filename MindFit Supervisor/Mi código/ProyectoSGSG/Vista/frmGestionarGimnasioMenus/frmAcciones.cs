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
using Controlador;

namespace Vista
{
    public partial class frmAcciones : Form
    {
        #region "Variables"
        // Propiedad pública para almacenar las acciones seleccionadas
        public List<Accion> AccionesSeleccionadas { get; set; } // No inicializamos aquí
        private int ultimoUsuarioId;
        private ToolTip toolTipAcciones = new ToolTip();
        private Dictionary<string, string> DescripcionesAcciones = new Dictionary<string, string>();
        #endregion
        #region "Metodos"
        private void MarcarAcciones(List<Accion> acciones)
        {
            foreach (Accion accion in acciones)
            {
                if (accion.NombreAccion == btnMenuAgregar.Name) btnMenuAgregar.Checked = true;
                if (accion.NombreAccion == btnMenuConsultar.Name) btnMenuConsultar.Checked = true;
                if (accion.NombreAccion == btnMenuEliminar.Name) btnMenuEliminar.Checked = true;
                if (accion.NombreAccion == btnMenuTurno.Name) btnMenuTurno.Checked = true;

                if (accion.NombreAccion == menuUsuarios.Name) menuUsuarios.Checked = true;
                if (accion.NombreAccion == menuRoles.Name) menuRoles.Checked = true;
                if (accion.NombreAccion == menuCalentamiento.Name) menuCalentamiento.Checked = true;
                if (accion.NombreAccion == menuElementosGym.Name) menuElementosGym.Checked = true;
                if (accion.NombreAccion == menuEstiramiento.Name) menuEstiramiento.Checked = true;
                if (accion.NombreAccion == menuRangosHorarios.Name) menuRangosHorarios.Checked = true;
                if (accion.NombreAccion == menuNegocio.Name) menuNegocio.Checked = true;

                if (accion.NombreAccion == btnEliminar.Name) btnEliminar.Checked = true;
                if (accion.NombreAccion == btnHistorial.Name) btnHistorial.Checked = true;
                if (accion.NombreAccion == btnGuardarRutina.Name) btnGuardarRutina.Checked = true;
                if (accion.NombreAccion == btnRestaurar.Name) btnRestaurar.Checked = true;
            }
        }

        private void CargarDescripciones()
        {
            DescripcionesAcciones.Clear();
            List<Accion> todas = new Controlador.ControladorGymAccion().ListarTodo();

            foreach (Accion a in todas)
            {
                if (!DescripcionesAcciones.ContainsKey(a.NombreAccion))
                {
                    DescripcionesAcciones[a.NombreAccion] = a.Descripcion;
                }
            }
        }

        private void MostrarToolTipAccion(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb != null && DescripcionesAcciones.TryGetValue(cb.Name, out string descripcion))
            {
                toolTipAcciones.SetToolTip(cb, descripcion);
            }
        }

        private void AsignarEventosToolTip(Control controlPadre)
        {
            foreach (Control control in controlPadre.Controls)
            {
                if (control is CheckBox cb)
                {
                    cb.MouseHover += MostrarToolTipAccion;
                }

                // Si el control contiene otros controles (GroupBox, Panel, etc.)
                if (control.HasChildren)
                {
                    AsignarEventosToolTip(control);
                }
            }
        }

        #endregion

        public frmAcciones(int id, List<Accion> accionesPrevias)
        {
            ultimoUsuarioId = id;

            // Clonamos la lista para no compartir referencia con el formulario padre
            AccionesSeleccionadas = accionesPrevias != null
                ? new List<Accion>(accionesPrevias.Select(a => new Accion { NombreAccion = a.NombreAccion }))
                : new List<Accion>();

            InitializeComponent();
        }

        private void frmAcciones_Load(object sender, EventArgs e)
        {
            CargarDescripciones();

            // Asignar evento MouseHover a cada CheckBox
            toolTipAcciones.AutoPopDelay = 5000;
            toolTipAcciones.InitialDelay = 500;
            toolTipAcciones.ReshowDelay = 500;
            toolTipAcciones.ShowAlways = true;

            AsignarEventosToolTip(this); // <- esto explora todo, incluso dentro de GroupBox

            if (AccionesSeleccionadas != null && AccionesSeleccionadas.Count > 0)
            {
                MarcarAcciones(AccionesSeleccionadas);
            }
            else
            {
                // Solo traemos desde la base si no vino ninguna acción
                List<Accion> accionesBD = new Controlador.ControladorGymAccion().Listar(ultimoUsuarioId);
                AccionesSeleccionadas = accionesBD ?? new List<Accion>();
                MarcarAcciones(AccionesSeleccionadas);
            }
            if (
                btnMenuAgregar.Checked &&
                btnMenuConsultar.Checked &&
                btnMenuEliminar.Checked &&
                btnMenuTurno.Checked &&

                menuUsuarios.Checked &&
                menuRoles.Checked &&
                menuCalentamiento.Checked &&
                menuElementosGym.Checked &&
                menuEstiramiento.Checked &&
                menuRangosHorarios.Checked &&
                menuNegocio.Checked &&

                btnEliminar.Checked &&
                btnHistorial.Checked &&
                btnGuardarRutina.Checked &&
                btnRestaurar.Checked
)
            {
                cbTodo.Checked = true;
            }
            else
            {
                cbTodo.Checked = false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            List<Accion> listaTemporal = new List<Accion>();

            // Verificar cada checkbox
            if (btnMenuAgregar.Checked) listaTemporal.Add(new Accion { NombreAccion = btnMenuAgregar.Name });
            if (btnMenuConsultar.Checked) listaTemporal.Add(new Accion { NombreAccion = btnMenuConsultar.Name });
            if (btnMenuEliminar.Checked) listaTemporal.Add(new Accion { NombreAccion = btnMenuEliminar.Name });
            if (btnMenuTurno.Checked) listaTemporal.Add(new Accion { NombreAccion = btnMenuTurno.Name });

            if (menuUsuarios.Checked) listaTemporal.Add(new Accion { NombreAccion = menuUsuarios.Name });
            if (menuRoles.Checked) listaTemporal.Add(new Accion { NombreAccion = menuRoles.Name });
            if (menuCalentamiento.Checked) listaTemporal.Add(new Accion { NombreAccion = menuCalentamiento.Name });
            if (menuElementosGym.Checked) listaTemporal.Add(new Accion { NombreAccion = menuElementosGym.Name });
            if (menuEstiramiento.Checked) listaTemporal.Add(new Accion { NombreAccion = menuEstiramiento.Name });
            if (menuRangosHorarios.Checked) listaTemporal.Add(new Accion { NombreAccion = menuRangosHorarios.Name });
            if (menuNegocio.Checked) listaTemporal.Add(new Accion { NombreAccion = menuNegocio.Name });

            if (btnEliminar.Checked) listaTemporal.Add(new Accion { NombreAccion = btnEliminar.Name });
            if (btnHistorial.Checked) listaTemporal.Add(new Accion { NombreAccion = btnHistorial.Name });
            if (btnGuardarRutina.Checked) listaTemporal.Add(new Accion { NombreAccion = btnGuardarRutina.Name });
            if (btnRestaurar.Checked) listaTemporal.Add(new Accion { NombreAccion = btnRestaurar.Name });

            // Actualizá la propiedad pública
            AccionesSeleccionadas = listaTemporal;

            // Cierra el formulario y devuelve OK
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cbTodo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTodo.Checked == true)
            {
                
                btnMenuAgregar.Checked = true;
                btnMenuConsultar.Checked = true;
                btnMenuEliminar.Checked = true;
                btnMenuTurno.Checked = true;

                menuUsuarios.Checked = true;
                menuRoles.Checked = true;
                menuCalentamiento.Checked = true;
                menuElementosGym.Checked = true;
                menuEstiramiento.Checked = true;
                menuRangosHorarios.Checked = true;
                menuNegocio.Checked = true;

                btnEliminar.Checked = true;
                btnHistorial.Checked = true;
                btnGuardarRutina.Checked = true;
                btnRestaurar.Checked = true;
            }
            else {
                
                btnMenuAgregar.Checked = false;
                btnMenuConsultar.Checked = false;
                btnMenuEliminar.Checked = false;
                btnMenuTurno.Checked = false;

                menuUsuarios.Checked = false;
                menuRoles.Checked = false;
                menuCalentamiento.Checked = false;
                menuElementosGym.Checked = false;
                menuEstiramiento.Checked = false;
                menuRangosHorarios.Checked = false;
                menuNegocio.Checked = false;

                btnEliminar.Checked = false;
                btnHistorial.Checked = false;
                btnGuardarRutina.Checked = false;
                btnRestaurar.Checked = false;
                
            }
        }
    }
}
