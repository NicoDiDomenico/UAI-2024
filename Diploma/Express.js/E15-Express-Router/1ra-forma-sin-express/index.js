//// API REST

// Módulos
const express = require('express');
const morgan = require('morgan');
/* voy a usar nodemon --> para hacerlo andar poner: npm run dev */
const app = express();
const HomeRoutes = require('./routes/home');
const ProductsRoutes = require('./routes/products');

// Settings
app.set('appName', 'Express Course');
app.set('port', 4000);

// Midlewares
/* app.use(morgan('dev')); */ // Logger
app.use(express.json()); 

// Usar las funciones importadas
HomeRoutes(app)
ProductsRoutes(app);

app.listen(app.get('port'), function(){
    console.log(`Server ${app.get('appName')} on port ${app.get('port')}`);
});
