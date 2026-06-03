using MindFit_Intelligence_Backend.Models.Enums;

namespace MindFit_Intelligence_Backend.DTOs.Cuota
{
    public class CuotaInsertDto
    {
        public int? IdUsuario { get; set; } // Me parece que no es necesaria porque el socio se va a crear con la cuota, pero por las dudas la dejo por si tengo que insertar al cuota de forma independiente
        public Plan Plan { get; set; }
        public decimal Monto { get; set; }
    }
}
