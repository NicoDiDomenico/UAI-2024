using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindFit_Intelligence_Backend.Models
{
    [Index(nameof(Codigo), IsUnique = true)]
    public class Permiso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPermiso { get; set; }

        [Required]
        [Column(TypeName = "varchar(80)")]
        public string Codigo { get; set; } = string.Empty;

        [Column(TypeName = "varchar(200)")]
        public string? Descripcion { get; set; }

        // Navegación (N:M)
        public ICollection<GrupoPermiso> GrupoPermisos { get; set; } = new List<GrupoPermiso>();
    }
}
