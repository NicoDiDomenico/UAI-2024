/* Tipos de datos */
/* Primitivos: --> están en el stack, hacen referencia a la direccion física
Number --> 1 2 5 6 8 94 45 
String --> "Hola mundo"
Boolean --> true false 
Undefined --> no tiene un valor declarado
Null --> cuando el usuario no elige nada
*/
/* De referencia: --> están en el heap de la RAM, parte dinamica.
Array
Object
Function
Clases
*/

/* Declaraciones y expresiones */
/* 
en el lengiaje ingles se agrega un 3er concepto que es: statement, que seria declaracion pero de forma escrita. Mientras que declaration es de forma oral
En JS:
declaration es para algo que vas a referenciar en un futuro, ej. let, const, function + *, async function + *, class export/import
Statements es para cuando aplicas lógica, ej. it, for, else, switch
expresion: cualquier linea de codigo que evalua en un valor 4 + 6 --> 10, X = 4, miFuncion(). A las expresiones se les puede aplicar console.log() al resto no
*/

/* JS usa tipado dinamico, el tipo de variable se va modificando. string -a-> number */
var variable; /* si no ponemos nada la variable devuelve undefined */
var nombre = "Nico" /* asignamos --> guardamos espacio en memoria para esa variable */ 
var edad = 20
var soltero = true
var trabajo
var numero = 10 + 10 /* opradores matematicos */
var rta = 10 === "10" /* --> acostumbrarse a usar esta que es la que mi cabeza interpreta mejor */
var nacimiento = 1996 
var actualidad = 2024
/* si queremos hacer que las variables no puedan cambiar su valor usamos const */

console.log(variable)
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
console.log(nacimiento)

/* operadores aritmeticos: 
+ adicion
- resta
* multiplicacion
/ division
% módulo o resto 
** potencia
*/

/* operador incremento: 
var a = 1;
++a (suma 1 a 1 => 2)
a++ (a 2 le suma 1 => 3)
*/

/* operador de asignacion: 
son los operadores aritmeticos + un =, asi hace la operacion y la asigna a una variable.
a += 5; le sumo 5.
*/

/* operador logico: */
var a = 10
/* relacionales */
console.log(5 > 1);
console.log(5 < 1);
console.log(2 >= 2);
console.log( 5 <= 3);   

/* de igualdad */
console.log(10 == a) /* True */
console.log(10 != a) /* False */
console.log(a == '10') /* True, porque == pregunta si los valores son iguales y NO si los tipos son iguales. */ 
console.log(a === '10') /* False, ahora si pregunta si el valor y el tipo es igual */
console.log(a !== '10') /* True, ahora pregunta si el valor o el tipo son ditintos */
/* usar los 2 ultimos para no equivocarme nunca */

/* operadores lógicos:
/* and */
console.log(true && true); //--> true
console.log(false && true); //--> false 
/* or */
console.log(false || true ) // si al menos uno es true devuelve el total como true, por lo tanto para que devuelva salse como total tienen que ser ambos false
/* not - invierte el valor*/
console.log(!true)
console.log(!false) 

/* 
temas que explico el de hla mundo pero no los vimos en clases:
Short circuit
Operadores bitwise
*/
/* Orden de operaciones: */
/* var rta = 8/2(2+2); */ /* MAL */
var rta = 8/2*(2+2); /* ANDA */
var rta = 8/(2*(2+2)); /* BIEN */ 

/* operador ternario: */
// Otra forma de hacer if-else: operador ternario
/* (condicion) ? (caso que es true) : (caso que es false) */ /* no se recomienda hacer if anidado en este operador ternario  */
var edad = 15
acceso = edad > 16 ? "Es mayor, puede pasar" : "tererible pendejo, afuere bobito."
console.log(acceso) 