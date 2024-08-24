// index.js
/* const express = require("express"); */ // Forma antigua
import express from 'express' // Forma moderna
import userRoute from './routes/user.js'

const app = express(); 

// Middleware para manejar JSON en el cuerpo de las solicitudes
app.use(express.json()); // al incluir app.use(express.json()) en index.js, el middleware express.json() se aplica globalmente a todas las rutas y controladores definidos en tu aplicación Express. Esto significa que todas las solicitudes entrantes que contengan cuerpos JSON serán parseadas automáticamente y estarán disponibles en req.body en cualquier archivo de tu aplicación.

// Montar el router en la ruta '/user'
app.use('/user', userRoute);

// Iniciar el servidor en el puerto 3005
app.listen(3005, () => {
    console.log("Server running on port 3005");
});

