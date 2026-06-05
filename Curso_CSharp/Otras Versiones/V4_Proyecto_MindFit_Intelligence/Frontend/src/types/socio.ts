export type EstadoSocio = 'Nuevo' | 'Actualizado' | 'Suspendido' | 'Eliminado' | string

export interface SocioGridItem {
  idUsuario: number
  username: string
  fechaRegistro: string
  nombreCompleto: string | null
  email: string | null
  estadoSocio: EstadoSocio
  plan: string | null
  fechaFinPeriodo: string | null
}

export interface ProcesarEliminacionesResponse {
  sociosElimidados: number
  idsProcesados: number[]
  mensaje: string
  fechaEjecucion: string
}

