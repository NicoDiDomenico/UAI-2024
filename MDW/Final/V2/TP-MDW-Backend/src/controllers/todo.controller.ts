import { NextFunction, Response, Request } from "express";
import { Todo } from "../models/todo.model";
import { ErrorResponse } from "../middlewares/errorHandler.middleware";
import { error } from "console";

export const createTodo = async (
  req: Request,
  res: Response,
  next: NextFunction
) => {
  const { title, description, tags } = req.body;

  try {
    const todo = await Todo.create({ // Esto es lo mismo que el save pero para cagarme la vida
      title,
      description,
      tags,
      user: req.user,
    });

    res.status(200).json({
      error: false,
      message: "Nota creada correctamente",
      todo,
    });
  } catch (error) {
    next(error);
  }
};

export const editTodo = async (
  req: Request,
  res: Response,
  next: NextFunction
) => {
  const { title, description, tags, isPinned } = req.body;
  const { id } = req.params; /* es lo mismo que usar directamente req.params.id */

  try {
    const todo = await Todo.findOne({ _id: id, user: req.user });

    if (!todo) return next(new ErrorResponse("Nota no encontrada", 404));

    if (title) todo.title = title;
    if (description) todo.description = description;
    if (tags) todo.tags = tags;
    if (isPinned) todo.isPinned = isPinned;

    await todo.save();

    res.status(200).json({
      error: false,
      message: "Nota editada correctamente",
      todo,
    });
  } catch (error) {
    next(error);
  }
};

export const getEveryTodo = async (
  req: Request,
  res: Response,
  next: NextFunction
) => {
  try {
    const todos = await Todo.find({// Se busca en la colección de tareas (Todo) todas las que tengan un campo user igual al ID del usuario autenticado (req.user). Este ID fue agregado previamente por el middleware checkAuth.
      user: req.user, // Filtra las tareas que pertenecen al usuario autenticado.
    }).sort({ // El método sort() en Mongoose organiza los documentos que se obtienen de la base de datos en un orden específico antes de enviarlos como respuesta. Este orden se define según los valores de uno o más campos. Por lo tanto ordena las tareas de mayor a menor prioridad según el campo `isPinned`
      isPinned: -1,
      /* 
      isPinned: Es el campo del modelo que se utiliza como criterio de ordenación.
      -1: Indica que los documentos se ordenan en orden descendente. Esto significa que:
        Los documentos con isPinned: true aparecerán antes que los documentos con isPinned: false.
      */
    });

    res.status(200).json({
      error: false,
      todos,
    });
  } catch (error) {
    next(error);
  }
};

export const getTodo = async (
  req: Request,
  res: Response,
  next: NextFunction
) => {
  try {
    const todo = await Todo.findById(req.params.id);

    res.status(200).json({
      error: false,
      todo,
    });
  } catch (error) {
    next(error);
  }
};

export const deleteTodo = async (
  req: Request,
  res: Response,
  next: NextFunction
) => {
  const id = req.params.id;
  try {
    const todo = await Todo.findOne({ _id: id, user: req.user });

    if (!todo) return next(new ErrorResponse("Nota no encontrada", 404));

    await Todo.deleteOne({ _id: id, user: req.user });

    res.status(200).json({
      error: false,
      message: "Nota eliminada correctamente",
    });
  } catch (error) {
    next(error);
  }
};

export const updateIsPinned = async (
  req: Request,
  res: Response,
  next: NextFunction
) => {
  /* Desestructuración de parámetros */
  const { isPinned } = req.body; // Extrae el valor de la propiedad `isPinned` del cuerpo de la solicitud (req.body). Este campo probablemente lo envía el cliente para indicar si desea cambiar el estado de "pin" de la nota.
  const { id } = req.params; // Extrae el parámetro `id` de los parámetros de la ruta (req.params). Este `id` corresponde al identificador único de la nota que se desea actualizar.  

  try {
    /* Buscar la nota en la base de datos */
    const todo = await Todo.findOne({ _id: id, user: req.user });

    /* Validación: Nota no encontrada */
    if (!todo) return next(new ErrorResponse("Nota no encontrada", 404));

    /* Alternar el estado de isPinned */
    todo.isPinned = !todo.isPinned;
      // Si isPinned es true, lo cambia a false.
      // Si isPinned es false, lo cambia a true.

    await todo.save(); // Guarda el documento actualizado en la base de datos. Si el documento ya existe en la base de datos (es decir, ya tiene un _id asignado), .save() simplemente actualiza el documento existente, manteniendo el mismo _id.

    /* Respuesta al cliente */
    res.status(200).json({
      error: false,
      message: "Nota editada correctamente",
      todo,
    }); // Devuelve un estado 200 (éxito) junto con un mensaje de confirmación y el documento actualizado (todo).
  } catch (error) { // Captura cualquier error que ocurra en el bloque try y lo pasa al middleware global de manejo de errores.
    next(error);
  }
};

export const searchNotes = async (
  req: Request,
  res: Response,
  next: NextFunction
) => {
  let { query } = req.query; // Extrae el término de búsqueda desde los parámetros de la URL (ejemplo: `/search?query=nota`).

  query = String(query); // Asegura que `query` sea tratado como una cadena. Si no lo conviertes, RegExp podría dar problemas si `query` es undefined o no es un string.

  if (!query) // Valida si no se proporcionó un término de búsqueda.
    return next(new ErrorResponse("Query necesaria", 400)); // Llama al middleware de manejo de errores con un mensaje y código HTTP 400 (solicitud incorrecta).

  try { 
    // Usa Mongoose para buscar notas que coincidan con el término de búsqueda.
    const matchedNotes = await Todo.find({ // matchedNotes contendrá un arreglo de objetos cuando se resuelva la promesa. Estos objetos son documentos de MongoDB que coinciden con la búsqueda que has definido en la consulta de Mongoose.
      user: req.user, // Busca solo las notas del usuario autenticado. Este ID es añadido al `req` por un middleware (como `checkAuth`).
      $or: [ // Aplica el operador `$or` para que la búsqueda sea sobre múltiples campos.
        { title: { $regex: new RegExp(query, "i") } }, // Busca coincidencias en el campo `title`.
        { description: { $regex: new RegExp(query, "i") } }, // Busca coincidencias en el campo `description`.
      /* `$regex`:
       - Busca valores que coincidan con la expresión regular.
       - Se utiliza `new RegExp(query, "i")` para crear la expresión regular.
       - `"i"` hace que la búsqueda sea insensible a mayúsculas/minúsculas.
      */
      ],
    });

    res.status(200).json({
      error: false, // Indica que no hubo errores en la solicitud.
      todos: matchedNotes, // Devuelve las notas que coinciden con la búsqueda.
      message: "Notas encontradas con éxito"
    })
  } catch (error) {
    next(error); // Si ocurre un error durante la búsqueda, llama al middleware de manejo de errores.
  }
};
