using Backend.DTOs;

namespace Backend.Services
{
    public interface IPostsService
    {
        public Task<IEnumerable<PostDto>> Get();
        // public → el método será accesible desde cualquier parte
        // Task<...> → clase en .NET que indica que el método es asíncrono (puede tardar, devuelve una "tarea")
        // IEnumerable<PostDto> → cuando la tarea termine, va a devolver una colección de PostDto (una lista)
        // PostDto → es un objeto DTO que representa los datos de un Post (ej. Id, Título, Contenido)
        // Get() → nombre del método, no recibe parámetros
    }
}
