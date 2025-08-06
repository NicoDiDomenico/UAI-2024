// Polimorfismo de Subtipos - permite objetos con diferentes tipos pero con una relación de herencia. Este tipo de polimorfismo es un concepto en la programación orientada a objetos donde una función o método puede trabajar con objetos de diferentes tipos que pertenecen a una jerarquía de clases (es decir, clases que heredan de una clase base común). En otras palabras, permite que una función opere sobre diferentes subtipos de una misma superclase, lo cual es comúnmente implementado a través de la herencia y la sobreescritura de métodos.

class Person {
    constructor(name, lastname) {
      this.name = name;
      this.lastname = lastname;
    }
  }
  
  class Programmer extends Person {
    constructor(language, name, lastname) {
      super(name, lastname);
      this.language = language;
    }
  }
  
  const john = new Person('john', 'ray');
  const ryan = new Programmer('javascript', 'ryan', 'ray');
  
  console.log(john);
  console.log(ryan);
  
  function writeFullName(p) { // notar como le paso un solo objeto que en realidad puede ser tanto john como ryan, pero al tener ambos los mismmos atributos de la superclase entonces la funcion sirve para ambos.
    console.log(p.name + " " + p.lastname);
  }
  
  writeFullName(john);
  writeFullName(ryan);
  