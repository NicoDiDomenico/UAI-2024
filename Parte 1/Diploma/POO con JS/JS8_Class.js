// Class fue agregado en ES6, con el objetivo de hacer todo mas legible, y unificando todos los conceptos vistos anteriormente como prototype o 'use strict'.

// Antes:
// function Person(name, lastname) {
//  'use strict'
//  this.name = name
//  this.lastname = lastname
// }


// Ahora:
class Person {
    // Primero se define el constructor
    constructor(name, lastname) {
      this.name = name
      this.lastname = lastname
    }
    // Separado van lo métodos cómo se hacia en los objetos literales
    greet() {
      return `Hello I am ${this.name} ${this.lastname}`
    }
  }
  
  const user = new Person('joe', 'ray')
  const user2 = new Person('ryan', 'ray')
 
  console.log(user)
  console.log(user2)
  