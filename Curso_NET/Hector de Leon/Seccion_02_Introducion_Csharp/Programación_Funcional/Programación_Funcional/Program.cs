/*
## 📝 **Ejercicios para practicar**
### **1. Funciones puras vs impuras**

1. Escribí una función **impura** que reste un número a una variable global y otra **pura** que haga lo mismo pero sin modificar la variable global.
2. Creamos una clase `Producto` y escribí:

   * Un método impuro que cambie el precio dentro del objeto recibido.
   * Un método puro que devuelva un **nuevo producto** con el precio cambiado sin modificar el original.

---
*/

// 1.
Trabajador programador = new Trabajador();
programador.Nombre = "Nico";
programador.Salario = 1550;

Console.WriteLine("Variable global:" + programador.Salario);
ModificarSalario(programador);
Trabajador programadorCopia = ModificarSalario2(programador);
Console.WriteLine("Despues de aplicar la funcion impura: " + programador.Salario + ". La variable global cambia");
Console.WriteLine("Despues de aplicar la funcion pura: " + programadorCopia.Salario);
Console.WriteLine("Notar como no cambia la variable global no cambia: " + programador.Salario);

// Función impura:
void ModificarSalario(Trabajador trabajador)
{
    trabajador.Salario -= 1000;
}

// Función pura:
Trabajador ModificarSalario2(Trabajador trabajador)
{
    Trabajador programadorCopia = new Trabajador()
    {
        Nombre = trabajador.Nombre,
        Salario = trabajador.Salario - 1000,
    };
    return programadorCopia;
}

public class Trabajador
{
    public string Nombre {  get; set; }
    public double Salario {  get; set; }
}


/*
### **2. Inmutabilidad**

1. Usando `DateTime`, sumale días a una fecha sin modificar la original y verificá que la original sigue igual.
2. Usando un `struct` propio (ej: `struct Punto { public int X; public int Y; }`), probá modificarlo y comprobá si cambia el original o la copia.
3. Usando una `class` `Persona`, cambia su nombre dentro de un método y analizá por qué cambia el original.

---

### **3. Funciones de primera clase**

1. Guardá una función `Doble(int x)` en una variable `Func<int,int>` y llamala desde ahí.
2. Pasá la función `Doble` como parámetro a otra función que reciba un `Func<int,int>` y la ejecute.
3. Crea una función que devuelva otra función (ej: `Saludador` que recibe un nombre y devuelve una función que imprime “Hola, {nombre}”).

---

### **4. Funciones de orden superior**

1. Escribí una función `Operar` que reciba dos números y una función (`Func<int,int,int>`) para definir si suma, resta o multiplica.
2. Crea una función `Potenciador(int potencia)` que devuelva una función que eleve un número a esa potencia.
3. Hacé un programa que reciba una lista de números y use `Func<int,bool>` para filtrar solo los pares.

---

### **5. Delegados, Action y Func**

1. Usá un **delegado explícito** `delegate void MostrarMensaje(string texto);` para imprimir un saludo.
2. Reemplazá el delegado explícito por un `Action<string>` y probá que funcione igual.
3. Creá un `Func<int,int,int>` que apunte a una función `Sumar` y mostrala en pantalla.
4. Creá una función `ProcesarLista` que reciba:

   * Una lista de enteros.
   * Un `Action<int>` para procesar cada elemento (por ejemplo, imprimirlo).
   * Un `Func<int,bool>` para decidir qué elementos procesar (por ejemplo, solo mayores a 5).

---

### **6. Ejercicio Integrador**

📌 Juntá todo lo anterior en un mini programa:

* Una lista de `Producto` (con `Nombre` y `Precio`).
* Usá **inmutabilidad** para aplicar un descuento sin modificar los productos originales.
* Usá **funciones de orden superior** para aplicar distintos cálculos (`IVA`, `Descuento`).
* Usá **Action** para mostrar el resultado final en pantalla.
* Usá **Func** para calcular el total final de todos los precios.

 */