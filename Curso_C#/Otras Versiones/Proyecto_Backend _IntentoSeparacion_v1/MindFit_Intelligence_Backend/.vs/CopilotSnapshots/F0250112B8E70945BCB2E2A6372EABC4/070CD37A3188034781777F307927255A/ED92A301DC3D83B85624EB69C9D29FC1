using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindFit_Intelligence_Backend.Models
{
    [PrimaryKey(nameof(IdUsuario), nameof(IdGrupo))]
    public class UsuarioGrupo
    {
        public int IdUsuario { get; set; }
        public int IdGrupo { get; set; }

        // Navegaciones
        [ForeignKey(nameof(IdUsuario))]
        public Usuario Usuario { get; set; } = null!;

        [ForeignKey(nameof(IdGrupo))]
        public Grupo Grupo { get; set; } = null!;
    }
}
