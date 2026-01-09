import { AuthService } from "../services/auth.service.js";
const authService = new AuthService();
export const login = async (req, res) => {
    try {
        const { gymId, nombreUsuario, password } = req.body;
        // Validar campos requeridos
        if (!gymId || !nombreUsuario || !password) {
            res.status(400).json({
                error: "gymId, nombreUsuario y password son requeridos",
            });
            return;
        }
        const result = await authService.login(gymId, nombreUsuario, password);
        res.json(result);
    }
    catch (error) {
        console.error("Error en login:", error);
        const message = error instanceof Error ? error.message : "Error al iniciar sesión";
        if (message === "Credenciales inválidas") {
            res.status(401).json({ error: message });
            return;
        }
        res.status(500).json({ error: "Error al iniciar sesión" });
    }
};
export const getCurrentUser = async (req, res) => {
    try {
        // El middleware auth ya validó el token y agregó user a req
        const { usuarioId, gymId } = req.user;
        const usuario = await authService.getUserWithPermissions(usuarioId, gymId);
        res.json({ usuario });
    }
    catch (error) {
        console.error("Error al obtener usuario actual:", error);
        res.status(500).json({ error: "Error al obtener usuario actual" });
    }
};
//# sourceMappingURL=auth.controller.js.map