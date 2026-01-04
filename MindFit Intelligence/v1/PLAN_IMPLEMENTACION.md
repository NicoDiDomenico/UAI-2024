# Plan de Implementación - MindFit Intelligence

## Índice

1. [Estrategia general](#estrategia-general)
2. [Fase 0: Preparación](#fase-0-preparación)
3. [Fase 1: Infraestructura y Autenticación](#fase-1-infraestructura-y-autenticación)
4. [Fase 2: Módulo Seguridad](#fase-2-módulo-seguridad)
5. [Fase 3: Módulo Socios](#fase-3-módulo-socios)
6. [Fase 4: Módulo Turnos](#fase-4-módulo-turnos)
7. [Fase 5: Módulo Rutinas](#fase-5-módulo-rutinas)
8. [Fase 6: Módulo Gimnasio](#fase-6-módulo-gimnasio)
9. [Fase 7: Módulo IA (Mock)](#fase-7-módulo-ia-mock)
10. [Fase 8: Sitio Público](#fase-8-sitio-público)
11. [Fase 9: Testing y Refinamiento](#fase-9-testing-y-refinamiento)
12. [Fase 10: Deployment](#fase-10-deployment)
13. [Cronograma estimado](#cronograma-estimado)

---

## Estrategia general

### Principios de implementación

1. **Incremental**: Entregar valor en cada fase
2. **Vertical slicing**: Implementar features completas (Backend + Frontend + BD)
3. **Testing continuo**: Pruebas en cada fase antes de avanzar
4. **Priorización**: Módulos críticos primero (Autenticación → Seguridad → Socios → Turnos)
5. **Documentación paralela**: Actualizar AI_WORKLOG.md en cada fase

### Metodología

- **Iteraciones cortas**: 1-2 semanas por fase
- **Demo al final de cada fase**: Validar funcionalidad
- **Refactoring continuo**: Mejorar código sin cambiar funcionalidad
- **Git branching**: `main` (estable), `develop`, `feature/nombre-feature`

---

## Fase 0: Preparación

**Duración estimada:** 3-5 días

### Objetivos

- Configurar entorno de desarrollo
- Crear estructura de proyectos
- Configurar herramientas

### Tareas Backend

#### 0.1. Crear proyecto ASP.NET Core

```bash
dotnet new webapi -n MindFit.Api -f net8.0
cd MindFit.Api
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package FluentValidation.AspNetCore
dotnet add package BCrypt.Net-Next
```

#### 0.2. Crear estructura de carpetas

```
MindFit.Api/
├── Controllers/
├── Services/
│   └── Interfaces/
├── Models/
├── DTOs/
├── Validators/
├── Data/
├── Middleware/
├── Filters/
├── Extensions/
├── Helpers/
└── Constants/
```

#### 0.3. Configurar `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MindFitDB;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "JwtSettings": {
    "SecretKey": "your-super-secret-key-min-32-chars",
    "Issuer": "MindFitAPI",
    "Audience": "MindFitWeb",
    "AccessTokenExpirationMinutes": 15,
    "RefreshTokenExpirationDays": 7
  },
  "SmtpSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "EnableSsl": true,
    "Username": "",
    "Password": ""
  }
}
```

### Tareas Frontend

#### 0.4. Crear proyecto React

```bash
npm create vite@latest mindfit-web -- --template react
cd mindfit-web
npm install
npm install react-router-dom axios
npm install @mui/material @emotion/react @emotion/styled
npm install react-hook-form yup
```

#### 0.5. Crear estructura de carpetas

```
src/
├── routes/
├── layouts/
├── pages/
├── components/
├── context/
├── hooks/
├── services/
├── utils/
└── assets/
```

#### 0.6. Configurar `.env`

```
VITE_API_URL=https://localhost:5001/api
```

### Tareas Base de Datos

#### 0.7. Crear base de datos

```sql
CREATE DATABASE MindFitDB;
GO
```

### Tareas Git

#### 0.8. Inicializar repositorio

```bash
git init
git add .
git commit -m "Initial commit - Project structure"
```

### Entregables Fase 0

- [x] Proyecto Backend configurado
- [x] Proyecto Frontend configurado
- [x] Base de datos creada
- [x] Repositorio Git inicializado
- [x] Estructura de carpetas completa

---

## Fase 1: Infraestructura y Autenticación

**Duración estimada:** 1-2 semanas  
**Prioridad:** CRÍTICA

### Objetivos

- Implementar modelo de datos base (Gym, Usuario, Grupo, Permiso)
- Implementar autenticación completa (JWT + Refresh Token)
- Implementar RBAC básico
- Configurar multi-tenancy

### Tareas Backend

#### 1.1. Crear modelos base

- [x] `Gym.cs`
- [x] `Usuario.cs`
- [x] `Grupo.cs`
- [x] `Permiso.cs`
- [x] `UsuarioGrupo.cs`
- [x] `GrupoPermiso.cs`
- [x] `RefreshToken.cs`
- [x] `PasswordResetToken.cs`

#### 1.2. Configurar DbContext

```csharp
public class ApplicationDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<Gym> Gyms { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Grupo> Grupos { get; set; }
    public DbSet<Permiso> Permisos { get; set; }
    // ...

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuraciones de entidades
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Query Filters para multi-tenancy
        var tenantId = GetCurrentTenantId();
        if (tenantId.HasValue)
        {
            modelBuilder.Entity<Usuario>().HasQueryFilter(e => e.GymId == tenantId);
            modelBuilder.Entity<Grupo>().HasQueryFilter(e => e.GymId == tenantId);
            // ... aplicar a todas las entidades con GymId
        }
    }

    private int? GetCurrentTenantId()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context?.Items.ContainsKey("TenantId") == true)
        {
            return (int)context.Items["TenantId"];
        }
        return null;
    }
}
```

#### 1.3. Crear migración inicial

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

#### 1.4. Seeder de permisos y grupos

```csharp
public class DatabaseSeeder
{
    public static void SeedPermissions(ApplicationDbContext context)
    {
        if (context.Permisos.Any()) return;

        var permisos = new List<Permiso>
        {
            new() { Codigo = "SOCIOS_VER", Nombre = "Ver socios", Modulo = "Socios" },
            new() { Codigo = "SOCIOS_CREAR", Nombre = "Crear socios", Modulo = "Socios" },
            // ... todos los permisos
        };

        context.Permisos.AddRange(permisos);
        context.SaveChanges();
    }

    public static void SeedGymWithGroups(ApplicationDbContext context, int gymId)
    {
        var grupos = new List<Grupo>
        {
            new() { GymId = gymId, Nombre = "ADMIN_GYM", EsSistema = true },
            new() { GymId = gymId, Nombre = "ADMIN_SEGURIDAD", EsSistema = true },
            // ...
        };

        context.Grupos.AddRange(grupos);
        context.SaveChanges();

        // Asignar permisos a grupos
        // ...
    }
}
```

#### 1.5. Implementar servicios de autenticación

**ITokenService:**

```csharp
public interface ITokenService
{
    string GenerateAccessToken(Usuario usuario, int gymId);
    string GenerateRefreshToken();
    ClaimsPrincipal ValidateToken(string token);
}
```

**IAuthService:**

```csharp
public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
    Task<RefreshTokenResponseDto> RefreshTokenAsync(string refreshToken);
    Task LogoutAsync(string refreshToken);
    Task ForgotPasswordAsync(ForgotPasswordRequestDto dto);
    Task ResetPasswordAsync(ResetPasswordRequestDto dto);
    Task ChangePasswordAsync(int userId, ChangePasswordRequestDto dto);
}
```

#### 1.6. Implementar AuthController

```csharp
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var result = await _authService.LoginAsync(dto);

        // Enviar Refresh Token en cookie HttpOnly
        Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });

        return Ok(new { accessToken = result.AccessToken, user = result.User });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken))
            return Unauthorized();

        var result = await _authService.RefreshTokenAsync(refreshToken);

        // Rotar Refresh Token
        Response.Cookies.Append("refreshToken", result.NewRefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });

        return Ok(new { accessToken = result.AccessToken });
    }

    // ... otros endpoints
}
```

#### 1.7. Configurar JWT en `Program.cs`

```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
        };
    });

builder.Services.AddAuthorization();
```

#### 1.8. Implementar TenantResolverMiddleware

```csharp
public class TenantResolverMiddleware
{
    private readonly RequestDelegate _next;

    public TenantResolverMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var gymIdClaim = context.User.FindFirst("gymId")?.Value;
            if (!string.IsNullOrEmpty(gymIdClaim) && int.TryParse(gymIdClaim, out var gymId))
            {
                context.Items["TenantId"] = gymId;
            }
        }

        await _next(context);
    }
}
```

#### 1.9. Implementar PermissionService

```csharp
public interface IPermissionService
{
    Task<List<string>> GetUserPermissionsAsync(int userId, int gymId);
    Task<bool> UserHasPermissionAsync(int userId, string permission, int gymId);
}
```

#### 1.10. Implementar RequirePermissionAttribute

```csharp
public class RequirePermissionAttribute : TypeFilterAttribute
{
    public RequirePermissionAttribute(string permission)
        : base(typeof(PermissionAuthorizationFilter))
    {
        Arguments = new object[] { permission };
    }
}

public class PermissionAuthorizationFilter : IAsyncAuthorizationFilter
{
    private readonly string _permission;
    private readonly IPermissionService _permissionService;

    public PermissionAuthorizationFilter(string permission, IPermissionService permissionService)
    {
        _permission = permission;
        _permissionService = permissionService;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var userIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var gymIdClaim = context.HttpContext.User.FindFirst("gymId")?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(gymIdClaim))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var hasPermission = await _permissionService.UserHasPermissionAsync(
            int.Parse(userIdClaim), _permission, int.Parse(gymIdClaim));

        if (!hasPermission)
        {
            context.Result = new ForbidResult();
        }
    }
}
```

### Tareas Frontend

#### 1.11. Crear AuthContext

```jsx
import { createContext, useState, useContext } from "react";
import authService from "../services/authService";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [accessToken, setAccessToken] = useState(null); // En memoria

  const login = async (gymId, username, password) => {
    const { accessToken, user } = await authService.login(
      gymId,
      username,
      password
    );
    setAccessToken(accessToken);
    setUser(user);
  };

  const logout = async () => {
    await authService.logout();
    setAccessToken(null);
    setUser(null);
  };

  const refreshAccessToken = async () => {
    const { accessToken } = await authService.refresh();
    setAccessToken(accessToken);
    return accessToken;
  };

  return (
    <AuthContext.Provider
      value={{ user, accessToken, login, logout, refreshAccessToken }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
```

#### 1.12. Configurar Axios con interceptores

```jsx
import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  withCredentials: true, // Para cookies
});

let isRefreshing = false;
let refreshSubscribers = [];

const subscribeTokenRefresh = (callback) => {
  refreshSubscribers.push(callback);
};

const onRefreshed = (token) => {
  refreshSubscribers.forEach((callback) => callback(token));
  refreshSubscribers = [];
};

api.interceptors.request.use((config) => {
  const token = getAccessToken(); // Obtener de contexto
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

api.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    if (error.response?.status === 401 && !originalRequest._retry) {
      if (isRefreshing) {
        // Esperar a que el refresh termine
        return new Promise((resolve) => {
          subscribeTokenRefresh((token) => {
            originalRequest.headers.Authorization = `Bearer ${token}`;
            resolve(api(originalRequest));
          });
        });
      }

      originalRequest._retry = true;
      isRefreshing = true;

      try {
        const { data } = await axios.post(
          "/api/auth/refresh",
          {},
          { withCredentials: true }
        );
        const newToken = data.accessToken;
        setAccessToken(newToken); // Actualizar contexto
        isRefreshing = false;
        onRefreshed(newToken);
        originalRequest.headers.Authorization = `Bearer ${newToken}`;
        return api(originalRequest);
      } catch (refreshError) {
        isRefreshing = false;
        logout(); // Logout y redirigir a login
        window.location.href = "/login";
        return Promise.reject(refreshError);
      }
    }

    return Promise.reject(error);
  }
);

export default api;
```

#### 1.13. Crear LoginPage

```jsx
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";

export const LoginPage = () => {
  const [gymId, setGymId] = useState("");
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const { login } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await login(gymId, username, password);
      navigate("/portal/dashboard");
    } catch (error) {
      alert("Error al iniciar sesión");
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      {/* Selector de gimnasio */}
      <input
        value={gymId}
        onChange={(e) => setGymId(e.target.value)}
        placeholder="Gimnasio"
      />

      {/* Usuario */}
      <input
        value={username}
        onChange={(e) => setUsername(e.target.value)}
        placeholder="Usuario"
      />

      {/* Contraseña */}
      <input
        type="password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        placeholder="Contraseña"
      />

      <button type="submit">Iniciar Sesión</button>
    </form>
  );
};
```

### Pruebas Fase 1

- [x] Login exitoso con credenciales válidas
- [x] Login fallido con credenciales inválidas
- [x] Access Token expira en 15 minutos
- [x] Refresh Token renueva Access Token automáticamente
- [x] Logout revoca Refresh Token
- [x] Aislamiento multi-tenant (no ver datos de otros gimnasios)
- [x] Cambio de contraseña
- [x] Recuperar contraseña (email mock)

### Entregables Fase 1

- [x] Autenticación completa (Backend + Frontend)
- [x] Multi-tenancy configurado
- [x] RBAC básico funcionando
- [x] Usuarios pueden hacer login y navegar al portal

---

## Fase 2: Módulo Seguridad

**Duración estimada:** 1 semana  
**Prioridad:** ALTA

### Objetivos

- Gestión de usuarios (CRUD)
- Gestión de grupos (CRUD)
- Asignación de usuarios a grupos
- Asignación de permisos a grupos

### Tareas Backend

#### 2.1. Implementar UsuarioService

- [x] Listar usuarios (con paginación)
- [x] Obtener usuario por ID
- [x] Crear usuario
- [x] Actualizar usuario
- [x] Eliminar usuario (desactivar)
- [x] Resetear contraseña

#### 2.2. Implementar GrupoService

- [x] Listar grupos
- [x] Obtener grupo con permisos
- [x] Crear grupo
- [x] Actualizar grupo (incluye permisos)
- [x] Eliminar grupo (validar que no tenga usuarios)

#### 2.3. Implementar Controllers

- [x] `UsuariosController`
- [x] `GruposController`
- [x] `PermisosController` (solo lectura)

### Tareas Frontend

#### 2.4. Implementar páginas

- [x] `/portal/seguridad/usuarios` - Listado
- [x] `/portal/seguridad/usuarios/crear` - Formulario
- [x] `/portal/seguridad/usuarios/:id/editar` - Formulario
- [x] `/portal/seguridad/grupos` - Listado
- [x] `/portal/seguridad/grupos/crear` - Formulario con selector de permisos
- [x] `/portal/seguridad/grupos/:id/editar` - Formulario

### Pruebas Fase 2

- [x] ADMIN_SEGURIDAD puede gestionar usuarios y grupos
- [x] Otros roles NO pueden acceder a Seguridad
- [x] Crear usuario con múltiples grupos
- [x] Modificar permisos de grupo afecta a usuarios inmediatamente
- [x] No se puede eliminar grupo con usuarios asignados
- [x] No se puede eliminar grupo del sistema

### Entregables Fase 2

- [x] Módulo Seguridad completo (Backend + Frontend)
- [x] Gestión de usuarios y grupos funcional
- [x] Permisos dinámicos funcionando

---

## Fase 3: Módulo Socios

**Duración estimada:** 1 semana  
**Prioridad:** ALTA

### Objetivos

- Gestión completa de socios
- Validaciones de negocio (cuota al día, etc.)

### Tareas Backend

#### 3.1. Crear modelos

- [x] `Socio.cs`

#### 3.2. Migración de BD

```bash
dotnet ef migrations add AddSocios
dotnet ef database update
```

#### 3.3. Implementar SocioService

- [x] Listar socios (paginación, filtros)
- [x] Obtener socio por ID
- [x] Crear socio (auto-generar número de socio)
- [x] Actualizar socio
- [x] Eliminar socio (lógico)
- [x] Recuperar socio eliminado
- [x] Validar cuota al día

#### 3.4. Implementar SociosController

- [x] Todos los endpoints del módulo

#### 3.5. Implementar validadores FluentValidation

- [x] `SocioRequestValidator`
- [x] `SocioUpdateValidator`

### Tareas Frontend

#### 3.6. Implementar páginas

- [x] `/portal/socios` - Listado con filtros y paginación
- [x] `/portal/socios/:id` - Detalle del socio
- [x] `/portal/socios/crear` - Formulario alta
- [x] `/portal/socios/:id/editar` - Formulario edición

#### 3.7. Componentes

- [x] `SocioTable` - Tabla con acciones
- [x] `SocioForm` - Formulario reutilizable
- [x] `SocioCard` - Card para vista detalle

### Pruebas Fase 3

- [x] ADMIN_GYM y ASISTENTE pueden crear/editar socios
- [x] ENTRENADOR solo puede ver socios
- [x] SOCIO no puede acceder a gestión de socios
- [x] Validar email único por gimnasio
- [x] Validar número de socio único por gimnasio
- [x] Eliminación lógica funciona
- [x] Recuperación de socio funciona

### Entregables Fase 3

- [x] Módulo Socios completo (Backend + Frontend)
- [x] CRUD funcional con permisos
- [x] Validaciones de negocio

---

## Fase 4: Módulo Turnos

**Duración estimada:** 1-2 semanas  
**Prioridad:** ALTA

### Objetivos

- Gestión de turnos con reservas
- Validación de cupos
- Notificaciones por email

### Tareas Backend

#### 4.1. Crear modelos

- [x] `Turno.cs`
- [x] `Horario.cs` (para validar horarios de atención)

#### 4.2. Migración de BD

```bash
dotnet ef migrations add AddTurnos
dotnet ef database update
```

#### 4.3. Implementar TurnoService

- [x] Listar turnos (filtros por fecha, socio, estado)
- [x] Obtener turnos disponibles (slots libres)
- [x] Reservar turno (validaciones)
- [x] Cancelar turno
- [x] Enviar notificación de confirmación (email)
- [x] Enviar notificación de cancelación (email)

**Validaciones al reservar:**

- Socio activo
- Socio con cuota al día
- Horario dentro de los horarios de atención
- Cupos disponibles
- No tener otro turno en el mismo horario

#### 4.4. Implementar TurnosController

#### 4.5. Implementar HorarioService

- [x] Listar horarios del gimnasio
- [x] Actualizar horarios

### Tareas Frontend

#### 4.6. Implementar páginas

- [x] `/portal/turnos` - Listado de turnos
- [x] `/portal/turnos/reservar` - Formulario de reserva
- [x] `/portal/turnos/calendario` - Vista de calendario

#### 4.7. Componentes

- [x] `TurnoTable` - Tabla de turnos
- [x] `TurnoForm` - Formulario de reserva
- [x] `Calendar` - Componente calendario (FullCalendar o similar)

### Pruebas Fase 4

- [x] ASISTENTE puede reservar turnos
- [x] SOCIO puede reservar sus propios turnos
- [x] No se puede reservar si cuota vencida
- [x] No se puede reservar si no hay cupos
- [x] Email de confirmación se envía (mock)
- [x] Cancelación de turno libera cupo
- [x] Email de cancelación se envía (mock)

### Entregables Fase 4

- [x] Módulo Turnos completo (Backend + Frontend)
- [x] Reserva de turnos funcional
- [x] Validaciones de negocio implementadas
- [x] Notificaciones por email (mock)

---

## Fase 5: Módulo Rutinas

**Duración estimada:** 1-2 semanas  
**Prioridad:** MEDIA

### Objetivos

- Gestión de rutinas personalizadas
- Asignación de ejercicios a rutinas
- Historial de rutinas por socio

### Tareas Backend

#### 5.1. Crear modelos

- [x] `Rutina.cs`
- [x] `RutinaEjercicio.cs`
- [x] `Ejercicio.cs`
- [x] `Maquina.cs` (para asociar a ejercicios)

#### 5.2. Migración de BD

```bash
dotnet ef migrations add AddRutinas
dotnet ef database update
```

#### 5.3. Implementar servicios

- [x] `RutinaService`
  - Listar rutinas (filtros por socio, activa)
  - Obtener rutina con ejercicios
  - Crear rutina con ejercicios
  - Actualizar rutina
  - Eliminar rutina
- [x] `EjercicioService` (para catálogo de ejercicios)

#### 5.4. Implementar Controllers

- [x] `RutinasController`
- [x] `EjerciciosController` (parte de Gimnasio)

### Tareas Frontend

#### 5.5. Implementar páginas

- [x] `/portal/rutinas` - Listado de rutinas
- [x] `/portal/rutinas/:id` - Detalle de rutina con ejercicios
- [x] `/portal/rutinas/crear` - Formulario de creación
- [x] `/portal/rutinas/:id/editar` - Formulario de edición

#### 5.6. Componentes

- [x] `RutinaTable` - Tabla de rutinas
- [x] `RutinaForm` - Formulario con selector de ejercicios
- [x] `EjercicioSelector` - Selector/buscador de ejercicios

### Pruebas Fase 5

- [x] ENTRENADOR puede crear/editar rutinas
- [x] ADMIN_GYM puede crear/editar rutinas
- [x] SOCIO solo puede ver sus rutinas (no editar)
- [x] Rutina con múltiples ejercicios
- [x] Orden de ejercicios se respeta
- [x] Historial de rutinas por socio

### Entregables Fase 5

- [x] Módulo Rutinas completo (Backend + Frontend)
- [x] Asignación de rutinas a socios funcional
- [x] Catálogo de ejercicios

---

## Fase 6: Módulo Gimnasio

**Duración estimada:** 1 semana  
**Prioridad:** MEDIA-BAJA

### Objetivos

- Gestión de máquinas, equipamiento
- Gestión de entrenadores
- Configuraciones del gimnasio

### Tareas Backend

#### 6.1. Crear modelos

- [x] `Maquina.cs` (si no está creado)
- [x] `Equipamiento.cs`
- [x] `Entrenador.cs`
- [x] `Configuracion.cs`

#### 6.2. Migración de BD

#### 6.3. Implementar servicios

- [x] `MaquinaService`
- [x] `EquipamientoService`
- [x] `EntrenadorService`
- [x] `ConfiguracionService`

#### 6.4. Implementar Controllers

- [x] `MaquinasController`
- [x] `EquipamientoController`
- [x] `EntrenadoresController`
- [x] `HorariosController`
- [x] `ConfiguracionesController`

### Tareas Frontend

#### 6.5. Implementar páginas

- [x] `/portal/gimnasio/maquinas` - CRUD máquinas
- [x] `/portal/gimnasio/equipamiento` - CRUD equipamiento
- [x] `/portal/gimnasio/entrenadores` - CRUD entrenadores
- [x] `/portal/gimnasio/horarios` - Gestión de horarios
- [x] `/portal/gimnasio/configuraciones` - Configuraciones

### Pruebas Fase 6

- [x] ADMIN_GYM puede gestionar todo el módulo
- [x] Otros roles no pueden acceder
- [x] Máquinas asociadas a ejercicios
- [x] Entrenadores asociados a turnos

### Entregables Fase 6

- [x] Módulo Gimnasio completo (Backend + Frontend)
- [x] Gestión de recursos del gimnasio funcional

---

## Fase 7: Módulo IA (Mock)

**Duración estimada:** 3-5 días  
**Prioridad:** BAJA

### Objetivos

- Implementar endpoints de IA
- Respuestas mock (predefinidas)
- Preparar arquitectura para integración futura

### Tareas Backend

#### 7.1. Implementar IAService (mock)

```csharp
public class IAService : IIAService
{
    public async Task<string> GetChatResponseAsync(int socioId, string mensaje)
    {
        // Respuestas predefinidas según keywords
        if (mensaje.ToLower().Contains("pecho"))
        {
            return "Para pecho te recomiendo: Press de banca, Press inclinado, Fondos.";
        }

        return "Consulta recibida. Un entrenador te responderá pronto.";
    }

    public async Task<List<string>> GetRecommendationsAsync(int socioId)
    {
        // Mock: recomendaciones genéricas
        return new List<string>
        {
            "Mantén la constancia en tus entrenamientos",
            "Recuerda hidratarte adecuadamente",
            "Consulta con un entrenador para personalizar tu rutina"
        };
    }
}
```

#### 7.2. Implementar IAController

### Tareas Frontend

#### 7.3. Implementar páginas

- [x] `/portal/ia` - Chat asistente IA

#### 7.4. Componentes

- [x] `ChatAssistant` - Interfaz de chat

### Pruebas Fase 7

- [x] Usuarios con permiso IA_USAR pueden acceder
- [x] Respuestas mock funcionan
- [x] Interfaz de chat funcional

### Entregables Fase 7

- [x] Módulo IA mock funcional
- [x] Arquitectura preparada para integración futura con LLM

---

## Fase 8: Sitio Público

**Duración estimada:** 1 semana  
**Prioridad:** MEDIA

### Objetivos

- Landing page marketing
- Formularios de contacto y demo
- Endpoint de búsqueda de gimnasios

### Tareas Backend

#### 8.1. Crear modelos

- [x] `Lead.cs`
- [x] `ContactMessage.cs`

#### 8.2. Migración de BD

#### 8.3. Implementar PublicService

- [x] Crear lead (solicitud de demo)
- [x] Crear mensaje de contacto
- [x] Buscar gimnasios (para selector de login)

#### 8.4. Implementar PublicController

### Tareas Frontend

#### 8.5. Implementar páginas públicas

- [x] `/` - Home/Inicio
- [x] `/funcionalidades` - Funcionalidades del sistema
- [x] `/precios` - Planes y precios
- [x] `/testimonios` - Testimonios de clientes
- [x] `/blog` - Blog (contenido estático)
- [x] `/contacto` - Formulario de contacto
- [x] `/solicitar-demo` - Formulario solicitud de demo

#### 8.6. Componentes públicos

- [x] `Header` - Header con navegación
- [x] `Footer` - Footer
- [x] `Hero` - Sección hero
- [x] `Features` - Sección de funcionalidades
- [x] `Pricing` - Sección de precios
- [x] `Testimonials` - Testimonios
- [x] `ContactForm` - Formulario de contacto
- [x] `DemoRequestForm` - Formulario de demo

### Pruebas Fase 8

- [x] Sitio público accesible sin autenticación
- [x] Formularios capturan leads y mensajes
- [x] Búsqueda de gimnasios funciona para login
- [x] Responsive (mobile-first)

### Entregables Fase 8

- [x] Sitio público completo
- [x] Captura de leads funcional
- [x] Flujo de acceso al portal desde sitio público

---

## Fase 9: Testing y Refinamiento

**Duración estimada:** 1-2 semanas  
**Prioridad:** ALTA

### Objetivos

- Testing integral (unitario, integración, E2E)
- Corrección de bugs
- Optimización de performance
- Refinamiento de UX

### Tareas

#### 9.1. Testing Backend

- [ ] Tests unitarios de servicios (xUnit)
- [ ] Tests de integración de controllers (WebApplicationFactory)
- [ ] Tests de aislamiento multi-tenant
- [ ] Tests de autorización (permisos)

#### 9.2. Testing Frontend

- [ ] Tests unitarios de componentes (Vitest)
- [ ] Tests de integración de páginas
- [ ] Tests E2E (Playwright o Cypress)

#### 9.3. Performance

- [ ] Optimizar queries N+1 (Include, Select)
- [ ] Verificar índices de BD
- [ ] Medir tiempo de respuesta de endpoints críticos
- [ ] Optimizar bundle size del frontend

#### 9.4. UX/UI

- [ ] Validaciones en tiempo real en formularios
- [ ] Mensajes de error claros y útiles
- [ ] Feedback visual en acciones (loaders, toasts)
- [ ] Accesibilidad (ARIA, keyboard navigation)

#### 9.5. Seguridad

- [ ] Pruebas de penetración básicas (OWASP Top 10)
- [ ] Validación de aislamiento multi-tenant
- [ ] Revisar CORS y cookies
- [ ] Rate limiting en endpoints públicos

### Entregables Fase 9

- [ ] Suite de tests completa
- [ ] Bugs críticos resueltos
- [ ] Performance optimizada
- [ ] UX refinada

---

## Fase 10: Deployment

**Duración estimada:** 3-5 días  
**Prioridad:** ALTA

### Objetivos

- Desplegar aplicación en entorno de producción
- Configurar CI/CD (opcional)
- Documentación de deployment

### Tareas

#### 10.1. Backend

- [ ] Publicar en Azure App Service o IIS
- [ ] Configurar connection string de producción
- [ ] Configurar variables de entorno
- [ ] Habilitar HTTPS
- [ ] Configurar logging (Application Insights)

#### 10.2. Base de Datos

- [ ] Crear BD en Azure SQL Database o SQL Server on-premise
- [ ] Ejecutar migraciones en producción
- [ ] Ejecutar seeders (permisos, grupos base)
- [ ] Backup automático configurado

#### 10.3. Frontend

- [ ] Build de producción (`npm run build`)
- [ ] Desplegar en Azure Static Web Apps, Netlify o Vercel
- [ ] Configurar variables de entorno de producción
- [ ] Configurar dominio personalizado

#### 10.4. CI/CD (opcional)

- [ ] GitHub Actions para build automático
- [ ] Deploy automático en push a `main`

#### 10.5. Documentación

- [ ] README.md con instrucciones de instalación
- [ ] Documentación de API (Swagger)
- [ ] Manual de usuario (básico)

### Entregables Fase 10

- [ ] Aplicación desplegada en producción
- [ ] BD en producción con seeders
- [ ] Documentación de deployment

---

## Cronograma estimado

| Fase                                    | Duración    | Inicio    | Fin          |
| --------------------------------------- | ----------- | --------- | ------------ |
| Fase 0: Preparación                     | 3-5 días    | Semana 1  | Semana 1     |
| Fase 1: Infraestructura y Autenticación | 1-2 semanas | Semana 1  | Semana 2-3   |
| Fase 2: Módulo Seguridad                | 1 semana    | Semana 3  | Semana 4     |
| Fase 3: Módulo Socios                   | 1 semana    | Semana 4  | Semana 5     |
| Fase 4: Módulo Turnos                   | 1-2 semanas | Semana 5  | Semana 6-7   |
| Fase 5: Módulo Rutinas                  | 1-2 semanas | Semana 7  | Semana 8-9   |
| Fase 6: Módulo Gimnasio                 | 1 semana    | Semana 9  | Semana 10    |
| Fase 7: Módulo IA (Mock)                | 3-5 días    | Semana 10 | Semana 10    |
| Fase 8: Sitio Público                   | 1 semana    | Semana 11 | Semana 12    |
| Fase 9: Testing y Refinamiento          | 1-2 semanas | Semana 12 | Semana 13-14 |
| Fase 10: Deployment                     | 3-5 días    | Semana 14 | Semana 14    |

**Duración total estimada:** 14 semanas (3.5 meses)

**Nota:** Las semanas pueden superponerse según la disponibilidad del equipo.

---

## Hitos clave

### Hito 1: MVP Autenticación (Fin Fase 1)

- Login funcional
- Multi-tenancy operativo
- RBAC básico

### Hito 2: MVP Gestión Básica (Fin Fase 3)

- Módulo Seguridad completo
- Módulo Socios completo
- Administrador puede gestionar usuarios y socios

### Hito 3: MVP Portal Completo (Fin Fase 7)

- Todos los módulos del portal funcionales
- IA mock implementado
- Portal operativo end-to-end

### Hito 4: Sistema Completo (Fin Fase 8)

- Sitio público operativo
- Portal completo
- Captura de leads funcional

### Hito 5: Producción (Fin Fase 10)

- Sistema desplegado en producción
- Documentación completa
- Listo para usuarios finales

---

## Riesgos y mitigación

| Riesgo                            | Impacto | Probabilidad | Mitigación                              |
| --------------------------------- | ------- | ------------ | --------------------------------------- |
| Aislamiento multi-tenant fallido  | Crítico | Media        | Testing exhaustivo, auditoría de código |
| Performance pobre en listados     | Alto    | Media        | Paginación obligatoria, índices, caché  |
| Complejidad de permisos dinámicos | Medio   | Alta         | Diseño claro, documentación, tests      |
| Integración frontend-backend      | Medio   | Baja         | Contratos API claros (DTOs), Swagger    |
| Deployment fallido                | Alto    | Baja         | Pruebas en staging, documentación       |

---

## Criterios de aceptación

### Por fase

Cada fase debe cumplir:

- [ ] Código implementado según especificación
- [ ] Tests escritos y pasando (mínimo 70% coverage)
- [ ] Documentación actualizada (AI_WORKLOG.md)
- [ ] Demo funcional presentada
- [ ] Sin bugs críticos

### Para producción (Fase 10)

- [ ] Todos los módulos funcionando end-to-end
- [ ] Testing completo ejecutado
- [ ] Performance dentro de requisitos (turnos ≤ 2 seg)
- [ ] Seguridad validada (aislamiento, autenticación)
- [ ] Documentación completa (técnica y usuario)
- [ ] Sistema desplegado y accesible

---

## Próximos pasos después del MVP

### Mejoras futuras (fuera del alcance académico actual)

1. **Integración IA real**: GPT API para recomendaciones personalizadas
2. **Reportes y dashboards**: Analíticas de asistencia, socios activos, ingresos
3. **Pagos integrados**: MercadoPago, Stripe
4. **Notificaciones push**: Web push notifications
5. **App móvil**: React Native o Flutter
6. **Múltiples sedes**: Un gimnasio con varias sucursales
7. **Gestión de productos**: Venta de productos (suplementos, etc.)
8. **Integración con wearables**: Sincronizar datos de entrenamientos
9. **Sistema de reservas de clases grupales**: Spinning, yoga, etc.
10. **Chat en tiempo real**: Soporte al cliente

---

**Última actualización:** 01/01/2026
