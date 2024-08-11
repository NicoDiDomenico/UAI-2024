user1 = {
    name: 'Ryan',
    lastname: 'Perez',
    age: 30,
    showFullName(){
        return this.name + ' ' + this.lastname
    }
}

user2 = {
    name: 'Ryan',
    lastname: 'Perez',
    age: 30,
    showFullName(){
        return this.name + ' ' + this.lastname
    }
}

user3 = {
    name: 'Ryan',
    lastname: 'Perez',
    age: 30,
    showFullName(){
        return this.name + ' ' + this.lastname
    }
}

// Cuando tengamos que crera varios obejetos del mismo tipo vamos a recurrir al constructor
/* CONSTRUCTOR: */
function Persona(name, lastname, age){
    this.name = name /* Notar que no es con : es con = */
    this.lastname = lastname
    this.age = age
    this.showFullName = function(){ // En los constructores esta forma es obligatoria para que funcionen los metodos
        return this.name + ' ' + this.lastname
    }
}

var user4 = new Persona('Juan', 'Perez', 78);
console.log(user4);
console.log(user4.showFullName())

// Metodos utiles de la palabra clave Object
console.log(Object.keys(user4))
console.log(Object.values(user4))