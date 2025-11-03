/**
 * App Routes - Configuración de rutas de la aplicación
 * Siguiendo MEJORES_PRACTICAS.md - Estructura de rutas
 */
import React from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import { ProtectedRoute } from './ProtectedRoute';

// Auth Pages
import { LoginPage } from '@/pages/auth/LoginPage';
import { ChangePasswordPage } from '@/pages/auth/ChangePasswordPage';
import { RecoverPasswordPage } from '@/pages/auth/RecoverPasswordPage';
import { ConfirmRecoverPasswordPage } from '@/pages/auth/ConfirmRecoverPasswordPage';

// Dashboard
import { DashboardPage } from '@/pages/dashboard/DashboardPage';

// Users
import { UsersListPage } from '@/pages/users/UsersListPage';
import { UserFormPage } from '@/pages/users/UserFormPage';
import { UserPermissionsPage } from '@/pages/users/UserPermissionsPage';

// Groups
import { GroupsListPage } from '@/pages/groups/GroupsListPage';
import { GroupFormPage } from '@/pages/groups/GroupFormPage';
import { GroupActionsPage } from '@/pages/groups/GroupActionsPage';
import { GroupActionsViewPage } from '@/pages/groups/GroupActionsViewPage';
import { GroupChildrenPage } from '@/pages/groups/GroupChildrenPage';

// Actions
import { ActionsListPage } from '@/pages/actions/ActionsListPage';
import { ActionFormPage } from '@/pages/actions/ActionFormPage';

// Error Pages
import { ForbiddenPage } from '@/pages/errors/ForbiddenPage';

export const AppRoutes: React.FC = () => {
  return (
    <Routes>
      {/* Ruta raíz - redirigir a dashboard */}
      <Route path="/" element={<Navigate to="/dashboard" replace />} />

      {/* Rutas públicas de autenticación */}
      <Route path="/login" element={<LoginPage />} />
      <Route path="/auth/recover" element={<RecoverPasswordPage />} />
      <Route path="/auth/recover/confirm" element={<ConfirmRecoverPasswordPage />} />
      
      {/* Rutas de error */}
      <Route path="/forbidden" element={<ForbiddenPage />} />

      {/* Rutas protegidas - Solo autenticación requerida */}
      <Route element={<ProtectedRoute />}>
        <Route path="/dashboard" element={<DashboardPage />} />
        <Route path="/auth/change-password" element={<ChangePasswordPage />} />
      </Route>
        
      {/* Rutas de usuarios - Requiere permisos de usuarios */}
      <Route element={<ProtectedRoute requiredPermissions={['config.usuarios.listar']} />}>
        <Route path="/users" element={<UsersListPage />} />
      </Route>
      <Route element={<ProtectedRoute requiredPermissions={['config.usuarios.crear']} />}>
        <Route path="/users/create" element={<UserFormPage />} />
      </Route>
      <Route element={<ProtectedRoute requiredPermissions={['config.usuarios.modificar']} />}>
        <Route path="/users/:id/edit" element={<UserFormPage />} />
      </Route>
      <Route element={<ProtectedRoute requiredPermissions={['config.usuarios.asignarGrupos', 'config.usuarios.asignarAcciones']} requireAll={false} />}>
        <Route path="/users/:id/permissions" element={<UserPermissionsPage />} />
      </Route>

      {/* Rutas de grupos - Requiere permisos de grupos */}
      <Route element={<ProtectedRoute requiredPermissions={['config.grupos.listar']} />}>
        <Route path="/groups" element={<GroupsListPage />} />
      </Route>
      <Route element={<ProtectedRoute requiredPermissions={['config.grupos.crear']} />}>
        <Route path="/groups/create" element={<GroupFormPage />} />
      </Route>
      <Route element={<ProtectedRoute requiredPermissions={['config.grupos.modificar']} />}>
        <Route path="/groups/:id/edit" element={<GroupFormPage />} />
      </Route>
      {/* Vista de solo lectura de acciones - solo requiere autenticación */}
      <Route element={<ProtectedRoute />}>
        <Route path="/groups/:id/actions/view" element={<GroupActionsViewPage />} />
      </Route>
      <Route element={<ProtectedRoute requiredPermissions={['config.grupos.asignarAcciones']} />}>
        <Route path="/groups/:id/actions" element={<GroupActionsPage />} />
      </Route>
      <Route element={<ProtectedRoute requiredPermissions={['config.grupos.asignarHijos']} />}>
        <Route path="/groups/:id/children" element={<GroupChildrenPage />} />
      </Route>

      {/* Rutas de acciones - Requiere permisos de acciones */}
      <Route element={<ProtectedRoute requiredPermissions={['config.acciones.listar']} />}>
        <Route path="/actions" element={<ActionsListPage />} />
      </Route>
      <Route element={<ProtectedRoute requiredPermissions={['config.acciones.crear']} />}>
        <Route path="/actions/create" element={<ActionFormPage />} />
      </Route>
      <Route element={<ProtectedRoute requiredPermissions={['config.acciones.modificar']} />}>
        <Route path="/actions/:id/edit" element={<ActionFormPage />} />
      </Route>

      {/* Ruta 404 */}
      <Route path="*" element={<NotFoundPage />} />
    </Routes>
  );
};

// Página 404 simple
const NotFoundPage: React.FC = () => {
  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-50">
      <div className="text-center">
        <h1 className="text-6xl font-bold text-gray-300 mb-4">404</h1>
        <p className="text-xl text-gray-600 mb-8">Página no encontrada</p>
        <a href="/dashboard" className="btn-primary">
          Volver al Dashboard
        </a>
      </div>
    </div>
  );
};
