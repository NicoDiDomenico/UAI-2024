var edad = 20
var dia = 3

// if-else
if (edad < 10) {
    console.log("Es niño")
} else if (edad <= 10){
    console.log("Es niño o joven")
} else {
    console.log("Es adulto") 
}

// Otra forma de hacer if-else: operador ternario
/* (condicion) ? (caso que es true) : (caso que es false) */ /* no se recomienda hacer if anidado en este operador ternario  */
 
(edad <= 10) ? console.log("Es niño") : console.log("Es adulto") 

// Switch
console.log("")
console.log("Switch:")
switch (dia){
    case 1:
        console.log("LUNES")
        break;
    case 2:
        console.log("MARTES")
        break;
    default:
        console.log("Dia no admitido")
        break;
}

// bucle for
console.log("")
console.log("for:")
for (var index = 0; index <= 10; index++){
    console.log("El indice es: ", index) /* , tiene la misma funcion que + en el consol.log() */
}
console.log("")
for (var index = 10; index > 0; index--){
    console.log("El indice es: ", index) /* , tiene la misma funcion que + en el consol.log() */
}

// while 
console.log("")
console.log("while: ") /* se ejecuta si se cumple esa condicion */
var index = 0
while (index <= 10){
    console.log(index)
    index++
}

//do
console.log("")
console.log("do: ") /* se ejecuta siempre */
var i = 0
do{
    console.log("index: ", i)
    i ++
} while (i <= 10);

// Arreglos
console.log("")
console.log("Arreglos:")
var frutas = ["Banana","Manzana"] /* declaro el arreglo */

console.log(frutas)
console.log(frutas[0])
console.log(frutas[1])

console.log("Largo del arreglo: ", frutas.length) 

/* console.log("Ejemplo practico:")
for (var index = 0; index < frutas.length; index++){
   var fruta = frutas[index];
   console.log(fruta)
} */

// Push - agregar un elemento al final del array
console.log("Push:")
console.log(frutas)
frutas.push("Pera")
console.log(frutas)

//unshift - googlear
console.log("Unshift:")
console.log(frutas)
frutas.unshift("Naranja")
console.log(frutas)

//pop - googlear
console.log("Pop:")
console.log(frutas)
frutas.pop("Pera")
console.log(frutas)

//shift - googlear
console.log("Shift:")
console.log(frutas)
frutas.shift("Banana")
console.log(frutas)

// Objetos (en python creo que se llama libreria o bilbioteca aslgo asi)
console.log("")
console.log("Objetos:")
var persona = {
    nombre: "Nico",
    apellido: "Di Domenico",
    edad: 27,
    trabajo: null,
    soltero: true
}
console.log(persona["apellido"])

var personas = [
    {
    nombre: "Nico",
    apellido: "Di Domenico",
    edad: 27,
    trabajo: null,
    soltero: true,
    gustos: ["Leer", "Viciar"]
    },
    {
    nombre: "Juan",
    apellido: "Di Domenico",
    edad: 25,
    trabajo: null,
    soltero: true,
    gustos: ["Codear", "Beber"]
    }
]

/* for (var index = 0; index < personas.length; index++){
    var persona = personas[index];
    console.log(persona)
 } */  /* habia hecho mas cosas el profe */

 //Funciones
 console.log("")
 console.log("Funciones:")
 function multiplicar(num1, num2){
    return num1 * num2;
 } /* dijero que "una funcion siempre puede devolver algo excepto si usas void" */

 var rta = multiplicar(2, 6)
console.log(rta) /* no entiendo en que caso devuele NaN */

// DE ACÁ PARA ADELANTE ME RE PERDI

// Ejemplo practico viendo arreglos y funciones - que
var frutas2 = []

function guardarFruta(fruta) {
    frutas2.push(fruta)
}

guardarFruta("Manzana")
guardarFruta("Pera")

/* console.log(frutas2) */

// forEach
frutas2.forEach(function(fruta, index, array){
 /* no se que va acá */
})

// map
var nuevoArreglo = frutas2.map(function(fruta, index, array){
    return fruta + "!!!"
})

console.log(frutas2)
console.log(nuevoArreglo)

// metodo some
var existe = frutas.some(function(fruta) {
    return fruta === "Manzana Verde"
}) /* creo que me quedó incompleto */

