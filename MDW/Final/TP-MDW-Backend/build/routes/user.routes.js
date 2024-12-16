"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.userRouter = void 0;
const express_1 = __importDefault(require("express"));
const userRouter = express_1.default.Router(); // instancia independiente de un router, es una instancia más pequeña que hereda muchos métodos de app. Es como un mini-servidor o subservidor que se utiliza para organizar y definir rutas relacionadas con una funcionalidad específica (por ejemplo, "usuarios").
exports.userRouter = userRouter;
/* Creacion de subrutas para app.use("/api/users", userRouter); en app.ts
Crea un router independiente para manejar las rutas relacionadas con usuarios.
`express.Router()` permite modularizar las rutas, haciendo el código más organizado y escalable.
Este router será conectado a la aplicación principal con un prefijo, por ejemplo: "/api/users".
*/
const controllers_1 = require("../controllers");
const schemaValidation_middleware_1 = __importDefault(require("../middlewares/schemaValidation.middleware"));
const schemaValidations_1 = require("../controllers/schemaValidations");
const checkAuth_middleware_1 = require("../middlewares/checkAuth.middleware");
// Subrutas del router
userRouter.post("/register", (0, schemaValidation_middleware_1.default)(schemaValidations_1.registerSchema), controllers_1.register); // Middlewares para validar esquemas de datos enviados en req.body (schemaValidation.middleware.ts).
userRouter.post("/login", (0, schemaValidation_middleware_1.default)(schemaValidations_1.loginSchema), controllers_1.login);
userRouter.get("/user-info", checkAuth_middleware_1.checkAuth, controllers_1.getUser); // Middlewares para verificar que el usuario esté autenticado mediante un token JWT (checkAuth.middleware.ts).
/* También se podría exportar como export default userRouter;
   --> Con export default, solo puedes exportar una cosa principal desde el archivo.
   --> Además, al importar un export default, puedes usar cualquier nombre, mientras que con export { userRouter }
       (exportación nombrada), debes respetar exactamente el nombre de lo exportado. */
