// Herencia - crear objetos especializados a partir de uno mas genérico
// Antes:
function Person() {
    this.name = '';
    this.lastname = '';
  }
  
  function Programmer() {
    this.language = '';
  }
  
  Programmer.prototype = new Person();
  
  console.log(Programmer);
  console.log(Person);
  
  const person = new Person();
  person.name = 'maria';
  person.lastname = 'perez';
  console.log(person);
  
  const programmer = new Programmer();
  programmer.name = 'ryan';
  programmer.lastname = 'ray';
  programmer.language = 'javascript';
  console.log(programmer);

  // Ahora usando class:
  class Person {
    constructor(name, lastname) {
      this.name = name;
      this.lastname = lastname;
    }
  }
  
  class Programmer extends Person { // con extends me permite definir que Programmer será subclase de Person
    constructor(language, name, lastname) {
      super(name, lastname); // super() es lo que me permite heredar metodos y atrobitos de Person
      this.language = language;
    }
  }
  
  const person = new Person('maria', 'perez');
  console.log(person);
  
  const programmer = new Programmer('python', 'joe', 'mcmillan');
  console.log(programmer);
  
