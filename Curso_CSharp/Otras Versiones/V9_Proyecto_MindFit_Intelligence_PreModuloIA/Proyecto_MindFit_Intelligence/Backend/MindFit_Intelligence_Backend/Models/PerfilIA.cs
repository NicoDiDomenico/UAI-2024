using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindFit_Intelligence_Backend.Models
{
    public class PerfilIA
    {
        [Key]
        [ForeignKey(nameof(PersonaSocio))]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdUsuario { get; set; } // ahora es PK y FK

        public PersonaSocio PersonaSocio { get; set; } = null!;

        [Column(TypeName = "varchar(500)")]
        public string? ObjetivoPrincipal { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string? NivelExperiencia { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string? EjerciciosPreferidos { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string? EjerciciosAEvitar{ get; set; }

        [Column(TypeName = "varchar(500)")]
        public string? DisponibilidadHoraria { get; set; } 

        [Column(TypeName = "varchar(500)")]
        public string? MotivacionPersonal { get; set; } 
    }
}
