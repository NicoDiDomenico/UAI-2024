/**
 * Componente Can - Renderizado condicional basado en permisos
 * Siguiendo MEJORES_PRACTICAS.md - Separación de concerns
 *
 * Ejemplos de uso:
 * <Can perform="users.create">
 *   <button>Crear Usuario</button>
 * </Can>
 *
 * <Can perform={['users.create', 'users.edit']} requireAll={false}>
 *   <button>Gestionar Usuarios</button>
 * </Can>
 */
import React from 'react';
import { usePermissions } from '@/contexts/PermissionsContext';

interface CanProps {
  perform: string | string[];
  requireAll?: boolean; // true = AND, false = OR
  children: React.ReactNode;
  fallback?: React.ReactNode;
}

export const Can: React.FC<CanProps> = ({
  perform,
  requireAll = true,
  children,
  fallback = null,
}) => {
  const { hasPermission, hasAllPermissions, hasAnyPermission } = usePermissions();

  const permissions = Array.isArray(perform) ? perform : [perform];

  let hasAccess: boolean;

  if (permissions.length === 1) {
    hasAccess = hasPermission(permissions[0]);
  } else {
    hasAccess = requireAll
      ? hasAllPermissions(permissions)
      : hasAnyPermission(permissions);
  }

  return hasAccess ? <>{children}</> : <>{fallback}</>;
};
