import { IsEmail, IsNotEmpty } from 'class-validator';

/**
 * DTO para solicitud de recuperación de contraseña
 * Patrón: Data Transfer Object
 * Usado en: POST /auth/recover/request
 */
export class RecoverRequestDto {
  /**
   * Email del usuario
   * @example "admin@hotel.com"
   */
  @IsEmail()
  @IsNotEmpty()
  email: string;
}
