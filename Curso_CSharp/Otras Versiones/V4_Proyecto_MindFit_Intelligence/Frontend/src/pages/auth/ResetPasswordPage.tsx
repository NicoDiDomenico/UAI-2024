import { useEffect, useState } from 'react'
import { Link, useNavigate, useSearchParams } from 'react-router-dom'
import { AuthShowcase } from '../../components/auth/AuthShowcase'
import { authService } from '../../services/authService'
import { getResetPasswordErrorMessage } from '../../utils/apiError'

interface ResetPasswordFormState {
  newPassword: string
  confirmNewPassword: string
}

interface ResetPasswordFormErrors {
  newPassword?: string
  confirmNewPassword?: string
}

const initialFormState: ResetPasswordFormState = {
  newPassword: '',
  confirmNewPassword: '',
}

export function ResetPasswordPage() {
  const navigate = useNavigate()
  const [searchParams] = useSearchParams()
  const token = searchParams.get('token')?.trim() ?? ''
  const rawGymId = searchParams.get('gymId')?.trim() ?? ''
  const parsedGymId = Number(rawGymId)
  const hasGymId = rawGymId.length > 0
  const gymId = hasGymId && Number.isInteger(parsedGymId) && parsedGymId > 0 ? parsedGymId : null
  const [form, setForm] = useState(initialFormState)
  const [errors, setErrors] = useState<ResetPasswordFormErrors>({})
  const [requestError, setRequestError] = useState('')
  const [requestSuccess, setRequestSuccess] = useState('')
  const [isSubmitting, setIsSubmitting] = useState(false)

  const hasToken = token.length > 0
  const isRecoveryLinkValid = hasToken && gymId !== null

  useEffect(() => {
    if (!requestSuccess) {
      return
    }

    const timeoutId = window.setTimeout(() => {
      navigate('/login')
    }, 1500)

    return () => window.clearTimeout(timeoutId)
  }, [navigate, requestSuccess])

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

    if (!form.newPassword.trim()) {
      nextErrors.newPassword = 'Ingresa una nueva contrasena.'
    }

    if (!form.confirmNewPassword.trim()) {
      nextErrors.confirmNewPassword = 'Repite la nueva contrasena.'
    } else if (form.newPassword !== form.confirmNewPassword) {
      nextErrors.confirmNewPassword = 'Las contrasenas no coinciden.'
    }

    setErrors(nextErrors)

    return Object.keys(nextErrors).length === 0
  }

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault()

    if (!hasToken) {
      setRequestError('El enlace de recuperacion no incluye el token necesario para continuar.')
      return
    }

    if (gymId === null) {
      setRequestError('El enlace de recuperacion no incluye un gymId valido.')
      return
    }

    if (!validateForm()) {
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
        gymId,
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
          <p className="auth-subtitle">Usa el enlace recibido por email y crea tu nueva contrasena.</p>

          {!hasToken ? (
            <div className="form-alert form-alert--error">
              El enlace de recuperacion no incluye el token necesario para continuar.
            </div>
          ) : null}

          {hasToken && gymId === null ? (
            <div className="form-alert form-alert--error">
              El enlace de recuperacion no incluye un gymId valido.
            </div>
          ) : null}

          <form className="auth-form" onSubmit={handleSubmit} noValidate>
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
                disabled={isSubmitting || !isRecoveryLinkValid}
              />
              {errors.newPassword ? <p className="field-error">{errors.newPassword}</p> : null}
            </div>

            <div className="field-group">
              <label className="field-label" htmlFor="confirm-new-password">
                Repetir nueva contrasena
              </label>
              <input
                id="confirm-new-password"
                className="field-input"
                type="password"
                autoComplete="new-password"
                value={form.confirmNewPassword}
                onChange={(event) => updateField('confirmNewPassword', event.target.value)}
                disabled={isSubmitting || !isRecoveryLinkValid}
              />
              {errors.confirmNewPassword ? (
                <p className="field-error">{errors.confirmNewPassword}</p>
              ) : null}
            </div>

            {requestError ? <div className="form-alert form-alert--error">{requestError}</div> : null}
            {requestSuccess ? (
              <div className="form-alert form-alert--success">
                {requestSuccess} Seras redirigido al login en unos segundos.
              </div>
            ) : null}

            <div className="auth-actions auth-actions--stacked-mobile">
              <button
                className="submit-button"
                type="submit"
                disabled={isSubmitting || !isRecoveryLinkValid}
              >
                {isSubmitting ? 'Actualizando...' : 'Restablecer contrasena'}
              </button>
              <Link className="inline-link" to="/login">
                Volver al login
              </Link>
            </div>
          </form>

          <p className="auth-note">
            Si el token expiro, ya fue usado o el enlace es incorrecto, podras solicitar una nueva
            recuperacion desde login.
          </p>
        </div>
      </section>

      <AuthShowcase
        eyebrow="Restablecimiento seguro"
        title="El enlace del email desemboca en una accion breve y verificable."
        copy="El token y el gymId viajan en la URL; el front usa ese contexto para enviar solo `tokenPlano` en el body y `X-Gym-Id` por header."
        points={[
          'No se envia `idGym` en el body.',
          'La sede se resuelve automaticamente desde el link.',
          'Se valida que ambas contrasenas coincidan antes del submit.',
          'La UI diferencia exito real de token invalido o expirado.',
          'Tras el exito, la redireccion al login ocurre automaticamente.',
        ]}
      />
    </main>
  )
}
