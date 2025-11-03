import { NestFactory } from '@nestjs/core';
import { ValidationPipe } from '@nestjs/common';
import { DocumentBuilder, SwaggerModule } from '@nestjs/swagger';
import helmet from 'helmet';
import { AppModule } from './app.module';
import { TransformInterceptor } from '@common/interceptors/transform.interceptor';
import { HttpExceptionFilter } from '@common/filters/http-exception.filter';

async function bootstrap() {
  const app = await NestFactory.create(AppModule);

  // Configurar prefijo global para todas las rutas
  app.setGlobalPrefix('api/v1', {
    exclude: ['api/docs'], // Mantener Swagger en /api/docs
  });

  // Security headers con Helmet
  app.use(
    helmet({
      contentSecurityPolicy: {
        directives: {
          defaultSrc: ["'self'"],
          styleSrc: ["'self'", "'unsafe-inline'"],
          scriptSrc: ["'self'"],
          imgSrc: ["'self'", 'data:', 'https:'],
        },
      },
      crossOriginEmbedderPolicy: false, // Necesario para Swagger
    }),
  );

  // Habilitar CORS para el frontend
  app.enableCors({
    origin: process.env.FRONTEND_URL || 'http://localhost:5173',
    credentials: true,
  });

  // Global validation pipe
  app.useGlobalPipes(
    new ValidationPipe({
      whitelist: true, // Eliminar propiedades no definidas en DTOs
      forbidNonWhitelisted: true, // Lanzar error si hay propiedades extras
      transform: true, // Transformar tipos automáticamente
    }),
  );

  // Global interceptor para estandarizar respuestas exitosas
  app.useGlobalInterceptors(new TransformInterceptor());

  // Global filter para estandarizar respuestas de error
  app.useGlobalFilters(new HttpExceptionFilter());

  // Configuración de Swagger
  const config = new DocumentBuilder()
    .setTitle('MyHotelFlow API')
    .setDescription(
      'API del sistema de gestión hotelera MyHotelFlow. Incluye módulos de autenticación, autorización basada en permisos (Composite Pattern), gestión de usuarios, grupos y acciones.',
    )
    .setVersion('v1')
    .addBearerAuth(
      {
        type: 'http',
        scheme: 'bearer',
        bearerFormat: 'JWT',
        name: 'Authorization',
        description: 'Ingrese su JWT access token',
        in: 'header',
      },
      'access-token',
    )
    .addTag(
      'auth',
      'Endpoints de autenticación (login, logout, refresh, password)',
    )
    .addTag('users', 'Gestión de usuarios')
    .addTag('groups', 'Gestión de grupos de permisos')
    .addTag('actions', 'Gestión de acciones/permisos')
    .addTag('Health', 'Endpoints de salud de la aplicación')
    .addTag('Metrics', 'Endpoints de métricas y estadísticas de uso')
    .build();

  const document = SwaggerModule.createDocument(app, config);
  SwaggerModule.setup('api/docs', app, document);

  const port = process.env.PORT || 3000;
  await app.listen(port);

  console.log(`🚀 Application is running on: http://localhost:${port}`);
  console.log(`📡 API prefix: /api/v1`);
  console.log(`📚 Swagger documentation: http://localhost:${port}/api/docs`);
}

bootstrap();
