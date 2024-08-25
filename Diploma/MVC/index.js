// index.js
import express from 'express'; // Forma moderna
import userRoute from './routes/user.js';
import mongoose from 'mongoose';

const app = express(); 

const mongoURL = 'mongodb+srv://Nico:8845@nico-bd.qzxau.mongodb.net/Nico-BD?retryWrites=true&w=majority&appName=Nico-BD'; // Esto me hizo agregarlo el del curso pero no lo terminé de implementar/usar

async function startServer() {
    try {
      await mongoose.connect(mongoURL);
      console.log('Connected to MongoDB');
      // Aquí puedes agregar el código para iniciar tu servidor
    } catch (error) {
      console.error('Error connecting to MongoDB:', error);
    }
  }
  
  startServer();  

// Middleware para manejar JSON en el cuerpo de las solicitudes
app.use(express.json()); // Se aplica globalmente a todas las rutas y controladores definidos en tu aplicación Express

// Montar el router en la ruta '/user'
app.use('/user', userRoute);

// Iniciar el servidor en el puerto 3005
app.listen(3005, () => {
    console.log("Server running on port 3005");
});