import type { PropsWithChildren } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { useAuth } from '../hooks/useAuth'

export function AppLayout({ children }: PropsWithChildren) {
  const navigate = useNavigate()
  const { logout, session } = useAuth()

  function handleLogout() {
    logout()
    navigate('/login', { replace: true })
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

        <div className="workspace-session">
          <span className="workspace-session__gym">Gym {session?.idGym ?? '-'}</span>
          <button className="workspace-logout" type="button" onClick={handleLogout}>
            Cerrar sesion
          </button>
        </div>
      </header>

      {children}
    </div>
  )
}
