"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.todoRouter = void 0;
const express_1 = __importDefault(require("express"));
const todoRouter = express_1.default.Router(); // Creacion de subrutas para app.use("/api/todos", todoRouter); en app.ts
exports.todoRouter = todoRouter;
const schemaValidation_middleware_1 = __importDefault(require("../middlewares/schemaValidation.middleware"));
const controllers_1 = require("../controllers");
const todo_schema_1 = require("../controllers/schemaValidations/todo.schema");
const checkAuth_middleware_1 = require("../middlewares/checkAuth.middleware");
// Subrutas del router
todoRouter.post("/", checkAuth_middleware_1.checkAuth, (0, schemaValidation_middleware_1.default)(todo_schema_1.createTodoSchema), controllers_1.createTodo);
todoRouter.get("/", checkAuth_middleware_1.checkAuth, controllers_1.getEveryTodo);
todoRouter.get("/:id", checkAuth_middleware_1.checkAuth, controllers_1.getTodo);
todoRouter.put("/:id", checkAuth_middleware_1.checkAuth, controllers_1.editTodo);
todoRouter.delete("/:id", checkAuth_middleware_1.checkAuth, controllers_1.deleteTodo);
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
