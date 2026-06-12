import type { PermissionNavigationItem } from '../../utils/navigationPermissions'

export interface GimnasioMenuItemConfig extends PermissionNavigationItem {
  description: string
  iconLabel: string
}

export const gimnasioMenuItems: readonly GimnasioMenuItemConfig[] = [
  {
    key: 'usuarios',
    label: 'Usuarios',
    description: 'Alta, edicion y baja de responsables.',
    path: '/gimnasio/usuarios',
    iconLabel: 'US',
    permissionCodes: [
      'CREAR_USUARIO_RESPONSABLE',
      'EDITAR_USUARIO_RESPONSABLE',
      'ELIMINAR_USUARIO_RESPONSABLE_DEFINITIVAMENTE',
    ],
  },
  {
    key: 'permisos',
    label: 'Permisos',
    description: 'Gestion de grupos y accesos.',
    path: '/gimnasio/permisos',
    iconLabel: 'PR',
    permissionCodes: ['CREAR_GRUPO', 'EDITAR_GRUPO', 'ELIMINAR_GRUPO'],
  },
  {
    key: 'equipamientos',
    label: 'Equipamientos',
    description: 'Elementos disponibles del gimnasio.',
    path: '/gimnasio/equipamientos',
    iconLabel: 'EQ',
    permissionCodes: ['CREAR_EQUIPAMIENTO', 'EDITAR_EQUIPAMIENTO', 'ELIMINAR_EQUIPAMIENTO'],
  },
  {
    key: 'maquinas',
    label: 'Maquinas',
    description: 'Inventario operativo de maquinas.',
    path: '/gimnasio/maquinas',
    iconLabel: 'MQ',
    permissionCodes: ['CREAR_MAQUINA', 'EDITAR_MAQUINA', 'ELIMINAR_MAQUINA'],
  },
  {
    key: 'ejercicios',
    label: 'Ejercicios',
    description: 'Base de ejercicios para rutinas.',
    path: '/gimnasio/ejercicios',
    iconLabel: 'EX',
    permissionCodes: ['CREAR_EJERCICIO', 'EDITAR_EJERCICIO', 'ELIMINAR_EJERCICIO'],
  },
  {
    key: 'rangos-horarios',
    label: 'Rangos Horarios',
    description: 'Dias, horarios y entrenadores.',
    path: '/gimnasio/rangos-horarios',
    iconLabel: 'RH',
    permissionCodes: ['MODIFICAR_DIA_RH', 'QUITAR_ENTRENADOR_DIA_RH'],
  },
] as const
