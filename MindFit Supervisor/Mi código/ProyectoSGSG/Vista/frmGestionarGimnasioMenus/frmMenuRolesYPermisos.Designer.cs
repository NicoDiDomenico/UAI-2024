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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbPermiso = new System.Windows.Forms.GroupBox();
            this.cboPermiso = new System.Windows.Forms.ComboBox();
            this.btnAgreagar = new FontAwesome.Sharp.IconButton();
            this.label7 = new System.Windows.Forms.Label();
            this.btnRegistrarRol = new FontAwesome.Sharp.IconButton();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.dgvPermisosSeleccionados = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreMenu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescripcionPermiso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdGrupo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox1.SuspendLayout();
            this.gbPermiso.SuspendLayout();
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
            this.label2.Location = new System.Drawing.Point(18, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Nombre del Rol:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(107, 72);
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
            this.groupBox1.Controls.Add(this.gbPermiso);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.btnRegistrarRol);
            this.groupBox1.Controls.Add(this.btnLimpiar);
            this.groupBox1.Location = new System.Drawing.Point(31, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 553);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            // 
            // gbPermiso
            // 
            this.gbPermiso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.gbPermiso.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gbPermiso.Controls.Add(this.cboPermiso);
            this.gbPermiso.Controls.Add(this.btnAgreagar);
            this.gbPermiso.Controls.Add(this.label7);
            this.gbPermiso.Enabled = false;
            this.gbPermiso.Location = new System.Drawing.Point(21, 119);
            this.gbPermiso.Name = "gbPermiso";
            this.gbPermiso.Size = new System.Drawing.Size(334, 92);
            this.gbPermiso.TabIndex = 64;
            this.gbPermiso.TabStop = false;
            this.gbPermiso.Text = "Agregar Permiso para Rol";
            // 
            // cboPermiso
            // 
            this.cboPermiso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPermiso.FormattingEnabled = true;
            this.cboPermiso.Location = new System.Drawing.Point(17, 34);
            this.cboPermiso.Name = "cboPermiso";
            this.cboPermiso.Size = new System.Drawing.Size(218, 21);
            this.cboPermiso.TabIndex = 40;
            // 
            // btnAgreagar
            // 
            this.btnAgreagar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgreagar.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnAgreagar.IconColor = System.Drawing.Color.ForestGreen;
            this.btnAgreagar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAgreagar.IconSize = 60;
            this.btnAgreagar.Location = new System.Drawing.Point(254, 19);
            this.btnAgreagar.Name = "btnAgreagar";
            this.btnAgreagar.Size = new System.Drawing.Size(60, 60);
            this.btnAgreagar.TabIndex = 63;
            this.btnAgreagar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAgreagar.UseVisualStyleBackColor = true;
            this.btnAgreagar.Click += new System.EventHandler(this.btnAgreagar_Click);
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
            this.btnRegistrarRol.Location = new System.Drawing.Point(88, 454);
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
            this.btnLimpiar.Location = new System.Drawing.Point(88, 490);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(199, 29);
            this.btnLimpiar.TabIndex = 43;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // dgvPermisosSeleccionados
            // 
            this.dgvPermisosSeleccionados.AllowUserToAddRows = false;
            this.dgvPermisosSeleccionados.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPermisosSeleccionados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPermisosSeleccionados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPermisosSeleccionados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Descripcion,
            this.NombreMenu,
            this.DescripcionPermiso,
            this.IdGrupo,
            this.btnEliminar});
            this.dgvPermisosSeleccionados.Location = new System.Drawing.Point(504, 40);
            this.dgvPermisosSeleccionados.MultiSelect = false;
            this.dgvPermisosSeleccionados.Name = "dgvPermisosSeleccionados";
            this.dgvPermisosSeleccionados.ReadOnly = true;
            this.dgvPermisosSeleccionados.RowHeadersWidth = 34;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvPermisosSeleccionados.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPermisosSeleccionados.RowTemplate.Height = 42;
            this.dgvPermisosSeleccionados.Size = new System.Drawing.Size(1238, 553);
            this.dgvPermisosSeleccionados.TabIndex = 64;
            this.dgvPermisosSeleccionados.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPermisosSeleccionados_CellContentClick);
            this.dgvPermisosSeleccionados.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvPermisosSeleccionados_CellPainting);
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
            // Descripcion
            // 
            this.Descripcion.HeaderText = "Rol";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            this.Descripcion.Width = 150;
            // 
            // NombreMenu
            // 
            this.NombreMenu.HeaderText = "Permiso";
            this.NombreMenu.Name = "NombreMenu";
            this.NombreMenu.ReadOnly = true;
            this.NombreMenu.Width = 150;
            // 
            // DescripcionPermiso
            // 
            this.DescripcionPermiso.HeaderText = "Descripcion Permiso";
            this.DescripcionPermiso.Name = "DescripcionPermiso";
            this.DescripcionPermiso.ReadOnly = true;
            this.DescripcionPermiso.Width = 800;
            // 
            // IdGrupo
            // 
            this.IdGrupo.HeaderText = "IdGrupo";
            this.IdGrupo.Name = "IdGrupo";
            this.IdGrupo.ReadOnly = true;
            this.IdGrupo.Visible = false;
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
            this.gbPermiso.ResumeLayout(false);
            this.gbPermiso.PerformLayout();
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboPermiso;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbPermiso;
        private FontAwesome.Sharp.IconButton btnAgreagar;
        private System.Windows.Forms.DataGridView dgvPermisosSeleccionados;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreMenu;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescripcionPermiso;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdGrupo;
        private System.Windows.Forms.DataGridViewButtonColumn btnEliminar;
    }
}