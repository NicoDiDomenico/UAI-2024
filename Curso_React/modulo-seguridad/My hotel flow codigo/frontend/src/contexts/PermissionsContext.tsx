/**
 * Permissions Context - Manejo global de permisos
 * Siguiendo MEJORES_PRACTICAS.md - Context Pattern + React Query
 */
import React, { createContext, useContext, useState, useEffect } from 'react';
import { useQuery } from '@tanstack/react-query';
import { permissionsApi } from '@/api/permissions.api';
import { PermissionsContextValue } from '@/types/permissions.types';
import { setItem, removeToken } from '@/utils/storage';
import { PERMISSIONS_CACHE_KEY } from '@/config/constants';
import { useAuth } from './AuthContext';

const PermissionsContext = createContext<PermissionsContextValue | undefined>(
  undefined
);

export const PermissionsProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [permissions, setPermissions] = useState<Set<string>>(new Set());
  const { isAuthenticated } = useAuth();

  // Query para obtener permisos del usuario
  // Se ejecuta automáticamente cuando isAuthenticated cambia a true
  const { data, isLoading, refetch } = useQuery<string[]>({
    queryKey: ['permissions'],
    queryFn: permissionsApi.getPermissions,
    enabled: isAuthenticated,
    staleTime: 5 * 60 * 1000, // 5 minutos
    refetchOnWindowFocus: false,
  });

  // Sincronizar permisos cuando cambian
  useEffect(() => {
    if (data) {
      const permsSet = new Set(data);
      setPermissions(permsSet);
      setItem(PERMISSIONS_CACHE_KEY, data);
    }
  }, [data]);

  // Limpiar permisos cuando el usuario cierra sesión
  useEffect(() => {
    if (!isAuthenticated) {
      setPermissions(new Set());
      removeToken(PERMISSIONS_CACHE_KEY);
    }
  }, [isAuthenticated]);

  const hasPermission = (action: string): boolean => {
    return permissions.has(action);
  };

  const hasAllPermissions = (actions: string[]): boolean => {
    return actions.every((action) => permissions.has(action));
  };

  const hasAnyPermission = (actions: string[]): boolean => {
    return actions.some((action) => permissions.has(action));
  };

  const refetchPermissions = async (): Promise<void> => {
    await refetch();
  };

  const value: PermissionsContextValue = {
    permissions,
    isLoading,
    hasPermission,
    hasAllPermissions,
    hasAnyPermission,
    refetchPermissions,
  };

  return (
    <PermissionsContext.Provider value={value}>
      {children}
    </PermissionsContext.Provider>
  );
};

export const usePermissions = (): PermissionsContextValue => {
  const context = useContext(PermissionsContext);
  if (context === undefined) {
    throw new Error('usePermissions must be used within a PermissionsProvider');
  }
  return context;
};
