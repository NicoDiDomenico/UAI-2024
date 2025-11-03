import { IsString, IsNotEmpty, IsOptional, MaxLength } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

/**
 * DTO para crear un nuevo grupo
 * Patrón: Data Transfer Object
 */
export class CreateGroupDto {
  @ApiProperty({
    example: 'rol.recepcionista',
    description: 'Clave única del grupo',
  })
  @IsString()
  @IsNotEmpty()
  @MaxLength(100)
  key: string;

  @ApiProperty({
    example: 'Recepcionista',
    description: 'Nombre legible del grupo',
  })
  @IsString()
  @IsNotEmpty()
  @MaxLength(255)
  name: string;

  @ApiProperty({
    example: 'Personal de recepción y atención al cliente',
    description: 'Descripción del grupo y su propósito',
    required: false,
  })
  @IsString()
  @IsOptional()
  description?: string;
}
