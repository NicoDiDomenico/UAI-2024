using MindFit_Intelligence_Backend.DTOs.Grupos;
using MindFit_Intelligence_Backend.DTOs.Personas;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindFit_Intelligence_Backend.DTOs.Usuarios
{
    public class UsuarioResponsableDto
    {
        public int IdUsuario { get; set; }

        public PersonaResponsableDto? PersonaResponsableDto { get; set; } 

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        #region JWT
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Rol { get; set; } = null!;
        #endregion
        public List<GrupoDto> Grupos { get; set; } = new();
    }
}
