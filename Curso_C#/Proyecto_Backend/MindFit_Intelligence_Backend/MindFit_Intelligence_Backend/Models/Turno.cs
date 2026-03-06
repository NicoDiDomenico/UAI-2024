using MindFit_Intelligence_Backend.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindFit_Intelligence_Backend.Models
{
    public class Turno
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTurno { get; set; }

        [ForeignKey(nameof(PersonaResponsable))]
        public int IdUsuarioResponsable { get; set; }

        public PersonaResponsable PersonaResponsable { get; set; } = null!;

        [ForeignKey(nameof(PersonaSocio))]
        public int IdUsuarioSocio { get; set; }

        public PersonaSocio PersonaSocio { get; set; } = null!;
        /*
        [ForeignKey(nameof(RangoHorario))]
        public int IdRangoHorario { get; set; }

        public RangoHorario RangoHorario { get; set; } = null!;
        */
        [Column(TypeName = "date")]
        public DateTime FechaTurno { get; set; }

        public EstadoTurno EstadoTurno { get; private set; }

        [Column(TypeName = "varchar(50)")]
        public string CodigoIngreso { get; set; } = null!;
    }
}
