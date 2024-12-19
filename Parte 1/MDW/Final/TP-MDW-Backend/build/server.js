"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
/*
 Es importante aclarar cómo funcionan las importaciones en JavaScript/TypeScript para que quede claro por qué app.ts ya se ejecuta en el momento en que se importa, sin esperar a que se use app.listen.
*/
const app_1 = __importDefault(require("./app")); /* acá están las rutas, middlewares y otras funcionalidades principales */
const connectDb_1 = __importDefault(require("./config/connectDb")); /* conectar el backend con una base de datos */
const env_1 = require("./config/env"); /* - `PORT` es una variable de entorno que define en qué puerto se ejecutará el servidor. */
// con {} importo los elementos de las const exportadas, sin { } importo todo el elemento exportado con export default
const startServer = () => __awaiter(void 0, void 0, void 0, function* () {
    try {
        yield (0, connectDb_1.default)(); // Se espera que esta promesa se resuelva
        // Si connectDb() se resuelve correctamente:
        app_1.default.listen(env_1.PORT, () => {
            console.log(`Servidor corriento en el puerto ${env_1.PORT}`); /* *preguntar si están bien estos console.log() */
        });
    }
    catch (error) {
        console.log("Error al arrancar el servidor");
    }
});
startServer();
