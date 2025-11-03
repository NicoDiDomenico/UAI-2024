/**
 * HTTP Exception Filter - Maneja excepciones y errores de forma estandarizada
 * Patrón: Exception Filter - Captura excepciones y las transforma en respuestas consistentes
 * Siguiendo MEJORES_PRACTICAS.md - Estructura estándar de respuestas API
 */
import {
  ExceptionFilter,
  Catch,
  ArgumentsHost,
  HttpException,
  HttpStatus,
  Logger,
} from '@nestjs/common';
import { Request, Response } from 'express';
import { randomUUID } from 'crypto';
import {
  ApiErrorResponse,
  ValidationErrorDetail,
} from '@common/interfaces/api-response.interface';

/**
 * Mapeo de códigos HTTP a códigos de error estándar
 */
const HTTP_CODE_TO_ERROR_CODE: Record<number, string> = {
  400: 'BAD_REQUEST',
  401: 'UNAUTHORIZED',
  403: 'FORBIDDEN',
  404: 'NOT_FOUND',
  409: 'CONFLICT',
  422: 'UNPROCESSABLE_ENTITY',
  429: 'RATE_LIMIT_EXCEEDED',
  500: 'INTERNAL_SERVER_ERROR',
  503: 'SERVICE_UNAVAILABLE',
};

@Catch()
export class HttpExceptionFilter implements ExceptionFilter {
  private readonly logger = new Logger(HttpExceptionFilter.name);

  catch(exception: unknown, host: ArgumentsHost): void {
    const ctx = host.switchToHttp();
    const response = ctx.getResponse<Response>();
    const request = ctx.getRequest<Request>();

    // Obtener o generar request ID
    const requestId =
      (request as any).requestId ||
      request.headers['x-request-id'] ||
      randomUUID();

    let status = HttpStatus.INTERNAL_SERVER_ERROR;
    let errorCode = 'INTERNAL_SERVER_ERROR';
    let message = 'An unexpected error occurred';
    let details: ValidationErrorDetail[] | any = undefined;
    let additionalProps: Record<string, any> = {};

    // Manejar HttpException de NestJS
    if (exception instanceof HttpException) {
      status = exception.getStatus();
      const exceptionResponse = exception.getResponse();

      // Determinar código de error
      errorCode = HTTP_CODE_TO_ERROR_CODE[status] || 'UNKNOWN_ERROR';

      if (typeof exceptionResponse === 'object') {
        const responseObj = exceptionResponse as any;

        // Mensaje
        message =
          responseObj.message ||
          (Array.isArray(responseObj.message)
            ? responseObj.message[0]
            : responseObj.error) ||
          message;

        // Código de error personalizado
        if (responseObj.error && typeof responseObj.error === 'string') {
          errorCode = responseObj.error;
        }

        // Detalles de validación (class-validator)
        if (Array.isArray(responseObj.message)) {
          details = responseObj.message.map((msg: any) => {
            if (typeof msg === 'string') {
              return { message: msg };
            }
            return msg;
          });
        } else if (responseObj.details) {
          details = responseObj.details;
        }

        // Propiedades adicionales
        const { message: _, error: __, details: ___, ...rest } = responseObj;
        additionalProps = rest;
      } else {
        message = exceptionResponse as string;
      }
    } else if (exception instanceof Error) {
      // Error genérico de JavaScript
      message = exception.message;
      this.logger.error(
        `Unhandled error: ${exception.message}`,
        exception.stack,
      );
    }

    // Construir respuesta de error
    const errorResponse: ApiErrorResponse = {
      success: false,
      error: {
        code: errorCode,
        message,
        ...(details && { details }),
        ...(status === 500 && { errorId: randomUUID() }),
        ...additionalProps,
      },
      meta: {
        timestamp: new Date().toISOString(),
        requestId,
      },
    };

    // Log según severidad
    if (status >= 500) {
      this.logger.error(
        `[${requestId}] ${status} ${errorCode}: ${message}`,
        exception instanceof Error ? exception.stack : '',
      );
    } else if (status >= 400) {
      this.logger.warn(
        `[${requestId}] ${status} ${errorCode}: ${message}`,
        JSON.stringify(details || {}),
      );
    }

    // Enviar respuesta
    response.status(status).json(errorResponse);
  }
}
