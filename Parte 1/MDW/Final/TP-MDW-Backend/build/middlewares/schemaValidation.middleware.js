"use strict";
/* validateSchema retorna una función que valida req.body usando un esquema de Joi. Si hay errores, los pasa al manejador de errores de Express con next(new ErrorResponse(...)); si no hay errores, llama a next() para continuar. */
Object.defineProperty(exports, "__esModule", { value: true });
const errorHandler_middleware_1 = require("./errorHandler.middleware");
// Propósito: Validar que los datos enviados en req.body cumplan con el esquema definido.
const validateSchema = (schema) => {
    return (req, res, next) => {
        const { error } = schema.validate(req.body); // Usa el esquema (pasado en el paso 1) para validar los datos enviados por el cliente (req.body).
        if (error) {
            return next(new errorHandler_middleware_1.ErrorResponse(error.details[0].message, 400));
        }
        next(); // Si los datos enviados cumplen con el esquema de validación, este next() pasa el control al siguiente middleware definido en la ruta (en este caso, el controlador "register"). Si no se llama a next(), la solicitud quedaría "colgada" y no recibiría respuesta.
    };
};
exports.default = validateSchema;
