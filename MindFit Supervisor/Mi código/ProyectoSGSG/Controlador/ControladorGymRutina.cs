﻿using DAO;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ControladorGymRutina
    {
        private RutinaDAO daoRutina = new RutinaDAO();
        public List<Rutina> Listar(int idSocioSeleccionado)
        {
            List<Rutina> rutinas = daoRutina.Listar(idSocioSeleccionado);

            return rutinas;
        }
    }
}
