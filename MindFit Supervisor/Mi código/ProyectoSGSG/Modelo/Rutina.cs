using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Rutina
    {
        public int IdRutina { get; set; }
        public int IdSocio { get; set; } // Clave foránea
        public DateTime FechaModificacion { get; set; }
        public string Dia { get; set; } // "Lunes", "Martes", etc.

        // Relación con Socio
        public Socio Socio { get; set; } // No creo que la use, por las dudas la dejo
        public List<Estiramiento> Estiramientos { get; set; } = new List<Estiramiento>();
        public List<Calentamiento> Calentamientos { get; set; } = new List<Calentamiento>();
        public List<Entrenamiento> Entrenamientos { get; set; } = new List<Entrenamiento>();
    }
}