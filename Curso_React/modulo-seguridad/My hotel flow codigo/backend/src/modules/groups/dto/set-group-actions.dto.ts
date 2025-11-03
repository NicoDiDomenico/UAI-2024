import { IsArray, IsString } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

/**
 * DTO para asignar acciones a un grupo
 * Patrón: Data Transfer Object
 */
export class SetGroupActionsDto {
  @ApiProperty({
    example: ['reservas.crear', 'reservas.modificar', 'checkin.registrar'],
    description: 'Array de keys de acciones a asignar al grupo',
    type: [String],
  })
  @IsArray()
  @IsString({ each: true })
  actionKeys: string[];
}
