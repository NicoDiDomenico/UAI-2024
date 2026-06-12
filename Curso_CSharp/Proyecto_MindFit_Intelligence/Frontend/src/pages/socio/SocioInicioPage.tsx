import axios from 'axios'
import { useCallback, useEffect, useMemo, useState } from 'react'
import { NuevoTurnoSocioModal } from '../../components/socio/NuevoTurnoSocioModal'
import { CancelarTurnoModal } from '../../components/turnos/CancelarTurnoModal'
import { TurnosHistorialGrid } from '../../components/turnos/TurnosHistorialGrid'
import { AppLayout } from '../../layouts/AppLayout'
import { turnosService } from '../../services/turnosService'
import type { TurnoHistorialItem } from '../../types/turno'
import { getSocioTurnosErrorMessage } from '../../utils/apiError'
import { formatDateCell } from '../../utils/date'

function getCancelBlockedMessage(estadoTurno: string) {
  if (estadoTurno === 'Cancelado') {
    return 'El turno ya fue cancelado.'
  }

  if (estadoTurno === 'Finalizado') {
    return 'No es posible cancelar un turno finalizado.'
  }

  if (estadoTurno === 'Vencido') {
    return 'No es posible cancelar un turno vencido.'
  }

  if (estadoTurno !== 'EnCurso') {
    return 'No es posible cancelar el turno seleccionado por su estado actual.'
  }

  return null
}

function formatTimeCell(timeValue: string) {
  const normalizedValue = timeValue.trim()

  if (!normalizedValue) {
    return '-'
  }

  const [hours, minutes] = normalizedValue.split(':')

  if (!hours || !minutes) {
    return normalizedValue
  }

  return `${hours.padStart(2, '0')}:${minutes.padStart(2, '0')}`
}

function getTurnoSummary(turno: TurnoHistorialItem) {
  return `${formatDateCell(turno.fechaAlta)} - ${formatTimeCell(turno.horaDesde)} a ${formatTimeCell(
    turno.horaHasta,
  )}`
}

export function SocioInicioPage() {
  const [turnos, setTurnos] = useState<TurnoHistorialItem[]>([])
  const [selectedTurnoId, setSelectedTurnoId] = useState<number | null>(null)
  const [isLoading, setIsLoading] = useState(true)
  const [loadError, setLoadError] = useState('')
  const [cancelTargetTurno, setCancelTargetTurno] = useState<TurnoHistorialItem | null>(null)
  const [isNuevoTurnoOpen, setIsNuevoTurnoOpen] = useState(false)

  const selectedTurno = useMemo(
    () => turnos.find((turno) => turno.idTurno === selectedTurnoId) ?? null,
    [selectedTurnoId, turnos],
  )

  const loadTurnos = useCallback(async (options?: { keepLoading?: boolean }) => {
    if (options?.keepLoading !== false) {
      setIsLoading(true)
    }

    setLoadError('')

    try {
      await turnosService.procesarTurnosVencidos()
      const response = await turnosService.getTurnosSocioLogueado()

      setTurnos(response)
      setSelectedTurnoId((currentSelectedTurnoId) =>
        response.some((turno) => turno.idTurno === currentSelectedTurnoId)
          ? currentSelectedTurnoId
          : null,
      )
    } catch (requestError) {
      setLoadError(getSocioTurnosErrorMessage(requestError))
      throw requestError
    } finally {
      setIsLoading(false)
    }
  }, [])

  useEffect(() => {
    let isActive = true

    async function loadInitialTurnos() {
      try {
        await loadTurnos()
      } catch {
        if (!isActive) {
          return
        }
      }
    }

    if (isActive) {
      void loadInitialTurnos()
    }

    return () => {
      isActive = false
    }
  }, [loadTurnos])

  async function refreshTurnosAfterChange() {
    try {
      await loadTurnos({ keepLoading: false })
    } catch {
      return
    }
  }

  function openNuevoTurnoModal() {
    setIsNuevoTurnoOpen(true)
  }

  function handleCancelTurnoClick() {
    if (!selectedTurno) {
      return
    }

    setCancelTargetTurno(selectedTurno)
  }

  function closeCancelModal() {
    setCancelTargetTurno(null)
  }

  async function handleConfirmCancelTurno() {
    if (!cancelTargetTurno) {
      throw new Error('No hay turno seleccionado.')
    }

    const blockedMessage = getCancelBlockedMessage(cancelTargetTurno.estadoTurno)

    if (blockedMessage) {
      throw new Error(blockedMessage)
    }

    try {
      await turnosService.cancelarTurnoSocio(cancelTargetTurno.idTurno)
      setSelectedTurnoId(null)
      await refreshTurnosAfterChange()
    } catch (requestError) {
      if (axios.isAxiosError(requestError) && requestError.response?.status === 404) {
        setSelectedTurnoId(null)
        await refreshTurnosAfterChange()
      }

      throw requestError
    }
  }

  return (
    <AppLayout>
      <main className="dashboard-page socio-inicio-page">
        <section className="dashboard-card socio-inicio-card">
          <header className="socio-inicio-header">
            <div>
              <span className="section-kicker">Portal Socio</span>
              <h1 className="dashboard-title">Mis turnos</h1>
              <p className="dashboard-copy">
                Consulta tus turnos registrados y deja preparado el siguiente paso operativo.
              </p>
            </div>
          </header>

          {isLoading ? <p className="inicio-status">Cargando historial de turnos...</p> : null}
          {loadError ? <p className="inicio-status inicio-status--error">{loadError}</p> : null}
          {!isLoading && !loadError && turnos.length === 0 ? (
            <p className="inicio-status">No tenes turnos registrados.</p>
          ) : null}

          {turnos.length > 0 ? (
            <TurnosHistorialGrid
              turnos={turnos}
              selectedTurnoId={selectedTurnoId}
              onSelectTurno={setSelectedTurnoId}
            />
          ) : null}

          <div className="gestionar-turnos-modal__actions socio-inicio-actions">
            <button
              className="submit-button consultar-footer__save"
              type="button"
              onClick={openNuevoTurnoModal}
            >
              Nuevo Turno
            </button>
            <button
              className="ghost-button consultar-footer__close gestionar-turnos-modal__cancel"
              type="button"
              disabled={selectedTurnoId === null || isLoading}
              onClick={handleCancelTurnoClick}
            >
              Cancelar Turno
            </button>
          </div>
        </section>
      </main>

      {cancelTargetTurno ? (
        <CancelarTurnoModal
          turnoId={cancelTargetTurno.idTurno}
          turnoSummary={getTurnoSummary(cancelTargetTurno)}
          onClose={closeCancelModal}
          onConfirm={handleConfirmCancelTurno}
        />
      ) : null}

      {isNuevoTurnoOpen ? (
        <NuevoTurnoSocioModal
          onClose={() => setIsNuevoTurnoOpen(false)}
          onRegistered={async () => {
            setSelectedTurnoId(null)
            await refreshTurnosAfterChange()
          }}
        />
      ) : null}
    </AppLayout>
  )
}
