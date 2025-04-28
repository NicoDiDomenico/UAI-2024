using CapaPresentacion.Utilidades;
using Controlador;
using FontAwesome.Sharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        private Socio socioActual;
        private List<Rutina> RutinasActuales;
        private Rutina RutinaSeleccionada;
        private Usuario usuario;
        private string DiaRestaurado = "";
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
        private void CargarCalentamientos()
        {
            LimpiezaPanelCalentamiento();

            List<RutinaCalentamiento> lista = new ControladorGymCalentamiento().ListarCalentamientosPorRutina(RutinaSeleccionada.IdRutina);

            if (lista.Count() == 0) msjCalentamiento.Visible = true;
            else msjCalentamiento.Visible = false;

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
                    Font = new System.Drawing.Font("Segoe UI", 8, System.Drawing.FontStyle.Bold)
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
        private void CargarEntrenamiento()
        {
            LimpiezaPanelEntrenamiento();

            if (RutinaSeleccionada == null) return;

            List<Entrenamiento> lista = new ControladorGymEntrenamiento().ListarPorRutina(RutinaSeleccionada.IdRutina);
            
            if (lista.Count() == 0) msjEntrenamiento.Visible = true;
            else msjEntrenamiento.Visible = false;

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
                    Font = new System.Drawing.Font("Segoe UI", 8, System.Drawing.FontStyle.Bold)
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
            }
        }

        private void CargarEstiramientos()
        {
            LimpiezaPaneltlpEstiramiento();

            List<RutinaEstiramiento> lista = new ControladorGymEstiramiento().ListarEstiramientosPorRutina(RutinaSeleccionada.IdRutina);
            
            if (lista.Count() == 0) msjEstiramiento.Visible = true;
            else msjEstiramiento.Visible = false;

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
                    Font = new System.Drawing.Font("Segoe UI", 8, System.Drawing.FontStyle.Bold)
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
                Width = 600,
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
                Font = new System.Drawing.Font("Segoe UI", 8, System.Drawing.FontStyle.Bold)
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
            int rowIndex = tlpEntrenamiento.RowCount++;
            tlpEntrenamiento.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // ComboBox de Elemento (puede ser Maquina, Ejercicio o Equipamiento)
            ComboBox combo = new ComboBox
            {
                Width = 600,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(20, 5, 5, 0)
            };

            // Cargar todos los elementos del gimnasio
            List<ElementoGimnasio> elementos = new ControladorGymElementoGimnasio().Listar(); // Este método debe devolver todos los elementos
            foreach (var elem in elementos)
            {
                combo.Items.Add(new OpcionComboElementoGimnasio
                {
                    IdElemento = elem.IdElemento,
                    NombreElemento = elem.NombreElemento
                });
            }

            combo.SelectedIndex = -1;
            tlpEntrenamiento.Controls.Add(combo, 0, rowIndex);

            // Panel series
            FlowLayoutPanel panelSeries = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(10, 5, 5, 0)
            };

            NumericUpDown numSeries = new NumericUpDown
            {
                Minimum = 0,
                Maximum = 999,
                Value = 0,
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

            panelSeries.Controls.Add(numSeries);
            panelSeries.Controls.Add(lblSeries);
            tlpEntrenamiento.Controls.Add(panelSeries, 1, rowIndex);

            // Panel repeticiones
            FlowLayoutPanel panelReps = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(10, 5, 5, 0)
            };

            NumericUpDown numReps = new NumericUpDown
            {
                Minimum = 0,
                Maximum = 999,
                Value = 0,
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

            panelReps.Controls.Add(numReps);
            panelReps.Controls.Add(lblReps);
            tlpEntrenamiento.Controls.Add(panelReps, 2, rowIndex);

            // Panel peso (opcional)
            FlowLayoutPanel panelPeso = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(10, 5, 5, 0)
            };

            NumericUpDown numPeso = new NumericUpDown
            {
                Minimum = 0,
                Maximum = 999,
                Value = 0,
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

            panelPeso.Controls.Add(numPeso);
            panelPeso.Controls.Add(lblKg);
            tlpEntrenamiento.Controls.Add(panelPeso, 3, rowIndex);

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
                Font = new System.Drawing.Font("Segoe UI", 8, System.Drawing.FontStyle.Bold)
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
                TextAlign = HorizontalAlignment.Center,
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
                Font = new System.Drawing.Font("Segoe UI", 8, System.Drawing.FontStyle.Bold)
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
            LimpiezaDelPanel();

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
                CargarEntrenamiento();
                CargarEstiramientos();

                lblUltimaFecha.Visible = true;
                lblUltimaFecha.Text = "Última fecha de modificación: " + RutinaSeleccionada.FechaModificacion.ToString("dd/MM/yyyy");
            }
            else
            {
                deshabilitarRutina();
                msjCalentamiento.Visible = false;
                msjEntrenamiento.Visible = false;
                msjEstiramiento.Visible = false;
                msjRutina.Text = $"El Socio no asiste los dias {dia}.";
                msjRutina.Visible = true;
                lblUltimaFecha.Visible = false;
            }
        }

        private void AbrirFormulario(Form formulario)
        {
            formulario.StartPosition = FormStartPosition.CenterScreen; // Centra la ventana
            formulario.Load += (s, e) =>
            {
                // Ajustar la posición para que la ventana baje 50 píxeles desde la posición centrada
                formulario.Top += 95;
            };

            formulario.ShowDialog(); // Muestra como ventana modal
        }

        public void CargarRutinaDesdeHistorial(List<RutinaCalentamiento> calentamientos, List<Entrenamiento> entrenamientos, List<RutinaEstiramiento> estiramientos)
        {
            btnLimpiar.PerformClick(); // Limpia los paneles primero

            foreach (var item in calentamientos)
            {
                AgregarFilaCalentamiento();
                int fila = tlpCalentamiento.RowCount - 1;

                ComboBox combo = tlpCalentamiento.GetControlFromPosition(0, fila) as ComboBox;
                FlowLayoutPanel panel = tlpCalentamiento.GetControlFromPosition(1, fila) as FlowLayoutPanel;
                NumericUpDown minutos = panel?.Controls.OfType<NumericUpDown>().FirstOrDefault();

                if (combo != null && minutos != null)
                {
                    combo.SelectedItem = combo.Items
                        .OfType<OpcionComboCalentamiento>()
                        .FirstOrDefault(c => c.IdCalentamiento == item.IdCalentamiento);
                    minutos.Value = item.Minutos;
                }
            }

            foreach (var item in entrenamientos)
            {
                AgregarFilaEntrenamiento();
                int fila = tlpEntrenamiento.RowCount - 1;

                ComboBox combo = tlpEntrenamiento.GetControlFromPosition(0, fila) as ComboBox;
                FlowLayoutPanel panelSeries = tlpEntrenamiento.GetControlFromPosition(1, fila) as FlowLayoutPanel;
                FlowLayoutPanel panelReps = tlpEntrenamiento.GetControlFromPosition(2, fila) as FlowLayoutPanel;
                FlowLayoutPanel panelPeso = tlpEntrenamiento.GetControlFromPosition(3, fila) as FlowLayoutPanel;

                NumericUpDown nudSeries = panelSeries?.Controls.OfType<NumericUpDown>().FirstOrDefault();
                NumericUpDown nudReps = panelReps?.Controls.OfType<NumericUpDown>().FirstOrDefault();
                NumericUpDown nudPeso = panelPeso?.Controls.OfType<NumericUpDown>().FirstOrDefault();

                if (combo != null && nudSeries != null && nudReps != null && nudPeso != null)
                {
                    combo.SelectedItem = combo.Items
                        .OfType<OpcionComboElementoGimnasio>()
                        .FirstOrDefault(e => e.IdElemento == item.ElementoGimnasio.IdElemento);

                    nudSeries.Value = item.Series;
                    nudReps.Value = item.Repeticiones;
                    nudPeso.Value = item.Peso;
                }
            }

            foreach (var item in estiramientos)
            {
                AgregarFilaEstiramiento();
                int fila = tlpEstiramiento.RowCount - 1;

                ComboBox combo = tlpEstiramiento.GetControlFromPosition(0, fila) as ComboBox;
                FlowLayoutPanel panel = tlpEstiramiento.GetControlFromPosition(1, fila) as FlowLayoutPanel;
                NumericUpDown minutos = panel?.Controls.OfType<NumericUpDown>().FirstOrDefault();

                if (combo != null && minutos != null)
                {
                    combo.SelectedItem = combo.Items
                        .OfType<OpcionComboCalentamiento>()
                        .FirstOrDefault(c => c.IdCalentamiento == item.IdEstiramiento);
                    minutos.Value = item.Minutos;
                }
            }

            MessageBox.Show("Rutina restaurada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void validarPermisos()
        {
            List<Permiso> listaPermisos = new ControladorGymPermiso().Listar(usuario.IdUsuario);

            // Lista de botones a validar
            List<IconButton> botones = new List<IconButton> { btnEliminar, btnHistorial, btnGuardarRutina };

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
        private void LimpiezaPanelCalentamiento()
        {
            tlpCalentamiento.Controls.Clear();
            tlpCalentamiento.RowStyles.Clear();
            tlpCalentamiento.RowCount = 0;
        }
        private void LimpiezaPanelEntrenamiento()
        {
            tlpEntrenamiento.Controls.Clear();
            tlpEntrenamiento.RowStyles.Clear();
            tlpEntrenamiento.RowCount = 0;
        }
        private void LimpiezaPaneltlpEstiramiento()
        {
            tlpEstiramiento.Controls.Clear();
            tlpEstiramiento.RowStyles.Clear();
            tlpEstiramiento.RowCount = 0;
        }
        
        private void LimpiezaDelPanel()
        {
            LimpiezaPanelCalentamiento();
            LimpiezaPanelEntrenamiento();
            LimpiezaPaneltlpEstiramiento();
        }
        #endregion

        public frmGestionarRutinas(Usuario usuarioActual, string dia = "")
        {
            usuario = usuarioActual;
            InitializeComponent();
            DiaRestaurado = dia; // nueva variable que debés declarar
        }

        private void frmGestionarRutinas_Load(object sender, EventArgs e)
        {
            validarPermisos();
            desactivarRutina();
            deshabilitarRutina();
            this.BackColor = ColorTranslator.FromHtml("#E6EAEA");
            lblMensaje.Visible = false;
            cargarCBO();
            LimpiarBotones();

            if (!string.IsNullOrEmpty(DiaRestaurado))
            {
                switch (DiaRestaurado)
                {
                    case "Lunes": btnLunes.PerformClick(); break;
                    case "Martes": btnMartes.PerformClick(); break;
                    case "Miércoles": btnMiercoles.PerformClick(); break;
                    case "Jueves": btnJueves.PerformClick(); break;
                    case "Viernes": btnViernes.PerformClick(); break;
                    case "Sábado": btnSabado.PerformClick(); break;
                }
            }
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

                    e.Graphics.DrawImage(Properties.Resources.check2, new System.Drawing.Rectangle(x, y, w, h));
                }
                e.Handled = true;
            }
        }

        private void dgvDataEntrenador_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDataEntrenador.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                LimpiezaDelPanel();
                LimpiarBotones();
                desactivarRutina();
                deshabilitarRutina();

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

                    e.Graphics.DrawImage(Properties.Resources.check2, new System.Drawing.Rectangle(x, y, w, h));
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

                    LimpiezaDelPanel();

                    LimpiarBotones();
                    // Traer Rutinas
                    PintarBotonDiaActual();
                    activarRutina();
                    habilitarRutina();

                    IdSocioActual = Convert.ToInt32(dgvDataSocio.Rows[indice].Cells["IdSocio"].Value);

                    RutinasActuales = new ControladorGymRutina().Listar(IdSocioActual);

                    socioActual = new ControladorGymSocio().GetSocio(IdSocioActual);
                    
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
                        CargarEntrenamiento();
                        CargarEstiramientos();

                        lblUltimaFecha.Visible = true;
                        lblUltimaFecha.Text = "Última fecha de modificación: " + RutinaSeleccionada.FechaModificacion.ToString("dd/MM/yyyy");
                    } else
                    {
                        msjCalentamiento.Visible = false;
                        msjEntrenamiento.Visible = false;
                        msjEstiramiento.Visible = false;
                        msjRutina.Text = $"El Socio no asiste los dias {diaActualEsp}.";
                        msjRutina.Visible = true;
                        deshabilitarRutina();
                        lblUltimaFecha.Visible = false;
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
            msjCalentamiento.Visible = false;
            AgregarFilaCalentamiento();
        }

        private void btnAgregarFilaEntrenamiento_Click(object sender, EventArgs e)
        {
            msjEntrenamiento.Visible = false;
            AgregarFilaEntrenamiento();
        }

        private void btnAgregarFilaEstiramiento_Click(object sender, EventArgs e)
        {
            msjEstiramiento.Visible = false;
            AgregarFilaEstiramiento();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Falta implementar para Editar --> En realidad está implemmentado porque se borra la rutina anterior antes de insertar la nueva, tiene sentido porque son la misma sol oque actualizada
            // Cuando guardo un tlb vacio se confu¿irma el alta pero se carga como estaba antes

            if (RutinaSeleccionada == null)
            {
                MessageBox.Show("Debe seleccionar una rutina antes de guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<RutinaCalentamiento> listaCalentamientos = ObtenerCalentamientosDesdeFormulario();
            List<Entrenamiento> listaEntrenamientos = ObtenerEntrenamientosDesdeFormulario();
            List<RutinaEstiramiento> listaEstiramientos = ObtenerEstiramientosDesdeFormulario();

            bool exitoC = new ControladorGymRutina().GuardarCalentamientos(listaCalentamientos, out string mensajeC);
            bool exitoT = new ControladorGymRutina().GuardarEntrenamientos(listaEntrenamientos, out string mensajeT);
            bool exitoE = new ControladorGymRutina().GuardarEstiramientos(listaEstiramientos, out string mensajeE);

            if (exitoC || exitoT || exitoE)
            {
                ControladorGymHistorialRutinas controladorHistorial = new ControladorGymHistorialRutinas();
                int idHistorial = controladorHistorial.CrearHistorialRutina(IdSocioActual, RutinaSeleccionada.Dia, out string mensajeHistorial);

                if (idHistorial > 0)
                {
                    bool exitoHC = controladorHistorial.GuardarHistorialCalentamientos(idHistorial, listaCalentamientos, out string mensajeHC);
                    bool exitoHT = controladorHistorial.GuardarHistorialEntrenamientos(idHistorial, listaEntrenamientos, out string mensajeHT);
                    bool exitoHE = controladorHistorial.GuardarHistorialEstiramientos(idHistorial, listaEstiramientos, out string mensajeHE);

                    if (!exitoHC || !exitoHT || !exitoHE)
                    {
                        if (!exitoHC) MessageBox.Show("Error al guardar historial de calentamientos: " + mensajeHC, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (!exitoHT) MessageBox.Show("Error al guardar historial de entrenamientos: " + mensajeHT, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (!exitoHE) MessageBox.Show("Error al guardar historial de estiramientos: " + mensajeHE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo crear el historial de rutina: " + mensajeHistorial, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                new ControladorGymRutina().CambiarEstadoRutina(RutinaSeleccionada.IdRutina);
                RutinaSeleccionada.FechaModificacion = DateTime.Now;
                new ControladorGymSocio().ActualizarEstadoSocio(IdSocioActual, "Actualizado");

                MessageBox.Show("Rutina guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblUltimaFecha.Text = "Última fecha de modificación: " + DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                if (!exitoC && mensajeC != null)
                    MessageBox.Show("Error al guardar calentamientos: " + mensajeC, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!exitoT && mensajeT != null)
                    MessageBox.Show("Error al guardar entrenamientos: " + mensajeT, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!exitoE && mensajeE != null)
                    MessageBox.Show("Error al guardar estiramientos: " + mensajeE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            msjCalentamiento.Visible = false;
            msjEntrenamiento.Visible = false;
            msjEstiramiento.Visible = false;
            LimpiezaDelPanel();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Podria hacer que se 
            
            if (RutinaSeleccionada != null)
            {
                DialogResult confirmacion = MessageBox.Show(
                    $"¿Está seguro de que desea eliminar la rutina del día {RutinaSeleccionada.Dia}?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    bool rta = new ControladorGymRutina().DesactivarRutina(RutinaSeleccionada.IdRutina);

                    if (rta)
                    {
                        // Limpiar visualmente la rutina eliminada
                        LimpiezaDelPanel();

                        lblUltimaFecha.Text = "Última fecha de modificación: " + DateTime.Now.ToString("dd/MM/yyyy");
                        msjRutina.Visible = true;
                        deshabilitarRutina();

                        // Eliminar de la lista actual y desasignar rutina seleccionada
                        //RutinasActuales.Remove(RutinaSeleccionada);
                        //RutinaSeleccionada = null;

                        MessageBox.Show("La rutina fue eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error al intentar eliminar la rutina.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una rutina antes de eliminarla.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            // Pasás 'this' como referencia al formulario padre
            //var historial = new frmHistorialRutinas(this, IdSocioActual, RutinaSeleccionada.Dia);
            //historial.ShowDialog();
            AbrirFormulario(new frmHistorialRutinas(this, IdSocioActual, RutinaSeleccionada.Dia, usuario));
        }

        // Falta compilar, no lo hice porque era domingo y no sea visiaulizaba el btnReporte al no poder cargar socios.
        private void btnReporte_Click(object sender, EventArgs e)
        {
            if (RutinaSeleccionada == null)
            {
                MessageBox.Show("Debe seleccionar una rutina antes de generar el reporte.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = "Rutina_" + socioActual.NombreYApellido.Replace(" ", "_") + "_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".pdf";
            guardar.Filter = "Archivos PDF (*.pdf)|*.pdf";

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                // 1. Cargar la plantilla
                string paginahtml_texto = Properties.Resources.plantillaRutina.ToString();
                paginahtml_texto = paginahtml_texto.Replace("@NOMBRE", socioActual.NombreYApellido.ToUpper());
                paginahtml_texto = paginahtml_texto.Replace("@DIRECCION", socioActual.Direccion);
                paginahtml_texto = paginahtml_texto.Replace("@TELEFONO", socioActual.Telefono);
                paginahtml_texto = paginahtml_texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));

                // 2. Reemplazar las secciones dinámicamente

                // Calentamientos
                List<RutinaCalentamiento> listaCalentamientos = ObtenerCalentamientosDesdeFormulario();
                StringBuilder filasCalentamientos = new StringBuilder();

                foreach (var cal in listaCalentamientos)
                {
                    string descripcion = new ControladorGymCalentamiento().ObtenerPorId(cal.IdCalentamiento)?.DescripcionCalentamiento ?? "N/A";
                    filasCalentamientos.AppendLine($"<tr><td>{descripcion}</td><td>{cal.Minutos}</td></tr>");
                }

                if (filasCalentamientos.Length == 0)
                    filasCalentamientos.AppendLine("<tr><td colspan='2'>No se registraron calentamientos.</td></tr>");

                paginahtml_texto = paginahtml_texto.Replace("@CALENTAMIENTOS", filasCalentamientos.ToString());

                // Entrenamientos
                List<Entrenamiento> listaEntrenamientos = ObtenerEntrenamientosDesdeFormulario();
                StringBuilder filasEntrenamientos = new StringBuilder();

                foreach (var ent in listaEntrenamientos)
                {
                    filasEntrenamientos.AppendLine($"<tr><td>{ent.ElementoGimnasio.NombreElemento}</td><td>{ent.Series}</td><td>{ent.Repeticiones}</td><td>{ent.Peso}</td></tr>");
                }

                if (filasEntrenamientos.Length == 0)
                    filasEntrenamientos.AppendLine("<tr><td colspan='4'>No se registraron entrenamientos.</td></tr>");

                paginahtml_texto = paginahtml_texto.Replace("@ENTRENAMIENTOS", filasEntrenamientos.ToString());

                // Estiramientos
                List<RutinaEstiramiento> listaEstiramientos = ObtenerEstiramientosDesdeFormulario();
                StringBuilder filasEstiramientos = new StringBuilder();

                foreach (var est in listaEstiramientos)
                {
                    string descripcion = new ControladorGymEstiramiento().ObtenerPorId(est.IdEstiramiento)?.DescripcionEstiramiento ?? "N/A";
                    filasEstiramientos.AppendLine($"<tr><td>{descripcion}</td><td>{est.Minutos}</td></tr>");
                }

                if (filasEstiramientos.Length == 0)
                    filasEstiramientos.AppendLine("<tr><td colspan='2'>No se registraron estiramientos.</td></tr>");

                paginahtml_texto = paginahtml_texto.Replace("@ESTIRAMIENTOS", filasEstiramientos.ToString());

                // 3. Crear el PDF
                using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase("")); // Espacio arriba

                    using (StringReader sr = new StringReader(paginahtml_texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                }

                MessageBox.Show("Reporte generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
