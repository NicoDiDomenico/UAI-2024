using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.DTOs;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        // Este es un controlador en el que se ejecuta un servicio de tercero
        IPostsService _titlesService;

        public PostsController(IPostsService titlesService)
        {
            _titlesService = titlesService;
        }

        [HttpGet]
        public async Task<IEnumerable<PostDto>> Get() =>
            await _titlesService.Get();
        // 👇 Notas importantes:
        // - Este método devuelve objetos C# (IEnumerable<PostDto>).
        // - ASP.NET Core se encarga automáticamente de serializarlos a JSON 
        //   antes de enviarlos como respuesta HTTP.
        // - O sea: yo manejo objetos PostDto en el backend, 
        //   y el framework los transforma en JSON para el cliente.
    }
}
