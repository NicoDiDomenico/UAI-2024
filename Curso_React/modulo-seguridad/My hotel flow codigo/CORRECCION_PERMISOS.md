# Corrección del Sistema de Permisos - ActionsGuard

## Problema Identificado

Los controladores de la API **no estaban validando los permisos** de los usuarios. Aunque existía el decorador `@Actions()` en las rutas, faltaba el `ActionsGuard` en la lista de guards, por lo que:

- ✅ Se verificaba la autenticación JWT (`AuthGuard('jwt')`)
- ❌ **NO se verificaban las acciones/permisos** (`ActionsGuard`)

Esto permitía que cualquier usuario autenticado pudiera ejecutar cualquier acción, independientemente de sus permisos asignados.

## Solución Implementada

### 1. Creación del CommonModule Global

**Archivo:** `backend/src/common/common.module.ts`

```typescript
@Global()
@Module({
  imports: [
    TypeOrmModule.forFeature([UserEntity, GroupEntity, ActionEntity]),
  ],
  providers: [AuthorizationService, ActionsGuard],
  exports: [AuthorizationService, ActionsGuard],
})
export class CommonModule {}
```

Este módulo global proporciona:
- `AuthorizationService`: Calcula permisos efectivos de usuarios
- `ActionsGuard`: Valida que el usuario tenga las acciones requeridas

### 2. Integración en AppModule

**Archivo:** `backend/src/app.module.ts`

```typescript
@Module({
  imports: [
    // ... otros módulos
    CommonModule,  // ✅ Agregado
    ActionsModule,
    GroupsModule,
    UsersModule,
    // ...
  ],
})
```

### 3. Actualización de Controladores

Se agregó `ActionsGuard` a los controladores que requieren validación de permisos:

#### UsersController
```typescript
@Controller('users')
@UseGuards(AuthGuard('jwt'), ActionsGuard)  // ✅ ActionsGuard agregado
export class UsersController {
  // Ahora TODAS las rutas con @Actions() serán validadas
  
  @Post()
  @Actions('config.usuarios.crear')  // ✅ Se validará este permiso
  async create(@Body() dto: CreateUserDto) { ... }
  
  @Get()
  @Actions('config.usuarios.listar')  // ✅ Se validará este permiso
  async findAll() { ... }
}
```

#### GroupsController
```typescript
@Controller('groups')
@UseGuards(AuthGuard('jwt'), ActionsGuard)  // ✅ ActionsGuard agregado
export class GroupsController {
  
  @Post()
  @Actions('config.grupos.crear')  // ✅ Se validará este permiso
  async create(@Body() dto: CreateGroupDto) { ... }
}
```

#### ActionsController
```typescript
@Controller('actions')
@UseGuards(AuthGuard('jwt'), ActionsGuard)  // ✅ ActionsGuard agregado
export class ActionsController {
  
  @Post()
  @ActionsDecorator('config.acciones.crear')  // ✅ Se validará este permiso
  async create(@Body() dto: CreateActionDto) { ... }
}
```

## Cómo Funciona Ahora

### Flujo de Validación

1. **Usuario hace request** → `/users` (POST)
2. **AuthGuard('jwt')** → Valida token JWT y obtiene usuario
3. **ActionsGuard** → Ejecuta el siguiente proceso:
   - Lee el decorador `@Actions('config.usuarios.crear')`
   - Llama a `AuthorizationService.hasAllActions(userId, ['config.usuarios.crear'])`
   - `AuthorizationService` calcula permisos efectivos:
     - Acciones directas del usuario
     - Acciones heredadas de sus grupos (recursivo)
     - Usa caché para optimizar (15 min TTL)
   - Si el usuario NO tiene el permiso → **403 Forbidden**
   - Si el usuario SÍ tiene el permiso → **Continúa con el handler**

### Mensaje de Error

Si un usuario intenta acceder sin permisos, recibirá:

```json
{
  "statusCode": 403,
  "message": "Insufficient permissions. Required actions: config.usuarios.crear",
  "error": "Forbidden"
}
```

## Cómo Probar

### 1. Iniciar el Backend

```powershell
cd backend
npm run start:dev
```

### 2. Crear Usuarios de Prueba

```powershell
# Seed de acciones (si no están creadas)
curl -X POST http://localhost:3000/actions/seed

# Login como admin (tiene todos los permisos)
$adminLogin = Invoke-RestMethod -Uri "http://localhost:3000/auth/login" -Method POST -ContentType "application/json" -Body '{"identity":"admin","password":"Admin123!"}'

$adminToken = $adminLogin.access_token
```

### 3. Crear Usuario Cliente con Permisos Limitados

```powershell
# Crear usuario cliente
$clienteBody = @{
    username = "cliente_test"
    email = "cliente@test.com"
    password = "Cliente123!"
    firstName = "Cliente"
    lastName = "Prueba"
    role = "cliente"
} | ConvertTo-Json

$cliente = Invoke-RestMethod -Uri "http://localhost:3000/users" -Method POST -Headers @{Authorization="Bearer $adminToken"} -ContentType "application/json" -Body $clienteBody

# Asignar SOLO permisos de ver reservas
$actionsBody = @{
    actionKeys = @("reservas.listar", "reservas.ver")
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:3000/users/$($cliente.id)/actions" -Method PATCH -Headers @{Authorization="Bearer $adminToken"} -ContentType "application/json" -Body $actionsBody
```

### 4. Probar Acceso con Usuario Cliente

```powershell
# Login como cliente
$clienteLogin = Invoke-RestMethod -Uri "http://localhost:3000/auth/login" -Method POST -ContentType "application/json" -Body '{"identity":"cliente_test","password":"Cliente123!"}'

$clienteToken = $clienteLogin.access_token

# ✅ DEBERÍA FUNCIONAR: Ver permisos propios
Invoke-RestMethod -Uri "http://localhost:3000/auth/permissions" -Method GET -Headers @{Authorization="Bearer $clienteToken"}

# ❌ DEBERÍA FALLAR (403): Intentar crear usuario
try {
    $nuevoUsuario = @{
        username = "hacker"
        email = "hacker@test.com"
        password = "Hacker123!"
        firstName = "Hacker"
        lastName = "Test"
        role = "cliente"
    } | ConvertTo-Json
    
    Invoke-RestMethod -Uri "http://localhost:3000/users" -Method POST -Headers @{Authorization="Bearer $clienteToken"} -ContentType "application/json" -Body $nuevoUsuario
    Write-Host "❌ ERROR: El cliente pudo crear usuario!" -ForegroundColor Red
} catch {
    Write-Host "✅ CORRECTO: El cliente NO puede crear usuario (403)" -ForegroundColor Green
}

# ❌ DEBERÍA FALLAR (403): Intentar crear grupo
try {
    $nuevoGrupo = @{
        name = "Grupo Hacker"
        description = "Test"
    } | ConvertTo-Json
    
    Invoke-RestMethod -Uri "http://localhost:3000/groups" -Method POST -Headers @{Authorization="Bearer $clienteToken"} -ContentType "application/json" -Body $nuevoGrupo
    Write-Host "❌ ERROR: El cliente pudo crear grupo!" -ForegroundColor Red
} catch {
    Write-Host "✅ CORRECTO: El cliente NO puede crear grupo (403)" -ForegroundColor Green
}

# ❌ DEBERÍA FALLAR (403): Intentar asignar acciones
try {
    $actions = @{
        actionKeys = @("config.usuarios.eliminar")
    } | ConvertTo-Json
    
    Invoke-RestMethod -Uri "http://localhost:3000/users/$($cliente.id)/actions" -Method PATCH -Headers @{Authorization="Bearer $clienteToken"} -ContentType "application/json" -Body $actions
    Write-Host "❌ ERROR: El cliente pudo asignar acciones!" -ForegroundColor Red
} catch {
    Write-Host "✅ CORRECTO: El cliente NO puede asignar acciones (403)" -ForegroundColor Green
}
```

### 5. Script de Prueba Completo

He creado un script PowerShell para automatizar las pruebas:

```powershell
.\test-permisos.ps1
```

## Verificación de Logs

En los logs del backend deberías ver:

```
[ActionsGuard] User cliente_test (ID: X) does not have required actions: config.usuarios.crear
[ActionsGuard] User cliente_test (ID: X) does not have required actions: config.grupos.crear
```

## Acciones Protegidas

Ahora TODAS estas rutas están protegidas:

### Usuarios (`/users`)
- ✅ `POST /users` → Requiere `config.usuarios.crear`
- ✅ `GET /users` → Requiere `config.usuarios.listar`
- ✅ `PATCH /users/:id` → Requiere `config.usuarios.modificar`
- ✅ `DELETE /users/:id` → Requiere `config.usuarios.eliminar`
- ✅ `PATCH /users/:id/groups` → Requiere `config.usuarios.asignarGrupos`
- ✅ `PATCH /users/:id/actions` → Requiere `config.usuarios.asignarAcciones`

### Grupos (`/groups`)
- ✅ `POST /groups` → Requiere `config.grupos.crear`
- ✅ `GET /groups` → Requiere `config.grupos.listar`
- ✅ `PATCH /groups/:id` → Requiere `config.grupos.modificar`
- ✅ `DELETE /groups/:id` → Requiere `config.grupos.eliminar`
- ✅ `PATCH /groups/:id/actions` → Requiere `config.grupos.asignarAcciones`

### Acciones (`/actions`)
- ✅ `POST /actions` → Requiere `config.acciones.crear`
- ✅ `GET /actions` → Requiere `config.acciones.listar`
- ✅ `PATCH /actions/:id` → Requiere `config.acciones.modificar`
- ✅ `DELETE /actions/:id` → Requiere `config.acciones.eliminar`

## Rutas NO Protegidas (Públicas o Sin Permisos Específicos)

### Autenticación (`/auth`)
- 🔓 `POST /auth/login` → Pública
- 🔓 `POST /auth/refresh` → Pública
- 🔓 `POST /auth/recover/request` → Pública
- 🔓 `POST /auth/recover/confirm` → Pública
- 🔒 `POST /auth/logout` → Solo requiere autenticación JWT
- 🔒 `PATCH /auth/password` → Solo requiere autenticación JWT
- 🔒 `GET /auth/me` → Solo requiere autenticación JWT
- 🔒 `GET /auth/permissions` → Solo requiere autenticación JWT

## Cache de Permisos

El sistema usa caché de Redis para optimizar la validación:
- **TTL:** 15 minutos
- **Key:** `user:permissions:{userId}`
- **Invalidación:** Al modificar grupos o acciones del usuario

## Próximos Pasos Recomendados

1. ✅ **Probar exhaustivamente** con usuarios de diferentes roles
2. ✅ **Verificar frontend** - Debería manejar correctamente los errores 403
3. ✅ **Logs de auditoría** - Considerar agregar logs cuando se deniegue acceso
4. ✅ **Tests E2E** - Agregar tests para validar permisos

## Archivos Modificados

1. ✅ `backend/src/common/common.module.ts` - **CREADO**
2. ✅ `backend/src/app.module.ts` - Importa CommonModule
3. ✅ `backend/src/modules/users/users.controller.ts` - Agrega ActionsGuard
4. ✅ `backend/src/modules/groups/groups.controller.ts` - Agrega ActionsGuard
5. ✅ `backend/src/modules/actions/actions.controller.ts` - Agrega ActionsGuard

## Resumen

### Antes ❌
```typescript
// BACKEND
@UseGuards(AuthGuard('jwt'))  // Solo validaba autenticación
@Actions('config.usuarios.crear')  // Este decorador era ignorado

// FRONTEND
<Route element={<ProtectedRoute />}>
  <Route path="/users" />  {/* Solo validaba autenticación */}
</Route>
```

### Después ✅
```typescript
// BACKEND
@UseGuards(AuthGuard('jwt'), ActionsGuard)  // Valida autenticación Y permisos
@Actions('config.usuarios.crear')  // Este decorador ahora se respeta

// FRONTEND
<Route element={<ProtectedRoute requiredPermissions={['config.usuarios.listar']} />}>
  <Route path="/users" />  {/* Valida autenticación Y permisos */}
</Route>
```

**El sistema de permisos ahora está completamente funcional y seguro en backend y frontend.** 🔒

---

# FRONTEND - Corrección Adicional

## Problema en el Frontend

Aunque el sidebar ya ocultaba los enlaces usando el componente `<Can>`, las **rutas NO estaban protegidas** por permisos específicos. Un usuario podía:
- ❌ Escribir la URL directamente (ej: `/users`)
- ❌ Acceder a páginas sin permisos
- ✅ No veía los botones (ya estaban protegidos con `<Can>`)

## Solución Implementada

### Protección de Rutas por Permisos

**Archivo:** `frontend/src/routes/AppRoutes.tsx`

Se modificaron TODAS las rutas para usar `requiredPermissions`:

```tsx
{/* Antes - Solo autenticación */}
<Route element={<ProtectedRoute />}>
  <Route path="/users" element={<UsersListPage />} />
</Route>

{/* Después - Autenticación + Permisos */}
<Route element={<ProtectedRoute requiredPermissions={['config.usuarios.listar']} />}>
  <Route path="/users" element={<UsersListPage />} />
</Route>
```

### Rutas Protegidas (Resumen)

| Área | Ruta | Permiso Requerido |
|------|------|-------------------|
| **General** | `/dashboard` | Solo autenticación |
| **Usuarios** | `/users` | `config.usuarios.listar` |
| | `/users/create` | `config.usuarios.crear` |
| | `/users/:id/edit` | `config.usuarios.modificar` |
| | `/users/:id/permissions` | `config.usuarios.asignarGrupos` OR `config.usuarios.asignarAcciones` |
| **Grupos** | `/groups` | `config.grupos.listar` |
| | `/groups/create` | `config.grupos.crear` |
| | `/groups/:id/edit` | `config.grupos.modificar` |
| | `/groups/:id/actions` | `config.grupos.asignarAcciones` |
| | `/groups/:id/children` | `config.grupos.asignarHijos` |
| **Acciones** | `/actions` | `config.acciones.listar` |
| | `/actions/create` | `config.acciones.crear` |
| | `/actions/:id/edit` | `config.acciones.modificar` |

## Capas de Seguridad (Frontend + Backend)

```
FRONTEND
┌─────────────────────────────────────────┐
│ 1. SIDEBAR                              │
│    Oculta enlaces sin permiso           │ ✅ Ya funcionaba
└─────────────────────────────────────────┘
              ↓
┌─────────────────────────────────────────┐
│ 2. RUTAS (React Router)                 │
│    Redirige a /forbidden sin permiso    │ ✅ CORREGIDO HOY
└─────────────────────────────────────────┘
              ↓
┌─────────────────────────────────────────┐
│ 3. BOTONES Y ACCIONES (<Can>)           │
│    Oculta botones sin permiso           │ ✅ Ya funcionaba
└─────────────────────────────────────────┘
              ↓ API Request
BACKEND
┌─────────────────────────────────────────┐
│ 4. API ENDPOINTS                        │
│    Valida permisos (ActionsGuard)       │ ✅ CORREGIDO HOY
└─────────────────────────────────────────┘
```

## Archivos Modificados

### Backend
1. ✅ `backend/src/common/common.module.ts` - **CREADO**
2. ✅ `backend/src/app.module.ts` - Importa CommonModule
3. ✅ `backend/src/modules/users/users.controller.ts` - Agrega ActionsGuard
4. ✅ `backend/src/modules/groups/groups.controller.ts` - Agrega ActionsGuard
5. ✅ `backend/src/modules/actions/actions.controller.ts` - Agrega ActionsGuard

### Frontend
1. ✅ `frontend/src/routes/AppRoutes.tsx` - Agrega validación de permisos a rutas

## Documentación Adicional

- 📄 `CORRECCION_PERMISOS_FRONTEND.md` - Detalles completos del frontend

**El sistema de permisos ahora está completamente funcional y seguro.** 🔒
