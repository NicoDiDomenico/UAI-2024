"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.User = void 0;
const mongoose_1 = require("mongoose");
const bcrypt_1 = __importDefault(require("bcrypt")); /* bcrypt es una librería de JavaScript que se utiliza para encriptar y comparar contraseñas de manera segura. */
const userSchema = new mongoose_1.Schema({
    name: { type: String, required: true },
    lastname: { type: String, required: true },
    email: { type: String, required: true, unique: true },
    password: { type: String, required: true },
    birthdate: { type: Date, required: true },
}, { timestamps: true });
/*Schema: Es una estructura de datos que define cómo deben lucir los documentos dentro de una colección.
  Propiedades del esquema:
  Cada propiedad (como name, email, password) tiene:
    type: El tipo de dato (similar a MySQL, pero más flexible). Ejemplo: String, Date, etc.
    required: Indica si el campo es obligatorio.
    unique: Garantiza que no haya duplicados para este campo (similar a una clave única en MySQL).
  timestamps:
    Si lo habilitas ({ timestamps: true }), MongoDB agrega automáticamente:
      createdAt: Fecha de creación del documento.
      updatedAt: Fecha de la última modificación del documento.
*/
// Compara la contraseña que ingreso el usuario con la que está en la BD, PERO para eso tengo que encriptarla, ya que la de la BD ser encriptó con userSchema.pre()
userSchema.methods.comparePassword = function (password) {
    return __awaiter(this, void 0, void 0, function* () {
        return yield bcrypt_1.default.compare(password, this.password);
    });
};
// Defino un metodo que antes de guardar el usuario en el registro encripta la contraseña.
userSchema.pre("save", function (next) {
    return __awaiter(this, void 0, void 0, function* () {
        if (!this.isModified("password"))
            return next();
        const salt = yield bcrypt_1.default.genSalt(10);
        this.password = yield bcrypt_1.default.hash(this.password, salt);
        next();
    });
});
exports.User = (0, mongoose_1.model)("User", userSchema);
// Con este nombre se guarda en la BD
