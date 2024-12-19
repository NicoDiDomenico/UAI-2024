import { Schema, model } from "mongoose";
import { ITodo } from "../interfaces/todo.interface";

const TodoSchema: Schema = new Schema<ITodo>(
  {
    title: { type: String, required: true },
    description: { type: String },
    completed: { type: Boolean, default: false },
    tags: {type: [String], default: []}, // Tipo de dato: Array de cadenas de texto.
    isPinned: { type:Boolean, default: false},  // Valor predeterminado: Si no se especifica, será `false`, Indica si la tarea debe destacarse o tener prioridad.
    createdOn: {type:Date, default: new Date().getTime()}, // Valor predeterminado: La fecha/hora actual en formato de timestamp.
    user: { 
      type: Schema.Types.ObjectId, // Tipo de dato: ObjectId (referencia a otro documento). 
      ref: "User", // Relación con la colección "User". Esto crea una referencia al usuario que creó la tarea.
      required: true // Este campo es obligatorio, cada tarea debe estar asociada a un usuario.
    },
  }
);

export const Todo = model<ITodo>("Todo", TodoSchema);
