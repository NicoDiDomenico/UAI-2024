import type { ProcesarEliminacionesResponse, SocioGridItem } from '../types/socio'
import { apiClient } from './apiClient'

export const sociosService = {
  async actualizarCuotasVencidas() {
    await apiClient.put('/Cuota/actualizar-vencidas')
  },

  async procesarEliminacionesPendientes() {
    const response = await apiClient.patch<ProcesarEliminacionesResponse>(
      '/Usuario/procesar-eliminaciones-pendientes',
    )

    return response.data
  },

  async getSociosGrid() {
    const response = await apiClient.get<SocioGridItem[]>('/Usuario/grilla-socio')
    return response.data
  },
}

