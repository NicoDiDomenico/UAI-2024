import { Request, Response, NextFunction } from "express";
import Joi from "joi";
import { ErrorResponse } from "./errorHandler.middleware";

// Propósito: Validar que los datos enviados en req.body cumplan con el esquema definido.
const validateSchema = (schema: Joi.ObjectSchema) => { // El middleware no valida nada por sí mismo. En cambio, toma como argumento un esquema de validación creado con Joi.
  return (req: Request, res: Response, next: NextFunction) => {
    const { error } = schema.validate(req.body); // Usa el esquema (pasado en el paso 1) para validar los datos enviados por el cliente (req.body).
    if (error) {
      return next(new ErrorResponse(error.details[0].message, 400));
    }
    next(); // Si los datos enviados cumplen con el esquema de validación, este next() pasa el control al siguiente middleware definido en la ruta (en este caso, el controlador "register"). Si no se llama a next(), la solicitud quedaría "colgada" y no recibiría respuesta.
  };
};

export default validateSchema;