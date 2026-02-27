using MindFit_Intelligence_Backend.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindFit_Intelligence_Backend.DTOs.Rutina
{
    public class RutinaDto
    {
        public int IdRutina { get; set; }

        public DateTime FechaModificacion { get; set; }

        public int IdDia { get; set; }
    }
}
