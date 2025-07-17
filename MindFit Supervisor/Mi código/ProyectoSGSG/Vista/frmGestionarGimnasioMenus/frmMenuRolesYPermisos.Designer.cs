namespace Vista
{
    partial class frmMenuRolesYPermisos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbPermisos = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAccion = new FontAwesome.Sharp.IconButton();
            this.gbAccion = new System.Windows.Forms.GroupBox();
            this.cboAccion = new System.Windows.Forms.ComboBox();
            this.btnAgregarAccion = new FontAwesome.Sharp.IconButton();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGrupo = new FontAwesome.Sharp.IconButton();
            this.gbGrupo = new System.Windows.Forms.GroupBox();
            this.btnAgreagar1 = new FontAwesome.Sharp.IconButton();
            this.cboGrupo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnRegistrarRol = new FontAwesome.Sharp.IconButton();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvPermisosSeleccionados = new System.Windows.Forms.DataGridView();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoPermiso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreMenu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreAccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescripcionPermiso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdGrupo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdAccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.chbGrupo = new System.Windows.Forms.CheckBox();
            this.chbAccion = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.gbPermisos.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gbAccion.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbGrupo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermisosSeleccionados)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 25);
            this.label9.TabIndex = 46;
            this.label9.Text = "Agregar Rol:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label2.Location = new System.Drawing.Point(18, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Nombre del Rol:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(107, 71);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(248, 20);
            this.txtDescripcion.TabIndex = 34;
            this.txtDescripcion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescripcion_KeyDown);
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
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.groupBox1.Controls.Add(this.gbPermisos);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.btnRegistrarRol);
            this.groupBox1.Controls.Add(this.btnLimpiar);
            this.groupBox1.Location = new System.Drawing.Point(31, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 601);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            // 
            // gbPermisos
            // 
            this.gbPermisos.Controls.Add(this.panel2);
            this.gbPermisos.Controls.Add(this.panel1);
            this.gbPermisos.Location = new System.Drawing.Point(6, 102);
            this.gbPermisos.Name = "gbPermisos";
            this.gbPermisos.Size = new System.Drawing.Size(369, 308);
            this.gbPermisos.TabIndex = 65;
            this.gbPermisos.TabStop = false;
            this.gbPermisos.Text = "Permisos por";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnAccion);
            this.panel2.Controls.Add(this.gbAccion);
            this.panel2.Location = new System.Drawing.Point(6, 159);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(357, 139);
            this.panel2.TabIndex = 69;
            // 
            // btnAccion
            // 
            this.btnAccion.IconChar = FontAwesome.Sharp.IconChar.HandPointRight;
            this.btnAccion.IconColor = System.Drawing.Color.Black;
            this.btnAccion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAccion.IconSize = 22;
            this.btnAccion.Location = new System.Drawing.Point(0, 0);
            this.btnAccion.Name = "btnAccion";
            this.btnAccion.Size = new System.Drawing.Size(90, 30);
            this.btnAccion.TabIndex = 67;
            this.btnAccion.Text = "Accion";
            this.btnAccion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAccion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAccion.UseVisualStyleBackColor = true;
            this.btnAccion.Click += new System.EventHandler(this.btnAccion_Click);
            // 
            // gbAccion
            // 
            this.gbAccion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.gbAccion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gbAccion.Controls.Add(this.chbAccion);
            this.gbAccion.Controls.Add(this.cboAccion);
            this.gbAccion.Controls.Add(this.btnAgregarAccion);
            this.gbAccion.Controls.Add(this.label4);
            this.gbAccion.Enabled = false;
            this.gbAccion.Location = new System.Drawing.Point(3, 30);
            this.gbAccion.Name = "gbAccion";
            this.gbAccion.Size = new System.Drawing.Size(334, 106);
            this.gbAccion.TabIndex = 64;
            this.gbAccion.TabStop = false;
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label4.Location = new System.Drawing.Point(11, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 39;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnGrupo);
            this.panel1.Controls.Add(this.gbGrupo);
            this.panel1.Location = new System.Drawing.Point(6, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(357, 139);
            this.panel1.TabIndex = 68;
            // 
            // btnGrupo
            // 
            this.btnGrupo.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.btnGrupo.IconColor = System.Drawing.Color.Black;
            this.btnGrupo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnGrupo.IconSize = 22;
            this.btnGrupo.Location = new System.Drawing.Point(0, 0);
            this.btnGrupo.Name = "btnGrupo";
            this.btnGrupo.Size = new System.Drawing.Size(90, 30);
            this.btnGrupo.TabIndex = 58;
            this.btnGrupo.Text = "Grupo";
            this.btnGrupo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGrupo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGrupo.UseVisualStyleBackColor = true;
            this.btnGrupo.Click += new System.EventHandler(this.btnGrupo_Click);
            // 
            // gbGrupo
            // 
            this.gbGrupo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.gbGrupo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gbGrupo.Controls.Add(this.chbGrupo);
            this.gbGrupo.Controls.Add(this.btnAgreagar1);
            this.gbGrupo.Controls.Add(this.cboGrupo);
            this.gbGrupo.Controls.Add(this.label7);
            this.gbGrupo.Enabled = false;
            this.gbGrupo.Location = new System.Drawing.Point(3, 30);
            this.gbGrupo.Name = "gbGrupo";
            this.gbGrupo.Size = new System.Drawing.Size(334, 106);
            this.gbGrupo.TabIndex = 64;
            this.gbGrupo.TabStop = false;
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
            this.btnAgreagar1.Click += new System.EventHandler(this.btnAgreagar_Click);
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
            // btnRegistrarRol
            // 
            this.btnRegistrarRol.BackColor = System.Drawing.Color.ForestGreen;
            this.btnRegistrarRol.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRegistrarRol.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrarRol.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnRegistrarRol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarRol.ForeColor = System.Drawing.Color.White;
            this.btnRegistrarRol.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            this.btnRegistrarRol.IconColor = System.Drawing.Color.White;
            this.btnRegistrarRol.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRegistrarRol.IconSize = 20;
            this.btnRegistrarRol.Location = new System.Drawing.Point(87, 524);
            this.btnRegistrarRol.Name = "btnRegistrarRol";
            this.btnRegistrarRol.Size = new System.Drawing.Size(199, 29);
            this.btnRegistrarRol.TabIndex = 44;
            this.btnRegistrarRol.Text = "Registrar Rol";
            this.btnRegistrarRol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRegistrarRol.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRegistrarRol.UseVisualStyleBackColor = false;
            this.btnRegistrarRol.Click += new System.EventHandler(this.btnRegistrarRol_Click);
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
            this.btnLimpiar.Location = new System.Drawing.Point(87, 560);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(199, 29);
            this.btnLimpiar.TabIndex = 43;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
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
            this.label1.TabIndex = 58;
            // 
            // dgvPermisosSeleccionados
            // 
            this.dgvPermisosSeleccionados.AllowUserToAddRows = false;
            this.dgvPermisosSeleccionados.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPermisosSeleccionados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvPermisosSeleccionados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPermisosSeleccionados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Descripcion,
            this.TipoPermiso,
            this.NombreMenu,
            this.nombreAccion,
            this.DescripcionPermiso,
            this.IdGrupo,
            this.IdAccion,
            this.btnEliminar});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPermisosSeleccionados.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvPermisosSeleccionados.Location = new System.Drawing.Point(504, 40);
            this.dgvPermisosSeleccionados.MultiSelect = false;
            this.dgvPermisosSeleccionados.Name = "dgvPermisosSeleccionados";
            this.dgvPermisosSeleccionados.ReadOnly = true;
            this.dgvPermisosSeleccionados.RowHeadersWidth = 34;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvPermisosSeleccionados.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvPermisosSeleccionados.RowTemplate.Height = 42;
            this.dgvPermisosSeleccionados.Size = new System.Drawing.Size(1238, 601);
            this.dgvPermisosSeleccionados.TabIndex = 64;
            this.dgvPermisosSeleccionados.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPermisosSeleccionados_CellContentClick);
            this.dgvPermisosSeleccionados.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvPermisosSeleccionados_CellPainting);
            // 
            // Descripcion
            // 
            this.Descripcion.HeaderText = "Rol";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            this.Descripcion.Width = 150;
            // 
            // TipoPermiso
            // 
            this.TipoPermiso.HeaderText = "Tipo Permiso";
            this.TipoPermiso.Name = "TipoPermiso";
            this.TipoPermiso.ReadOnly = true;
            this.TipoPermiso.Width = 125;
            // 
            // NombreMenu
            // 
            this.NombreMenu.HeaderText = "Grupo";
            this.NombreMenu.Name = "NombreMenu";
            this.NombreMenu.ReadOnly = true;
            this.NombreMenu.Width = 150;
            // 
            // nombreAccion
            // 
            this.nombreAccion.HeaderText = "Accion";
            this.nombreAccion.Name = "nombreAccion";
            this.nombreAccion.ReadOnly = true;
            // 
            // DescripcionPermiso
            // 
            this.DescripcionPermiso.HeaderText = "Descripcion Permiso";
            this.DescripcionPermiso.Name = "DescripcionPermiso";
            this.DescripcionPermiso.ReadOnly = true;
            this.DescripcionPermiso.Width = 605;
            // 
            // IdGrupo
            // 
            this.IdGrupo.HeaderText = "IdGrupo";
            this.IdGrupo.Name = "IdGrupo";
            this.IdGrupo.ReadOnly = true;
            this.IdGrupo.Visible = false;
            // 
            // IdAccion
            // 
            this.IdAccion.HeaderText = "IdAccion";
            this.IdAccion.Name = "IdAccion";
            this.IdAccion.ReadOnly = true;
            this.IdAccion.Visible = false;
            this.IdAccion.Width = 150;
            // 
            // btnEliminar
            // 
            this.btnEliminar.HeaderText = "";
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.ReadOnly = true;
            this.btnEliminar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnEliminar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btnEliminar.Width = 50;
            // 
            // chbGrupo
            // 
            this.chbGrupo.AutoSize = true;
            this.chbGrupo.Location = new System.Drawing.Point(17, 83);
            this.chbGrupo.Name = "chbGrupo";
            this.chbGrupo.Size = new System.Drawing.Size(200, 17);
            this.chbGrupo.TabIndex = 65;
            this.chbGrupo.Text = "Agregar todos los permisos por grupo";
            this.chbGrupo.UseVisualStyleBackColor = true;
            this.chbGrupo.CheckedChanged += new System.EventHandler(this.chbGrupo_CheckedChanged);
            // 
            // chbAccion
            // 
            this.chbAccion.AutoSize = true;
            this.chbAccion.Location = new System.Drawing.Point(17, 83);
            this.chbAccion.Name = "chbAccion";
            this.chbAccion.Size = new System.Drawing.Size(205, 17);
            this.chbAccion.TabIndex = 66;
            this.chbAccion.Text = "Agregar todos los permisos por accion";
            this.chbAccion.UseVisualStyleBackColor = true;
            this.chbAccion.CheckedChanged += new System.EventHandler(this.chbAccion_CheckedChanged);
            // 
            // frmMenuRolesYPermisos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1772, 812);
            this.Controls.Add(this.dgvPermisosSeleccionados);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(504, 14);
            this.Name = "frmMenuRolesYPermisos";
            this.Text = "frmMenuRolesYPermisos";
            this.Load += new System.EventHandler(this.frmMenuRolesYPermisos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbPermisos.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.gbAccion.ResumeLayout(false);
            this.gbAccion.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gbGrupo.ResumeLayout(false);
            this.gbGrupo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermisosSeleccionados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.GroupBox groupBox1;
        private FontAwesome.Sharp.IconButton btnRegistrarRol;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbPermisos;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton btnGrupo;
        private System.Windows.Forms.GroupBox gbGrupo;
        private System.Windows.Forms.ComboBox cboGrupo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvPermisosSeleccionados;
        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconButton btnAccion;
        private System.Windows.Forms.GroupBox gbAccion;
        private System.Windows.Forms.ComboBox cboAccion;
        private FontAwesome.Sharp.IconButton btnAgregarAccion;
        private System.Windows.Forms.Label label4;
        private FontAwesome.Sharp.IconButton btnAgreagar1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoPermiso;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreMenu;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreAccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescripcionPermiso;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdGrupo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdAccion;
        private System.Windows.Forms.DataGridViewButtonColumn btnEliminar;
        private System.Windows.Forms.CheckBox chbGrupo;
        private System.Windows.Forms.CheckBox chbAccion;
    }
}