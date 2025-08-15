```csharp
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

```
