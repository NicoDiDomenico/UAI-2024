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
const mongoose_1 = __importDefault(require("mongoose")); // Importa la biblioteca Mongoose, que es una herramienta para trabajar con bases de datos MongoDB en Node.js.
const env_1 = require("./env"); // Importa la constante MONGODB_URI, que contiene la dirección (URI) de tu base de datos, que es dada por MongoDB Atlas (la versión en la nube de MongoDB), la URL la obtienes directamente desde su plataform.
const connectDb = () => __awaiter(void 0, void 0, void 0, function* () {
    try {
        yield mongoose_1.default.connect(env_1.MONGODB_URI); // método connect de Mongoose para establecer la conexión con la base de datos.  Si estás usando una base de datos local de MongoDB, también necesitas proporcionar un URI (URL) al método .connect(). Sin embargo, la URL será diferente, ya que apunta a tu máquina local en lugar de un servidor en la nube.
        console.log("Conexion a la DB exitosa");
    }
    catch (error) {
        console.log("Error conectando a la DB");
    }
});
exports.default = connectDb;
