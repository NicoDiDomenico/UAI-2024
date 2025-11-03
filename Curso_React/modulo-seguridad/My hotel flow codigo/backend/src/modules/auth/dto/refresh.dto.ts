import { IsString, IsNotEmpty } from 'class-validator';

/**
 * DTO para refresh de tokens
 * Patrón: Data Transfer Object
 * Usado en: POST /auth/refresh
 */
export class RefreshDto {
  /**
   * Refresh token JWT
   * @example "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
   */
  @IsString()
  @IsNotEmpty()
  refreshToken: string;
}
