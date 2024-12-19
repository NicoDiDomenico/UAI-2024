import express from "express";
const todoRouter = express.Router(); // Creacion de subrutas para app.use("/api/todos", todoRouter); en app.ts
import validateSchema from "../middlewares/schemaValidation.middleware";
import { createTodo, deleteTodo, editTodo, getEveryTodo, getTodo, searchNotes, updateIsPinned } from "../controllers";
import { createTodoSchema } from "../controllers/schemaValidations/todo.schema";
import { checkAuth } from "../middlewares/checkAuth.middleware";

// Subrutas del router
todoRouter.post("/", checkAuth, validateSchema(createTodoSchema), createTodo);
todoRouter.get("/search", checkAuth, searchNotes)
todoRouter.get("/", checkAuth, getEveryTodo);
todoRouter.get("/:id", checkAuth, getTodo);
todoRouter.put("/pin-todo/:id", checkAuth, updateIsPinned);
todoRouter.put("/:id", checkAuth, editTodo);
todoRouter.delete("/:id", checkAuth, deleteTodo);
/* 
"/:id"    --> req.params.id dice "qué recurso quiero modificar".
checkAuth --> req.user dice "¿tengo permiso para modificar este recurso?".
*/

export { todoRouter };

/*
Métodos disponibles en las subrutas:

1. GET: CRUD - R: Read (Leer/Obtener)
   - Propósito: Obtener recursos (solo lectura).
   - Ejemplo: userRouter.get("/profile", (req, res) => { ... });

2. POST: CRUD - C: Create (Crear)
   - Propósito: Crear nuevos recursos.
   - Ejemplo: userRouter.post("/register", (req, res) => { ... });

3. PUT: CRUD - U: Update (Actualizar) --> Este se usa mas que PATCH
   - Propósito: Actualizar completamente un recurso existente.
   - Ejemplo: userRouter.put("/update", (req, res) => { ... });

4. PATCH: CRUD - U: Update (Actualizar)
   - Propósito: Actualizar parcialmente un recurso existente.
   - Ejemplo: userRouter.patch("/update", (req, res) => { ... });

5. DELETE: CRUD - D: Delete (Eliminar)
   - Propósito: Eliminar un recurso.
   - Ejemplo: userRouter.delete("/:id", (req, res) => { ... });

6. ALL:
   - Propósito: Manejar cualquier método HTTP en una ruta.
   - Ejemplo: userRouter.all("/health", (req, res) => { ... });

7. OPTIONS:
   - Propósito: Consultar los métodos HTTP permitidos en una ruta.
   - Ejemplo: userRouter.options("/profile", (req, res) => { ... });

8. HEAD:
   - Propósito: Similar a GET, pero solo devuelve los encabezados (sin el cuerpo).
   - Ejemplo: userRouter.head("/profile", (req, res) => { ... });

Estos métodos te permiten crear rutas específicas para diferentes acciones
según el tipo de solicitud HTTP, manteniendo tu código organizado y funcional.
*/
