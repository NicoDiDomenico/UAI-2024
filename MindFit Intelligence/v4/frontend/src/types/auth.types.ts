export interface Gym {
  gymId: number;
  nombre: string;
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

export interface LoginRequest {
  gymId: number;
  nombreUsuario: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  usuario: AuthUser;
}
