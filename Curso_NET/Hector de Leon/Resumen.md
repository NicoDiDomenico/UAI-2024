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
```
