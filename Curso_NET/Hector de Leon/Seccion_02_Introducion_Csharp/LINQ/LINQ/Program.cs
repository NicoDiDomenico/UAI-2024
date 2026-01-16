// 15. LINQ - Lenguaje Integrado de Consultas
// LINQ nos permite consultar y manipular colecciones (listas, arrays, etc.)
// usando una sintaxis parecida a SQL o bien métodos encadenados.

// Lista base de ejemplo
var names = new List<string>()
{
    "Juan", "Pepe", "Ana", "Hugo", "Nico"
};

// ======================================================
// 1) Sintaxis de CONSULTA (parecida a SQL)
// ======================================================
// "from n in names"  → recorre la lista "names" y asigna cada elemento a la variable temporal "n".
// "where ..."        → aplica un filtro (solo deja pasar nombres con longitud > 3 y < 5 caracteres).
// "orderby n"        → ordena alfabéticamente de forma ascendente.
// "select n"         → selecciona qué devolver (en este caso, el mismo elemento "n").
// El resultado NO se calcula todavía (ejecución diferida), recién se evaluará en el foreach.
var namesResult = from n in names
                  where n.Length > 3 && n.Length < 5
                  orderby n
                  select n;

// ======================================================
// 2) Sintaxis de FUNCIONES (métodos de extensión LINQ)
// ======================================================
// Funciona igual que el ejemplo anterior pero usando funciones encadenadas:
//
// Where(...)            → filtra la lista con la misma condición.
// OrderByDescending(...)→ ordena alfabéticamente pero de forma descendente.
// Select(...)           → elige qué devolver (en este caso, el mismo elemento).
// También es ejecución diferida: no se evalúa hasta recorrerlo.
var namesResult2 = names.Where(n => n.Length > 3 && n.Length < 5)
                        .OrderByDescending(n => n)
                        .Select(d => d);

/*  
    EJECUCIÓN INMEDIATA:
    Si agregamos ".ToList()" o ".ToArray()" al final, LINQ ejecuta la consulta en ese momento
    y guarda los datos en memoria como lista o arreglo.
    Ejemplo:
    var namesResult3 = (from n in names
                        orderby n
                        select n).ToList();
*/

// ======================================================
// 3) Recorremos el resultado
// ======================================================
// Aquí es cuando realmente se ejecuta la consulta si NO usamos ToList() antes.
// En cada vuelta, LINQ toma el siguiente elemento filtrado y ordenado
// y lo pasa a "name" para que lo usemos en el cuerpo del foreach.
foreach (var name in namesResult2)
{
    Console.WriteLine(name);
}

Console.WriteLine(); // Imprime una línea en blanco
