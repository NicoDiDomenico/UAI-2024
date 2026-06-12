import { apiClient } from './apiClient'
import type { GymPublico } from '../types/gym'

export const gymsService = {
  async getActiveGyms() {
    const response = await apiClient.get<GymPublico[]>('/Gyms/activos')
    return response.data
  },
}
