// NPM - npm (Node Package Manager) es una herramienta fundamental en el ecosistema de Node.js que se utiliza para gestionar paquetes de software. Un paquete es un módulo o una biblioteca de código que otros desarrolladores han creado y publicado para que pueda ser reutilizado en diferentes proyectos.

// Para instalar alguna biblioteca hay que ir a https://www.npmjs.com/ buscar la biblioteca de preferencia e instalarla colocando 'npm install <biblioteca>' en el cmd en el path que se quiera instalar.

const http = require('http');
const colors = require('colors'); // Esta es la biblioteca/modulo que instalé --> https://www.npmjs.com/package/colors

const handlerServer = function handlerServer(req,res){ 
    res.writeHead(200, {'content-type': 'text/html'}); 
    res.write('<h1>Hola mundo desde node.js</h1>');
    res.end;
};
const server = http.createServer(handlerServer);

server.listen(3000, function(){
    console.log('Server on en puerto 3000'.rainbow); /* Agregando color a la consola */
});