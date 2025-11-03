import { IsString, IsNotEmpty, MinLength } from 'class-validator';

/**
 * DTO para login
 * Patrón: Data Transfer Object
 * Usado en: POST /auth/login
 */
export class LoginDto {
  /**
   * Username o email del usuario
   * @example "admin@hotel.com"
   */
  @IsString()
  @IsNotEmpty()
  identity: string;

  /**
   * Contraseña del usuario
   * @example "Admin123!"
   */
  @IsString()
  @IsNotEmpty()
  @MinLength(8)
  password: string;
}
