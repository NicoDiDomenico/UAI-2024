// Es importante entender que cuando estamos escribiendo una aplicacion real no es necesario escribirlo todo desde 0, existen los Frameworks como express que simplifica la creación y gestión de aplicaciones web y APIs. Proporciona una serie de herramientas y características que hacen más fácil manejar rutas, peticiones HTTP, respuestas, middlewares, y más, todo dentro de una estructura organizada.

const express = require('express');
const colors = require('colors');

const server = express(); // Crea el servidor

server.get('/', function(req, res){
    res.send('<h1>Hola mundo con express y node</h1>');
    res.end(); // No es necesario ponerlo, res.send lo hace automaticamente
});

server.listen(3000, function(){
    console.log('Server on port 3000'.green);
});

// Recordar que esto es lo mismo:
/* express().get('/', function(req, res){
    res.send('<h1>Hola mundo con express y node</h1>');
    res.end(); // No es necesario ponerlo, res.send lo hace automaticamente
}).listen(3000, function(){
    console.log('Server on port 3000'.green);
}); */