import jwt from "jsonwebtoken";
import { config } from "../config/env.js";
import type { JwtPayload } from "../types/jwt.types.js";

export const generateToken = (payload: JwtPayload): string => {
  return jwt.sign(
    payload,
    config.jwtSecret,
    { expiresIn: config.jwtExpiration }
  );
};

export const verifyToken = (token: string): JwtPayload => {
  return jwt.verify(token, config.jwtSecret) as JwtPayload;
};
