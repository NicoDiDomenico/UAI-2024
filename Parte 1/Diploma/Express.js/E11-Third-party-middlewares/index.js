////// Third-party-middlewares - middlewares preconstruidos
//// Antes vimos como crearlos. ahora hay que aprender que tambien podemos usar middlewares ya creados, ya que al fin y al cabo son funciones.
// instalaremos middlewares pre-construidos a través de npm --> 2:00:00 / 4:00:29

const express = require('express');
const morgan = require('morgan'); // Acá importamos el middleware precontruido a partir de npm i install

app.use(express.json()); // Sin saber ya habiamos usado un middleware de este tipo!
/* 
Recordar: 
Propósito: Este middleware incorporado en Express.js se utiliza para analizar (parsear) las solicitudes entrantes con un cuerpo en formato JSON.

Funcionalidad: Cuando incluyes app.use(express.json()); en tu código, estás diciéndole a Express que debe procesar cualquier cuerpo de la solicitud que tenga un contenido en formato JSON. Esto convierte ese JSON en un objeto JavaScript accesible a través de req.body.

Uso: Una vez que express.json() ha analizado el JSON en la solicitud, req.body contiene un objeto JavaScript con los datos enviados por el cliente. Al usar console.log(req.body);, puedes ver estos datos en la consola para depuración o monitoreo.

*/

const app = express();

// Al mover las rutas profile y about arriba evito que pasen primero por el middleware, ya que este fue pensado solo para la ruta dashboard.
app.get('/profile', function(req, res){
  res.send('profile page');
});
  
app.all('/about', function(req, res){
  res.send('about page');
});

// Middlewere Logger de Morgan - Nos va a permitir ver por consola las peticiones que nos van llegando
app.use(morgan('dev'));

// Middlewere isAuthenticated
app.use(function(req, res, next){ 
  if (req.query.login === 'nico@nicoweb.com'){
    next();
  } else{
    res.send('No autorizado.');
  }
});

app.get('/dashboard', (req, res) => {
  res.send('Dashboard page');
});  

//// CONCLUSIÓN: El orden importa!!!

app.listen(3000, function(){
  console.log('Server on port 3000'); 
});
