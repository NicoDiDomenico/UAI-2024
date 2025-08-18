using Backend.DTOs;
using System.Text.Json;

namespace Backend.Services
{
    public class PostsService : IPostsService
    {
        private HttpClient _httpClient; // Este objeto sirve para hacer peticiones HTTP (ir a una página web, API, etc.).

        public PostsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PostDto>> Get()
        {
            var result = await _httpClient.GetAsync(_httpClient.BaseAddress);
            // var -> HttpResponseMessage: respuesta HTTP (status code, headers, contenido).

            var body = await result.Content.ReadAsStringAsync();
            // var -> String
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            // var -> JsonSerializerOptions: configuración para el deserializador JSON.

            /*
            JsonSerializer: es una clase de .NET que sirve para convertir entre JSON (texto) <-> objetos C#.
            Deserialize<T>(): toma un texto JSON y lo convierte a un objeto del tipo T que le digas entre < >.
            */
            var post = JsonSerializer.Deserialize<IEnumerable<PostDto>>(body, options);
            // IEnumerable<PostDto>: colección (enumerable) de PostDto o null si falla.
            // PostDto: tu DTO con Id, UserId, Title, Body.

            return post;
        }
    }
}
