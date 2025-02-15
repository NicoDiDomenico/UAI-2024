﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using pjGestionEmpleados.Datos;

namespace pjGestionEmpleados.Presentacion
{
    public partial class frmConectar : Form
    {
        public frmConectar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection SqlCon = new SqlConnection();
            
            Conexion unaConexion = Conexion.crearInstancia();
            
            SqlCon = unaConexion.CrearConexion();

            try 
            { 
                SqlCon.Open();
                MessageBox.Show("Conexion exitosa");
            } 
            catch ( Exception ex)
            { 
                MessageBox.Show("Conexion fallida");
                MessageBox.Show(ex.Message); 
            }    
        }

        private void frmConectar_Load(object sender, EventArgs e)
        {

        }
    }
}
