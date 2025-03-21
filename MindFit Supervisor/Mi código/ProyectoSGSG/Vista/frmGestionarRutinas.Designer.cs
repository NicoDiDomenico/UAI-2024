namespace Vista
{
    partial class frmGestionarRutinas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle61 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle62 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle63 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle64 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle65 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle66 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle67 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle68 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle69 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle70 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cboRangoHorario = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvDataEntrenador = new System.Windows.Forms.DataGridView();
            this.btnSeleccionar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreYApellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Seleccionado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.dgvDataSocio = new System.Windows.Forms.DataGridView();
            this.btnSeleccionar2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.IdSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Seleccionado2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panelDias = new System.Windows.Forms.Panel();
            this.btnJueves = new FontAwesome.Sharp.IconButton();
            this.btnViernes = new FontAwesome.Sharp.IconButton();
            this.btnSabado = new FontAwesome.Sharp.IconButton();
            this.btnLunes = new FontAwesome.Sharp.IconButton();
            this.btnMiercoles = new FontAwesome.Sharp.IconButton();
            this.btnMartes = new FontAwesome.Sharp.IconButton();
            this.panelRutinas = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataEntrenador)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataSocio)).BeginInit();
            this.panelDias.SuspendLayout();
            this.panelRutinas.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboRangoHorario
            // 
            this.cboRangoHorario.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cboRangoHorario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRangoHorario.FormattingEnabled = true;
            this.cboRangoHorario.Location = new System.Drawing.Point(70, 10);
            this.cboRangoHorario.Name = "cboRangoHorario";
            this.cboRangoHorario.Size = new System.Drawing.Size(213, 21);
            this.cboRangoHorario.TabIndex = 65;
            this.cboRangoHorario.SelectedIndexChanged += new System.EventHandler(this.cboRangoHorario_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(96)))), ((int)(((byte)(146)))));
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(0, 8);
            this.label13.Name = "label13";
            this.label13.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.label13.Size = new System.Drawing.Size(290, 25);
            this.label13.TabIndex = 64;
            this.label13.Text = "Turno";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboRangoHorario);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Location = new System.Drawing.Point(6, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 39);
            this.panel1.TabIndex = 66;
            // 
            // dgvDataEntrenador
            // 
            this.dgvDataEntrenador.AllowUserToAddRows = false;
            dataGridViewCellStyle61.BackColor = System.Drawing.Color.White;
            this.dgvDataEntrenador.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle61;
            this.dgvDataEntrenador.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.dgvDataEntrenador.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle62.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle62.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            dataGridViewCellStyle62.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle62.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle62.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle62.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle62.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle62.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDataEntrenador.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle62;
            this.dgvDataEntrenador.ColumnHeadersHeight = 50;
            this.dgvDataEntrenador.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnSeleccionar,
            this.Id,
            this.NombreYApellido,
            this.Seleccionado});
            dataGridViewCellStyle63.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle63.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            dataGridViewCellStyle63.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle63.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle63.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle63.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle63.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDataEntrenador.DefaultCellStyle = dataGridViewCellStyle63;
            this.dgvDataEntrenador.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.dgvDataEntrenador.Location = new System.Drawing.Point(11, 54);
            this.dgvDataEntrenador.MultiSelect = false;
            this.dgvDataEntrenador.Name = "dgvDataEntrenador";
            this.dgvDataEntrenador.ReadOnly = true;
            dataGridViewCellStyle64.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle64.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            dataGridViewCellStyle64.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle64.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle64.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle64.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle64.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDataEntrenador.RowHeadersDefaultCellStyle = dataGridViewCellStyle64;
            this.dgvDataEntrenador.RowHeadersVisible = false;
            dataGridViewCellStyle65.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle65.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle65.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle65.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle65.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle65.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDataEntrenador.RowsDefaultCellStyle = dataGridViewCellStyle65;
            this.dgvDataEntrenador.RowTemplate.Height = 35;
            this.dgvDataEntrenador.Size = new System.Drawing.Size(281, 677);
            this.dgvDataEntrenador.TabIndex = 93;
            this.dgvDataEntrenador.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDataEntrenador_CellContentClick);
            this.dgvDataEntrenador.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvDataEntrenador_CellPainting);
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.HeaderText = "";
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.ReadOnly = true;
            this.btnSeleccionar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnSeleccionar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btnSeleccionar.Width = 35;
            // 
            // Id
            // 
            this.Id.HeaderText = "IdUsuario";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // NombreYApellido
            // 
            this.NombreYApellido.HeaderText = "Entrenador";
            this.NombreYApellido.Name = "NombreYApellido";
            this.NombreYApellido.ReadOnly = true;
            this.NombreYApellido.Width = 243;
            // 
            // Seleccionado
            // 
            this.Seleccionado.HeaderText = "Seleccionado";
            this.Seleccionado.Name = "Seleccionado";
            this.Seleccionado.ReadOnly = true;
            this.Seleccionado.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.label1.Location = new System.Drawing.Point(6, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1776, 689);
            this.label1.TabIndex = 95;
            this.label1.Text = "label1";
            // 
            // lblMensaje
            // 
            this.lblMensaje.BackColor = System.Drawing.Color.White;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.ForeColor = System.Drawing.Color.Black;
            this.lblMensaje.Location = new System.Drawing.Point(300, 93);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(275, 600);
            this.lblMensaje.TabIndex = 98;
            this.lblMensaje.Text = "No hay Socios actualmente.";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMensaje.Visible = false;
            // 
            // dgvDataSocio
            // 
            this.dgvDataSocio.AllowUserToAddRows = false;
            dataGridViewCellStyle66.BackColor = System.Drawing.Color.White;
            this.dgvDataSocio.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle66;
            this.dgvDataSocio.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.dgvDataSocio.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle67.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle67.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            dataGridViewCellStyle67.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle67.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle67.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle67.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle67.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle67.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDataSocio.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle67;
            this.dgvDataSocio.ColumnHeadersHeight = 50;
            this.dgvDataSocio.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnSeleccionar2,
            this.IdSocio,
            this.NombreSocio,
            this.Seleccionado2});
            dataGridViewCellStyle68.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle68.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            dataGridViewCellStyle68.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle68.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle68.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle68.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle68.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDataSocio.DefaultCellStyle = dataGridViewCellStyle68;
            this.dgvDataSocio.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.dgvDataSocio.Location = new System.Drawing.Point(296, 54);
            this.dgvDataSocio.MultiSelect = false;
            this.dgvDataSocio.Name = "dgvDataSocio";
            this.dgvDataSocio.ReadOnly = true;
            dataGridViewCellStyle69.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle69.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            dataGridViewCellStyle69.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle69.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle69.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle69.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle69.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDataSocio.RowHeadersDefaultCellStyle = dataGridViewCellStyle69;
            this.dgvDataSocio.RowHeadersVisible = false;
            dataGridViewCellStyle70.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle70.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle70.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle70.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle70.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle70.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDataSocio.RowsDefaultCellStyle = dataGridViewCellStyle70;
            this.dgvDataSocio.RowTemplate.Height = 35;
            this.dgvDataSocio.Size = new System.Drawing.Size(281, 677);
            this.dgvDataSocio.TabIndex = 99;
            this.dgvDataSocio.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDataSocio_CellContentClick);
            this.dgvDataSocio.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvDataSocio_CellPainting);
            // 
            // btnSeleccionar2
            // 
            this.btnSeleccionar2.HeaderText = "";
            this.btnSeleccionar2.Name = "btnSeleccionar2";
            this.btnSeleccionar2.ReadOnly = true;
            this.btnSeleccionar2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnSeleccionar2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btnSeleccionar2.Width = 35;
            // 
            // IdSocio
            // 
            this.IdSocio.HeaderText = "IdSocio";
            this.IdSocio.Name = "IdSocio";
            this.IdSocio.ReadOnly = true;
            this.IdSocio.Visible = false;
            // 
            // NombreSocio
            // 
            this.NombreSocio.HeaderText = "Socio";
            this.NombreSocio.Name = "NombreSocio";
            this.NombreSocio.ReadOnly = true;
            this.NombreSocio.Width = 243;
            // 
            // Seleccionado2
            // 
            this.Seleccionado2.HeaderText = "Seleccionado";
            this.Seleccionado2.Name = "Seleccionado2";
            this.Seleccionado2.ReadOnly = true;
            this.Seleccionado2.Visible = false;
            // 
            // panelDias
            // 
            this.panelDias.BackColor = System.Drawing.Color.Black;
            this.panelDias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDias.Controls.Add(this.btnMartes);
            this.panelDias.Controls.Add(this.btnSabado);
            this.panelDias.Controls.Add(this.btnViernes);
            this.panelDias.Controls.Add(this.btnJueves);
            this.panelDias.Controls.Add(this.btnMiercoles);
            this.panelDias.Controls.Add(this.btnLunes);
            this.panelDias.Location = new System.Drawing.Point(0, 0);
            this.panelDias.Name = "panelDias";
            this.panelDias.Size = new System.Drawing.Size(1195, 52);
            this.panelDias.TabIndex = 100;
            // 
            // btnJueves
            // 
            this.btnJueves.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.btnJueves.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJueves.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnJueves.ForeColor = System.Drawing.Color.Black;
            this.btnJueves.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnJueves.IconColor = System.Drawing.Color.Black;
            this.btnJueves.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnJueves.Location = new System.Drawing.Point(595, 0);
            this.btnJueves.Name = "btnJueves";
            this.btnJueves.Size = new System.Drawing.Size(200, 50);
            this.btnJueves.TabIndex = 3;
            this.btnJueves.Text = "Jueves";
            this.btnJueves.UseVisualStyleBackColor = false;
            this.btnJueves.Click += new System.EventHandler(this.btnJueves_Click);
            // 
            // btnViernes
            // 
            this.btnViernes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.btnViernes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViernes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnViernes.ForeColor = System.Drawing.Color.Black;
            this.btnViernes.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnViernes.IconColor = System.Drawing.Color.Black;
            this.btnViernes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnViernes.Location = new System.Drawing.Point(794, 0);
            this.btnViernes.Name = "btnViernes";
            this.btnViernes.Size = new System.Drawing.Size(200, 50);
            this.btnViernes.TabIndex = 4;
            this.btnViernes.Text = "Viernes";
            this.btnViernes.UseVisualStyleBackColor = false;
            this.btnViernes.Click += new System.EventHandler(this.btnViernes_Click);
            // 
            // btnSabado
            // 
            this.btnSabado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.btnSabado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSabado.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnSabado.ForeColor = System.Drawing.Color.Black;
            this.btnSabado.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnSabado.IconColor = System.Drawing.Color.Black;
            this.btnSabado.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSabado.Location = new System.Drawing.Point(993, 0);
            this.btnSabado.Name = "btnSabado";
            this.btnSabado.Size = new System.Drawing.Size(200, 50);
            this.btnSabado.TabIndex = 5;
            this.btnSabado.Text = "Sábado";
            this.btnSabado.UseVisualStyleBackColor = false;
            this.btnSabado.Click += new System.EventHandler(this.btnSabado_Click);
            // 
            // btnLunes
            // 
            this.btnLunes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.btnLunes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLunes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnLunes.ForeColor = System.Drawing.Color.Black;
            this.btnLunes.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnLunes.IconColor = System.Drawing.Color.Black;
            this.btnLunes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLunes.Location = new System.Drawing.Point(0, 0);
            this.btnLunes.Margin = new System.Windows.Forms.Padding(0);
            this.btnLunes.Name = "btnLunes";
            this.btnLunes.Size = new System.Drawing.Size(200, 50);
            this.btnLunes.TabIndex = 0;
            this.btnLunes.Text = "Lunes";
            this.btnLunes.UseVisualStyleBackColor = false;
            this.btnLunes.Click += new System.EventHandler(this.btnLunes_Click);
            // 
            // btnMiercoles
            // 
            this.btnMiercoles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.btnMiercoles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMiercoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnMiercoles.ForeColor = System.Drawing.Color.Black;
            this.btnMiercoles.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnMiercoles.IconColor = System.Drawing.Color.Black;
            this.btnMiercoles.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMiercoles.Location = new System.Drawing.Point(396, 0);
            this.btnMiercoles.Name = "btnMiercoles";
            this.btnMiercoles.Size = new System.Drawing.Size(200, 50);
            this.btnMiercoles.TabIndex = 2;
            this.btnMiercoles.Text = "Miércoles";
            this.btnMiercoles.UseVisualStyleBackColor = false;
            this.btnMiercoles.Click += new System.EventHandler(this.btnMiercoles_Click);
            // 
            // btnMartes
            // 
            this.btnMartes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.btnMartes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMartes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnMartes.ForeColor = System.Drawing.Color.Black;
            this.btnMartes.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnMartes.IconColor = System.Drawing.Color.Black;
            this.btnMartes.IconFont = FontAwesome.Sharp.IconFont.Regular;
            this.btnMartes.Location = new System.Drawing.Point(199, 0);
            this.btnMartes.Margin = new System.Windows.Forms.Padding(0);
            this.btnMartes.Name = "btnMartes";
            this.btnMartes.Size = new System.Drawing.Size(198, 50);
            this.btnMartes.TabIndex = 1;
            this.btnMartes.Text = "Martes";
            this.btnMartes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMartes.UseVisualStyleBackColor = false;
            this.btnMartes.Click += new System.EventHandler(this.btnMartes_Click);
            // 
            // panelRutinas
            // 
            this.panelRutinas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.panelRutinas.Controls.Add(this.panelDias);
            this.panelRutinas.Location = new System.Drawing.Point(580, 54);
            this.panelRutinas.Name = "panelRutinas";
            this.panelRutinas.Size = new System.Drawing.Size(1196, 624);
            this.panelRutinas.TabIndex = 101;
            // 
            // frmGestionarRutinas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.ClientSize = new System.Drawing.Size(1788, 740);
            this.Controls.Add(this.panelRutinas);
            this.Controls.Add(this.dgvDataSocio);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.dgvDataEntrenador);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.Name = "frmGestionarRutinas";
            this.Text = "frmGestionarRutinas";
            this.Load += new System.EventHandler(this.frmGestionarRutinas_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataEntrenador)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataSocio)).EndInit();
            this.panelDias.ResumeLayout(false);
            this.panelRutinas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboRangoHorario;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvDataEntrenador;
        private System.Windows.Forms.DataGridViewButtonColumn btnSeleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreYApellido;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionado;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.DataGridView dgvDataSocio;
        private System.Windows.Forms.DataGridViewButtonColumn btnSeleccionar2;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdSocio;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreSocio;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionado2;
        private System.Windows.Forms.Panel panelDias;
        private FontAwesome.Sharp.IconButton btnSabado;
        private FontAwesome.Sharp.IconButton btnViernes;
        private FontAwesome.Sharp.IconButton btnJueves;
        private FontAwesome.Sharp.IconButton btnMartes;
        private FontAwesome.Sharp.IconButton btnMiercoles;
        private FontAwesome.Sharp.IconButton btnLunes;
        private System.Windows.Forms.Panel panelRutinas;
    }
}