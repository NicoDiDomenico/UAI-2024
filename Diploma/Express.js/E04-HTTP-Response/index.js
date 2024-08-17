// Ahora vamos a ver Responde(res) . lo que puede responder el servidor
/* Hasta ahora solo creamos rutas que pueden devolver texto, pero tambien se puede devolver archivos html, videos,  */

const express = require('express');

const app = express();

// Res - texto
app.get('/', function(req, res){
    res.send('Hola Pa');    
});

// Res - imagen (en realidad puede ser cualquier archivo como hicimos con .html)
app.get('/miarchivo', function(req, res){
    res.sendFile('./JavaScript-logo.png', {root: __dirname});    
});

// Res - JSON
app.get('/user', function(req, res){
    res.json({'name': 'Nico'}); // Lo normal va a ser traer esto de la base de datos
});

// Res - Codigo de Estado
app.get('/isAlive', function(req, res){
    res.sendStatus(204); // Notar como no devuelve ningun contenido que cambie mi pagina, esto es porque solo envio el codigo de estado y ningun archivo o texto (por eso el X04)
});


app.listen(3000, function(){
    console.log('Server on port 3000');
});