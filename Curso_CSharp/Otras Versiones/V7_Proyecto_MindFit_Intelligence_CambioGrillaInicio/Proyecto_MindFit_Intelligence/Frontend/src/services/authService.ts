import { apiClient } from './apiClient'
import type {
  ForgotPasswordRequest,
  LoginRequest,
  ResetPasswordRequest,
  TokenResponse,
} from '../types/auth'

export const authService = {
  async login(request: LoginRequest, idGym: number) {
    const response = await apiClient.post<TokenResponse>('/Auth/login', request, {
      headers: {
        'X-Gym-Id': String(idGym),
      },
    })

    return response.data
  },

  async forgotPassword(request: ForgotPasswordRequest, idGym: number) {
    const response = await apiClient.post<string>('/Auth/forgot-password', request, {
      headers: {
        'X-Gym-Id': String(idGym),
      },
    })

    return response.data
  },

  async resetPassword(request: ResetPasswordRequest, idGym: number) {
    const response = await apiClient.post<string>('/Auth/reset-password', request, {
      headers: {
        'X-Gym-Id': String(idGym),
      },
    })

    return response.data
  },
}
