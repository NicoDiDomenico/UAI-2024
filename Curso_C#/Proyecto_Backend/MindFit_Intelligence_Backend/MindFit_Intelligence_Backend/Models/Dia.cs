using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MindFit_Intelligence_Backend.Models
{
    [Index(nameof(NombreDia), IsUnique = true)]
    public class Dia
    {
        [Key]
        public int IdDia { get; set; }

        [Column(TypeName = "varchar(25)")]
        public string NombreDia { get; set; } = string.Empty;

        // Navegación
        public required ICollection<Rutina> Rutinas { get; set; }
    }
}
