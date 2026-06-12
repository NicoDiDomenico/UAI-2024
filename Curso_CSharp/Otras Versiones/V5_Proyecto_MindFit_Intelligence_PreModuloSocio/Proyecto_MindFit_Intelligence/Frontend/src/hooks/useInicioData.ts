import { useEffect, useState } from 'react'
import { formulariosService } from '../services/formulariosService'
import { turnosService } from '../services/turnosService'
import type { Formulario } from '../types/formulario'
import type { TurnoDetalle } from '../types/turno'
import { formatLocalDateForApi } from '../utils/date'
import { getInicioErrorMessage } from '../utils/apiError'

export function useInicioData() {
  const [formularios, setFormularios] = useState<Formulario[]>([])
  const [turnos, setTurnos] = useState<TurnoDetalle[]>([])
  const [isLoadingFormularios, setIsLoadingFormularios] = useState(true)
  const [isLoadingTurnos, setIsLoadingTurnos] = useState(true)
  const [formulariosError, setFormulariosError] = useState('')
  const [turnosError, setTurnosError] = useState('')
  const today = new Date()
  const fecha = formatLocalDateForApi(today)

  useEffect(() => {
    let isActive = true

    async function loadFormularios() {
      try {
        const response = await formulariosService.getAll()
        if (isActive) setFormularios(response)
      } catch (error) {
        if (isActive) setFormulariosError(getInicioErrorMessage(error, 'formularios'))
      } finally {
        if (isActive) setIsLoadingFormularios(false)
      }
    }

    async function loadTurnos() {
      try {
        const response = await turnosService.getInicioGridByDate(fecha)
        if (isActive) setTurnos(response)
      } catch (error) {
        if (isActive) setTurnosError(getInicioErrorMessage(error, 'turnos'))
      } finally {
        if (isActive) setIsLoadingTurnos(false)
      }
    }

    void loadFormularios()
    void loadTurnos()

    return () => {
      isActive = false
    }
  }, [fecha])

  return {
    formularios,
    turnos,
    today,
    isLoadingFormularios,
    isLoadingTurnos,
    formulariosError,
    turnosError,
  }
}
