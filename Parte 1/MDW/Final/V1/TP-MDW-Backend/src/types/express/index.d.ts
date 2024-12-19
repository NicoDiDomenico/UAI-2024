import { Language, User } from "../custom";
// No le doy bola
export {}

declare global {
  namespace Express {
    export interface Request {
      user?: any;
    }
  }
}