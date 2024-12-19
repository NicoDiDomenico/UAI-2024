import { model, models, Schema } from "mongoose";
import { IUser } from "../interfaces";
import bcrypt from "bcrypt"; /* bcrypt es una librería de JavaScript que se utiliza para encriptar y comparar contraseñas de manera segura. */

const userSchema = new Schema<IUser>(
  {
    name: { type: String, required: true },
    lastname: { type: String, required: true },
    email: { type: String, required: true, unique: true },
    password: { type: String, required: true },
    birthdate: { type: Date, required: true },
  },
  { timestamps: true }
);
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
userSchema.methods.comparePassword = async function (
  password: string
): Promise<Boolean> {
  return await bcrypt.compare(password, this.password);
};

// Defino un metodo que antes de guardar el usuario en el registro encripta la contraseña.
userSchema.pre("save", async function (next) {
  if (!this.isModified("password")) return next();
  const salt = await bcrypt.genSalt(10);
  this.password = await bcrypt.hash(this.password, salt);
  next();
});

export const User = model("User", userSchema); /* Esta función se utiliza para crear un modelo basado en un esquema (que es un conjunto de reglas para los documentos dentro de una colección en MongoDB). */
// Con este nombre se guarda en la BD
