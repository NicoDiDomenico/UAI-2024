// 1)
using System;

Console.WriteLine("1)");

AutoDeportivo autoDeportivo = new AutoDeportivo();
autoDeportivo.Marca = "Ferrari"; 
Console.WriteLine($"Marca: {autoDeportivo.Marca}"); // Acceso público a Marca
autoDeportivo.SetCombustible("Gasolina"); 
autoDeportivo.MostrarCombustible();

// 2)
Console.WriteLine("\n2)");

Gerente gerente = new Gerente();
gerente.Nombre = "Juan Perez";
gerente.Departamento = "Ventas";
Console.WriteLine($"Nombre: {gerente.Nombre}, Departamento: {gerente.Departamento}");
gerente.Trabajar();

// 3)
Console.WriteLine("\n3)");

GerenteSuperior gerenteSuperior = new GerenteSuperior();
gerenteSuperior.Nombre = "Ana Gomez";
gerenteSuperior.Departamento = "Marketing";
gerenteSuperior.Trabajar(); // Llama al método sobreescrito en GerenteSuperior

// 4)
Console.WriteLine("\n4)");

Calculadora calculadora = new Calculadora();
calculadora.Sumar(5, 10); // Suma de dos enteros
calculadora.Sumar(5, 10, 15); // Suma de tres enteros
calculadora.Sumar(5.5, 10.3); // Suma de dos double

// 5)
Console.WriteLine("\n5)");

Animal animal = new Animal();
Perro perro = new Perro();
Gato gato = new Gato();

animal.HacerSonido(); // Sonido genérico
perro.HacerSonido(); // Guau
gato.HacerSonido(); // Miau

// 6)
Console.WriteLine("\n6)");

Mensaje mensaje = new Mensaje();
MensajeUrgente mensajeUrgente = new MensajeUrgente();

mensaje.Enviar("Hola Mundo"); // Mensaje normal
mensaje.Enviar("Hola Mundo", "Juan"); // Mensaje con destinatario

mensajeUrgente.Enviar("Atención inmediata"); // Mensaje urgente con un solo parámetro
/*
 1. Modificadores de acceso (public, private, protected)
Enunciado:
Creá una clase llamada Auto que tenga los siguientes campos:
Una propiedad Marca accesible desde cualquier parte del programa.
Un campo patente que solo sea accesible dentro de la clase Auto.
Un campo combustible que pueda ser accedido desde la clase Auto y también por cualquier clase que herede de Auto.
Luego creá una clase AutoDeportivo que herede de Auto y mostrá cómo puede acceder a combustible pero no a patente.
 */

class Auto
{
    public string Marca { get; set; }
    // public string Marca { get; private set; } // se puede leer desde fuera, pero solo escribir desde dentro de la clase
    private string _patente { get; set; }
    protected string _combustible { get; set; }

}

class AutoDeportivo : Auto
{
    public void MostrarCombustible()
    {
        // Console.WriteLine(_patente); // No se puede acceder a patente desde AutoDeportivo
        Console.WriteLine($"Combustible: {_combustible}"); // Se puede acceder a combustible porque es protected
    }

    public void SetCombustible(string combustible)
    {
        _combustible = combustible; // Se puede modificar el combustible desde AutoDeportivo
    }
}

/*
2. Herencia
Enunciado:
Creá una clase base llamada Empleado que contenga una propiedad Nombre y un método Trabajar() que imprima un mensaje genérico.

Luego, creá una clase derivada llamada Gerente que herede de Empleado y agregue una propiedad Departamento. Mostrá cómo usar el.
método Trabajar() desde un objeto Gerente.
*/

class Empleado
{
    public string Nombre { get; set;}

    public virtual void Trabajar()
    {
       Console.WriteLine("El empleado está trabajando.");
    }
}

class Gerente : Empleado
{
    public string Departamento { get; set; }
}

/*
3. Sobreescritura (override)
Enunciado:
Modificá el ejercicio anterior. Hacé que el método Trabajar() de la clase Empleado sea virtual, y sobreescribilo en Gerente para que diga: "El gerente está gestionando el departamento de [Departamento]".

Mostrá la diferencia entre llamar al método con una instancia de Empleado y una de Gerente.
*/
class GerenteSuperior : Empleado
{
    public string Departamento { get; set; }
    public override void Trabajar()
    {
        Console.WriteLine("El gerente está gestionando el departamento de " + Departamento);
    }
}
/*
 4. Sobrecarga (overload)
Enunciado:
Creá una clase Calculadora que tenga un método llamado Sumar.

Sobrecargá el método Sumar para que funcione con:

Dos enteros

Tres enteros

Dos double

Probalo desde el programa principal.
*/

class Calculadora
{
    public void Sumar(int a, int b)
    {
        Console.WriteLine($"Suma de dos enteros: {a + b}");
    }
    public void Sumar(int a, int b, int c)
    {
        Console.WriteLine($"Suma de tres enteros: {a + b + c}");
    }
    public void Sumar(double a, double b)
    {
        Console.WriteLine($"Suma de dos double: {a + b}");
    }
}

/*
5.Combinar herencia y sobreescritura
Enunciado:
Creá una clase Animal con un método HacerSonido() virtual que diga "Sonido genérico".

Luego, creá dos clases hijas: Perro y Gato, cada una sobreescribiendo HacerSonido() con "Guau" y "Miau" respectivamente.

Instanciá un animal de cada tipo y llamá al método HacerSonido().
*/

class Animal
{
    public virtual void HacerSonido()
    {
        Console.WriteLine("Sonido genérico");
    }
}

class  Perro : Animal
{
    public override void HacerSonido()
    {
        Console.WriteLine("Guau");
    }
}

class  Gato : Animal
{
    public override void HacerSonido()
    {
        Console.WriteLine("Miau");
    }
}

/*
6. Combinar sobrecarga y herencia
Enunciado:
Creá una clase Mensaje con un método Enviar que reciba un solo parámetro string.

Sobrecargá el método Enviar para aceptar dos parámetros: string mensaje y string destinatario.

Luego, creá una clase hija MensajeUrgente que herede de Mensaje y sobreescriba el método de un solo parámetro para agregar la palabra "[URGENTE]" al inicio del mensaje.
*/

class Mensaje
{
    public virtual void Enviar(string mensaje)
    {
        Console.WriteLine($"Mensaje: {mensaje}");
    }

    public void Enviar(string mensaje, string destinatario)
    {
        Console.WriteLine($"Mensaje: {mensaje} para {destinatario}");
    }
}

class MensajeUrgente : Mensaje
{
    public override void Enviar(string mensaje)
    {
        Console.WriteLine($"[URGENTE] {mensaje}");
    }
}