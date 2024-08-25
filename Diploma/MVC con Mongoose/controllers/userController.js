import User from '../models/userModel.js';

export default {
    // Controlador para obtener todos los usuarios
    get: async (req, res) => {
        try {
            const users = await User.getAllUsers();
            res.json(users);
        } catch (error) {
            res.status(500).json({ message: 'Error al obtener los usuarios' });
        }
    },

    // Controlador para obtener un usuario por ID
    getById: async (req, res) => {
        try {
            const userId = req.params.id;
            const user = await User.getUserById(userId);

            if (user) {
                res.json(user);
            } else {
                res.status(404).json({ message: 'User not found' });
            }
        } catch (error) {
            res.status(500).json({ message: 'Error al obtener el usuario' });
        }
    },

    // Controlador para agregar un nuevo usuario
    add: async (req, res) => {
        try {
            const newUser = new User(req.body.name);
            const addedUser = await newUser.save();
            res.status(201).json(addedUser);
        } catch (error) {
            res.status(400).json({ message: 'Error al agregar el usuario', error: error.message });
        }
    },

    // Controlador para actualizar un usuario
    update: async (req, res) => {
        try {
            const updatedUser = await User.updateById(req.params.id, req.body);
            if (updatedUser) {
                res.json(updatedUser);
            } else {
                res.status(404).json({ message: 'User not found' });
            }
        } catch (error) {
            res.status(400).json({ message: 'Error al actualizar el usuario', error: error.message });
        }
    },

    // Controlador para eliminar un usuario
    remove: async (req, res) => {
        try {
            const deletedUser = await User.deleteById(req.params.id);
            if (deletedUser) {
                res.json({ message: 'User deleted successfully' });
            } else {
                res.status(404).json({ message: 'User not found' });
            }
        } catch (error) {
            res.status(500).json({ message: 'Error al eliminar el usuario' });
        }
    }
};
