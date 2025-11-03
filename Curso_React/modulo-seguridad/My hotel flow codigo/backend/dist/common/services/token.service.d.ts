import { ConfigService } from '@nestjs/config';
import { JwtService } from '@nestjs/jwt';
import { Repository } from 'typeorm';
import { RevokedTokenEntity } from '@infra/database/entities';
export interface JwtPayload {
    sub: number;
    username: string;
    email: string;
    jti: string;
    type: 'access' | 'refresh';
    iat?: number;
    exp?: number;
}
export interface TokenPair {
    accessToken: string;
    refreshToken: string;
    expiresIn: number;
}
export declare class TokenService {
    private jwtService;
    private config;
    private revokedTokenRepo;
    constructor(jwtService: JwtService, config: ConfigService, revokedTokenRepo: Repository<RevokedTokenEntity>);
    generateTokenPair(userId: number, username: string, email: string): TokenPair;
    verifyToken(token: string, expectedType?: 'access' | 'refresh'): Promise<JwtPayload>;
    revokeToken(jti: string, userId: number, tokenType: 'access' | 'refresh', reason: string, ip?: string): Promise<void>;
    isRevoked(jti: string): Promise<boolean>;
    cleanExpiredTokens(): Promise<number>;
    private parseExpirationToSeconds;
    private calculateExpirationDate;
}
