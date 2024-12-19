"use strict";
/*
Estos archivos definen esquemas de validación. Cada esquema establece las reglas que los datos deben cumplir antes de procesarlos en el backend.
- user.schema.ts: Define reglas para validar datos relacionados con los usuarios, como el registro y el inicio de sesión.
*/
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.loginSchema = exports.registerSchema = void 0;
const joi_1 = __importDefault(require("joi")); // Joi es una biblioteca que te permite definir esquemas para validar datos. Un esquema describe las reglas que los datos deben cumplir.
const registerSchema = joi_1.default.object({
    // Reglas de validación de los campos:
    // 1. email:
    //    - Joi.string(): El campo debe ser una cadena de texto.
    //    - .email(): Valida que la cadena sea un correo electrónico válido, como usuario@dominio.com.
    //    - .required(): Indica que este campo es obligatorio.
    // 2. password:
    //    - Joi.string(): El campo debe ser una cadena de texto.
    //    - .min(6): La cadena debe tener al menos 6 caracteres.
    //    - .required(): Este campo es obligatorio.
    // 3. name:
    //    - Joi.string(): El campo debe ser una cadena de texto.
    //    - .min(2): La cadena debe tener al menos 2 caracteres.
    //    - .required(): Este campo es obligatorio.
    // 4. lastname:
    //    - Igual que "name", valida que sea un string de al menos 2 caracteres y que sea obligatorio.
    // 5. birthdate:
    //    - Joi.date(): El campo debe ser una fecha válida.
    //    - .less("now"): Valida que la fecha sea anterior a la fecha actual, asegurando que no sea una fecha futura. Se lee como "Menor a (valor)", se puede usar con numeros.
    //    - .required(): Este campo es obligatorio.
    email: joi_1.default.string().email().required(),
    password: joi_1.default.string().min(6).required(),
    name: joi_1.default.string().min(2).required(),
    lastname: joi_1.default.string().min(2).required(),
    birthdate: joi_1.default.date().less("now").required(),
});
exports.registerSchema = registerSchema;
// Validación ESTÁTICA con TypeScript
// TypeScript verifica en tiempo de desarrollo que el esquema de Joi sea consistente con la interfaz IUser.
// Por ejemplo, si intentamos agregar una propiedad no definida en IUser, TypeScript mostrará un error.
// Validación DINÁMICA con Joi
// Ocurre en tiempo de ejecución, cuando los datos enviados por el cliente son validados contra el esquema definido.
// Si los datos no cumplen las reglas del esquema, Joi genera un error que se puede manejar en el middleware de validación.
// Ejemplo de validación dinámica: Si el cliente envía un email inválido o una fecha de nacimiento futura, Joi lo detectará y rechazará la solicitud.
const loginSchema = joi_1.default.object({
    email: joi_1.default.string().email().required(),
    password: joi_1.default.string().min(6).required(),
}); // El esquema de Joi no necesita tener todas las propiedades de la interfaz IUser, solo las necesarias para validar la operación específica (por ejemplo, login o registro).
exports.loginSchema = loginSchema;
