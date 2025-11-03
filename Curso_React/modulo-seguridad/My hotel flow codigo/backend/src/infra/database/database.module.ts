import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { ConfigModule, ConfigService } from '@nestjs/config';
import {
  ActionEntity,
  GroupEntity,
  UserEntity,
  RevokedTokenEntity,
  AuditLogEntity,
} from './entities';

/**
 * Módulo de base de datos
 * Configura TypeORM con PostgreSQL y carga todas las entidades
 * Patrón: Module (NestJS)
 */
@Module({
  imports: [
    TypeOrmModule.forRootAsync({
      imports: [ConfigModule],
      inject: [ConfigService],
      useFactory: (config: ConfigService) => ({
        type: 'postgres',
        host: config.get<string>('database.host'),
        port: config.get<number>('database.port'),
        username: config.get<string>('database.username'),
        password: config.get<string>('database.password'),
        database: config.get<string>('database.database'),
        entities: [
          ActionEntity,
          GroupEntity,
          UserEntity,
          RevokedTokenEntity,
          AuditLogEntity,
        ],
        synchronize: config.get<boolean>('database.synchronize'),
        logging: config.get<boolean>('database.logging'),
        autoLoadEntities: true,
      }),
    }),
  ],
})
export class DatabaseModule {}
