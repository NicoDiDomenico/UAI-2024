import { PartialType } from '@nestjs/swagger';
import { CreateUserDto } from './create-user.dto';
import { IsOptional, IsString } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

/**
 * DTO para actualizar un usuario existente
 */
export class UpdateUserDto extends PartialType(CreateUserDto) {
  @ApiProperty({
    description: 'Nueva contraseña (solo si se quiere cambiar)',
    required: false,
  })
  @IsString()
  @IsOptional()
  password?: string;
}
