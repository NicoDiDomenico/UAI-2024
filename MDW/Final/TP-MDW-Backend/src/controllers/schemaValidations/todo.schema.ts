/* 
Estos archivos definen esquemas de validación. Cada esquema establece las reglas que los datos deben cumplir antes de procesarlos en el backend.
- user.schema.ts: Define reglas para validar datos relacionados con los usuarios, como el registro y el inicio de sesión.
todo.schema.ts: Define reglas para validar datos relacionados con las tareas (todos).
*/

import Joi from "joi";
import { ITodo } from "../../interfaces";

const createTodoSchema = Joi.object<ITodo>({
    title: Joi.string().min(3).required(),
    description: Joi.string().min(3).required()
});

export { createTodoSchema };
