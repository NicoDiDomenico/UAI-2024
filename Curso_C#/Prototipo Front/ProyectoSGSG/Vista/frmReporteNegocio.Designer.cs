namespace Vista
{
    partial class frmReporteNegocio
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnExportar = new FontAwesome.Sharp.IconButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTituloSocios = new System.Windows.Forms.Label();
            this.lblTituloChart = new System.Windows.Forms.Label();
            this.dgvSociosInactivos = new System.Windows.Forms.DataGridView();
            this.chartEstados = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.NombreCompleto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UltimoTurno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSociosInactivos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartEstados)).BeginInit();
            this.SuspendLayout();
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
            this.btnExportar.Location = new System.Drawing.Point(586, 511);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(118, 29);
            this.btnExportar.TabIndex = 32;
            this.btnExportar.Text = "Exportar PDF";
            this.btnExportar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click_1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.lblTituloSocios, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chartEstados, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgvSociosInactivos, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTituloChart, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(29, 32);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1131, 453);
            this.tableLayoutPanel1.TabIndex = 33;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // lblTituloSocios
            // 
            this.lblTituloSocios.AutoSize = true;
            this.lblTituloSocios.Location = new System.Drawing.Point(3, 0);
            this.lblTituloSocios.Name = "lblTituloSocios";
            this.lblTituloSocios.Size = new System.Drawing.Size(75, 13);
            this.lblTituloSocios.TabIndex = 35;
            this.lblTituloSocios.Text = "lblTituloSocios";
            this.lblTituloSocios.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTituloChart
            // 
            this.lblTituloChart.AutoSize = true;
            this.lblTituloChart.Location = new System.Drawing.Point(681, 0);
            this.lblTituloChart.Name = "lblTituloChart";
            this.lblTituloChart.Size = new System.Drawing.Size(68, 13);
            this.lblTituloChart.TabIndex = 36;
            this.lblTituloChart.Text = "lblTituloChart";
            this.lblTituloChart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvSociosInactivos
            // 
            this.dgvSociosInactivos.BackgroundColor = System.Drawing.Color.White;
            this.dgvSociosInactivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSociosInactivos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NombreCompleto,
            this.Email,
            this.Estado,
            this.UltimoTurno});
            this.dgvSociosInactivos.Location = new System.Drawing.Point(3, 48);
            this.dgvSociosInactivos.Name = "dgvSociosInactivos";
            this.dgvSociosInactivos.Size = new System.Drawing.Size(672, 402);
            this.dgvSociosInactivos.TabIndex = 4;
            // 
            // chartEstados
            // 
            this.chartEstados.BorderlineColor = System.Drawing.Color.Black;
            this.chartEstados.BorderlineWidth = 2;
            this.chartEstados.BorderSkin.BackColor = System.Drawing.Color.Black;
            this.chartEstados.BorderSkin.BorderWidth = 2;
            chartArea2.Name = "ChartArea1";
            this.chartEstados.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartEstados.Legends.Add(legend2);
            this.chartEstados.Location = new System.Drawing.Point(681, 48);
            this.chartEstados.Name = "chartEstados";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartEstados.Series.Add(series2);
            this.chartEstados.Size = new System.Drawing.Size(444, 402);
            this.chartEstados.TabIndex = 5;
            this.chartEstados.Text = "chart1";
            // 
            // NombreCompleto
            // 
            this.NombreCompleto.HeaderText = "Nombre Completo";
            this.NombreCompleto.Name = "NombreCompleto";
            this.NombreCompleto.Width = 150;
            // 
            // Email
            // 
            this.Email.HeaderText = "Email";
            this.Email.Name = "Email";
            this.Email.Width = 200;
            // 
            // Estado
            // 
            this.Estado.HeaderText = "Estado";
            this.Estado.Name = "Estado";
            this.Estado.Width = 130;
            // 
            // UltimoTurno
            // 
            this.UltimoTurno.HeaderText = "Último Turno";
            this.UltimoTurno.Name = "UltimoTurno";
            this.UltimoTurno.Width = 130;
            // 
            // frmReporteNegocio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 583);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnExportar);
            this.Name = "frmReporteNegocio";
            this.Text = "frmReporteNegocio";
            this.Load += new System.EventHandler(this.frmReporteNegocio_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSociosInactivos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartEstados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private FontAwesome.Sharp.IconButton btnExportar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblTituloSocios;
        private System.Windows.Forms.Label lblTituloChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEstados;
        private System.Windows.Forms.DataGridView dgvSociosInactivos;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreCompleto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn UltimoTurno;
    }
}