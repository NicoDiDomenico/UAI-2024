import { Document } from "mongoose"; /* Me parece al pedo el extend si no valido _id. --> No porque esto es general para todos los archivos y puede que uno si haga uso del _id */

/*  Una interfaz en TypeScript define la forma o estructura que debe tener un objeto. (o funciones, clases, etc.) Es como un "contrato" que asegura que un objeto cumpla con ciertas reglas. */

export interface IUser extends Document { /* extends significa que IUser hereda todas las propiedades y métodos de Document. 
                               Document: Es una interfaz proporcionada por Mongoose que describe la estructura básica de un documento en MongoDB.
                               Incluye propiedades como _id (el identificador único generado automáticamente) y métodos relacionados con MongoDB. */
  name: string; // El usuario debe tener un campo 'name' de tipo string
  lastname: string;
  email: string;
  birthdate: Date;
  password: string;
  comparePassword(password: string): Promise<Boolean>; 
  /*  ¿Qué significa tener un método en una interfaz?
  Una interfaz en TypeScript puede definir no solo propiedades, sino también métodos.
  Esto asegura que cualquier objeto que implemente esa interfaz debe tener ese método y que dicho método cumpla con la firma especificada (es decir, con sus parámetros y tipo de retorno). */
}
