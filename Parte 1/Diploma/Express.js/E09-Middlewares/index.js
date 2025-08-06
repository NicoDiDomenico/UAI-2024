const express = require("express");

const app = express();

// Recordar que .use es una funcion que permite a express ejecutar otras funciones
// Notar que mi middleware es una ruta pero sin nombre, esto hace que primero tenga que pasar por acá y luego seguir con las rutas con nombre.
// next es una funcion que se pasa como parámetro para indicar que cuando la ruta actual se termine de ejecutar pase a la siguiente, y asi evitar que la pagina se quede esperando la respuesta del servidor.
app.use(function(req, res, next){ /* next es un concepto específico de Express.js. No es parte de JavaScript puro ni de Node.js en general, sino que es una función proporcionada por el marco de trabajo Express para manejar la cadena de middleware. */
  console.log('Primero va a pasar por aqui');
  console.log('Route: ' + req.url + ';' + ' Metodo: ' + req.method);
  next();
});

app.get('/profile', function(req, res){
  res.send('profile page');
});

app.all('/about', function(req, res){
  res.send('about page');
});

app.listen(3000, function(){
    console.log('Server on port 3000');  // Muestra en la consola que el servidor está corriendo en el puerto 3000
});
