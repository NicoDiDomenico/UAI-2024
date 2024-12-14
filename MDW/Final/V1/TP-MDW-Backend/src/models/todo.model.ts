import { Schema, model } from "mongoose";
import { ITodo } from "../interfaces/todo.interface";

const TodoSchema: Schema = new Schema<ITodo>(
  {
    title: { 
      type: String, // Tipo de dato: Cadena de texto (String).
      required: true // Este campo es obligatorio al crear un documento.
    },
    description: { 
      type: String // Tipo de dato: Cadena de texto (String).
      // Este campo no tiene `required`, por lo que es opcional.
    },
    completed: { 
      type: Boolean, // Tipo de dato: Booleano (true o false).
      default: false // Valor predeterminado: Si no se especifica, será `false`.
    },
    tags: {
      type: [String], // Tipo de dato: Array de cadenas de texto.
      default: [] // Valor predeterminado: Si no se especifica, será un array vacío.
    },
    isPinned: { 
      type: Boolean, // Tipo de dato: Booleano (true o false).
      default: false // Valor predeterminado: Si no se especifica, será `false`.
      // Indica si la tarea debe destacarse o tener prioridad.
    },
    createdOn: { 
      type: Date, // Tipo de dato: Fecha (Date).
      default: new Date().getTime() // Valor predeterminado: La fecha/hora actual en formato de timestamp.
    },
    user: { 
      type: Schema.Types.ObjectId, // Tipo de dato: ObjectId (referencia a otro documento).
      ref: "User", // Relación con la colección "User". Esto crea una referencia al usuario que creó la tarea.
      required: true // Este campo es obligatorio, cada tarea debe estar asociada a un usuario.
    }
  }
);

export const Todo = model("Todo", TodoSchema);
