/* 
 Es importante aclarar cómo funcionan las importaciones en JavaScript/TypeScript para que quede claro por qué app.ts ya se ejecuta en el momento en que se importa, sin esperar a que se use app.listen.
*/
import app from "./app";  
import connectDb from "./config/connectDb"; /* conectar el backend con una base de datos */
import { PORT } from "./config/env"; /* - `PORT` es una variable de entorno que define en qué puerto se ejecutará el servidor. */
// con {} importo los elementos de las const exportadas, sin { } importo todo el elemento exportado con export default

const startServer = async () => { 
  try {
    await connectDb(); // Se espera que esta promesa se resuelva

    // Si connectDb() se resuelve correctamente:
    app.listen(PORT, () => { /* Inicia el servidor HTTP en el puerto especificado por PORT. Se queda "esperando" solicitudes entrantes desde un cliente (como un navegador o una aplicación móvil). */
      console.log(`Servidor corriento en el puerto ${PORT}`); /* *preguntar si están bien estos console.log() */
    });
  } catch (error) {
    console.log("Error al arrancar el servidor");
  }
};

startServer();
