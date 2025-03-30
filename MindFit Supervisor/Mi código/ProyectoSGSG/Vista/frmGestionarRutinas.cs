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
    public partial class frmGestionarRutinas : Form
    {
        #region "Variables"
        private int idRangoHorarioActual; // Variable global para almacenar el Id del rango horario actual
        private int IdSocioActual;
        private List<Rutina> RutinasActuales;
        private Rutina RutinaSeleccionada;
        #endregion
        #region "Métodos"
        private void cargarCBO()
        {
            cboRangoHorario.Items.Clear();
            List<RangoHorario> listaRangos = new ControladorGymRangoHorario().Listar();

            // Obtener la hora actual sin minutos ni segundos
            int horaActual = DateTime.Now.Hour;
            idRangoHorarioActual = -1; // Inicializamos con un valor no válido

            foreach (RangoHorario item in listaRangos)
            {
                cboRangoHorario.Items.Add(new OpcionCombo()
                {
                    Valor = item.IdRangoHorario,
                    Texto = $"{item.HoraDesde} - {item.HoraHasta}"
                });
            }

            cboRangoHorario.DisplayMember = "Texto";
            cboRangoHorario.ValueMember = "Valor";

            // **Seleccionar el rango con la misma HoraDesde que la hora actual**
            for (int i = 0; i < cboRangoHorario.Items.Count; i++)
            {
                OpcionCombo oc = (OpcionCombo)cboRangoHorario.Items[i];

                // Obtener la hora de `HoraDesde` desde el texto
                string[] horas = oc.Texto.Split('-'); // Separa el rango "HH:mm - HH:mm"
                int horaDesdeCombo = Convert.ToInt32(horas[0].Trim().Split(':')[0]); // Obtiene la hora como int

                if (horaDesdeCombo == horaActual)
                {
                    cboRangoHorario.SelectedIndex = i;
                    idRangoHorarioActual = Convert.ToInt32(oc.Valor); // Guardar el Id del rango horario actual
                    break;
                }
            }
        }

        private void CargarSocios(int IdEntrenador)
        {
            lblMensaje.Visible = false;

            dgvDataSocio.Rows.Clear();

            List<Socio> socios = new ControladorGymSocio().ListarSociosActuales(IdEntrenador, idRangoHorarioActual);

            if (socios.Count > 0)
            {
                foreach (var socio in socios)
                {
                    dgvDataSocio.Rows.Add("", socio.IdSocio, socio.NombreYApellido);
                }
            } else
            {
                lblMensaje.Visible = true;
            }
        }
        private string TraducirDia(string diaIngles)
        {
            switch (diaIngles)
            {
                case "Monday": return "Lunes";
                case "Tuesday": return "Martes";
                case "Wednesday": return "Miercoles";
                case "Thursday": return "Jueves";
                case "Friday": return "Viernes";
                case "Saturday": return "Sabado";
                case "Sunday": return "Domingo";
                default: return "";
            }
        }

        private string TraducirDiaConTilde(string diaIngles)
        {
            switch (diaIngles)
            {
                case "Monday": return "Lunes";
                case "Tuesday": return "Martes";
                case "Wednesday": return "Miércoles";
                case "Thursday": return "Jueves";
                case "Friday": return "Viernes";
                case "Saturday": return "Sábado";
                case "Sunday": return "Domingo";
                default: return "";
            }
        }

        private void PintarBotonDiaActual()
        {
            // Obtener el día actual en español
            string diaActual = DateTime.Now.DayOfWeek.ToString();

            // Convertir el día al formato esperado
            string diaBoton = TraducirDia(diaActual);

            if (btnLunes.Name == "btn" + diaBoton) btnLunes.BackColor = Color.SteelBlue;
            if (btnMartes.Name == "btn" + diaBoton) btnMartes.BackColor = Color.SteelBlue;
            if (btnMiercoles.Name == "btn" + diaBoton) btnMiercoles.BackColor = Color.SteelBlue;
            if (btnJueves.Name == "btn" + diaBoton) btnJueves.BackColor = Color.SteelBlue;
            if (btnViernes.Name == "btn" + diaBoton) btnViernes.BackColor = Color.SteelBlue;
            if (btnSabado.Name == "btn" + diaBoton) btnSabado.BackColor = Color.SteelBlue;
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

        private void activarRutina()
        {
            gbCalentamiento.Visible = true;
            gbEntrenamiento.Visible = true;
            gbEstiramiento.Visible = true;

            panelBotones.Visible = true;

            btnAgregarFilaCalentamiento.Visible = true;
            btnAgregarFilaEntrenamiento.Visible = true;
            btnAgregarFilaEstiramiento.Visible = true;
        }

        private void habilitarRutina()
        {
            panelBotones.Enabled = true;

            btnAgregarFilaCalentamiento.Enabled = true;
            btnAgregarFilaEntrenamiento.Enabled = true;
            btnAgregarFilaEstiramiento.Enabled = true;
        }

        private void desactivarRutina()
        {
            gbCalentamiento.Visible = false;
            gbEntrenamiento.Visible = false;
            gbEstiramiento.Visible = false;

            panelBotones.Visible = false;

            btnAgregarFilaCalentamiento.Visible = false;
            btnAgregarFilaEntrenamiento.Visible = false;
            btnAgregarFilaEstiramiento.Visible = false;
        }

        private void deshabilitarRutina()
        {
            panelBotones.Enabled = false;

            btnAgregarFilaCalentamiento.Enabled = false;
            btnAgregarFilaEntrenamiento.Enabled = false;
            btnAgregarFilaEstiramiento.Enabled = false;
        }

        // Este método recorre todas las filas del TableLayoutPanel "tlpCalentamiento"
        // y extrae los datos ingresados en los controles (ComboBox + NumericUpDown)
        // para devolver una lista de objetos "RutinaCalentamiento"
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
        private void CargarCalentamientos()
        {
            tlpCalentamiento.Controls.Clear(); // Limpiar filas anteriores
            tlpCalentamiento.RowStyles.Clear();
            tlpCalentamiento.RowCount = 0;

            List<RutinaCalentamiento> lista = new ControladorGymCalentamiento().ListarCalentamientosPorRutina(RutinaSeleccionada.IdRutina);

            foreach (var item in lista)
            {
                int rowIndex = tlpCalentamiento.RowCount++;
                tlpCalentamiento.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                // ComboBox
                ComboBox combo = new ComboBox
                {
                    Width = 400,
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

                // Seleccionar el que corresponde
                combo.SelectedItem = combo.Items
                    .OfType<OpcionComboCalentamiento>()
                    .FirstOrDefault(c => c.IdCalentamiento == item.IdCalentamiento);

                tlpCalentamiento.Controls.Add(combo, 0, rowIndex);

                // Panel minutos
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

                // Botón eliminar
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
            }
        }

        private void CargarEstiramientos()
        {
            tlpEstiramiento.Controls.Clear(); // Limpiar filas anteriores
            tlpEstiramiento.RowStyles.Clear();
            tlpEstiramiento.RowCount = 0;

            List<RutinaEstiramiento> lista = new ControladorGymEstiramiento().ListarEstiramientosPorRutina(RutinaSeleccionada.IdRutina);

            foreach (var item in lista)
            {
                int rowIndex = tlpEstiramiento.RowCount++;
                tlpEstiramiento.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                // ComboBox
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

                // Seleccionar el que corresponde
                combo.SelectedItem = combo.Items
                    .OfType<OpcionComboCalentamiento>()
                    .FirstOrDefault(c => c.IdCalentamiento == item.IdEstiramiento);

                tlpEstiramiento.Controls.Add(combo, 0, rowIndex);

                // Panel minutos
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

                // Botón eliminar
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
            }
        }

        private void AgregarFilaCalentamiento()
        {
            int rowIndex = tlpCalentamiento.RowCount++;
            tlpCalentamiento.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // ComboBox
            ComboBox combo = new ComboBox
            {
                Width = 400,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(20, 5, 5, 0)
            };

            List<Calentamiento> lista = new ControladorGymCalentamiento().Listar();
            foreach (Calentamiento item in lista)
            {
                combo.Items.Add(new OpcionComboCalentamiento
                {
                    IdCalentamiento = item.IdCalentamiento,
                    IdMaquina = item.MaquinaTipoCardio?.IdElemento,
                    DescripcionCalentamiento = item.DescripcionCalentamiento
                });
            }

            combo.SelectedIndex = -1;
            tlpCalentamiento.Controls.Add(combo, 0, rowIndex);

            // Panel minutos
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
                Value = 0,
                Width = 60,
                Anchor = AnchorStyles.Left,
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

            // Botón eliminar fila
            Button btnEliminar = new Button
            {
                Text = "✖",
                Width = 25,
                Height = 25,
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Right,
                Margin = new Padding(5, 0, 15, 0), // separa un poco del borde derecho
                Tag = rowIndex,
                TextAlign = ContentAlignment.MiddleCenter, // <-- centra el texto
                Font = new Font("Segoe UI", 8, FontStyle.Bold) // opcional para mejor visibilidad
            };
            // Quitar borde extra
            btnEliminar.FlatAppearance.BorderSize = 0;

            btnEliminar.Click += (s, e) =>
            {
                Button boton = (Button)s;
                int fila = tlpCalentamiento.GetRow(boton);

                // Eliminar controles de esa fila
                for (int col = 0; col < tlpCalentamiento.ColumnCount; col++)
                {
                    var ctrl = tlpCalentamiento.GetControlFromPosition(col, fila);
                    if (ctrl != null)
                        tlpCalentamiento.Controls.Remove(ctrl);
                }

                // Solo si el índice es válido
                if (fila < tlpCalentamiento.RowStyles.Count)
                    tlpCalentamiento.RowStyles.RemoveAt(fila);

                tlpCalentamiento.RowCount--;
            };

            tlpCalentamiento.Controls.Add(btnEliminar, 2, rowIndex);
        }

        private void AgregarFilaEntrenamiento()
        {
            // Implmeentación parececida a Calentamiento pero con mas columnas y para Entrenamiento - NO HACER
        }

        private void AgregarFilaEstiramiento()
        {
            int rowIndex = tlpEstiramiento.RowCount++;
            tlpEstiramiento.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // ComboBox
            ComboBox combo = new ComboBox
            {
                Width = 820,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(20, 5, 5, 0)
            };

            List<Estiramiento> lista = new ControladorGymEstiramiento().Listar();
            foreach (Estiramiento item in lista)
            {
                combo.Items.Add(new OpcionComboCalentamiento
                {
                    IdCalentamiento = item.IdEstiramiento,
                    DescripcionCalentamiento = item.DescripcionEstiramiento
                });
            }

            combo.SelectedIndex = -1;
            tlpEstiramiento.Controls.Add(combo, 0, rowIndex);

            // Panel minutos
            FlowLayoutPanel panelDuracion = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(20, 5, 5, 0)
            };

            NumericUpDown duracion = new NumericUpDown
            {
                Minimum = 0,
                Maximum = 60,
                Value = 0,
                Width = 60,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(0)
            };

            Label lblDuracion = new Label
            {
                Text = "Minutos",
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(5, 6, 0, 0)
            };

            panelDuracion.Controls.Add(duracion);
            panelDuracion.Controls.Add(lblDuracion);

            tlpEstiramiento.Controls.Add(panelDuracion, 1, rowIndex);

            // Botón eliminar fila
            Button btnEliminarE = new Button
            {
                Text = "✖",
                Width = 25,
                Height = 25,
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Right,
                Margin = new Padding(5, 0, 15, 0), // separa un poco del borde derecho
                Tag = rowIndex,
                TextAlign = ContentAlignment.MiddleCenter, // <-- centra el texto
                Font = new Font("Segoe UI", 8, FontStyle.Bold) // opcional para mejor visibilidad
            };
            // Quitar borde extra
            btnEliminarE.FlatAppearance.BorderSize = 0;

            btnEliminarE.Click += (s, e) =>
            {
                Button botonE = (Button)s;
                int fila = tlpEstiramiento.GetRow(botonE);

                // Eliminar controles de esa fila
                for (int col = 0; col < tlpEstiramiento.ColumnCount; col++)
                {
                    var ctrl = tlpEstiramiento.GetControlFromPosition(col, fila);
                    if (ctrl != null)
                        tlpEstiramiento.Controls.Remove(ctrl);
                }

                // Solo si el índice es válido
                if (fila < tlpEstiramiento.RowStyles.Count)
                    tlpEstiramiento.RowStyles.RemoveAt(fila);

                tlpEstiramiento.RowCount--;
            };

            tlpEstiramiento.Controls.Add(btnEliminarE, 2, rowIndex);
        }

        private void SeleccionarDia(string dia, Button boton)
        {
            // Limpieza del panel
            tlpCalentamiento.Controls.Clear();
            tlpCalentamiento.RowStyles.Clear();
            tlpCalentamiento.RowCount = 0;

            tlpEstiramiento.Controls.Clear();
            tlpEstiramiento.RowStyles.Clear();
            tlpEstiramiento.RowCount = 0;

            // Limpieza visual
            LimpiarBotones();
            boton.BackColor = Color.SteelBlue;

            // Buscar rutina del día
            RutinaSeleccionada = RutinasActuales.FirstOrDefault(r => r.Dia.Equals(dia, StringComparison.OrdinalIgnoreCase));

            if (RutinaSeleccionada != null)
            {
                habilitarRutina();
                msjRutina.Visible = false;
                CargarCalentamientos();
                CargarEstiramientos();

                lblUltimaFecha.Visible = true;
                lblUltimaFecha.Text = "Última fecha de modificación: " + RutinaSeleccionada.FechaModificacion.ToString("dd/MM/yyyy");
            }
            else
            {
                deshabilitarRutina();
                msjRutina.Visible = true;
                lblUltimaFecha.Visible = false;
            }
        }

        #endregion

        public frmGestionarRutinas()
        {
            InitializeComponent();
        }

        private void frmGestionarRutinas_Load(object sender, EventArgs e)
        {
            desactivarRutina();
            deshabilitarRutina();
            this.BackColor = ColorTranslator.FromHtml("#E6EAEA");
            lblMensaje.Visible = false;
            cargarCBO();
            LimpiarBotones();
        }

        private void cboRangoHorario_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvDataSocio.Rows.Clear();
            LimpiarBotones();
            desactivarRutina();
            deshabilitarRutina();

            if (cboRangoHorario.SelectedItem != null)
            {
                OpcionCombo seleccion = (OpcionCombo)cboRangoHorario.SelectedItem;

                idRangoHorarioActual = Convert.ToInt32(seleccion.Valor);

                List<Usuario> usuarios = new ControladorGymUsuario().ObtenerPorRangoHorario(Convert.ToInt32(idRangoHorarioActual));

                dgvDataEntrenador.Rows.Clear();

                foreach (var usuario in usuarios)
                {
                    dgvDataEntrenador.Rows.Add("", usuario.IdUsuario, usuario.NombreYApellido);
                }
            }
        }

        private void dgvDataEntrenador_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                bool isSelected = Convert.ToBoolean(dgvDataEntrenador.Rows[e.RowIndex].Cells["Seleccionado"].Value);

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

        private void dgvDataEntrenador_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LimpiarBotones();
            desactivarRutina();
            deshabilitarRutina();

            if (dgvDataEntrenador.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    // Desmarcar todas las filas primero
                    foreach (DataGridViewRow row in dgvDataEntrenador.Rows)
                    {
                        row.Cells["Seleccionado"].Value = false;
                    }

                    // Activar el check2 solo en la fila clickeada
                    dgvDataEntrenador.Rows[indice].Cells["Seleccionado"].Value = true;

                    // Refrescar la vista
                    dgvDataEntrenador.Refresh();

                    CargarSocios(Convert.ToInt32(dgvDataEntrenador.Rows[indice].Cells["Id"].Value));
                }
            }
        }

        private void dgvDataSocio_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                bool isSelected = Convert.ToBoolean(dgvDataSocio.Rows[e.RowIndex].Cells["Seleccionado2"].Value);

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

        private void dgvDataSocio_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDataSocio.Columns[e.ColumnIndex].Name == "btnSeleccionar2")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    // Desmarcar todas las filas primero
                    foreach (DataGridViewRow row in dgvDataSocio.Rows)
                    {
                        row.Cells["Seleccionado2"].Value = false;
                    }

                    // Activar el check2 solo en la fila clickeada
                    dgvDataSocio.Rows[indice].Cells["Seleccionado2"].Value = true;

                    // Refrescar la vista
                    dgvDataSocio.Refresh();

                    LimpiarBotones();
                    // Traer Rutinas
                    PintarBotonDiaActual();
                    activarRutina();
                    habilitarRutina();

                    IdSocioActual = Convert.ToInt32(dgvDataSocio.Rows[indice].Cells["IdSocio"].Value);

                    RutinasActuales = new ControladorGymRutina().Listar(IdSocioActual);
                    
                    // Obtener el día actual en español
                    string diaActual = DateTime.Now.DayOfWeek.ToString();

                    // Convertir el día al formato esperado
                    string diaActualEsp = TraducirDiaConTilde(diaActual);

                    RutinaSeleccionada = RutinasActuales.FirstOrDefault(r => r.Dia == diaActualEsp);


                    if (RutinaSeleccionada != null)
                    {
                        habilitarRutina();
                        msjRutina.Visible = false;
                        //MessageBox.Show($"Dia: {RutinaSeleccionada.Dia}, IdRutina: {RutinaSeleccionada.IdRutina}", "Mensaje");
                        CargarCalentamientos();
                        CargarEstiramientos();

                        lblUltimaFecha.Enabled = true;
                        lblUltimaFecha.Text = "Última fecha de modificación: " + RutinaSeleccionada.FechaModificacion.ToString("dd/MM/yyyy");
                    } else
                    {
                        msjRutina.Visible = true;
                        deshabilitarRutina();
                        lblUltimaFecha.Enabled = false;
                    }
                }
            }
        }

        private void btnLunes_Click(object sender, EventArgs e)
        {
            SeleccionarDia("Lunes", btnLunes);
        }

        private void btnMartes_Click(object sender, EventArgs e)
        {
            SeleccionarDia("Martes", btnMartes);
        }

        private void btnMiercoles_Click(object sender, EventArgs e)
        {
            SeleccionarDia("Miércoles", btnMiercoles); // ¡Con tilde!
        }

        private void btnJueves_Click(object sender, EventArgs e)
        {
            SeleccionarDia("Jueves", btnJueves);
        }

        private void btnViernes_Click(object sender, EventArgs e)
        {
            SeleccionarDia("Viernes", btnViernes);
        }

        private void btnSabado_Click(object sender, EventArgs e)
        {
            SeleccionarDia("Sábado", btnSabado); // ¡Con tilde!
        }

        private void btnAgregarFilaCalentamiento_Click(object sender, EventArgs e)
        {
            AgregarFilaCalentamiento();
        }

        private void btnAgregarFilaEntrenamiento_Click(object sender, EventArgs e)
        {
            AgregarFilaEntrenamiento();
        }

        private void btnAgregarFilaEstiramiento_Click(object sender, EventArgs e)
        {
            AgregarFilaEstiramiento();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Falta implementar para Editar --> En realidad está implemmentado porque se borra la rutina anterior antes de insertar la nueva, tiene sentido porque son la misma sol oque actualizada

            if (RutinaSeleccionada == null)
            {
                MessageBox.Show("Debe seleccionar una rutina antes de guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<RutinaCalentamiento> listaCalentamientos = ObtenerCalentamientosDesdeFormulario();
            List<RutinaEstiramiento> listaEstiramientos = ObtenerEstiramientosDesdeFormulario();

            bool exitoC = new ControladorGymRutina().GuardarCalentamientos(listaCalentamientos, out string mensaje);
            bool exitoE = new ControladorGymRutina().GuardarEstiramientos(listaEstiramientos, out string mensaje2);

            if (exitoC || exitoE)
            {
                new ControladorGymRutina().CambiarEstadoRutina(RutinaSeleccionada.IdRutina);
                MessageBox.Show("Rutina guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //MessageBox.Show($"Dia: {RutinaSeleccionada.Dia}, IdRutina: {RutinaSeleccionada.IdRutina}", "Mensaje");
                
                // No me anda para volver a cargar la fecha :C
                lblUltimaFecha.Text = "Última fecha de modificación: " + RutinaSeleccionada.FechaModificacion.ToString("dd/MM/yyyy");
            } 
            else
            {
                if (mensaje != null) MessageBox.Show("Error al guardar la rutina: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (mensaje2 != null) MessageBox.Show("Error al guardar la rutina: " + mensaje2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Error al guardar la rutina: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
