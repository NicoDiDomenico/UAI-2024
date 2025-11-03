import { IsString, IsNotEmpty, MinLength } from 'class-validator';

/**
 * DTO para confirmación de recuperación de contraseña
 * Patrón: Data Transfer Object
 * Usado en: POST /auth/recover/confirm
 */
export class RecoverConfirmDto {
  /**
   * Token de recuperación recibido por email
   * @example "abc123def456"
   */
  @IsString()
  @IsNotEmpty()
  token: string;

  /**
   * Nueva contraseña
   * @example "NewPass123!"
   */
  @IsString()
  @IsNotEmpty()
  @MinLength(8)
  newPassword: string;
}
