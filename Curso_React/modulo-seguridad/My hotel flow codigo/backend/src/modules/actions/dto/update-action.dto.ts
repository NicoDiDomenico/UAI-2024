import { PartialType } from '@nestjs/swagger';
import { CreateActionDto } from './create-action.dto';

/**
 * DTO para actualizar una acción existente
 * Hereda de CreateActionDto con todos los campos opcionales
 */
export class UpdateActionDto extends PartialType(CreateActionDto) {}
