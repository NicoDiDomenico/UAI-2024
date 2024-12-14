import { NextFunction, Response, Request } from "express";
import { Todo } from "../models/todo.model";
import { ErrorResponse } from "../middlewares/errorHandler.middleware";

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
  const { isPinned } = req.body;
  const { id } = req.params;

  try {
    const todo = await Todo.findOne({ _id: id, user: req.user });

    if (!todo) return next(new ErrorResponse("Nota no encontrada", 404));

    if (isPinned) todo.isPinned = isPinned || false;

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