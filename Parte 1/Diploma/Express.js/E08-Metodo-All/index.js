// all = GET, POST, PUT, DELETE, etc.

const express = require('express');

const app = express();

// El uso de app.all() es útil cuando deseas que una ruta responda de la misma manera independientemente del método HTTP. Es menos común que app.get(), app.post(), etc., que se utilizan para manejar solicitudes específicas según el método HTTP.
app.all('/info', (req, res) => {
    res.send('server info')
  })
  
app.listen(3000, function(){
    console.log('Server on port 3000');  // Muestra en la consola que el servidor está corriendo en el puerto 3000
});
