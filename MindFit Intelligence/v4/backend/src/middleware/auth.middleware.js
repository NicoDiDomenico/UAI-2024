import { verifyToken } from "../utils/jwt.util.js";
export const authMiddleware = (req, res, next) => {
    try {
        const authHeader = req.headers.authorization;
        if (!authHeader || !authHeader.startsWith("Bearer ")) {
            res.status(401).json({ error: "Token no proporcionado" });
            return;
        }
        const token = authHeader.substring(7); // Remover "Bearer "
        const payload = verifyToken(token);
        // Agregar payload al request para uso posterior
        req.user = payload;
        next();
    }
    catch (error) {
        console.error("Error en auth middleware:", error);
        res.status(401).json({ error: "Token inválido o expirado" });
    }
};
//# sourceMappingURL=auth.middleware.js.map