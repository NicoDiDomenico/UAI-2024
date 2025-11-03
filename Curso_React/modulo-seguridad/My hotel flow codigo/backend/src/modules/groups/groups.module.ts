import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { GroupEntity } from '@infra/database/entities';
import { ActionsModule } from '@modules/actions/actions.module';
import { GroupsService } from './groups.service';
import { GroupsController } from './groups.controller';

/**
 * Módulo de Grupos
 * Gestiona grupos de permisos con composición jerárquica
 * Patrón: Module (NestJS) + Composite Pattern
 */
@Module({
  imports: [TypeOrmModule.forFeature([GroupEntity]), ActionsModule],
  controllers: [GroupsController],
  providers: [GroupsService],
  exports: [GroupsService],
})
export class GroupsModule {}
