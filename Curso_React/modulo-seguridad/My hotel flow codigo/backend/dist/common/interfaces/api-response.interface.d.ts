export interface ApiMetadata {
    timestamp: string;
    requestId: string;
    [key: string]: any;
}
export interface PaginationMeta {
    page: number;
    limit: number;
    total: number;
    totalPages: number;
    hasNextPage: boolean;
    hasPreviousPage: boolean;
}
export interface ApiSuccessResponse<T = any> {
    success: true;
    data?: T;
    message?: string;
    pagination?: PaginationMeta;
    meta: ApiMetadata;
}
export interface ValidationErrorDetail {
    field: string;
    message: string;
    constraint?: string;
    value?: any;
}
export interface ApiErrorDetail {
    code: string;
    message: string;
    details?: ValidationErrorDetail[] | any;
    errorId?: string;
    resource?: string;
    resourceId?: number | string;
    conflictField?: string;
    conflictValue?: any;
    requiredPermissions?: string[];
    [key: string]: any;
}
export interface ApiErrorResponse {
    success: false;
    error: ApiErrorDetail;
    meta: ApiMetadata;
}
export type ApiResponse<T = any> = ApiSuccessResponse<T> | ApiErrorResponse;
export declare function createSuccessResponse<T>(data: T, meta?: Partial<ApiMetadata>): ApiSuccessResponse<T>;
export declare function createPaginatedResponse<T>(data: T[], pagination: PaginationMeta, meta?: Partial<ApiMetadata>): ApiSuccessResponse<T[]>;
export declare function createErrorResponse(error: Partial<ApiErrorDetail>, meta?: Partial<ApiMetadata>): ApiErrorResponse;
