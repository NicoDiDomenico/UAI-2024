import { Navigate, Route, Routes } from 'react-router-dom'
import { useAuth } from '../hooks/useAuth'
import { InicioPage } from '../pages/InicioPage'
import { PlaceholderPage } from '../pages/PlaceholderPage'
import { SociosPage } from '../pages/SociosPage'
import { ForgotPasswordPage } from '../pages/auth/ForgotPasswordPage'
import { LoginPage } from '../pages/auth/LoginPage'
import { ResetPasswordPage } from '../pages/auth/ResetPasswordPage'
import { ProtectedRoute } from './ProtectedRoute'

function FallbackRoute() {
  const { isAuthenticated, isHydrated } = useAuth()

  if (!isHydrated) {
    return null
  }

  return <Navigate to={isAuthenticated ? '/dashboard' : '/login'} replace />
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
          <Route path="/rutinas" element={<PlaceholderPage title="Gestionar rutinas" />} />
          <Route path="/socios" element={<SociosPage />} />
          <Route path="/socios/agregar" element={<PlaceholderPage title="Agregar socio" />} />
          <Route path="/socios/:idUsuario/consultar" element={<PlaceholderPage title="Consultar socio" />} />
          <Route path="/socios/:idUsuario/eliminar" element={<PlaceholderPage title="Eliminar socio" />} />
          <Route path="/socios/:idUsuario/turnos" element={<PlaceholderPage title="Turnos del socio" />} />
          <Route path="/gimnasio" element={<PlaceholderPage title="Gestionar gimnasio" />} />
        </Route>
        <Route path="*" element={<FallbackRoute />} />
      </Routes>
    </div>
  )
}
