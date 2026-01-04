using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindFitIntelligence_Backend.DTOs
{
    public class RefreshTokenRequestDto
    {
        public int UserId { get; set; } // El profe uso Guid que es un identificador único global, pero yo uso int como en la tabla Usuario de la BD
        public required string RefreshToken { get; set; }
    }
}
