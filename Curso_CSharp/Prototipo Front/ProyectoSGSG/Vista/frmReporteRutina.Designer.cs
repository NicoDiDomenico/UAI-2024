namespace Vista
{
    partial class frmReporteRutina
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSocio = new System.Windows.Forms.Label();
            this.lblDia = new System.Windows.Forms.Label();
            this.lblEntrenador = new System.Windows.Forms.Label();
            this.dgvCalentamientos = new System.Windows.Forms.DataGridView();
            this.dgvEntrenamientos = new System.Windows.Forms.DataGridView();
            this.dgvEstiramientos = new System.Windows.Forms.DataGridView();
            this.chartHistorial = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnExportar = new FontAwesome.Sharp.IconButton();
            this.Actividad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Duración = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Actividad2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Duración2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Elemento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Series = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Repeticiones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PesoKG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalentamientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntrenamientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstiramientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartHistorial)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(1106, 49);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(71, 20);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "lblTitulo";
            this.lblTitulo.Click += new System.EventHandler(this.lblTitulo_Click);
            // 
            // lblSocio
            // 
            this.lblSocio.AutoSize = true;
            this.lblSocio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSocio.ForeColor = System.Drawing.Color.White;
            this.lblSocio.Location = new System.Drawing.Point(662, 49);
            this.lblSocio.Name = "lblSocio";
            this.lblSocio.Size = new System.Drawing.Size(72, 20);
            this.lblSocio.TabIndex = 1;
            this.lblSocio.Text = "lblSocio";
            this.lblSocio.Click += new System.EventHandler(this.lblSocio_Click);
            // 
            // lblDia
            // 
            this.lblDia.AutoSize = true;
            this.lblDia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDia.ForeColor = System.Drawing.Color.White;
            this.lblDia.Location = new System.Drawing.Point(1549, 49);
            this.lblDia.Name = "lblDia";
            this.lblDia.Size = new System.Drawing.Size(54, 20);
            this.lblDia.TabIndex = 2;
            this.lblDia.Text = "lblDia";
            this.lblDia.Click += new System.EventHandler(this.lblDia_Click);
            // 
            // lblEntrenador
            // 
            this.lblEntrenador.AutoSize = true;
            this.lblEntrenador.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntrenador.ForeColor = System.Drawing.Color.White;
            this.lblEntrenador.Location = new System.Drawing.Point(173, 49);
            this.lblEntrenador.Name = "lblEntrenador";
            this.lblEntrenador.Size = new System.Drawing.Size(117, 20);
            this.lblEntrenador.TabIndex = 3;
            this.lblEntrenador.Text = "lblEntrenador";
            this.lblEntrenador.Click += new System.EventHandler(this.lblEntrenador_Click);
            // 
            // dgvCalentamientos
            // 
            this.dgvCalentamientos.BackgroundColor = System.Drawing.Color.White;
            this.dgvCalentamientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalentamientos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Actividad,
            this.Duración});
            this.dgvCalentamientos.Location = new System.Drawing.Point(47, 117);
            this.dgvCalentamientos.Name = "dgvCalentamientos";
            this.dgvCalentamientos.Size = new System.Drawing.Size(906, 150);
            this.dgvCalentamientos.TabIndex = 4;
            this.dgvCalentamientos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCalentamientos_CellContentClick);
            // 
            // dgvEntrenamientos
            // 
            this.dgvEntrenamientos.BackgroundColor = System.Drawing.Color.White;
            this.dgvEntrenamientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEntrenamientos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Elemento,
            this.Series,
            this.Repeticiones,
            this.PesoKG});
            this.dgvEntrenamientos.Location = new System.Drawing.Point(47, 273);
            this.dgvEntrenamientos.Name = "dgvEntrenamientos";
            this.dgvEntrenamientos.Size = new System.Drawing.Size(906, 150);
            this.dgvEntrenamientos.TabIndex = 5;
            this.dgvEntrenamientos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEntrenamientos_CellContentClick);
            // 
            // dgvEstiramientos
            // 
            this.dgvEstiramientos.BackgroundColor = System.Drawing.Color.White;
            this.dgvEstiramientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEstiramientos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Actividad2,
            this.Duración2});
            this.dgvEstiramientos.Location = new System.Drawing.Point(47, 429);
            this.dgvEstiramientos.Name = "dgvEstiramientos";
            this.dgvEstiramientos.Size = new System.Drawing.Size(906, 150);
            this.dgvEstiramientos.TabIndex = 6;
            this.dgvEstiramientos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEstiramientos_CellContentClick);
            // 
            // chartHistorial
            // 
            chartArea3.Name = "ChartArea1";
            this.chartHistorial.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartHistorial.Legends.Add(legend3);
            this.chartHistorial.Location = new System.Drawing.Point(995, 117);
            this.chartHistorial.Name = "chartHistorial";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartHistorial.Series.Add(series3);
            this.chartHistorial.Size = new System.Drawing.Size(744, 462);
            this.chartHistorial.TabIndex = 7;
            this.chartHistorial.Text = "chart1";
            // 
            // btnExportar
            // 
            this.btnExportar.BackColor = System.Drawing.Color.Silver;
            this.btnExportar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnExportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.ForeColor = System.Drawing.Color.Black;
            this.btnExportar.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.btnExportar.IconColor = System.Drawing.Color.Black;
            this.btnExportar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExportar.IconSize = 20;
            this.btnExportar.Location = new System.Drawing.Point(835, 635);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(118, 29);
            this.btnExportar.TabIndex = 33;
            this.btnExportar.Text = "Exportar PDF";
            this.btnExportar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // Actividad
            // 
            this.Actividad.HeaderText = "Actividad";
            this.Actividad.Name = "Actividad";
            this.Actividad.Width = 700;
            // 
            // Duración
            // 
            this.Duración.HeaderText = "Duración (min.)";
            this.Duración.Name = "Duración";
            this.Duración.Width = 150;
            // 
            // Actividad2
            // 
            this.Actividad2.HeaderText = "Actividad";
            this.Actividad2.Name = "Actividad2";
            this.Actividad2.Width = 700;
            // 
            // Duración2
            // 
            this.Duración2.HeaderText = "Duración (min.)";
            this.Duración2.Name = "Duración2";
            this.Duración2.Width = 150;
            // 
            // Elemento
            // 
            this.Elemento.HeaderText = "Elemento";
            this.Elemento.Name = "Elemento";
            this.Elemento.Width = 550;
            // 
            // Series
            // 
            this.Series.HeaderText = "Series";
            this.Series.Name = "Series";
            // 
            // Repeticiones
            // 
            this.Repeticiones.HeaderText = "Repeticiones";
            this.Repeticiones.Name = "Repeticiones";
            // 
            // PesoKG
            // 
            this.PesoKG.HeaderText = "Peso (KG)";
            this.PesoKG.Name = "PesoKG";
            // 
            // frmReporteRutina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(76)))), ((int)(((byte)(127)))));
            this.ClientSize = new System.Drawing.Size(1788, 727);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.chartHistorial);
            this.Controls.Add(this.dgvEstiramientos);
            this.Controls.Add(this.dgvEntrenamientos);
            this.Controls.Add(this.dgvCalentamientos);
            this.Controls.Add(this.lblEntrenador);
            this.Controls.Add(this.lblDia);
            this.Controls.Add(this.lblSocio);
            this.Controls.Add(this.lblTitulo);
            this.Name = "frmReporteRutina";
            this.Text = "frmReporteRutina";
            this.Load += new System.EventHandler(this.frmReporteRutina_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalentamientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntrenamientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstiramientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartHistorial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSocio;
        private System.Windows.Forms.Label lblDia;
        private System.Windows.Forms.Label lblEntrenador;
        private System.Windows.Forms.DataGridView dgvCalentamientos;
        private System.Windows.Forms.DataGridView dgvEntrenamientos;
        private System.Windows.Forms.DataGridView dgvEstiramientos;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartHistorial;
        private FontAwesome.Sharp.IconButton btnExportar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Actividad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Duración;
        private System.Windows.Forms.DataGridViewTextBoxColumn Elemento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Series;
        private System.Windows.Forms.DataGridViewTextBoxColumn Repeticiones;
        private System.Windows.Forms.DataGridViewTextBoxColumn PesoKG;
        private System.Windows.Forms.DataGridViewTextBoxColumn Actividad2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Duración2;
    }
}