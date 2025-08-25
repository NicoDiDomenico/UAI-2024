````csharp
//// ✅ 1. Modificadores de Acceso
public string NombrePersona; // Se puede usar desde cualquier parte del código. PascalCase
private int _edadPersona; // Solo accesible dentro de la clase donde fue declarado. _camelCase
protected string tipoPersona; // Accesible desde la clase actual y sus hijas. camelCase

//// ✅ 2. Herencia (con constructor)
// Clase base
public class Animal
{
    public string Nombre { get; set; }

    public Animal(string nombre)
    {
        Nombre = nombre;
    }

    public virtual void HacerSonido()
    {
        Console.WriteLine("Sonido genérico");
    }
}

// Clase derivada
public class Perro : Animal
{
    public string Raza { get; set; }

    // Constructor de la clase derivada llama al del padre con base()
    public Perro(string nombre, string raza) : base(nombre)
    {
        Raza = raza;
    }

    // Override de método heredado
    public override void HacerSonido()
    {
        Console.WriteLine("Guau guau");
    }
}

//// ✅ 3. Overload (Sobrecarga)
public class Calculadora
{
    public int Sumar(int a, int b)
    {
        return a + b;
    }

    public double Sumar(double a, double b)
    {
        return a + b;
    }

    public int Sumar(int a, int b, int c)
    {
        return a + b + c;
    }
}

//// ✅ 4. Interface
// Definición de la interfaz
public interface IVehiculo
{
    void Arrancar();
    void Frenar();
}

// Clase que implementa la interfaz
public class Auto : IVehiculo
{
    public void Arrancar()
    {
        Console.WriteLine("El auto arranca");
    }

    public void Frenar()
    {
        Console.WriteLine("El auto frena");
    }
}

//// ✅ 5. MODIFICADORES ADICIONALES EN C#
/*
1. 🔸 abstract
¿Qué es?: Declara una clase o método incompleto, o sea sin instanciacion, que debe ser implementado por una clase hija.
¿Dónde se usa?: En clases y métodos.
¿Para qué sirve?: Forzar a las clases hijas a implementar ciertos métodos.
*/
// Ejemplo:
public abstract class Animal
{
    public abstract void HacerSonido(); // No tiene cuerpo
}

public class Perro : Animal
{
    public override void HacerSonido()
    {
        Console.WriteLine("Guau");
    }
}
/*
2. 🔸 sealed
¿Qué es?: Indica que una clase no puede ser heredada.
¿Dónde se usa?: Solo en clases.
¿Para qué sirve?: Para evitar que alguien derive una clase por seguridad o diseño.
*/
// Ejemplo:
public sealed class BancoCentral
{
    // No se puede hacer: class OtroBanco : BancoCentral
}
/*
3. 🔸 static
¿Qué es?: Declara clases, métodos o propiedades que pertenecen a la clase y no a una instancia.
¿Dónde se usa?: En clases, métodos, propiedades, campos.
¿Para qué sirve?: Para usar métodos o miembros sin crear un objeto.
*/
// Ejemplo:
public static class Calculadora
{
    public static int Sumar(int a, int b)
    {
        return a + b;
    }
}
// Uso: Calculadora.Sumar(3, 4);
/*
4. 🔸 virtual
¿Qué es?: Permite que un método pueda ser sobrescrito (override) en una clase hija.
¿Dónde se usa?: En métodos, propiedades.
¿Para qué sirve?: Para marcar algo que puede cambiar en la herencia.
*/
// Ejemplo:
public class Animal
{
    public virtual void HacerSonido()
    {
        Console.WriteLine("Sonido genérico");
    }
}
/*
5. 🔸 override
¿Qué es?: Indica que se está reescribiendo un método heredado marcado como virtual o abstract.
¿Dónde se usa?: En métodos y propiedades de clases hijas.
¿Para qué sirve?: Para cambiar el comportamiento de un método heredado.
*/
// Ejemplo:
public class Perro : Animal
{
    public override void HacerSonido()
    {
        Console.WriteLine("Guau");
    }
}
/*
6. 🔸 async
¿Qué es?: Indica que el método se puede ejecutar de forma asíncrona, y normalmente se usa junto con await.
¿Dónde se usa?: En métodos.
¿Para qué sirve?: Para ejecutar tareas en segundo plano sin bloquear el hilo principal (muy útil en aplicaciones gráficas o web).
*/
// Ejemplo:
public async Task DescargarDatosAsync()
{
    string contenido = await httpClient.GetStringAsync("https://...");
    Console.WriteLine(contenido);
}

//// ✅ 6. ORDEN GENERAL DE MODIFICADORES EN C#
/// 📦 1. Para declarar una CLASE
[modificadores de acceso] [modificadores adicionales] class NombreClase [: claseBase] [, interfaces]

// Ejemplo:
public abstract class Animal : SerVivo, IComestible
{
    // cuerpo de la clase
}

/// ⚙️ 2. Para declarar un MÉTODO
[modificadores de acceso] [modificadores adicionales] tipoRetorno NombreMétodo([parámetros])

// Ejemplo:
protected virtual int CalcularEdad(DateTime nacimiento)
{
    // cuerpo
}

/// 🧩 3. Para declarar una INTERFAZ
[modificador de acceso] interface NombreInterfaz [: interfacesBase]

//  Ejemplo:
public interface IComestible
{
    void Comer();
}

/// 📐 4. Para declarar una PROPIEDAD
[modificador de acceso] [modificadores adicionales] tipo Nombre { get; set; }

// Ejemplo:
public string Nombre { get; set; }

//// ✅ 7. Serialización y Deserialización en C#
JsonSerializer.Serialize(objeto)  // --> retorna el json
JsonSerializer.Deserialize<Tipo>(json) // --> retorna el objeto

//// ✅ 11. Generics
/*
¿Qué es?: Permiten definir clases, interfaces o métodos que reciben un tipo de dato como parámetro.
¿Para qué sirve?: Para reutilizar código y trabajar con distintos tipos de datos sin duplicar lógica.
Ventajas:
- Más eficiencia: el compilador genera código específico para el tipo usado (sin boxing/unboxing).
- Más seguridad: se detectan errores de tipo en tiempo de compilación.
- Más limpieza: no se necesitan conversiones explícitas.

📌 Sintaxis: class NombreClase<T> { ... }
T → Tipo de dato genérico (por convención T, pero puede ser cualquier nombre).
*/

// Ejemplo básico:
public class Caja<T>
{
    public T Contenido { get; set; }
}
Caja<string> miCaja = new Caja<string>();
miCaja.Contenido = "Juguete";

// Ejemplo con lista genérica y límite:
public class MyList<T>
{
    private List<T> _list;
    private int _limit;

    public MyList(int limite)
    {
        _limit = limite;
        _list = new List<T>();
    }

    public void Add(T item)
    {
        if (_list.Count < _limit) _list.Add(item);
    }

    public string GetContent()
    {
        string content = "";
        foreach (var item in _list)
            content += item + " ";
        return content;
    }
}

// Ejemplo de uso con distintos tipos:
var numbers = new MyList<int>(5);
var names = new MyList<string>(5);
var beers = new MyList<Beer>(5);

//// 📚 Ejemplos de genéricos en .NET:
List<T>, Dictionary<TKey, TValue>, Queue<T>, Stack<T>, Nullable<T>, Task<T>

//// ✅ 12. Programación Funcional en C# (versión simplificada y con ejemplos claros)

/*
📌 PRINCIPIO CENTRAL: Funciones Puras
- Deterministas: si la entrada es la misma, la salida siempre será igual.
- Sin efectos colaterales: no cambian variables globales ni modifican objetos externos.
*/

// ❌ Ejemplo de función impura (modifica el objeto que recibe)
void CambiarNombre(Beer b)
{
    b.Name = "Heineken"; // Afecta al objeto externo
}

// ✔️ Ejemplo de función pura (no toca el original)
Beer CambiarNombrePuro(Beer b)
{
    Beer copia = new Beer();
    copia.Name = "Heineken";
    return copia;
}

/*
------------------------------------------------------------
📌 INMUTABILIDAD
- Un dato inmutable no puede cambiar después de crearse.
- Evita cambios accidentales y ayuda a mantener la pureza.
- En C#, tipos como string o DateTime son inmutables.
*/

// Ejemplo con struct inmutable (DateTime) --> Struct se pasa por valor, al pasarse copias son inmutables.
DateTime fecha = DateTime.Now;
DateTime nuevaFecha = fecha.AddDays(5); // Crea un nuevo objeto
Console.WriteLine(fecha);       // Original intacto
Console.WriteLine(nuevaFecha);  // Fecha modificada (nueva instancia)

// Ejemplo con class mutable (se puede alterar desde fuera) --> Los Objetos se pasan por referencia, por eso son mutables.
public class Persona
{
    public string Nombre { get; set; }
}

void CambiarNombrePersona(Persona p)
{
    p.Nombre = "Juan"; // Cambia el original
}

Persona persona = new Persona { Nombre = "Nico" };
CambiarNombrePersona(persona);
Console.WriteLine(persona.Nombre); // "Juan"

/*
💡 Conexión:
Programación funcional = funciones puras + datos inmutables.
Un struct inmutable (como DateTime) es ideal para mantener la pureza.
------------------------------------------------------------
*/

/*
📌 FUNCIONES DE PRIMERA CLASE
- Se pueden guardar en variables, pasar como parámetros y devolver.
*/

int Cuadrado(int numero)
{
    return numero * numero;
}

// Guardar en una variable (delegado Func)
Func<int, int> funcionCuadrado = Cuadrado;
Console.WriteLine(funcionCuadrado(4)); // 16

// Pasar como parámetro
void Ejecutar(Func<int, int> funcion, int valor)
{
    Console.WriteLine(funcion(valor));
}
Ejecutar(Cuadrado, 5); // 25

/*
------------------------------------------------------------
📌 FUNCIONES DE ORDEN SUPERIOR
- Reciben otra función como parámetro o devuelven una función.
*/

int Sumar(int a, int b) { return a + b; }
int Multiplicar(int a, int b) { return a * b; }

// Función que recibe otra función
int Calcular(Func<int, int, int> operacion, int a, int b)
{

    return operacion(a, b);
}
Console.WriteLine(Calcular(Sumar, 3, 4));         // 7
Console.WriteLine(Calcular(Multiplicar, 3, 4));   // 12

// Función que devuelve otra función
Func<int, int> Multiplicador(int factor)
{
    int FuncionInterna(int x)
    {
        return x * factor;
    }
    return FuncionInterna;
}

Func<int, int> por10 = Multiplicador(10);
Console.WriteLine(por10(5)); // 50

/*
------------------------------------------------------------
📌 DELEGADOS EN C#
- Un delegado es un tipo que guarda la referencia a un método con firma específica.
- Ya no se usan delegados (C# v1.0), en cambio se usan Action<> y Func<> (C# v3.0) que ya son delegados genéricos listos para usar.
*/

delegate void MostrarMensaje(string mensaje);

void Saludar(string nombre)
{
    Console.WriteLine("Hola " + nombre);
}

// Uso de delegado explícito
MostrarMensaje delegado = Saludar;
delegado("Nico"); // Hola Nico

Action<string> delegado = Saludar;
delegado("Nico")

// Action<T> → para métodos que no devuelven nada
void MostrarEnPantalla(string texto)
{
    Console.WriteLine(texto);
}
Action<string> mostrar = MostrarEnPantalla;
mostrar("Hola Juan");

// Func<T> → para métodos que devuelven algo
int SumarEnteros(int x, int y)
{
    return x + y;
}
Func<int, int, int> sumar = SumarEnteros;
Console.WriteLine(sumar(3, 4)); // 7

//// ✅ 13. Expresiones Lambda en C#

/*
📌 ¿Qué son?
Son funciones anónimas (sin nombre) que pueden definirse directamente en el lugar donde se usan,
sin necesidad de declararlas previamente. Muy útiles para funciones que se ejecutan una sola vez
o que se pasan como parámetro a funciones de orden superior.
*/

/*
📐 Sintaxis básica:
(parámetros) => expresión
*/

//// Ejemplos:
// Con dos parámetros (tipados explícitamente)
(int a, int b) => a - b;

// Con inferencia de tipos
(a, b) => a + b;

// Un solo parámetro, sin paréntesis
a => a * 2;

// Varias líneas de código (usar llaves y return)
a => {
    a += 1;
    return a * 5;
};

/*
🎯 Uso con funciones de orden superior
Las lambdas pueden pasarse como parámetro a funciones que reciben otras funciones.
*/

// Función que recibe otra función y un número
int Sum(Func<int, int, int> fn, int numero)
{
    return fn(numero, numero);
}

// Llamada con lambda inline
var resultado = Sum((a, b) => a + b, 5); // resultado = 10

/*
✨ Beneficios:
- Evitan crear funciones adicionales para lógica simple.
- Más legibilidad en funciones pequeñas.
- Combinan muy bien con LINQ y programación funcional en C#.
*/

//// ✅ 14. LINQ en C#
/*
📌 ¿Qué es?
(Language Integrated Query) Extensión de C# para consultar y manipular colecciones
(lista, array, datos de BD, XML, JSON, etc.) usando una sintaxis declarativa similar a SQL.

💡 Permite: filtrar, ordenar, agrupar y proyectar datos sin escribir bucles manuales.
*/

/*
📐 Partes de LINQ:
1. Origen de datos → lista, array, base de datos, XML, JSON.
2. Consulta → define filtro, orden y selección (sintaxis de consulta o de funciones).
3. Ejecución:
   - Diferida → se ejecuta al recorrer (foreach).
   - Inmediata → usando .ToList(), .ToArray().
*/

//// 📝 Ejemplo con sintaxis de consulta:
var names = new List<string>() { "Juan", "Pepe", "Ana", "Hugo", "Nico" };

var namesResult = from n in names
                  where n.Length > 3 && n.Length < 5
                  orderby n
                  select n;

foreach (var name in namesResult)
{
    Console.WriteLine(name);
}

//// 📝 Ejemplo con sintaxis de funciones:
var namesResult2 = names
    .Where(n => n.Length > 3 && n.Length < 5)    // Filtra
    .OrderByDescending(n => n)                   // Ordena descendente
    .Select(n => n);                             // Selecciona

foreach (var name in namesResult2)
{
    Console.WriteLine(name);
}

/*
🎯 Métodos comunes:
- Where(...) → Filtrar
- OrderBy(...) / OrderByDescending(...) → Ordenar
- Select(...) → Proyectar
- GroupBy(...) → Agrupar
- First(), FirstOrDefault(), Any(), Count(), Sum(), Max(), Min(), Average()

✨ Tips:
- LINQ no modifica la colección original.
- El compilador traduce la sintaxis de consulta a funciones antes de ejecutarla.
*/

//// ✅ ASP.NET Core Web API – Conceptos clave nuevos

// 🔹 Controladores
// Clase que hereda de ControllerBase.
// Define endpoints (URLs) que responden a solicitudes HTTP.
// Convención: https://localhost:puerto/api/[NombreControladorSinController]

// 🔹 Métodos HTTP (atributos)
[HttpGet]     // Obtener datos
[HttpPost]    // Crear datos
[HttpPut]     // Actualizar datos
[HttpDelete]  // Eliminar datos

// 🔹 Parámetros
// Por URL → GET (?a=10&b=22)
// Por Body (JSON) → POST/PUT (deserializa a objeto C# automáticamente)
public IActionResult Add([FromBody] Numbers n) { ... }

// 🔹 Headers
[FromHeader] string host
[FromHeader(Name = "Content-Length")] int len
// Headers personalizados: [FromHeader(Name = "X-Sum")] string sum

// 🔹 Respuestas enriquecidas
ActionResult<T> → Representa una respuesta HTTP que puede incluir datos de tipo T y un código de estado. Garantiza que, si hay datos, sean siempre del tipo especificado. Ejemplos: Ok(obj), NotFound().

IActionResult → Representa una respuesta HTTP genérica que puede incluir solo código o código + datos de cualquier tipo. Ejemplos: NoContent(), BadRequest(), Ok(obj).

// 🔹 Buenas prácticas
// - Filtrar datos (First, Where, Contains) → evitar devolver todos sin necesidad.
// - Usar códigos adecuados: 200 OK, 204 No Content, 400 Bad Request, 404 Not Found.
// - Swagger → documentación y prueba rápida
// - Postman → pruebas avanzadas y organización de requests

// 🔹 Capa de Servicio
// Separa la lógica de negocio del controlador.
// Definida mediante interfaces (convención: prefijo 'I') y clases de implementación.
// Ventaja: un solo punto de cambio para reglas usadas en varios controladores.

// 🔹 Inyección de Dependencias (DI)
// El controlador recibe servicios ya instanciados por el framework (no usa 'new').
// Registro en Program.cs: builder.Services.AddSingleton<IPeopleService, PeopleService>();
// Principio SOLID: depender de abstracciones (interfaces), no implementaciones.
// Cambiar implementación requiere solo modificar el registro en Program.cs.

// 🔹 DI por Clave (Key) – .NET 8
// Permite registrar varias implementaciones de una interfaz y seleccionarlas por un key.
// builder.Services.AddKeyedSingleton<IPeopleService, PeopleService>("PeopleService");
// Uso en constructor: [FromKeyedServices("PeopleService")] IPeopleService service

// 🔹 Ciclos de Vida en DI
// Singleton → mismo objeto para toda la app.
// Scoped → un objeto por solicitud HTTP.
// Transient → un objeto nuevo en cada inyección, incluso en la misma solicitud.

//// ✅ 15. Programación Asíncrona en C#

/*
📌 ¿Qué es?
Permite ejecutar tareas en segundo plano sin bloquear el hilo principal, aprovechando el tiempo muerto de operaciones lentas (conexión a BD, lectura/escritura de archivos, llamadas HTTP).

📌 Claves:
- async → marca un método como asíncrono.
- await → espera que termine una tarea antes de seguir.
- Task / Task<T> → representa una operación asíncrona (con o sin retorno).
*/

/*
⚖️ Síncrono vs Asíncrono
Síncrono: ejecuta tareas una tras otra, esperando a que cada una termine.
Asíncrono: inicia varias tareas a la vez y espera sus resultados al final, reduciendo el tiempo total.
*/

// Ejemplo: dos tareas que duran 1 seg cada una
// Síncrono → total ≈ 2 seg
// Asíncrono → total ≈ 1 seg
public async Task<IActionResult> EjemploAsync()
{
    var t1 = Task.Run(() => {
        Thread.Sleep(1000);
        Console.WriteLine("Conexión a BD lista");
    });

    var t2 = Task.Run(() => {
        Thread.Sleep(1000);
        Console.WriteLine("Correo enviado");
    });

    await Task.WhenAll(t1, t2); // Espera que ambas terminen
    return Ok("Todo ha terminado");
}

//// ✅ 16. Modelos vs DTOs
// Modelo → Representa directamente la tabla de BD (ej: Usuario con Id, Correo, Password).
// DTO (Data Transfer Object) → Clase ligera que transfiere solo lo necesario entre capas.
// Ventaja → Menos datos, más seguridad (no exponer Password), mejor rendimiento.
public class Usuario
{
    public int Id { get; set; }
    public string Correo { get; set; }
    public string Password { get; set; }
}
public class UsuarioDto
{
    public int Id { get; set; }
    public string Correo { get; set; }
}

//// ✅ 17. HttpClient
// Clase de .NET para consumir APIs externas (GET, POST, PUT, DELETE).
// Siempre usar async/await (operaciones lentas como llamadas HTTP).
public class PostService
{
    private readonly HttpClient _http;
    public PostService(HttpClient http) => _http = http;

    public async Task<IEnumerable<PostDto>> Get()
    {
        var response = await _http.GetAsync("/posts");
        var body = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<PostDto>>(body,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}

//// ✅ 18. IHttpClientFactory
// Patrón Factory aplicado a HttpClient. Permite centralizar configuración y reuso.
builder.Services.AddHttpClient<IPostService, PostService>(c =>
{
    c.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
});
// Ventajas: no crear HttpClient a mano, mejor mantenimiento, evita fugas de sockets.

//// ✅ 19. appsettings.json
// Archivo de configuración → se usa para no hardcodear valores en el código.
{
  "UrlPost": "https://jsonplaceholder.typicode.com/posts"
}
// Acceso en Program.cs:
var url = builder.Configuration["UrlPost"];

//// ✅ Entity Framework Core (EF Core) – Resumen rápido

🔹 ¿Qué es?
- ORM por defecto en .NET → mapea tablas ↔ clases (objetos).
- Permite trabajar con BD sin SQL directo (usa LINQ).

🔹 Enfoques:
- Code First → clases → migraciones → BD.
- Database First → BD → genera clases.
- Manual → BD y clases a mano (asegurando equivalencia).

🔹 Modelos:
- Clase ↔ tabla. Propiedades ↔ columnas.
- Atributos: [Key], [DatabaseGenerated], [ForeignKey].
- Relaciones: 1:N (Brand ↔ Beers).

🔹 Contexto:
- Clase que hereda de DbContext.
- Define DbSet<T> para cada tabla.
- Configuración e inyección en Program.cs con AddDbContext().

🔹 Migraciones:
- Versionan la BD (historial de cambios).
- Métodos: Up (aplica cambios), Down (revierte).
- Flujo: Add-Migration → Update-Database → Refresh en SSMS.
- En desarrollo: se puede revertir. En producción: generar scripts SQL.

🔹 Modificaciones:
1. Cambiar modelo (ej. agregar columna).
2. Add-Migration Nombre.
3. Update-Database.
4. Verificar en SQL Server.

🔹 Comandos útiles:
- `Add-Migration Nombre` → crear migración.
- `Update-Database` → aplicar migraciones.
- `Update-Database Nombre` → volver a un estado previo.
- `Remove-Migration` → eliminar la última (si no se aplicó).
- `Script-Migration` → generar script SQL.
- `Get-Migration` → listar migraciones.

📌 Conclusión: EF Core permite que la base de datos evolucione junto con el código, manteniendo control de versiones y evitando escribir SQL manual.

//// ✅ 20. CRUD
// MODELO (Entidad de BD)
public class Beer   // ↔ tabla Beers
{
    public int BeerID { get; set; }      // PK
    public string Name { get; set; } = "";
    public decimal Alcohol { get; set; }
    public int BrandID { get; set; }     // FK (opcional)
    public Brand? Brand { get; set; }    // nav (opcional)
}

// ✅ DTOS (Entrada/Salida)
public class BeerDto                 // lo que devuelvo al cliente (Read)
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Alcohol { get; set; }
    public int BrandID { get; set; }
}
public class BeerInsertDto           // lo que recibo en POST (Create)
{
    public string Name { get; set; } = "";
    public decimal Alcohol { get; set; }
    public int BrandID { get; set; }
}
public class BeerUpdateDto           // lo que recibo en PUT (Update)
{
    public string Name { get; set; } = "";
    public decimal Alcohol { get; set; }
    public int BrandID { get; set; }
}

// ✅ DB CONTEXT (EF Core)
public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }
    public DbSet<Beer> Beers => Set<Beer>();
    public DbSet<Brand> Brands => Set<Brand>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Beer>()
          .HasKey(b => b.BeerID);

        mb.Entity<Beer>()
          .HasOne(b => b.Brand)
          .WithMany()
          .HasForeignKey(b => b.BrandID);
    }
}

// ✅ PROGRAM.cs (registro básico)
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"))); // o UseSqlite, etc.
var app = builder.Build();
app.MapControllers();
app.Run();

// ✅ CONTROLADOR (CRUD completo con buenas prácticas)
[ApiController]
[Route("api/[controller]")]
public class BeersController : ControllerBase
{
    private readonly StoreContext _context;
    public BeersController(StoreContext context) => _context = context;

    // READ: lista
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BeerDto>>> GetAll()
    {
        // AsNoTracking para lecturas (mejor rendimiento)
        var data = await _context.Beers
            .AsNoTracking()
            .Select(b => new BeerDto
            {
                Id = b.BeerID,
                Name = b.Name,
                Alcohol = b.Alcohol,
                BrandID = b.BrandID
            })
            .ToListAsync();

        return Ok(data);
    }

    // READ: por id
    [HttpGet("{id:int}")]
    public async Task<ActionResult<BeerDto>> GetById(int id)
    {
        var b = await _context.Beers.AsNoTracking().FirstOrDefaultAsync(x => x.BeerID == id);
        if (b is null) return NotFound();

        return Ok(new BeerDto
        {
            Id = b.BeerID,
            Name = b.Name,
            Alcohol = b.Alcohol,
            BrandID = b.BrandID
        });
    }

    // CREATE
    [HttpPost]
    public async Task<ActionResult<BeerDto>> Create(BeerInsertDto dto)
    {
        var entity = new Beer
        {
            Name = dto.Name,
            Alcohol = dto.Alcohol,
            BrandID = dto.BrandID
        };

        _context.Beers.Add(entity);
        await _context.SaveChangesAsync(); // acá se genera BeerID

        var result = new BeerDto
        {
            Id = entity.BeerID,
            Name = entity.Name,
            Alcohol = entity.Alcohol,
            BrandID = entity.BrandID
        };

        // 201 + Location: api/Beers/{id}
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    // UPDATE (reemplazo total del recurso)
    [HttpPut("{id:int}")]
    public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto dto)
    {
        var entity = await _context.Beers.FindAsync(id);
        if (entity is null) return NotFound();

        entity.Name = dto.Name;
        entity.Alcohol = dto.Alcohol;
        entity.BrandID = dto.BrandID;

        await _context.SaveChangesAsync();

        return Ok(new BeerDto
        {
            Id = entity.BeerID,
            Name = entity.Name,
            Alcohol = entity.Alcohol,
            BrandID = entity.BrandID
        });
    }

    // DELETE
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Beers.FindAsync(id);
        if (entity is null) return NotFound();

        _context.Beers.Remove(entity);
        await _context.SaveChangesAsync();

        // recomendado en DELETE
        return NoContent(); // 204
    }
}

/* 🧠 Notas rápidas para reutilizar:
- [ApiController] activa validación automática de ModelState (400) y binding.
- Usá DTOs distintos para entrada (Insert/Update) y salida (Read).
- AsNoTracking() en lecturas. Tracking para Update/Delete.
- CreatedAtAction en POST para 201 + Location.
- NoContent() (204) en DELETE; Ok() (200) en GET/PUT.
- Si necesitás filtros/paginación: agregá query params (ej. ?q=...&page=1&pageSize=20) y aplicá .Where/.Skip/.Take.
*/

//// ✅ 21. Parámetros en métodos HTTP (ASP.NET Core)

Cuando escribís un método en un controlador, ASP.NET Core se encarga de **rellenar automáticamente** los parámetros con los valores que llegan en la request.
Este proceso se llama **Model Binding**.

---

### 1. `[FromRoute]` → Valor en la **ruta** (segmento de la URL)

```csharp
[HttpGet("search/{search}")]
public IActionResult Get([FromRoute] string search)
```

👉 URL: `/api/people/search/nico`
✅ `search = "nico"`

**¿Cuándo usarlo?**

* Cuando el dato **identifica un recurso** de forma única.
* Ej: `/users/15` → quiero **ese usuario** con ID=15.
* Se usa en **GET** principalmente.
* Es más **legible** y se puede cachear/facilitar en SEO.

---

### 2. `[FromQuery]` → **Query string** (`?key=value`)

```csharp
[HttpGet("search")]
public IActionResult Get([FromQuery] string search)
```

👉 URL: `/api/people/search?search=nico`
✅ `search = "nico"`

**¿Cuándo usarlo?**

* Cuando el dato es un **filtro, búsqueda o parámetro opcional**.
* Ej: `/products?category=ropa&page=2`.
* Ideal para **GETs con filtros o paginación**.
* Permite combinar varios parámetros sin alterar la ruta.

---

### 3. `[FromBody]` → **Cuerpo** de la request (JSON en POST/PUT/PATCH)

```csharp
[HttpPost("search")]
public IActionResult Post([FromBody] string search)
```

👉 Body JSON: `{ "search": "nico" }`
✅ `search = "nico"`

**¿Cuándo usarlo?**

* Cuando mandás **información compleja** (objetos, listas, formularios grandes).
* Ej: crear usuario → `{ "name": "Nico", "email": "nico@test.com" }`.
* Usado en **POST/PUT/PATCH**, nunca en GET.
* El body es más **seguro y flexible** que la URL para datos sensibles o largos.

---

### 4. `[FromHeader]` → **Headers** personalizados

```csharp
public IActionResult Get([FromHeader(Name = "x-custom-header")] string value)
```

👉 Header: `x-custom-header: hola`
✅ `value = "hola"`

**¿Cuándo usarlo?**

* Para **metadatos de la request**, no para los datos principales.
* Ej: `Authorization: Bearer <token>` (autenticación).
* También útil para **tracking, versiones de API, claves API**.
* No se usa para información de negocio (ej: nombre del usuario), sino para datos de control.

---

### 5. `[FromForm]` → **Datos de formularios HTML** (`form-data` o `multipart/form-data`)

```csharp
[HttpPost("upload")]
public IActionResult Upload([FromForm] string name)
```

👉 FormData: `name=nico`
✅ `name = "nico"`

**¿Cuándo usarlo?**

* Cuando el front manda datos desde un **formulario HTML tradicional**.
* Especialmente útil en **subida de archivos** (porque se codifican como `multipart/form-data`).
* Ej: un form con inputs de texto + un archivo adjunto.
* Si solo son textos, suele ser mejor `[FromBody]` con JSON.

---

## 🚀 Resumen rápido (con lógica de uso)

| Fuente         | Ejemplo URL / Data                             | ¿Cuándo usarlo?                                    |
| -------------- | ---------------------------------------------- | -------------------------------------------------- |
| `[FromRoute]`  | `/users/15`                                    | Identificador único de un recurso                  |
| `[FromQuery]`  | `/products?category=ropa&page=2`               | Filtros, búsqueda, parámetros opcionales           |
| `[FromBody]`   | `{ "name": "Nico", "email": "test@test.com" }` | Objetos complejos, datos sensibles, POST/PUT       |
| `[FromHeader]` | `Authorization: Bearer token123`               | Metadatos: auth, tracking, API keys                |
| `[FromForm]`   | `name=nico + archivo.jpg`                      | Formularios HTML, especialmente subida de archivos |

📌 **Tip:**

* **Route y Query** → más para *identificar o filtrar*.
* **Body** → para *enviar datos completos*.
* **Header** → para *autenticación y control*.
* **Form** → para *formularios tradicionales / uploads*.

---
//// ✅ 22. Validaciones con FluentValidation en ASP.NET Core

// 🔹 Instalación
// Se agrega la librería FluentValidation vía NuGet.
// Permite separar reglas de validación en clases distintas a los DTOs.

// 🔹 Crear validador (ejemplo Insert)
public class BeerInsertValidator : AbstractValidator<BeerInsertDto>
{
    public BeerInsertValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .Length(2, 20).WithMessage("El nombre debe medir de 2 a 20 caracteres");

        RuleFor(x => x.BrandID)
            .NotNull().WithMessage("La marca es obligatoria")
            .GreaterThan(0).WithMessage("Valor inválido de marca");

        RuleFor(x => x.Alcohol)
            .GreaterThan(0).WithMessage("{PropertyName} debe ser mayor a cero");
    }
}

// 🔹 Inyección en Program.cs
builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidator>();

// 🔹 Uso en el controlador
var result = await _validator.ValidateAsync(dto);
if (!result.IsValid)
    return BadRequest(result.Errors);

// 🔹 Personalización de mensajes
// Se utiliza .WithMessage("texto personalizado") en cada regla.

// 🔹 Validaciones al Editar
// Se crea un nuevo validador (ej: BeerUpdateValidator) que agrega regla para validar el ID.
// Separación de responsabilidades:
// - DTO → transportar datos
// - Validator → validar reglas de negocio
// - Modelo → reflejar la BD

---

//// ✅ 23. Refactorización en ASP.NET Core

/*
📌 ¿Qué es refactorizar?
- Reestructurar el código sin cambiar su comportamiento externo.
- Objetivo: mayor legibilidad, mantenibilidad y escalabilidad.
- Evita duplicidad de código y mezcla de responsabilidades.
*/

/// 🔹 Capas y responsabilidades
- **Controller** → Maneja solicitudes y respuestas HTTP (ActionResult, códigos 200/404/201, etc.).
- **Service** → Contiene reglas de negocio y lógica con la BD (vía EF Core).
- **Entity Framework (Context)** → Acceso real a la base de datos.

/// 🔹 Interface de Servicio
- Define contrato con métodos asíncronos (Task).
- Métodos típicos: GetAll, GetById, Add, Update, Delete.
- NO devuelve ActionResult → eso es responsabilidad del Controller.
- Se implementa en la clase de servicio y se inyecta en Program.cs.

/// 🔹 Inyección de Dependencias
```csharp
builder.Services.AddScoped<IBeerService, BeerService>();
````

- El Controller recibe la interfaz en el constructor:

```csharp
private readonly IBeerService _service;
public BeersController(IBeerService service) => _service = service;
```

/// 🔹 Refactorización CRUD

- **Read**: Controller → invoca \_service.GetAll / GetById.
- **Create**: Controller valida → Service crea entidad, guarda y retorna DTO con ID.
- **Update**: Service actualiza entidad; Controller maneja null (NotFound) u Ok.
- **Delete**: Service elimina y retorna DTO borrado; Controller devuelve Ok(dto) o NotFound.

/// 🔹 Generics en Interfaces

- Permiten crear un contrato reutilizable para varios servicios.

````csharp
public interface IService<TDto, TInsert, TUpdate>
{
    Task<IEnumerable<TDto>> Get();
    Task<TDto?> GetById(int id);
    Task<TDto> Add(TInsert dto);
    Task<TDto?> Update(int id, TUpdate dto);
    Task<TDto?> Delete(int id);
}

* Cada servicio implementa con sus propios DTOs (ej: BeerDto, BeerInsertDto, BeerUpdateDto).
* Ventaja: menos interfaces duplicadas y mayor reutilización.

//// ✅ 24. Repository Pattern (ASP.NET Core)

📌 **¿Qué es la capa Repositorio?**
- Es una capa intermedia entre **Servicio** y **Entity Framework (Contexto/BD)**.
- Se encarga de la **persistencia de datos** (acceso a BD, procedimientos almacenados, consultas SQL, etc.).
- Permite que la capa de **Servicio** se enfoque en la **lógica de negocio**, sin preocuparse de cómo se accede a la BD.

---

🔹 **Ventajas**
- Separa responsabilidades → código más limpio y mantenible.
- Oculta la implementación del acceso a datos → el servicio solo conoce la interfaz, no el detalle.
- Facilita pruebas unitarias y cambios de motor de BD.

---

🔹 **Interfaz genérica (IRepository<T>)**
Métodos típicos definidos con *Generics*:
```csharp
public interface IRepository<TEntity>
{
    Task<IEnumerable<TEntity>> Get();
    Task<TEntity?> GetById(int id);
    Task Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task Save();
}
````

---

🔹 **Implementación (ejemplo con BeerRepository)**

```csharp
public class BeerRepository : IRepository<Beer>
{
    private readonly StoreContext _context;
    public BeerRepository(StoreContext context) => _context = context;

    public async Task<IEnumerable<Beer>> Get() => await _context.Beers.ToListAsync();
    public async Task<Beer?> GetById(int id) => await _context.Beers.FindAsync(id);
    public async Task Add(Beer entity) => await _context.Beers.AddAsync(entity);
    public void Update(Beer entity)
    {
        _context.Beers.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
    public void Delete(Beer entity) => _context.Beers.Remove(entity);
    public async Task Save() => await _context.SaveChangesAsync();
}
```

---

🔹 **Uso en la capa Servicio**

- El **Servicio** consume la interfaz `IRepository<T>` en lugar del contexto.
- Convierte las **Entidades** obtenidas del Repositorio en **DTOs** para devolver al Controller.
- Así, la lógica de negocio (ej: promociones, cálculos, validaciones) queda en la capa Servicio.

---

📌 **Resumen rápido**

- **Repository** = acceso a BD.
- **Service** = reglas de negocio.
- **Controller** = maneja requests/responses HTTP.

````
//// ✅ 25. AutoMapper en ASP.NET Core


📌 **¿Qué es AutoMapper?**
- Herramienta que evita asignar manualmente campo por campo entre objetos (ej. DTO ↔ Entidad).
- Transforma un objeto origen en un destino en una sola línea de código.
- Reduce código repetitivo y centraliza reglas de mapeo.


---


🔹 **Instalación y configuración**
1. Instalar paquete NuGet: `AutoMapper.Extensions.Microsoft.DependencyInjection`.
2. Crear clase `MappingProfile` que herede de `Profile`.
3. Registrar en `Program.cs`:
```csharp
builder.Services.AddAutoMapper(typeof(MappingProfile));
````

---

🔹 **Casos principales**

1. **Propiedades con el mismo nombre**

```csharp
CreateMap<BeerInsertDto, Beer>();
```

👉 AutoMapper asigna automáticamente propiedades coincidentes.

2. **Propiedades con distinto nombre**

```csharp
CreateMap<Beer, BeerDto>()
.ForMember(dto => dto.Id,
m => m.MapFrom(b => b.BeerID));
```

👉 Permite mapear campos con nombres diferentes.

3. **Mapeo sobre objeto existente (Update)**

```csharp
_mapper.Map(beerUpdateDto, beerExistente);
```

👉 Solo actualiza propiedades presentes en el DTO, mantiene intactas las demás (ej. `Id`).

---

🔹 **Beneficios**

- Menos código repetido.
- Reutilización de reglas de mapeo.
- Consistencia en toda la app.
- Mejor legibilidad y mantenimiento.
  `

````
//// ✅ 26. Manejo de Errores en ASP.NET Core

📌 **Errores de negocio**: son los que rompen las reglas propias del sistema (ej: nombre de cerveza repetido, venta sin inventario).

---

### 🔹 Alternativas para manejar errores
1. **Excepciones en el servicio**
   - Lanzar `throw` y capturar en el controlador.
   - Útil para errores excepcionales (BD caída, fallo de servicio).
   - No ideal para reglas de negocio (impacta rendimiento).

2. **Capa intermedia de validaciones**
   - Entre controlador y servicio.
   - Valida reglas de negocio (ej: unicidad de nombre, descuentos).
   - Diferente a validaciones de formato.

3. **Validaciones dentro del validador existente**
   - Se mezclan validaciones de formato + negocio.
   - Problema: muchas veces requiere consultar repositorio.

4. **En la capa de servicio (✅ opción usada)**
   - Método `Validate(dto)` → retorna `true/false`.
   - Propiedad pública `Errors: List<string>` con mensajes descriptivos.
   - El controlador:
     ```csharp
     if (!service.Validate(dto))
         return BadRequest(service.Errors);
     ```

---

### 🔹 Métodos de Validación en el Servicio
- Definidos en la interfaz (`ICommandService`).
- Uso de **sobrecarga**:
  ```csharp
  bool Validate(BeerInsertDto dto);
  bool Validate(BeerUpdateDto dto);
````

- `Errors` solo tiene getter (no se setea desde afuera).
- En el controlador se chequea antes de ejecutar la acción.

---

### 🔹 Método de Búsqueda en Repositorio

- Regla: **no repetir nombre de cerveza**.
- No se usa `constraint UNIQUE` (no sirve con borrado lógico).
- Se crea método genérico:
  ```csharp
  IEnumerable<TEntity> Search(Func<TEntity, bool> filter);
  ```
- Permite búsquedas dinámicas con condiciones (`WHERE`) sin estar acoplado a un campo fijo.
- Definido también en `IRepository<T>`.

---

### 🔹 Implementación en Capa Servicio

✔️ **Insert**

```csharp
var result = _repo.Search(b => b.Name == dto.Name);
if (result.Count() > 0)
{
    Errors.Add("No puede existir una cerveza con un nombre ya existente.");
    return false;
}
return true;
```

✔️ **Update**

```csharp
var result = _repo.Search(b => b.Name == dto.Name && b.BeerID != dto.Id);
if (result.Count() > 0)
{
    Errors.Add("No puede existir una cerveza con un nombre ya existente.");
    return false;
}
return true;
```

- En **Update** se ignora el mismo `Id` para permitir actualizar sin cambiar el nombre.
- En el **controlador** no hace falta cambiar nada: solo se revisa el resultado de `Validate()` y se responde con **400 Bad Request** o **200 OK**.

---

### ✅ Resumen

- Se centraliza el manejo de errores de negocio en la **capa de servicio**.
- Se implementa con:
  - `Validate()` (sobrecarga Insert/Update).
  - Propiedad `Errors` con lista de mensajes.
  - `Search(Func)` en repositorio para búsquedas dinámicas.
- Ventaja: **código claro, flexible y reutilizable**.
- Es solo una alternativa: la elección depende de la arquitectura y políticas del proyecto.

```

```
