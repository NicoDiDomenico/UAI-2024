"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.createSuccessResponse = createSuccessResponse;
exports.createPaginatedResponse = createPaginatedResponse;
exports.createErrorResponse = createErrorResponse;
function createSuccessResponse(data, meta = {}) {
    return {
        success: true,
        data,
        meta: {
            timestamp: new Date().toISOString(),
            ...meta,
        },
    };
}
function createPaginatedResponse(data, pagination, meta = {}) {
    return {
        success: true,
        data,
        pagination,
        meta: {
            timestamp: new Date().toISOString(),
            ...meta,
        },
    };
}
function createErrorResponse(error, meta = {}) {
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
        },
    };
}
//# sourceMappingURL=api-response.interface.js.map