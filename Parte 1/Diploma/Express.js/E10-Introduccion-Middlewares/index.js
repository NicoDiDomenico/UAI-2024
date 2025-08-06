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
  if (req.query.login === 'nico@nicoweb.com'){
    next();
  } else{
    res.send('No autorizado.');
  }
});
  
app.get('/dashboard', function(req, res){
  console.log('Ahora si se va a mostrar el mensaje en el navegador');
  res.send('Dashboard page');
});  

//// CONCLUSIÓN: El orden importa!!!

app.listen(3000, function(){
    console.log('Server on port 3000'); 
});
