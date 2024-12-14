import { Request, Response, NextFunction } from "express"; // Si no usas TypeScript, no necesitas importar Request, Response y NextFunction de Express. Estos son tipos que solo tienen sentido en un proyecto con TypeScript para definir y validar los parámetros en las funciones de los controladores o middlewares, en este caso para señar que el tipo de esos parametros son objetos.
import { User } from "../models/user.model"; // Importa el modelo User, que probablemente define la estructura y comportamiento de los datos del usuario, incluyendo métodos como findOne, save y comparePassword.
import jwt from "jsonwebtoken"; // Generar tokens de autenticación basados en JWT para identificar usuarios autenticados. Esta forma en ESM/ES6 reemplaza la forma tradicional en node.js que es: const jwt = require("jsonwebtoken"); 
import { ErrorResponse } from "../middlewares/errorHandler.middleware"; // Facilitar el manejo de errores personalizados, como los de validación o autenticación.
import { JWT_TOKEN } from "../config/env"; // Importa el secreto que se usa para firmar los tokens JWT. Este secreto se encuentra en las variables de entorno.

export const register = async ( /* Este controlador maneja las solicitudes POST /register para registrar un nuevo usuario. */
  req: Request, /* parámetros con su tipo (objeto) */
  res: Response,
  next: NextFunction
) => {
  try {
    const { email, password, name, lastname, birthdate } = req.body; /* No es necesario declarar los tipos al desestructurar `req.body` porque TypeScript los infiere automáticamente según la interfaz definida para `req.body`.*/

    const foundUser = await User.findOne({ email }); /* findOne es un método proporcionado automáticamente por Mongoose, devuelve el objeto usurio que encuentre con el mismo mail que le pasamos por parametros, recordar que el parametro tiene que ser un objeto literal, tal que { email } = { email: email } = { email: "example@mail.com" } */

    if (foundUser) // Si el usuario ya existe (True), devuelve un error con el mensaje "Ya existe un usuario con ese email" y código HTTP 400.
      return next(new ErrorResponse("Ya existe un usuario con ese email", 400));
      /* Si usara `throw` en lugar de `next()`, el flujo de ejecución se interrumpe de inmediato 
      debido a que se lanza una excepción. El error lanzado será capturado por un bloque `catch`
      o, si no hay uno, por el middleware de manejo de errores de Express.

      Con `next(error)` acompañado de un `return`, como en este caso, el flujo de ejecución
      también se interrumpe, pero sin lanzar una excepción. En su lugar, el control pasa 
      directamente al siguiente middleware configurado en la aplicación, lo cual es más adecuado 
      en el contexto de Express. */

    
    // A continuacion creo un objeto usuario con las propuedades del req.body, notar que es distinto porque es un esquema según mongoose y no el constructor tipico de js que conozco
    const user = new User({
      name, /* acá tambien seria una forma simplificada de escribir name: name (key: valu, tal que value es la variable de req.body) */
      lastname,
      email,
      password,
      birthdate,
    });

    await user.save(); // Lo guarda en MongoDB transformandolo en un JSON y le asigna un _id único a ese documento.

    res
      .status(201) // Recordar: 200 → Éxito general, 400 → Solicitud incorrecta (errores del cliente), 500 → Error interno del servidor.
      .json({ error: false, message: "Cuenta creada correctamente", user }); /* medio al pedo devuelve el usuario, endria que ver si el front lo usa */
  } catch (error) {
    /* 
    Este bloque `catch` captura únicamente los errores que ocurren dentro del bloque `try`, ya sea porque se lanzaron explícitamente con `throw` o porque surgieron durante una operación asíncrona (como `await user.save()`).
    Sin embargo, los errores manejados con `next()` dentro del bloque `try` (por ejemplo, `next(new ErrorResponse(...))`) NO pasarán por este `catch`. 
    En esos casos, `next()` simplemente redirige el flujo de ejecución al middleware global de manejo de errores, sin que este bloque intercepte dichos errores.
    Aquí, usamos `next(error)` para pasar cualquier error capturado en este bloque al middleware global de manejo de errores, donde se procesará y se enviará una respuesta adecuada al cliente.
    */
    next(error);
  }
};

export const login = async ( /* Este controlador maneja las solicitudes de inicio de sesión (POST /login). */
  req: Request,
  res: Response,
  next: NextFunction
) => {
  try {
    const { email, password } = req.body;
    const user = await User.findOne({ email }); // si encuentro un user lo voy a usar para ver el _id y asi incluirlo en el token.
    if (!user) {  // si no encontre un usuario es porque las credenciales son claramente invalidas => null --> False / !False = True
      return next(new ErrorResponse("Credenciales invalidas", 401)); //Se llama a next con un error personalizado (ErrorResponse), y el middleware global de errores devuelve un mensaje al cliente.
    }

    const isMatch = await user.comparePassword(password); // Usa el método personalizado comparePassword definido en el modelo User para comparar la contraseña ingresada con la contraseña almacenada en la base de datos. Devuelve True si la contrasela es la misma que la que le apse por parametros
    if (!isMatch) { // tambien lo niego porque la idea es enviar un error si la contraseña es invalida --> isMatch = False, !False = True => Envio error
      return next(new ErrorResponse("Contraseña incorrecta", 401));
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

    const token = jwt.sign({ id: user._id }, JWT_TOKEN || "", {
      expiresIn: "1d",
    });

    /* 
    Mas entendible:
     const payload = { id: user._id }; // Datos (Payload) que se incluirán en el token. En este caso, estás creando un objeto con una clave id y un valor que proviene de user._id.  El campo _id es un identificador especial que MongoDB genera automáticamente para cada documento en una colección.
    const secret = JWT_TOKEN || ""; // Secreto para firmar el token
    const options = { expiresIn: "1d" }; // Opciones del token, como la expiración
    
    const token = jwt.sign(payload, secret, options); // Generar el token
    */

    const formatedUser = {
      name: user.name,
      lastname: user.lastname,
      email: user.email,
      birthdate: user.birthdate,
      id: user._id
    }

    res.status(200).json({ error: false, token, message: "Sesión iniciada correctamente", user: formatedUser }); 
    // Al establecer error: false, el servidor está indicando que la operación se completó sin errores + envio el token con sus 3 partes codificadas. 

    /* Notar cómo solo envio el token con lo sufciciente y neesario para el front, y no todo el objeto como hacia yo tradicionalemnte con MySQL aplicando MVC con Dao */ 

    /* Crea un objeto `formatedUser` para estructurar y filtrar los datos del usuario que serán enviados al cliente.
    Este objeto contiene únicamente la información esencial que necesita el cliente:
    - `name`, `lastname`, `email`, y `birthdate` son datos personales básicos.
    - `id` es el identificador único del usuario (proveniente de MongoDB) que puede ser útil para futuras solicitudes.
    Nota: No se devuelve el objeto completo del usuario (`user`) para evitar exponer información sensible como la contraseña 
    o datos innecesarios como `__v` o `timestamps`, que son internos de la base de datos.*/
  } catch (error) {
    next(error);
  }
};

export const getUser = async ( // Este controlador maneja las solicitudes para obtener información del usuario autenticado. Se espera que sea utilizado en una ruta protegida donde el usuario ya esté autenticado.
  req: Request, // Va tener el agregado de .user que hizo el middleware checkAuth.middleware.ts
  res: Response,
  next: NextFunction
) => {
  try { // A continuación se busca un usuario en la base de datos de MongoDB utilizando su ID.
    const user = await User.findById(req.user) // req.user no es parte de Express por defecto. Es un campo añadido manualmente por un middleware de autenticación (como checkAuth) después de validar al usuario. 

    // Con el operador ?., si user es null o undefined, el resultado de user?.name será simplemente undefined, y no lanzará ningún error.
    const formatedUser = {
      name: user?.name,
      lastname: user?.lastname,
      email: user?.email,
      birthdate: user?.birthdate,
      id: user?._id
    }

    res.status(200).json({
      error: false,
      user: formatedUser
    })
  } catch (error) {
    next(error)
  }
}