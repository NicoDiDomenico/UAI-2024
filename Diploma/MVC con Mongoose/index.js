import express from 'express'; // Importación moderna de ES6
import mongoose from 'mongoose';
import userRoute from './routes/user.js';

const app = express(); 

const mongoURL = 'mongodb+srv://Nico:8845@nico-bd.qzxau.mongodb.net/Nico-BD?retryWrites=true&w=majority&appName=Nico-BD'; // URL de conexión a MongoDB

// Función para iniciar el servidor y conectarse a MongoDB
async function startServer() {
    try {
        await mongoose.connect(mongoURL, {
            useNewUrlParser: true,
            useUnifiedTopology: true
        });
        console.log('Connected to MongoDB');

        // Iniciar el servidor en el puerto 3005
        app.listen(3005, () => {
            console.log("Server running on port 3005");
        });

    } catch (error) {
        console.error('Error connecting to MongoDB:', error);
    }
}

startServer();  

// Middleware para manejar JSON en el cuerpo de las solicitudes
app.use(express.json()); // Se aplica globalmente a todas las rutas y controladores definidos en tu aplicación Express

// Montar el router en la ruta '/user'
app.use('/user', userRoute);
