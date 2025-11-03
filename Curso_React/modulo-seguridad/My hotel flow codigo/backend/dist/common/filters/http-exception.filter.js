"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var HttpExceptionFilter_1;
Object.defineProperty(exports, "__esModule", { value: true });
exports.HttpExceptionFilter = void 0;
const common_1 = require("@nestjs/common");
const crypto_1 = require("crypto");
const HTTP_CODE_TO_ERROR_CODE = {
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
let HttpExceptionFilter = HttpExceptionFilter_1 = class HttpExceptionFilter {
    logger = new common_1.Logger(HttpExceptionFilter_1.name);
    catch(exception, host) {
        const ctx = host.switchToHttp();
        const response = ctx.getResponse();
        const request = ctx.getRequest();
        const requestId = request.requestId ||
            request.headers['x-request-id'] ||
            (0, crypto_1.randomUUID)();
        let status = common_1.HttpStatus.INTERNAL_SERVER_ERROR;
        let errorCode = 'INTERNAL_SERVER_ERROR';
        let message = 'An unexpected error occurred';
        let details = undefined;
        let additionalProps = {};
        if (exception instanceof common_1.HttpException) {
            status = exception.getStatus();
            const exceptionResponse = exception.getResponse();
            errorCode = HTTP_CODE_TO_ERROR_CODE[status] || 'UNKNOWN_ERROR';
            if (typeof exceptionResponse === 'object') {
                const responseObj = exceptionResponse;
                message =
                    responseObj.message ||
                        (Array.isArray(responseObj.message)
                            ? responseObj.message[0]
                            : responseObj.error) ||
                        message;
                if (responseObj.error && typeof responseObj.error === 'string') {
                    errorCode = responseObj.error;
                }
                if (Array.isArray(responseObj.message)) {
                    details = responseObj.message.map((msg) => {
                        if (typeof msg === 'string') {
                            return { message: msg };
                        }
                        return msg;
                    });
                }
                else if (responseObj.details) {
                    details = responseObj.details;
                }
                const { message: _, error: __, details: ___, ...rest } = responseObj;
                additionalProps = rest;
            }
            else {
                message = exceptionResponse;
            }
        }
        else if (exception instanceof Error) {
            message = exception.message;
            this.logger.error(`Unhandled error: ${exception.message}`, exception.stack);
        }
        const errorResponse = {
            success: false,
            error: {
                code: errorCode,
                message,
                ...(details && { details }),
                ...(status === 500 && { errorId: (0, crypto_1.randomUUID)() }),
                ...additionalProps,
            },
            meta: {
                timestamp: new Date().toISOString(),
                requestId,
            },
        };
        if (status >= 500) {
            this.logger.error(`[${requestId}] ${status} ${errorCode}: ${message}`, exception instanceof Error ? exception.stack : '');
        }
        else if (status >= 400) {
            this.logger.warn(`[${requestId}] ${status} ${errorCode}: ${message}`, JSON.stringify(details || {}));
        }
        response.status(status).json(errorResponse);
    }
};
exports.HttpExceptionFilter = HttpExceptionFilter;
exports.HttpExceptionFilter = HttpExceptionFilter = HttpExceptionFilter_1 = __decorate([
    (0, common_1.Catch)()
], HttpExceptionFilter);
//# sourceMappingURL=http-exception.filter.js.map