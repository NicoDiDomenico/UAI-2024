# Decisiones Técnicas Justificadas - MindFit Intelligence

## Índice

1. [Multi-tenancy (SaaS)](#multi-tenancy-saas)
2. [Autenticación y Autorización](#autenticación-y-autorización)
3. [Arquitectura Backend](#arquitectura-backend)
4. [Frontend](#frontend)
5. [Base de Datos](#base-de-datos)
6. [Seguridad](#seguridad)
7. [Performance y Escalabilidad](#performance-y-escalabilidad)
8. [Servicios Transversales](#servicios-transversales)

---

## Multi-tenancy (SaaS)

### Decisión: Base de datos única con discriminador GymId

#### Opciones consideradas

| Opción                   | Ventajas                                                                                                       | Desventajas                                                                      |
| ------------------------ | -------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------- |
| **BD única con GymId** ✓ | • Simplicidad operativa<br>• Menor costo<br>• Facilita backups y migraciones<br>• Un solo código de aplicación | • Requiere aislamiento riguroso<br>• Riesgo de data leakage si mal implementado  |
| BD por tenant            | • Aislamiento total<br>• Personalización por cliente<br>• Cumplimiento regulatorio estricto                    | • Alta complejidad operativa<br>• Costos elevados<br>• Dificulta actualizaciones |
| Esquema por tenant       | • Balance entre aislamiento y simplicidad                                                                      | • Mayor complejidad que BD única<br>• Limitaciones en SQL Server                 |

#### Justificación

**Elegimos BD única con GymId porque:**

1. **Alcance académico**: Simplicidad operativa adecuada para un proyecto de esta naturaleza
2. **Escala esperada**: 75 usuarios concurrentes no justifican complejidad de BD separadas
3. **Costo-beneficio**: Menor overhead operativo y costos de infraestructura
4. **Facilidad de mantenimiento**: Migraciones, backups y actualizaciones centralizadas
5. **Protección suficiente**: EF Core Query Filters garantizan aislamiento automático

#### Implementación

**Query Filters en EF Core:**

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Aplicar filtro automático por GymId
    modelBuilder.Entity<Usuario>().HasQueryFilter(e => e.GymId == _tenantId);
    modelBuilder.Entity<Socio>().HasQueryFilter(e => e.GymId == _tenantId);
    // ... todas las entidades con GymId
}
```

**Middleware de resolución de tenant:**

```csharp
public class TenantResolverMiddleware
{
    public async Task InvokeAsync(HttpContext context)
    {
        var gymId = context.User.FindFirst("gymId")?.Value;
        if (!string.IsNullOrEmpty(gymId))
        {
            context.Items["TenantId"] = int.Parse(gymId);
        }
        await _next(context);
    }
}
```

#### Riesgos mitigados

- **Data leakage**: Query Filters automáticos + validaciones en servicios
- **Performance**: Índices compuestos en (GymId, ...)
- **Testing**: Suites de pruebas específicas para aislamiento multi-tenant

---

## Autenticación y Autorización

### Decisión: JWT (Access Token) + Refresh Token + RBAC dinámico

#### Opciones consideradas

| Opción                    | Ventajas                                                               | Desventajas                                                 |
| ------------------------- | ---------------------------------------------------------------------- | ----------------------------------------------------------- |
| **JWT + Refresh Token** ✓ | • Stateless<br>• Escalable<br>• Revocable (refresh)<br>• Tokens cortos | • Complejidad media<br>• Requiere gestión de refresh tokens |
| Solo JWT largo            | • Simplicidad                                                          | • No revocable<br>• Mayor riesgo de seguridad               |
| Sesiones en BD            | • Revocación inmediata<br>• Control total                              | • No escalable<br>• Stateful<br>• Overhead de BD            |
| OAuth2/OIDC               | • Estándar de industria<br>• Delegación                                | • Complejidad alta<br>• Overkill para alcance actual        |

#### Justificación

**Elegimos JWT + Refresh Token porque:**

1. **Balance seguridad-usabilidad**

   - Access Token corto (15 min) minimiza ventana de riesgo
   - Refresh Token largo (7 días) evita relogin frecuente

2. **Revocación selectiva**

   - Refresh Tokens persistidos permiten logout inmediato
   - Útil para cambio de contraseña, sesiones comprometidas

3. **Escalabilidad**

   - JWT stateless: no requiere consulta de sesión en cada request
   - Backend escalable horizontalmente

4. **Compatibilidad**
   - Estándar RFC 7519
   - Librerías maduras en .NET y React

#### Claims del Access Token (JWT)

```json
{
  "sub": "1", // UserId
  "email": "usuario@example.com",
  "gymId": "1", // TenantId
  "iat": 1704192000, // Issued at
  "exp": 1704192900 // Expiration (15 min)
}
```

**¿Por qué NO incluir permisos en JWT?**

- **Cambios de permisos**: Deben aplicarse inmediatamente, sin esperar expiración del token
- **Tamaño del token**: Permisos pueden ser numerosos (30+), aumentan tamaño del JWT
- **Seguridad**: Permisos centralizados en BD, no en cliente

**Consulta de permisos en cada request:**

```csharp
public async Task<bool> UserHasPermission(int userId, string permission, int gymId)
{
    var permisos = await _context.Usuarios
        .Where(u => u.Id == userId && u.GymId == gymId)
        .SelectMany(u => u.UsuarioGrupo)
        .SelectMany(ug => ug.Grupo.GrupoPermiso)
        .Select(gp => gp.Permiso.Codigo)
        .Distinct()
        .ToListAsync();

    return permisos.Contains(permission);
}
```

**Optimización**: Caché en memoria por request (HttpContext.Items)

---

### Decisión: Refresh Token en cookie HttpOnly

#### Opciones consideradas

| Opción                | Ventajas                                           | Desventajas                                                 |
| --------------------- | -------------------------------------------------- | ----------------------------------------------------------- |
| **Cookie HttpOnly** ✓ | • Protegido contra XSS<br>• Automático en requests | • Requiere SameSite/CORS<br>• Vulnerable a CSRF (mitigable) |
| localStorage          | • Fácil acceso desde JS                            | • Vulnerable a XSS<br>• Accesible por scripts maliciosos    |
| sessionStorage        | • Expira al cerrar pestaña                         | • Vulnerable a XSS<br>• Experiencia de usuario reducida     |

#### Justificación

**Elegimos Cookie HttpOnly porque:**

1. **Seguridad superior**: Inaccesible desde JavaScript, protege contra XSS
2. **CSRF mitigado**: SameSite=Strict + CORS configurado
3. **UX**: Token se envía automáticamente sin intervención del cliente
4. **Rotación**: Facilita implementación de rotación de tokens

**Configuración de cookie:**

```csharp
var cookieOptions = new CookieOptions
{
    HttpOnly = true,
    Secure = true,           // Solo HTTPS
    SameSite = SameSiteMode.Strict,
    Expires = DateTime.UtcNow.AddDays(7)
};
context.Response.Cookies.Append("refreshToken", token, cookieOptions);
```

---

### Decisión: RBAC por grupos con permisos dinámicos

#### Opciones consideradas

| Opción                         | Ventajas                                                         | Desventajas                                   |
| ------------------------------ | ---------------------------------------------------------------- | --------------------------------------------- |
| **RBAC por grupos** ✓          | • Flexibilidad<br>• Gestión centralizada<br>• Cambios inmediatos | • Consulta en cada request                    |
| Permisos directos              | • Simplicidad                                                    | • Difícil de gestionar<br>• Asignación manual |
| Roles fijos (ASP.NET Identity) | • Integración nativa                                             | • Poca flexibilidad<br>• Roles hardcodeados   |

#### Justificación

**Elegimos RBAC por grupos porque:**

1. **Flexibilidad**: Administradores pueden crear/modificar grupos según necesidades
2. **Herencia múltiple**: Un usuario puede tener múltiples grupos (ej: ENTRENADOR + ASISTENTE)
3. **Cambios inmediatos**: Modificar permisos de un grupo afecta a todos los usuarios del grupo
4. **Granularidad**: Permisos específicos (SOCIOS_VER vs SOCIOS_EDITAR)
5. **Aislamiento**: Grupos y permisos aislados por GymId

**Grupos predefinidos:**

- ADMIN_GYM: Gestión completa (excepto seguridad)
- ADMIN_SEGURIDAD: Gestión de usuarios y grupos
- ENTRENADOR: Visualización + asignación de rutinas
- ASISTENTE: Gestión de socios y turnos
- SOCIO: Acceso limitado (turnos y rutinas propias)

---

## Arquitectura Backend

### Decisión: Arquitectura en capas simple (Controllers, Services, Models)

#### Opciones consideradas

| Opción              | Ventajas                                                           | Desventajas                                          |
| ------------------- | ------------------------------------------------------------------ | ---------------------------------------------------- |
| **Capas simples** ✓ | • Simplicidad<br>• Adecuado para alcance académico<br>• Mantenible | • No DDD completo<br>• Menos abstracción             |
| Clean Architecture  | • Alta testabilidad<br>• Desacoplamiento                           | • Complejidad alta<br>• Overhead para alcance actual |
| DDD completo        | • Expresividad del dominio<br>• Escalabilidad                      | • Curva de aprendizaje<br>• Overkill para MVP        |

#### Justificación

**Elegimos capas simples porque:**

1. **Adecuación al alcance**: Proyecto académico MVP, no requiere complejidad de Clean Architecture
2. **Separación de responsabilidades clara**:
   - Controllers: solo exponer HTTP
   - Services: toda la lógica de negocio
   - Models: entidades de dominio
3. **Facilidad de comprensión**: Equipo puede entender rápidamente la estructura
4. **Evolución futura**: Arquitectura permite refactoring a Clean/DDD si es necesario

**Responsabilidades:**

```csharp
// Controller: delgado, sin lógica
[ApiController]
[Route("api/[controller]")]
public class SociosController : ControllerBase
{
    private readonly ISocioService _socioService;

    [HttpGet]
    [RequirePermission("SOCIOS_VER")]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1)
    {
        var result = await _socioService.GetAllAsync(page);
        return Ok(result);
    }
}

// Service: lógica de negocio
public class SocioService : ISocioService
{
    public async Task<PagedResult<SocioDto>> GetAllAsync(int page)
    {
        // Validaciones de negocio
        // Consultas con filtrado por GymId
        // Mapeo a DTOs
        return result;
    }
}
```

---

### Decisión: No implementar repositorios adicionales sobre EF Core

#### Justificación

**EF Core ya implementa Repository + Unit of Work:**

1. **DbSet<T>** actúa como Repository
2. **DbContext** actúa como Unit of Work
3. Capa adicional agrega complejidad sin valor en este alcance

**Abstracción de datos en Services:**

```csharp
public class SocioService : ISocioService
{
    private readonly ApplicationDbContext _context;

    public async Task<SocioDto> GetByIdAsync(int id)
    {
        var socio = await _context.Socios
            .Include(s => s.Usuario)
            .FirstOrDefaultAsync(s => s.Id == id);
        // ... mapeo a DTO
    }
}
```

**Si en el futuro se requiere abstracción** (ej: cambiar ORM), se puede refactorizar a repositorios sin cambiar contratos públicos (ISocioService).

---

## Frontend

### Decisión: React con Context API para estado global

#### Opciones consideradas

| Opción                    | Ventajas                                                        | Desventajas                               |
| ------------------------- | --------------------------------------------------------------- | ----------------------------------------- |
| **Context API + Hooks** ✓ | • Nativo de React<br>• Simplicidad<br>• Sin dependencias extras | • No optimizado para estado complejo      |
| Redux/Redux Toolkit       | • Herramientas de debug<br>• Escalabilidad                      | • Complejidad<br>• Boilerplate            |
| Zustand                   | • Simplicidad<br>• Performance                                  | • Menos adopción<br>• Dependencia externa |

#### Justificación

**Elegimos Context API porque:**

1. **Adecuación al alcance**: Estado global simple (auth, tenant)
2. **Nativo**: No requiere librerías adicionales
3. **Suficiencia**: No hay necesidad de estado complejo compartido entre muchos componentes

**Contextos principales:**

- **AuthContext**: Usuario autenticado, access token (en memoria), funciones login/logout
- **TenantContext**: Gimnasio actual del usuario
- **PermissionContext** (opcional): Permisos del usuario

**Si el proyecto crece**, migración a Zustand o Redux Toolkit es directa.

---

### Decisión: Access Token en memoria, Refresh Token en cookie

#### Justificación

**Access Token en memoria (no localStorage):**

- **Protección XSS**: localStorage accesible desde cualquier script
- **Seguridad**: Token expira rápido (15 min), perdido al recargar página (pero refresh automático)

```jsx
const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [accessToken, setAccessToken] = useState(null); // En memoria

  const login = async (gymId, username, password) => {
    const { accessToken } = await authService.login(gymId, username, password);
    setAccessToken(accessToken); // Refresh Token en cookie HttpOnly
  };

  return (
    <AuthContext.Provider value={{ accessToken, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
```

**Auto-refresh en interceptor Axios:**

```jsx
api.interceptors.response.use(
  (response) => response,
  async (error) => {
    if (error.response?.status === 401 && !error.config._retry) {
      error.config._retry = true;
      const { accessToken } = await authService.refresh(); // Usa cookie
      setAccessToken(accessToken);
      return api(error.config); // Reintentar request
    }
    return Promise.reject(error);
  }
);
```

---

## Base de Datos

### Decisión: Microsoft SQL Server

#### Opciones consideradas

| Opción           | Ventajas                                                                   | Desventajas                                 |
| ---------------- | -------------------------------------------------------------------------- | ------------------------------------------- |
| **SQL Server** ✓ | • Integración con .NET<br>• Herramientas robustas<br>• Soporte empresarial | • Costo (versión paga)<br>• Windows-centric |
| PostgreSQL       | • Open source<br>• Funcionalidades avanzadas                               | • Menos integración con .NET                |
| MySQL            | • Open source<br>• Popular                                                 | • Funcionalidades limitadas                 |

#### Justificación

**Elegimos SQL Server porque:**

1. **Stack tecnológico**: Especificado en el PROMPT (coherencia con .NET)
2. **EF Core**: Excelente soporte e integración
3. **Herramientas**: SQL Server Management Studio, Azure Data Studio
4. **Entorno académico**: Licencia gratuita (Developer Edition)
5. **Deployment**: Azure SQL Database facilita producción

---

### Decisión: Entity Framework Core Code-First

#### Opciones consideradas

| Opción                   | Ventajas                                                                    | Desventajas                                |
| ------------------------ | --------------------------------------------------------------------------- | ------------------------------------------ |
| **EF Core Code-First** ✓ | • Control desde código<br>• Migraciones versionadas<br>• Fuertemente tipado | • Curva de aprendizaje                     |
| Database-First           | • Diseño visual<br>• BD existente                                           | • Sincronización manual<br>• Menos control |
| Dapper (micro-ORM)       | • Performance<br>• Control SQL                                              | • Mapeo manual<br>• Más código             |

#### Justificación

**Elegimos EF Core Code-First porque:**

1. **Migraciones**: Versionado automático de esquema
2. **Productividad**: LINQ, tracking de cambios, navegación por relaciones
3. **Convención sobre configuración**: Menos código para casos comunes
4. **Query Filters**: Esencial para multi-tenancy automático

**Ejemplo de migración:**

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## Seguridad

### Decisión: Contraseñas hasheadas con BCrypt

#### Opciones consideradas

| Opción        | Ventajas                                                                          | Desventajas                        |
| ------------- | --------------------------------------------------------------------------------- | ---------------------------------- |
| **BCrypt** ✓  | • Estándar de industria<br>• Resistente a fuerza bruta<br>• Cost factor ajustable | • Más lento (intencional)          |
| Argon2        | • Ganador PHC<br>• Resistente a GPU                                               | • Menos adopción<br>• Más complejo |
| SHA256 + salt | • Rápido                                                                          | • Inseguro (GPU/rainbow tables)    |

#### Justificación

**Elegimos BCrypt porque:**

1. **Seguridad probada**: Estándar desde hace años
2. **Resistencia a ataques**: Algoritmo costoso computacionalmente
3. **Madurez**: Librerías probadas (BCrypt.Net)

```csharp
public class PasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
    }

    public bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
```

---

### Decisión: HTTPS obligatorio

#### Justificación

- **Encriptación en tránsito**: Credenciales, tokens y datos sensibles
- **Prevención MITM**: Man-in-the-middle attacks
- **SEO y confianza**: Requisito para sitios modernos

**Configuración .NET:**

```csharp
app.UseHttpsRedirection();
app.UseHsts(); // HTTP Strict Transport Security
```

**Configuración cookies:**

```csharp
Secure = true // Solo HTTPS
```

---

### Decisión: CORS configurado para dominio frontend específico

#### Justificación

**No usar wildcard (`*`):**

- **Seguridad**: Solo el dominio del frontend puede acceder a la API
- **Cookies**: CORS con credenciales requiere origen específico

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("https://app.mindfit.com.ar")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // Para cookies HttpOnly
    });
});

app.UseCors("FrontendPolicy");
```

---

## Performance y Escalabilidad

### Decisión: Índices en columnas de búsqueda frecuente

#### Justificación

**Índices esenciales:**

- (GymId, ...) en todas las tablas multi-tenant
- (Email), (Username) para login
- (FechaTurno, Estado) en Turnos
- (Activo) en entidades con filtrado por estado

**EF Core:**

```csharp
modelBuilder.Entity<Socio>()
    .HasIndex(s => new { s.GymId, s.NumeroSocio }).IsUnique();

modelBuilder.Entity<Usuario>()
    .HasIndex(u => new { u.GymId, u.Email }).IsUnique();
```

---

### Decisión: Paginación obligatoria en listados

#### Justificación

**Evitar carga de datos masivos:**

```csharp
public async Task<PagedResult<SocioDto>> GetAllAsync(int page, int pageSize)
{
    var query = _context.Socios.AsQueryable();

    var totalItems = await query.CountAsync();
    var items = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    return new PagedResult<SocioDto>
    {
        Items = items,
        TotalItems = totalItems,
        CurrentPage = page,
        PageSize = pageSize
    };
}
```

---

### Decisión: Caché en memoria para permisos (HttpContext.Items)

#### Justificación

**Evitar consultas repetidas en mismo request:**

```csharp
public async Task<bool> UserHasPermission(int userId, string permission)
{
    var cacheKey = $"permissions_{userId}";

    if (HttpContext.Items.ContainsKey(cacheKey))
        return ((List<string>)HttpContext.Items[cacheKey]).Contains(permission);

    var permisos = await GetUserPermissions(userId);
    HttpContext.Items[cacheKey] = permisos;

    return permisos.Contains(permission);
}
```

**Alcance**: Solo durante el request actual, no entre requests (permisos pueden cambiar).

---

## Servicios Transversales

### Decisión: IEmailService abstracto

#### Justificación

**Abstracción permite:**

1. **Mock en desarrollo**: No enviar emails reales
2. **Testing**: Verificar envío sin SMTP real
3. **Cambio de proveedor**: SendGrid, MailKit, AWS SES, etc.

```csharp
public interface IEmailService
{
    Task SendAsync(string to, string subject, string body);
}

public class SmtpEmailService : IEmailService
{
    public async Task SendAsync(string to, string subject, string body)
    {
        // Implementación con MailKit/SmtpClient
    }
}

public class MockEmailService : IEmailService
{
    public async Task SendAsync(string to, string subject, string body)
    {
        Console.WriteLine($"MOCK EMAIL: To={to}, Subject={subject}");
        await Task.CompletedTask;
    }
}
```

**Registro en DI:**

```csharp
#if DEBUG
services.AddScoped<IEmailService, MockEmailService>();
#else
services.AddScoped<IEmailService, SmtpEmailService>();
#endif
```

---

### Decisión: Logging con ILogger de .NET

#### Justificación

**ILogger nativo:**

1. **Integración**: Parte de .NET, sin dependencias
2. **Proveedores**: Console, Debug, EventLog, Application Insights, Serilog
3. **Niveles**: Trace, Debug, Information, Warning, Error, Critical

```csharp
public class SocioService
{
    private readonly ILogger<SocioService> _logger;

    public async Task<SocioDto> CreateAsync(SocioRequestDto dto)
    {
        _logger.LogInformation("Creando socio: {Email}", dto.Email);
        try
        {
            // ... lógica
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear socio: {Email}", dto.Email);
            throw;
        }
    }
}
```

**Extensión opcional**: Serilog para logging estructurado avanzado.

---

### Decisión: Validación con FluentValidation

#### Justificación

**FluentValidation vs Data Annotations:**

| Aspecto                | FluentValidation | Data Annotations |
| ---------------------- | ---------------- | ---------------- |
| Expresividad           | Alta             | Media            |
| Validaciones complejas | ✓                | Limitado         |
| Reutilización          | ✓                | Limitado         |
| Testing                | Fácil            | Difícil          |

```csharp
public class SocioRequestValidator : AbstractValidator<SocioRequestDto>
{
    public SocioRequestValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.FechaNacimiento)
            .Must(BeAValidAge).WithMessage("Debe ser mayor de 16 años");
    }

    private bool BeAValidAge(DateTime? fecha)
    {
        if (!fecha.HasValue) return true;
        var age = DateTime.Today.Year - fecha.Value.Year;
        return age >= 16;
    }
}
```

---

## Resumen de Decisiones Clave

| Aspecto              | Decisión                  | Justificación principal                  |
| -------------------- | ------------------------- | ---------------------------------------- |
| Multi-tenancy        | BD única con GymId        | Simplicidad operativa y bajo costo       |
| Autenticación        | JWT + Refresh Token       | Balance seguridad-usabilidad             |
| Autorización         | RBAC dinámico             | Flexibilidad y cambios inmediatos        |
| Refresh Token        | Cookie HttpOnly           | Protección contra XSS                    |
| Permisos en JWT      | NO incluidos              | Cambios deben aplicar inmediatamente     |
| Arquitectura         | Capas simples             | Adecuado para alcance académico          |
| Repositorios         | No sobre EF Core          | EF Core ya es Repository + UoW           |
| Frontend Estado      | Context API               | Nativo, suficiente para alcance          |
| Access Token Storage | Memoria (no localStorage) | Protección contra XSS                    |
| Base de Datos        | SQL Server                | Especificado en PROMPT, integración .NET |
| ORM                  | EF Core Code-First        | Migraciones, Query Filters multi-tenant  |
| Passwords            | BCrypt                    | Estándar de industria                    |
| Email                | IEmailService abstracto   | Testabilidad y cambio de proveedor       |
| Validación           | FluentValidation          | Expresividad y reutilización             |
| Logging              | ILogger .NET              | Nativo, integración completa             |

---

## Trade-offs Aceptados

### Consulta de permisos en cada request

**Trade-off**: Pequeño overhead de consulta vs actualización inmediata de permisos  
**Decisión**: Aceptado. Consultas optimizadas con caché en HttpContext.Items por request.

### Access Token en memoria (se pierde al recargar)

**Trade-off**: UX (relogin al F5) vs seguridad (no XSS)  
**Decisión**: Aceptado. Auto-refresh mitiga impacto. Seguridad prioritaria.

### BD única (no aislamiento total)

**Trade-off**: Riesgo teórico de data leakage vs simplicidad  
**Decisión**: Aceptado. Query Filters + testing riguroso mitigan riesgo.

### Arquitectura simple (no Clean Architecture)

**Trade-off**: Menor abstracción vs rapidez de desarrollo  
**Decisión**: Aceptado. Alcance académico MVP. Refactoring futuro posible.

---

**Última actualización:** 01/01/2026
