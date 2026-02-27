using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindFit_Intelligence_Backend.Models
{
    [PrimaryKey(nameof(IdGrupo), nameof(IdPermiso))]
    public class GrupoPermiso
    {
        public int IdGrupo { get; set; }
        public int IdPermiso { get; set; }

        // Navegaciones
        [ForeignKey(nameof(IdGrupo))]
        public Grupo Grupo { get; set; } = null!;

        [ForeignKey(nameof(IdPermiso))]
        public Permiso Permiso { get; set; } = null!;
    }
}
