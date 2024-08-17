const express = require('express');

const app = express();

// Ruta 1:
app.get('/',function(req, res){
    /* res.end('Hola pa'); */ // Usar esto no es correcto, no da mucha informacion, en cambio express proporciona un metodo exclusivo que es send
    res.send('Hola pa'); // Cuando yo le mando un texto le indico al navegador que es un texto, es decir le doy mas informacion
})

// Ruta 2:
app.get('/about',function(req, res){
    res.send('Seccion About');
})

// Si la ruta no existe esta es la forma de mostar algo:
app.use(function(req, res){
    res.send('No se encontró tu página!');
});

app.listen(3000, function(){
    console.log('Server on port 3000');
});