// Polimorfismo parametrico - es la capacidad de un método que puede funcionar con parametros de cualquier tipo (generics).
function Stack() {
    this.items = [];
  
    this.push = function(item) {
      this.items.push(item);
    }
  }
  
  const stack = new Stack();
  stack.push('asdasdas');
  
  const stack2 = new Stack();
  stack2.push(1000);
  
  console.log(stack);
  console.log(stack2);
  
  /* 
   Polimorfismo parametrico en tipado dinámico vs estático:
   El polimorfismo paramétrico es un concepto de la programación orientada a objetos que permite que las funciones o métodos trabajen con diferentes tipos de datos sin necesidad de especificar el tipo exacto con el que van a trabajar. Es común en lenguajes que soportan tipado dinámico, como JavaScript, y en lenguajes con soporte para generics, como C#.

Polimorfismo Paramétrico en JavaScript
JavaScript es un lenguaje de tipado dinámico, lo que significa que las variables y funciones no están restringidas a un tipo de datos específico. Esto hace que el polimorfismo paramétrico sea muy fácil de aplicar en JavaScript. Una función puede operar sobre cualquier tipo de dato sin necesidad de modificaciones adicionales:
    
    function identity(x) {
        return x;
    }   

    console.log(identity(5)); // Funciona con un número
    console.log(identity("Hola")); // Funciona con una cadena
    console.log(identity({ name: "Alice" })); // Funciona con un objeto

En el ejemplo anterior, la función identity puede aceptar cualquier tipo de dato (number, string, object, etc.) sin requerir ninguna modificación en la declaración de la función.

Polimorfismo Paramétrico en C#
C# es un lenguaje de tipado estático, lo que significa que los tipos de datos deben ser conocidos y definidos en tiempo de compilación. Para implementar polimorfismo paramétrico en C#, se utilizan generics. Los generics permiten que una clase o método opere sobre tipos especificados en tiempo de ejecución, pero requieren una definición explícita del tipo de dato al momento de la declaración:

    public T Identity<T>(T x) {
        return x;
    }

    Console.WriteLine(Identity(5)); // Funciona con un número
    Console.WriteLine(Identity("Hola")); // Funciona con una cadena
    Console.WriteLine(Identity(new { Name = "Alice" })); // Funciona con un objeto anónimo

Aquí, se define una función genérica Identity que puede aceptar cualquier tipo de dato, pero es necesario especificar el tipo T en la firma del método.

¿Por qué es más fácil aplicar Polimorfismo Paramétrico en JavaScript?
    Tipado Dinámico: En JavaScript, no es necesario especificar el tipo de las variables o los parámetros de las funciones. Esto significa que una función puede operar directamente sobre cualquier tipo de dato sin ninguna configuración adicional, facilitando la implementación del polimorfismo paramétrico.

    Simplicidad: Dado que JavaScript no requiere la definición explícita de tipos, el código es generalmente más conciso y flexible. No necesitas declarar genéricos o manejar múltiples sobrecargas de métodos, como en C#.

    Flexibilidad en Tiempo de Ejecución: JavaScript permite cambiar el tipo de una variable en tiempo de ejecución, lo que hace que el manejo de diferentes tipos de datos en una función sea más directo y menos restrictivo.
  */