namespace Vista
{
    partial class frmMenuRangosHorarios
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.cboRangoHorario = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbEntrenador = new System.Windows.Forms.GroupBox();
            this.txtIdEntrenador = new System.Windows.Forms.TextBox();
            this.btnBuscarEntrenador = new FontAwesome.Sharp.IconButton();
            this.txtNombreEntrenador = new System.Windows.Forms.TextBox();
            this.Nombre = new System.Windows.Forms.Label();
            this.gbRangoHorario = new System.Windows.Forms.GroupBox();
            this.btnEditarCupos = new FontAwesome.Sharp.IconButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCupoMaximo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.btnRegistrar = new FontAwesome.Sharp.IconButton();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.label10 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.txtIndice = new System.Windows.Forms.TextBox();
            this.dgvEntrenadores = new System.Windows.Forms.DataGridView();
            this.NombreYApellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdRangoHorario2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoraDesde2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoraHasta2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.cbActivo = new System.Windows.Forms.CheckBox();
            this.btnSeleccionar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.IdRangoHorario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoraDesde = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoraHasta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CupoMaximo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Activo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entrenadoresAsignados = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Seleccionado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1.SuspendLayout();
            this.gbEntrenador.SuspendLayout();
            this.gbRangoHorario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntrenadores)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(465, 812);
            this.label1.TabIndex = 65;
            // 
            // cboRangoHorario
            // 
            this.cboRangoHorario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRangoHorario.Enabled = false;
            this.cboRangoHorario.FormattingEnabled = true;
            this.cboRangoHorario.Location = new System.Drawing.Point(17, 34);
            this.cboRangoHorario.Name = "cboRangoHorario";
            this.cboRangoHorario.Size = new System.Drawing.Size(283, 21);
            this.cboRangoHorario.TabIndex = 40;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label7.Location = new System.Drawing.Point(11, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 39;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.groupBox1.Controls.Add(this.gbEntrenador);
            this.groupBox1.Controls.Add(this.gbRangoHorario);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.btnRegistrar);
            this.groupBox1.Controls.Add(this.btnLimpiar);
            this.groupBox1.Location = new System.Drawing.Point(46, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 603);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            // 
            // gbEntrenador
            // 
            this.gbEntrenador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.gbEntrenador.Controls.Add(this.txtIdEntrenador);
            this.gbEntrenador.Controls.Add(this.btnBuscarEntrenador);
            this.gbEntrenador.Controls.Add(this.txtNombreEntrenador);
            this.gbEntrenador.Controls.Add(this.Nombre);
            this.gbEntrenador.Location = new System.Drawing.Point(21, 302);
            this.gbEntrenador.Name = "gbEntrenador";
            this.gbEntrenador.Size = new System.Drawing.Size(334, 85);
            this.gbEntrenador.TabIndex = 66;
            this.gbEntrenador.TabStop = false;
            this.gbEntrenador.Text = "Entrenador";
            // 
            // txtIdEntrenador
            // 
            this.txtIdEntrenador.Location = new System.Drawing.Point(277, 8);
            this.txtIdEntrenador.Name = "txtIdEntrenador";
            this.txtIdEntrenador.Size = new System.Drawing.Size(28, 20);
            this.txtIdEntrenador.TabIndex = 31;
            this.txtIdEntrenador.Text = "0";
            this.txtIdEntrenador.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnBuscarEntrenador
            // 
            this.btnBuscarEntrenador.BackColor = System.Drawing.Color.White;
            this.btnBuscarEntrenador.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBuscarEntrenador.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarEntrenador.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnBuscarEntrenador.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarEntrenador.ForeColor = System.Drawing.Color.White;
            this.btnBuscarEntrenador.IconChar = FontAwesome.Sharp.IconChar.Searchengin;
            this.btnBuscarEntrenador.IconColor = System.Drawing.Color.Black;
            this.btnBuscarEntrenador.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscarEntrenador.IconSize = 20;
            this.btnBuscarEntrenador.Location = new System.Drawing.Point(260, 44);
            this.btnBuscarEntrenador.Name = "btnBuscarEntrenador";
            this.btnBuscarEntrenador.Size = new System.Drawing.Size(40, 20);
            this.btnBuscarEntrenador.TabIndex = 29;
            this.btnBuscarEntrenador.UseVisualStyleBackColor = false;
            this.btnBuscarEntrenador.Click += new System.EventHandler(this.btnBuscarEntrenador_Click);
            // 
            // txtNombreEntrenador
            // 
            this.txtNombreEntrenador.Enabled = false;
            this.txtNombreEntrenador.Location = new System.Drawing.Point(15, 45);
            this.txtNombreEntrenador.Name = "txtNombreEntrenador";
            this.txtNombreEntrenador.Size = new System.Drawing.Size(231, 20);
            this.txtNombreEntrenador.TabIndex = 26;
            // 
            // Nombre
            // 
            this.Nombre.AutoSize = true;
            this.Nombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.Nombre.Location = new System.Drawing.Point(12, 29);
            this.Nombre.Name = "Nombre";
            this.Nombre.Size = new System.Drawing.Size(47, 13);
            this.Nombre.TabIndex = 24;
            this.Nombre.Text = "Nombre:";
            // 
            // gbRangoHorario
            // 
            this.gbRangoHorario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.gbRangoHorario.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gbRangoHorario.Controls.Add(this.cbActivo);
            this.gbRangoHorario.Controls.Add(this.btnEditarCupos);
            this.gbRangoHorario.Controls.Add(this.label2);
            this.gbRangoHorario.Controls.Add(this.cboRangoHorario);
            this.gbRangoHorario.Controls.Add(this.txtCupoMaximo);
            this.gbRangoHorario.Controls.Add(this.label7);
            this.gbRangoHorario.Location = new System.Drawing.Point(21, 86);
            this.gbRangoHorario.Name = "gbRangoHorario";
            this.gbRangoHorario.Size = new System.Drawing.Size(334, 187);
            this.gbRangoHorario.TabIndex = 64;
            this.gbRangoHorario.TabStop = false;
            this.gbRangoHorario.Text = "Rango Horario";
            // 
            // btnEditarCupos
            // 
            this.btnEditarCupos.BackColor = System.Drawing.Color.White;
            this.btnEditarCupos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnEditarCupos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditarCupos.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnEditarCupos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarCupos.ForeColor = System.Drawing.Color.White;
            this.btnEditarCupos.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.btnEditarCupos.IconColor = System.Drawing.Color.Black;
            this.btnEditarCupos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEditarCupos.IconSize = 20;
            this.btnEditarCupos.Location = new System.Drawing.Point(264, 145);
            this.btnEditarCupos.Name = "btnEditarCupos";
            this.btnEditarCupos.Size = new System.Drawing.Size(36, 20);
            this.btnEditarCupos.TabIndex = 32;
            this.btnEditarCupos.UseVisualStyleBackColor = false;
            this.btnEditarCupos.Click += new System.EventHandler(this.btnEditarCupos_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label2.Location = new System.Drawing.Point(12, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Cupos Máximos:";
            // 
            // txtCupoMaximo
            // 
            this.txtCupoMaximo.Enabled = false;
            this.txtCupoMaximo.Location = new System.Drawing.Point(17, 146);
            this.txtCupoMaximo.Name = "txtCupoMaximo";
            this.txtCupoMaximo.Size = new System.Drawing.Size(213, 20);
            this.txtCupoMaximo.TabIndex = 65;
            this.txtCupoMaximo.Text = "0";
            this.txtCupoMaximo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCupoMaximo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCupoMaximo_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(186, 25);
            this.label9.TabIndex = 46;
            this.label9.Text = "Asignar Entrenador:";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(327, 32);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(28, 20);
            this.txtId.TabIndex = 49;
            this.txtId.Text = "0";
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.BackColor = System.Drawing.Color.ForestGreen;
            this.btnRegistrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrar.ForeColor = System.Drawing.Color.White;
            this.btnRegistrar.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            this.btnRegistrar.IconColor = System.Drawing.Color.White;
            this.btnRegistrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRegistrar.IconSize = 20;
            this.btnRegistrar.Location = new System.Drawing.Point(88, 436);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(199, 29);
            this.btnRegistrar.TabIndex = 44;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRegistrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRegistrar.UseVisualStyleBackColor = false;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.btnLimpiar.IconColor = System.Drawing.Color.White;
            this.btnLimpiar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLimpiar.IconSize = 20;
            this.btnLimpiar.Location = new System.Drawing.Point(88, 472);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(199, 29);
            this.btnLimpiar.TabIndex = 43;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(480, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(775, 67);
            this.label10.TabIndex = 68;
            this.label10.Text = "Lista de Rangos Horarios:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnSeleccionar,
            this.IdRangoHorario,
            this.HoraDesde,
            this.HoraHasta,
            this.CupoMaximo,
            this.Activo,
            this.entrenadoresAsignados,
            this.Seleccionado});
            this.dgvData.Location = new System.Drawing.Point(480, 95);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.RowTemplate.Height = 35;
            this.dgvData.Size = new System.Drawing.Size(775, 533);
            this.dgvData.TabIndex = 67;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            this.dgvData.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvData_CellPainting);
            // 
            // txtIndice
            // 
            this.txtIndice.Location = new System.Drawing.Point(484, 100);
            this.txtIndice.Name = "txtIndice";
            this.txtIndice.Size = new System.Drawing.Size(28, 20);
            this.txtIndice.TabIndex = 69;
            this.txtIndice.Text = "-1";
            this.txtIndice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dgvEntrenadores
            // 
            this.dgvEntrenadores.AllowUserToAddRows = false;
            this.dgvEntrenadores.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEntrenadores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvEntrenadores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEntrenadores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NombreYApellido,
            this.IdRangoHorario2,
            this.HoraDesde2,
            this.HoraHasta2,
            this.IdUsuario,
            this.btnEliminar});
            this.dgvEntrenadores.Location = new System.Drawing.Point(1269, 95);
            this.dgvEntrenadores.MultiSelect = false;
            this.dgvEntrenadores.Name = "dgvEntrenadores";
            this.dgvEntrenadores.ReadOnly = true;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvEntrenadores.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvEntrenadores.RowTemplate.Height = 35;
            this.dgvEntrenadores.Size = new System.Drawing.Size(501, 533);
            this.dgvEntrenadores.TabIndex = 70;
            this.dgvEntrenadores.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEntrenadores_CellContentClick);
            this.dgvEntrenadores.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvEntrenadores_CellPainting);
            // 
            // NombreYApellido
            // 
            this.NombreYApellido.HeaderText = "Entrenador";
            this.NombreYApellido.Name = "NombreYApellido";
            this.NombreYApellido.ReadOnly = true;
            this.NombreYApellido.Width = 200;
            // 
            // IdRangoHorario2
            // 
            this.IdRangoHorario2.HeaderText = "IdRangoHorario";
            this.IdRangoHorario2.Name = "IdRangoHorario2";
            this.IdRangoHorario2.ReadOnly = true;
            this.IdRangoHorario2.Visible = false;
            // 
            // HoraDesde2
            // 
            this.HoraDesde2.HeaderText = "Hora Desde";
            this.HoraDesde2.Name = "HoraDesde2";
            this.HoraDesde2.ReadOnly = true;
            // 
            // HoraHasta2
            // 
            this.HoraHasta2.HeaderText = "Hora Hasta";
            this.HoraHasta2.Name = "HoraHasta2";
            this.HoraHasta2.ReadOnly = true;
            // 
            // IdUsuario
            // 
            this.IdUsuario.HeaderText = "IdUsuario";
            this.IdUsuario.Name = "IdUsuario";
            this.IdUsuario.ReadOnly = true;
            this.IdUsuario.Visible = false;
            // 
            // btnEliminar
            // 
            this.btnEliminar.HeaderText = "";
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.ReadOnly = true;
            this.btnEliminar.Width = 35;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1269, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(501, 67);
            this.label3.TabIndex = 71;
            this.label3.Text = "Horarios Entrenadores:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbActivo
            // 
            this.cbActivo.AutoSize = true;
            this.cbActivo.Enabled = false;
            this.cbActivo.Location = new System.Drawing.Point(17, 83);
            this.cbActivo.Name = "cbActivo";
            this.cbActivo.Size = new System.Drawing.Size(136, 17);
            this.cbActivo.TabIndex = 66;
            this.cbActivo.Text = "Habilitar Rango Horario";
            this.cbActivo.UseVisualStyleBackColor = true;
            this.cbActivo.CheckedChanged += new System.EventHandler(this.cbActivo_CheckedChanged);
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
            // IdRangoHorario
            // 
            this.IdRangoHorario.HeaderText = "IdRangoHorario";
            this.IdRangoHorario.Name = "IdRangoHorario";
            this.IdRangoHorario.ReadOnly = true;
            this.IdRangoHorario.Visible = false;
            // 
            // HoraDesde
            // 
            this.HoraDesde.HeaderText = "Hora Desde";
            this.HoraDesde.Name = "HoraDesde";
            this.HoraDesde.ReadOnly = true;
            this.HoraDesde.Width = 150;
            // 
            // HoraHasta
            // 
            this.HoraHasta.HeaderText = "Hora Hasta";
            this.HoraHasta.Name = "HoraHasta";
            this.HoraHasta.ReadOnly = true;
            this.HoraHasta.Width = 150;
            // 
            // CupoMaximo
            // 
            this.CupoMaximo.HeaderText = "Cupo Maximo";
            this.CupoMaximo.Name = "CupoMaximo";
            this.CupoMaximo.ReadOnly = true;
            this.CupoMaximo.Width = 150;
            // 
            // Activo
            // 
            this.Activo.HeaderText = "Activo";
            this.Activo.Name = "Activo";
            this.Activo.ReadOnly = true;
            this.Activo.Width = 50;
            // 
            // entrenadoresAsignados
            // 
            this.entrenadoresAsignados.HeaderText = "Entrenadores Asignados";
            this.entrenadoresAsignados.Name = "entrenadoresAsignados";
            this.entrenadoresAsignados.ReadOnly = true;
            this.entrenadoresAsignados.Width = 180;
            // 
            // Seleccionado
            // 
            this.Seleccionado.HeaderText = "Seleccionado";
            this.Seleccionado.Name = "Seleccionado";
            this.Seleccionado.ReadOnly = true;
            this.Seleccionado.Visible = false;
            // 
            // frmMenuRangosHorarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1772, 812);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvEntrenadores);
            this.Controls.Add(this.txtIndice);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "frmMenuRangosHorarios";
            this.Text = "frmMenuRangosHorarios";
            this.Load += new System.EventHandler(this.frmMenuRangosHorarios_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbEntrenador.ResumeLayout(false);
            this.gbEntrenador.PerformLayout();
            this.gbRangoHorario.ResumeLayout(false);
            this.gbRangoHorario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntrenadores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private System.Windows.Forms.ComboBox cboRangoHorario;
        private FontAwesome.Sharp.IconButton btnRegistrar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbRangoHorario;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtCupoMaximo;
        private System.Windows.Forms.GroupBox gbEntrenador;
        private FontAwesome.Sharp.IconButton btnBuscarEntrenador;
        private System.Windows.Forms.TextBox txtNombreEntrenador;
        private System.Windows.Forms.Label Nombre;
        private System.Windows.Forms.TextBox txtIdEntrenador;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.TextBox txtIndice;
        private FontAwesome.Sharp.IconButton btnEditarCupos;
        private System.Windows.Forms.DataGridView dgvEntrenadores;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreYApellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdRangoHorario2;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoraDesde2;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoraHasta2;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdUsuario;
        private System.Windows.Forms.DataGridViewButtonColumn btnEliminar;
        private System.Windows.Forms.CheckBox cbActivo;
        private System.Windows.Forms.DataGridViewButtonColumn btnSeleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdRangoHorario;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoraDesde;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoraHasta;
        private System.Windows.Forms.DataGridViewTextBoxColumn CupoMaximo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Activo;
        private System.Windows.Forms.DataGridViewTextBoxColumn entrenadoresAsignados;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionado;
    }
}