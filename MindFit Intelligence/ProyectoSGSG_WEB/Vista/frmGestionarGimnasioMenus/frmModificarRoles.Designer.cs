﻿namespace Vista
{
    partial class frmModificarRoles
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnLimpiarBuscador = new FontAwesome.Sharp.IconButton();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnBuscar = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cboBusqueda = new System.Windows.Forms.ComboBox();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelPermiso = new System.Windows.Forms.Panel();
            this.txbDescripcion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPermisoRol = new System.Windows.Forms.TextBox();
            this.gbPermisos = new System.Windows.Forms.GroupBox();
            this.panelGrupo = new System.Windows.Forms.Panel();
            this.gbAccion = new System.Windows.Forms.GroupBox();
            this.cboAccion = new System.Windows.Forms.ComboBox();
            this.btnAgregarAccion = new FontAwesome.Sharp.IconButton();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbGrupo = new System.Windows.Forms.GroupBox();
            this.btnAgreagar1 = new FontAwesome.Sharp.IconButton();
            this.cboGrupo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.btnGuardar = new FontAwesome.Sharp.IconButton();
            this.btnEliminar = new FontAwesome.Sharp.IconButton();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.txtIndice = new System.Windows.Forms.TextBox();
            this.dgvDataPermisos = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIdGrupo = new System.Windows.Forms.TextBox();
            this.Seleccionado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Rol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdRol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSeleccionar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnSeleccionar2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.TipoPermiso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreMenu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdGrupo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreAccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescAccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdAccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Seleccionado2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnEliminarFila = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelPermiso.SuspendLayout();
            this.gbPermisos.SuspendLayout();
            this.panelGrupo.SuspendLayout();
            this.gbAccion.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbGrupo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataPermisos)).BeginInit();
            this.SuspendLayout();
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
            this.btnLimpiarBuscador.Location = new System.Drawing.Point(453, 19);
            this.btnLimpiarBuscador.Name = "btnLimpiarBuscador";
            this.btnLimpiarBuscador.Size = new System.Drawing.Size(40, 20);
            this.btnLimpiarBuscador.TabIndex = 53;
            this.btnLimpiarBuscador.UseVisualStyleBackColor = false;
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
            this.IdRol,
            this.Rol,
            this.Seleccionado});
            this.dgvData.Location = new System.Drawing.Point(566, 84);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.RowTemplate.Height = 35;
            this.dgvData.Size = new System.Drawing.Size(1176, 211);
            this.dgvData.TabIndex = 69;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            this.dgvData.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvData_CellPainting);
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
            this.btnBuscar.Location = new System.Drawing.Point(407, 19);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 20);
            this.btnBuscar.TabIndex = 54;
            this.btnBuscar.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(529, 606);
            this.label1.TabIndex = 65;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(26, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 13);
            this.label11.TabIndex = 50;
            this.label11.Text = "Buscar Por:";
            // 
            // cboBusqueda
            // 
            this.cboBusqueda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBusqueda.FormattingEnabled = true;
            this.cboBusqueda.Location = new System.Drawing.Point(94, 19);
            this.cboBusqueda.Name = "cboBusqueda";
            this.cboBusqueda.Size = new System.Drawing.Size(127, 21);
            this.cboBusqueda.TabIndex = 51;
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Location = new System.Drawing.Point(227, 19);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(171, 20);
            this.txtBusqueda.TabIndex = 52;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.cboBusqueda);
            this.groupBox2.Controls.Add(this.txtBusqueda);
            this.groupBox2.Controls.Add(this.btnBuscar);
            this.groupBox2.Controls.Add(this.btnLimpiarBuscador);
            this.groupBox2.Location = new System.Drawing.Point(1224, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(511, 59);
            this.groupBox2.TabIndex = 68;
            this.groupBox2.TabStop = false;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(566, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1176, 67);
            this.label10.TabIndex = 66;
            this.label10.Text = "Lista de Roles";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.groupBox1.Controls.Add(this.panelPermiso);
            this.groupBox1.Controls.Add(this.gbPermisos);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.btnGuardar);
            this.groupBox1.Controls.Add(this.btnEliminar);
            this.groupBox1.Controls.Add(this.btnLimpiar);
            this.groupBox1.Location = new System.Drawing.Point(36, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(453, 579);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            // 
            // panelPermiso
            // 
            this.panelPermiso.Controls.Add(this.txbDescripcion);
            this.panelPermiso.Controls.Add(this.label5);
            this.panelPermiso.Controls.Add(this.label3);
            this.panelPermiso.Controls.Add(this.txtPermisoRol);
            this.panelPermiso.Location = new System.Drawing.Point(6, 95);
            this.panelPermiso.Name = "panelPermiso";
            this.panelPermiso.Size = new System.Drawing.Size(428, 223);
            this.panelPermiso.TabIndex = 69;
            // 
            // txbDescripcion
            // 
            this.txbDescripcion.Enabled = false;
            this.txbDescripcion.Location = new System.Drawing.Point(138, 52);
            this.txbDescripcion.Multiline = true;
            this.txbDescripcion.Name = "txbDescripcion";
            this.txbDescripcion.Size = new System.Drawing.Size(277, 165);
            this.txbDescripcion.TabIndex = 67;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label5.Location = new System.Drawing.Point(9, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 13);
            this.label5.TabIndex = 66;
            this.label5.Text = "Descripción del Permiso:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label3.Location = new System.Drawing.Point(66, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Permiso Rol:";
            // 
            // txtPermisoRol
            // 
            this.txtPermisoRol.Enabled = false;
            this.txtPermisoRol.Location = new System.Drawing.Point(138, 12);
            this.txtPermisoRol.Name = "txtPermisoRol";
            this.txtPermisoRol.Size = new System.Drawing.Size(277, 20);
            this.txtPermisoRol.TabIndex = 33;
            // 
            // gbPermisos
            // 
            this.gbPermisos.Controls.Add(this.panelGrupo);
            this.gbPermisos.Controls.Add(this.panel1);
            this.gbPermisos.Location = new System.Drawing.Point(52, 318);
            this.gbPermisos.Name = "gbPermisos";
            this.gbPermisos.Size = new System.Drawing.Size(369, 193);
            this.gbPermisos.TabIndex = 68;
            this.gbPermisos.TabStop = false;
            this.gbPermisos.Text = "Permisos por";
            // 
            // panelGrupo
            // 
            this.panelGrupo.Controls.Add(this.gbAccion);
            this.panelGrupo.Location = new System.Drawing.Point(6, 103);
            this.panelGrupo.Name = "panelGrupo";
            this.panelGrupo.Size = new System.Drawing.Size(357, 83);
            this.panelGrupo.TabIndex = 69;
            // 
            // gbAccion
            // 
            this.gbAccion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.gbAccion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gbAccion.Controls.Add(this.cboAccion);
            this.gbAccion.Controls.Add(this.btnAgregarAccion);
            this.gbAccion.Controls.Add(this.label6);
            this.gbAccion.Enabled = false;
            this.gbAccion.Location = new System.Drawing.Point(3, 3);
            this.gbAccion.Name = "gbAccion";
            this.gbAccion.Size = new System.Drawing.Size(334, 74);
            this.gbAccion.TabIndex = 64;
            this.gbAccion.TabStop = false;
            this.gbAccion.Text = "Acción";
            // 
            // cboAccion
            // 
            this.cboAccion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAccion.FormattingEnabled = true;
            this.cboAccion.Location = new System.Drawing.Point(17, 31);
            this.cboAccion.Name = "cboAccion";
            this.cboAccion.Size = new System.Drawing.Size(218, 21);
            this.cboAccion.TabIndex = 40;
            // 
            // btnAgregarAccion
            // 
            this.btnAgregarAccion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarAccion.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnAgregarAccion.IconColor = System.Drawing.Color.ForestGreen;
            this.btnAgregarAccion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAgregarAccion.IconSize = 55;
            this.btnAgregarAccion.Location = new System.Drawing.Point(254, 15);
            this.btnAgregarAccion.Margin = new System.Windows.Forms.Padding(0);
            this.btnAgregarAccion.Name = "btnAgregarAccion";
            this.btnAgregarAccion.Padding = new System.Windows.Forms.Padding(2, 6, 0, 0);
            this.btnAgregarAccion.Size = new System.Drawing.Size(50, 50);
            this.btnAgregarAccion.TabIndex = 63;
            this.btnAgregarAccion.UseVisualStyleBackColor = true;
            this.btnAgregarAccion.Click += new System.EventHandler(this.btnAgregarAccion_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label6.Location = new System.Drawing.Point(11, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 39;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbGrupo);
            this.panel1.Location = new System.Drawing.Point(6, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(357, 83);
            this.panel1.TabIndex = 68;
            // 
            // gbGrupo
            // 
            this.gbGrupo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.gbGrupo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gbGrupo.Controls.Add(this.btnAgreagar1);
            this.gbGrupo.Controls.Add(this.cboGrupo);
            this.gbGrupo.Controls.Add(this.label7);
            this.gbGrupo.Enabled = false;
            this.gbGrupo.Location = new System.Drawing.Point(3, 3);
            this.gbGrupo.Name = "gbGrupo";
            this.gbGrupo.Size = new System.Drawing.Size(334, 74);
            this.gbGrupo.TabIndex = 64;
            this.gbGrupo.TabStop = false;
            this.gbGrupo.Text = "Grupo";
            // 
            // btnAgreagar1
            // 
            this.btnAgreagar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgreagar1.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnAgreagar1.IconColor = System.Drawing.Color.ForestGreen;
            this.btnAgreagar1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAgreagar1.IconSize = 55;
            this.btnAgreagar1.Location = new System.Drawing.Point(254, 15);
            this.btnAgreagar1.Margin = new System.Windows.Forms.Padding(0);
            this.btnAgreagar1.Name = "btnAgreagar1";
            this.btnAgreagar1.Padding = new System.Windows.Forms.Padding(2, 6, 0, 0);
            this.btnAgreagar1.Size = new System.Drawing.Size(50, 50);
            this.btnAgreagar1.TabIndex = 64;
            this.btnAgreagar1.UseVisualStyleBackColor = true;
            this.btnAgreagar1.Click += new System.EventHandler(this.btnAgreagar1_Click);
            // 
            // cboGrupo
            // 
            this.cboGrupo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGrupo.FormattingEnabled = true;
            this.cboGrupo.Location = new System.Drawing.Point(17, 31);
            this.cboGrupo.Name = "cboGrupo";
            this.cboGrupo.Size = new System.Drawing.Size(218, 21);
            this.cboGrupo.TabIndex = 40;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label2.Location = new System.Drawing.Point(55, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 64;
            this.label2.Text = "Nombre del Rol:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Enabled = false;
            this.txtDescripcion.Location = new System.Drawing.Point(144, 74);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(277, 20);
            this.txtDescripcion.TabIndex = 65;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 25);
            this.label9.TabIndex = 46;
            this.label9.Text = "Detalle Rol:";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(393, 32);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(28, 20);
            this.txtId.TabIndex = 49;
            this.txtId.Text = "0";
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.ForestGreen;
            this.btnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnGuardar.IconColor = System.Drawing.Color.White;
            this.btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnGuardar.IconSize = 20;
            this.btnGuardar.Location = new System.Drawing.Point(61, 536);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(90, 29);
            this.btnGuardar.TabIndex = 44;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.Firebrick;
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            this.btnEliminar.IconColor = System.Drawing.Color.White;
            this.btnEliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEliminar.IconSize = 20;
            this.btnEliminar.Location = new System.Drawing.Point(325, 536);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(90, 29);
            this.btnEliminar.TabIndex = 45;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
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
            this.btnLimpiar.Location = new System.Drawing.Point(193, 536);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(90, 29);
            this.btnLimpiar.TabIndex = 43;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // txtIndice
            // 
            this.txtIndice.Location = new System.Drawing.Point(573, 89);
            this.txtIndice.Name = "txtIndice";
            this.txtIndice.Size = new System.Drawing.Size(28, 20);
            this.txtIndice.TabIndex = 71;
            this.txtIndice.Text = "-1";
            this.txtIndice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dgvDataPermisos
            // 
            this.dgvDataPermisos.AllowUserToAddRows = false;
            this.dgvDataPermisos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDataPermisos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDataPermisos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataPermisos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnSeleccionar2,
            this.TipoPermiso,
            this.NombreMenu,
            this.Descripcion,
            this.IdGrupo,
            this.NombreAccion,
            this.DescAccion,
            this.IdAccion,
            this.Seleccionado2,
            this.btnEliminarFila});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDataPermisos.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDataPermisos.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvDataPermisos.Location = new System.Drawing.Point(566, 375);
            this.dgvDataPermisos.MultiSelect = false;
            this.dgvDataPermisos.Name = "dgvDataPermisos";
            this.dgvDataPermisos.ReadOnly = true;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDataPermisos.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDataPermisos.RowTemplate.Height = 50;
            this.dgvDataPermisos.Size = new System.Drawing.Size(1176, 211);
            this.dgvDataPermisos.TabIndex = 73;
            this.dgvDataPermisos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDataPermisos_CellContentClick);
            this.dgvDataPermisos.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvDataPermisos_CellPainting);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(566, 305);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1176, 67);
            this.label4.TabIndex = 72;
            this.label4.Text = "Lista de Permisos";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdGrupo
            // 
            this.txtIdGrupo.Location = new System.Drawing.Point(573, 379);
            this.txtIdGrupo.Name = "txtIdGrupo";
            this.txtIdGrupo.Size = new System.Drawing.Size(28, 20);
            this.txtIdGrupo.TabIndex = 74;
            this.txtIdGrupo.Text = "-1";
            this.txtIdGrupo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Seleccionado
            // 
            this.Seleccionado.HeaderText = "Seleccionado";
            this.Seleccionado.Name = "Seleccionado";
            this.Seleccionado.ReadOnly = true;
            this.Seleccionado.Visible = false;
            // 
            // Rol
            // 
            this.Rol.HeaderText = "Rol";
            this.Rol.Name = "Rol";
            this.Rol.ReadOnly = true;
            this.Rol.Width = 981;
            // 
            // IdRol
            // 
            this.IdRol.HeaderText = "IdRol";
            this.IdRol.Name = "IdRol";
            this.IdRol.ReadOnly = true;
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
            // btnSeleccionar2
            // 
            this.btnSeleccionar2.HeaderText = "";
            this.btnSeleccionar2.Name = "btnSeleccionar2";
            this.btnSeleccionar2.ReadOnly = true;
            this.btnSeleccionar2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnSeleccionar2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btnSeleccionar2.Width = 50;
            // 
            // TipoPermiso
            // 
            this.TipoPermiso.HeaderText = "Tipo Permiso";
            this.TipoPermiso.Name = "TipoPermiso";
            this.TipoPermiso.ReadOnly = true;
            this.TipoPermiso.Width = 120;
            // 
            // NombreMenu
            // 
            this.NombreMenu.HeaderText = "Nombre Grupo";
            this.NombreMenu.Name = "NombreMenu";
            this.NombreMenu.ReadOnly = true;
            this.NombreMenu.Width = 150;
            // 
            // Descripcion
            // 
            this.Descripcion.HeaderText = "Descripcion Grupo";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            this.Descripcion.Width = 298;
            // 
            // IdGrupo
            // 
            this.IdGrupo.HeaderText = "IdGrupo";
            this.IdGrupo.Name = "IdGrupo";
            this.IdGrupo.ReadOnly = true;
            this.IdGrupo.Visible = false;
            // 
            // NombreAccion
            // 
            this.NombreAccion.HeaderText = "Nombre Accion";
            this.NombreAccion.Name = "NombreAccion";
            this.NombreAccion.ReadOnly = true;
            this.NombreAccion.Width = 150;
            // 
            // DescAccion
            // 
            this.DescAccion.HeaderText = "Descripción Acción";
            this.DescAccion.Name = "DescAccion";
            this.DescAccion.ReadOnly = true;
            this.DescAccion.Width = 298;
            // 
            // IdAccion
            // 
            this.IdAccion.HeaderText = "IdAccion";
            this.IdAccion.Name = "IdAccion";
            this.IdAccion.ReadOnly = true;
            this.IdAccion.Visible = false;
            // 
            // Seleccionado2
            // 
            this.Seleccionado2.HeaderText = "Seleccionado";
            this.Seleccionado2.Name = "Seleccionado2";
            this.Seleccionado2.ReadOnly = true;
            this.Seleccionado2.Visible = false;
            // 
            // btnEliminarFila
            // 
            this.btnEliminarFila.HeaderText = "";
            this.btnEliminarFila.Name = "btnEliminarFila";
            this.btnEliminarFila.ReadOnly = true;
            this.btnEliminarFila.Width = 50;
            // 
            // frmModificarRoles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1772, 606);
            this.Controls.Add(this.txtIdGrupo);
            this.Controls.Add(this.dgvDataPermisos);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtIndice);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label1);
            this.Name = "frmModificarRoles";
            this.Text = "frmModificarRoles";
            this.Load += new System.EventHandler(this.frmModificarRoles_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelPermiso.ResumeLayout(false);
            this.panelPermiso.PerformLayout();
            this.gbPermisos.ResumeLayout(false);
            this.panelGrupo.ResumeLayout(false);
            this.gbAccion.ResumeLayout(false);
            this.gbAccion.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gbGrupo.ResumeLayout(false);
            this.gbGrupo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataPermisos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnLimpiarBuscador;
        private System.Windows.Forms.DataGridView dgvData;
        private FontAwesome.Sharp.IconButton btnBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboBusqueda;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPermisoRol;
        private System.Windows.Forms.TextBox txtId;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton btnEliminar;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtIndice;
        private System.Windows.Forms.DataGridView dgvDataPermisos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txbDescripcion;
        private System.Windows.Forms.TextBox txtIdGrupo;
        private System.Windows.Forms.GroupBox gbPermisos;
        private System.Windows.Forms.Panel panelGrupo;
        private System.Windows.Forms.GroupBox gbAccion;
        private System.Windows.Forms.ComboBox cboAccion;
        private FontAwesome.Sharp.IconButton btnAgregarAccion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbGrupo;
        private FontAwesome.Sharp.IconButton btnAgreagar1;
        private System.Windows.Forms.ComboBox cboGrupo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panelPermiso;
        private System.Windows.Forms.DataGridViewButtonColumn btnSeleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdRol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rol;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionado;
        private System.Windows.Forms.DataGridViewButtonColumn btnSeleccionar2;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoPermiso;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreMenu;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdGrupo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreAccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescAccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdAccion;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionado2;
        private System.Windows.Forms.DataGridViewButtonColumn btnEliminarFila;
    }
}