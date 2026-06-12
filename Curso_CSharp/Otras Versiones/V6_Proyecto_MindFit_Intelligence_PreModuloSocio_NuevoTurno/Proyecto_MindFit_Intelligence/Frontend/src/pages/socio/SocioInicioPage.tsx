import axios from 'axios'
import { useCallback, useEffect, useMemo, useState } from 'react'
import { TurnosHistorialGrid } from '../../components/turnos/TurnosHistorialGrid'
import { AppLayout } from '../../layouts/AppLayout'
import { turnosService } from '../../services/turnosService'
import type { TurnoHistorialItem } from '../../types/turno'
import {
  getCancelarTurnoErrorMessage,
  getSocioTurnosErrorMessage,
} from '../../utils/apiError'

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

interface CancelarTurnoConfirmModalProps {
  isSubmitting: boolean
  error: string
  turno: TurnoHistorialItem
  onClose: () => void
  onConfirm: () => Promise<void>
}

function CancelarTurnoConfirmModal({
  isSubmitting,
  error,
  turno,
  onClose,
  onConfirm,
}: CancelarTurnoConfirmModalProps) {
  return (
    <div className="consultar-backdrop consultar-backdrop--nested" role="presentation">
      <section
        className="consultar-modal eliminar-modal turnos-cancel-modal"
        role="dialog"
        aria-modal="true"
        aria-labelledby="cancelar-turno-socio-title"
      >
        <header className="consultar-header eliminar-modal__header">
          <div>
            <h2 id="cancelar-turno-socio-title">Confirmar cancelacion</h2>
            <div className="consultar-meta">
              <span className="consultar-status consultar-status--danger">Turno seleccionado</span>
              <span>ID: {turno.idTurno}</span>
            </div>
          </div>
          <button
            className="consultar-close"
            type="button"
            aria-label="Cerrar"
            onClick={onClose}
            disabled={isSubmitting}
          >
            x
          </button>
        </header>

        <div className="consultar-body eliminar-modal__body">
          <div className="eliminar-modal__content">
            <p className="eliminar-modal__label">Confirma la accion</p>
            <h3>{turno.nombreDia}</h3>
            <p className="eliminar-modal__copy">¿Confirma que desea cancelar este turno?</p>
            {error ? <p className="form-alert form-alert--error">{error}</p> : null}
          </div>
        </div>

        <footer className="consultar-footer eliminar-modal__footer">
          <button
            className="ghost-button consultar-footer__close"
            type="button"
            onClick={onClose}
            disabled={isSubmitting}
          >
            Volver
          </button>
          <button
            className="submit-button consultar-button--danger eliminar-modal__confirm"
            type="button"
            disabled={isSubmitting}
            onClick={() => void onConfirm()}
          >
            {isSubmitting ? 'Cancelando turno...' : 'Confirmar cancelacion'}
          </button>
        </footer>
      </section>
    </div>
  )
}

export function SocioInicioPage() {
  const [turnos, setTurnos] = useState<TurnoHistorialItem[]>([])
  const [selectedTurnoId, setSelectedTurnoId] = useState<number | null>(null)
  const [isLoading, setIsLoading] = useState(true)
  const [loadError, setLoadError] = useState('')
  const [actionError, setActionError] = useState('')
  const [notice, setNotice] = useState('')
  const [cancelTargetTurno, setCancelTargetTurno] = useState<TurnoHistorialItem | null>(null)
  const [isCancelSubmitting, setIsCancelSubmitting] = useState(false)
  const [cancelError, setCancelError] = useState('')

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

  useEffect(() => {
    if (notice !== 'El turno fue cancelado correctamente.') {
      return
    }

    const timeoutId = window.setTimeout(() => {
      setNotice('')
    }, 4000)

    return () => {
      window.clearTimeout(timeoutId)
    }
  }, [notice])

  async function refreshTurnosAfterChange() {
    try {
      await loadTurnos({ keepLoading: false })
    } catch {
      return
    }
  }

  function showPendingAction() {
    setActionError('')
    setNotice('Próximamente...')
  }

  function handleCancelTurnoClick() {
    if (!selectedTurno) {
      return
    }

    const blockedMessage = getCancelBlockedMessage(selectedTurno.estadoTurno)

    if (blockedMessage) {
      setActionError(blockedMessage)
      setNotice('')
      return
    }

    setActionError('')
    setNotice('')
    setCancelError('')
    setCancelTargetTurno(selectedTurno)
  }

  function closeCancelModal() {
    if (isCancelSubmitting) {
      return
    }

    setCancelError('')
    setCancelTargetTurno(null)
  }

  async function handleConfirmCancelTurno() {
    if (!cancelTargetTurno) {
      return
    }

    setIsCancelSubmitting(true)
    setCancelError('')

    try {
      await turnosService.cancelarTurnoSocio(cancelTargetTurno.idTurno)
      setNotice('El turno fue cancelado correctamente.')
      setActionError('')
      setCancelTargetTurno(null)
      setSelectedTurnoId(null)
      await refreshTurnosAfterChange()
    } catch (requestError) {
      const nextError = getCancelarTurnoErrorMessage(requestError)

      if (axios.isAxiosError(requestError) && requestError.response?.status === 404) {
        setActionError(nextError)
        setNotice('')
        setCancelTargetTurno(null)
        setSelectedTurnoId(null)
        await refreshTurnosAfterChange()
        return
      }

      setCancelError(nextError)
      setActionError('')
      setNotice('')
    } finally {
      setIsCancelSubmitting(false)
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

          {notice ? <p className="form-alert form-alert--success">{notice}</p> : null}
          {actionError ? <p className="form-alert form-alert--error">{actionError}</p> : null}
          {isLoading ? <p className="inicio-status">Cargando historial de turnos...</p> : null}
          {loadError ? <p className="inicio-status inicio-status--error">{loadError}</p> : null}
          {!isLoading && !loadError && turnos.length === 0 ? (
            <p className="inicio-status">No tenés turnos registrados.</p>
          ) : null}

          {turnos.length > 0 ? (
            <TurnosHistorialGrid
              turnos={turnos}
              selectedTurnoId={selectedTurnoId}
              onSelectTurno={(idTurno) => {
                setSelectedTurnoId(idTurno)
                setActionError('')
              }}
            />
          ) : null}

          <div className="gestionar-turnos-modal__actions socio-inicio-actions">
            <button
              className="submit-button consultar-footer__save"
              type="button"
              onClick={showPendingAction}
            >
              Nuevo Turno
            </button>
            <button
              className="ghost-button consultar-footer__close gestionar-turnos-modal__cancel"
              type="button"
              disabled={selectedTurnoId === null || isLoading || isCancelSubmitting}
              onClick={handleCancelTurnoClick}
            >
              Cancelar Turno
            </button>
          </div>
        </section>
      </main>

      {cancelTargetTurno ? (
        <CancelarTurnoConfirmModal
          turno={cancelTargetTurno}
          isSubmitting={isCancelSubmitting}
          error={cancelError}
          onClose={closeCancelModal}
          onConfirm={handleConfirmCancelTurno}
        />
      ) : null}
    </AppLayout>
  )
}
