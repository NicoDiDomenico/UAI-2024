import mongoose from "mongoose"; // Importa la biblioteca Mongoose, que es una herramienta para trabajar con bases de datos MongoDB en Node.js.
import { MONGODB_URI } from "./env"; // Importa la constante MONGODB_URI, que contiene la dirección (URI) de tu base de datos, que es dada por MongoDB Atlas (la versión en la nube de MongoDB), la URL la obtienes directamente desde su plataform.

const connectDb = async () => {
  try {
    await mongoose.connect(MONGODB_URI); // método connect de Mongoose para establecer la conexión con la base de datos.  Si estás usando una base de datos local de MongoDB, también necesitas proporcionar un URI (URL) al método .connect(). Sin embargo, la URL será diferente, ya que apunta a tu máquina local en lugar de un servidor en la nube.
    console.log("Conexion a la DB exitosa");
  } catch (error) {
    console.log("Error conectando a la DB");
  }
};

export default connectDb;
