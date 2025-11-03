# CHECKLIST DE IMPLEMENTACIÓN — Módulo de Seguridad (PARTE 3 - FINAL)

**Última parte:** Auth Service, Strategies, Guards, Decorators, Tests, Redis, Swagger

---

## FASE 6: Autenticación (Continuación)

### 6.5 Auth Service

**Archivo:** `src/modules/auth/auth.service.ts`

```typescript
@Injectable()
export class AuthService {
  constructor(
    private usersService: UsersService,
    private hashService: HashService,
    private tokenService: TokenService,
  ) {}

  async validateUser(email: string, password: string): Promise<UserEntity | null> {
    const user = await this.usersService.findByEmail(email);
    if (!user || !user.isActive) return null;
    
    // Verificar lockout
    if (await this.usersService.isLocked(user.id)) {
      throw new UnauthorizedException('Account locked');
    }

    const valid = await this.hashService.verify(user.passwordHash, password);
    if (!valid) {
      await this.usersService.incrementFailedAttempts(user.id);
      return null;
    }

    await this.usersService.resetFailedAttempts(user.id);
    return user;
  }

  async login(user: UserEntity): Promise<TokenPair> {
    return await this.tokenService.generateTokenPair(user.id, user.email);
  }

  async refresh(refreshToken: string): Promise<TokenPair> {
    const payload = await this.tokenService.verifyToken(refreshToken);
    if (payload.type !== 'refresh') throw new UnauthorizedException();
    
    // Revocar token anterior
    await this.tokenService.revokeToken(payload.jti, payload.sub, 'refresh');
    
    return await this.tokenService.generateTokenPair(payload.sub, payload.email);
  }

  async logout(jti: string, userId: number): Promise<void> {
    await this.tokenService.revokeToken(jti, userId, 'logout');
  }

  async changePassword(userId: number, dto: ChangePasswordDto): Promise<void> {
    const user = await this.usersService.findOne(userId);
    const valid = await this.hashService.verify(user.passwordHash, dto.currentPassword);
    if (!valid) throw new BadRequestException('Invalid current password');
    
    user.passwordHash = await this.hashService.hash(dto.newPassword);
    await this.usersService.update(userId, { passwordHash: user.passwordHash } as any);
  }

  async recoverRequest(email: string): Promise<void> {
    // Generar token único de recuperación y enviar email
    // TODO: implementar con mailer
  }

  async recoverConfirm(token: string, newPassword: string): Promise<void> {
    // Validar token y actualizar contraseña
    // TODO: implementar validación de token de recuperación
  }
}
```

### 6.6 Strategies

**Archivo:** `src/modules/auth/strategies/jwt.strategy.ts`

```typescript
@Injectable()
export class JwtStrategy extends PassportStrategy(Strategy) {
  constructor(
    config: ConfigService,
    private tokenService: TokenService,
  ) {
    super({
      jwtFromRequest: ExtractJwt.fromAuthHeaderAsBearerToken(),
      secretOrKey: config.get('jwt.secret'),
    });
  }

  async validate(payload: JwtPayload): Promise<JwtPayload> {
    if (await this.tokenService.isRevoked(payload.jti)) {
      throw new UnauthorizedException('Token revoked');
    }
    return payload;
  }
}
```

**Archivo:** `src/modules/auth/strategies/local.strategy.ts`

```typescript
@Injectable()
export class LocalStrategy extends PassportStrategy(Strategy) {
  constructor(private authService: AuthService) {
    super({ usernameField: 'identity', passwordField: 'password' });
  }

  async validate(identity: string, password: string): Promise<UserEntity> {
    const user = await this.authService.validateUser(identity, password);
    if (!user) throw new UnauthorizedException('Invalid credentials');
    return user;
  }
}
```

### 6.7 Auth Controller

```typescript
@ApiTags('Auth')
@Controller('auth')
export class AuthController {
  @Public()
  @Post('login')
  async login(@Body() dto: LoginDto) {
    const user = await this.authService.validateUser(dto.identity, dto.password);
    return await this.authService.login(user);
  }

  @Public()
  @Post('refresh')
  async refresh(@Body() dto: RefreshDto) {
    return await this.authService.refresh(dto.refreshToken);
  }

  @Post('logout')
  async logout(@CurrentUser() user: JwtPayload) {
    await this.authService.logout(user.jti, user.sub);
  }

  @Patch('change-password')
  async changePassword(@CurrentUser() user: JwtPayload, @Body() dto: ChangePasswordDto) {
    await this.authService.changePassword(user.sub, dto);
  }
}
```

### 6.8 Auth Module

```typescript
@Module({
  imports: [
    UsersModule,
    PassportModule,
    JwtModule.registerAsync({
      inject: [ConfigService],
      useFactory: (config: ConfigService) => ({
        secret: config.get('jwt.secret'),
        signOptions: { expiresIn: config.get('jwt.accessExpiration') },
      }),
    }),
    TypeOrmModule.forFeature([RevokedTokenEntity]),
  ],
  providers: [AuthService, HashService, TokenService, JwtStrategy, LocalStrategy],
  controllers: [AuthController],
})
export class AuthModule {}
```

---

## FASE 7: Guards y Decorators

### 7.1 JWT Auth Guard

**Archivo:** `src/common/guards/jwt-auth.guard.ts`

```typescript
@Injectable()
export class JwtAuthGuard extends AuthGuard('jwt') {
  constructor(private reflector: Reflector) {
    super();
  }

  canActivate(context: ExecutionContext) {
    const isPublic = this.reflector.get<boolean>('isPublic', context.getHandler());
    if (isPublic) return true;
    return super.canActivate(context);
  }
}
```

### 7.2 Actions Guard

**Archivo:** `src/common/guards/actions.guard.ts`

```typescript
@Injectable()
export class ActionsGuard implements CanActivate {
  constructor(
    private reflector: Reflector,
    private authzService: AuthorizationService,
  ) {}

  async canActivate(context: ExecutionContext): Promise<boolean> {
    const requiredActions = this.reflector.get<string[]>('actions', context.getHandler());
    if (!requiredActions) return true;

    const request = context.switchToHttp().getRequest();
    const user = request.user as JwtPayload;

    return await this.authzService.hasAllActions(user.sub, requiredActions);
  }
}
```

### 7.3 Decorators

**Archivo:** `src/common/decorators/actions.decorator.ts`

```typescript
export const Actions = (...actions: string[]) => SetMetadata('actions', actions);
```

**Archivo:** `src/common/decorators/public.decorator.ts`

```typescript
export const Public = () => SetMetadata('isPublic', true);
```

**Archivo:** `src/common/decorators/current-user.decorator.ts`

```typescript
export const CurrentUser = createParamDecorator(
  (data: unknown, ctx: ExecutionContext) => {
    const request = ctx.switchToHttp().getRequest();
    return request.user;
  },
);
```

---

## FASE 8: Redis Cache

### 8.1 Configurar Redis Module

**En app.module.ts:**

```typescript
@Module({
  imports: [
    CacheModule.registerAsync({
      isGlobal: true,
      inject: [ConfigService],
      useFactory: (config: ConfigService) => ({
        store: redisStore,
        host: config.get('redis.host'),
        port: config.get('redis.port'),
        ttl: 900, // 15 minutos
      }),
    }),
    // ... otros módulos
  ],
})
```

**Tareas:**
- [ ] Instalar `cache-manager` y `cache-manager-redis-store`
- [ ] Configurar CacheModule en app.module
- [ ] AuthorizationService ya usa caché (ver PARTE 2)
- [ ] Invalidar caché en GroupsService y UsersService al modificar relaciones

---

## FASE 9: Tests

### 9.1 Unit Test - Authorization Service

**Archivo:** `src/common/services/authorization.service.spec.ts`

```typescript
describe('AuthorizationService', () => {
  it('should calculate effective actions with composite groups', async () => {
    // Mock: User -> Group A (action1) -> Group B (action2) + User.actions (action3)
    const actions = await service.getEffectiveActions(1);
    expect(actions).toContain('action1');
    expect(actions).toContain('action2');
    expect(actions).toContain('action3');
    expect(actions.size).toBe(3);
  });
});
```

### 9.2 E2E Test - Auth

**Archivo:** `test/auth.e2e-spec.ts`

```typescript
describe('Auth E2E', () => {
  it('/auth/login (POST)', () => {
    return request(app.getHttpServer())
      .post('/auth/login')
      .send({ identity: 'admin@hotel.com', password: 'Admin123!' })
      .expect(200)
      .expect((res) => {
        expect(res.body.accessToken).toBeDefined();
        expect(res.body.refreshToken).toBeDefined();
      });
  });

  it('/auth/refresh (POST)', async () => {
    const { refreshToken } = await login();
    return request(app.getHttpServer())
      .post('/auth/refresh')
      .send({ refreshToken })
      .expect(200);
  });

  it('/auth/logout (POST)', async () => {
    const { accessToken } = await login();
    return request(app.getHttpServer())
      .post('/auth/logout')
      .set('Authorization', `Bearer ${accessToken}`)
      .expect(200);
  });
});
```

**Tareas:**
- [ ] Crear tests unitarios para AuthorizationService (patrón Composite)
- [ ] Crear tests E2E para flujos de Auth (login, refresh, logout)
- [ ] Configurar Jest coverage mínimo 80%
- [ ] Ejecutar: `npm run test` y `npm run test:e2e`

---

## FASE 10: Swagger Documentation

### 10.1 Configurar Swagger

**En main.ts:**

```typescript
const config = new DocumentBuilder()
  .setTitle('MyHotelFlow API')
  .setDescription('Sistema de Reservas Hoteleras - Módulo de Seguridad')
  .setVersion('1.0')
  .addBearerAuth()
  .build();

const document = SwaggerModule.createDocument(app, config);
SwaggerModule.setup('api/docs', app, document);
```

### 10.2 Decoradores en DTOs

```typescript
export class CreateUserDto {
  @ApiProperty({ example: 'jdoe', description: 'Username único' })
  @IsString()
  username: string;
  // ... resto de propiedades con @ApiProperty
}
```

**Tareas:**
- [ ] Configurar Swagger en main.ts
- [ ] Agregar @ApiProperty en TODOS los DTOs
- [ ] Agregar @ApiTags, @ApiOperation, @ApiBearerAuth en controllers
- [ ] Exportar OpenAPI JSON: `GET /api/docs-json`
- [ ] Verificar en navegador: `http://localhost:3000/api/docs`

---

## FASE 11: Postman Collection

### 11.1 Crear Collection

```json
{
  "info": { "name": "MyHotelFlow - Security Module" },
  "item": [
    {
      "name": "Auth",
      "item": [
        { "name": "Login", "request": { "method": "POST", "url": "{{base}}/auth/login" } },
        { "name": "Refresh", "request": { "method": "POST", "url": "{{base}}/auth/refresh" } },
        { "name": "Logout", "request": { "method": "POST", "url": "{{base}}/auth/logout" } }
      ]
    },
    {
      "name": "Users",
      "item": [
        { "name": "List Users", "request": { "method": "GET", "url": "{{base}}/users" } }
      ]
    }
  ]
}
```

**Tareas:**
- [ ] Crear collection en Postman
- [ ] Configurar environment con variables: `base_url`, `access_token`
- [ ] Crear requests para todos los endpoints (Auth, Users, Groups, Actions)
- [ ] Configurar auto-refresh de tokens con Pre-request Scripts
- [ ] Exportar collection JSON

---

## FASE 12: Verificación Final

### 12.1 Checklist de Validación

- [ ] **Entities:** 5 entidades TypeORM con relaciones ManyToMany
- [ ] **Migrations:** Generadas y ejecutadas correctamente
- [ ] **Seeders:** 52 acciones, 3 grupos, 1 admin creados
- [ ] **Modules:** Actions, Groups, Users, Auth registrados en AppModule
- [ ] **Auth:** Login con Argon2, JWT con JTI, refresh con rotación, logout con blacklist
- [ ] **Guards:** JwtAuthGuard + ActionsGuard protegiendo endpoints
- [ ] **Authorization:** Composite pattern con DFS, caché Redis 15min
- [ ] **Tests:** Unit tests para getEffectiveActions, E2E para auth flows
- [ ] **Swagger:** Documentación completa en `/api/docs`
- [ ] **Postman:** Collection funcional con todos los endpoints

### 12.2 Comandos de Verificación

```bash
# Compilar
npm run build

# Ejecutar migraciones
npm run migration:run

# Ejecutar seeders
npm run seed

# Tests
npm run test
npm run test:e2e
npm run test:cov

# Iniciar aplicación
npm run start:dev

# Verificar Swagger
curl http://localhost:3000/api/docs

# Probar login
curl -X POST http://localhost:3000/auth/login \
  -H "Content-Type: application/json" \
  -d '{"identity":"admin@hotel.com","password":"Admin123!"}'
```

---

## FASE 13: Próximos Pasos

### 13.1 Mejoras Opcionales

- [ ] **Rate Limiting:** Throttler de NestJS (5 requests/min en login)
- [ ] **Audit Log:** Interceptor global para registrar todas las operaciones
- [ ] **Email Service:** Integrar Mailer para recuperación de contraseña
- [ ] **2FA:** Autenticación de dos factores con TOTP
- [ ] **Session Management:** Listar y revocar sesiones activas por usuario
- [ ] **Password Complexity:** Validador custom con zxcvbn
- [ ] **CORS:** Configurar whitelist de dominios permitidos
- [ ] **Helmet:** Security headers (CSP, HSTS, X-Frame-Options)
- [ ] **Monitoring:** Integrar Sentry para error tracking

### 13.2 Documentación Adicional

- [ ] README.md con instrucciones de setup
- [ ] ARCHITECTURE.md explicando Clean Architecture + Hexagonal
- [ ] SECURITY.md con políticas de seguridad
- [ ] CONTRIBUTING.md para nuevos desarrolladores

---

## 🎯 RESUMEN EJECUTIVO

### Arquitectura Implementada

- **Clean Architecture:** Domain → Application → Infrastructure → Interface
- **Patrón Composite:** Grupos pueden contener acciones y/o grupos hijos (recursivo)
- **Cache Aside:** Redis para permisos efectivos (TTL 15min)
- **Repository Pattern:** TypeORM Data Mapper
- **JWT con JTI:** Tokens únicos identificables, rotación en refresh, blacklist en logout
- **Argon2id:** Hashing de contraseñas con parámetros configurables
- **Guards + Decorators:** Protección declarativa de endpoints

### Flujo de Autenticación

1. **Login:** Validar credenciales → Verificar lockout → Hash con Argon2 → Generar JWT (access 15m, refresh 7d)
2. **Request:** Verificar token → Validar JTI no revocado → Cargar permisos de caché → Verificar acción requerida
3. **Refresh:** Verificar refresh token → Revocar anterior → Generar nuevo par
4. **Logout:** Revocar JTI en blacklist → Invalidar caché de permisos

### Permisos Efectivos (Composite)

```
Usuario → [Grupos] → [Grupos Hijos] → [Acciones]
         ↘ [Acciones directas] (excepciones)

Algoritmo: DFS con Set<visited> para evitar ciclos
Caché: Redis con key user:perm:{userId}, TTL 15min
```

### Endpoints Principales

```
POST   /auth/login              (Public)
POST   /auth/refresh            (Public)
POST   /auth/logout             (JWT)
PATCH  /auth/change-password    (JWT)

GET    /users                   @Actions('config.usuarios.listar')
POST   /users                   @Actions('config.usuarios.crear')
PATCH  /users/:id/groups        @Actions('config.usuarios.asignarGrupos')

GET    /groups                  @Actions('config.grupos.listar')
PATCH  /groups/:id/actions      @Actions('config.grupos.asignarAcciones')
PATCH  /groups/:id/children     @Actions('config.grupos.asignarHijos')

GET    /actions                 @Actions('config.acciones.listar')
```

### Stack Tecnológico Final

- **NestJS 11** + TypeScript 5+
- **PostgreSQL 15** + TypeORM 0.3
- **Redis 7** + cache-manager
- **Argon2** + JWT + Passport
- **Jest** + Supertest
- **Swagger** + Postman
- **Docker Compose** (postgres, redis, mailhog)

---

## ✅ FIN DEL CHECKLIST

**El módulo de seguridad está completamente especificado y listo para implementar paso a paso.**

**Tiempo estimado de implementación:** 40-60 horas (1-2 semanas para 1 desarrollador)

**Orden recomendado:** FASE 1 → FASE 2 → FASE 3 → FASE 4 → FASE 5 → FASE 6 → FASE 7 → FASE 8 → FASE 9 → FASE 10 → FASE 11 → FASE 12

🚀 **¡Éxito en la implementación!**
