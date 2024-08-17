//// Antes vimos como crearlos. ahora hay que aprender que tambien podemos usar middlewares ya creados, ya que al fin y al cabo son funciones.
// instalaremos middlewares pre-construidos a través de npm --> 2:00:00 / 4:00:29

const express = require("express");

const app = express();

// Al mover las rutas profile y about arriba evito que pasen primero por el middleware, ya que este fue pensado solo para la ruta dashboard.
app.get('/profile', function(req, res){
    res.send('profile page');
});
  
app.all('/about', function(req, res){
    res.send('about page');
});

// Middlewere Logger - Muestro la ruta y el metodo nomás
app.use(function(req, res, next){ 
  console.log('Route: ' + req.url + ';' + ' Metodo: ' + req.method);
  next();
});

// Middlewere isAuthenticated
app.use(function(req, res, next){ 
    console.log('Route: ' + req.url + ';' + ' Metodo: ' + req.method);
    next();
  });
  
app.get('/dashboard', (req, res) => {
    res.send('Dashboard page');
  });  

//// CONCLUSIÓN: El orden importa!!!

app.listen(3000, function(){
    console.log('Server on port 3000'); 
});
