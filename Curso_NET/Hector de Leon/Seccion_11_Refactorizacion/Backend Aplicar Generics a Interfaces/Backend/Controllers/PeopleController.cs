using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private IPeopleService _peopleService;

        public PeopleController([FromKeyedServices("people2Service")] IPeopleService peopleService)
        {
        /*
        Acá estás diciendo:
            "Cuando construyas un PeopleController, necesito que me pases algo que cumpla IPeopleServices."
        ASP.NET Core ve esto y dice:
            "Ok, el Program.cs ya me dijo que para IPeopleServices use PeopleService".
            Lo crea (o reutiliza si es singleton).
            Lo pasa como parámetro al constructor automáticamente. 
        */
            _peopleService = peopleService; // Flexibilidad: puedes asignar cualquier objeto que implemente la interfaz. 
        }

        [HttpGet("all")]
        public List<People> GetPeople() => Repository.People;

        [HttpGet("{id}")]
        //public People Get(int id) => Repository.People.First(p => p.Id == id);
        public ActionResult<People> Get(int id)
        {
            // ActionResult<T> permite manejar respuestas HTTP más complejas, como NotFound, Ok, etc. Esto me puede servir para que en vez de un codigo 500 forzar a que se devuelva un 404, ya que no se enconntró ese Usuario con ese Id, ya que seria lo mas acorde.
            var people = Repository.People.FirstOrDefault(p => p.Id == id); // FirstOrDefault devuelve null si no encuentra nada (en vez de lanzar excepción).

            if (people == null)
            {
                return NotFound(); // 404 Not Found
            }

            return Ok(people); // 200 OK con el objeto People
        }
        
        [HttpGet("search/{search}")]
        public List<People> Get(string search) => Repository.People.Where(p => p.Name.ToUpper().Contains(search.ToUpper())).ToList();

        [HttpPost] // Este método se ejecuta cuando llega una solicitud HTTP POST a la ruta del controlador.
        public IActionResult Add(People people) // IActionResult permite devolver distintos tipos de respuestas HTTP.
        {
            // Validación: si el nombre de la persona es nulo o una cadena vacía, se devuelve un error 400 (Bad Request).
            if (!_peopleService.Validate(people))
            {
                return BadRequest(); // Respuesta HTTP 400: el cliente envió datos inválidos.
            }

            // Si pasa la validación, agregamos el objeto 'people' al repositorio (simula guardarlo en base de datos).
            Repository.People.Add(people);

            // Devolvemos HTTP 204 (No Content): la operación fue exitosa, pero no enviamos contenido en la respuesta.
            return NoContent();
        }
        /*
        IActionResult → cuando solo te interesa devolver códigos de estado HTTP (y opcionalmente algún mensaje corto, pero sin un tipo de dato fijo).
        ActionResult<T> → cuando querés devolver código de estado + un objeto de un tipo específico en caso de éxito.
        */
    }
    public class Repository
    {
        public static List<People> People = new List<People>()
        {
            new People() { Id = 1, Name = "Juan", BirthDate = new DateTime(1990, 1, 1) },
            new People() { Id = 2, Name = "Maria", BirthDate = new DateTime(1995, 5, 15) },
            new People() { Id = 3, Name = "Pedro", BirthDate = new DateTime(1985, 10, 30) }
        };
    }
    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
