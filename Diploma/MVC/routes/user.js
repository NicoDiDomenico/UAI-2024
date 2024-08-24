// user.js
const express = require("express");
const router = express.Router();
const userController = require('../controllers/userController.js');

// Definir la ruta para obtener todos los usuarios
router.get("/", userController.get);

// Definir la ruta para obtener un usuario por ID
router.get("/:id", userController.getById);

// Definir la ruta para agregar un nuevo usuario
router.post("/", userController.add);

module.exports = router;
