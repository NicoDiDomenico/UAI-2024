# CHECKLIST DE IMPLEMENTACIÓN — Módulo de Seguridad (PARTE 1)

Checklist detallado para implementar el módulo de seguridad desde el inicio.

**Proyecto:** MyHotelFlow - Sistema de Reservas Hoteleras  
**Framework:** NestJS + TypeORM + PostgreSQL  
**Patrón principal:** Clean Architecture + Composite Pattern para permisos

---

## FASE 1: Configuración Inicial del Proyecto

### 1.1 Setup del Backend

**Tareas:**
- [x] Crear proyecto NestJS (`nest new backend`)
- [x] Instalar dependencias principales
- [x] Configurar estructura de carpetas
- [x] Configurar ESLint y Prettier
- [x] Crear archivo .gitignore

**Dependencias instaladas:**
```bash
npm install --save @nestjs/typeorm typeorm pg
npm install --save @nestjs/config
npm install --save @nestjs/jwt @nestjs/passport passport passport-jwt passport-local
npm install --save argon2
npm install --save class-validator class-transformer
npm install --save @nestjs/swagger
npm install --save @nestjs/cache-manager cache-manager cache-manager-redis-store redis
npm install --save @nestjs/throttler
npm install --save uuid date-fns
npm install --save-dev @types/passport-jwt @types/passport-local
```

---

### 1.2 Configuración de TypeScript

**Archivo:** `tsconfig.json`

**Tareas:**
- [x] Configurar path aliases para imports limpios
- [x] Habilitar `strict` mode
- [x] Configurar decorators y metadata

**Path aliases configurados:**
```json
"paths": {
  "@infra/*": ["src/infra/*"],
  "@modules/*": ["src/modules/*"],
  "@common/*": ["src/common/*"],
  "@config/*": ["src/config/*"]
}
```

---

## FASE 2: Infraestructura Base

### 2.1 Variables de Entorno

**Archivo:** `.env` y `.env.example`

**Tareas:**
- [x] Crear archivo .env.example con todas las variables
- [x] Crear archivo .env para desarrollo local
- [x] Documentar cada variable

**Variables configuradas:**
```env
# Base de Datos
DB_HOST=localhost
DB_PORT=5432
DB_USERNAME=postgres
DB_PASSWORD=postgres
DB_DATABASE=myhotelflow
DB_SYNCHRONIZE=true
DB_LOGGING=false

# JWT
JWT_SECRET=your-secret-key
JWT_ACCESS_EXPIRATION=15m
JWT_REFRESH_EXPIRATION=7d

# Argon2
ARGON2_MEMORY_COST=65536
ARGON2_TIME_COST=3
ARGON2_PARALLELISM=4

# Redis
REDIS_HOST=localhost
REDIS_PORT=6379
REDIS_DB=0

# Seguridad
LOCKOUT_THRESHOLD=5
LOCKOUT_DURATION=900000
PASSWORD_RESET_EXPIRATION=3600000
PERMISSIONS_CACHE_TTL=900

# CORS
CORS_ORIGIN=http://localhost:5173

# App
PORT=3000
NODE_ENV=development
API_PREFIX=api
```

---

### 2.2 Módulo de Configuración

**Archivo:** `src/config/configuration.ts`

**Tareas:**
- [x] Crear función de configuración centralizada
- [x] Mapear variables de entorno
- [x] Definir valores por defecto
- [x] Tipar configuración correctamente

**Secciones de configuración:**
- [x] Database
- [x] JWT
- [x] Argon2
- [x] Redis
- [x] Security
- [x] CORS
- [x] App

---

### 2.3 Docker Compose

**Archivo:** `docker-compose.yml`

**Tareas:**
- [x] Configurar servicio de PostgreSQL
- [x] Configurar servicio de Redis
- [x] Configurar servicio de API (NestJS)
- [x] Definir volúmenes persistentes
- [x] Configurar red entre servicios

**Servicios configurados:**
- [x] `api` - Backend NestJS
- [x] `db` - PostgreSQL 15
- [x] `redis` - Redis 7

---

## FASE 3: Entidades de Base de Datos

### 3.1 Entidad Action

**Archivo:** `src/infra/database/entities/action.entity.ts`

**Tareas:**
- [x] Crear entidad ActionEntity
- [x] Definir columnas: id, key, name, description, area
- [x] Agregar índice único en `key`
- [x] Agregar timestamps (createdAt, updatedAt)
- [x] Documentar con JSDoc

**Campos:**
- [x] `id` - PrimaryGeneratedColumn
- [x] `key` - Clave única (ej: 'reservas.crear')
- [x] `name` - Nombre legible
- [x] `description` - Descripción detallada
- [x] `area` - Área funcional (extraído de key)
- [x] `createdAt` - Fecha de creación
- [x] `updatedAt` - Fecha de actualización

---

### 3.2 Entidad Group

**Archivo:** `src/infra/database/entities/group.entity.ts`

**Tareas:**
- [x] Crear entidad GroupEntity
- [x] Definir relación ManyToMany con ActionEntity
- [x] Definir relación ManyToMany consigo misma (children)
- [x] Configurar eager loading
- [x] Documentar patrón Composite

**Campos:**
- [x] `id` - PrimaryGeneratedColumn
- [x] `key` - Clave única (ej: 'rol.admin')
- [x] `name` - Nombre legible
- [x] `description` - Descripción
- [x] `actions` - ManyToMany con ActionEntity
- [x] `children` - ManyToMany con GroupEntity (composición)
- [x] `createdAt` - Fecha de creación
- [x] `updatedAt` - Fecha de actualización

**Tablas de relación:**
- [x] `group_actions` - Relación Group-Action
- [x] `group_children` - Relación Group-Group (jerarquía)

---

### 3.3 Entidad User

**Archivo:** `src/infra/database/entities/user.entity.ts`

**Tareas:**
- [x] Crear entidad UserEntity
- [x] Definir relación ManyToMany con GroupEntity
- [x] Definir relación ManyToMany con ActionEntity (excepciones)
- [x] Agregar campos de seguridad (lockout, reset token)
- [x] Agregar índices únicos en username y email

**Campos:**
- [x] `id` - PrimaryGeneratedColumn
- [x] `username` - Nombre de usuario único
- [x] `email` - Email único
- [x] `passwordHash` - Hash Argon2id (nunca exponer)
- [x] `fullName` - Nombre completo
- [x] `isActive` - Usuario activo/inactivo
- [x] `lastLoginAt` - Último login exitoso
- [x] `failedLoginAttempts` - Contador de intentos fallidos
- [x] `lockedUntil` - Fecha de bloqueo por intentos
- [x] `groups` - ManyToMany con GroupEntity
- [x] `actions` - ManyToMany con ActionEntity (excepciones)
- [x] `passwordResetToken` - Token de recuperación
- [x] `passwordResetExpires` - Expiración del token
- [x] `createdAt` - Fecha de creación
- [x] `updatedAt` - Fecha de actualización

**Tablas de relación:**
- [x] `user_groups` - Relación User-Group
- [x] `user_actions` - Relación User-Action (excepciones)

---

### 3.4 Entidad RevokedToken

**Archivo:** `src/infra/database/entities/revoked-token.entity.ts`

**Tareas:**
- [x] Crear entidad RevokedTokenEntity
- [x] Implementar blacklist de JWT
- [x] Agregar índices para consultas rápidas
- [x] Incluir metadata de revocación

**Campos:**
- [x] `id` - PrimaryGeneratedColumn
- [x] `jti` - JWT ID único
- [x] `userId` - Usuario propietario del token
- [x] `tokenType` - 'access' o 'refresh'
- [x] `reason` - Razón de revocación
- [x] `expiresAt` - Expiración original del token
- [x] `ip` - IP de revocación
- [x] `createdAt` - Fecha de revocación

**Índices:**
- [x] Índice compuesto en (jti, expiresAt)

---

### 3.5 Entidad AuditLog

**Archivo:** `src/infra/database/entities/audit-log.entity.ts`

**Tareas:**
- [x] Crear entidad AuditLogEntity
- [x] Definir campos para trazabilidad completa
- [x] Agregar soporte para metadata JSON
- [x] Crear índices para búsquedas eficientes

**Campos:**
- [x] `id` - PrimaryGeneratedColumn
- [x] `userId` - Usuario que realizó la acción (nullable)
- [x] `userIdentity` - Email o username
- [x] `action` - Acción realizada (namespaced)
- [x] `entityType` - Tipo de entidad afectada
- [x] `entityId` - ID de la entidad
- [x] `status` - 'success' | 'failure' | 'partial'
- [x] `message` - Mensaje descriptivo
- [x] `metadata` - JSON con datos adicionales
- [x] `ip` - IP del cliente
- [x] `userAgent` - User-Agent del cliente
- [x] `severity` - Nivel de severidad
- [x] `createdAt` - Timestamp

**Índices:**
- [x] Índice en (userId, createdAt)
- [x] Índice en (action, createdAt)

---

### 3.6 Barrel Export de Entidades

**Archivo:** `src/infra/database/entities/index.ts`

**Tareas:**
- [x] Crear archivo index con exports
- [x] Facilitar imports desde otros módulos

```typescript
export * from './action.entity';
export * from './group.entity';
export * from './user.entity';
export * from './revoked-token.entity';
export * from './audit-log.entity';
```

---

## FASE 4: Módulo de Base de Datos

### 4.1 Database Module

**Archivo:** `src/infra/database/database.module.ts`

**Tareas:**
- [x] Crear DatabaseModule
- [x] Configurar TypeOrmModule.forRootAsync()
- [x] Inyectar ConfigService
- [x] Registrar todas las entidades
- [x] Configurar opciones de conexión

**Configuración:**
- [x] Conexión PostgreSQL
- [x] Carga automática de entidades
- [x] Synchronize (solo desarrollo)
- [x] Logging configurable

---

### 4.2 Integración con App Module

**Archivo:** `src/app.module.ts`

**Tareas:**
- [x] Importar ConfigModule como global
- [x] Importar DatabaseModule
- [x] Cargar configuración desde configuration.ts

---

## FASE 5: Módulo Actions (Core)

### 5.1 DTOs del módulo Actions

#### 5.1.1 CreateActionDto

**Archivo:** `src/modules/actions/dto/create-action.dto.ts`

**Tareas:**
- [x] Crear CreateActionDto
- [x] Agregar validaciones con class-validator
- [x] Documentar con @ApiProperty (Swagger)
- [x] Validar key único
- [x] Validar longitud máxima de campos

**Campos validados:**
- [x] `key` - @IsString, @IsNotEmpty, @MaxLength(100)
- [x] `name` - @IsString, @IsNotEmpty, @MaxLength(255)
- [x] `description` - @IsString, @IsOptional
- [x] `area` - @IsString, @IsOptional, @MaxLength(50)

---

#### 5.1.2 UpdateActionDto

**Archivo:** `src/modules/actions/dto/update-action.dto.ts`

**Tareas:**
- [x] Crear UpdateActionDto
- [x] Extender de PartialType(CreateActionDto)
- [x] Todos los campos opcionales automáticamente

---

#### 5.1.3 Barrel Export

**Archivo:** `src/modules/actions/dto/index.ts`

**Tareas:**
- [x] Crear archivo index
- [x] Exportar todos los DTOs

---

### 5.2 Service de Actions

**Archivo:** `src/modules/actions/actions.service.ts`

**Tareas:**
- [x] Crear ActionsService con @Injectable()
- [x] Inyectar Repository<ActionEntity>
- [x] Implementar método create()
- [x] Implementar método findAll()
- [x] Implementar método findOne()
- [x] Implementar método findByKey()
- [x] Implementar método findByKeys() - para validación
- [x] Implementar método findByArea()
- [x] Implementar método update()
- [x] Implementar método remove()
- [x] Implementar método seed() - para datos iniciales
- [x] Agregar Logger de NestJS
- [x] Validación de duplicados
- [x] Extracción automática de área desde key

**Funcionalidades implementadas:**
- [x] CRUD completo
- [x] Búsqueda por key único
- [x] Búsqueda múltiple por array de keys
- [x] Filtrado por área funcional
- [x] Seed de acciones iniciales (40+ acciones)
- [x] Logging de operaciones
- [x] Manejo de errores con excepciones HTTP

**Acciones seeded:**
- [x] Reservas (listar, ver, crear, modificar, cancelar)
- [x] Check-in (registrar, asignar habitación)
- [x] Check-out (calcular cargos, registrar pago, cerrar)
- [x] Comprobantes (emitir, anular, imprimir, ver)
- [x] Habitaciones (listar, ver, crear, modificar, cambiar estado)
- [x] Clientes (listar, ver, crear, modificar)
- [x] Config.Usuarios (listar, crear, modificar, eliminar, resetear clave, asignar grupos/acciones)
- [x] Config.Grupos (listar, crear, modificar, eliminar, asignar acciones, asignar hijos)
- [x] Config.Acciones (listar, crear, modificar, eliminar)

---

### 5.3 Controller de Actions

**Archivo:** `src/modules/actions/actions.controller.ts`

**Tareas:**
- [x] Crear ActionsController con @Controller('actions')
- [x] Implementar endpoint POST / - crear acción
- [x] Implementar endpoint GET / - listar todas
- [x] Implementar endpoint GET /area/:area - filtrar por área
- [x] Implementar endpoint GET /:id - obtener por ID
- [x] Implementar endpoint PATCH /:id - actualizar
- [x] Implementar endpoint DELETE /:id - eliminar
- [x] Implementar endpoint POST /seed - poblar BD
- [x] Agregar decoradores de Swagger (@ApiTags, @ApiOperation, @ApiResponse)
- [x] Agregar @ApiBearerAuth para seguridad
- [x] Usar ParseIntPipe para validación de IDs
- [x] Configurar códigos HTTP correctos

**Endpoints implementados:**
```
POST   /actions        - Crear acción
GET    /actions        - Listar todas
GET    /actions/area/:area - Filtrar por área
GET    /actions/:id    - Obtener por ID
PATCH  /actions/:id    - Actualizar
DELETE /actions/:id    - Eliminar (204)
POST   /actions/seed   - Seed inicial
```

---

### 5.4 Module de Actions

**Archivo:** `src/modules/actions/actions.module.ts`

**Tareas:**
- [x] Crear ActionsModule con @Module()
- [x] Importar TypeOrmModule.forFeature([ActionEntity])
- [x] Registrar ActionsController
- [x] Registrar ActionsService
- [x] Exportar ActionsService (para uso en otros módulos)

---

### 5.5 Integración con App Module

**Tareas:**
- [x] Importar ActionsModule en app.module.ts
- [x] Verificar que compile sin errores

---

## RESUMEN DE PROGRESO - PARTE 1

### ✅ Completado

1. **Infraestructura Base**
   - ✅ Proyecto NestJS inicializado
   - ✅ Dependencias instaladas
   - ✅ Variables de entorno configuradas
   - ✅ Docker Compose creado
   - ✅ Path aliases en TypeScript

2. **Entidades de Base de Datos**
   - ✅ ActionEntity (5 campos + timestamps)
   - ✅ GroupEntity (4 campos + relaciones + timestamps)
   - ✅ UserEntity (13 campos + relaciones + timestamps)
   - ✅ RevokedTokenEntity (7 campos + timestamps)
   - ✅ AuditLogEntity (11 campos + timestamps)

3. **Módulo de Base de Datos**
   - ✅ DatabaseModule configurado
   - ✅ TypeORM integrado con PostgreSQL
   - ✅ Configuración centralizada

4. **Módulo Actions (Completo)**
   - ✅ DTOs con validación
   - ✅ ActionsService con CRUD completo
   - ✅ ActionsController con 7 endpoints
   - ✅ Seed de 40+ acciones iniciales
   - ✅ Documentación Swagger

### 📊 Estadísticas

- **Archivos creados:** 15+
- **Líneas de código:** ~1500+
- **Entidades:** 5
- **Módulos:** 2 (Database, Actions)
- **Endpoints API:** 7
- **Acciones seeded:** 40+

### ➡️ Siguiente: PARTE 2

Continuar con:
- FASE 5.2: Módulo Groups (DTOs, Service, Controller)
- Implementación de patrón Composite
- Validación anti-ciclos en jerarquía

---

**Versión:** 1.0  
**Fecha:** Octubre 2025  
**Mantenedor:** Equipo MyHotelFlow
