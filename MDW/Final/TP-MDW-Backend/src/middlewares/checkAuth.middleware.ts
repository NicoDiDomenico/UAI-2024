import jwt from "jsonwebtoken";
import { Request, Response, NextFunction } from "express";
import { ErrorResponse } from "./errorHandler.middleware";
import { JWT_TOKEN, MONGODB_URI } from "../config/env";

// No le doy bola DICE agustin 
interface IJwtPayload extends jwt.JwtPayload {
  id: string;
}

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

  try {
    const decodedToken = jwt.verify(token, JWT_TOKEN) as IJwtPayload;

    req.user = decodedToken.id; // Añade el ID del usuario a req.user --> Se usa en user.controller.ts
    next();
  } catch (error) {
    return next(new ErrorResponse("Usuario no autorizado", 401));
  }
};
