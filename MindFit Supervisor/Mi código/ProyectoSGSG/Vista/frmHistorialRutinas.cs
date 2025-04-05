using Controlador;
using FontAwesome.Sharp;
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
    public partial class frmHistorialRutinas : Form
    {
        #region "Variables"
        private int IdSocioActual;
        private List<Rutina> rutinas;
        private Rutina RutinaSeleccionada;
        private string DiaActual;
        private string DiaPasado;
        private Usuario usuario;
        private frmGestionarRutinas formularioPadre;
        #endregion
        #region "Métodos"

        private void cargarGrid()
        {
            List<Rutina> listaRutina = new ControladorGymHistorialRutinas().Listar(IdSocioActual, DiaActual);
            rutinas = listaRutina;

            if (listaRutina != null && listaRutina.Count() > 0)
            {
                msjDgvData.Visible = false;
                msjRutina.Visible = true;

                foreach (Rutina item in listaRutina)
                {
                    int rowIndex = dgvData.Rows.Add(new object[] {
                    "",
                    item.FechaModificacion,
                    item.IdRutina,
                    item.Socio.IdSocio,
                    item.Dia,
                    });
                }
            } 
            else
            {
                msjDgvData.Visible = true;
            }
            if (msjDgvData.Visible == true) msjRutina.Visible = false;
            else msjRutina.Visible = true;

        }

        private string quitarTildeDia(string diaTilde)
        {
            switch (diaTilde)
            {
                case "Lunes": return "Lunes";
                case "Martes": return "Martes";
                case "Miércoles": return "Miercoles";
                case "Jueves": return "Jueves";
                case "Viernes": return "Viernes";
                case "Sábado": return "Sabado";
                case "Domingo": return "Domingo";
                default: return "";
            }
        }

        private void PintarBotonDiaActual(string diaTilde)
        {
            string dia = quitarTildeDia(diaTilde);

            if (btnLunes.Name == "btn" + dia) btnLunes.BackColor = Color.SteelBlue;
            if (btnMartes.Name == "btn" + dia) btnMartes.BackColor = Color.SteelBlue;
            if (btnMiercoles.Name == "btn" + dia) btnMiercoles.BackColor = Color.SteelBlue;
            if (btnJueves.Name == "btn" + dia) btnJueves.BackColor = Color.SteelBlue;
            if (btnViernes.Name == "btn" + dia) btnViernes.BackColor = Color.SteelBlue;
            if (btnSabado.Name == "btn" + dia) btnSabado.BackColor = Color.SteelBlue;
        }
        private void LimpiarPaneles()
        {
            tlpCalentamiento.Controls.Clear();
            tlpCalentamiento.RowStyles.Clear();
            tlpCalentamiento.RowCount = 0;

            tlpEntrenamiento.Controls.Clear();
            tlpEntrenamiento.RowStyles.Clear();
            tlpEntrenamiento.RowCount = 0;

            tlpEstiramiento.Controls.Clear();
            tlpEstiramiento.RowStyles.Clear();
            tlpEstiramiento.RowCount = 0;
        }

        private void LimpiarBotones()
        {
            btnLunes.BackColor = Color.White;
            btnMartes.BackColor = Color.White; ;
            btnMiercoles.BackColor = Color.White;
            btnJueves.BackColor = Color.White;
            btnViernes.BackColor = Color.White;
            btnSabado.BackColor = Color.White;
            //btnSabado.BackColor = ColorTranslator.FromHtml("#E6EAEA");
        }

        private void CargarCalentamientos()
        {
            tlpCalentamiento.Controls.Clear();
            tlpCalentamiento.RowStyles.Clear();
            tlpCalentamiento.RowCount = 0;

            List<RutinaCalentamiento> lista = new ControladorGymHistorialCalentamiento().ListarCalentamientosPorHistorialRutina(RutinaSeleccionada.IdRutina);

            foreach (var item in lista)
            {
                int rowIndex = tlpCalentamiento.RowCount++;
                tlpCalentamiento.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                ComboBox combo = new ComboBox
                {
                    Width = 600,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(20, 5, 5, 0)
                };

                List<Calentamiento> todos = new ControladorGymCalentamiento().Listar();
                foreach (var calentamiento in todos)
                {
                    combo.Items.Add(new OpcionComboCalentamiento
                    {
                        IdCalentamiento = calentamiento.IdCalentamiento,
                        IdMaquina = calentamiento.MaquinaTipoCardio?.IdElemento,
                        DescripcionCalentamiento = calentamiento.DescripcionCalentamiento
                    });
                }

                combo.SelectedItem = combo.Items
                    .OfType<OpcionComboCalentamiento>()
                    .FirstOrDefault(c => c.IdCalentamiento == item.IdCalentamiento);

                tlpCalentamiento.Controls.Add(combo, 0, rowIndex);

                FlowLayoutPanel panelMinutos = new FlowLayoutPanel
                {
                    AutoSize = true,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = false,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(20, 5, 5, 0)
                };

                NumericUpDown minutos = new NumericUpDown
                {
                    Minimum = 0,
                    Maximum = 60,
                    Value = item.Minutos,
                    Width = 60,
                    Anchor = AnchorStyles.Left,
                    TextAlign = HorizontalAlignment.Center,
                    Margin = new Padding(0)
                };

                Label lblMinutos = new Label
                {
                    Text = "Minutos",
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(5, 6, 0, 0)
                };

                panelMinutos.Controls.Add(minutos);
                panelMinutos.Controls.Add(lblMinutos);
                tlpCalentamiento.Controls.Add(panelMinutos, 1, rowIndex);

                /*
                Button btnEliminar = new Button
                {
                    Text = "✖",
                    Width = 25,
                    Height = 25,
                    BackColor = Color.Red,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Anchor = AnchorStyles.Right,
                    Margin = new Padding(5, 0, 15, 0),
                    Tag = rowIndex,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 8, FontStyle.Bold)
                };
                btnEliminar.FlatAppearance.BorderSize = 0;

                btnEliminar.Click += (s, e) =>
                {
                    Button boton = (Button)s;
                    int fila = tlpCalentamiento.GetRow(boton);
                    for (int col = 0; col < tlpCalentamiento.ColumnCount; col++)
                    {
                        var ctrl = tlpCalentamiento.GetControlFromPosition(col, fila);
                        if (ctrl != null)
                            tlpCalentamiento.Controls.Remove(ctrl);
                    }
                    if (fila < tlpCalentamiento.RowStyles.Count)
                        tlpCalentamiento.RowStyles.RemoveAt(fila);

                    tlpCalentamiento.RowCount--;
                };

                tlpCalentamiento.Controls.Add(btnEliminar, 2, rowIndex);
                */
            }
        }
        private void CargarEntrenamiento()
        {
            tlpEntrenamiento.Controls.Clear();
            tlpEntrenamiento.RowStyles.Clear();
            tlpEntrenamiento.RowCount = 0;

            if (RutinaSeleccionada == null) return;

            List<Entrenamiento> lista = new ControladorGymHistorialEntrenamiento().ListarEntrenamientoPorHistorialRutina(RutinaSeleccionada.IdRutina);

            List<ElementoGimnasio> elementos = new ControladorGymElementoGimnasio().Listar();

            foreach (var item in lista)
            {
                int rowIndex = tlpEntrenamiento.RowCount++;
                tlpEntrenamiento.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                // ComboBox de Elemento
                ComboBox combo = new ComboBox
                {
                    Width = 600,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(20, 5, 5, 0)
                };

                foreach (var elem in elementos)
                {
                    combo.Items.Add(new OpcionComboElementoGimnasio
                    {
                        IdElemento = elem.IdElemento,
                        NombreElemento = elem.NombreElemento
                    });
                }

                combo.SelectedItem = combo.Items
                    .OfType<OpcionComboElementoGimnasio>()
                    .FirstOrDefault(x => x.IdElemento == item.ElementoGimnasio.IdElemento);

                tlpEntrenamiento.Controls.Add(combo, 0, rowIndex);

                // Series
                FlowLayoutPanel panelSeries = new FlowLayoutPanel
                {
                    AutoSize = true,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = false,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(10, 5, 5, 0)
                };

                NumericUpDown nudSeries = new NumericUpDown
                {
                    Minimum = 0,
                    Maximum = 999,
                    Value = item.Series,
                    Width = 60,
                    Anchor = AnchorStyles.Left,
                    TextAlign = HorizontalAlignment.Center,
                    Margin = new Padding(10, 0, 0, 0)
                };

                Label lblSeries = new Label
                {
                    Text = "Series",
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(5, 6, 0, 0)
                };

                panelSeries.Controls.Add(nudSeries);
                panelSeries.Controls.Add(lblSeries);
                tlpEntrenamiento.Controls.Add(panelSeries, 1, rowIndex);

                // Repeticiones
                FlowLayoutPanel panelReps = new FlowLayoutPanel
                {
                    AutoSize = true,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = false,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(10, 5, 5, 0)
                };

                NumericUpDown nudReps = new NumericUpDown
                {
                    Minimum = 0,
                    Maximum = 999,
                    Value = item.Repeticiones,
                    Width = 60,
                    Anchor = AnchorStyles.Left,
                    TextAlign = HorizontalAlignment.Center,
                    Margin = new Padding(0)
                };

                Label lblReps = new Label
                {
                    Text = "Repeticiones",
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(5, 6, 0, 0)
                };

                panelReps.Controls.Add(nudReps);
                panelReps.Controls.Add(lblReps);
                tlpEntrenamiento.Controls.Add(panelReps, 2, rowIndex);

                // Peso
                FlowLayoutPanel panelPeso = new FlowLayoutPanel
                {
                    AutoSize = true,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = false,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(10, 5, 5, 0)
                };

                NumericUpDown nudPeso = new NumericUpDown
                {
                    Minimum = 0,
                    Maximum = 999,
                    Value = item.Peso,
                    Width = 60,
                    Anchor = AnchorStyles.Left,
                    TextAlign = HorizontalAlignment.Center,
                    Margin = new Padding(9, 0, 0, 0)
                };

                Label lblKg = new Label
                {
                    Text = "kg",
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(5, 6, 0, 0)
                };

                panelPeso.Controls.Add(nudPeso);
                panelPeso.Controls.Add(lblKg);
                tlpEntrenamiento.Controls.Add(panelPeso, 3, rowIndex);

                /*
                Button btnEliminar = new Button
                {
                    Text = "✖",
                    Width = 25,
                    Height = 25,
                    BackColor = Color.Red,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Anchor = AnchorStyles.Right,
                    Margin = new Padding(5, 0, 15, 0),
                    Tag = rowIndex,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 8, FontStyle.Bold)
                };
                btnEliminar.FlatAppearance.BorderSize = 0;

                btnEliminar.Click += (s, e) =>
                {
                    Button boton = (Button)s;
                    int fila = tlpEntrenamiento.GetRow(boton);

                    for (int col = 0; col < tlpEntrenamiento.ColumnCount; col++)
                    {
                        var ctrl = tlpEntrenamiento.GetControlFromPosition(col, fila);
                        if (ctrl != null)
                            tlpEntrenamiento.Controls.Remove(ctrl);
                    }

                    if (fila < tlpEntrenamiento.RowStyles.Count)
                        tlpEntrenamiento.RowStyles.RemoveAt(fila);

                    tlpEntrenamiento.RowCount--;
                };

                tlpEntrenamiento.Controls.Add(btnEliminar, 4, rowIndex);
                */
            }
        }

        private void CargarEstiramientos()
        {
            tlpEstiramiento.Controls.Clear();
            tlpEstiramiento.RowStyles.Clear();
            tlpEstiramiento.RowCount = 0;

            List<RutinaEstiramiento> lista = new ControladorGymHistorialEstiramiento().ListarEstiramientosPorHistorialRutina(RutinaSeleccionada.IdRutina);

            foreach (var item in lista)
            {
                int rowIndex = tlpEstiramiento.RowCount++;
                tlpEstiramiento.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                ComboBox combo = new ComboBox
                {
                    Width = 820,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(20, 5, 5, 0)
                };

                List<Estiramiento> todos = new ControladorGymEstiramiento().Listar();
                foreach (var estiramiento in todos)
                {
                    combo.Items.Add(new OpcionComboCalentamiento
                    {
                        IdCalentamiento = estiramiento.IdEstiramiento,
                        DescripcionCalentamiento = estiramiento.DescripcionEstiramiento
                    });
                }

                combo.SelectedItem = combo.Items
                    .OfType<OpcionComboCalentamiento>()
                    .FirstOrDefault(c => c.IdCalentamiento == item.IdEstiramiento);

                tlpEstiramiento.Controls.Add(combo, 0, rowIndex);

                FlowLayoutPanel panelMinutos = new FlowLayoutPanel
                {
                    AutoSize = true,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = false,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(20, 5, 5, 0)
                };

                NumericUpDown minutos = new NumericUpDown
                {
                    Minimum = 0,
                    Maximum = 60,
                    Value = item.Minutos,
                    Width = 60,
                    Anchor = AnchorStyles.Left,
                    TextAlign = HorizontalAlignment.Center,
                    Margin = new Padding(0)
                };

                Label lblMinutos = new Label
                {
                    Text = "Minutos",
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(5, 6, 0, 0)
                };

                panelMinutos.Controls.Add(minutos);
                panelMinutos.Controls.Add(lblMinutos);
                tlpEstiramiento.Controls.Add(panelMinutos, 1, rowIndex);

                /*
                Button btnEliminar = new Button
                {
                    Text = "✖",
                    Width = 25,
                    Height = 25,
                    BackColor = Color.Red,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Anchor = AnchorStyles.Right,
                    Margin = new Padding(5, 0, 15, 0),
                    Tag = rowIndex,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 8, FontStyle.Bold)
                };
                btnEliminar.FlatAppearance.BorderSize = 0;

                btnEliminar.Click += (s, e) =>
                {
                    Button boton = (Button)s;
                    int fila = tlpEstiramiento.GetRow(boton);
                    for (int col = 0; col < tlpEstiramiento.ColumnCount; col++)
                    {
                        var ctrl = tlpEstiramiento.GetControlFromPosition(col, fila);
                        if (ctrl != null)
                            tlpEstiramiento.Controls.Remove(ctrl);
                    }
                    if (fila < tlpEstiramiento.RowStyles.Count)
                        tlpEstiramiento.RowStyles.RemoveAt(fila);

                    tlpEstiramiento.RowCount--;
                };

                tlpEstiramiento.Controls.Add(btnEliminar, 2, rowIndex);
                */
            }
        }

        private void CargarRutinaDia(string dia, int IdHistorial)
        {
            LimpiarPaneles();

            LimpiarBotones();

            PintarBotonDiaActual(dia);

            // Buscar rutina del día
            RutinaSeleccionada = rutinas.FirstOrDefault(
                r => r.Dia.Equals(dia, StringComparison.OrdinalIgnoreCase) && r.IdRutina == IdHistorial
            );

            if (RutinaSeleccionada != null)
            {
                msjRutina.Visible = false;
                CargarCalentamientos();
                CargarEntrenamiento();
                CargarEstiramientos();
            }
            else
            {
                msjRutina.Visible = true;
            }
        }

        private List<RutinaCalentamiento> ObtenerCalentamientosDesdeFormulario()
        {
            // Lista donde se almacenarán los objetos RutinaCalentamiento
            List<RutinaCalentamiento> lista = new List<RutinaCalentamiento>();

            // Recorre todas las filas del TableLayoutPanel
            for (int row = 0; row < tlpCalentamiento.RowCount; row++)
            {
                // Intenta obtener el ComboBox que se encuentra en la columna 0 de la fila actual
                ComboBox combo = tlpCalentamiento.GetControlFromPosition(0, row) as ComboBox;

                // Intenta obtener el FlowLayoutPanel (que contiene el NumericUpDown y el Label)
                // ubicado en la columna 1 de la misma fila
                FlowLayoutPanel panel = tlpCalentamiento.GetControlFromPosition(1, row) as FlowLayoutPanel;

                // Verifica que ambos controles existan y que el ComboBox tenga una opción seleccionada
                if (combo != null && panel != null && combo.SelectedItem != null)
                {
                    // Convierte el ítem seleccionado del combo a OpcionComboCalentamiento
                    var opcion = combo.SelectedItem as OpcionComboCalentamiento;

                    // Busca dentro del panel el primer control de tipo NumericUpDown
                    NumericUpDown minutos = panel.Controls.OfType<NumericUpDown>().FirstOrDefault();

                    // Verifica que tanto la opción como el control de minutos sean válidos
                    if (opcion != null && minutos != null)
                    {
                        // Agrega a la lista un nuevo objeto RutinaCalentamiento con los datos recolectados
                        lista.Add(new RutinaCalentamiento
                        {
                            IdRutina = RutinaSeleccionada.IdRutina,        // ID de la rutina actual seleccionada
                            IdCalentamiento = opcion.IdCalentamiento,      // ID del calentamiento seleccionado
                            Minutos = (int)minutos.Value                   // Cantidad de minutos ingresados
                        });
                    }
                }
            }
            // Devuelve la lista de calentamientos ingresados por el usuario
            return lista;
        }

        private List<Entrenamiento> ObtenerEntrenamientosDesdeFormulario()
        {
            List<Entrenamiento> lista = new List<Entrenamiento>();

            for (int row = 0; row < tlpEntrenamiento.RowCount; row++)
            {
                ComboBox combo = tlpEntrenamiento.GetControlFromPosition(0, row) as ComboBox;
                FlowLayoutPanel panelSeries = tlpEntrenamiento.GetControlFromPosition(1, row) as FlowLayoutPanel;
                FlowLayoutPanel panelReps = tlpEntrenamiento.GetControlFromPosition(2, row) as FlowLayoutPanel;
                FlowLayoutPanel panelPeso = tlpEntrenamiento.GetControlFromPosition(3, row) as FlowLayoutPanel;

                if (combo != null && combo.SelectedItem != null &&
                    panelSeries != null && panelReps != null && panelPeso != null)
                {
                    var opcion = combo.SelectedItem as OpcionComboElementoGimnasio;
                    NumericUpDown nudSeries = panelSeries.Controls.OfType<NumericUpDown>().FirstOrDefault();
                    NumericUpDown nudReps = panelReps.Controls.OfType<NumericUpDown>().FirstOrDefault();
                    NumericUpDown nudPeso = panelPeso.Controls.OfType<NumericUpDown>().FirstOrDefault();

                    if (opcion != null && nudSeries != null && nudReps != null && nudPeso != null)
                    {
                        lista.Add(new Entrenamiento
                        {
                            IdRutina = RutinaSeleccionada.IdRutina,
                            Series = (int)nudSeries.Value,
                            Repeticiones = (int)nudReps.Value,
                            Peso = (int)nudPeso.Value,
                            ElementoGimnasio = new ElementoGimnasio
                            {
                                IdElemento = opcion.IdElemento,
                                NombreElemento = opcion.NombreElemento
                            }
                        });
                    }
                }
            }

            return lista;
        }

        private List<RutinaEstiramiento> ObtenerEstiramientosDesdeFormulario()
        {
            // Lista donde se almacenarán los objetos RutinaCalentamiento
            List<RutinaEstiramiento> lista = new List<RutinaEstiramiento>();

            // Recorre todas las filas del TableLayoutPanel
            for (int row = 0; row < tlpEstiramiento.RowCount; row++)
            {
                // Intenta obtener el ComboBox que se encuentra en la columna 0 de la fila actual
                ComboBox combo = tlpEstiramiento.GetControlFromPosition(0, row) as ComboBox;

                // Intenta obtener el FlowLayoutPanel (que contiene el NumericUpDown y el Label)
                // ubicado en la columna 1 de la misma fila
                FlowLayoutPanel panel = tlpEstiramiento.GetControlFromPosition(1, row) as FlowLayoutPanel;

                // Verifica que ambos controles existan y que el ComboBox tenga una opción seleccionada
                if (combo != null && panel != null && combo.SelectedItem != null)
                {
                    // Convierte el ítem seleccionado del combo a OpcionComboCalentamiento
                    var opcion = combo.SelectedItem as OpcionComboCalentamiento;

                    // Busca dentro del panel el primer control de tipo NumericUpDown
                    NumericUpDown minutos = panel.Controls.OfType<NumericUpDown>().FirstOrDefault();

                    // Verifica que tanto la opción como el control de minutos sean válidos
                    if (opcion != null && minutos != null)
                    {
                        // Agrega a la lista un nuevo objeto RutinaCalentamiento con los datos recolectados
                        lista.Add(new RutinaEstiramiento
                        {
                            IdRutina = RutinaSeleccionada.IdRutina,        // ID de la rutina actual seleccionada
                            IdEstiramiento = opcion.IdCalentamiento,      // ID del calentamiento seleccionado
                            Minutos = (int)minutos.Value                   // Cantidad de minutos ingresados
                        });
                    }
                }
            }

            // Devuelve la lista de calentamientos ingresados por el usuario
            return lista;
        }

        private void validarPermisos()
        {
            List<Permiso> listaPermisos = new ControladorGymPermiso().Listar(usuario.IdUsuario);

            // Lista de botones a validar
            List<IconButton> botones = new List<IconButton> { btnRestaurar };

            foreach (IconButton boton in botones)
            {
                // Verificar si el usuario tiene permiso por Grupo o por Acción
                bool tienePermiso = listaPermisos.Any(p =>
                    (p.Grupo != null && p.Grupo.NombreMenu == boton.Name) ||
                    (p.Accion != null && p.Accion.NombreAccion == boton.Name)
                );

                if (!tienePermiso)
                {
                    boton.Visible = false; // En lo botones cambio .Enabled por .Visible para evitar que al hacer clica un usuario igualmente se active aunque no tenga el permiso.
                    boton.BackColor = Color.Gainsboro;
                }
            }
        }
        #endregion

        public frmHistorialRutinas(frmGestionarRutinas padre, int IdSocio, string dia, Usuario usuarioActual)
        {
            usuario = usuarioActual;
            InitializeComponent();
            formularioPadre = padre;
            IdSocioActual = IdSocio;
            DiaActual = dia;
            DiaPasado = dia;
        }

        private void frmHistorialRutinas_Load(object sender, EventArgs e)
        {
            btnRestaurar.Enabled = false;

            //desactivarRutina();

            dgvData.Rows.Clear();

            PintarBotonDiaActual(DiaActual);

            cargarGrid();
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Verifica que la fila no sea el encabezado (-1 indica el encabezado)
            if (e.RowIndex < 0)
                return;

            // Verifica que está en la primera columna (columna de botones)
            if (e.ColumnIndex == 0)
            {
                // Pinta la celda con sus partes predeterminadas
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                //// Verifica si la celda actual tiene el valor "true" en la columna "Seleccionado"
                bool isSelected = Convert.ToBoolean(dgvData.Rows[e.RowIndex].Cells["Seleccionado3"].Value);

                if (isSelected) // Solo dibuja el check2 si la celda está seleccionada
                {
                    var w = Properties.Resources.check2.Width;
                    var h = Properties.Resources.check2.Height;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    e.Graphics.DrawImage(Properties.Resources.check2, new Rectangle(x, y, w, h));
                }
                e.Handled = true;
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar3")
            {
                int indice = e.RowIndex;
                
                if (indice >= 0)
                {
                    btnRestaurar.Enabled = true;
                    
                    msjRutina.Visible = true;

                    // Desmarcar todas las filas primero
                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        row.Cells["Seleccionado3"].Value = false;
                    }

                    // Activar el check2 solo en la fila clickeada
                    dgvData.Rows[indice].Cells["Seleccionado3"].Value = true;

                    string diaRutina = (dgvData.Rows[indice].Cells["Dia"].Value).ToString();
                    int IdHistorial = Convert.ToInt32(dgvData.Rows[indice].Cells["IdHistorial"].Value);

                    PintarBotonDiaActual(diaRutina);
                    CargarRutinaDia(diaRutina, IdHistorial);

                    // Refrescar la vista
                    dgvData.Refresh();
                }
            }
        }

        private void btnLunes_Click(object sender, EventArgs e)
        {
            btnRestaurar.Enabled = false;
            LimpiarPaneles();
            LimpiarBotones();

            DiaActual = "Lunes";
            PintarBotonDiaActual(DiaActual);

            dgvData.Rows.Clear();
            cargarGrid();
        }

        private void btnMartes_Click(object sender, EventArgs e)
        {
            btnRestaurar.Enabled = false;
            LimpiarPaneles();
            LimpiarBotones();

            DiaActual = "Martes";
            PintarBotonDiaActual(DiaActual);

            dgvData.Rows.Clear();
            cargarGrid();
        }

        private void btnMiercoles_Click(object sender, EventArgs e)
        {
            btnRestaurar.Enabled = false;
            LimpiarPaneles();
            LimpiarBotones();

            DiaActual = "Miércoles";
            PintarBotonDiaActual(DiaActual);

            dgvData.Rows.Clear();
            cargarGrid();
        }

        private void btnJueves_Click(object sender, EventArgs e)
        {
            btnRestaurar.Enabled = false;
            LimpiarPaneles();
            LimpiarBotones();

            DiaActual = "Jueves";
            PintarBotonDiaActual(DiaActual);

            dgvData.Rows.Clear();
            cargarGrid();
        }

        private void btnViernes_Click(object sender, EventArgs e)
        {
            btnRestaurar.Enabled = false;
            LimpiarPaneles();
            LimpiarBotones();

            DiaActual = "Viernes";
            PintarBotonDiaActual(DiaActual);

            dgvData.Rows.Clear();
            cargarGrid();
        }

        private void btnSabado_Click(object sender, EventArgs e)
        {
            btnRestaurar.Enabled = false;
            LimpiarPaneles();
            LimpiarBotones();

            DiaActual = "Sábado";
            PintarBotonDiaActual(DiaActual);

            dgvData.Rows.Clear();
            cargarGrid();
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (RutinaSeleccionada == null)
            {
                MessageBox.Show("Debe seleccionar una rutina antes de restaurar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var resultado = DialogResult.No;

            if (RutinaSeleccionada.Dia == DiaPasado)
            {
                // Pregunta de confirmación
                resultado = MessageBox.Show(
                $"¿Está seguro que desea restaurar la rutina del {RutinaSeleccionada.FechaModificacion.ToString("dd/MM/yyyy")} para el día {DiaPasado}?",
                "Confirmar restauración",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            }
            else
            {
                // Pregunta de confirmación
                resultado = MessageBox.Show(
                $"¿Está seguro que desea restaurar la rutina del {RutinaSeleccionada.Dia} {RutinaSeleccionada.FechaModificacion.ToString("dd/MM/yyyy")} para el día {DiaPasado}?",
                "Confirmar restauración",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            }

            if (resultado == DialogResult.Yes)
            {
                var listaCalentamientos = ObtenerCalentamientosDesdeFormulario();
                var listaEntrenamientos = ObtenerEntrenamientosDesdeFormulario();
                var listaEstiramientos = ObtenerEstiramientosDesdeFormulario();

                formularioPadre.CargarRutinaDesdeHistorial(listaCalentamientos, listaEntrenamientos, listaEstiramientos);
                this.Close(); // Cerrás solo el historial
            }
        }
    }
}
