import axios from 'axios'
import { useCallback, useEffect, useMemo, useRef, useState } from 'react'
import { BackToHomeLink } from '../components/BackToHomeLink'
import { useAuth } from '../hooks/useAuth'
import { AppLayout } from '../layouts/AppLayout'
import { rutinasService } from '../services/rutinasService'
import type {
  CalentamientoDto,
  DiaDto,
  EjercicioDto,
  EntrenadorRutinaDto,
  EntrenamientoDto,
  EstiramientoDto,
  GrupoMuscularDto,
  RangoHorarioDto,
  RutinaBloquesUpdateDto,
  RutinaDto,
  RutinaHistorialDetalleDto,
  RutinaHistorialResumenDto,
  SocioTurnoDto,
} from '../types/rutina'
import { getApiErrorMessage } from '../utils/apiError'

const EDITAR_RUTINA = 'EDITAR_RUTINA'
const ELIMINAR_RUTINA = 'ELIMINAR_RUTINA'
const VER_HISTORIAL_RUTINA = 'VER_HISTORIAL_RUTINA'
const RECUPERAR_RUTINA = 'RECUPERAR_RUTINA'
const DEFAULT_NO_RUTINA_MESSAGE = 'El socio no asiste este dia'

type RutinaBlockType = 'calentamientos' | 'entrenamientos' | 'estiramientos'

interface RutinaDraftRow {
  uid: string
  idOrigen: number | null
  idGrupoMuscular: number | null
  grupoMuscularLabel: string
  idEjercicio: number | null
  ejercicioLabel: string
  duracion: string
  series: string
  repeticiones: string
  pesoAsignado: string
  tiempoDescansoSegundos: string
  orden: string
  observaciones: string
}

interface RutinaDraft {
  idRutina: number
  fechaModificacion: string
  calentamientos: RutinaDraftRow[]
  entrenamientos: RutinaDraftRow[]
  estiramientos: RutinaDraftRow[]
}

type RutinaModal =
  | { type: 'info'; title: string; message: string }
  | { type: 'confirm-delete'; title: string; message: string }

const BLOCK_LABELS: Record<RutinaBlockType, string> = {
  calentamientos: 'Calentamiento',
  entrenamientos: 'Entrenamiento',
  estiramientos: 'Estiramiento',
}

function formatTime(value: string) {
  return value.slice(0, 5)
}

function formatRango(rango: RangoHorarioDto) {
  return `${formatTime(rango.horaDesde)} - ${formatTime(rango.horaHasta)}`
}

function parseTimeToMinutes(value: string) {
  const [hours = '0', minutes = '0'] = value.split(':')
  return Number(hours) * 60 + Number(minutes)
}

function getCurrentRango(rangos: RangoHorarioDto[], now = new Date()) {
  const currentMinutes = now.getHours() * 60 + now.getMinutes() + now.getSeconds() / 60

  return (
    rangos.find((rango) => {
      const startMinutes = parseTimeToMinutes(rango.horaDesde)
      const endMinutes = parseTimeToMinutes(rango.horaHasta)

      if (startMinutes < endMinutes) {
        return currentMinutes >= startMinutes && currentMinutes < endMinutes
      }

      if (startMinutes > endMinutes) {
        return currentMinutes >= startMinutes || currentMinutes < endMinutes
      }

      return false
    }) ?? null
  )
}

function getFullName(persona: EntrenadorRutinaDto | SocioTurnoDto) {
  return `${persona.nombre} ${persona.apellido}`.trim()
}

function hasPermission(userPermissions: readonly string[], permission: string) {
  return userPermissions.includes(permission)
}

function normalizeDayName(value: string) {
  return value
    .normalize('NFD')
    .replace(/[\u0300-\u036f]/g, '')
    .toLowerCase()
    .trim()
}

function getCurrentDia(dias: DiaDto[], now = new Date()) {
  const dayNames = [
    'domingo',
    'lunes',
    'martes',
    'miercoles',
    'jueves',
    'viernes',
    'sabado',
  ]
  const currentDayName = dayNames[now.getDay()]

  return (
    dias.find((dia) => normalizeDayName(dia.nombreDia) === currentDayName) ??
    dias[0] ??
    null
  )
}

function createEmptyDraftRow(order: number): RutinaDraftRow {
  return {
    uid: `draft-${Date.now()}-${Math.random().toString(36).slice(2)}`,
    idOrigen: null,
    idGrupoMuscular: null,
    grupoMuscularLabel: '',
    idEjercicio: null,
    ejercicioLabel: '',
    duracion: '',
    series: '',
    repeticiones: '',
    pesoAsignado: '',
    tiempoDescansoSegundos: '',
    orden: String(order),
    observaciones: '',
  }
}

function getDraftRowOrder(row: RutinaDraftRow) {
  const order = Number(row.orden)
  return Number.isFinite(order) && order > 0 ? order : Number.MAX_SAFE_INTEGER
}

function normalizeDraftRows(rows: RutinaDraftRow[]) {
  return [...rows]
    .sort((first, second) => getDraftRowOrder(first) - getDraftRowOrder(second))
    .map((row, index) => ({
      ...row,
      orden: String(index + 1),
    }))
}

function baseRowFromExercise(
  idOrigen: number,
  ejercicio: EjercicioDto,
  orden: number,
  observaciones: string | null,
): RutinaDraftRow {
  return {
    ...createEmptyDraftRow(orden),
    uid: `existing-${idOrigen}-${Date.now()}-${Math.random().toString(36).slice(2)}`,
    idOrigen,
    idGrupoMuscular: ejercicio.grupoMuscular.idGrupoMuscular,
    grupoMuscularLabel: ejercicio.grupoMuscular.nombreMusculo,
    idEjercicio: ejercicio.idEjercicio,
    ejercicioLabel: ejercicio.descEjercicio,
    observaciones: observaciones ?? '',
  }
}

function calentamientoToDraftRow(item: CalentamientoDto): RutinaDraftRow {
  return {
    ...baseRowFromExercise(
      item.idCalentamiento,
      item.ejercicio,
      item.orden,
      item.observaciones,
    ),
    duracion: String(item.duracion),
  }
}

function entrenamientoToDraftRow(item: EntrenamientoDto): RutinaDraftRow {
  return {
    ...baseRowFromExercise(
      item.idEntrenamiento,
      item.ejercicio,
      item.orden,
      item.observaciones,
    ),
    series: String(item.series),
    repeticiones: String(item.repeticiones),
    pesoAsignado: item.pesoAsignado === null ? '' : String(item.pesoAsignado),
    tiempoDescansoSegundos:
      item.tiempoDescansoSegundos === null ? '' : String(item.tiempoDescansoSegundos),
  }
}

function estiramientoToDraftRow(item: EstiramientoDto): RutinaDraftRow {
  return {
    ...baseRowFromExercise(
      item.idEstiramiento,
      item.ejercicio,
      item.orden,
      item.observaciones,
    ),
    duracion: String(item.duracion),
  }
}

function rutinaToDraft(rutina: RutinaDto): RutinaDraft {
  return {
    idRutina: rutina.idRutina,
    fechaModificacion: rutina.fechaModificacion,
    calentamientos: normalizeDraftRows(rutina.calentamientos.map(calentamientoToDraftRow)),
    entrenamientos: normalizeDraftRows(rutina.entrenamientos.map(entrenamientoToDraftRow)),
    estiramientos: normalizeDraftRows(rutina.estiramientos.map(estiramientoToDraftRow)),
  }
}

function formatDate(value: string) {
  const parsed = new Date(value)

  if (Number.isNaN(parsed.getTime())) {
    return value
  }

  return new Intl.DateTimeFormat('es-AR', {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
  }).format(parsed)
}

function formatDateTime(value: string) {
  const parsed = new Date(value)

  if (Number.isNaN(parsed.getTime())) {
    return value
  }

  return new Intl.DateTimeFormat('es-AR', {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
  }).format(parsed)
}

function formatSnapshotDate(value: string) {
  const parsed = new Date(value)

  if (Number.isNaN(parsed.getTime())) {
    return value
  }

  return new Intl.DateTimeFormat('es-AR', {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
  }).format(parsed)
}

function formatSnapshotTime(value: string) {
  const parsed = new Date(value)

  if (Number.isNaN(parsed.getTime())) {
    return value
  }

  return new Intl.DateTimeFormat('es-AR', {
    hour: '2-digit',
    minute: '2-digit',
  }).format(parsed)
}

function formatHistoryVersion(version: number) {
  return `Version ${version.toFixed(1)}`
}

function getExerciseLabel(idEjercicio: number, ejerciciosById: Map<number, EjercicioDto>) {
  return ejerciciosById.get(idEjercicio)?.descEjercicio ?? `Ejercicio #${idEjercicio}`
}

function toRequiredNumber(value: string, label: string, errors: string[]) {
  const numberValue = Number(value)

  if (!value.trim() || !Number.isFinite(numberValue) || numberValue <= 0) {
    errors.push(label)
    return 0
  }

  return numberValue
}

function toOptionalNumber(value: string) {
  if (!value.trim()) {
    return null
  }

  const numberValue = Number(value)
  return Number.isFinite(numberValue) ? numberValue : null
}

function normalizeObservation(value: string) {
  const normalized = value.trim()
  return normalized.length > 0 ? normalized : null
}

function buildRutinaPayload(draft: RutinaDraft): {
  payload: RutinaBloquesUpdateDto
  errors: string[]
} {
  const errors: string[] = []

  const calentamientos = normalizeDraftRows(draft.calentamientos).map((row) => {
    if (row.idEjercicio === null) {
      errors.push(`Calentamiento linea ${row.orden}: selecciona un ejercicio.`)
    }

    return {
      idEjercicio: row.idEjercicio ?? 0,
      duracion: toRequiredNumber(
        row.duracion,
        `Calentamiento linea ${row.orden}: duracion requerida.`,
        errors,
      ),
      orden: Number(row.orden),
      observaciones: normalizeObservation(row.observaciones),
    }
  })

  const entrenamientos = normalizeDraftRows(draft.entrenamientos).map((row) => {
    if (row.idEjercicio === null) {
      errors.push(`Entrenamiento linea ${row.orden}: selecciona un ejercicio.`)
    }

    return {
      idEjercicio: row.idEjercicio ?? 0,
      series: toRequiredNumber(
        row.series,
        `Entrenamiento linea ${row.orden}: series requeridas.`,
        errors,
      ),
      repeticiones: toRequiredNumber(
        row.repeticiones,
        `Entrenamiento linea ${row.orden}: repeticiones requeridas.`,
        errors,
      ),
      pesoAsignado: toOptionalNumber(row.pesoAsignado),
      tiempoDescansoSegundos: toOptionalNumber(row.tiempoDescansoSegundos),
      orden: Number(row.orden),
      observaciones: normalizeObservation(row.observaciones),
    }
  })

  const estiramientos = normalizeDraftRows(draft.estiramientos).map((row) => {
    if (row.idEjercicio === null) {
      errors.push(`Estiramiento linea ${row.orden}: selecciona un ejercicio.`)
    }

    return {
      idEjercicio: row.idEjercicio ?? 0,
      duracion: toRequiredNumber(
        row.duracion,
        `Estiramiento linea ${row.orden}: duracion requerida.`,
        errors,
      ),
      orden: Number(row.orden),
      observaciones: normalizeObservation(row.observaciones),
    }
  })

  return {
    payload: {
      calentamientos,
      entrenamientos,
      estiramientos,
    },
    errors,
  }
}

export function GestionRutinasPage() {
  const { session } = useAuth()
  const userPermissions = session?.permisos ?? []
  const canEditRutina = hasPermission(userPermissions, EDITAR_RUTINA)
  const canDeleteRutina = hasPermission(userPermissions, ELIMINAR_RUTINA)
  const canViewHistorial = hasPermission(userPermissions, VER_HISTORIAL_RUTINA)
  const canRestoreRutina = hasPermission(userPermissions, RECUPERAR_RUTINA)

  const [rangos, setRangos] = useState<RangoHorarioDto[]>([])
  const [entrenadores, setEntrenadores] = useState<EntrenadorRutinaDto[]>([])
  const [socios, setSocios] = useState<SocioTurnoDto[]>([])
  const [dias, setDias] = useState<DiaDto[]>([])
  const [gruposMusculares, setGruposMusculares] = useState<GrupoMuscularDto[]>([])
  const [ejerciciosByGrupo, setEjerciciosByGrupo] = useState<Record<number, EjercicioDto[]>>({})
  const [selectedRangoId, setSelectedRangoId] = useState<number | null>(null)
  const [selectedEntrenadorId, setSelectedEntrenadorId] = useState<number | null>(null)
  const [selectedSocioId, setSelectedSocioId] = useState<number | null>(null)
  const [selectedDiaId, setSelectedDiaId] = useState<number | null>(null)
  const [rutinaDraft, setRutinaDraft] = useState<RutinaDraft | null>(null)
  const [isLoadingRangos, setIsLoadingRangos] = useState(true)
  const [isLoadingEntrenadores, setIsLoadingEntrenadores] = useState(false)
  const [isLoadingSocios, setIsLoadingSocios] = useState(false)
  const [isLoadingDias, setIsLoadingDias] = useState(false)
  const [isLoadingGrupos, setIsLoadingGrupos] = useState(false)
  const [isLoadingRutina, setIsLoadingRutina] = useState(false)
  const [loadingEjercicioGroupIds, setLoadingEjercicioGroupIds] = useState<number[]>([])
  const [rangosError, setRangosError] = useState('')
  const [entrenadoresError, setEntrenadoresError] = useState('')
  const [sociosError, setSociosError] = useState('')
  const [diasError, setDiasError] = useState('')
  const [gruposError, setGruposError] = useState('')
  const [rutinaError, setRutinaError] = useState('')
  const [noRutinaMessage, setNoRutinaMessage] = useState('')
  const [actionModal, setActionModal] = useState<RutinaModal | null>(null)
  const [isSavingRutina, setIsSavingRutina] = useState(false)
  const [isDeletingRutina, setIsDeletingRutina] = useState(false)
  const [isHistoryOpen, setIsHistoryOpen] = useState(false)
  const [historialResumen, setHistorialResumen] = useState<RutinaHistorialResumenDto[]>([])
  const [historialDetalle, setHistorialDetalle] =
    useState<RutinaHistorialDetalleDto | null>(null)
  const [selectedHistorialId, setSelectedHistorialId] = useState<number | null>(null)
  const [historialRutinaId, setHistorialRutinaId] = useState<number | null>(null)
  const [historialDiaId, setHistorialDiaId] = useState<number | null>(null)
  const [historialNoRutinaMessage, setHistorialNoRutinaMessage] = useState('')
  const [isLoadingHistorial, setIsLoadingHistorial] = useState(false)
  const [isLoadingHistorialDetalle, setIsLoadingHistorialDetalle] = useState(false)
  const [isRestoringHistorial, setIsRestoringHistorial] = useState(false)
  const [historialError, setHistorialError] = useState('')
  const [historialDetalleError, setHistorialDetalleError] = useState('')
  const [ejerciciosCatalogo, setEjerciciosCatalogo] = useState<EjercicioDto[]>([])
  const entrenadoresRequestId = useRef(0)
  const sociosRequestId = useRef(0)
  const rutinaRequestId = useRef(0)
  const diasRef = useRef<DiaDto[]>([])
  const gruposRef = useRef<GrupoMuscularDto[]>([])
  const ejerciciosByGrupoRef = useRef<Record<number, EjercicioDto[]>>({})
  const ejerciciosCatalogoRef = useRef<EjercicioDto[]>([])
  const loadingEjerciciosRef = useRef(new Set<number>())
  const nextNoRutinaMessageRef = useRef(DEFAULT_NO_RUTINA_MESSAGE)

  const selectedRango = useMemo(
    () => rangos.find((rango) => rango.idRangoHorario === selectedRangoId) ?? null,
    [rangos, selectedRangoId],
  )
  const selectedEntrenador = useMemo(
    () =>
      entrenadores.find((entrenador) => entrenador.idUsuario === selectedEntrenadorId) ?? null,
    [entrenadores, selectedEntrenadorId],
  )
  const selectedSocio = useMemo(
    () => socios.find((socio) => socio.idUsuario === selectedSocioId) ?? null,
    [socios, selectedSocioId],
  )
  const ejerciciosById = useMemo(
    () => new Map(ejerciciosCatalogo.map((ejercicio) => [ejercicio.idEjercicio, ejercicio])),
    [ejerciciosCatalogo],
  )

  const resetRutinaWorkspace = useCallback(() => {
    rutinaRequestId.current += 1
    setSelectedDiaId(null)
    setRutinaDraft(null)
    setRutinaError('')
    setNoRutinaMessage('')
    setIsLoadingRutina(false)
  }, [])

  const loadGruposMusculares = useCallback(async () => {
    if (gruposRef.current.length > 0) {
      return gruposRef.current
    }

    setIsLoadingGrupos(true)
    setGruposError('')

    try {
      const response = await rutinasService.getGruposMusculares()
      gruposRef.current = response
      setGruposMusculares(response)
      return response
    } catch (error) {
      setGruposError(
        getApiErrorMessage(error, 'No pudimos cargar los grupos musculares.'),
      )
      return []
    } finally {
      setIsLoadingGrupos(false)
    }
  }, [])

  const ensureEjerciciosByGrupo = useCallback(async (idGrupoMuscular: number) => {
    if (ejerciciosByGrupoRef.current[idGrupoMuscular]) {
      return ejerciciosByGrupoRef.current[idGrupoMuscular]
    }

    if (loadingEjerciciosRef.current.has(idGrupoMuscular)) {
      return []
    }

    loadingEjerciciosRef.current.add(idGrupoMuscular)
    setLoadingEjercicioGroupIds((current) => [...current, idGrupoMuscular])

    try {
      const response = await rutinasService.getEjerciciosPorGrupoMuscular(idGrupoMuscular)
      ejerciciosByGrupoRef.current = {
        ...ejerciciosByGrupoRef.current,
        [idGrupoMuscular]: response,
      }
      setEjerciciosByGrupo(ejerciciosByGrupoRef.current)
      return response
    } catch {
      return []
    } finally {
      loadingEjerciciosRef.current.delete(idGrupoMuscular)
      setLoadingEjercicioGroupIds((current) =>
        current.filter((id) => id !== idGrupoMuscular),
      )
    }
  }, [])

  const loadEjerciciosCatalogo = useCallback(async () => {
    if (ejerciciosCatalogoRef.current.length > 0) {
      return ejerciciosCatalogoRef.current
    }

    try {
      const response = await rutinasService.getEjercicios()
      ejerciciosCatalogoRef.current = response
      setEjerciciosCatalogo(response)
      return response
    } catch {
      return []
    }
  }, [])

  const warmUpEjerciciosFromDraft = useCallback(
    (draft: RutinaDraft) => {
      const ids = new Set<number>()

      ;(['calentamientos', 'entrenamientos', 'estiramientos'] as RutinaBlockType[]).forEach(
        (blockType) => {
          draft[blockType].forEach((row) => {
            if (row.idGrupoMuscular !== null) {
              ids.add(row.idGrupoMuscular)
            }
          })
        },
      )

      ids.forEach((idGrupoMuscular) => {
        void ensureEjerciciosByGrupo(idGrupoMuscular)
      })
    },
    [ensureEjerciciosByGrupo],
  )

  const loadRutina = useCallback(
    async (idUsuarioSocio: number, idDia: number) => {
      const requestId = ++rutinaRequestId.current
      setIsLoadingRutina(true)
      setRutinaError('')
      setNoRutinaMessage('')
      setRutinaDraft(null)

      try {
        const response = await rutinasService.getRutinaPorSocioYDia(idUsuarioSocio, idDia)

        if (requestId === rutinaRequestId.current) {
          const draft = rutinaToDraft(response)
          setRutinaDraft(draft)
          warmUpEjerciciosFromDraft(draft)
        }
      } catch (error) {
        if (requestId !== rutinaRequestId.current) {
          return
        }

        if (axios.isAxiosError(error) && error.response?.status === 404) {
          setNoRutinaMessage(nextNoRutinaMessageRef.current)
          nextNoRutinaMessageRef.current = DEFAULT_NO_RUTINA_MESSAGE
          return
        }

        setRutinaError(
          getApiErrorMessage(error, 'No pudimos cargar la rutina para el dia seleccionado.'),
        )
      } finally {
        if (requestId === rutinaRequestId.current) {
          setIsLoadingRutina(false)
        }
      }
    },
    [warmUpEjerciciosFromDraft],
  )

  const loadDias = useCallback(async () => {
    if (diasRef.current.length > 0) {
      return diasRef.current
    }

    setIsLoadingDias(true)
    setDiasError('')

    try {
      const response = await rutinasService.getDias()
      diasRef.current = response
      setDias(response)
      return response
    } catch (error) {
      setDiasError(getApiErrorMessage(error, 'No pudimos cargar los dias de rutina.'))
      return []
    } finally {
      setIsLoadingDias(false)
    }
  }, [])

  const loadEntrenadores = useCallback(async (idRangoHorario: number) => {
    const requestId = ++entrenadoresRequestId.current
    setIsLoadingEntrenadores(true)
    setEntrenadoresError('')

    try {
      const response = await rutinasService.getEntrenadoresPorHorario(idRangoHorario)

      if (requestId === entrenadoresRequestId.current) {
        setEntrenadores(response)
      }
    } catch (error) {
      if (requestId === entrenadoresRequestId.current) {
        setEntrenadoresError(
          getApiErrorMessage(error, 'No pudimos cargar los entrenadores para este horario.'),
        )
      }
    } finally {
      if (requestId === entrenadoresRequestId.current) {
        setIsLoadingEntrenadores(false)
      }
    }
  }, [])

  const loadRangos = useCallback(async () => {
    setIsLoadingRangos(true)
    setRangosError('')

    try {
      const response = await rutinasService.getRangosHorarios()
      const currentRango = getCurrentRango(response)

      setRangos(response)
      setSelectedRangoId(currentRango?.idRangoHorario ?? null)

      if (currentRango) {
        void loadEntrenadores(currentRango.idRangoHorario)
      }
    } catch (error) {
      setRangosError(
        getApiErrorMessage(error, 'No pudimos cargar los rangos horarios. Intenta nuevamente.'),
      )
    } finally {
      setIsLoadingRangos(false)
    }
  }, [loadEntrenadores])

  const loadSocios = useCallback(
    async (idUsuarioResponsable: number, idRangoHorario: number) => {
      const requestId = ++sociosRequestId.current
      setIsLoadingSocios(true)
      setSociosError('')

      try {
        const response = await rutinasService.getSociosPorEntrenadorYHorario(
          idUsuarioResponsable,
          idRangoHorario,
        )

        if (requestId === sociosRequestId.current) {
          setSocios(response)
        }
      } catch (error) {
        if (requestId === sociosRequestId.current) {
          setSociosError(
            getApiErrorMessage(error, 'No pudimos cargar los socios para esta seleccion.'),
          )
        }
      } finally {
        if (requestId === sociosRequestId.current) {
          setIsLoadingSocios(false)
        }
      }
    },
    [],
  )

  const loadCurrentDayRoutine = useCallback(
    async (idUsuarioSocio: number) => {
      const availableDias = await loadDias()
      void loadGruposMusculares()

      if (availableDias.length === 0) {
        return
      }

      const currentDia = getCurrentDia(availableDias)

      if (!currentDia) {
        return
      }

      setSelectedDiaId(currentDia.idDia)
      nextNoRutinaMessageRef.current = DEFAULT_NO_RUTINA_MESSAGE
      void loadRutina(idUsuarioSocio, currentDia.idDia)
    },
    [loadDias, loadGruposMusculares, loadRutina],
  )

  useEffect(() => {
    let isActive = true

    async function loadInitialRangos() {
      if (isActive) {
        await loadRangos()
      }
    }

    void loadInitialRangos()

    return () => {
      isActive = false
    }
  }, [loadRangos])

  function handleRangoChange(value: string) {
    const idRangoHorario = value ? Number(value) : null

    entrenadoresRequestId.current += 1
    sociosRequestId.current += 1
    setSelectedRangoId(idRangoHorario)
    setSelectedEntrenadorId(null)
    setSelectedSocioId(null)
    setEntrenadores([])
    setSocios([])
    setEntrenadoresError('')
    setSociosError('')
    setIsLoadingEntrenadores(false)
    setIsLoadingSocios(false)
    resetRutinaWorkspace()

    if (idRangoHorario !== null) {
      void loadEntrenadores(idRangoHorario)
    }
  }

  function handleEntrenadorChange(idUsuario: number) {
    if (selectedRangoId === null) {
      return
    }

    sociosRequestId.current += 1
    setSelectedEntrenadorId(idUsuario)
    setSelectedSocioId(null)
    setSocios([])
    setSociosError('')
    setIsLoadingSocios(false)
    resetRutinaWorkspace()
    void loadSocios(idUsuario, selectedRangoId)
  }

  function handleSocioChange(idUsuario: number) {
    setSelectedSocioId(idUsuario)
    resetRutinaWorkspace()
    void loadCurrentDayRoutine(idUsuario)
  }

  function handleDiaSelect(idDia: number) {
    if (selectedSocioId === null) {
      return
    }

    setSelectedDiaId(idDia)
    nextNoRutinaMessageRef.current = DEFAULT_NO_RUTINA_MESSAGE
    void loadRutina(selectedSocioId, idDia)
  }

  function updateDraftRow(
    blockType: RutinaBlockType,
    uid: string,
    changes: Partial<RutinaDraftRow>,
  ) {
    setRutinaDraft((current) => {
      if (!current) {
        return current
      }

      return {
        ...current,
        [blockType]: current[blockType].map((row) =>
          row.uid === uid ? { ...row, ...changes } : row,
        ),
      }
    })
  }

  function handleGrupoChange(blockType: RutinaBlockType, row: RutinaDraftRow, value: string) {
    const idGrupoMuscular = value ? Number(value) : null
    const grupo = gruposMusculares.find((item) => item.idGrupoMuscular === idGrupoMuscular)

    updateDraftRow(blockType, row.uid, {
      idGrupoMuscular,
      grupoMuscularLabel: grupo?.nombreMusculo ?? '',
      idEjercicio: null,
      ejercicioLabel: '',
    })

    if (idGrupoMuscular !== null) {
      void ensureEjerciciosByGrupo(idGrupoMuscular)
    }
  }

  function handleEjercicioChange(blockType: RutinaBlockType, row: RutinaDraftRow, value: string) {
    const idEjercicio = value ? Number(value) : null
    const ejercicios =
      row.idGrupoMuscular === null ? [] : ejerciciosByGrupo[row.idGrupoMuscular] ?? []
    const ejercicio = ejercicios.find((item) => item.idEjercicio === idEjercicio)

    updateDraftRow(blockType, row.uid, {
      idEjercicio,
      ejercicioLabel: ejercicio?.descEjercicio ?? '',
    })
  }

  function addDraftRow(blockType: RutinaBlockType) {
    setRutinaDraft((current) => {
      if (!current) {
        return current
      }

      return {
        ...current,
        [blockType]: normalizeDraftRows([
          ...current[blockType],
          createEmptyDraftRow(current[blockType].length + 1),
        ]),
      }
    })
  }

  function removeDraftRow(blockType: RutinaBlockType, uid: string) {
    setRutinaDraft((current) => {
      if (!current) {
        return current
      }

      return {
        ...current,
        [blockType]: normalizeDraftRows(current[blockType].filter((row) => row.uid !== uid)),
      }
    })
  }

  async function handleSaveRutina() {
    if (!rutinaDraft) {
      return
    }

    const { payload, errors } = buildRutinaPayload(rutinaDraft)

    if (errors.length > 0) {
      setActionModal({
        type: 'info',
        title: 'Revisa la rutina',
        message: errors.join(' '),
      })
      return
    }

    setIsSavingRutina(true)

    try {
      const response = await rutinasService.guardarBloquesRutina(rutinaDraft.idRutina, payload)
      const nextDraft = rutinaToDraft(response)
      setSelectedDiaId(response.idDia)
      setRutinaDraft(nextDraft)
      warmUpEjerciciosFromDraft(nextDraft)
      setActionModal({
        type: 'info',
        title: 'Rutina guardada',
        message: 'Los bloques de la rutina se actualizaron correctamente.',
      })
    } catch (error) {
      setActionModal({
        type: 'info',
        title: 'No pudimos guardar',
        message: getApiErrorMessage(
          error,
          'No pudimos guardar la rutina. Revisa los datos e intenta nuevamente.',
        ),
      })
    } finally {
      setIsSavingRutina(false)
    }
  }

  function requestDeleteRutina() {
    if (!rutinaDraft) {
      return
    }

    setActionModal({
      type: 'confirm-delete',
      title: 'Desactivar rutina',
      message:
        'Esta accion desactiva la rutina activa del socio para este dia. Podras continuar consultando otras rutinas, pero esta dejara de aparecer como activa.',
    })
  }

  async function confirmDeleteRutina() {
    if (!rutinaDraft) {
      setActionModal(null)
      return
    }

    const idRutina = rutinaDraft.idRutina
    setIsDeletingRutina(true)

    try {
      await rutinasService.cambiarEstadoRutina(idRutina, { activo: false })
      setActionModal({
        type: 'info',
        title: 'Rutina desactivada',
        message: 'La rutina fue desactivada correctamente.',
      })

      if (selectedSocioId !== null && selectedDiaId !== null) {
        nextNoRutinaMessageRef.current = 'No hay rutina activa para este dia.'
        void loadRutina(selectedSocioId, selectedDiaId)
      }
    } catch (error) {
      setActionModal({
        type: 'info',
        title: 'No pudimos desactivar',
        message: getApiErrorMessage(
          error,
          'No pudimos desactivar la rutina. Intenta nuevamente.',
        ),
      })
    } finally {
      setIsDeletingRutina(false)
    }
  }

  async function loadHistorialDetalle(idRutina: number, idRutinaHistorial: number) {
    setSelectedHistorialId(idRutinaHistorial)
    setIsLoadingHistorialDetalle(true)
    setHistorialDetalleError('')
    setHistorialDetalle(null)
    void loadEjerciciosCatalogo()

    try {
      const response = await rutinasService.getDetalleHistorial(idRutina, idRutinaHistorial)
      setHistorialDetalle(response)
    } catch (error) {
      setHistorialDetalleError(
        getApiErrorMessage(error, 'No pudimos cargar el detalle del historial.'),
      )
    } finally {
      setIsLoadingHistorialDetalle(false)
    }
  }

  async function loadHistorialForRutina(idRutina: number) {
    setHistorialRutinaId(idRutina)
    setIsLoadingHistorial(true)
    setHistorialError('')
    setHistorialDetalleError('')
    setHistorialNoRutinaMessage('')
    setHistorialResumen([])
    setHistorialDetalle(null)
    setSelectedHistorialId(null)
    void loadEjerciciosCatalogo()

    try {
      const response = await rutinasService.getHistorialRutina(idRutina)
      setHistorialResumen(response)

      if (response.length > 0) {
        void loadHistorialDetalle(idRutina, response[0].idRutinaHistorial)
      }
    } catch (error) {
      setHistorialError(
        getApiErrorMessage(error, 'No pudimos cargar el historial de la rutina.'),
      )
    } finally {
      setIsLoadingHistorial(false)
    }
  }

  async function openHistorialModal() {
    if (!rutinaDraft) {
      return
    }

    setIsHistoryOpen(true)
    setHistorialDiaId(selectedDiaId)
    await loadHistorialForRutina(rutinaDraft.idRutina)
  }

  async function handleHistorialDiaSelect(idDia: number) {
    if (selectedSocioId === null) {
      return
    }

    setHistorialDiaId(idDia)
    setHistorialNoRutinaMessage('')
    setHistorialError('')
    setHistorialDetalleError('')
    setHistorialResumen([])
    setHistorialDetalle(null)
    setSelectedHistorialId(null)
    setHistorialRutinaId(null)
    setIsLoadingHistorial(true)

    try {
      const rutina = await rutinasService.getRutinaPorSocioYDia(selectedSocioId, idDia)
      await loadHistorialForRutina(rutina.idRutina)
    } catch (error) {
      if (axios.isAxiosError(error) && error.response?.status === 404) {
        setHistorialNoRutinaMessage('No hay rutina activa para este dia.')
      } else {
        setHistorialError(
          getApiErrorMessage(error, 'No pudimos cargar la rutina de ese dia.'),
        )
      }
      setIsLoadingHistorial(false)
    }
  }

  async function restoreSelectedHistorial() {
    if (historialRutinaId === null || selectedHistorialId === null) {
      return
    }

    setIsRestoringHistorial(true)

    try {
      const response = await rutinasService.restaurarRutinaDesdeHistorial(
        historialRutinaId,
        selectedHistorialId,
      )
      const nextDraft = rutinaToDraft(response)
      setRutinaDraft(nextDraft)
      warmUpEjerciciosFromDraft(nextDraft)
      setIsHistoryOpen(false)
      setActionModal({
        type: 'info',
        title: 'Rutina restaurada',
        message: 'La rutina actual fue reemplazada por la version historica seleccionada.',
      })
    } catch (error) {
      setHistorialDetalleError(
        getApiErrorMessage(error, 'No pudimos restaurar la version seleccionada.'),
      )
    } finally {
      setIsRestoringHistorial(false)
    }
  }

  return (
    <AppLayout>
      <main className="rutinas-page">
        <header className="rutinas-intro">
          <div>
            <span className="section-kicker">Rutinas / Operacion</span>
            <h1 className="dashboard-title">Gestionar rutinas</h1>
            <p className="dashboard-copy">
              Selecciona un turno, un entrenador y el socio cuya rutina necesitas gestionar.
            </p>
          </div>
          <BackToHomeLink />
        </header>

        <section className="rutinas-filter" aria-labelledby="rutinas-turno-title">
          <div>
            <span className="rutinas-step">Paso 1</span>
            <h2 id="rutinas-turno-title">Turno de hoy</h2>
          </div>
          <label className="rutinas-select-field" htmlFor="rutinas-rango">
            <span>Rango horario</span>
            <select
              id="rutinas-rango"
              value={selectedRangoId ?? ''}
              disabled={isLoadingRangos || Boolean(rangosError)}
              onChange={(event) => handleRangoChange(event.target.value)}
            >
              <option value="">
                {isLoadingRangos ? 'Cargando horarios...' : 'Seleccionar horario'}
              </option>
              {rangos.map((rango) => (
                <option key={rango.idRangoHorario} value={rango.idRangoHorario}>
                  {formatRango(rango)}
                </option>
              ))}
            </select>
          </label>
          {rangosError ? (
            <div className="rutinas-inline-error" role="alert">
              <span>{rangosError}</span>
              <button type="button" onClick={() => void loadRangos()}>
                Reintentar
              </button>
            </div>
          ) : null}
          {!isLoadingRangos && !rangosError && rangos.length === 0 ? (
            <p className="rutinas-inline-empty">No hay rangos horarios disponibles.</p>
          ) : null}
        </section>

        <section className="rutinas-workspace" aria-label="Seleccion de entrenador, socio y rutina">
          <SelectionList
            title="Entrenador"
            step="Paso 2"
            description={
              selectedRango
                ? `Disponibles de ${formatRango(selectedRango)}`
                : 'Primero selecciona un horario'
            }
            groupName="entrenador-rutina"
            items={entrenadores}
            selectedId={selectedEntrenadorId}
            isLoading={isLoadingEntrenadores}
            error={entrenadoresError}
            disabled={selectedRangoId === null}
            emptyMessage="No hay entrenadores asignados a este horario."
            onSelect={handleEntrenadorChange}
            onRetry={() => {
              if (selectedRangoId !== null) {
                void loadEntrenadores(selectedRangoId)
              }
            }}
          />

          <SelectionList
            title="Socio"
            step="Paso 3"
            description={
              selectedEntrenador
                ? `Turnos de hoy con ${getFullName(selectedEntrenador)}`
                : 'Selecciona un entrenador para continuar'
            }
            groupName="socio-rutina"
            items={socios}
            selectedId={selectedSocioId}
            isLoading={isLoadingSocios}
            error={sociosError}
            disabled={selectedEntrenadorId === null}
            emptyMessage="No hay socios con turno para esta seleccion."
            onSelect={handleSocioChange}
            onRetry={() => {
              if (selectedRangoId !== null && selectedEntrenadorId !== null) {
                void loadSocios(selectedEntrenadorId, selectedRangoId)
              }
            }}
          />

          <RutinaWorkspace
            selectedSocio={selectedSocio}
            dias={dias}
            selectedDiaId={selectedDiaId}
            rutinaDraft={rutinaDraft}
            gruposMusculares={gruposMusculares}
            ejerciciosByGrupo={ejerciciosByGrupo}
            loadingEjercicioGroupIds={loadingEjercicioGroupIds}
            isLoadingDias={isLoadingDias}
            isLoadingGrupos={isLoadingGrupos}
            isLoadingRutina={isLoadingRutina}
            isSavingRutina={isSavingRutina}
            isDeletingRutina={isDeletingRutina}
            diasError={diasError}
            gruposError={gruposError}
            rutinaError={rutinaError}
            noRutinaMessage={noRutinaMessage}
            canEditRutina={canEditRutina}
            canDeleteRutina={canDeleteRutina}
            canViewHistorial={canViewHistorial}
            onSelectDia={handleDiaSelect}
            onRetryDias={() => {
              if (selectedSocioId !== null) {
                void loadCurrentDayRoutine(selectedSocioId)
              }
            }}
            onRetryRutina={() => {
              if (selectedSocioId !== null && selectedDiaId !== null) {
                void loadRutina(selectedSocioId, selectedDiaId)
              }
            }}
            onRetryGrupos={() => void loadGruposMusculares()}
            onAddRow={addDraftRow}
            onRemoveRow={removeDraftRow}
            onUpdateRow={updateDraftRow}
            onGrupoChange={handleGrupoChange}
            onEjercicioChange={handleEjercicioChange}
            onLoadEjercicios={ensureEjerciciosByGrupo}
            onSaveRutina={handleSaveRutina}
            onRequestDeleteRutina={requestDeleteRutina}
            onOpenHistorial={() => void openHistorialModal()}
          />
        </section>

        {isHistoryOpen && rutinaDraft ? (
          <RutinaHistorialModal
            canRestoreRutina={canRestoreRutina}
            dias={dias}
            ejerciciosById={ejerciciosById}
            historialDetalle={historialDetalle}
            historialDetalleError={historialDetalleError}
            historialResumen={historialResumen}
            historialError={historialError}
            historialNoRutinaMessage={historialNoRutinaMessage}
            isLoadingHistorial={isLoadingHistorial}
            isLoadingHistorialDetalle={isLoadingHistorialDetalle}
            isRestoringHistorial={isRestoringHistorial}
            selectedHistoryDiaId={historialDiaId}
            selectedHistorialId={selectedHistorialId}
            socioName={selectedSocio ? getFullName(selectedSocio) : ''}
            onClose={() => setIsHistoryOpen(false)}
            onRetryHistorial={() => {
              if (historialDiaId !== null) {
                void handleHistorialDiaSelect(historialDiaId)
              } else {
                void openHistorialModal()
              }
            }}
            onRestore={() => void restoreSelectedHistorial()}
            onSelectDia={(idDia) => void handleHistorialDiaSelect(idDia)}
            onSelectHistorial={(idRutinaHistorial) =>
              historialRutinaId !== null
                ? void loadHistorialDetalle(historialRutinaId, idRutinaHistorial)
                : undefined
            }
          />
        ) : null}

        {actionModal ? (
          <div className="rutinas-modal-backdrop" role="presentation">
            <section
              className="rutinas-modal"
              role="dialog"
              aria-modal="true"
              aria-labelledby="rutinas-modal-title"
            >
              <span className="rutinas-step">
                {actionModal.type === 'confirm-delete' ? 'Confirmacion' : 'Rutinas'}
              </span>
              <h2 id="rutinas-modal-title">{actionModal.title}</h2>
              <p>{actionModal.message}</p>
              {actionModal.type === 'confirm-delete' ? (
                <div className="rutinas-modal__actions">
                  <button
                    className="rutinas-modal__secondary"
                    type="button"
                    disabled={isDeletingRutina}
                    onClick={() => setActionModal(null)}
                  >
                    Cancelar
                  </button>
                  <button
                    className="rutinas-modal__danger"
                    type="button"
                    disabled={isDeletingRutina}
                    onClick={() => void confirmDeleteRutina()}
                  >
                    {isDeletingRutina ? 'Desactivando...' : 'Desactivar'}
                  </button>
                </div>
              ) : (
                <button type="button" onClick={() => setActionModal(null)}>
                  Entendido
                </button>
              )}
            </section>
          </div>
        ) : null}
      </main>
    </AppLayout>
  )
}

interface RutinaHistorialModalProps {
  canRestoreRutina: boolean
  dias: DiaDto[]
  ejerciciosById: Map<number, EjercicioDto>
  historialResumen: RutinaHistorialResumenDto[]
  historialDetalle: RutinaHistorialDetalleDto | null
  historialError: string
  historialDetalleError: string
  historialNoRutinaMessage: string
  isLoadingHistorial: boolean
  isLoadingHistorialDetalle: boolean
  isRestoringHistorial: boolean
  selectedHistoryDiaId: number | null
  selectedHistorialId: number | null
  socioName: string
  onClose: () => void
  onRetryHistorial: () => void
  onRestore: () => void
  onSelectDia: (idDia: number) => void
  onSelectHistorial: (idRutinaHistorial: number) => void
}

function RutinaHistorialModal({
  canRestoreRutina,
  dias,
  ejerciciosById,
  historialResumen,
  historialDetalle,
  historialError,
  historialDetalleError,
  historialNoRutinaMessage,
  isLoadingHistorial,
  isLoadingHistorialDetalle,
  isRestoringHistorial,
  selectedHistoryDiaId,
  selectedHistorialId,
  socioName,
  onClose,
  onRetryHistorial,
  onRestore,
  onSelectDia,
  onSelectHistorial,
}: RutinaHistorialModalProps) {
  return (
    <div className="rutinas-modal-backdrop" role="presentation">
      <section
        className="rutinas-history-modal"
        role="dialog"
        aria-modal="true"
        aria-labelledby="rutinas-history-title"
      >
        <header className="rutinas-history-modal__header">
          <div>
            <span className="rutinas-step">Historial de rutinas</span>
            <h2 id="rutinas-history-title">
              {socioName ? `Historial de rutinas de ${socioName}` : 'Historial de rutinas'}
            </h2>
            <p>Selecciona una version para revisar sus bloques y restaurarla si corresponde.</p>
          </div>
          <button type="button" aria-label="Cerrar historial" onClick={onClose}>
            &times;
          </button>
        </header>

        <div className="rutinas-history-modal__body">
          <aside className="rutinas-history-list">
            <header>
              <h3>Versiones</h3>
              <span>{historialResumen.length}</span>
            </header>

            {isLoadingHistorial ? (
              <p className="rutinas-list__status">Cargando historial...</p>
            ) : null}
            {!isLoadingHistorial && historialError ? (
              <div className="rutinas-list__error" role="alert">
                <p>{historialError}</p>
                <button type="button" onClick={onRetryHistorial}>
                  Reintentar
                </button>
              </div>
            ) : null}
            {!isLoadingHistorial &&
            !historialError &&
            !historialNoRutinaMessage &&
            historialResumen.length === 0 ? (
              <p className="rutinas-list__status">No hay versiones historicas guardadas.</p>
            ) : null}

            {!isLoadingHistorial && !historialError && historialResumen.length > 0 ? (
              <div className="rutinas-history-options" role="radiogroup" aria-label="Versiones">
                {historialResumen.map((version) => {
                  const isSelected = selectedHistorialId === version.idRutinaHistorial

                  return (
                    <label
                      className={`rutinas-history-option${isSelected ? ' is-selected' : ''}`}
                      key={version.idRutinaHistorial}
                    >
                      <input
                        type="radio"
                        name="rutina-historial"
                        checked={isSelected}
                        onChange={() => onSelectHistorial(version.idRutinaHistorial)}
                      />
                      <span>{formatHistoryVersion(version.version)}</span>
                      <small>{formatDateTime(version.fechaSnapshot)}</small>
                    </label>
                  )
                })}
              </div>
            ) : null}
          </aside>

          <section className="rutinas-history-detail">
            <div className="rutinas-day-tabs" aria-label="Dia de la version historica">
              {dias.length > 0 ? (
                dias.map((dia) => (
                  <button
                    className={selectedHistoryDiaId === dia.idDia ? 'is-active' : ''}
                    disabled={isLoadingHistorial || isLoadingHistorialDetalle || isRestoringHistorial}
                    key={dia.idDia}
                    type="button"
                    onClick={() => onSelectDia(dia.idDia)}
                  >
                    {dia.nombreDia}
                  </button>
                ))
              ) : (
                <span className="rutinas-history-detail__empty">Dia actual de la rutina</span>
              )}
            </div>

            {isLoadingHistorialDetalle ? (
              <div className="rutinas-editor__state">Cargando detalle de la version...</div>
            ) : null}
            {!isLoadingHistorial && historialNoRutinaMessage ? (
              <div className="rutinas-editor__state rutinas-editor__state--empty">
                {historialNoRutinaMessage}
              </div>
            ) : null}
            {!isLoadingHistorialDetalle && historialDetalleError ? (
              <div className="rutinas-editor__state rutinas-editor__state--error" role="alert">
                <p>{historialDetalleError}</p>
              </div>
            ) : null}
            {!isLoadingHistorial &&
            !isLoadingHistorialDetalle &&
            !historialNoRutinaMessage &&
            !historialDetalleError &&
            !historialDetalle ? (
              <div className="rutinas-editor__state">Selecciona una version historica.</div>
            ) : null}
            {!isLoadingHistorialDetalle &&
            !historialNoRutinaMessage &&
            !historialDetalleError &&
            historialDetalle ? (
              <>
                <div className="rutinas-history-detail__meta">
                  <span>{formatHistoryVersion(historialDetalle.version)}</span>
                  <span>Fecha actualizacion: {formatSnapshotDate(historialDetalle.fechaSnapshot)}</span>
                  <span>Hora actualizacion: {formatSnapshotTime(historialDetalle.fechaSnapshot)}</span>
                </div>
                <RutinaHistorialBlocks
                  detalle={historialDetalle}
                  ejerciciosById={ejerciciosById}
                />
              </>
            ) : null}
          </section>
        </div>

        <footer className="rutinas-history-modal__footer">
          <button className="rutinas-modal__secondary" type="button" onClick={onClose}>
            Cerrar
          </button>
          {canRestoreRutina && historialDetalle ? (
            <button
              className="rutinas-modal__danger"
              type="button"
              disabled={isRestoringHistorial}
              onClick={onRestore}
            >
              {isRestoringHistorial ? 'Restaurando...' : 'Restaurar'}
            </button>
          ) : null}
        </footer>
      </section>
    </div>
  )
}

interface RutinaHistorialBlocksProps {
  detalle: RutinaHistorialDetalleDto
  ejerciciosById: Map<number, EjercicioDto>
}

function RutinaHistorialBlocks({ detalle, ejerciciosById }: RutinaHistorialBlocksProps) {
  return (
    <div className="rutinas-blocks rutinas-history-blocks">
      <RutinaHistorialBlock
        blockType="calentamientos"
        ejerciciosById={ejerciciosById}
        rows={detalle.calentamientos}
      />
      <RutinaHistorialBlock
        blockType="entrenamientos"
        ejerciciosById={ejerciciosById}
        rows={detalle.entrenamientos}
      />
      <RutinaHistorialBlock
        blockType="estiramientos"
        ejerciciosById={ejerciciosById}
        rows={detalle.estiramientos}
      />
    </div>
  )
}

interface RutinaHistorialBlockProps {
  blockType: RutinaBlockType
  ejerciciosById: Map<number, EjercicioDto>
  rows:
    | RutinaHistorialDetalleDto['calentamientos']
    | RutinaHistorialDetalleDto['entrenamientos']
    | RutinaHistorialDetalleDto['estiramientos']
}

function RutinaHistorialBlock({ blockType, ejerciciosById, rows }: RutinaHistorialBlockProps) {
  const label = BLOCK_LABELS[blockType]
  const sortedRows = [...rows].sort((first, second) => first.orden - second.orden)

  return (
    <section className="rutinas-block">
      <header className="rutinas-block__header">
        <div>
          <span className="rutinas-step">{label}</span>
          <h3>{label}</h3>
        </div>
      </header>

      {sortedRows.length === 0 ? (
        <div className="rutinas-block__empty-state">
          <p className="rutinas-block__empty">No hay {label.toLowerCase()} en esta version.</p>
        </div>
      ) : (
        <div className="rutinas-block__rows">
          {sortedRows.map((row) => (
            <article className="rutinas-row rutinas-history-row" key={`${blockType}-${row.orden}`}>
              <div className="rutinas-row__top">
                <span>Linea {row.orden}</span>
              </div>
              <div className="rutinas-history-row__grid">
                <ReadOnlyValue label="Ejercicio" value={getExerciseLabel(row.idEjercicio, ejerciciosById)} />
                {blockType === 'entrenamientos' ? (
                  <>
                    <ReadOnlyValue label="Series" value={String('series' in row ? row.series : '')} />
                    <ReadOnlyValue
                      label="Repeticiones"
                      value={String('repeticiones' in row ? row.repeticiones : '')}
                    />
                    <ReadOnlyValue
                      label="Peso"
                      value={'pesoAsignado' in row && row.pesoAsignado !== null ? String(row.pesoAsignado) : '-'}
                    />
                    <ReadOnlyValue
                      label="Descanso seg."
                      value={
                        'tiempoDescansoSegundos' in row && row.tiempoDescansoSegundos !== null
                          ? String(row.tiempoDescansoSegundos)
                          : '-'
                      }
                    />
                  </>
                ) : (
                  <ReadOnlyValue label="Duracion seg." value={String('duracion' in row ? row.duracion : '')} />
                )}
                <ReadOnlyValue
                  isWide
                  label="Observaciones"
                  value={row.observaciones?.trim() ? row.observaciones : 'Sin observaciones'}
                />
              </div>
            </article>
          ))}
        </div>
      )}
    </section>
  )
}

interface ReadOnlyValueProps {
  label: string
  value: string
  isWide?: boolean
}

function ReadOnlyValue({ label, value, isWide = false }: ReadOnlyValueProps) {
  return (
    <div className={isWide ? 'rutinas-history-value rutinas-history-value--wide' : 'rutinas-history-value'}>
      <span>{label}</span>
      <strong>{value}</strong>
    </div>
  )
}

interface RutinaWorkspaceProps {
  selectedSocio: SocioTurnoDto | null
  dias: DiaDto[]
  selectedDiaId: number | null
  rutinaDraft: RutinaDraft | null
  gruposMusculares: GrupoMuscularDto[]
  ejerciciosByGrupo: Record<number, EjercicioDto[]>
  loadingEjercicioGroupIds: number[]
  isLoadingDias: boolean
  isLoadingGrupos: boolean
  isLoadingRutina: boolean
  isSavingRutina: boolean
  isDeletingRutina: boolean
  diasError: string
  gruposError: string
  rutinaError: string
  noRutinaMessage: string
  canEditRutina: boolean
  canDeleteRutina: boolean
  canViewHistorial: boolean
  onSelectDia: (idDia: number) => void
  onRetryDias: () => void
  onRetryRutina: () => void
  onRetryGrupos: () => void
  onAddRow: (blockType: RutinaBlockType) => void
  onRemoveRow: (blockType: RutinaBlockType, uid: string) => void
  onUpdateRow: (
    blockType: RutinaBlockType,
    uid: string,
    changes: Partial<RutinaDraftRow>,
  ) => void
  onGrupoChange: (blockType: RutinaBlockType, row: RutinaDraftRow, value: string) => void
  onEjercicioChange: (blockType: RutinaBlockType, row: RutinaDraftRow, value: string) => void
  onLoadEjercicios: (idGrupoMuscular: number) => Promise<EjercicioDto[]>
  onSaveRutina: () => void
  onRequestDeleteRutina: () => void
  onOpenHistorial: () => void
}

function RutinaWorkspace({
  selectedSocio,
  dias,
  selectedDiaId,
  rutinaDraft,
  gruposMusculares,
  ejerciciosByGrupo,
  loadingEjercicioGroupIds,
  isLoadingDias,
  isLoadingGrupos,
  isLoadingRutina,
  isSavingRutina,
  isDeletingRutina,
  diasError,
  gruposError,
  rutinaError,
  noRutinaMessage,
  canEditRutina,
  canDeleteRutina,
  canViewHistorial,
  onSelectDia,
  onRetryDias,
  onRetryRutina,
  onRetryGrupos,
  onAddRow,
  onRemoveRow,
  onUpdateRow,
  onGrupoChange,
  onEjercicioChange,
  onLoadEjercicios,
  onSaveRutina,
  onRequestDeleteRutina,
  onOpenHistorial,
}: RutinaWorkspaceProps) {
  if (!selectedSocio) {
    return (
      <section className="rutinas-coming-soon" aria-live="polite">
        <span className="rutinas-step">Siguiente etapa</span>
        <h2>Espacio de rutina</h2>
        <p>Completa los tres pasos para preparar el area de trabajo.</p>
        <ol className="rutinas-progress" aria-label="Progreso de seleccion">
          <li className="is-complete">Horario</li>
          <li>Entrenador</li>
          <li>Socio</li>
        </ol>
      </section>
    )
  }

  return (
    <section className="rutinas-editor" aria-live="polite">
      <header className="rutinas-editor__header">
        <div>
          <span className="rutinas-step">Rutina del socio</span>
          <h2>{getFullName(selectedSocio)}</h2>
          <p>Seleccioná el día de entrenamiento para cargar la rutina del socio y gestionar su avance.</p>
        </div>
        {rutinaDraft ? (
          <span className="rutinas-editor__date">
            Ultima modificacion: {formatDate(rutinaDraft.fechaModificacion)}
          </span>
        ) : null}
      </header>

      <div className="rutinas-day-tabs" aria-label="Dias de rutina">
        {isLoadingDias ? <p className="rutinas-list__status">Cargando dias...</p> : null}
        {!isLoadingDias && diasError ? (
          <div className="rutinas-inline-error rutinas-inline-error--wide" role="alert">
            <span>{diasError}</span>
            <button type="button" onClick={onRetryDias}>
              Reintentar
            </button>
          </div>
        ) : null}
        {!isLoadingDias && !diasError && dias.length === 0 ? (
          <p className="rutinas-inline-empty">No hay dias disponibles para rutinas.</p>
        ) : null}
        {!isLoadingDias && !diasError
          ? dias.map((dia) => (
              <button
                className={selectedDiaId === dia.idDia ? 'is-active' : ''}
                key={dia.idDia}
                type="button"
                onClick={() => onSelectDia(dia.idDia)}
              >
                {dia.nombreDia}
              </button>
            ))
          : null}
      </div>

      {gruposError ? (
        <div className="rutinas-inline-error rutinas-inline-error--wide" role="alert">
          <span>{gruposError}</span>
          <button type="button" onClick={onRetryGrupos}>
            Reintentar
          </button>
        </div>
      ) : null}

      {isLoadingRutina ? (
        <div className="rutinas-editor__state">Cargando rutina del dia...</div>
      ) : null}

      {!isLoadingRutina && rutinaError ? (
        <div className="rutinas-editor__state rutinas-editor__state--error" role="alert">
          <p>{rutinaError}</p>
          <button type="button" onClick={onRetryRutina}>
            Reintentar
          </button>
        </div>
      ) : null}

      {!isLoadingRutina && noRutinaMessage ? (
        <div className="rutinas-editor__state rutinas-editor__state--empty">
          {noRutinaMessage}
        </div>
      ) : null}

      {!isLoadingRutina && !rutinaError && !noRutinaMessage && rutinaDraft ? (
        <>
          <div className="rutinas-blocks">
            {(['calentamientos', 'entrenamientos', 'estiramientos'] as RutinaBlockType[]).map(
              (blockType) => (
                <RutinaBlockPanel
                  blockType={blockType}
                  key={blockType}
                  rows={rutinaDraft[blockType]}
                  gruposMusculares={gruposMusculares}
                  ejerciciosByGrupo={ejerciciosByGrupo}
                  loadingEjercicioGroupIds={loadingEjercicioGroupIds}
                  isLoadingGrupos={isLoadingGrupos}
                  canEditRutina={canEditRutina}
                  onAddRow={onAddRow}
                  onRemoveRow={onRemoveRow}
                  onUpdateRow={onUpdateRow}
                  onGrupoChange={onGrupoChange}
                  onEjercicioChange={onEjercicioChange}
                  onLoadEjercicios={onLoadEjercicios}
                />
              ),
            )}
          </div>

          <footer className="rutinas-actions">
            <div>
              {canDeleteRutina ? (
                <button
                  className="rutinas-action rutinas-action--danger"
                  type="button"
                  disabled={isSavingRutina || isDeletingRutina}
                  onClick={onRequestDeleteRutina}
                >
                  {isDeletingRutina ? 'Eliminando...' : 'Eliminar'}
                </button>
              ) : null}
            </div>
            <div className="rutinas-actions__main">
              {canViewHistorial ? (
                <button
                  className="rutinas-action"
                  type="button"
                  disabled={isSavingRutina || isDeletingRutina}
                  onClick={onOpenHistorial}
                >
                  Historial
                </button>
              ) : null}
              {canEditRutina ? (
                <button
                  className="rutinas-action rutinas-action--save"
                  type="button"
                  disabled={isSavingRutina || isDeletingRutina}
                  onClick={onSaveRutina}
                >
                  {isSavingRutina ? 'Guardando...' : 'Guardar'}
                </button>
              ) : null}
            </div>
          </footer>
        </>
      ) : null}
    </section>
  )
}

interface RutinaBlockPanelProps {
  blockType: RutinaBlockType
  rows: RutinaDraftRow[]
  gruposMusculares: GrupoMuscularDto[]
  ejerciciosByGrupo: Record<number, EjercicioDto[]>
  loadingEjercicioGroupIds: number[]
  isLoadingGrupos: boolean
  canEditRutina: boolean
  onAddRow: (blockType: RutinaBlockType) => void
  onRemoveRow: (blockType: RutinaBlockType, uid: string) => void
  onUpdateRow: (
    blockType: RutinaBlockType,
    uid: string,
    changes: Partial<RutinaDraftRow>,
  ) => void
  onGrupoChange: (blockType: RutinaBlockType, row: RutinaDraftRow, value: string) => void
  onEjercicioChange: (blockType: RutinaBlockType, row: RutinaDraftRow, value: string) => void
  onLoadEjercicios: (idGrupoMuscular: number) => Promise<EjercicioDto[]>
}

function RutinaBlockPanel({
  blockType,
  rows,
  gruposMusculares,
  ejerciciosByGrupo,
  loadingEjercicioGroupIds,
  isLoadingGrupos,
  canEditRutina,
  onAddRow,
  onRemoveRow,
  onUpdateRow,
  onGrupoChange,
  onEjercicioChange,
  onLoadEjercicios,
}: RutinaBlockPanelProps) {
  const label = BLOCK_LABELS[blockType]

  return (
    <section className="rutinas-block">
      <header className="rutinas-block__header">
        <div>
          <span className="rutinas-step">{label}</span>
          <h3>{label}</h3>
        </div>
      </header>

      {rows.length === 0 ? (
        <div className="rutinas-block__empty-state">
          <p className="rutinas-block__empty">No hay {label.toLowerCase()} cargado.</p>
          {canEditRutina ? (
            <div className="rutinas-block__add-after">
              <button
                className="rutinas-add-row"
                type="button"
                aria-label={`Agregar ${label}`}
                onClick={() => onAddRow(blockType)}
              >
                +
              </button>
            </div>
          ) : null}
        </div>
      ) : (
        <div className="rutinas-block__rows">
          {rows.map((row) => (
            <RutinaEditableRow
              blockType={blockType}
              key={row.uid}
              row={row}
              gruposMusculares={gruposMusculares}
              ejerciciosByGrupo={ejerciciosByGrupo}
              loadingEjercicioGroupIds={loadingEjercicioGroupIds}
              isLoadingGrupos={isLoadingGrupos}
              canEditRutina={canEditRutina}
              onRemoveRow={onRemoveRow}
              onUpdateRow={onUpdateRow}
              onGrupoChange={onGrupoChange}
              onEjercicioChange={onEjercicioChange}
              onLoadEjercicios={onLoadEjercicios}
            />
          ))}
          {canEditRutina ? (
            <div className="rutinas-block__add-after">
              <button
                className="rutinas-add-row"
                type="button"
                aria-label={`Agregar ${label}`}
                onClick={() => onAddRow(blockType)}
              >
                +
              </button>
            </div>
          ) : null}
        </div>
      )}
    </section>
  )
}

interface RutinaEditableRowProps {
  blockType: RutinaBlockType
  row: RutinaDraftRow
  gruposMusculares: GrupoMuscularDto[]
  ejerciciosByGrupo: Record<number, EjercicioDto[]>
  loadingEjercicioGroupIds: number[]
  isLoadingGrupos: boolean
  canEditRutina: boolean
  onRemoveRow: (blockType: RutinaBlockType, uid: string) => void
  onUpdateRow: (
    blockType: RutinaBlockType,
    uid: string,
    changes: Partial<RutinaDraftRow>,
  ) => void
  onGrupoChange: (blockType: RutinaBlockType, row: RutinaDraftRow, value: string) => void
  onEjercicioChange: (blockType: RutinaBlockType, row: RutinaDraftRow, value: string) => void
  onLoadEjercicios: (idGrupoMuscular: number) => Promise<EjercicioDto[]>
}

function RutinaEditableRow({
  blockType,
  row,
  gruposMusculares,
  ejerciciosByGrupo,
  loadingEjercicioGroupIds,
  isLoadingGrupos,
  canEditRutina,
  onRemoveRow,
  onUpdateRow,
  onGrupoChange,
  onEjercicioChange,
  onLoadEjercicios,
}: RutinaEditableRowProps) {
  const ejercicios =
    row.idGrupoMuscular === null ? [] : ejerciciosByGrupo[row.idGrupoMuscular] ?? []
  const isLoadingEjercicios =
    row.idGrupoMuscular !== null && loadingEjercicioGroupIds.includes(row.idGrupoMuscular)
  const hasCurrentGrupo =
    row.idGrupoMuscular === null ||
    gruposMusculares.some((grupo) => grupo.idGrupoMuscular === row.idGrupoMuscular)
  const hasCurrentEjercicio =
    row.idEjercicio === null ||
    ejercicios.some((ejercicio) => ejercicio.idEjercicio === row.idEjercicio)

  function updateField(field: keyof RutinaDraftRow, value: string) {
    onUpdateRow(blockType, row.uid, { [field]: value })
  }

  return (
    <article className="rutinas-row">
      <div className="rutinas-row__top">
        <span>Linea {row.orden}</span>
        {canEditRutina ? (
          <button
            className="rutinas-row__remove"
            type="button"
            aria-label={`Quitar linea ${row.orden}`}
            onClick={() => onRemoveRow(blockType, row.uid)}
          >
            ×
          </button>
        ) : null}
      </div>

      <div className="rutinas-row__grid">
        <label>
          <span>Grupo muscular</span>
          <select
            value={row.idGrupoMuscular ?? ''}
            disabled={!canEditRutina || isLoadingGrupos}
            onChange={(event) => onGrupoChange(blockType, row, event.target.value)}
          >
            <option value="">
              {isLoadingGrupos ? 'Cargando grupos...' : 'Seleccionar grupo'}
            </option>
            {!hasCurrentGrupo && row.idGrupoMuscular !== null ? (
              <option value={row.idGrupoMuscular}>{row.grupoMuscularLabel}</option>
            ) : null}
            {gruposMusculares.map((grupo) => (
              <option key={grupo.idGrupoMuscular} value={grupo.idGrupoMuscular}>
                {grupo.nombreMusculo}
              </option>
            ))}
          </select>
        </label>

        <label>
          <span>Ejercicio</span>
          <select
            value={row.idEjercicio ?? ''}
            disabled={!canEditRutina || row.idGrupoMuscular === null || isLoadingEjercicios}
            onFocus={() => {
              if (row.idGrupoMuscular !== null) {
                void onLoadEjercicios(row.idGrupoMuscular)
              }
            }}
            onChange={(event) => onEjercicioChange(blockType, row, event.target.value)}
          >
            <option value="">
              {isLoadingEjercicios ? 'Cargando ejercicios...' : 'Seleccionar ejercicio'}
            </option>
            {!hasCurrentEjercicio && row.idEjercicio !== null ? (
              <option value={row.idEjercicio}>{row.ejercicioLabel}</option>
            ) : null}
            {ejercicios.map((ejercicio) => (
              <option key={ejercicio.idEjercicio} value={ejercicio.idEjercicio}>
                {ejercicio.descEjercicio}
              </option>
            ))}
          </select>
        </label>

        {blockType === 'entrenamientos' ? (
          <>
            <NumberField
              disabled={!canEditRutina}
              label="Series"
              value={row.series}
              onChange={(value) => updateField('series', value)}
            />
            <NumberField
              disabled={!canEditRutina}
              label="Repeticiones"
              value={row.repeticiones}
              onChange={(value) => updateField('repeticiones', value)}
            />
            <NumberField
              disabled={!canEditRutina}
              label="Peso"
              step="0.01"
              value={row.pesoAsignado}
              onChange={(value) => updateField('pesoAsignado', value)}
            />
            <NumberField
              disabled={!canEditRutina}
              label="Descanso seg."
              value={row.tiempoDescansoSegundos}
              onChange={(value) => updateField('tiempoDescansoSegundos', value)}
            />
          </>
        ) : (
          <NumberField
            disabled={!canEditRutina}
            label="Duracion seg."
            value={row.duracion}
            onChange={(value) => updateField('duracion', value)}
          />
        )}

        <label className="rutinas-row__observaciones">
          <span>Observaciones</span>
          <input
            type="text"
            value={row.observaciones}
            disabled={!canEditRutina}
            onChange={(event) => updateField('observaciones', event.target.value)}
            placeholder="Sin observaciones"
          />
        </label>
      </div>
    </article>
  )
}

interface NumberFieldProps {
  label: string
  value: string
  disabled: boolean
  step?: string
  onChange: (value: string) => void
}

function NumberField({ label, value, disabled, step = '1', onChange }: NumberFieldProps) {
  return (
    <label>
      <span>{label}</span>
      <input
        type="number"
        min="0"
        step={step}
        value={value}
        disabled={disabled}
        onChange={(event) => onChange(event.target.value)}
      />
    </label>
  )
}

interface SelectionItem {
  idUsuario: number
  nombre: string
  apellido: string
}

interface SelectionListProps<T extends SelectionItem> {
  title: string
  step: string
  description: string
  groupName: string
  items: T[]
  selectedId: number | null
  isLoading: boolean
  error: string
  disabled: boolean
  emptyMessage: string
  onSelect: (idUsuario: number) => void
  onRetry: () => void
}

function SelectionList<T extends SelectionItem>({
  title,
  step,
  description,
  groupName,
  items,
  selectedId,
  isLoading,
  error,
  disabled,
  emptyMessage,
  onSelect,
  onRetry,
}: SelectionListProps<T>) {
  return (
    <section className={`rutinas-list${disabled ? ' rutinas-list--disabled' : ''}`}>
      <header className="rutinas-list__header">
        <div>
          <span className="rutinas-step">{step}</span>
          <h2>{title}</h2>
        </div>
        <span className="rutinas-list__count">{items.length}</span>
      </header>
      <p className="rutinas-list__description">{description}</p>

      <div className="rutinas-list__body" aria-busy={isLoading}>
        {isLoading ? (
          <p className="rutinas-list__status">Cargando {title.toLowerCase()}s...</p>
        ) : null}
        {!isLoading && error ? (
          <div className="rutinas-list__error" role="alert">
            <p>{error}</p>
            <button type="button" onClick={onRetry}>
              Reintentar
            </button>
          </div>
        ) : null}
        {!isLoading && !error && !disabled && items.length === 0 ? (
          <p className="rutinas-list__status">{emptyMessage}</p>
        ) : null}
        {!isLoading && !error && disabled ? (
          <p className="rutinas-list__status">Completa el paso anterior.</p>
        ) : null}
        {!isLoading && !error && items.length > 0 ? (
          <div className="rutinas-radio-list" role="radiogroup" aria-label={`Seleccionar ${title}`}>
            {items.map((item) => {
              const isSelected = selectedId === item.idUsuario

              return (
                <label
                  className={`rutinas-radio-row${isSelected ? ' is-selected' : ''}`}
                  key={item.idUsuario}
                >
                  <input
                    type="radio"
                    name={groupName}
                    checked={isSelected}
                    onChange={() => onSelect(item.idUsuario)}
                  />
                  <span className="rutinas-radio-control" aria-hidden="true" />
                  <span className="rutinas-radio-name">{getFullName(item)}</span>
                </label>
              )
            })}
          </div>
        ) : null}
      </div>
    </section>
  )
}
