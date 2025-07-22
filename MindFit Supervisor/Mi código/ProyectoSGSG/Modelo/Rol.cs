﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string Descripcion { get; set; }
        public List<Permiso> Permisos { get; set; }
        public DateTime FechaRegistro { get; set; }

        public List<string> Acciones {  get; set; }
    }
}
