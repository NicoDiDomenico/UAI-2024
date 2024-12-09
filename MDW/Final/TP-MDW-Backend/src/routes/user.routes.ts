import express from "express";
const userRouter = express.Router(); // instancia independiente de un router, es una instancia más pequeña que hereda muchos métodos de app. Es como un mini-servidor o subservidor que se utiliza para organizar y definir rutas relacionadas con una funcionalidad específica (por ejemplo, "usuarios").
/* Creacion de subrutas para app.use("/api/users", userRouter); en app.ts
Crea un router independiente para manejar las rutas relacionadas con usuarios.
`express.Router()` permite modularizar las rutas, haciendo el código más organizado y escalable.
Este router será conectado a la aplicación principal con un prefijo, por ejemplo: "/api/users".
*/
import { getUser, login, register } from "../controllers";
import validateSchema from "../middlewares/schemaValidation.middleware";
import { loginSchema, registerSchema } from "../controllers/schemaValidations";
import { checkAuth } from "../middlewares/checkAuth.middleware";

// Subrutas del router
userRouter.post("/register", validateSchema(registerSchema), register);
userRouter.post("/login", validateSchema(loginSchema), login);
userRouter.get("/user-info", checkAuth, getUser);


export {userRouter};
/* También se podría exportar como export default userRouter;
   --> Con export default, solo puedes exportar una cosa principal desde el archivo.
   --> Además, al importar un export default, puedes usar cualquier nombre, mientras que con export { userRouter } 
       (exportación nombrada), debes respetar exactamente el nombre de lo exportado. */
