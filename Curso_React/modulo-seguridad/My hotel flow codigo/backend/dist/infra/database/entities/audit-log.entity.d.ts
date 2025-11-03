export declare class AuditLogEntity {
    id: number;
    userId?: number;
    userIdentity?: string;
    action: string;
    entityType?: string;
    entityId?: string;
    status: string;
    message?: string;
    metadata?: Record<string, any>;
    ip?: string;
    userAgent?: string;
    severity: string;
    createdAt: Date;
}
