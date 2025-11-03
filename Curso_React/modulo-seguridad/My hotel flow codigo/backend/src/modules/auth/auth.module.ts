import { Module } from '@nestjs/common';
import { PassportModule } from '@nestjs/passport';
import { JwtModule } from '@nestjs/jwt';
import { ConfigModule, ConfigService } from '@nestjs/config';
import { TypeOrmModule } from '@nestjs/typeorm';
import { RevokedTokenEntity, UserEntity, GroupEntity, ActionEntity } from '@infra/database/entities';
import { AuthService } from './auth.service';
import { AuthController } from './auth.controller';
import { JwtStrategy, LocalStrategy } from './strategies';
import { UsersModule } from '@modules/users/users.module';
import { HashService, TokenService, AuthorizationService } from '@common/services';

/**
 * Módulo de autenticación
 * Patrón: Module Pattern (NestJS)
 * Responsabilidad: Login, logout, refresh, password management
 */
@Module({
  imports: [
    PassportModule,
    JwtModule.registerAsync({
      imports: [ConfigModule],
      inject: [ConfigService],

      useFactory: (configService: ConfigService): any => ({
        secret: configService.get<string>('jwt.secret') || 'default-secret',
        signOptions: {
          expiresIn: configService.get<string>('jwt.accessExpiresIn') || '15m',
        },
      }),
    }),
    TypeOrmModule.forFeature([RevokedTokenEntity, UserEntity, GroupEntity, ActionEntity]),
    UsersModule,
  ],
  controllers: [AuthController],
  providers: [
    AuthService,
    JwtStrategy,
    LocalStrategy,
    HashService,
    TokenService,
    AuthorizationService,
  ],
  exports: [AuthService],
})
export class AuthModule {}
