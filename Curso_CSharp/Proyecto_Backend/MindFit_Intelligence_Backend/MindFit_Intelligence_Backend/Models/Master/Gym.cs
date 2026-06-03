using System.ComponentModel.DataAnnotations;

namespace MindFit_Intelligence_Backend.Models.Master
{
    public class Gym
    {
        [Key]
        public int IdGym { get; set; }

        [Required]
        [MaxLength(150)]
        public string NombreGym { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string ConnectionString { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}