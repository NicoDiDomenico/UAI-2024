var nombre = "Nico" /* asignamos --> guardamos espacio en memoria para esa variable */
var edad = 20
var soltero = true
var trabajo
var numero = 10 + 10 /* opradores matematicos */
var rta = 10 === "10" /* --> acostumbrarse a usar esta que es la que mi cabeza interpreta mejor */
var nacimiento = 1996 
var actualidad = 2024

console.log(nombre) /* Muestra mensaje en pantalla */
console.log(typeof(nombre)) /* es el type() de python */

console.log(edad)
console.log(typeof(edad))
console.log("La edad es " + edad + " y su tipo es " + typeof edad)

edad = "veinte"
console.log("La edad es " + edad + " y su tipo es " + typeof edad) /* notar como se adapta a la variable el console, en cambio en python el print tiene que estar compuesto por strings */

console.log("Soltero: " + soltero + " y " + typeof soltero)

console.log(trabajo)

trabajo = null
console.log(trabajo)

/* ==  */
/* === */ 
/* The main difference between the two operators is how they compare values. The == operator compares the values of two variables after performing type conversion if necessary. On the other hand, the === operator compares the values of two variables without performing type conversion. */

console.log(++numero) /* si pongo ++numero antes se va a mostrar si pongo numero++ no se mostrara porque se incrementa despues de mostarlo */
console.log(rta)

/* esto sirve para numer nomas */
nacimiento = nacimiento + actualidad
/* optimizo */
nacimiento += actualidad 
console-log(nacimiento)