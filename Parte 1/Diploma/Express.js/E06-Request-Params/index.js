/* 
Parámetros de ruta (Route Parameters)
    Ejemplo: /name/:nombre/age/:age
    Uso: Los parámetros de ruta son parte de la estructura de la URL y se definen directamente dentro de la ruta, usando : para indicar que son dinámicos. Estos parámetros son obligatorios y forman parte integral de la ruta.
    Contexto: Son utilizados para definir rutas que dependen de un recurso específico, como un ID, nombre, o cualquier otra información que sea esencial para acceder a un recurso particular.
*/

const express = require('express');

const app = express();

// Request Params - URL que contiene parámetros, es decir datos tipeados en la misma
app.get('/nombre/:name', function(req, res){
    console.log(req.params);  // Muestra todos los parámetros capturados en la URL
    console.log(typeof(req.params.name));  // Muestra el tipo de dato del parámetro 'name'
    res.send('Hola ' + req.params.name + '!');  // Responde con un saludo que incluye el nombre pasado en la URL
});

// Request Params - Suma de dos números proporcionados en la URL como parámetros
app.get('/add/:x/:y', function(req, res){
    console.log(req.params.x);  // Muestra el valor del parámetro 'x'
    console.log(req.params.y);  // Muestra el valor del parámetro 'y'
  
    const result = parseInt(req.params.x) + parseInt(req.params.y);  // Suma los valores de 'x' e 'y' como enteros
    console.log(result);  // Muestra el resultado de la suma
    res.send('Result: ' + result);  // Responde con el resultado de la suma
});

// Request Params - Validación de un nombre de usuario para mostrar una foto si coincide
app.get('/users/:username/photo', function(req, res){
    if (req.params.username === "fazt") {  // Verifica si el nombre de usuario es 'fazt'
      res.sendFile('./JavaScript-logo.png', {
        root: __dirname  // Envía el archivo 'javascript.png' si el nombre coincide
      });
    } else {
      res.send('el usuario no tiene acceso');  // Responde con un mensaje de acceso denegado si no coincide
    }
}); 

// Request Params - Mostrar el nombre y la edad de un usuario proporcionados en la URL
app.get('/name/:nombre/age/:age', function(req, res){
    console.log(req.params);  // Muestra todos los parámetros capturados en la URL
    res.send('El usuario ' + req.params.nombre + ' tiene ' + req.params.age + ' años');  // Responde con un mensaje que incluye el nombre y la edad
});  

app.listen(3000, function(){
    console.log('Server on port 3000');  // Muestra en la consola que el servidor está corriendo en el puerto 3000
});
