import { Navigate, Route, Routes } from 'react-router-dom'
import { useAuth } from '../hooks/useAuth'
import { BlogPage } from '../pages/BlogPage'
import { ContactoPage } from '../pages/ContactoPage'
import { EjerciciosPage } from '../pages/EjerciciosPage'
import { EquipamientosPage } from '../pages/EquipamientosPage'
import { FuncionalidadesPage } from '../pages/FuncionalidadesPage'
import { GimnasioPage } from '../pages/GimnasioPage'
import { GymOnboardingPage } from '../pages/GymOnboardingPage'
import { InicioPage } from '../pages/InicioPage'
import { LandingPage } from '../pages/LandingPage'
import { MaquinasPage } from '../pages/MaquinasPage'
import { PermisosPage } from '../pages/PermisosPage'
import { PlaceholderPage } from '../pages/PlaceholderPage'
import { PreciosPage } from '../pages/PreciosPage'
import { RangosHorariosPage } from '../pages/RangosHorariosPage'
import { SociosPage } from '../pages/SociosPage'
import { TestimoniosPage } from '../pages/TestimoniosPage'
import { UsuariosPage } from '../pages/UsuariosPage'
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
        <Route path="/" element={<LandingPage />} />
        <Route path="/funcionalidades" element={<FuncionalidadesPage />} />
        <Route path="/precios" element={<PreciosPage />} />
        <Route path="/testimonios" element={<TestimoniosPage />} />
        <Route path="/blog" element={<BlogPage />} />
        <Route path="/contacto" element={<ContactoPage />} />
        <Route path="/registro-gym" element={<GymOnboardingPage />} />
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
          <Route path="/gimnasio/usuarios" element={<UsuariosPage />} />
          <Route path="/gimnasio/permisos" element={<PermisosPage />} />
          <Route path="/gimnasio/equipamientos" element={<EquipamientosPage />} />
          <Route path="/gimnasio/maquinas" element={<MaquinasPage />} />
          <Route path="/gimnasio/ejercicios" element={<EjerciciosPage />} />
          <Route path="/gimnasio/rangos-horarios" element={<RangosHorariosPage />} />
        </Route>
        <Route path="*" element={<FallbackRoute />} />
      </Routes>
    </div>
  )
}
