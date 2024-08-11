// Asociación - relaciona 2 objetos entre si
class Person {

    constructor(name, lastname) {
      this.name = name
      this.lastname = lastname
    }
  }
  
  const john = new Person('john', 'ray')
  const maria = new Person('maria', 'perez')
  
  // relation
  maria.parent = john; /* maria tiene como 'padre' a john */
  
  console.log(maria)
  console.log(john)