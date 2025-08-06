// /models/userModel.js
/* 
Al usar static en tu modelo User, mantienes una estructura clara y sencilla para manipular un estado compartido (el arreglo users). Esto es útil cuando deseas trabajar con datos compartidos entre todas las instancias de una clase sin crear múltiples instancias de esa clase para acceder a los mismos datos.
*/
class User {
    // Propiedad estática para simular una base de datos en memoria
    static users = [
        { id: 1, name: 'John Doe' },
        { id: 2, name: 'Jane Doe' }
    ];

    constructor(name) {
        this.id = User.users.length + 1; // Asigna un ID basado en la longitud actual del arreglo
        this.name = name;
    }

    // Método para obtener todos los usuarios
    static getAllUsers() {
        return User.users;
    }

    // Método para obtener un usuario por ID
    static getUserById(id) {
        return User.users.find(user => user.id === id);
    }

    // Método para agregar un nuevo usuario
    static addUser(user) {
        User.users.push(user);
        return user;
    }
}

// Exportar la clase User
export default User;

/* 
El loco del curso hizo esto como el models:https://www.youtube.com/watch?v=05k_fekF8nY&t=463s
import mongoose from "mongoose"

const hotelSchema = mongoose.Schema({
    cat: String,
    title: String,
    image: String,
    rating: Number,
    location: {
        type: {
            type: String,
            enum: ['Point'],
            required: true,
        },
        coordinates: {
            type: [Number],
            required: true,
        }
    },
    address: String,
    description: String,
})

export default mongoose.model("vendorHotels", hotelSchema)

*/