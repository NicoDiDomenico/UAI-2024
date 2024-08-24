// user.js
import express from 'express'
import userController from '../controllers/userController.js'

const router = express.Router();

// Definir la ruta para obtener todos los usuarios
router.get("/", userController.get);

// Definir la ruta para obtener un usuario por ID
router.get("/:id", userController.getById);

// Definir la ruta para agregar un nuevo usuario
router.post("/", userController.add);

export default router;
