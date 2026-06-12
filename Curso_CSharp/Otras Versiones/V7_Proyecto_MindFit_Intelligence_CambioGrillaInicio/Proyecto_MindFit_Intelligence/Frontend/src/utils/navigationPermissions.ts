import type { Formulario } from '../types/formulario'

export interface NavigationItem {
  key: 'rutinas' | 'socios' | 'gimnasio'
  label: string
  description: string
  path: string
  permissionCodes: readonly string[]
}

export const navigationItems: readonly NavigationItem[] = [
  {
    key: 'rutinas',
    label: 'Gestionar rutinas',
    description: 'Consulta y administra el trabajo planificado.',
    path: '/rutinas',
    permissionCodes: [
      'EDITAR_RUTINA',
      'VER_HISTORIAL_RUTINA',
      'ELIMINAR_RUTINA',
      'RECUPERAR_RUTINA',
    ],
  },
  {
    key: 'socios',
    label: 'Ver socios',
    description: 'Accede a socios y gestiona sus turnos.',
    path: '/socios',
    permissionCodes: [
      'CREAR_USUARIO_SOCIO',
      'EDITAR_USUARIO_SOCIO',
      'ELIMINAR_USUARIO_SOCIO',
      'AGREGAR_TURNO',
      'CANCELAR_TURNO',
    ],
  },
  {
    key: 'gimnasio',
    label: 'Gestionar gimnasio',
    description: 'Configura equipo, personal y operacion.',
    path: '/gimnasio',
    permissionCodes: [
      'CREAR_USUARIO_RESPONSABLE',
      'EDITAR_USUARIO_RESPONSABLE',
      'ELIMINAR_USUARIO_RESPONSABLE_DEFINITIVAMENTE',
      'CREAR_GRUPO',
      'EDITAR_GRUPO',
      'ELIMINAR_GRUPO',
      'CREAR_EQUIPAMIENTO',
      'EDITAR_EQUIPAMIENTO',
      'ELIMINAR_EQUIPAMIENTO',
      'CREAR_MAQUINA',
      'EDITAR_MAQUINA',
      'ELIMINAR_MAQUINA',
      'CREAR_EJERCICIO',
      'EDITAR_EJERCICIO',
      'ELIMINAR_EJERCICIO',
      'MODIFICAR_DIA_RH',
      'QUITAR_ENTRENADOR_DIA_RH',
    ],
  },
] as const

export function getVisibleNavigationItems(
  userPermissions: readonly string[],
  formularios: readonly Formulario[],
) {
  const userPermissionSet = new Set(userPermissions)

  return navigationItems.filter((item) => {
    const compatibleFormPermissions = formularios
      .filter((formulario) =>
        formulario.permisos.some((permission) => item.permissionCodes.includes(permission)),
      )
      .flatMap((formulario) => formulario.permisos)

    return compatibleFormPermissions.some(
      (permission) => item.permissionCodes.includes(permission) && userPermissionSet.has(permission),
    )
  })
}
