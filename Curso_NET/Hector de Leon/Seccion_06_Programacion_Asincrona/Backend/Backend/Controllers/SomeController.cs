using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        [HttpGet("sync")]
        public IActionResult GetSync()
        {
            //// Programación síncrona
            //Stopwatch stopwatch = new Stopwatch(); // Crea un nuevo cronómetro
            //stopwatch.Start(); // Inicia el cronómetro
            Stopwatch stopwatch = Stopwatch.StartNew(); // Crea e inicia el cronómetro

            Thread.Sleep(1000); // Simula una operación de larga duración
            Console.WriteLine("Conexión a BD terminada");

            Thread.Sleep(1000);
            Console.WriteLine("Envío de mail terminado");

            Console.WriteLine("Todo ha terminado");

            stopwatch.Stop(); // Detiene el cronómetro

            return Ok(stopwatch.Elapsed);
        }
    }
}
