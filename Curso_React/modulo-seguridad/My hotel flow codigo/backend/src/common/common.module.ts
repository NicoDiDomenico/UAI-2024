import { Module, Global } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import {
  UserEntity,
  GroupEntity,
  ActionEntity,
} from '@infra/database/entities';
import { AuthorizationService } from './services/authorization.service';
import { ActionsGuard } from './guards/actions.guard';

/**
 * Módulo común global
 * Proporciona servicios y guards comunes a toda la aplicación
 */
@Global()
@Module({
  imports: [
    TypeOrmModule.forFeature([UserEntity, GroupEntity, ActionEntity]),
  ],
  providers: [AuthorizationService, ActionsGuard],
  exports: [AuthorizationService, ActionsGuard],
})
export class CommonModule {}
