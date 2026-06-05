import axios from 'axios'
import type { TurnoDetalle } from '../types/turno'
import { apiClient } from './apiClient'

export const turnosService = {
  async getInicioGridByDate(fecha: string) {
    try {
      const response = await apiClient.get<TurnoDetalle[]>('/Turno/inicio/grilla-fecha', {
        params: { fecha },
      })

      return response.data
    } catch (error) {
      if (axios.isAxiosError(error) && error.response?.status === 404) {
        return []
      }

      throw error
    }
  },
}
