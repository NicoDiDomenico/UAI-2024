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

// Switch - depende del valor de la variables, se ejecuta un determinado codigo.
console.log("")
console.log("Switch:")
switch (dia){
    case 1:
        console.log("LUNES")
        break; /* si no pongoi el break se van a ejecutar los siguintes case */
    case 2:
        console.log("MARTES")
        break;
    default: /* si ningun caso se ejecuta se ejecutará esta. */
        console.log("Dia no admitido")
        break;
}

// bucle for
console.log("")
console.log("for:")
// for (inicializacion; comparacion; fin-iteracion){} --> 3 expresiones tiene el for
for (var index = 0; index <= 10; index++){
    console.log("El indice es: ", index) /* , tiene la misma funcion que + en el consol.log() */
}
console.log("")
for (var index = 10; index > 0; index--){
    console.log("El indice es: ", index) /* , tiene la misma funcion que + en el consol.log() */
}
// for of (estilo python)
console.log("")
console.log("for of:")
var i = 1
var marcas = ["Samsung", "Lenovo", "Apple"];
for (var marca of marcas){
    console.log(i + "°: " + marca);
    i++;
}
// for in --> permite iterar las propiedades
console.log("")
console.log("for in:")
var persona = {
    id: 1,
    nombre: "Juan",
    apellido: "Lopez",
    edad: "40"
}
for (var key in persona){
    /* console.log(key) */
    console.log(key, persona[key])
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

// break | continue
console.log("")
console.log("continue: ") /* salteo el 3 */
var i = 1;
var j = 1;
while (i < 6){
    i++;
    console.log("n°: " + i)
    if (i === 3) {
        continue; 
    }
}
console.log("")
console.log("break: ") /* salteo los >= 3, corto el bucle. */
while (j < 6){
    if (j === 3) {
        break; 
    }
    console.log("n°: " + i)
    j++;
}

// Arreglos
console.log("")
console.log("Arreglos:")
var frutas = ["Banana","Manzana"] /* declaro el arreglo */

console.log(frutas)
console.log(frutas[0])
console.log(frutas[1])
frutas[2] = "Naranja"

console.log(typeof(frutas))
/* los array  se comportan como objetos
var frutas = {
0: Banana
1: Manzana
2: Naranja
}
*/

console.log("Largo del arreglo: ", frutas.length) /* te cuenta hasta los que esten Undenined */

/* console.log("Ejemplo practico:")
for (var index = 0; index < frutas.length; index++){
   var fruta = frutas[index];
   console.log(fruta)
} */

// Push - agregar un elemento al final(derecha) del array 
console.log("Push:")
console.log(frutas)
frutas.push("Pera")
console.log(frutas)

//unshift - agregar un elemento al inicio(izquierda) del array
console.log("Unshift:")
console.log(frutas)
frutas.unshift("Naranja")
console.log(frutas)

//pop - quita un elemento al final del array
console.log("Pop:")
console.log(frutas)
frutas.pop("Pera")
console.log(frutas)

//shift - quita un elemento al inicio del array
console.log("Shift:")
console.log(frutas)
frutas.shift("Banana")
console.log(frutas)

//splice - permite hacer un corte en el rango de indices que pasemos como parametros
console.log("Splice:");
console.log(frutas);
frutas.splice(1,1);
console.log(frutas);

// Otros: 2:24 / 12:23, https://www.youtube.com/watch?v=3i7BTHXVNec&list=PLJubkp8BnTJsDgWXWcS1Z0VDV7rAOqbU4&index=29&ab_channel=CodingTube 
/* 
var arregloNuevo = array.slice(1,3); - da como resultado un nuevo arreglo cuyos valores corresponden a los indices del 1 al 3 del arreglo con que trabajemos
arrayTotal = array1.concat(array2); - concatemanos arreglos
array.indexOf("Valor"); - devuelve el indice al que corresponda el valor que se encuentra en el arreglo, devuelve -1 si el elemento no se encontró
array.includes("Valor"); - devuelve true o false dependiendo si existe el valor en el arreglo
var rta = array.find(item => item."Valor" === 3); - .find se usa para arreglos de objetos, entonces en rta se almacena el objeto que tenga el valor en 3
var rta = array.findIndex(item => item."Valor" === 3); - .findIndex se usa para arreglos de objetos, entonces en rta se almacena el indice del objeto que estemos buscando. devuelve -1 si el objeto no se encontró
var rta = array.filter(item => item."Valor" <= 2); - devuelve un arreglo de aquellos objetos que se filtraron. 
*/
//forEach - En clases lo hicimos distinto. Ver mas abajo.
console.log("forEach:");
var array13 = [5, 2, 4, 3, 1];
console.log(array13);
var rta13 = [];
console.log(rta13);
array13.forEach(item => rta13.push(item*2)); // Pareciera que usó otra forma de hacer una función
console.log(rta13);
//map - =
// sort - ordenar el arreglo
console.log("sort:");
var array15 = [5, 2, 10, 3, 1];
array15.sort((a,b)=>a-b); // (a,b)=>a-b) le da lo ascendente, la verdad no entendí un carajo
console.log(array15);
//reverse - invertimos el orden
console.log("reverse:");
var array16 = [5, 2, 4, 3, 1];
array16.reverse();
console.log(array16);
//split - separa el string en arreglos de letras, la separacion depende del argumento.
console.log("split:");
var cadena = "amanecer";
var rta17 = cadena.split(""); 
console.log(rta17);
//join - junta los elementos del arreglo en un string. Puedo pasarle parametros al join para añadirle caracteres en la union. ej. join("/")
console.log("join:");
var arreglo = ["H", "o", "l", "a"];
console.log(arreglo);
var rta18 = arreglo.join(""); 
console.log(rta18);
//reduce - reducir lo que tenemos dentro del arreglo a un solo valor. Tiene 2 parámetros --> (funcion(acumulador,ValorEnIndice), valorInicialDelAcumulador)
console.log("reduce:");
var array19 = [5, 2, 4, 3, 1];
console.log(array19);
var rta19 = array19.reduce((sum, index) => sum + index, 0);
console.log(rta19);

// Objetos (en python creo que se llama libreria o bilbioteca aslgo asi)
console.log("")
console.log("Objetos:")
var persona = {
    nombre: "Nico",/* llave: valor --en js--> propiedad: valor */
    apellido: "Di Domenico",
    edad: 27,
    trabajo: null,
    soltero: true
} /* js no asegura el orden en que va a mostrar las propiedades */
/* accediendo: */
console.log(persona.apellido)
console.log(persona["apellido"])
delete persona.edad;
console.log(persona)

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
// Entendiendo su comportamiento
const personaje = {id: 9}; /* por mas que sea const yo puedo modificar y remover las ropiedades del objeto, pero no el mismisimo objeto. -para que las propiedades no se puedan modificar usamos-> const personaje = Object.freeze{id: 9}; */

personaje.nombre = "Chandler";
personaje.serie = "Friends";
personaje.comportamiento = function () {console.log("Hace chistes " + personaje.nombre);}

console.log(personaje);
personaje.comportamiento();

//Factory funciton
function creandoUsuarios(n, a, e, t, s){
    return {
        nombre: n,
        apellido: a,
        edad: e,
        trabajo: t,
        soltero: s,
    };
}

var user1 = creandoUsuarios("Pepe", "Mujica", 69, "Ladron", "Si")
var user2 = creandoUsuarios("Juan", "Mujica", 59, "Ladron", "Si")
console.log(user1, user2)

/* se agrego propiedades al objeto como vimos con los areglos: */
/* const arreglo = [0, 1, 2];
arreglo[3] = 3; */

/* for (var index = 0; index < personas.length; index++){
    var persona = personas[index];
    console.log(persona)
 } */  /* habia hecho mas cosas el profe */

// Temas que no dimos en clases pero si las dió HolaMundo - 3:18:58 / 3:58:55:
/* 
Funciones constructoras
Atajos constructores
Funciones
Function
Valor y referencia
Listar propiedades
*/

 //Funciones
 console.log("")
 console.log("Funciones:")
 function multiplicar(num1, num2){
    console.log(arguments);
    console.log(arguments[0]*arguments[1]); /* esto nos va a servir para pasar muchos elementos como un "arreglo" NO SE USA ESTA FORMA */
    return num1 * num2;
    /* otra forma: */
 } /* dijero que "una funcion siempre puede devolver algo excepto si usas void" */

 var rta = multiplicar(2, 6)
console.log(rta) /* no entiendo en que caso devuele NaN */

// Ejemplo practico viendo arreglos y funciones
var frutas2 = []
function guardarFruta(fruta) {
    frutas2.push(fruta)
}
guardarFruta("Manzana")
guardarFruta("Pera")
console.log(frutas2)

// Metodos muy usados en arreglos
// forEach - a cada elemento lo voy a pasar por una funcion, los argumentos de dicah funcion seran especificos para forEach (elemento, indice, arreglo_al_que_le_hacemos_el_forEach). 
console.log('forEach:');
console.log(frutas2);
frutas2.forEach(function(fruta, index, array){
    console.log("Elemento: " + fruta);
    console.log("Indice: " + index);
    console.log("Arreglo ingresado: " + array);
    console.log("");
})

// map - parecido al forEach pero con la diferencia que este tiene la capacidad de retornar lo que hagamos en la función.
console.log('map: ');
var nuevoArreglo = frutas2.map(function(fruta, index, array){
    return fruta + "!!!";
});
console.log(frutas2)
console.log(nuevoArreglo)

// metodo some
// Some es muy parecido al metodo every pero every requiere que TODO sea true para regresar true, some por otro lado con que un solo valor cumpla la condicion entonces regresa true.
// ejercicios para entenderlo

console.log('some:');
var palabras = ['tomate', 'año', 'remo', 'tentaculo', 'perro', 'liebre'];
console.log(palabras);

console.log('Alguna de estas palabras tiene mas de 6 caracteres?');
var rta1 = palabras.some(function(palabra){
    return palabra.length > 6; 
});
console.log(rta1);

console.log('Alguna de estas palabras comienza con b?');
var rta2 = palabras.some(function(palabra){
    return palabra[0] === 'b'; 
});
console.log(rta2);

console.log('Alguna de estas palabras contiene mate?');
// Si es que se refiere a que las 4 letras estén en alguna palabra
var rta3 = palabras.some(function(palabra){
    return palabra === 'mate'; 
});
console.log(rta3);
// Si es que se refiere a que la palabra mate esté en el arreglo
var rta3 = palabras.some(function(palabra){
    return palabra.includes('mate'); 
});
console.log(rta3);

// Lo que hicieron en clases
console.log('');
console.log("Ejercicio en clases:");
console.log("Arreglo: " + frutas);
var existe = frutas.some(function(fruta) {
    return fruta === "Manzana Verde"
}); 
console.log("¿La manzana verde forma parte del arreglo?: " + existe);

/* Ejercicio 02 - HolaMundo */
console.log('');
console.log('Ejercicio 02 - HolaMundo:')
function resolucion(w, h){
    if (w >= 7680 && h >= 4320){
        return "8K";
    } else if (w >= 3840 && h >= 2160){
        return "4k";
    } else if (w >= 2560 && h >= 1440){
        return "WQHD";
    } else if (w >= 1920 && h >= 1080){
        return "FHD";
    } else if (w >= 1280 && h >= 720){
        return "HD";
    } else {
        return "no pertenece a ninguna categoria";
    }
};
var ancho = 128;
var alto = 72;
var pantalla = resolucion(ancho, alto);
console.log("Resolucion " + pantalla);