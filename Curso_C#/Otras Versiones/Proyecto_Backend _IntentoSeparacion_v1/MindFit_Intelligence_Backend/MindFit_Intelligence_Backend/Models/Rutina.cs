using MindFit_Intelligence_Backend.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindFit_Intelligence_Backend.Models
{
    public class Rutina
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRutina { get; set; }

        public DateTime FechaModificacion { get; set; }

        [ForeignKey(nameof(PersonaSocio))]
        public int IdPersonaSocio { get; set; }
        public PersonaSocio PersonaSocio { get; set; } = null!;

        [ForeignKey(nameof(Dia))]
        public int IdDia { get; set; }
        public Dia Dia { get; set; } = null!;

        public bool Activo { get; set; }
    }
}
