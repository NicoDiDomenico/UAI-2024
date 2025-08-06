import mongoose from 'mongoose';

// Definir el esquema de Mongoose para User
const userSchema = new mongoose.Schema({
    name: { type: String, required: true }
});

// Crear el modelo de Mongoose para User
const UserModel = mongoose.model('User', userSchema);

export default UserModel;
