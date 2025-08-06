// Ahora vamos a aprender como el cliente nos va a enviar datos a través de objetos json
const express = require('express');

const app = express();

// Usas app.use(express.text()) para asegurarte de que los datos enviados en el cuerpo de la solicitud sean correctamente analizados(parsear)  y estén disponibles en req.body. Sin este middleware, Express no puede interpretar los datos del cuerpo, lo que resulta en req.body siendo undefined.
app.use(express.text()); // si en el body hay text
app.use(express.json()); // si en el body hay json
app.use(express.urlencoded({extended: false})); // si en el body hay un form

/* ¡Si los app.use van despues de app.get no se podran procesar antes! */

// Notar que los .get se muestran en el navegador pero los .post no (a través de res.send)
app.get('/', function(req, res){
    res.send('Hola :)');    
});

// Cómo el cliente va a enviar datos tendremos que usar una ruta post
app.post('/user', function(req, res){
    console.log(req.body); // Para ver que hay en el body primero hay que darle algo a través de thunder client 
    res.send('Nuevo usuario creado!');    
});

app.listen(3000, function(){
    console.log('Server on port 3000');
});