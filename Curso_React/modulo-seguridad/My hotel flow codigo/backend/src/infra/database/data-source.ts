import { DataSource } from 'typeorm';
import { config } from 'dotenv';

// Cargar variables de entorno
config();

/**
 * DataSource para migraciones de TypeORM
 * Este archivo se usa exclusivamente para ejecutar comandos de migración
 * desde la CLI de TypeORM
 */
export const AppDataSource = new DataSource({
  type: 'postgres',
  host: process.env.DB_HOST || 'localhost',
  port: parseInt(process.env.DB_PORT || '5432', 10),
  username: process.env.DB_USERNAME || 'postgres',
  password: process.env.DB_PASSWORD || 'postgres',
  database: process.env.DB_NAME || 'myhotelflow',
  entities: ['src/infra/database/entities/**/*.entity.ts'],
  migrations: ['src/infra/database/migrations/**/*.ts'],
  synchronize: false, // NUNCA usar true en producción
  logging: process.env.NODE_ENV === 'development',
});
