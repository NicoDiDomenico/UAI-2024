import { Injectable, UnauthorizedException } from '@nestjs/common';
import { ConfigService } from '@nestjs/config';
import { JwtService } from '@nestjs/jwt';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { RevokedTokenEntity } from '@infra/database/entities';
import { v4 as uuidv4 } from 'uuid';

/**
 * Payload del JWT
 */
export interface JwtPayload {
  sub: number; // User ID
  username: string;
  email: string;
  jti: string; // JWT ID único
  type: 'access' | 'refresh';
  iat?: number; // Issued at
  exp?: number; // Expiration
}

/**
 * Par de tokens (access + refresh)
 */
export interface TokenPair {
  accessToken: string;
  refreshToken: string;
  expiresIn: number;
}

/**
 * Servicio de gestión de tokens JWT
 * Patrón: Service Layer + Repository
 * Responsabilidad: Generar, validar y revocar tokens
 */
@Injectable()
export class TokenService {
  constructor(
    private jwtService: JwtService,
    private config: ConfigService,
    @InjectRepository(RevokedTokenEntity)
    private revokedTokenRepo: Repository<RevokedTokenEntity>,
  ) {}

  /**
   * Generar un par de tokens (access + refresh)
   * @param userId - ID del usuario
   * @param username - Username del usuario
   * @param email - Email del usuario
   * @returns Par de tokens
   */
  generateTokenPair(
    userId: number,
    username: string,
    email: string,
  ): TokenPair {
    const accessJti = uuidv4();
    const refreshJti = uuidv4();

    const accessPayload: Omit<JwtPayload, 'iat' | 'exp'> = {
      sub: userId,
      username,
      email,
      jti: accessJti,
      type: 'access',
    };

    const refreshPayload: Omit<JwtPayload, 'iat' | 'exp'> = {
      sub: userId,
      username,
      email,
      jti: refreshJti,
      type: 'refresh',
    };

    const accessExpiration =
      this.config.get<string>('jwt.accessExpiration') || '15m';
    const refreshExpiration =
      this.config.get<string>('jwt.refreshExpiration') || '7d';

    // JwtService.sign() acepta el payload como object y las opciones con expiresIn

    const accessToken = this.jwtService.sign(
      accessPayload as any,
      { expiresIn: accessExpiration } as any,
    );

    const refreshToken = this.jwtService.sign(
      refreshPayload as any,
      { expiresIn: refreshExpiration } as any,
    );

    // Calcular tiempo de expiración en segundos
    const expiresIn = this.parseExpirationToSeconds(accessExpiration);

    return {
      accessToken,
      refreshToken,
      expiresIn,
    };
  }

  /**
   * Verificar y decodificar un token
   * @param token - Token a verificar
   * @param expectedType - Tipo esperado de token (opcional)
   * @returns Payload decodificado
   * @throws UnauthorizedException si el token es inválido o está revocado
   */
  async verifyToken(
    token: string,
    expectedType?: 'access' | 'refresh',
  ): Promise<JwtPayload> {
    try {
      const payload = this.jwtService.verify<JwtPayload>(token);

      // Verificar si el token está en la blacklist
      if (await this.isRevoked(payload.jti)) {
        throw new UnauthorizedException('Token has been revoked');
      }

      return payload;
    } catch (error) {
      if ((error as Error).message === 'Token has been revoked') {
        throw error;
      }
      throw new UnauthorizedException('Invalid or expired token');
    }
  }

  /**
   * Revocar un token (agregarlo a la blacklist)
   * @param jti - JWT ID
   * @param userId - ID del usuario
   * @param tokenType - Tipo de token
   * @param reason - Razón de la revocación
   * @param ip - IP del cliente (opcional)
   */
  async revokeToken(
    jti: string,
    userId: number,
    tokenType: 'access' | 'refresh',
    reason: string,
    ip?: string,
  ): Promise<void> {
    // Calcular fecha de expiración del token
    const expiration = tokenType === 'access' ? '15m' : '7d';
    const expiresAt = this.calculateExpirationDate(expiration);

    const revokedToken = this.revokedTokenRepo.create({
      jti,
      userId,
      tokenType,
      reason,
      expiresAt,
      ip,
    });

    await this.revokedTokenRepo.save(revokedToken);
  }

  /**
   * Verificar si un token está revocado
   * @param jti - JWT ID
   * @returns true si está revocado, false si no
   */
  async isRevoked(jti: string): Promise<boolean> {
    const revoked = await this.revokedTokenRepo.findOne({
      where: { jti },
    });

    return revoked !== null;
  }

  /**
   * Limpiar tokens expirados de la blacklist
   * Debe ejecutarse periódicamente (cron job)
   */
  async cleanExpiredTokens(): Promise<number> {
    const result = await this.revokedTokenRepo
      .createQueryBuilder()
      .delete()
      .where('expiresAt < :now', { now: new Date() })
      .execute();

    return result.affected || 0;
  }

  /**
   * Parsear string de expiración a segundos
   * Ejemplos: '15m' -> 900, '7d' -> 604800
   */
  private parseExpirationToSeconds(expiration: string): number {
    const value = parseInt(expiration.slice(0, -1));
    const unit = expiration.slice(-1);

    switch (unit) {
      case 's':
        return value;
      case 'm':
        return value * 60;
      case 'h':
        return value * 3600;
      case 'd':
        return value * 86400;
      default:
        return 900; // 15 minutos por defecto
    }
  }

  /**
   * Calcular fecha de expiración a partir de un string
   */
  private calculateExpirationDate(expiration: string): Date {
    const seconds = this.parseExpirationToSeconds(expiration);
    return new Date(Date.now() + seconds * 1000);
  }
}
