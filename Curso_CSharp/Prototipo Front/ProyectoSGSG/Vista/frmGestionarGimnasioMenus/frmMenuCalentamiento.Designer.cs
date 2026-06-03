namespace Vista
{
    partial class frmMenuCalentamiento
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txbDescripcion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gbEntrenador = new System.Windows.Forms.GroupBox();
            this.btnLimpiarMaquina = new FontAwesome.Sharp.IconButton();
            this.txtIdMaquina = new System.Windows.Forms.TextBox();
            this.btnBuscarMaquina = new FontAwesome.Sharp.IconButton();
            this.txtNombreMaquina = new System.Windows.Forms.TextBox();
            this.Nombre = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.btnGuardar = new FontAwesome.Sharp.IconButton();
            this.btnEliminar = new FontAwesome.Sharp.IconButton();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cboBusqueda = new System.Windows.Forms.ComboBox();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.btnBuscar = new FontAwesome.Sharp.IconButton();
            this.btnLimpiarBuscador = new FontAwesome.Sharp.IconButton();
            this.label10 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnSeleccionar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.IdCalentamiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdMaquina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescripcionCalentamiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Seleccionado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txtIndice = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.gbEntrenador.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.groupBox1.Controls.Add(this.txbDescripcion);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.gbEntrenador);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.btnGuardar);
            this.groupBox1.Controls.Add(this.btnEliminar);
            this.groupBox1.Controls.Add(this.btnLimpiar);
            this.groupBox1.Location = new System.Drawing.Point(33, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 626);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            // 
            // txbDescripcion
            // 
            this.txbDescripcion.Location = new System.Drawing.Point(93, 71);
            this.txbDescripcion.Multiline = true;
            this.txbDescripcion.Name = "txbDescripcion";
            this.txbDescripcion.Size = new System.Drawing.Size(277, 181);
            this.txbDescripcion.TabIndex = 69;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label5.Location = new System.Drawing.Point(21, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 68;
            this.label5.Text = "Descripción:";
            // 
            // gbEntrenador
            // 
            this.gbEntrenador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.gbEntrenador.Controls.Add(this.btnLimpiarMaquina);
            this.gbEntrenador.Controls.Add(this.txtIdMaquina);
            this.gbEntrenador.Controls.Add(this.btnBuscarMaquina);
            this.gbEntrenador.Controls.Add(this.txtNombreMaquina);
            this.gbEntrenador.Controls.Add(this.Nombre);
            this.gbEntrenador.Location = new System.Drawing.Point(24, 277);
            this.gbEntrenador.Name = "gbEntrenador";
            this.gbEntrenador.Size = new System.Drawing.Size(346, 85);
            this.gbEntrenador.TabIndex = 67;
            this.gbEntrenador.TabStop = false;
            this.gbEntrenador.Text = "Máquina";
            // 
            // btnLimpiarMaquina
            // 
            this.btnLimpiarMaquina.BackColor = System.Drawing.Color.White;
            this.btnLimpiarMaquina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiarMaquina.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLimpiarMaquina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarMaquina.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarMaquina.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.btnLimpiarMaquina.IconColor = System.Drawing.Color.Black;
            this.btnLimpiarMaquina.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLimpiarMaquina.IconSize = 20;
            this.btnLimpiarMaquina.Location = new System.Drawing.Point(299, 44);
            this.btnLimpiarMaquina.Name = "btnLimpiarMaquina";
            this.btnLimpiarMaquina.Size = new System.Drawing.Size(40, 20);
            this.btnLimpiarMaquina.TabIndex = 54;
            this.btnLimpiarMaquina.UseVisualStyleBackColor = false;
            this.btnLimpiarMaquina.Click += new System.EventHandler(this.btnLimpiarMaquina_Click);
            // 
            // txtIdMaquina
            // 
            this.txtIdMaquina.Location = new System.Drawing.Point(312, 13);
            this.txtIdMaquina.Name = "txtIdMaquina";
            this.txtIdMaquina.Size = new System.Drawing.Size(28, 20);
            this.txtIdMaquina.TabIndex = 31;
            this.txtIdMaquina.Text = "0";
            this.txtIdMaquina.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnBuscarMaquina
            // 
            this.btnBuscarMaquina.BackColor = System.Drawing.Color.White;
            this.btnBuscarMaquina.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBuscarMaquina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarMaquina.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnBuscarMaquina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarMaquina.ForeColor = System.Drawing.Color.White;
            this.btnBuscarMaquina.IconChar = FontAwesome.Sharp.IconChar.Searchengin;
            this.btnBuscarMaquina.IconColor = System.Drawing.Color.Black;
            this.btnBuscarMaquina.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscarMaquina.IconSize = 20;
            this.btnBuscarMaquina.Location = new System.Drawing.Point(253, 44);
            this.btnBuscarMaquina.Name = "btnBuscarMaquina";
            this.btnBuscarMaquina.Size = new System.Drawing.Size(40, 20);
            this.btnBuscarMaquina.TabIndex = 29;
            this.btnBuscarMaquina.UseVisualStyleBackColor = false;
            this.btnBuscarMaquina.Click += new System.EventHandler(this.btnBuscarMaquina_Click);
            // 
            // txtNombreMaquina
            // 
            this.txtNombreMaquina.Enabled = false;
            this.txtNombreMaquina.Location = new System.Drawing.Point(15, 44);
            this.txtNombreMaquina.Name = "txtNombreMaquina";
            this.txtNombreMaquina.Size = new System.Drawing.Size(233, 20);
            this.txtNombreMaquina.TabIndex = 26;
            this.txtNombreMaquina.Text = "Sin Máquina";
            // 
            // Nombre
            // 
            this.Nombre.AutoSize = true;
            this.Nombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.Nombre.Location = new System.Drawing.Point(12, 28);
            this.Nombre.Name = "Nombre";
            this.Nombre.Size = new System.Drawing.Size(47, 13);
            this.Nombre.TabIndex = 24;
            this.Nombre.Text = "Nombre:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(209, 25);
            this.label9.TabIndex = 46;
            this.label9.Text = "Detalle Calentamiento:";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(342, 24);
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
            this.btnGuardar.Location = new System.Drawing.Point(127, 383);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(199, 29);
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
            this.btnEliminar.Location = new System.Drawing.Point(127, 453);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(199, 29);
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
            this.btnLimpiar.Location = new System.Drawing.Point(127, 418);
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
            this.label1.Size = new System.Drawing.Size(465, 656);
            this.label1.TabIndex = 58;
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
            this.groupBox2.Location = new System.Drawing.Point(1220, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(511, 59);
            this.groupBox2.TabIndex = 63;
            this.groupBox2.TabStop = false;
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
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(504, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1238, 67);
            this.label10.TabIndex = 60;
            this.label10.Text = "Lista de Calentamientos:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.IdCalentamiento,
            this.IdMaquina,
            this.DescripcionCalentamiento,
            this.Seleccionado});
            this.dgvData.Location = new System.Drawing.Point(504, 86);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.RowTemplate.Height = 35;
            this.dgvData.Size = new System.Drawing.Size(1238, 502);
            this.dgvData.TabIndex = 59;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            this.dgvData.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvData_CellPainting);
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
            // IdCalentamiento
            // 
            this.IdCalentamiento.HeaderText = "IdCalentamiento";
            this.IdCalentamiento.Name = "IdCalentamiento";
            this.IdCalentamiento.ReadOnly = true;
            this.IdCalentamiento.Visible = false;
            // 
            // IdMaquina
            // 
            this.IdMaquina.HeaderText = "IdMaquina";
            this.IdMaquina.Name = "IdMaquina";
            this.IdMaquina.ReadOnly = true;
            this.IdMaquina.Visible = false;
            // 
            // DescripcionCalentamiento
            // 
            this.DescripcionCalentamiento.HeaderText = "Descripcion";
            this.DescripcionCalentamiento.Name = "DescripcionCalentamiento";
            this.DescripcionCalentamiento.ReadOnly = true;
            this.DescripcionCalentamiento.Width = 1160;
            // 
            // Seleccionado
            // 
            this.Seleccionado.HeaderText = "Seleccionado";
            this.Seleccionado.Name = "Seleccionado";
            this.Seleccionado.ReadOnly = true;
            this.Seleccionado.Visible = false;
            // 
            // txtIndice
            // 
            this.txtIndice.Location = new System.Drawing.Point(512, 90);
            this.txtIndice.Name = "txtIndice";
            this.txtIndice.Size = new System.Drawing.Size(28, 20);
            this.txtIndice.TabIndex = 61;
            this.txtIndice.Text = "-1";
            this.txtIndice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frmMenuCalentamiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1772, 656);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtIndice);
            this.Controls.Add(this.dgvData);
            this.Name = "frmMenuCalentamiento";
            this.Text = "frmMenuCalentamiento";
            this.Load += new System.EventHandler(this.frmMenuCalentamiento_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbEntrenador.ResumeLayout(false);
            this.gbEntrenador.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnBuscar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtId;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton btnEliminar;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboBusqueda;
        private System.Windows.Forms.TextBox txtBusqueda;
        private FontAwesome.Sharp.IconButton btnLimpiarBuscador;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.TextBox txtIndice;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox gbEntrenador;
        private System.Windows.Forms.TextBox txtIdMaquina;
        private FontAwesome.Sharp.IconButton btnBuscarMaquina;
        private System.Windows.Forms.TextBox txtNombreMaquina;
        private System.Windows.Forms.Label Nombre;
        private System.Windows.Forms.TextBox txbDescripcion;
        private System.Windows.Forms.Label label5;
        private FontAwesome.Sharp.IconButton btnLimpiarMaquina;
        private System.Windows.Forms.DataGridViewButtonColumn btnSeleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCalentamiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdMaquina;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescripcionCalentamiento;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionado;
    }
}