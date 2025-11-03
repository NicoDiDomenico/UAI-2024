import { IsString, IsNotEmpty, IsOptional, MaxLength } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

/**
 * DTO para crear una nueva acción
 * Patrón: Data Transfer Object
 */
export class CreateActionDto {
  @ApiProperty({
    example: 'reservas.crear',
    description: 'Clave única de la acción (namespaced)',
  })
  @IsString()
  @IsNotEmpty()
  @MaxLength(100)
  key: string;

  @ApiProperty({
    example: 'Crear Reserva',
    description: 'Nombre legible de la acción',
  })
  @IsString()
  @IsNotEmpty()
  @MaxLength(255)
  name: string;

  @ApiProperty({
    example: 'Permite crear nuevas reservas en el sistema',
    description: 'Descripción detallada de la acción',
    required: false,
  })
  @IsString()
  @IsOptional()
  description?: string;

  @ApiProperty({
    example: 'reservas',
    description: 'Área funcional de la acción',
    required: false,
  })
  @IsString()
  @IsOptional()
  @MaxLength(50)
  area?: string;
}
