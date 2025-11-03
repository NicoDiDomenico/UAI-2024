import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { UserEntity } from '@infra/database/entities';
import { UsersService } from './users.service';
import { UsersController } from './users.controller';
import { HashService } from '@common/services';
import { ActionsModule } from '@modules/actions/actions.module';
import { GroupsModule } from '@modules/groups/groups.module';

/**
 * Módulo de usuarios
 * Patrón: Module Pattern (NestJS)
 * Responsabilidad: Gestión de usuarios, grupos y acciones
 */
@Module({
  imports: [
    TypeOrmModule.forFeature([UserEntity]),
    ActionsModule,
    GroupsModule,
  ],
  controllers: [UsersController],
  providers: [UsersService, HashService],
  exports: [UsersService], // Exportar para AuthModule
})
export class UsersModule {}
