using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    // En Postman usamos "https://localhost:7113/api/operation?a=10&b=22"
    /*En el body: 
    {
        "a": 10,
        "b": 5
    }
    */
    [Route("api/[controller]")] // La ruta usa [controller], que toma el nombre de la clase sin "Controller" → /api/operation
    [ApiController]
    public class OperationController : ControllerBase
    {
        [HttpGet] // Los datos acá viajan en la URL (Buenas Prácticas)
        public decimal Get(decimal a, decimal b)
        {
            return a + b;
        }

        [HttpPost] // Los datos acá viaja en el body
        public decimal Add(decimal a, decimal b, Numbers num, [FromHeader] string Host, [FromHeader(Name = "Host")] string numerito) {
            Console.WriteLine(Host);
            Console.WriteLine(numerito); // Ambos son iguales, pero se puede usar el atributo FromHeader para especificar el nombre del header que se quiere taer, y asi no depender del nombre que le coloquemos al parámetro.
            return a - b;
        }

        [HttpPut] // Los datos acá viaja en el body
        public string Edit(decimal a, decimal b, Letras l)
        {
            // ASP.NET Core hace "model binding" automáticamente:
            // - Tipos simples (decimal, string, etc.) se buscan en query string, ruta o formulario.
            // - Tipos complejos (como Letras) se buscan en el body (JSON) y se deserializan automáticamente.
            // Nota: Model binding en ASP.NET Core es el proceso automático por el cual el framework lee los datos que vienen en la petición HTTP (query string, ruta, formulario, body, headers, etc.) y los convierte en valores o en objetos C# (deserializar) para pasárselos como parámetros a tu método del controlador.
            decimal rta = a * b;
            return $"Lestras: {l.A} y {l.B}, Producto valores: {rta}";
        }

        [HttpDelete]
        public decimal Delete(decimal a, decimal b)
        {
            return a / b;
        }
    }

    // Esto no se hace pero es solo para aprendizaje:
    public class Numbers
    {
        public decimal A { get; set; }
        public decimal B { get; set; } 
    }

    public class Letras
    {
       public string A { get; set; }
       public string B { get; set; }
    }
}
