import { useState } from 'react'
import { Link } from 'react-router-dom'
import { AuthShowcase } from '../../components/auth/AuthShowcase'
import { GymSelect } from '../../components/auth/GymSelect'
import { useActiveGyms } from '../../hooks/useActiveGyms'
import { authService } from '../../services/authService'
import { getForgotPasswordErrorMessage } from '../../utils/apiError'

interface ForgotPasswordFormState {
  idGym: number | null
  email: string
}

interface ForgotPasswordFormErrors {
  idGym?: string
  email?: string
}

const initialFormState: ForgotPasswordFormState = {
  idGym: null,
  email: '',
}

const successMessage =
  'Si el email ingresado esta registrado, recibiras instrucciones para recuperar tu contrasena.'

export function ForgotPasswordPage() {
  const { gyms, gymsError, isLoadingGyms } = useActiveGyms()
  const [form, setForm] = useState(initialFormState)
  const [errors, setErrors] = useState<ForgotPasswordFormErrors>({})
  const [requestError, setRequestError] = useState('')
  const [requestSuccess, setRequestSuccess] = useState('')
  const [isSubmitting, setIsSubmitting] = useState(false)

  function updateField<K extends keyof ForgotPasswordFormState>(
    field: K,
    value: ForgotPasswordFormState[K],
  ) {
    setForm((current) => ({ ...current, [field]: value }))
    setErrors((current) => ({ ...current, [field]: undefined }))
    setRequestError('')
    setRequestSuccess('')
  }

  function validateForm() {
    const nextErrors: ForgotPasswordFormErrors = {}
    const email = form.email.trim()

    if (!form.idGym) {
      nextErrors.idGym = 'Selecciona un gimnasio.'
    }

    if (!email) {
      nextErrors.email = 'Ingresa tu email.'
    } else if (!/\S+@\S+\.\S+/.test(email)) {
      nextErrors.email = 'Ingresa un email valido.'
    }

    setErrors(nextErrors)

    return Object.keys(nextErrors).length === 0
  }

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault()

    if (!validateForm() || form.idGym === null) {
      return
    }

    setIsSubmitting(true)
    setRequestError('')
    setRequestSuccess('')

    try {
      await authService.forgotPassword({ email: form.email.trim() }, form.idGym)
      setRequestSuccess(successMessage)
    } catch (error) {
      setRequestError(getForgotPasswordErrorMessage(error))
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <main className="auth-page">
      <section className="auth-panel">
        <div className="auth-panel__inner">
          <span className="auth-kicker">Recuperacion de acceso</span>
          <h1 className="auth-title">Recupera tu contrasena sin perder el contexto del gimnasio.</h1>
          <p className="auth-subtitle">
            Selecciona la sede correcta y te enviaremos instrucciones para definir una nueva
            clave.
          </p>

          <form className="auth-form" onSubmit={handleSubmit} noValidate>
            <div className="field-group">
              <label className="field-label" htmlFor="gym-select">
                Gimnasio
              </label>
              <GymSelect
                gyms={gyms}
                selectedGymId={form.idGym}
                onChange={(gymId) => updateField('idGym', gymId)}
                isDisabled={isLoadingGyms || gyms.length === 0 || isSubmitting}
              />
              {errors.idGym ? <p className="field-error">{errors.idGym}</p> : null}
              {gymsError ? <p className="field-error">{gymsError}</p> : null}
            </div>

            <div className="field-group">
              <label className="field-label" htmlFor="email">
                Email
              </label>
              <input
                id="email"
                className="field-input"
                type="email"
                autoComplete="email"
                value={form.email}
                onChange={(event) => updateField('email', event.target.value)}
                disabled={isSubmitting}
              />
              {errors.email ? <p className="field-error">{errors.email}</p> : null}
            </div>

            {requestError ? <div className="form-alert form-alert--error">{requestError}</div> : null}
            {requestSuccess ? (
              <div className="form-alert form-alert--success">{requestSuccess}</div>
            ) : null}

            <div className="auth-actions auth-actions--stacked-mobile">
              <button className="submit-button" type="submit" disabled={isSubmitting}>
                {isSubmitting ? 'Enviando...' : 'Enviar instrucciones'}
              </button>
              <Link className="inline-link" to="/login">
                Volver al login
              </Link>
            </div>
          </form>

          <p className="auth-note">
            El mensaje de confirmacion es deliberadamente generico para no exponer si el email
            existe en la base.
          </p>
        </div>
      </section>

      <AuthShowcase
        eyebrow="Recuperacion guiada"
        title="Un paso publico, seguro y ligado a la sede correcta."
        copy="El flujo conserva la seleccion de gimnasio para que el backend resuelva la tenancy desde el primer request."
        points={[
          'Carga reutilizada de gimnasios activos.',
          'Validacion de email antes de llamar al backend.',
          'Respuesta generica para no revelar usuarios registrados.',
        ]}
      />
    </main>
  )
}
