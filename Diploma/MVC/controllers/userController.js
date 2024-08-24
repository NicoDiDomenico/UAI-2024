// userController.js

// Importar la clase User
const User = require('../models/userModel.js');

module.exports = {
    // Controlador para obtener todos los usuarios
    get: (req, res) => {
        const users = User.getAllUsers();
        res.json(users); 
    },
    
    // Controlador para obtener un usuario por ID
    getById: (req, res) => {
        const userId = parseInt(req.params.id);
        const user = User.getUserById(userId);

        if (user) {
            res.json(user);
        } else {
            res.status(404).json({ message: 'User not found' });
        }
    },
    
    // Controlador para agregar un nuevo usuario
    add: (req, res) => {
        const newUser = new User(req.body.name); // Crear una instancia de User
        const addedUser = User.addUser(newUser);
        res.status(201).json(addedUser);
    }
};
