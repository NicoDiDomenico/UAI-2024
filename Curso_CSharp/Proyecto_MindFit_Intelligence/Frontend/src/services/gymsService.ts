import { apiClient } from './apiClient'
import type { GymPublico } from '../types/gym'
import type { GymOnboardingRequest, GymOnboardingResponse } from '../types/gymOnboarding'

export const gymsService = {
  async getActiveGyms() {
    const response = await apiClient.get<GymPublico[]>('/Gyms/activos')
    return response.data
  },

  async registrarGymOnboarding(payload: GymOnboardingRequest) {
    const response = await apiClient.post<GymOnboardingResponse>('/Gyms/onboarding', payload, {
      skipGymId: true,
    })

    return response.data
  },
}
