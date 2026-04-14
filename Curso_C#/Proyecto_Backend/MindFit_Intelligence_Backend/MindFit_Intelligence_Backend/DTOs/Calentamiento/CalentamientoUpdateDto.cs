namespace MindFit_Intelligence_Backend.DTOs.Calentamiento
{
    public class CalentamientoUpdateDto
    {
        public int IdEjercicio { get; set; }
        public int Duracion { get; set; }
        public int Orden { get; set; }
        public string? Observaciones { get; set; }
    }
}
