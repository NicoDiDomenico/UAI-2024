using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindFit_Intelligence_Backend.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; } // PK

        public PersonaResponsable? PersonaResponsable { get; set; } // Navegación a PersonaResponsable

        public PersonaSocio? PersonaSocio { get; set; } // Navegación a PersonaSocio

        /*
        [Column(TypeName = "varchar(50)")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Column(TypeName = "varchar(255)")]
        public string ContrasenaHash { get; set; } = string.Empty;
        */

        [Column(TypeName = "date")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        #region JWT
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; // PasswordHash es la contraseña encriptada.
        public string Rol { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        #endregion
    }
}
