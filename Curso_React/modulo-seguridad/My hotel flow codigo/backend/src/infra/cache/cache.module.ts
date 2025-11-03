import { Module, Global } from '@nestjs/common';
import { CacheModule as NestCacheModule } from '@nestjs/cache-manager';
import { ConfigModule, ConfigService } from '@nestjs/config';
// import * as redisStore from 'cache-manager-redis-store';

/**
 * Módulo global de cache con Redis
 * Patrón: Module Pattern (NestJS)
 * Responsabilidad: Configurar cache con Redis para optimizar permisos
 * NOTA: Temporalmente usando cache en memoria para testing
 */
@Global()
@Module({
  imports: [
    NestCacheModule.registerAsync({
      imports: [ConfigModule],
      inject: [ConfigService],
      useFactory: async (configService: ConfigService) => {
        // const redisConfig = configService.get('redis');

        // Usar cache en memoria temporalmente
        return {
          // store: redisStore,
          // host: redisConfig?.host || 'localhost',
          // port: redisConfig?.port || 6379,
          ttl: 900, // 15 minutos en segundos
          max: 1000, // Máximo 1000 items en cache
        };
      },
    }),
  ],
  exports: [NestCacheModule],
})
export class CacheModule {}
