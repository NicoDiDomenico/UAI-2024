import { useEffect, useMemo, useState } from 'react'
import { turnosService } from '../../services/turnosService'
import type { SocioGridItem } from '../../types/socio'
import type { TurnoHistorialItem } from '../../types/turno'
import { getCancelarTurnoErrorMessage, getSocioTurnosErrorMessage } from '../../utils/apiError'
import { formatDateCell } from '../../utils/date'
import { NuevoTurnoModal } from './NuevoTurnoModal'

interface GestionTurnosModalProps {
  socio: SocioGridItem
  userPermissions: readonly string[]
  onClose: () => void
}

function getSocioDisplayName(socio: SocioGridItem) {
  return socio.nombreCompleto?.trim() || socio.username
}

function formatTurnoState(estadoTurno: string) {
  return estadoTurno === 'EnCurso' ? 'En Curso' : estadoTurno
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

function getTrainerName(turno: TurnoHistorialItem) {
  return `${turno.nombreResponsable} ${turno.apellidoResponsable}`.trim()
}

function getTurnoSummary(turno: TurnoHistorialItem) {
  return `${formatDateCell(turno.fechaAlta)} · ${formatTimeCell(turno.horaDesde)} a ${formatTimeCell(
    turno.horaHasta,
  )}`
}

interface CancelarTurnoConfirmModalProps {
  turno: TurnoHistorialItem
  onClose: () => void
  onConfirmed: () => Promise<void>
}

function CancelarTurnoConfirmModal({
  turno,
  onClose,
  onConfirmed,
}: CancelarTurnoConfirmModalProps) {
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [error, setError] = useState('')
  const [success, setSuccess] = useState('')

  async function handleConfirm() {
    if (isSubmitting || success) {
      return
    }

    setIsSubmitting(true)
    setError('')

    try {
      await onConfirmed()
      setSuccess('El turno fue cancelado correctamente.')
    } catch (requestError) {
      setError(getCancelarTurnoErrorMessage(requestError))
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <div className="consultar-backdrop consultar-backdrop--nested" role="presentation">
      <section
        className="consultar-modal eliminar-modal turnos-cancel-modal"
        role="dialog"
        aria-modal="true"
        aria-labelledby="cancelar-turno-title"
      >
        <header className="consultar-header eliminar-modal__header">
          <div>
            <h2 id="cancelar-turno-title">Confirmar cancelacion</h2>
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
            <h3>{getTurnoSummary(turno)}</h3>
            <p className="eliminar-modal__copy">¿Confirma que desea cancelar este turno?</p>

            {error ? <p className="form-alert form-alert--error">{error}</p> : null}
            {success ? <p className="form-alert form-alert--success">{success}</p> : null}
          </div>
        </div>

        <footer className="consultar-footer eliminar-modal__footer">
          {success ? (
            <button className="ghost-button consultar-footer__close" type="button" onClick={onClose}>
              Cerrar
            </button>
          ) : (
            <>
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
                onClick={() => void handleConfirm()}
              >
                {isSubmitting ? 'Cancelando turno...' : 'Confirmar cancelacion'}
              </button>
            </>
          )}
        </footer>
      </section>
    </div>
  )
}

export function GestionTurnosModal({
  socio,
  userPermissions,
  onClose,
}: GestionTurnosModalProps) {
  const [turnos, setTurnos] = useState<TurnoHistorialItem[]>([])
  const [selectedTurnoId, setSelectedTurnoId] = useState<number | null>(null)
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState('')
  const [isNuevoTurnoOpen, setIsNuevoTurnoOpen] = useState(false)
  const [cancelTargetTurno, setCancelTargetTurno] = useState<TurnoHistorialItem | null>(null)

  const canAgregarTurno = userPermissions.includes('AGREGAR_TURNO')
  const canCancelarTurno = userPermissions.includes('CANCELAR_TURNO')

  async function loadTurnos(options?: { keepLoading?: boolean }) {
    if (options?.keepLoading !== false) {
      setIsLoading(true)
    }
    setError('')

    try {
      await turnosService.procesarTurnosVencidos()
      const response = await turnosService.getSocioTurnos(socio.idUsuario)
      setTurnos(response)
      setSelectedTurnoId((currentSelectedTurnoId) =>
        response.some((turno) => turno.idTurno === currentSelectedTurnoId)
          ? currentSelectedTurnoId
          : null,
      )
    } catch (requestError) {
      setError(getSocioTurnosErrorMessage(requestError))
      throw requestError
    } finally {
      setIsLoading(false)
    }
  }

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
  }, [socio.idUsuario])

  const selectedTurno = useMemo(
    () => turnos.find((turno) => turno.idTurno === selectedTurnoId) ?? null,
    [selectedTurnoId, turnos],
  )

  async function handleConfirmCancelTurno() {
    if (!cancelTargetTurno) {
      throw new Error('No hay turno seleccionado.')
    }

    await turnosService.cancelarTurnoAsistente(cancelTargetTurno.idTurno)
    await loadTurnos({ keepLoading: false })
    setSelectedTurnoId(null)
  }

  return (
    <div className="consultar-backdrop" role="presentation">
      <section
        className="consultar-modal gestionar-turnos-modal"
        role="dialog"
        aria-modal="true"
        aria-labelledby="gestionar-turnos-title"
      >
        <header className="consultar-header gestionar-turnos-modal__header">
          <div>
            <span className="section-kicker">Turnos / Socio</span>
            <h2 id="gestionar-turnos-title">Gestionar turnos</h2>
            <div className="consultar-meta">
              <span className="consultar-status consultar-status--info">Historial operativo</span>
              <span>ID socio: {socio.idUsuario}</span>
            </div>
          </div>
          <button className="consultar-close" type="button" aria-label="Cerrar" onClick={onClose}>
            x
          </button>
        </header>

        <div className="consultar-body gestionar-turnos-modal__body">
          <div className="gestionar-turnos-modal__intro">
            <div>
              <span className="section-kicker">Seguimiento</span>
              <h3>Historial de turnos</h3>
            </div>

            <div className="gestionar-turnos-modal__socio">
              <div>
                <span>Socio</span>
                <strong>{getSocioDisplayName(socio)}</strong>
              </div>
              <div>
                <span>Nro Documento</span>
                <strong>{socio.nroDocumento?.trim() || '-'}</strong>
              </div>
            </div>
          </div>

          {isLoading ? <p className="inicio-status">Cargando historial de turnos...</p> : null}
          {error ? <p className="inicio-status inicio-status--error">{error}</p> : null}
          {!isLoading && !error && turnos.length === 0 ? (
            <p className="inicio-status">El socio no tiene turnos registrados.</p>
          ) : null}

          {turnos.length > 0 ? (
            <div className="gestionar-turnos-modal__table-wrap">
              <table className="turnos-table gestionar-turnos-table">
                <thead>
                  <tr>
                    <th aria-label="Seleccion" />
                    <th>Fecha turno</th>
                    <th>Hora desde</th>
                    <th>Hora hasta</th>
                    <th>Estado turno</th>
                    <th>Entrenador</th>
                  </tr>
                </thead>
                <tbody>
                  {turnos.map((turno) => {
                    const isSelected = turno.idTurno === selectedTurnoId

                    return (
                      <tr
                        key={turno.idTurno}
                        className={
                          isSelected
                            ? 'socios-row gestionar-turnos-row gestionar-turnos-row--selected'
                            : 'socios-row gestionar-turnos-row'
                        }
                        onClick={() => setSelectedTurnoId(turno.idTurno)}
                      >
                        <td data-label="Seleccion">
                          <span
                            className={
                              isSelected ? 'socios-radio socios-radio--selected' : 'socios-radio'
                            }
                            aria-hidden="true"
                          />
                        </td>
                        <td data-label="Fecha turno">
                          <strong>{formatDateCell(turno.fechaAlta)}</strong>
                        </td>
                        <td data-label="Hora desde">{formatTimeCell(turno.horaDesde)}</td>
                        <td data-label="Hora hasta">{formatTimeCell(turno.horaHasta)}</td>
                        <td data-label="Estado turno">
                          <span className="turno-state">
                            {formatTurnoState(turno.estadoTurno)}
                          </span>
                        </td>
                        <td data-label="Entrenador">{getTrainerName(turno)}</td>
                      </tr>
                    )
                  })}
                </tbody>
              </table>
            </div>
          ) : null}

        </div>

        <footer className="consultar-footer gestionar-turnos-modal__footer">
          <button className="ghost-button consultar-footer__close" type="button" onClick={onClose}>
            Volver
          </button>

          <div className="gestionar-turnos-modal__actions">
            {canAgregarTurno ? (
              <button
                className="submit-button consultar-footer__save"
                type="button"
                onClick={() => setIsNuevoTurnoOpen(true)}
              >
                Nuevo Turno
              </button>
            ) : null}

            {canCancelarTurno ? (
              <button
                className="ghost-button consultar-footer__close gestionar-turnos-modal__cancel"
                type="button"
                disabled={!selectedTurno || isLoading}
                onClick={() => setCancelTargetTurno(selectedTurno)}
              >
                Cancelar Turno
              </button>
            ) : null}
          </div>
        </footer>
      </section>

      {isNuevoTurnoOpen ? (
        <NuevoTurnoModal
          socio={socio}
          onClose={() => setIsNuevoTurnoOpen(false)}
          onRegistered={() => loadTurnos({ keepLoading: false })}
        />
      ) : null}

      {cancelTargetTurno ? (
        <CancelarTurnoConfirmModal
          turno={cancelTargetTurno}
          onClose={() => setCancelTargetTurno(null)}
          onConfirmed={handleConfirmCancelTurno}
        />
      ) : null}
    </div>
  )
}
