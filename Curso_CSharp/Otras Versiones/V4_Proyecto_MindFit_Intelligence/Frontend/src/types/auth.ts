export interface LoginRequest {
  username: string
  password: string
}

export interface ForgotPasswordRequest {
  email: string
}

export interface ResetPasswordRequest {
  tokenPlano: string
  newPassword: string
}

export interface TokenResponse {
  accessToken: string
  refreshToken: string
  permisos: string[]
}

export interface AuthSession extends TokenResponse {
  idGym: number
}

export interface LoginCredentials extends LoginRequest {
  idGym: number
}
