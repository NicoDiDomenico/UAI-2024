import { useState } from 'react'
import { Link, useSearchParams } from 'react-router-dom'
import { AuthShowcase } from '../../components/auth/AuthShowcase'
import { GymSelect } from '../../components/auth/GymSelect'
import { useActiveGyms } from '../../hooks/useActiveGyms'
import { authService } from '../../services/authService'
import { getResetPasswordErrorMessage } from '../../utils/apiError'

interface ResetPasswordFormState {
  idGym: number | null
  newPassword: string
}

interface ResetPasswordFormErrors {
  idGym?: string
  newPassword?: string
}

const initialFormState: ResetPasswordFormState = {
  idGym: null,
  newPassword: '',
}

export function ResetPasswordPage() {
  const [searchParams] = useSearchParams()
  const token = searchParams.get('token')?.trim() ?? ''
  const { gyms, gymsError, isLoadingGyms } = useActiveGyms()
  const [form, setForm] = useState(initialFormState)
  const [errors, setErrors] = useState<ResetPasswordFormErrors>({})
  const [requestError, setRequestError] = useState('')
  const [requestSuccess, setRequestSuccess] = useState('')
  const [isSubmitting, setIsSubmitting] = useState(false)

  const hasToken = token.length > 0

  function updateField<K extends keyof ResetPasswordFormState>(
    field: K,
    value: ResetPasswordFormState[K],
  ) {
    setForm((current) => ({ ...current, [field]: value }))
    setErrors((current) => ({ ...current, [field]: undefined }))
    setRequestError('')
  }

  function validateForm() {
    const nextErrors: ResetPasswordFormErrors = {}

    if (!form.idGym) {
      nextErrors.idGym = 'Selecciona un gimnasio.'
    }

    if (!form.newPassword.trim()) {
      nextErrors.newPassword = 'Ingresa una nueva contrasena.'
    }

    setErrors(nextErrors)

    return Object.keys(nextErrors).length === 0
  }

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault()

    if (!hasToken) {
      setRequestError('El enlace de recuperacion no es valido o esta incompleto.')
      return
    }

    if (!validateForm() || form.idGym === null) {
      return
    }

    setIsSubmitting(true)
    setRequestError('')
    setRequestSuccess('')

    try {
      const response = await authService.resetPassword(
        {
          tokenPlano: token,
          newPassword: form.newPassword,
        },
        form.idGym,
      )

      setRequestSuccess(response)
    } catch (error) {
      setRequestError(getResetPasswordErrorMessage(error))
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <main className="auth-page">
      <section className="auth-panel">
        <div className="auth-panel__inner">
          <span className="auth-kicker">Nueva contrasena</span>
          <h1 className="auth-title">Define una nueva clave para volver a entrar con seguridad.</h1>
          <p className="auth-subtitle">
            Usa el enlace recibido por email, selecciona la sede y crea tu nueva contrasena.
          </p>

          {!hasToken ? (
            <div className="form-alert form-alert--error">
              El enlace de recuperacion no es valido o esta incompleto.
            </div>
          ) : null}

          <form className="auth-form" onSubmit={handleSubmit} noValidate>
            <div className="field-group">
              <label className="field-label" htmlFor="gym-select">
                Gimnasio
              </label>
              <GymSelect
                gyms={gyms}
                selectedGymId={form.idGym}
                onChange={(gymId) => updateField('idGym', gymId)}
                isDisabled={isLoadingGyms || gyms.length === 0 || isSubmitting || !hasToken}
              />
              {errors.idGym ? <p className="field-error">{errors.idGym}</p> : null}
              {gymsError ? <p className="field-error">{gymsError}</p> : null}
            </div>

            <div className="field-group">
              <label className="field-label" htmlFor="new-password">
                Nueva contrasena
              </label>
              <input
                id="new-password"
                className="field-input"
                type="password"
                autoComplete="new-password"
                value={form.newPassword}
                onChange={(event) => updateField('newPassword', event.target.value)}
                disabled={isSubmitting || !hasToken}
              />
              {errors.newPassword ? <p className="field-error">{errors.newPassword}</p> : null}
            </div>

            {requestError ? <div className="form-alert form-alert--error">{requestError}</div> : null}
            {requestSuccess ? (
              <div className="form-alert form-alert--success">{requestSuccess}</div>
            ) : null}

            <div className="auth-actions auth-actions--stacked-mobile">
              <button
                className="submit-button"
                type="submit"
                disabled={isSubmitting || !hasToken}
              >
                {isSubmitting ? 'Actualizando...' : 'Restablecer contrasena'}
              </button>
              <Link className="inline-link" to="/login">
                Volver al login
              </Link>
            </div>
          </form>

          <p className="auth-note">
            Si el token expiro o ya fue usado, el backend responde con un mensaje claro para que
            puedas solicitar un nuevo enlace.
          </p>
        </div>
      </section>

      <AuthShowcase
        eyebrow="Restablecimiento seguro"
        title="El enlace del email desemboca en una accion breve y verificable."
        copy="El token viaja por query string y se envia como `tokenPlano`, mientras la sede seleccionada viaja solo en `X-Gym-Id`."
        points={[
          'No se envia `idGym` en el body.',
          'La UI diferencia exito real de token invalido o expirado.',
          'Despues del cambio puedes volver directo al login.',
        ]}
      />
    </main>
  )
}
