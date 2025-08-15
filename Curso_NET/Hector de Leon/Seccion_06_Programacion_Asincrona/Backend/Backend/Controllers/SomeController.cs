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

        [HttpGet("async")]
        public async Task<ActionResult<int>> GetAsync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Programacion Asincrona - Permite tener métodos ejecutandose en simultaneo (concurrencia), y controlar el flujo de ejecución con el await
            var task1 = new Task<int>(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Conexión a BD terminada");
                return 1;
            });

            var task2 = new Task<int>(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Envío de mail terminado");
                return 2;
            });

            task1.Start();
            task2.Start();

            var result1 = await task1; // Si saco el await, el código que le sigue no va a esperar que se termine de ejecutar task1.
            var result2 = await task2; 

            Console.WriteLine("Todo ha terminado");

            stopwatch.Stop();

            return Ok(result1 + " " + result2 + stopwatch.Elapsed); // Notar que con programación concurrente es mas rápido.
        }
    }
}
