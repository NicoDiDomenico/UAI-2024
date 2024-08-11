function Person(name, lastname) {
    this.name = name
    this.lastname = lastname
    this.displayName = function() {
        return `${this.name} ${this.lastname}`
    }
}

const john = new Person("John", "Mcmillan")
john.name = 'Joe'
console.log(john.displayName())

const mario = new Person("Mario", "Rossi")
console.log(mario.displayName())

// Notar como puedo agregar métodos al objeto
john.greet = function() {
return `Hello I'm ${this.name}`
}

console.log(john); // john tien el metodo nuevo porque se lo agregamos
console.log(mario); // pero mario sigue igual que antes porque el metodo solo s elo agregamos a john

// Para alterar constructores y asi agregar metodos o atributos utilizamos prototype, que hace referencia a los mismos y asi extenderlos.
Person.prototype.greet = function() {
    return "Me llamo " + this.name
}

console.log(john);
console.log(mario);

  