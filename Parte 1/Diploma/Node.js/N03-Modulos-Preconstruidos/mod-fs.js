// Módulo File system - https://nodejs.org/docs/latest/api/fs.html
/* 
Este módulo nos permite trabajar con los archivos del SO, esto será importante porque al momento de crear servidores o programas necesitaremos interactuar con los archivos que tenemos en el sistema y es este módulo el que no permite interactuar con ellos.
*/
const fs = require('fs');

// Creando archivo nuevo
fs.writeFile('./texto.txt', 'linea uno', function(error){ /* este tercer prámetros es un callback, dado que .wachFile es un metodo asíncrono */
    if (error){
        console.log(error);
    } else {
        console.log('Archivo creado.');
    }
});

console.log('Última linea de código.');

/* Leer archivos */
fs.readFile('./texto.txt', function(error, datos){
    if (error){
        console.log(error);
    } else {
        console.log(datos.toString());
    }
})