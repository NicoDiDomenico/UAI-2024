import {
  Injectable,
  UnauthorizedException,
  BadRequestException,
} from '@nestjs/common';
import { ConfigService } from '@nestjs/config';
import { UsersService } from '@modules/users/users.service';
import { HashService, TokenService, AuthorizationService } from '@common/services';
import { LoggerService } from '@common/logger/logger.service';
import {
  LoginDto,
  RefreshDto,
  ChangePasswordDto,
  RecoverRequestDto,
  RecoverConfirmDto,
} from './dto';
import { UserEntity } from '@infra/database/entities';
import { randomBytes } from 'crypto';

/**
 * Interface para la respuesta de login/refresh
 */
export interface AuthTokens {
  accessToken: string;
  refreshToken: string;
}

/**
 * Servicio de autenticación
 * Patrón: Service Layer
 * Responsabilidad: Login, logout, refresh, cambio de contraseña, recuperación
 */
@Injectable()
export class AuthService {
  private readonly logger = new LoggerService(AuthService.name);

  constructor(
    private readonly usersService: UsersService,
    private readonly hashService: HashService,
    private readonly tokenService: TokenService,
    private readonly authorizationService: AuthorizationService,
    private readonly configService: ConfigService,
  ) {}

  /**
   * Validar credenciales de usuario
   * Usado por LocalStrategy
   */
  async validateUser(
    identity: string,
    password: string,
  ): Promise<UserEntity | null> {
    // Buscar por email o username
    let user = await this.usersService.findByEmail(identity);
    if (!user) {
      user = await this.usersService.findByUsername(identity);
    }

    if (!user) {
      return null;
    }

    // Verificar si el usuario está bloqueado
    const isLocked = await this.usersService.isLocked(user.id);
    if (isLocked) {
      this.logger.security(
        `Login attempt blocked: Account ${identity} is locked`,
      );
      throw new UnauthorizedException(
        'Account is locked due to too many failed login attempts. Please try again later.',
      );
    }

    // Verificar si el usuario está activo
    if (!user.isActive) {
      this.logger.warn(`Login attempt for inactive account: ${identity}`);
      throw new UnauthorizedException('Account is disabled');
    }

    // Verificar contraseña
    const isValidPassword = await this.hashService.verify(
      user.passwordHash,
      password,
    );

    if (!isValidPassword) {
      // Incrementar contador de intentos fallidos
      await this.usersService.incrementFailedAttempts(user.id);
      this.logger.warn(
        `Failed login attempt for user: ${identity} (attempts increased)`,
      );
      return null;
    }

    // Resetear intentos fallidos después de login exitoso
    await this.usersService.resetFailedAttempts(user.id);
    this.logger.debug(`Password verified successfully for: ${identity}`);

    return user;
  }

  /**
   * Login: validar credenciales y generar tokens
   */
  async login(dto: LoginDto): Promise<AuthTokens> {
    const user = await this.validateUser(dto.identity, dto.password);

    if (!user) {
      throw new UnauthorizedException('Invalid credentials');
    }

    // Generar par de tokens
    const tokens = await this.tokenService.generateTokenPair(
      user.id,
      user.username,
      user.email,
    );

    // Nota: resetFailedAttempts ya fue llamado en validateUser()
    // y actualiza lastLoginAt, failedLoginAttempts y lockedUntil

    this.logger.auth(
      `Login successful: ${user.username} (${user.email}) - ID: ${user.id}`,
    );

    return tokens;
  }

  /**
   * Refresh: renovar access token usando refresh token
   * Implementa token rotation: invalida el refresh anterior
   */
  async refresh(dto: RefreshDto): Promise<AuthTokens> {
    try {
      // Verificar refresh token
      const payload = await this.tokenService.verifyToken(
        dto.refreshToken,
        'refresh',
      );

      // Verificar que no esté en la blacklist
      const isRevoked = await this.tokenService.isRevoked(payload.jti);
      if (isRevoked) {
        throw new UnauthorizedException('Token has been revoked');
      }

      // Revocar el refresh token anterior (rotation)
      await this.tokenService.revokeToken(
        payload.jti,
        payload.sub,
        'refresh',
        'Token rotation',
      );

      // Buscar usuario
      const user = await this.usersService.findOne(payload.sub);

      if (!user.isActive) {
        this.logger.warn(
          `Refresh attempt for inactive user: ${payload.username}`,
        );
        throw new UnauthorizedException('Account is disabled');
      }

      // Generar nuevo par de tokens
      const tokens = await this.tokenService.generateTokenPair(
        user.id,
        user.username,
        user.email,
      );

      this.logger.auth(
        `Tokens refreshed successfully: ${user.username} (ID: ${user.id})`,
      );

      return tokens;
    } catch (error) {
      this.logger.error(
        `Refresh token failed: ${(error as Error).message}`,
        '',
      );
      throw new UnauthorizedException('Invalid or expired refresh token');
    }
  }

  /**
   * Logout: revocar access y refresh tokens
   */
  async logout(
    userId: number,
    accessToken: string,
    refreshToken: string,
  ): Promise<void> {
    try {
      // Verificar y revocar access token
      const accessPayload = await this.tokenService.verifyToken(
        accessToken,
        'access',
      );
      await this.tokenService.revokeToken(
        accessPayload.jti,
        userId,
        'access',
        'User logout',
      );

      // Verificar y revocar refresh token
      const refreshPayload = await this.tokenService.verifyToken(
        refreshToken,
        'refresh',
      );
      await this.tokenService.revokeToken(
        refreshPayload.jti,
        userId,
        'refresh',
        'User logout',
      );

      this.logger.auth(`Logout successful: User ID ${userId}`);
    } catch (error) {
      this.logger.error(`Logout error: ${(error as Error).message}`, '');
      // No lanzar error en logout - mejor práctica
    }
  }

  /**
   * Cambiar contraseña del usuario autenticado
   */
  async changePassword(userId: number, dto: ChangePasswordDto): Promise<void> {
    const user = await this.usersService.findOne(userId);

    // Verificar contraseña actual
    const isValidPassword = await this.hashService.verify(
      user.passwordHash,
      dto.currentPassword,
    );

    if (!isValidPassword) {
      throw new UnauthorizedException('Current password is incorrect');
    }

    // Verificar que la nueva contraseña sea diferente
    if (dto.currentPassword === dto.newPassword) {
      throw new BadRequestException(
        'New password must be different from current password',
      );
    }

    // Actualizar contraseña
    await this.usersService.resetPassword(userId, {
      newPassword: dto.newPassword,
    });

    this.logger.log(`User ${userId} changed password`);
  }

  /**
   * Solicitar recuperación de contraseña
   * Genera token y lo guarda en el usuario (en producción, enviar por email)
   */
  async recoverRequest(dto: RecoverRequestDto): Promise<void> {
    const user = await this.usersService.findByEmail(dto.email);

    // No revelar si el email existe o no (seguridad)
    if (!user) {
      this.logger.warn(
        `Password recovery requested for non-existent email: ${dto.email}`,
      );
      return;
    }

    // Generar token de recuperación (32 bytes hex)
    const resetToken = randomBytes(32).toString('hex');
    const resetExpires = new Date(Date.now() + 60 * 60 * 1000); // 1 hora

    // Guardar token en el usuario
    user.passwordResetToken = resetToken;
    user.passwordResetExpires = resetExpires;

    await this.usersService['userRepo'].save(user);

    // TODO: Enviar email con el token
    // En desarrollo, loggearlo
    this.logger.log(`Password recovery token for ${user.email}: ${resetToken}`);
  }

  /**
   * Confirmar recuperación de contraseña con token
   */
  async recoverConfirm(dto: RecoverConfirmDto): Promise<void> {
    const user = await this.usersService['userRepo'].findOne({
      where: { passwordResetToken: dto.token },
    });

    if (!user) {
      throw new BadRequestException('Invalid recovery token');
    }

    // Verificar que el token no haya expirado
    if (!user.passwordResetExpires || user.passwordResetExpires < new Date()) {
      throw new BadRequestException('Recovery token has expired');
    }

    // Actualizar contraseña
    await this.usersService.resetPassword(user.id, {
      newPassword: dto.newPassword,
    });

    // Limpiar token de recuperación
    user.passwordResetToken = undefined;
    user.passwordResetExpires = undefined;
    await this.usersService['userRepo'].save(user);

    this.logger.log(`Password recovered for user ${user.username}`);
  }

  /**
   * Obtener información del usuario autenticado
   */
  async getMe(userId: number): Promise<UserEntity> {
    return await this.usersService.findOne(userId);
  }

  /**
   * Obtener permisos efectivos del usuario autenticado
   */
  async getPermissions(userId: number): Promise<string[]> {
    const actions = await this.authorizationService.getEffectiveActions(userId);
    return Array.from(actions);
  }
}
