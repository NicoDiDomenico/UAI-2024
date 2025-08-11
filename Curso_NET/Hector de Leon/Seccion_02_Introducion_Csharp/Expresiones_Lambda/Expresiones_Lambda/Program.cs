//// 14. Expresiones Lambda

// 1. Forma “tradicional” - Antes haciamos esto:
using System.Security.Cryptography;

Func<int, int, int> sub_PrimeraClase = sub1;

int sub1(int a, int b)
{
    return a - b;
}

// 2. Lambda con tipos explícitos - Ahora se pude reducir usando funciones lambda 
Func<int, int, int> sub2_PrimeraClase = (int a, int b) => a - b;

// 3. Lambda con tipos inferidos - A medida que C# se fue actualizando se permitió que no sea neecesario colocar el tipo de dato en los parámetors si se usa delegado:
Func<int, int, int> sub3_PrimeraClase = (a, b) => a - b;

// 4. Con un parámetro se simplifica mas, no se necesian los ():
Func<int, int> sub4_PrimeraClase = a => a * 2;

// 5. Pero si se necesita mas de una linea de código se recurres a usar {}:
Func<int, int> sub5_PrimeraClase = a => 
{
    a = a + 2;
    return a * 2;
};

Console.WriteLine(sub_PrimeraClase(1,2));

// 6. Aplicando lambda a funciones de orden superior
// Antes haciamos esto:
sub_OrdenSuperior(sub3_PrimeraClase, 1, 2);
void sub_OrdenSuperior(Func<int, int, int> fn, int a, int b)
{
    int rta = fn(a, b);
    Console.WriteLine("El resultado es " + rta);
}

// Ahora podemos aplicar la función lambda dentro del la función de orden superior:
sub2_OrdenSuperior((a, b) => a - b, 1, 2);
void sub2_OrdenSuperior(Func<int, int, int> fn, int a, int b)
{
    int rta = fn(a, b);
    Console.WriteLine("El resultado es " + rta);
}