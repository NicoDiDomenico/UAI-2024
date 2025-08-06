// Composicion - es un tipo fuerte de agreacion donde muchos objetos pueden pertener a otro objeto que tiene un rol mayor y que los objetos componentes no pueden tener una vida independiente
// Como objeto literal:
const person1 = {
    name: 'ryan',
    lastname: 'ray',
    // Notar como adress no existe por si solo, si o si depende de person1
    address: {
      street: '123 baker street',
      city: 'london',
      country: 'united kingdom'
    }
  }

// Usando Class:
class Person {
    constructor(name, lastname, street, city, country) {
      this.name = name;
      this.lastname = lastname;
      this.address = {
        street: street,
        city: city,
        country: country
      };
    }
  }
  
  const person2 = new Person('ryan', 'ray', '123 baker street', 'london', 'united kingdom');
  console.log(person2);
  