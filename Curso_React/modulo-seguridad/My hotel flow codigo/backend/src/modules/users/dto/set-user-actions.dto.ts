import { IsArray, IsString } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

/**
 * DTO para asignar acciones individuales a un usuario (excepciones)
 */
export class SetUserActionsDto {
  @ApiProperty({
    example: ['reservas.modificar', 'comprobantes.imprimir'],
    description: 'Array de keys de acciones a asignar directamente al usuario',
    type: [String],
  })
  @IsArray()
  @IsString({ each: true })
  actionKeys: string[];
}
