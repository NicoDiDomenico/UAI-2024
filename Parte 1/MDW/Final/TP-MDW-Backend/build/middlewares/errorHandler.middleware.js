"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.errorHandler = exports.ErrorResponse = void 0;
/*
Request: Representa la solicitud HTTP.
Response: Representa la respuesta HTTP que envía el servidor.
NextFunction: Es una función que se llama para pasar el control al siguiente middleware.
*/
// Clase ErrorResponse --> ¿Qué es? Una clase personalizada que extiende la clase Error de JavaScript.
/*
Error es una clase nativa de JavaScript que se usa para crear errores.
Al escribir extends Error, estás diciendo que ErrorResponse es un tipo especial de error que:
  Hereda todas las propiedades y métodos de la clase Error.
  Puede agregar propiedades o métodos propios (como statusCode).
*/
class ErrorResponse extends Error {
    /* Con statusCode, puedes enviar respuestas al cliente con un código adecuado dependiendo del tipo de error.
    404 para "No encontrado".
    401 para "No autorizado".
    500 para "Error interno del servidor". */
    constructor(message, statusCode) {
        super(message);
        /* La palabra clave super llama al constructor de la clase base (en este caso, Error).
        Esto asegura que el mensaje del error (message) también se guarde correctamente en el objeto Error. */
        this.statusCode = statusCode;
    }
}
exports.ErrorResponse = ErrorResponse;
/*
¿Cómo usas esta clase?
Cuando ocurre un error en tu aplicación, puedes usar una clase personalizada como ErrorResponse para crear un error específico. Por ejemplo:
  throw new ErrorResponse("Usuario no encontrado", 404);

Explicación del concepto: Es común personalizar la clase Error en JavaScript para adaptarla a las necesidades de manejo de errores más específicas, como los errores HTTP. La clase Error original de JavaScript fue pensada para manejar errores de programación (por ejemplo, en el navegador), pero con la llegada de Node.js, la gestión de errores en aplicaciones de servidor (como las que manejan APIs y solicitudes HTTP) requiere una estructura más adecuada.
El motivo de personalizarla es que Error por sí sola no contiene propiedades como statusCode o detalles específicos de HTTP, que son necesarios para devolver respuestas adecuadas a los usuarios o clientes de una API.
*/
/* Cuando ocurre un error en alguna ruta o middleware, Express:
  Detecta el error.
  Busca un middleware especial de manejo de errores que tenga 4 parámetros: (err, req, res, next).
  - Para que un middleware sea de manejo de errores, debe tener 4 parámetros, sino express lo maneja como uno normal*/
const errorHandler = (
/* Parámetros del middleware */
/* --- Este parámetro no es automático, debe ser lanzado manualmente desde tu código o generado por Express si algo falla ---*/
err, // El error que ocurrió, en este caso, suele ser un objeto de tipo ErrorResponse
/* --- Parametros que son generados automáticamente por Express cada vez que llega una solicitud --- */
req, // La solicitud HTTP que llegó al servidor
res, // La respuesta HTTP que enviaremos al cliente
next // Una función que pasa el control al siguiente middleware (no se usa aquí)
) => {
    const statusCode = err.statusCode || 500;
    // Enviar la respuesta al cliente
    res.status(statusCode).json({
        error: true,
        message: err.message || "Server Error",
    }); // basicamente está construyendo una respuesta JSON.
    /* Métodos importantes de res:
    res.status(codigo): Establece el código de estado HTTP de la respuesta.
    res.json(objeto): Envía un objeto como respuesta al cliente, automáticamente convertido en formato JSON.
    */
};
exports.errorHandler = errorHandler;