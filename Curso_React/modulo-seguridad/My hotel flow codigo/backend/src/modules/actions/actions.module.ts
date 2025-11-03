import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { ActionEntity } from '@infra/database/entities';
import { ActionsService } from './actions.service';
import { ActionsController } from './actions.controller';

/**
 * Módulo de Acciones
 * Gestiona el catálogo de permisos atómicos del sistema
 * Patrón: Module (NestJS)
 */
@Module({
  imports: [TypeOrmModule.forFeature([ActionEntity])],
  controllers: [ActionsController],
  providers: [ActionsService],
  exports: [ActionsService],
})
export class ActionsModule {}
