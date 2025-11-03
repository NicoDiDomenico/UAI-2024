export declare class RevokedTokenEntity {
    id: number;
    jti: string;
    userId: number;
    tokenType: string;
    reason: string;
    expiresAt: Date;
    ip?: string;
    createdAt: Date;
}
