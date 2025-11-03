/**
 * Schemas de validación para grupos
 * Siguiendo MEJORES_PRACTICAS.md - Validación con Zod
 */
import { z } from 'zod';

export const createGroupSchema = z.object({
  key: z
    .string()
    .min(1, 'Clave es requerida')
    .max(100, 'Máximo 100 caracteres')
    .regex(/^[a-z0-9._-]+$/, 'Solo minúsculas, números, puntos, guiones y guiones bajos'),
  name: z.string().min(1, 'Nombre es requerido').max(255, 'Máximo 255 caracteres'),
  description: z.string().max(1000, 'Máximo 1000 caracteres').optional(),
});

export const updateGroupSchema = createGroupSchema.partial();

export const setGroupActionsSchema = z.object({
  actionKeys: z.array(z.string()),
});

export const setGroupChildrenSchema = z.object({
  childGroupKeys: z.array(z.string()),
});

// Exportar tipos inferidos
export type CreateGroupFormData = z.infer<typeof createGroupSchema>;
export type UpdateGroupFormData = z.infer<typeof updateGroupSchema>;
export type SetGroupActionsFormData = z.infer<typeof setGroupActionsSchema>;
export type SetGroupChildrenFormData = z.infer<typeof setGroupChildrenSchema>;
