import { Injectable, UnauthorizedException, Logger } from '@nestjs/common';
import { PassportStrategy } from '@nestjs/passport';
import { ExtractJwt, Strategy } from 'passport-jwt';
import { ConfigService } from '@nestjs/config';
import { TokenService } from '@common/services';
import { UsersService } from '@modules/users/users.service';

/**
 * Payload del JWT
 */
export interface JwtPayload {
  sub: number; // User ID
  username: string;
  email: string;
  jti: string; // Token ID para blacklist
  type: 'access' | 'refresh';
  iat: number;
  exp: number;
}

/**
 * JWT Strategy
 * Patrón: Strategy Pattern (Passport)
 * Responsabilidad: Validar JWT access token y verificar que no esté revocado
 */
@Injectable()
export class JwtStrategy extends PassportStrategy(Strategy, 'jwt') {
  private readonly logger = new Logger(JwtStrategy.name);

  constructor(
    private readonly configService: ConfigService,
    private readonly tokenService: TokenService,
    private readonly usersService: UsersService,
  ) {
    super({
      jwtFromRequest: ExtractJwt.fromAuthHeaderAsBearerToken(),
      ignoreExpiration: false,
      secretOrKey: configService.get<string>('jwt.secret') || 'default-secret',
    });
  }

  /**
   * Validar JWT payload
   * Este método se ejecuta después de verificar la firma del token
   */
  async validate(payload: JwtPayload) {
    // Verificar que sea un access token
    if (payload.type !== 'access') {
      throw new UnauthorizedException('Invalid token type');
    }

    // Verificar que no esté revocado (blacklist)
    const isRevoked = await this.tokenService.isRevoked(payload.jti);
    if (isRevoked) {
      this.logger.warn(`Revoked token used: ${payload.jti}`);
      throw new UnauthorizedException('Token has been revoked');
    }

    // Verificar que el usuario exista y esté activo
    const user = await this.usersService.findOne(payload.sub);
    if (!user || !user.isActive) {
      throw new UnauthorizedException('User not found or inactive');
    }

    // Retornar el usuario para adjuntarlo a request.user
    return user;
  }
}
