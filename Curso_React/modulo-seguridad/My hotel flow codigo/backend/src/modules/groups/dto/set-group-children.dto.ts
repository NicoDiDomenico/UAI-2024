import { IsArray, IsString } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

/**
 * DTO para asignar grupos hijos (composición jerárquica)
 * Patrón: Composite
 */
export class SetGroupChildrenDto {
  @ApiProperty({
    example: ['group.frontdesk', 'group.housekeeping'],
    description: 'Array de keys de grupos hijos para composición jerárquica',
    type: [String],
  })
  @IsArray()
  @IsString({ each: true })
  childGroupKeys: string[];
}
