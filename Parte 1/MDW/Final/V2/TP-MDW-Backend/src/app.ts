import express from "express";
/* 
Importa el framework Express, que es la base del servidor.
Se utiliza para manejar solicitudes HTTP, definir rutas y middlewares, y gestionar respuestas.
*/
import "dotenv/config"
/* 
dotenv es una biblioteca que carga las variables de entorno definidas en un archivo .env y las coloca en el objeto global process.env.
Es equivalente a escribir: 
  import dotenv from "dotenv";
  dotenv.config();
*/  
import cors from "cors"; //  biblioteca en Node.js que permite que tu servidor acepte solicitudes desde otros dominios o aplicaciones. (Cross-Origin Resource Sharing)
import { userRouter, todoRouter } from "./routes";
/* 
Importa dos routers para manejar las rutas relacionadas con usuarios y tareas.
Propósito: Organizar las rutas del servidor de manera modular.
*/
import { errorHandler } from "./middlewares/errorHandler.middleware";
/* 
Importa un middleware personalizado para manejar errores de manera centralizada.
Propósito: Capturar errores de las rutas o middlewares y enviar respuestas claras al cliente.
*/

// Middleware
const app = express(); // Crea una instancia de Express (app), que será el núcleo de tu servidor. (Sirve para cualquier lógica que maneje solicitudes HTTP) => Crea una instancia de un servidor y voy definiendo metodos como los middlewares

app.use(express.json()); // Middleware que convierte el cuerpo de las solicitudes con formato JSON en un objeto JavaScript accesible en req.body.

app.use(
  cors({
    origin: "*", // Acá en vez de * podria haber puesto la URL de mi frontend.
  })
);
/* 
¿Qué hace?
  Configura el middleware CORS para que tu servidor acepte solicitudes de cualquier origen (origin: "*") por defecto.
Propósito: Facilitar la comunicación entre el frontend y el backend durante el desarrollo.
Nota: En producción, deberías especificar dominios de origen permitidos para mayor seguridad.
*/

//Aca agregar las rutas - Rutas base 

app.use("/api/users", userRouter); // Maneja todas las solicitudes relacionadas con usuarios.
app.use("/api/todos", todoRouter); // Maneja todas las solicitudes relacionadas con tareas.

//Aca agregar el middleware para controlar errores
/* Cuando se use throw en alguna parte del código, se está generando un error que es capturado automáticamente por Express. Tú no necesitas pasar explícitamente los otros parámetros (req, res, next), porque Express lo hace por ti. */
app.use(errorHandler);

export default app;