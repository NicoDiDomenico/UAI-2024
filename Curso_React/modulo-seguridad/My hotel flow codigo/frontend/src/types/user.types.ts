/**
 * Types de Usuarios
 * Siguiendo MEJORES_PRACTICAS.md - Type safety
 */

// Roles de usuario
export enum UserRole {
  ADMIN = 'admin',
  RECEPCIONISTA = 'recepcionista',
  CLIENTE = 'cliente',
}

// Importar tipos de grupo y acción
export interface Group {
  id: number;
  key: string;
  name: string;
  description?: string;
}

export interface Action {
  id: number;
  key: string;
  name: string;
  description?: string;
}

export interface User {
  id: number;
  username: string;
  email: string;
  fullName?: string;
  role: string;
  isActive: boolean;
  lastLoginAt?: string;
  groups?: Group[];
  actions?: Action[];
  createdAt: string;
  updatedAt: string;
}

export interface CreateUserPayload {
  username: string;
  email: string;
  password?: string;
  fullName?: string;
  role?: UserRole;
  isActive?: boolean;
}

export interface UpdateUserPayload {
  username?: string;
  email?: string;
  fullName?: string;
  role?: UserRole;
  isActive?: boolean;
}

export interface SetUserGroupsPayload {
  groupKeys: string[];
}

export interface SetUserActionsPayload {
  actionKeys: string[];
}

export interface ResetPasswordPayload {
  newPassword: string;
}
