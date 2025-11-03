import { PartialType } from '@nestjs/swagger';
import { CreateGroupDto } from './create-group.dto';

/**
 * DTO para actualizar un grupo existente
 * Hereda de CreateGroupDto con todos los campos opcionales
 */
export class UpdateGroupDto extends PartialType(CreateGroupDto) {}
