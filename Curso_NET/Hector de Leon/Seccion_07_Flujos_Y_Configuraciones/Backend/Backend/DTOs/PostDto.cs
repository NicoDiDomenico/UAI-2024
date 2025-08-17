namespace Backend.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Title { get; set; } // TipoDato? Propiedad --> "?" permite que mi propiedad sea nula
        public string? Body { get; set; }
    }
}
