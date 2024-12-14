import jwt from "jsonwebtoken";
import { Request, Response, NextFunction } from "express";
import { ErrorResponse } from "./errorHandler.middleware";
import { JWT_TOKEN, MONGODB_URI } from "../config/env";

// No le doy bola DICE agustin 
interface IJwtPayload extends jwt.JwtPayload {
  id: string;
}

// Se extiende la interfaz Request que viene de Express. Esto significa que cualquier objeto req de una solicitud en este proyecto ahora puede tener una propiedad user de tipo string. Esto es útil porque Express no tiene un campo req.user por defecto, pero queremos usarlo para guardar el id del usuario decodificado del token JWT. Typescript lo valida gracias a esta extensión.
declare global {
  namespace Express {
    interface Request {
      user: string;
    }
  }
}

// Este si dale bola
// Recibo del front el token, chequeo que tiene adentro, y lo paso al handler
export const checkAuth = (req: Request, res: Response, next: NextFunction) => {
  
  const token = req.headers.authorization?.split(" ")[1];
  if (!token) return next(new ErrorResponse("Usuario no autorizado", 401));

  try { /* Notar como no hace falta usar try catch con async await, esto es porque podemos tambien captar errores en funciones sincronicas */
    const decodedToken = jwt.verify(token, JWT_TOKEN) as IJwtPayload;
    // Verifica la validez del token JWT recibido (que se envió desde el frontend) utilizando la clave secreta (JWT_TOKEN).
    // Si el token es válido:
    //  - Decodifica el payload del token (los datos que contiene, como el id del usuario).
    //  - Devuelve el payload como un objeto de JavaScript.
    // Si el token no es válido (por ejemplo, expiró o fue manipulado), lanza un error.
    // La palabra clave `as IJwtPayload` asegura que el resultado se trate como un objeto con una propiedad `id` específica.

    // Aquí se toma el valor id del decodedToken y se asigna al campo req.user.
    req.user = decodedToken.id; // Añade el ID del usuario a req.user --> Se usa en user.controller.ts
    next();
  } catch (error) {
    return next(new ErrorResponse("Usuario no autorizado", 401));
  }
};
