//// Crear servidor puramente con nodejs vs usar framework express
// express:
const express = require('express');
const app = express();

app.get('/', function(req,res){
    res.sendFile('./static/index.html',{ // El objeto { root: __dirname } le dice a Express que la ruta relativa debe resolverse desde el directorio actual (__dirname).
        root: __dirname
    });
}); // notar como express abstrae la funcionalidad, con una sola funcion envio el archivo estatico como response

app.listen(3000, function(){
    console.log('Server running on port 3000 with Express');
});