/**
 * Transform Interceptor - Envuelve respuestas exitosas en estructura estándar
 * Patrón: Interceptor - Transforma respuestas antes de enviarlas al cliente
 * Siguiendo MEJORES_PRACTICAS.md - Estructura estándar de respuestas API
 */
import {
  Injectable,
  NestInterceptor,
  ExecutionContext,
  CallHandler,
} from '@nestjs/common';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { randomUUID } from 'crypto';
import { ApiSuccessResponse } from '@common/interfaces/api-response.interface';

@Injectable()
export class TransformInterceptor<T>
  implements NestInterceptor<T, ApiSuccessResponse<T>>
{
  intercept(
    context: ExecutionContext,
    next: CallHandler,
  ): Observable<ApiSuccessResponse<T>> {
    const request = context.switchToHttp().getRequest();

    // Obtener o generar request ID
    const requestId = request.headers['x-request-id'] || randomUUID();

    // Agregar requestId al request para logging/auditing
    request.requestId = requestId;

    return next.handle().pipe(
      map((data) => {
        // Si la respuesta ya tiene la estructura correcta, devolverla tal cual
        if (data && typeof data === 'object' && 'success' in data) {
          return data;
        }

        // Si es un objeto con data y pagination (respuesta paginada)
        if (
          data &&
          typeof data === 'object' &&
          'data' in data &&
          'pagination' in data
        ) {
          return {
            success: true,
            data: data.data,
            pagination: data.pagination,
            meta: {
              timestamp: new Date().toISOString(),
              requestId,
              ...(data.meta || {}),
            },
          };
        }

        // Envolver datos en estructura estándar
        return {
          success: true,
          data,
          meta: {
            timestamp: new Date().toISOString(),
            requestId,
          },
        };
      }),
    );
  }
}
