// /routes/user.js
import express from 'express'
import userController from '../controllers/userController.js'

const router = express.Router();

/* 
- Cómo Funciona .Router()
    Crear un Enrutador:
    Utilizas Router() para crear una nueva instancia de enrutador. Esta instancia es independiente de la aplicación principal y se comporta como una mini aplicación Express.

    Definir Rutas en el Enrutador:
    Definirás rutas en esta instancia de enrutador usando métodos como .get(), .post(), .put(), .delete(), etc. Estas rutas serán manejadas de forma similar a cómo lo haces en la aplicación principal.

    Montar el Enrutador en la Aplicación Principal:
    Una vez que hayas definido todas las rutas en el enrutador, puedes montarlo en la aplicación principal usando app.use(). Esto conecta el enrutador a la aplicación principal en una ruta específica.
*/

// Definir la ruta para obtener todos los usuarios
router.get("/", userController.get);

// Definir la ruta para obtener un usuario por ID
router.get("/:id", userController.getById);

// Definir la ruta para agregar un nuevo usuario
router.post("/", userController.add);

export default router;
