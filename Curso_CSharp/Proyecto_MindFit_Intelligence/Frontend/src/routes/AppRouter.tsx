import { Navigate, Route, Routes } from 'react-router-dom'
import { useAuth } from '../hooks/useAuth'
import { GimnasioPage } from '../pages/GimnasioPage'
import { InicioPage } from '../pages/InicioPage'
import { PlaceholderPage } from '../pages/PlaceholderPage'
import { SociosPage } from '../pages/SociosPage'
import { ForgotPasswordPage } from '../pages/auth/ForgotPasswordPage'
import { LoginPage } from '../pages/auth/LoginPage'
import { ResetPasswordPage } from '../pages/auth/ResetPasswordPage'
import { SocioInicioPage } from '../pages/socio/SocioInicioPage'
import { getAuthenticatedHomePath } from '../utils/authRoles'
import { ProtectedRoute } from './ProtectedRoute'

function FallbackRoute() {
  const { isAuthenticated, isHydrated, session } = useAuth()

  if (!isHydrated) {
    return null
  }

  return <Navigate to={isAuthenticated ? getAuthenticatedHomePath(session) : '/login'} replace />
}

export function AppRouter() {
  return (
    <div className="app-shell">
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/forgot-password" element={<ForgotPasswordPage />} />
        <Route path="/reset-password" element={<ResetPasswordPage />} />
        <Route element={<ProtectedRoute />}>
          <Route path="/dashboard" element={<InicioPage />} />
          <Route path="/socio/inicio" element={<SocioInicioPage />} />
          <Route path="/rutinas" element={<PlaceholderPage title="Gestionar rutinas" />} />
          <Route path="/socios" element={<SociosPage />} />
          <Route path="/socios/agregar" element={<SociosPage />} />
          <Route path="/socios/:idUsuario/consultar" element={<SociosPage />} />
          <Route path="/socios/:idUsuario/eliminar" element={<PlaceholderPage title="Eliminar socio" />} />
          <Route path="/socios/:idUsuario/turnos" element={<SociosPage />} />
          <Route path="/gimnasio" element={<GimnasioPage />} />
          <Route path="/gimnasio/usuarios" element={<PlaceholderPage title="Usuarios" />} />
          <Route path="/gimnasio/permisos" element={<PlaceholderPage title="Permisos" />} />
          <Route path="/gimnasio/equipamientos" element={<PlaceholderPage title="Equipamientos" />} />
          <Route path="/gimnasio/maquinas" element={<PlaceholderPage title="Maquinas" />} />
          <Route path="/gimnasio/ejercicios" element={<PlaceholderPage title="Ejercicios" />} />
          <Route
            path="/gimnasio/rangos-horarios"
            element={<PlaceholderPage title="Rangos Horarios" />}
          />
        </Route>
        <Route path="*" element={<FallbackRoute />} />
      </Routes>
    </div>
  )
}
