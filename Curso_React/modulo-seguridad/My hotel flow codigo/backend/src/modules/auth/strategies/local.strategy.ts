import { Injectable, UnauthorizedException, Logger } from '@nestjs/common';
import { PassportStrategy } from '@nestjs/passport';
import { Strategy } from 'passport-local';
import { AuthService } from '../auth.service';
import { UserEntity } from '@infra/database/entities';

/**
 * Local Strategy
 * Patrón: Strategy Pattern (Passport)
 * Responsabilidad: Validar credenciales (username/email + password)
 */
@Injectable()
export class LocalStrategy extends PassportStrategy(Strategy, 'local') {
  private readonly logger = new Logger(LocalStrategy.name);

  constructor(private readonly authService: AuthService) {
    // Configurar para aceptar 'identity' en lugar de 'username'
    super({
      usernameField: 'identity',
      passwordField: 'password',
    });
  }

  /**
   * Validar credenciales
   * Este método se ejecuta cuando se usa el guard 'local'
   */
  async validate(identity: string, password: string): Promise<UserEntity> {
    const user = await this.authService.validateUser(identity, password);

    if (!user) {
      this.logger.warn(`Failed login attempt for: ${identity}`);
      throw new UnauthorizedException('Invalid credentials');
    }

    return user;
  }
}
