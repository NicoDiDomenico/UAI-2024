import { IsArray, IsString } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

/**
 * DTO para asignar grupos a un usuario
 */
export class SetUserGroupsDto {
  @ApiProperty({
    example: ['rol.recepcionista', 'group.frontdesk'],
    description: 'Array de keys de grupos a asignar al usuario',
    type: [String],
  })
  @IsArray()
  @IsString({ each: true })
  groupKeys: string[];
}
