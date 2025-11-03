import { IsString, MinLength } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

/**
 * DTO para resetear la contraseña de un usuario (admin)
 */
export class ResetPasswordDto {
  @ApiProperty({
    example: 'NewSecure123!',
    description: 'Nueva contraseña temporal',
    minLength: 8,
  })
  @IsString()
  @MinLength(8)
  newPassword: string;
}
