"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
/*
Importa el framework Express, que es la base del servidor.
Se utiliza para manejar solicitudes HTTP, definir rutas y middlewares, y gestionar respuestas.
*/
require("dotenv/config");
/*
dotenv es una biblioteca que carga las variables de entorno definidas en un archivo .env y las coloca en el objeto global process.env.
Es equivalente a escribir:
  import dotenv from "dotenv";
  dotenv.config();
*/
const cors_1 = __importDefault(require("cors")); //  biblioteca en Node.js que permite que tu servidor acepte solicitudes desde otros dominios o aplicaciones. (Cross-Origin Resource Sharing)
const routes_1 = require("./routes");
/*
Importa dos routers para manejar las rutas relacionadas con usuarios y tareas.
Propósito: Organizar las rutas del servidor de manera modular.
*/
const errorHandler_middleware_1 = require("./middlewares/errorHandler.middleware");
/*
Importa un middleware personalizado para manejar errores de manera centralizada.
Propósito: Capturar errores de las rutas o middlewares y enviar respuestas claras al cliente.
*/
// Middleware
const app = (0, express_1.default)(); // Crea una instancia de Express (app), que será el núcleo de tu servidor. (Sirve para cualquier lógica que maneje solicitudes HTTP) => Crea una instancia de un servidor y voy definiendo metodos como los middlewares
app.use(express_1.default.json()); // Middleware que convierte el cuerpo de las solicitudes con formato JSON en un objeto JavaScript accesible en req.body.
app.use((0, cors_1.default)({
    origin: "*", // Acá en vez de * podria haber puesto la URL de mi frontend.
}));
/*
¿Qué hace?
  Configura el middleware CORS para que tu servidor acepte solicitudes de cualquier origen (origin: "*") por defecto.
Propósito: Facilitar la comunicación entre el frontend y el backend durante el desarrollo.
Nota: En producción, deberías especificar dominios de origen permitidos para mayor seguridad.
*/
//Aca agregar las rutas - Rutas base 
app.use("/api/users", routes_1.userRouter); // Maneja todas las solicitudes relacionadas con usuarios.
app.use("/api/todos", routes_1.todoRouter); // Maneja todas las solicitudes relacionadas con tareas.
//Aca agregar el middleware para controlar errores
/* Cuando se use throw en alguna parte del código, se está generando un error que es capturado automáticamente por Express. Tú no necesitas pasar explícitamente los otros parámetros (req, res, next), porque Express lo hace por ti. */
app.use(errorHandler_middleware_1.errorHandler); //Agrega un middleware global para capturar errores y responder al cliente de manera controlada.
/* El middleware errorHandler se ejecutará si ocurre un error en alguna de las rutas o middlewares previos. Esto incluye errores lanzados manualmente usando throw o errores generados por cualquier middleware (como errores de validación, base de datos, etc.). */
exports.default = app;
