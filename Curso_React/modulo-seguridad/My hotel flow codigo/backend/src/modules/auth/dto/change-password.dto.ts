import { IsString, IsNotEmpty, MinLength } from 'class-validator';

/**
 * DTO para cambio de contraseña
 * Patrón: Data Transfer Object
 * Usado en: PATCH /auth/password
 */
export class ChangePasswordDto {
  /**
   * Contraseña actual del usuario
   * @example "OldPass123!"
   */
  @IsString()
  @IsNotEmpty()
  currentPassword: string;

  /**
   * Nueva contraseña
   * @example "NewPass123!"
   */
  @IsString()
  @IsNotEmpty()
  @MinLength(8)
  newPassword: string;
}
