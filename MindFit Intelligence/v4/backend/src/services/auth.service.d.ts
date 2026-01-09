import type { AuthUser } from "../types/jwt.types.js";
export declare class AuthService {
    login(gymId: number, nombreUsuario: string, password: string): Promise<{
        token: string;
        usuario: AuthUser;
    }>;
    getUserWithPermissions(usuarioId: number, gymId: number): Promise<AuthUser>;
}
//# sourceMappingURL=auth.service.d.ts.map