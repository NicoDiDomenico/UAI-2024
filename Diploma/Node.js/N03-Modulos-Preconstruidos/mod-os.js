//// Módulos pre-construidos 
/* En Node.js, los módulos preconstruidos (también conocidos como "módulos integrados" o "módulos del núcleo") son bibliotecas que vienen incluidas con la instalación de Node.js y proporcionan funcionalidades esenciales para desarrollar aplicaciones de servidor y otras aplicaciones JavaScript. Estos módulos te permiten realizar tareas comunes como trabajar con archivos, manejar solicitudes HTTP, operar con el sistema de archivos, y mucho más, sin necesidad de instalar dependencias adicionales. */

// Por ejemplos vamos a usar el módulo OS (sobre Sistema Operativo) - https://nodejs.org/docs/latest/api/os.html
/* El módulo os proporciona información sobre el sistema operativo, como la memoria disponible, el número de CPUs, el tipo de sistema operativo, y más. */
const os = require('os');

console.log(os.platform());
console.log(os.release());
console.log('Memoria libre: ' + os.freemem() + ' Bytes');
console.log('Memoria total: ' + os.totalmem() + ' Bytes');
