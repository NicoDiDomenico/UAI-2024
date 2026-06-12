import type { ChangeEvent, FormEvent, PropsWithChildren } from 'react'
import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { useAuth } from '../hooks/useAuth'
import { turnosService } from '../services/turnosService'
import { getApiErrorMessage } from '../utils/apiError'

const VALIDAR_INGRESO_PERMISSION = 'VALIDAR_INGRESO'

function sanitizeDniInput(value: string) {
  return value.replace(/\D/g, '')
}

export function AppLayout({ children }: PropsWithChildren) {
  const navigate = useNavigate()
  const { logout, session } = useAuth()
  const [isValidarIngresoOpen, setIsValidarIngresoOpen] = useState(false)
  const [dniSocio, setDniSocio] = useState('')
  const [isSubmittingIngreso, setIsSubmittingIngreso] = useState(false)
  const [validarIngresoError, setValidarIngresoError] = useState('')
  const [validarIngresoSuccess, setValidarIngresoSuccess] = useState('')

  const canValidarIngreso = session?.permisos.includes(VALIDAR_INGRESO_PERMISSION) ?? false

  function handleLogout() {
    logout()
    navigate('/login', { replace: true })
  }

  function openValidarIngresoModal() {
    setIsValidarIngresoOpen(true)
    setValidarIngresoError('')
    setValidarIngresoSuccess('')
  }

  function closeValidarIngresoModal() {
    if (isSubmittingIngreso) {
      return
    }

    setIsValidarIngresoOpen(false)
    setDniSocio('')
    setValidarIngresoError('')
    setValidarIngresoSuccess('')
  }

  function handleDniChange(event: ChangeEvent<HTMLInputElement>) {
    setDniSocio(sanitizeDniInput(event.target.value))

    if (validarIngresoError) {
      setValidarIngresoError('')
    }
  }

  async function handleValidarIngresoSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()

    const normalizedDni = dniSocio.trim()

    if (!normalizedDni) {
      setValidarIngresoError('Ingresa el DNI del socio para continuar.')
      setValidarIngresoSuccess('')
      return
    }

    setIsSubmittingIngreso(true)
    setValidarIngresoError('')
    setValidarIngresoSuccess('')

    try {
      const response = await turnosService.validarIngreso({ dniSocio: normalizedDni })

      setDniSocio('')
      setValidarIngresoSuccess(response.message || 'Se valido el ingreso del socio.')
    } catch (error) {
      setValidarIngresoError(
        getApiErrorMessage(error, 'No pudimos validar el ingreso del socio. Intenta nuevamente.'),
      )
    } finally {
      setIsSubmittingIngreso(false)
    }
  }

  return (
    <div className="workspace-shell">
      <header className="workspace-header">
        <Link className="workspace-brand" to="/dashboard" aria-label="Ir a Inicio">
          <span className="workspace-brand__mark">MF</span>
          <span>
            <strong>MindFit</strong>
            <small>Intelligence</small>
          </span>
        </Link>

        <div className="workspace-actions">
          {canValidarIngreso ? (
            <button className="workspace-validate" type="button" onClick={openValidarIngresoModal}>
              Validar Ingreso
            </button>
          ) : null}
        </div>

        <div className="workspace-session">
          <span className="workspace-session__gym">Gym {session?.idGym ?? '-'}</span>
          <button className="workspace-logout" type="button" onClick={handleLogout}>
            Cerrar sesion
          </button>
        </div>
      </header>

      {children}

      {isValidarIngresoOpen ? (
        <div className="consultar-backdrop" role="presentation">
          <section
            className="consultar-modal validar-ingreso-modal"
            role="dialog"
            aria-modal="true"
            aria-labelledby="validar-ingreso-title"
          >
            <header className="consultar-header validar-ingreso-modal__header">
              <div>
                <h2 id="validar-ingreso-title">Validar ingreso</h2>
                <div className="consultar-meta">
                  <span className="consultar-status consultar-status--info">Turnos</span>
                  <span>Ingreso de socio por DNI</span>
                </div>
              </div>
              <button
                className="consultar-close"
                type="button"
                aria-label="Cerrar"
                disabled={isSubmittingIngreso}
                onClick={closeValidarIngresoModal}
              >
                x
              </button>
            </header>

            <form className="validar-ingreso-form" onSubmit={handleValidarIngresoSubmit} noValidate>
              <div className="consultar-body validar-ingreso-modal__body">
                <div className="validar-ingreso-modal__intro">
                  <span className="section-kicker">Control de acceso</span>
                  <p className="validar-ingreso-modal__copy">
                    Ingresa el DNI del socio para validar su asistencia desde cualquier pantalla
                    privada.
                  </p>
                </div>

                <label className="field-group" htmlFor="validar-ingreso-dni">
                  <span className="field-label">DNI del socio</span>
                  <input
                    id="validar-ingreso-dni"
                    className="field-input validar-ingreso-modal__input"
                    type="text"
                    inputMode="numeric"
                    autoComplete="off"
                    value={dniSocio}
                    disabled={isSubmittingIngreso}
                    onChange={handleDniChange}
                  />
                </label>

                {validarIngresoError ? (
                  <p className="form-alert form-alert--error">{validarIngresoError}</p>
                ) : null}
                {validarIngresoSuccess ? (
                  <p className="form-alert form-alert--success">{validarIngresoSuccess}</p>
                ) : null}
              </div>

              <footer className="consultar-footer validar-ingreso-modal__footer">
                <button
                  className="ghost-button consultar-footer__close"
                  type="button"
                  disabled={isSubmittingIngreso}
                  onClick={closeValidarIngresoModal}
                >
                  Cancelar
                </button>
                <button
                  className="submit-button consultar-footer__save validar-ingreso-modal__confirm"
                  type="submit"
                  disabled={isSubmittingIngreso || !dniSocio.trim()}
                >
                  {isSubmittingIngreso ? 'Confirmando...' : 'Confirmar'}
                </button>
              </footer>
            </form>
          </section>
        </div>
      ) : null}
    </div>
  )
}
