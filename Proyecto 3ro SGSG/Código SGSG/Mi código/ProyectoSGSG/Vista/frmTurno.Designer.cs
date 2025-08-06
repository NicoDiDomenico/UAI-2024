namespace Vista
{
    partial class frmTurno
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNuevoTurno = new FontAwesome.Sharp.IconButton();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSocio = new System.Windows.Forms.Label();
            this.btnCancelar = new FontAwesome.Sharp.IconButton();
            this.btnSeleccionar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.IdTurno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaTurno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdRangoHorario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoraDesde = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoraHasta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoTurno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoIngreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreEntrenador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Seleccionado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancelar);
            this.panel1.Controls.Add(this.btnNuevoTurno);
            this.panel1.Controls.Add(this.dgvData);
            this.panel1.Location = new System.Drawing.Point(12, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1752, 675);
            this.panel1.TabIndex = 0;
            // 
            // btnNuevoTurno
            // 
            this.btnNuevoTurno.BackColor = System.Drawing.Color.AliceBlue;
            this.btnNuevoTurno.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoTurno.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnNuevoTurno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoTurno.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoTurno.ForeColor = System.Drawing.Color.Black;
            this.btnNuevoTurno.IconChar = FontAwesome.Sharp.IconChar.PlusSquare;
            this.btnNuevoTurno.IconColor = System.Drawing.Color.Black;
            this.btnNuevoTurno.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNuevoTurno.IconSize = 30;
            this.btnNuevoTurno.Location = new System.Drawing.Point(787, 599);
            this.btnNuevoTurno.Name = "btnNuevoTurno";
            this.btnNuevoTurno.Size = new System.Drawing.Size(170, 56);
            this.btnNuevoTurno.TabIndex = 97;
            this.btnNuevoTurno.Text = "Nuevo Turno";
            this.btnNuevoTurno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNuevoTurno.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNuevoTurno.UseVisualStyleBackColor = false;
            this.btnNuevoTurno.Click += new System.EventHandler(this.btnNuevoTurno_Click);
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
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnSeleccionar,
            this.IdTurno,
            this.FechaTurno,
            this.IdRangoHorario,
            this.HoraDesde,
            this.HoraHasta,
            this.EstadoTurno,
            this.CodigoIngreso,
            this.IdUsuario,
            this.NombreEntrenador,
            this.IdSocio,
            this.NombreSocio,
            this.btnEliminar,
            this.Seleccionado});
            this.dgvData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.dgvData.Location = new System.Drawing.Point(165, 30);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
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
            this.dgvData.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.dgvData.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvData.RowTemplate.Height = 40;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1435, 553);
            this.dgvData.TabIndex = 66;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            this.dgvData.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvData_CellPainting);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Historial de Turnos:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1153, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Socio:";
            // 
            // lblSocio
            // 
            this.lblSocio.AutoSize = true;
            this.lblSocio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSocio.Location = new System.Drawing.Point(1278, 18);
            this.lblSocio.Name = "lblSocio";
            this.lblSocio.Size = new System.Drawing.Size(64, 20);
            this.lblSocio.TabIndex = 3;
            this.lblSocio.Text = "lblSocio";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Firebrick;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.IconChar = FontAwesome.Sharp.IconChar.CalendarMinus;
            this.btnCancelar.IconColor = System.Drawing.Color.White;
            this.btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCancelar.IconSize = 26;
            this.btnCancelar.Location = new System.Drawing.Point(165, 603);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(156, 50);
            this.btnCancelar.TabIndex = 98;
            this.btnCancelar.Text = "Cancelar Turno";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
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
            // IdTurno
            // 
            this.IdTurno.HeaderText = "IdTurno";
            this.IdTurno.Name = "IdTurno";
            this.IdTurno.ReadOnly = true;
            this.IdTurno.Visible = false;
            // 
            // FechaTurno
            // 
            this.FechaTurno.HeaderText = "Fecha Turno";
            this.FechaTurno.Name = "FechaTurno";
            this.FechaTurno.ReadOnly = true;
            this.FechaTurno.Width = 250;
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
            this.HoraDesde.Width = 170;
            // 
            // HoraHasta
            // 
            this.HoraHasta.HeaderText = "Hora Hasta";
            this.HoraHasta.Name = "HoraHasta";
            this.HoraHasta.ReadOnly = true;
            this.HoraHasta.Width = 170;
            // 
            // EstadoTurno
            // 
            this.EstadoTurno.HeaderText = "Estado Turno";
            this.EstadoTurno.Name = "EstadoTurno";
            this.EstadoTurno.ReadOnly = true;
            this.EstadoTurno.Width = 250;
            // 
            // CodigoIngreso
            // 
            this.CodigoIngreso.HeaderText = "Codigo Ingreso";
            this.CodigoIngreso.Name = "CodigoIngreso";
            this.CodigoIngreso.ReadOnly = true;
            this.CodigoIngreso.Width = 215;
            // 
            // IdUsuario
            // 
            this.IdUsuario.HeaderText = "IdUsuario";
            this.IdUsuario.Name = "IdUsuario";
            this.IdUsuario.ReadOnly = true;
            this.IdUsuario.Visible = false;
            // 
            // NombreEntrenador
            // 
            this.NombreEntrenador.HeaderText = "Entrenador";
            this.NombreEntrenador.Name = "NombreEntrenador";
            this.NombreEntrenador.ReadOnly = true;
            this.NombreEntrenador.Width = 300;
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
            this.NombreSocio.Visible = false;
            this.NombreSocio.Width = 250;
            // 
            // btnEliminar
            // 
            this.btnEliminar.HeaderText = "";
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.ReadOnly = true;
            this.btnEliminar.Width = 45;
            // 
            // Seleccionado
            // 
            this.Seleccionado.HeaderText = "Seleccionado";
            this.Seleccionado.Name = "Seleccionado";
            this.Seleccionado.ReadOnly = true;
            this.Seleccionado.Visible = false;
            // 
            // frmTurno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1788, 727);
            this.Controls.Add(this.lblSocio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTurno";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTurno";
            this.Load += new System.EventHandler(this.frmTurno_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSocio;
        private FontAwesome.Sharp.IconButton btnNuevoTurno;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private System.Windows.Forms.DataGridViewButtonColumn btnSeleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdTurno;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaTurno;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdRangoHorario;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoraDesde;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoraHasta;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoTurno;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoIngreso;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreEntrenador;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdSocio;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreSocio;
        private System.Windows.Forms.DataGridViewButtonColumn btnEliminar;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionado;
    }
}