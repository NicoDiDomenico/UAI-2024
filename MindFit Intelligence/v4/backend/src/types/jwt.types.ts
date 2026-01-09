export interface JwtPayload {
  usuarioId: number;
  gymId: number;
}

export interface AuthUser {
  usuarioId: number;
  nombreUsuario: string;
  gym: {
    gymId: number;
    nombre: string;
  };
  persona: {
    personaId: number;
    nombreYApellido: string;
    email: string;
  };
  roles: string[];
  permisos: string[];
}
