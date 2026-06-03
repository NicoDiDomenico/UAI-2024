import { Navigate, Route, Routes } from 'react-router-dom'
import { useAuth } from '../hooks/useAuth'
import { DashboardPage } from '../pages/DashboardPage'
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
          <Route path="/dashboard" element={<DashboardPage />} />
        </Route>
        <Route path="*" element={<FallbackRoute />} />
      </Routes>
    </div>
  )
}
