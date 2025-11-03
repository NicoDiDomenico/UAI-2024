import {
  Controller,
  Post,
  Patch,
  Get,
  Body,
  UseGuards,
  Request,
  HttpCode,
  HttpStatus,
  Logger,
} from '@nestjs/common';
import { AuthGuard } from '@nestjs/passport';
import { AuthService } from './auth.service';
import {
  LoginDto,
  RefreshDto,
  ChangePasswordDto,
  RecoverRequestDto,
  RecoverConfirmDto,
} from './dto';

/**
 * Controlador de autenticación
 * Patrón: REST API Controller
 * Endpoints:
 * - POST /auth/login - login con credenciales
 * - POST /auth/refresh - renovar access token
 * - POST /auth/logout - cerrar sesión (revocar tokens)
 * - PATCH /auth/password - cambiar contraseña
 * - POST /auth/recover/request - solicitar recuperación de contraseña
 * - POST /auth/recover/confirm - confirmar recuperación
 * - GET /auth/me - obtener usuario autenticado
 */
@Controller('auth')
export class AuthController {
  private readonly logger = new Logger(AuthController.name);

  constructor(private readonly authService: AuthService) {}

  /**
   * Login con credenciales
   * POST /auth/login
   */
  @Post('login')
  @HttpCode(HttpStatus.OK)
  async login(@Body() dto: LoginDto) {
    this.logger.log(`Login attempt: ${dto.identity}`);
    return await this.authService.login(dto);
  }

  /**
   * Refresh de access token
   * POST /auth/refresh
   */
  @Post('refresh')
  @HttpCode(HttpStatus.OK)
  async refresh(@Body() dto: RefreshDto) {
    return await this.authService.refresh(dto);
  }

  /**
   * Logout (revocar tokens)
   * POST /auth/logout
   * Requiere autenticación JWT
   */
  @Post('logout')
  @HttpCode(HttpStatus.NO_CONTENT)
  @UseGuards(AuthGuard('jwt'))
  async logout(@Request() req: any, @Body() body: { refreshToken: string }) {
    const userId = req.user.id;
    const accessToken = req.headers.authorization?.replace('Bearer ', '') || '';

    this.logger.log(`Logout user: ${userId}`);
    await this.authService.logout(userId, accessToken, body.refreshToken);
  }

  /**
   * Cambiar contraseña del usuario autenticado
   * PATCH /auth/password
   * Requiere autenticación JWT
   */
  @Patch('password')
  @HttpCode(HttpStatus.NO_CONTENT)
  @UseGuards(AuthGuard('jwt'))
  async changePassword(@Request() req: any, @Body() dto: ChangePasswordDto) {
    const userId = req.user.id;
    this.logger.log(`Password change for user: ${userId}`);
    await this.authService.changePassword(userId, dto);
  }

  /**
   * Solicitar recuperación de contraseña
   * POST /auth/recover/request
   */
  @Post('recover/request')
  @HttpCode(HttpStatus.NO_CONTENT)
  async recoverRequest(@Body() dto: RecoverRequestDto) {
    this.logger.log(`Password recovery requested for: ${dto.email}`);
    await this.authService.recoverRequest(dto);
  }

  /**
   * Confirmar recuperación de contraseña
   * POST /auth/recover/confirm
   */
  @Post('recover/confirm')
  @HttpCode(HttpStatus.NO_CONTENT)
  async recoverConfirm(@Body() dto: RecoverConfirmDto) {
    this.logger.log('Password recovery confirmation');
    await this.authService.recoverConfirm(dto);
  }

  /**
   * Obtener información del usuario autenticado
   * GET /auth/me
   * Requiere autenticación JWT
   */
  @Get('me')
  @UseGuards(AuthGuard('jwt'))
  async getMe(@Request() req: any) {
    const userId = req.user.id;
    return await this.authService.getMe(userId);
  }

  /**
   * Obtener permisos efectivos del usuario autenticado
   * GET /auth/permissions
   * Requiere autenticación JWT
   */
  @Get('permissions')
  @UseGuards(AuthGuard('jwt'))
  async getPermissions(@Request() req: any) {
    const userId = req.user.id;
    return await this.authService.getPermissions(userId);
  }
}
