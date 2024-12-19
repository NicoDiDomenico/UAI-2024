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
exports.getUser = exports.login = exports.register = void 0;
const user_model_1 = require("../models/user.model"); // Importa el modelo User, que probablemente define la estructura y comportamiento de los datos del usuario, incluyendo métodos como findOne, save y comparePassword.
const jsonwebtoken_1 = __importDefault(require("jsonwebtoken")); // Generar tokens de autenticación basados en JWT para identificar usuarios autenticados. Esta forma en ESM/ES6 reemplaza la forma tradicional en node.js que es: const jwt = require("jsonwebtoken"); 
const errorHandler_middleware_1 = require("../middlewares/errorHandler.middleware"); // Facilitar el manejo de errores personalizados, como los de validación o autenticación.
const env_1 = require("../config/env"); // Importa el secreto que se usa para firmar los tokens JWT. Este secreto se encuentra en las variables de entorno.
const register = (/* Este controlador maneja las solicitudes POST /register para registrar un nuevo usuario. */ req, /* parámetros con su tipo (objeto) */ res, next) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const { email, password, name, lastname, birthdate } = req.body; /* No es necesario declarar los tipos al desestructurar `req.body` porque TypeScript los infiere automáticamente según la interfaz definida para `req.body`.*/
        const foundUser = yield user_model_1.User.findOne({ email }); /* findOne es un método proporcionado automáticamente por Mongoose, devuelve el objeto usurio que encuentre con el mismo mail que le pasamos por parametros, recordar que el parametro tiene que ser un objeto literal, tal que { email } = { email: email } = { email: "example@mail.com" } */
        if (foundUser) // Si el usuario ya existe (True), devuelve un error con el mensaje "Ya existe un usuario con ese email" y código HTTP 400.
            return next(new errorHandler_middleware_1.ErrorResponse("Ya existe un usuario con ese email", 400));
        /*Si usara `throw` en lugar de `next()`, el flujo de ejecución se interrumpe de inmediato y todo el código posterior no se ejecuta. El error lanzado se pasará al middleware de manejo de errores de Express si está configurado.
        Con `next(error)`, el error se pasa al siguiente middleware sin detener la ejecución del código inmediatamente, permitiendo que otras operaciones ocurran antes de manejar el error.*/
        // A continuacion creo un objeto usuario con las propuedades del req.body, notar que es distinto porque es un esquema según mongoose y no el constructor tipico de js que conozco
        const user = new user_model_1.User({
            name, /* acá tambien seria una forma simplificada de escribir name: name (key: valu, tal que value es la variable de req.body) */
            lastname,
            email,
            password,
            birthdate,
        });
        yield user.save(); // Lo guarda en MongoDB transformandolo en un JSON y le asigna un _id único a ese documento.
        res
            .status(201) // Recordar: 200 → Éxito general, 400 → Solicitud incorrecta (errores del cliente), 500 → Error interno del servidor.
            .json({ error: false, message: "Cuenta creada correctamente", user });
    }
    catch (error) {
        /*
        Este bloque `catch` captura únicamente los errores que ocurren dentro del bloque `try`, ya sea porque se lanzaron explícitamente con `throw` o porque surgieron durante una operación asíncrona (como `await user.save()`).
        Sin embargo, los errores manejados con `next()` dentro del bloque `try` (por ejemplo, `next(new ErrorResponse(...))`) NO pasarán por este `catch`.
        En esos casos, `next()` simplemente redirige el flujo de ejecución al middleware global de manejo de errores, sin que este bloque intercepte dichos errores.
        Aquí, usamos `next(error)` para pasar cualquier error capturado en este bloque al middleware global de manejo de errores, donde se procesará y se enviará una respuesta adecuada al cliente.
        */
        next(error);
    }
});
exports.register = register;
const login = (/* Este controlador maneja las solicitudes de inicio de sesión (POST /login). */ req, res, next) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const { email, password } = req.body;
        const user = yield user_model_1.User.findOne({ email }); // si encuentro un user lo voy a usar para ver el _id y asi incluirlo en el token.
        if (!user) { // si no encontre un usuario es porque las credenciales son claramente invalidas => null --> False / !False = True
            return next(new errorHandler_middleware_1.ErrorResponse("Credenciales invalidas", 401)); //Se llama a next con un error personalizado (ErrorResponse), y el middleware global de errores devuelve un mensaje al cliente.
        }
        const isMatch = yield user.comparePassword(password); // Usa el método personalizado comparePassword definido en el modelo User para comparar la contraseña ingresada con la contraseña almacenada en la base de datos. Devuelve True si la contrasela es la misma que la que le apse por parametros
        if (!isMatch) { // tambien lo niego porque la idea es enviar un error si la contraseña es invalida --> isMatch = False, !False = True => Envio error
            return next(new errorHandler_middleware_1.ErrorResponse("Contraseña incorrecta", 401));
        }
        // El método jwt.sign devuelve un token JWT como un string codificado: header.payload.signature
        /* Forma general: jwt.sign(payload, secret, options)
            payload: Contiene los datos que quieres incluir en el token.
            secret: Es el secreto que el servidor usa para firmar el token.
            options (opcional): Configuraciones adicionales, como la duración del token.
          ¿Dónde está el header?
            El header es generado automáticamente por jsonwebtoken. No necesitas pasarlo como parámetro. Por defecto, usa:
            {
              "alg": "HS256",  // Algoritmo de firma
              "typ": "JWT"     // Tipo de token
            }
        */
        /* const token = jwt.sign({ id: user._id }, JWT_TOKEN || "", {
          expiresIn: "1d",
        }); // --> Reemplace todo esto por lo siguiente que lo entiendo mejor... Preguntarle a Agustin si podemos hacer este cambio */
        const payload = { id: user._id }; // Datos (Payload) que se incluirán en el token. En este caso, estás creando un objeto con una clave id y un valor que proviene de user._id.  El campo _id es un identificador especial que MongoDB genera automáticamente para cada documento en una colección.
        const secret = env_1.JWT_TOKEN || ""; // Secreto para firmar el token
        const options = { expiresIn: "1d" }; // Opciones del token, como la expiración
        const token = jsonwebtoken_1.default.sign(payload, secret, options); // Generar el token
        res.status(200).json({ error: false, token }); // Al establecer error: false, el servidor está indicando que la operación se completó sin errores + envio el token con sus 3 partes codificadas. 
        /* Notar cómo solo envio el token con lo sufciciente y neesario para el front, y no todo el objeto como hacia yo tradicionalemnte con MySQL aplicando MVC con Dao */
    }
    catch (error) {
        next(error);
    }
});
exports.login = login;
const getUser = (// Este controlador maneja las solicitudes para obtener información del usuario autenticado. Se espera que sea utilizado en una ruta protegida donde el usuario ya esté autenticado.
req, // Va tener el agregado de .user que hizo el middleware checkAuth.middleware.ts
res, next) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        // A continuación se busca un usuario en la base de datos de MongoDB utilizando su ID.
        const user = yield user_model_1.User.findById(req.user); // req.user no es parte de Express por defecto. Es un campo añadido manualmente por un middleware de autenticación (como checkAuth) después de validar al usuario. 
        res.status(200).json({
            error: false,
            user // igual a poner user: user 
        });
    }
    catch (error) {
        next(error);
    }
});
exports.getUser = getUser;
