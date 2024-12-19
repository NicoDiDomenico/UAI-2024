"use strict";
/*
Estos archivos definen esquemas de validación. Cada esquema establece las reglas que los datos deben cumplir antes de procesarlos en el backend.
- user.schema.ts: Define reglas para validar datos relacionados con los usuarios, como el registro y el inicio de sesión.
todo.schema.ts: Define reglas para validar datos relacionados con las tareas (todos).
*/
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.createTodoSchema = void 0;
const joi_1 = __importDefault(require("joi"));
const createTodoSchema = joi_1.default.object({
    title: joi_1.default.string().min(3).required(),
    description: joi_1.default.string().min(3).required()
});
exports.createTodoSchema = createTodoSchema;
