/**
 * Schemas de validación para usuarios
 * Siguiendo MEJORES_PRACTICAS.md - Validación con Zod
 */
import { z } from 'zod';

export const createUserSchema = z.object({
  username: z
    .string()
    .min(3, 'Mínimo 3 caracteres')
    .max(50, 'Máximo 50 caracteres')
    .regex(/^[a-zA-Z0-9_-]+$/, 'Solo letras, números, guiones y guiones bajos'),
  email: z.string().email('Email inválido'),
  password: z
    .string()
    .min(8, 'Mínimo 8 caracteres')
    .regex(
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]/,
      'Debe contener mayúsculas, minúsculas, números y símbolos'
    )
    .optional(),
  fullName: z.string().max(255, 'Máximo 255 caracteres').optional(),
  role: z.enum(['admin', 'recepcionista', 'cliente']).optional().default('cliente'),
  isActive: z.boolean().optional().default(true),
});

export const updateUserSchema = createUserSchema.partial().omit({ password: true });

export const setUserGroupsSchema = z.object({
  groupKeys: z.array(z.string()).min(1, 'Debe seleccionar al menos un grupo'),
});

export const setUserActionsSchema = z.object({
  actionKeys: z.array(z.string()),
});

export const resetPasswordSchema = z.object({
  newPassword: z
    .string()
    .min(8, 'Mínimo 8 caracteres')
    .regex(
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]/,
      'Debe contener mayúsculas, minúsculas, números y símbolos'
    ),
});

// Exportar tipos inferidos
export type CreateUserFormData = z.infer<typeof createUserSchema>;
export type UpdateUserFormData = z.infer<typeof updateUserSchema>;
export type SetUserGroupsFormData = z.infer<typeof setUserGroupsSchema>;
export type SetUserActionsFormData = z.infer<typeof setUserActionsSchema>;
export type ResetPasswordFormData = z.infer<typeof resetPasswordSchema>;
