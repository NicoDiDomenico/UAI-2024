import { useState } from 'react'
import { Link, Navigate, useNavigate } from 'react-router-dom'
import { AuthShowcase } from '../../components/auth/AuthShowcase'
import { GymSelect } from '../../components/auth/GymSelect'
import { useAuth } from '../../hooks/useAuth'
import { useActiveGyms } from '../../hooks/useActiveGyms'
import { getLoginErrorMessage } from '../../utils/apiError'

interface LoginFormState {
  idGym: number | null
  username: string
  password: string
}

interface LoginFormErrors {
  idGym?: string
  username?: string
  password?: string
}

const initialFormState: LoginFormState = {
  idGym: null,
  username: '',
  password: '',
}

export function LoginPage() {
  const navigate = useNavigate()
  const { isAuthenticated, isHydrated, login } = useAuth()
  const { gyms, gymsError, isLoadingGyms } = useActiveGyms()
  const [form, setForm] = useState(initialFormState)
  const [errors, setErrors] = useState<LoginFormErrors>({})
  const [loginError, setLoginError] = useState('')
  const [isSubmitting, setIsSubmitting] = useState(false)

  if (!isHydrated) {
    return null
  }

  if (isAuthenticated) {
    return <Navigate to="/dashboard" replace />
  }

  function updateField<K extends keyof LoginFormState>(field: K, value: LoginFormState[K]) {
    setForm((current) => ({ ...current, [field]: value }))
    setErrors((current) => ({ ...current, [field]: undefined }))
    setLoginError('')
  }

  function validateForm() {
    const nextErrors: LoginFormErrors = {}

    if (!form.idGym) {
      nextErrors.idGym = 'Selecciona un gimnasio para continuar.'
    }

    if (!form.username.trim()) {
      nextErrors.username = 'Ingresa tu usuario.'
    }

    if (!form.password.trim()) {
      nextErrors.password = 'Ingresa tu contrasena.'
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
    setLoginError('')

    try {
      await login({
        idGym: form.idGym,
        username: form.username.trim(),
        password: form.password,
      })

      navigate('/dashboard', { replace: true })
    } catch (error) {
      setLoginError(getLoginErrorMessage(error))
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <main className="auth-page">
      <section className="auth-panel">
        <div className="auth-panel__inner">
          <span className="auth-kicker">MindFit Intelligence</span>
          <h1 className="auth-title">Opera tu gimnasio desde un inicio de sesion claro.</h1>
          <p className="auth-subtitle">
            Elige la sede, ingresa tus credenciales y continua con el trabajo diario sin
            pasos extra.
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
              <label className="field-label" htmlFor="username">
                Usuario
              </label>
              <input
                id="username"
                className="field-input"
                type="text"
                autoComplete="username"
                value={form.username}
                onChange={(event) => updateField('username', event.target.value)}
                disabled={isSubmitting}
              />
              {errors.username ? <p className="field-error">{errors.username}</p> : null}
            </div>

            <div className="field-group">
              <label className="field-label" htmlFor="password">
                Contrasena
              </label>
              <input
                id="password"
                className="field-input"
                type="password"
                autoComplete="current-password"
                value={form.password}
                onChange={(event) => updateField('password', event.target.value)}
                disabled={isSubmitting}
              />
              {errors.password ? <p className="field-error">{errors.password}</p> : null}
            </div>

            {loginError ? <div className="form-alert form-alert--error">{loginError}</div> : null}

            <div className="auth-actions">
              <button className="submit-button" type="submit" disabled={isSubmitting}>
                {isSubmitting ? 'Ingresando...' : 'Iniciar sesion'}
              </button>
              <Link className="inline-link" to="/forgot-password">
                Olvide mi contrasena
              </Link>
            </div>
          </form>

          <p className="auth-note">
            En esta etapa conservamos los permisos tal como responde el backend, sin
            evaluacion visual.
          </p>
        </div>
      </section>

      <AuthShowcase
        eyebrow="Calma operativa y confianza cotidiana"
        title="Una entrada simple para una operacion completa."
        copy="El acceso prioriza claridad, foco y contexto para responsables y equipos del gimnasio."
        points={[
          'Seleccion rapida de sede con busqueda.',
          'Errores visibles sin ruido innecesario.',
          'Ruta protegida y sesion persistida desde el primer paso.',
        ]}
      />
    </main>
  )
}
