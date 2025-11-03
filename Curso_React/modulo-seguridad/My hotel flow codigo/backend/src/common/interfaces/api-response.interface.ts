/**
 * Interfaces para respuestas API estandarizadas
 * Siguiendo MEJORES_PRACTICAS.md - Estructura estándar de respuestas API
 */

/**
 * Metadata común para todas las respuestas
 */
export interface ApiMetadata {
  timestamp: string;
  requestId: string;
  [key: string]: any; // Permite metadata adicional (ej: total para listas)
}

/**
 * Metadata de paginación
 */
export interface PaginationMeta {
  page: number;
  limit: number;
  total: number;
  totalPages: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}

/**
 * Respuesta exitosa genérica
 */
export interface ApiSuccessResponse<T = any> {
  success: true;
  data?: T;
  message?: string;
  pagination?: PaginationMeta;
  meta: ApiMetadata;
}

/**
 * Detalle de error de validación
 */
export interface ValidationErrorDetail {
  field: string;
  message: string;
  constraint?: string;
  value?: any;
}

/**
 * Error de API
 */
export interface ApiErrorDetail {
  code: string;
  message: string;
  details?: ValidationErrorDetail[] | any;
  errorId?: string; // Para errores 500
  resource?: string; // Para errores 404
  resourceId?: number | string; // Para errores 404
  conflictField?: string; // Para errores 409
  conflictValue?: any; // Para errores 409
  requiredPermissions?: string[]; // Para errores 403
  [key: string]: any; // Permite propiedades adicionales
}

/**
 * Respuesta de error
 */
export interface ApiErrorResponse {
  success: false;
  error: ApiErrorDetail;
  meta: ApiMetadata;
}

/**
 * Unión de tipos de respuesta
 */
export type ApiResponse<T = any> = ApiSuccessResponse<T> | ApiErrorResponse;

/**
 * Helper para crear respuestas de éxito
 */
export function createSuccessResponse<T>(
  data: T,
  meta: Partial<ApiMetadata> = {},
): ApiSuccessResponse<T> {
  return {
    success: true,
    data,
    meta: {
      timestamp: new Date().toISOString(),
      ...meta,
    } as ApiMetadata,
  };
}

/**
 * Helper para crear respuestas paginadas
 */
export function createPaginatedResponse<T>(
  data: T[],
  pagination: PaginationMeta,
  meta: Partial<ApiMetadata> = {},
): ApiSuccessResponse<T[]> {
  return {
    success: true,
    data,
    pagination,
    meta: {
      timestamp: new Date().toISOString(),
      ...meta,
    } as ApiMetadata,
  };
}

/**
 * Helper para crear respuestas de error
 */
export function createErrorResponse(
  error: Partial<ApiErrorDetail>,
  meta: Partial<ApiMetadata> = {},
): ApiErrorResponse {
  return {
    success: false,
    error: {
      code: error.code || 'INTERNAL_SERVER_ERROR',
      message: error.message || 'An unexpected error occurred',
      ...error,
    },
    meta: {
      timestamp: new Date().toISOString(),
      ...meta,
    } as ApiMetadata,
  };
}
