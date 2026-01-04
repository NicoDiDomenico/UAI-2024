# Arquitectura General - MindFit Intelligence

## Índice

1. [Visión general](#visión-general)
2. [Arquitectura de alto nivel](#arquitectura-de-alto-nivel)
3. [Componentes principales](#componentes-principales)
4. [Flujos de datos](#flujos-de-datos)
5. [Patrones y principios](#patrones-y-principios)

---

## Visión general

MindFit Intelligence es un sistema SaaS B2B para gestión integral de gimnasios de musculación, compuesto por:

- **Sitio público (Marketing):** Landing page sin autenticación para captación de clientes
- **Portal de clientes (Plataforma):** Aplicación web autenticada multi-tenant para gestión del gimnasio

### Características clave

- Multi-tenancy por `GymId` en base de datos única
- Autenticación JWT + Refresh Token
- Autorización RBAC por grupos con permisos granulares
- Arquitectura en capas (Controllers, Services, Models)
- API REST para comunicación frontend-backend

---

## Arquitectura de alto nivel

```
┌─────────────────────────────────────────────────────────────────┐
│                         USUARIOS FINALES                         │
│  (Dueños gimnasios, Administradores, Entrenadores, Socios)      │
└────────────────┬────────────────────────────────────────────────┘
                 │ HTTPS
                 ▼
┌─────────────────────────────────────────────────────────────────┐
│                     FRONTEND (React.js)                          │
├─────────────────────────────────────────────────────────────────┤
│  ┌──────────────────────┐  ┌──────────────────────────────────┐ │
│  │  SITIO PÚBLICO       │  │  PORTAL DE CLIENTES              │ │
│  │  (Marketing)         │  │  (Plataforma)                    │ │
│  ├──────────────────────┤  ├──────────────────────────────────┤ │
│  │ • Inicio             │  │ • Dashboard                      │ │
│  │ • Funcionalidades    │  │ • Gestión Socios                 │ │
│  │ • Precios            │  │ • Gestión Turnos                 │ │
│  │ • Testimonios        │  │ • Gestión Rutinas                │ │
│  │ • Blog               │  │ • Gestión Gimnasio               │ │
│  │ • Contacto           │  │ • Seguridad (Usuarios/Grupos)    │ │
│  │ • Solicitar Demo     │  │ • Asistencia IA                  │ │
│  └──────────────────────┘  └──────────────────────────────────┘ │
└────────────────┬────────────────────────────────────────────────┘
                 │ REST API (JSON)
                 │ Authorization: Bearer <JWT>
                 ▼
┌─────────────────────────────────────────────────────────────────┐
│                   BACKEND (ASP.NET Core 8)                       │
├─────────────────────────────────────────────────────────────────┤
│  ┌────────────────────────────────────────────────────────────┐ │
│  │              MIDDLEWARE PIPELINE                           │ │
│  │  • CORS                                                    │ │
│  │  • Authentication (JWT Bearer)                             │ │
│  │  • Tenant Resolver (GymId from claims)                     │ │
│  │  • Authorization (Dynamic RBAC)                            │ │
│  │  • Exception Handling                                      │ │
│  │  • Request Logging                                         │ │
│  └────────────────────────────────────────────────────────────┘ │
│                                                                  │
│  ┌────────────────────────────────────────────────────────────┐ │
│  │                     CONTROLLERS                            │ │
│  │  • AuthController                                          │ │
│  │  • PublicController (Lead, Contact)                        │ │
│  │  • GymController (Search)                                  │ │
│  │  • SociosController                                        │ │
│  │  • TurnosController                                        │ │
│  │  • RutinasController                                       │ │
│  │  • GimnasioController (Máquinas, Ejercicios, Horarios...)  │ │
│  │  • SeguridadController (Usuarios, Grupos, Permisos)        │ │
│  │  • IAController (Mock)                                     │ │
│  └────────────────┬───────────────────────────────────────────┘ │
│                   │ Delegan lógica
│                   ▼
│  ┌────────────────────────────────────────────────────────────┐ │
│  │                       SERVICES                             │ │
│  │  • AuthService (Login, Refresh, Logout, Password Reset)    │ │
│  │  • PermissionService (Resolve permissions by GymId)        │ │
│  │  • PublicService (Lead, Contact)                           │ │
│  │  • SocioService                                            │ │
│  │  • TurnoService                                            │ │
│  │  • RutinaService                                           │ │
│  │  • GimnasioService                                         │ │
│  │  • UsuarioService                                          │ │
│  │  • GrupoService                                            │ │
│  │  • EmailService (IEmailService abstraction)                │ │
│  │  • IAService (Mock)                                        │ │
│  └────────────────┬───────────────────────────────────────────┘ │
│                   │ Acceso a datos
│                   ▼
│  ┌────────────────────────────────────────────────────────────┐ │
│  │           DATA ACCESS (Entity Framework Core)              │ │
│  │  • ApplicationDbContext                                    │ │
│  │  • Models (Entidades)                                      │ │
│  │  • Migrations                                              │ │
│  │  • Query Filters (Tenant Isolation by GymId)               │ │
│  └────────────────┬───────────────────────────────────────────┘ │
│                   │
└───────────────────┼─────────────────────────────────────────────┘
                    │ ADO.NET / EF Core
                    ▼
┌─────────────────────────────────────────────────────────────────┐
│               BASE DE DATOS (SQL Server)                         │
├─────────────────────────────────────────────────────────────────┤
│  • Gyms (Tenants)                                                │
│  • Usuarios, Grupos, Permisos (Multi-tenant by GymId)           │
│  • Socios, Turnos, Rutinas (Multi-tenant by GymId)              │
│  • Máquinas, Ejercicios, Equipamiento (Multi-tenant by GymId)   │
│  • RefreshTokens, PasswordResetTokens                            │
│  • Leads, ContactMessages (No GymId - público)                   │
└─────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────┐
│                   SERVICIOS EXTERNOS                             │
├─────────────────────────────────────────────────────────────────┤
│  • SMTP Server (envío de emails)                                │
│  • Futura integración IA (GPT API, etc.)                         │
└─────────────────────────────────────────────────────────────────┘
```

---

## Componentes principales

### 1. Frontend (React.js)

#### Sitio Público

- **Propósito:** Captación de clientes (gimnasios)
- **Características:**
  - Sin autenticación
  - Responsive (mobile-first)
  - SEO optimizado
  - Contenido estático (Blog, Testimonios, Precios)
  - Formularios: Solicitar Demo, Contacto
- **Rutas:**
  - `/` - Inicio
  - `/funcionalidades` - Funcionalidades
  - `/precios` - Planes y precios
  - `/testimonios` - Testimonios de clientes
  - `/blog` - Artículos (contenido estático)
  - `/contacto` - Formulario de contacto
  - `/solicitar-demo` - Formulario lead

#### Portal de Clientes

- **Propósito:** Gestión integral del gimnasio
- **Características:**
  - Autenticación obligatoria
  - Multi-tenant (usuario pertenece a un gimnasio)
  - RBAC (permisos dinámicos por grupos)
  - Access Token en memoria (no localStorage)
  - Refresh Token en cookie HttpOnly
  - Auto-refresh ante 401
- **Acceso:**
  - Botón "Acceso Clientes" en sitio público
  - Pantalla de login:
    1. Selector "Buscar gimnasio..." (autocomplete)
    2. Usuario
    3. Contraseña
- **Módulos:**
  - Dashboard
  - Socios
  - Turnos
  - Rutinas
  - Gestión del Gimnasio (Máquinas, Ejercicios, Horarios, Entrenadores)
  - Seguridad (Usuarios, Grupos, Permisos)
  - Asistencia IA

### 2. Backend (ASP.NET Core 8)

#### Organización

Proyecto único con estructura de carpetas:

```
MindFit.Backend/
├── Controllers/
│   ├── AuthController.cs
│   ├── PublicController.cs
│   ├── GymController.cs
│   ├── SociosController.cs
│   ├── TurnosController.cs
│   ├── RutinasController.cs
│   ├── GimnasioController.cs
│   ├── SeguridadController.cs
│   └── IAController.cs
├── Services/
│   ├── Interfaces/
│   │   ├── IAuthService.cs
│   │   ├── IPermissionService.cs
│   │   ├── IEmailService.cs
│   │   └── ...
│   ├── AuthService.cs
│   ├── PermissionService.cs
│   ├── SocioService.cs
│   ├── TurnoService.cs
│   ├── RutinaService.cs
│   ├── GimnasioService.cs
│   ├── UsuarioService.cs
│   ├── GrupoService.cs
│   ├── EmailService.cs
│   └── IAService.cs
├── Models/
│   ├── Gym.cs
│   ├── Usuario.cs
│   ├── Grupo.cs
│   ├── Permiso.cs
│   ├── UsuarioGrupo.cs
│   ├── GrupoPermiso.cs
│   ├── RefreshToken.cs
│   ├── PasswordResetToken.cs
│   ├── Socio.cs
│   ├── Turno.cs
│   ├── Rutina.cs
│   ├── Lead.cs
│   ├── ContactMessage.cs
│   └── ...
├── DTOs/
│   ├── Auth/
│   ├── Public/
│   ├── Socios/
│   ├── Turnos/
│   ├── Rutinas/
│   ├── Gimnasio/
│   ├── Seguridad/
│   └── IA/
├── Validators/
│   └── (FluentValidation validators)
├── Migrations/
│   └── (EF Core migrations)
├── Data/
│   └── ApplicationDbContext.cs
├── Middleware/
│   └── TenantResolverMiddleware.cs
├── Filters/
│   └── PermissionAuthorizationFilter.cs
└── Program.cs
```

#### Responsabilidades por capa

**Controllers:**

- Exponen endpoints HTTP (API REST)
- Validan DTOs (con FluentValidation)
- Aplican atributos de autorización
- Devuelven respuestas HTTP estandarizadas
- **No contienen lógica de negocio**

**Services:**

- Contienen toda la lógica de negocio
- Aplican reglas del dominio
- Validan datos de negocio
- Filtran por `GymId` en operaciones multi-tenant
- Invocan otros servicios si es necesario
- Acceden a datos via EF Core

**Models:**

- Entidades del dominio (POCOs)
- Mapean a tablas de SQL Server
- Incluyen `GymId` cuando aplica
- Relaciones navegacionales

**DTOs:**

- Request/Response models
- Separados por módulo funcional
- Validados con FluentValidation

**Validators:**

- Validaciones de entrada (DTOs)
- FluentValidation

**Migrations:**

- Migraciones de EF Core
- Control de versiones de esquema

#### Middleware Pipeline

```csharp
app.UseCors();
app.UseAuthentication(); // JWT Bearer
app.UseMiddleware<TenantResolverMiddleware>(); // GymId en contexto
app.UseAuthorization(); // RBAC dinámico
app.UseExceptionHandler(); // Global error handling
app.MapControllers();
```

### 3. Base de Datos (SQL Server)

#### Estrategia Multi-tenant

- **Una única base de datos** para todos los tenants (gimnasios)
- Discriminador: `GymId` (int, FK a Gyms)
- Query Filters de EF Core para aislamiento automático
- Todas las entidades del portal incluyen `GymId`

#### Grupos de tablas

**SaaS / Plataforma:**

- `Gyms` (Tenants)
- `Leads` (Solicitudes de demo - sin GymId)
- `ContactMessages` (Mensajes de contacto - sin GymId)

**Seguridad:**

- `Usuarios` (con GymId)
- `Grupos` (con GymId)
- `Permisos` (catálogo global, sin GymId)
- `UsuarioGrupo` (N:N)
- `GrupoPermiso` (N:N)
- `RefreshTokens`
- `PasswordResetTokens`

**Módulos del Portal:**

- `Socios` (con GymId)
- `Turnos` (con GymId)
- `Rutinas` (con GymId)
- `RutinaEjercicios` (con GymId)
- `Maquinas` (con GymId)
- `Ejercicios` (con GymId)
- `Equipamiento` (con GymId)
- `Horarios` (con GymId)
- `Entrenadores` (con GymId)
- `Configuraciones` (con GymId)

---

## Flujos de datos

### Flujo 1: Solicitud de Demo (Sitio público)

```
Usuario → Formulario "Solicitar Demo"
  → POST /api/public/lead/demo-request
    → PublicController
      → PublicService.CreateLead()
        → Guarda Lead en BD
        → EmailService.SendLeadNotification()
  ← 200 OK
```

### Flujo 2: Login al Portal

```
Usuario → Pantalla de acceso
  → GET /api/gyms/search?query=nombre
    ← Lista de gimnasios
  → Selecciona gimnasio (gymId)
  → Ingresa usuario y contraseña
  → POST /api/auth/login { gymId, username, password }
    → AuthController.Login()
      → AuthService.Authenticate()
        → Valida credenciales (hash bcrypt)
        → Genera Access Token JWT (15 min)
          Claims: sub (userId), email, gymId
        → Genera Refresh Token (random secure)
        → Guarda RefreshToken en BD
        → Devuelve Access Token + cookie HttpOnly con Refresh Token
  ← 200 OK { accessToken, user }
  → Frontend guarda Access Token en memoria
  → Redirige a Dashboard
```

### Flujo 3: Request autenticado al Portal

```
Frontend → Solicitud a endpoint protegido
  → Headers: Authorization: Bearer <accessToken>
  → POST /api/socios
    → Middleware Authentication: valida JWT
    → Middleware TenantResolver: extrae gymId del claim
    → Controller: aplica autorización por permisos
      → PermissionService.UserHasPermission(userId, "SOCIOS_CREAR", gymId)
        → Consulta grupos del usuario (filtrado por gymId)
        → Consulta permisos de los grupos
        → Retorna true/false
      → Si autorizado:
        → SocioService.Create(dto, gymId)
          → Aplica lógica de negocio
          → Guarda en BD con GymId
        ← 201 Created
      → Si no autorizado:
        ← 403 Forbidden
```

### Flujo 4: Token expirado y refresh

```
Frontend → Request con Access Token expirado
  → POST /api/socios
    ← 401 Unauthorized
  → Interceptor detecta 401
  → POST /api/auth/refresh
    → Cookie: refreshToken (HttpOnly)
    → AuthController.Refresh()
      → AuthService.RefreshAccessToken()
        → Valida Refresh Token en BD
        → Verifica no expirado, no revocado
        → Genera nuevo Access Token
        → Rota Refresh Token (nuevo token, invalida el anterior)
    ← 200 OK { accessToken }
  → Reintenta request original con nuevo token
  → POST /api/socios (con nuevo token)
    ← 201 Created
```

### Flujo 5: Recuperar contraseña

```
Usuario → Olvidé mi contraseña
  → POST /api/auth/forgot-password { email, gymId }
    → AuthService.GeneratePasswordResetToken()
      → Busca usuario por email y gymId
      → Genera token seguro (random 32 bytes)
      → Calcula hash del token (SHA256)
      → Guarda PasswordResetToken en BD (hash, expira en 30 min)
      → EmailService.SendPasswordResetEmail(email, token)
    ← 200 OK
  → Usuario recibe email con link
  → Clic en link → Pantalla reset
  → POST /api/auth/reset-password { token, newPassword }
    → AuthService.ResetPassword()
      → Calcula hash del token recibido
      → Busca token en BD
      → Valida: existe, no usado, no expirado
      → Hashea nueva contraseña (bcrypt)
      → Actualiza contraseña del usuario
      → Marca token como usado
      → Revoca todos los RefreshTokens del usuario
    ← 200 OK
```

---

## Patrones y principios

### Principios de diseño

1. **Separation of Concerns (SoC)**

   - Controllers: solo exposición HTTP
   - Services: solo lógica de negocio
   - Models: solo persistencia

2. **Dependency Injection (DI)**

   - Todos los servicios registrados en contenedor DI
   - Inyección de dependencias via constructor

3. **Single Responsibility Principle (SRP)**

   - Cada clase tiene una única responsabilidad
   - Services específicos por módulo

4. **Don't Repeat Yourself (DRY)**

   - Lógica común en servicios base
   - Filtros y middleware reutilizables

5. **Seguridad por diseño**
   - Autenticación obligatoria en portal
   - Autorización granular por permisos
   - Aislamiento multi-tenant automático
   - Tokens con expiración
   - Passwords hasheados
   - HTTPS obligatorio

### Patrones de diseño aplicados

#### 1. Repository Pattern (implícito en EF Core)

- `DbContext` actúa como Unit of Work
- `DbSet<T>` actúa como Repository
- No se implementan repositorios adicionales por simplicidad académica

#### 2. Service Layer Pattern

- Servicios encapsulan lógica de negocio
- Controllers delgados, servicios robustos

#### 3. DTO Pattern

- Separación entre entidades de dominio y contratos API
- Validación en DTOs, no en Models

#### 4. Middleware Pattern

- Pipeline de procesamiento de requests
- TenantResolverMiddleware para multi-tenancy

#### 5. Strategy Pattern (IEmailService)

- Abstracción para envío de emails
- Permite mock en desarrollo, SMTP en producción

#### 6. Filter Pattern (Authorization)

- Autorización dinámica por permisos
- Filtro personalizado que consulta BD

---

## Seguridad

### Autenticación

**JWT (Access Token):**

- Algoritmo: HS256 (HMAC SHA-256)
- Duración: 15 minutos
- Claims mínimos: sub, email/username, gymId
- No incluye permisos (se consultan en cada request)
- Se envía en header: `Authorization: Bearer <token>`

**Refresh Token:**

- Duración: 7 días
- Almacenado en cookie HttpOnly, Secure, SameSite=Strict
- Persiste en BD con hash
- Rotación en cada refresh
- Revocable (logout, cambio contraseña)

### Autorización

**RBAC por grupos:**

- Usuarios pertenecen a grupos
- Grupos tienen permisos
- Autorización por acumulación de permisos de grupos
- Aislamiento por GymId

**Permisos:**

- Granulares (SOCIOS_VER, SOCIOS_CREAR, etc.)
- Consultados en cada request autenticado
- No cacheados (cambios inmediatos)

### Multi-tenancy

**Aislamiento por GymId:**

- Query Filters en EF Core: `.HasQueryFilter(e => e.GymId == TenantId)`
- Middleware extrae GymId del JWT claim
- Contexto HTTP almacena GymId actual
- Services filtran por GymId automáticamente

### Protección de datos

- Contraseñas: bcrypt o Argon2
- Tokens: SHA256 hash en BD
- HTTPS obligatorio
- CORS configurado para dominio frontend

---

## Escalabilidad y rendimiento

### Consideraciones actuales (MVP)

- Concurrencia: 75 usuarios concurrentes
- Operaciones críticas < 2 segundos
- Base de datos única (escalabilidad vertical)

### Preparación para futuro

- Arquitectura permite separar frontend/backend en servidores distintos
- API REST stateless (escalabilidad horizontal)
- Refresh Tokens en BD permiten invalidación distribuida
- GymId permite migración a BD separadas por tenant

---

## Tecnologías y dependencias

### Backend (.NET 8)

- **Framework:** ASP.NET Core 8
- **ORM:** Entity Framework Core 8
- **Autenticación:** Microsoft.AspNetCore.Authentication.JwtBearer
- **Validación:** FluentValidation
- **Password hashing:** BCrypt.Net o Microsoft.AspNetCore.Identity
- **Email:** Abstracto (IEmailService) → MailKit/MimeKit
- **Logging:** Serilog o ILogger de .NET

### Frontend (React)

- **Framework:** React 18+
- **Routing:** React Router v6
- **State Management:** Context API + Hooks (o Zustand/Redux según complejidad)
- **HTTP Client:** Axios
- **UI Library:** Material-UI (MUI) o TailwindCSS + Headless UI
- **Formularios:** React Hook Form + Yup/Zod
- **Autenticación:** Custom hook para manejar tokens

### Base de Datos

- **Motor:** Microsoft SQL Server 2019+
- **Versión EF Core:** 8.x

---

## Decisiones técnicas clave

### ¿Por qué JWT + Refresh Token y no solo JWT?

- JWT cortos (15 min) minimizan riesgo de tokens robados
- Refresh Token permite renovación sin relogin constante
- Refresh Token revocable permite logout inmediato y control de sesiones

### ¿Por qué no incluir permisos en JWT?

- Cambios en permisos deben aplicar de inmediato
- JWT no revocable → permisos quedan cacheados hasta expiración
- Solución: consultar permisos en cada request (pequeño overhead aceptable)

### ¿Por qué base de datos única y no una BD por tenant?

- Simplicidad académica y operativa
- Menor costo y complejidad de mantenimiento
- 75 usuarios concurrentes no justifican separación
- GymId permite migración futura si es necesario

### ¿Por qué no usar repositorios adicionales sobre EF Core?

- EF Core ya implementa Repository y Unit of Work
- Capa adicional agrega complejidad sin beneficio claro en este alcance
- Si se requiere abstracción de datos, se puede agregar después

### ¿Por qué contenido estático en frontend para Blog/Testimonios?

- No se requiere ABM de contenido en esta etapa
- Reduce complejidad del backend
- Facilita SEO y performance
- Puede evolucionar a CMS en futuras versiones

---

## Próximos pasos

Documentación pendiente:

1. ✅ Arquitectura general
2. ⏳ Estructura detallada de carpetas y archivos
3. ⏳ Modelo de datos completo con diagrama ER
4. ⏳ Especificación de endpoints (contratos API)
5. ⏳ Casos de uso detallados
6. ⏳ Plan de implementación por etapas

---

**Última actualización:** 01/01/2026
