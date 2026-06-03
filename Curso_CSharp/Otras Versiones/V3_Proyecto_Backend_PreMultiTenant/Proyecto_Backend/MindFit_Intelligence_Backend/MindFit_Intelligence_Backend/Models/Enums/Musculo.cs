namespace MindFit_Intelligence_Backend.Models.Enums
{
    public enum Musculo
    {
        // Mapeados exactamente con los IDs y nombres de tu tabla GrupoMuscular
        Pecho = 1,
        Espalda = 2,
        Cuadriceps = 3,
        Biceps = 4,
        Tricep = 5,
        Gluteos = 6,
        Abdomen = 7,
        Hombro = 8,
        Gemelos = 9,
        
        // Músculos que tenías en tu código original pero aún no están en la BD.
        // Les asignamos los IDs siguientes para que no choquen si decides insertarlos luego.
        Antebrazos = 10,
        Lumbares = 11,
        Isquiotibiales = 12
    }
}
