namespace Vista
{
    partial class frmSocios
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cboBusqueda = new System.Windows.Forms.ComboBox();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.btnBuscar = new FontAwesome.Sharp.IconButton();
            this.btnLimpiarBuscador = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnSeleccionar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.IdSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreYApellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaNacimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Genero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NroDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ciudad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObraSocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Plan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaInicioActividades = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaFinActividades = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaNotificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RespuestaNotificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Seleccionado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ckbMostrarEliminados = new System.Windows.Forms.CheckBox();
            this.btnMenuTurno = new FontAwesome.Sharp.IconButton();
            this.btnMenuConsultar = new FontAwesome.Sharp.IconButton();
            this.btnMenuEliminar = new FontAwesome.Sharp.IconButton();
            this.btnMenuAgregar = new FontAwesome.Sharp.IconButton();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.label10.Location = new System.Drawing.Point(120, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1539, 651);
            this.label10.TabIndex = 58;
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.cboBusqueda);
            this.groupBox2.Controls.Add(this.txtBusqueda);
            this.groupBox2.Controls.Add(this.btnBuscar);
            this.groupBox2.Controls.Add(this.btnLimpiarBuscador);
            this.groupBox2.Location = new System.Drawing.Point(1036, 44);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(546, 59);
            this.groupBox2.TabIndex = 59;
            this.groupBox2.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label11.Location = new System.Drawing.Point(17, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 17);
            this.label11.TabIndex = 50;
            this.label11.Text = "Buscar Por:";
            // 
            // cboBusqueda
            // 
            this.cboBusqueda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBusqueda.FormattingEnabled = true;
            this.cboBusqueda.Location = new System.Drawing.Point(105, 22);
            this.cboBusqueda.Name = "cboBusqueda";
            this.cboBusqueda.Size = new System.Drawing.Size(127, 21);
            this.cboBusqueda.TabIndex = 51;
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Location = new System.Drawing.Point(238, 22);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(171, 20);
            this.txtBusqueda.TabIndex = 52;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.White;
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.IconChar = FontAwesome.Sharp.IconChar.Searchengin;
            this.btnBuscar.IconColor = System.Drawing.Color.Black;
            this.btnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscar.IconSize = 20;
            this.btnBuscar.Location = new System.Drawing.Point(418, 22);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 20);
            this.btnBuscar.TabIndex = 54;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnLimpiarBuscador
            // 
            this.btnLimpiarBuscador.BackColor = System.Drawing.Color.White;
            this.btnLimpiarBuscador.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiarBuscador.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLimpiarBuscador.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarBuscador.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarBuscador.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.btnLimpiarBuscador.IconColor = System.Drawing.Color.Black;
            this.btnLimpiarBuscador.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLimpiarBuscador.IconSize = 20;
            this.btnLimpiarBuscador.Location = new System.Drawing.Point(464, 22);
            this.btnLimpiarBuscador.Name = "btnLimpiarBuscador";
            this.btnLimpiarBuscador.Size = new System.Drawing.Size(40, 20);
            this.btnLimpiarBuscador.TabIndex = 53;
            this.btnLimpiarBuscador.UseVisualStyleBackColor = false;
            this.btnLimpiarBuscador.Click += new System.EventHandler(this.btnLimpiarBuscador_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(163, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 31);
            this.label1.TabIndex = 60;
            this.label1.Text = "Lista de Socios:";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.dgvData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dgvData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnSeleccionar,
            this.IdSocio,
            this.NombreYApellido,
            this.FechaNacimiento,
            this.Genero,
            this.NroDocumento,
            this.Ciudad,
            this.Direccion,
            this.Telefono,
            this.Email,
            this.ObraSocial,
            this.Plan,
            this.EstadoSocio,
            this.FechaInicioActividades,
            this.FechaFinActividades,
            this.FechaNotificacion,
            this.RespuestaNotificacion,
            this.Seleccionado});
            this.dgvData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.dgvData.Location = new System.Drawing.Point(121, 126);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvData.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvData.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.dgvData.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvData.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvData.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.dgvData.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvData.RowTemplate.Height = 35;
            this.dgvData.Size = new System.Drawing.Size(1537, 553);
            this.dgvData.TabIndex = 65;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick_1);
            this.dgvData.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvData_CellPainting_1);
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.HeaderText = "";
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.ReadOnly = true;
            this.btnSeleccionar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnSeleccionar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btnSeleccionar.Width = 34;
            // 
            // IdSocio
            // 
            this.IdSocio.HeaderText = "IdSocio";
            this.IdSocio.Name = "IdSocio";
            this.IdSocio.ReadOnly = true;
            this.IdSocio.Visible = false;
            // 
            // NombreYApellido
            // 
            this.NombreYApellido.HeaderText = "Socio";
            this.NombreYApellido.Name = "NombreYApellido";
            this.NombreYApellido.ReadOnly = true;
            this.NombreYApellido.Width = 500;
            // 
            // FechaNacimiento
            // 
            this.FechaNacimiento.HeaderText = "FechaNacimiento";
            this.FechaNacimiento.Name = "FechaNacimiento";
            this.FechaNacimiento.ReadOnly = true;
            this.FechaNacimiento.Visible = false;
            // 
            // Genero
            // 
            this.Genero.HeaderText = "Genero";
            this.Genero.Name = "Genero";
            this.Genero.ReadOnly = true;
            this.Genero.Visible = false;
            // 
            // NroDocumento
            // 
            this.NroDocumento.HeaderText = "NroDocumento";
            this.NroDocumento.Name = "NroDocumento";
            this.NroDocumento.ReadOnly = true;
            this.NroDocumento.Visible = false;
            // 
            // Ciudad
            // 
            this.Ciudad.HeaderText = "Ciudad";
            this.Ciudad.Name = "Ciudad";
            this.Ciudad.ReadOnly = true;
            this.Ciudad.Visible = false;
            // 
            // Direccion
            // 
            this.Direccion.HeaderText = "Direccion";
            this.Direccion.Name = "Direccion";
            this.Direccion.ReadOnly = true;
            this.Direccion.Visible = false;
            // 
            // Telefono
            // 
            this.Telefono.HeaderText = "Telefono";
            this.Telefono.Name = "Telefono";
            this.Telefono.ReadOnly = true;
            this.Telefono.Visible = false;
            // 
            // Email
            // 
            this.Email.HeaderText = "Email";
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            this.Email.Visible = false;
            // 
            // ObraSocial
            // 
            this.ObraSocial.HeaderText = "ObraSocial";
            this.ObraSocial.Name = "ObraSocial";
            this.ObraSocial.ReadOnly = true;
            this.ObraSocial.Visible = false;
            // 
            // Plan
            // 
            this.Plan.HeaderText = "Plan";
            this.Plan.Name = "Plan";
            this.Plan.ReadOnly = true;
            this.Plan.Visible = false;
            // 
            // EstadoSocio
            // 
            this.EstadoSocio.HeaderText = "Estado";
            this.EstadoSocio.Name = "EstadoSocio";
            this.EstadoSocio.ReadOnly = true;
            this.EstadoSocio.Width = 500;
            // 
            // FechaInicioActividades
            // 
            this.FechaInicioActividades.HeaderText = "FechaInicioActividades";
            this.FechaInicioActividades.Name = "FechaInicioActividades";
            this.FechaInicioActividades.ReadOnly = true;
            this.FechaInicioActividades.Visible = false;
            // 
            // FechaFinActividades
            // 
            this.FechaFinActividades.HeaderText = "Fecha Vencimiento Cuota";
            this.FechaFinActividades.Name = "FechaFinActividades";
            this.FechaFinActividades.ReadOnly = true;
            this.FechaFinActividades.Width = 503;
            // 
            // FechaNotificacion
            // 
            this.FechaNotificacion.HeaderText = "FechaNotificacion";
            this.FechaNotificacion.Name = "FechaNotificacion";
            this.FechaNotificacion.ReadOnly = true;
            this.FechaNotificacion.Visible = false;
            // 
            // RespuestaNotificacion
            // 
            this.RespuestaNotificacion.HeaderText = "RespuestaNotificacion";
            this.RespuestaNotificacion.Name = "RespuestaNotificacion";
            this.RespuestaNotificacion.ReadOnly = true;
            this.RespuestaNotificacion.Visible = false;
            // 
            // Seleccionado
            // 
            this.Seleccionado.HeaderText = "Seleccionado";
            this.Seleccionado.Name = "Seleccionado";
            this.Seleccionado.ReadOnly = true;
            this.Seleccionado.Visible = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(117, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1545, 660);
            this.label2.TabIndex = 66;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel3.Controls.Add(this.ckbMostrarEliminados);
            this.panel3.Controls.Add(this.btnMenuTurno);
            this.panel3.Controls.Add(this.btnMenuConsultar);
            this.panel3.Controls.Add(this.btnMenuEliminar);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1804, 896);
            this.panel3.TabIndex = 95;
            // 
            // ckbMostrarEliminados
            // 
            this.ckbMostrarEliminados.AutoSize = true;
            this.ckbMostrarEliminados.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbMostrarEliminados.ForeColor = System.Drawing.Color.Black;
            this.ckbMostrarEliminados.Location = new System.Drawing.Point(928, 721);
            this.ckbMostrarEliminados.Name = "ckbMostrarEliminados";
            this.ckbMostrarEliminados.Size = new System.Drawing.Size(207, 22);
            this.ckbMostrarEliminados.TabIndex = 0;
            this.ckbMostrarEliminados.Text = "Mostrar Socios Eliminados";
            this.ckbMostrarEliminados.UseVisualStyleBackColor = true;
            this.ckbMostrarEliminados.CheckedChanged += new System.EventHandler(this.ckbMostrarEliminados_CheckedChanged);
            // 
            // btnMenuTurno
            // 
            this.btnMenuTurno.BackColor = System.Drawing.Color.AliceBlue;
            this.btnMenuTurno.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuTurno.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnMenuTurno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuTurno.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuTurno.ForeColor = System.Drawing.Color.Black;
            this.btnMenuTurno.IconChar = FontAwesome.Sharp.IconChar.ClipboardList;
            this.btnMenuTurno.IconColor = System.Drawing.Color.Black;
            this.btnMenuTurno.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMenuTurno.IconSize = 30;
            this.btnMenuTurno.Location = new System.Drawing.Point(1344, 705);
            this.btnMenuTurno.Name = "btnMenuTurno";
            this.btnMenuTurno.Size = new System.Drawing.Size(150, 50);
            this.btnMenuTurno.TabIndex = 96;
            this.btnMenuTurno.Text = "Turnos";
            this.btnMenuTurno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMenuTurno.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMenuTurno.UseVisualStyleBackColor = false;
            this.btnMenuTurno.Click += new System.EventHandler(this.btnTurno_Click);
            // 
            // btnMenuConsultar
            // 
            this.btnMenuConsultar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnMenuConsultar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuConsultar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnMenuConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuConsultar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuConsultar.ForeColor = System.Drawing.Color.White;
            this.btnMenuConsultar.IconChar = FontAwesome.Sharp.IconChar.UserEdit;
            this.btnMenuConsultar.IconColor = System.Drawing.Color.White;
            this.btnMenuConsultar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMenuConsultar.IconSize = 30;
            this.btnMenuConsultar.Location = new System.Drawing.Point(530, 705);
            this.btnMenuConsultar.Name = "btnMenuConsultar";
            this.btnMenuConsultar.Size = new System.Drawing.Size(150, 50);
            this.btnMenuConsultar.TabIndex = 62;
            this.btnMenuConsultar.Text = "Consultar";
            this.btnMenuConsultar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMenuConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMenuConsultar.UseVisualStyleBackColor = false;
            this.btnMenuConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // btnMenuEliminar
            // 
            this.btnMenuEliminar.BackColor = System.Drawing.Color.Firebrick;
            this.btnMenuEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuEliminar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnMenuEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuEliminar.ForeColor = System.Drawing.Color.White;
            this.btnMenuEliminar.IconChar = FontAwesome.Sharp.IconChar.UserXmark;
            this.btnMenuEliminar.IconColor = System.Drawing.Color.White;
            this.btnMenuEliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMenuEliminar.IconSize = 30;
            this.btnMenuEliminar.Location = new System.Drawing.Point(742, 705);
            this.btnMenuEliminar.Name = "btnMenuEliminar";
            this.btnMenuEliminar.Size = new System.Drawing.Size(150, 50);
            this.btnMenuEliminar.TabIndex = 64;
            this.btnMenuEliminar.Text = "Eliminar";
            this.btnMenuEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMenuEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMenuEliminar.UseVisualStyleBackColor = false;
            this.btnMenuEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnMenuAgregar
            // 
            this.btnMenuAgregar.BackColor = System.Drawing.Color.ForestGreen;
            this.btnMenuAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMenuAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuAgregar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnMenuAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuAgregar.ForeColor = System.Drawing.Color.White;
            this.btnMenuAgregar.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            this.btnMenuAgregar.IconColor = System.Drawing.Color.White;
            this.btnMenuAgregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMenuAgregar.IconSize = 30;
            this.btnMenuAgregar.Location = new System.Drawing.Point(318, 705);
            this.btnMenuAgregar.Name = "btnMenuAgregar";
            this.btnMenuAgregar.Size = new System.Drawing.Size(150, 50);
            this.btnMenuAgregar.TabIndex = 63;
            this.btnMenuAgregar.Text = "Agregar";
            this.btnMenuAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMenuAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMenuAgregar.UseVisualStyleBackColor = false;
            this.btnMenuAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // frmSocios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1788, 857);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.btnMenuAgregar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel3);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.Name = "frmSocios";
            this.Text = "frmSocios";
            this.Load += new System.EventHandler(this.frmSocios_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboBusqueda;
        private System.Windows.Forms.TextBox txtBusqueda;
        private FontAwesome.Sharp.IconButton btnBuscar;
        private FontAwesome.Sharp.IconButton btnLimpiarBuscador;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton btnMenuAgregar;
        private FontAwesome.Sharp.IconButton btnMenuEliminar;
        private FontAwesome.Sharp.IconButton btnMenuConsultar;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private FontAwesome.Sharp.IconButton btnMenuTurno;
        private System.Windows.Forms.DataGridViewButtonColumn btnSeleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdSocio;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreYApellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaNacimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Genero;
        private System.Windows.Forms.DataGridViewTextBoxColumn NroDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ciudad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Direccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Telefono;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObraSocial;
        private System.Windows.Forms.DataGridViewTextBoxColumn Plan;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoSocio;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaInicioActividades;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaFinActividades;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaNotificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn RespuestaNotificacion;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionado;
        private System.Windows.Forms.CheckBox ckbMostrarEliminados;
    }
}