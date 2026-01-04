# Estructura de Proyectos y Carpetas - MindFit Intelligence

## Índice

1. [Estructura general](#estructura-general)
2. [Backend (.NET 8)](#backend-net-8)
3. [Frontend (React)](#frontend-react)
4. [Convenciones y estándares](#convenciones-y-estándares)

---

## Estructura general

```
c:\Users\Nicol\Desktop\UAI-2024\MindFit Intelligence\
├── README.md
├── PROMPT_MindFit_Intelligence.md
├── AI_WORKLOG.md
├── ARQUITECTURA_GENERAL.md
├── ESTRUCTURA_PROYECTO.md (este archivo)
├── MODELO_DATOS.md
├── ENDPOINTS_API.md
├── DECISIONES_TECNICAS.md
├── PLAN_IMPLEMENTACION.md
├── backend/
│   └── MindFit.Api/
└── frontend/
    └── mindfit-web/
```

---

## Backend (.NET 8)

### Estructura completa del proyecto ASP.NET Core

```
backend/
└── MindFit.Api/
    ├── MindFit.Api.csproj
    ├── MindFit.Api.sln
    ├── Program.cs
    ├── appsettings.json
    ├── appsettings.Development.json
    ├── appsettings.Production.json
    │
    ├── Controllers/
    │   ├── AuthController.cs
    │   ├── PublicController.cs
    │   ├── GymController.cs
    │   ├── SociosController.cs
    │   ├── TurnosController.cs
    │   ├── RutinasController.cs
    │   ├── GimnasioController.cs
    │   │   ├── MaquinasController.cs
    │   │   ├── EjerciciosController.cs
    │   │   ├── EquipamientoController.cs
    │   │   ├── HorariosController.cs
    │   │   ├── EntrenadoresController.cs
    │   │   └── ConfiguracionesController.cs
    │   ├── SeguridadController.cs
    │   │   ├── UsuariosController.cs
    │   │   ├── GruposController.cs
    │   │   └── PermisosController.cs
    │   └── IAController.cs
    │
    ├── Services/
    │   ├── Interfaces/
    │   │   ├── IAuthService.cs
    │   │   ├── IPermissionService.cs
    │   │   ├── ITokenService.cs
    │   │   ├── IEmailService.cs
    │   │   ├── IPublicService.cs
    │   │   ├── ISocioService.cs
    │   │   ├── ITurnoService.cs
    │   │   ├── IRutinaService.cs
    │   │   ├── IMaquinaService.cs
    │   │   ├── IEjercicioService.cs
    │   │   ├── IEquipamientoService.cs
    │   │   ├── IHorarioService.cs
    │   │   ├── IEntrenadorService.cs
    │   │   ├── IConfiguracionService.cs
    │   │   ├── IUsuarioService.cs
    │   │   ├── IGrupoService.cs
    │   │   ├── IPermisoService.cs
    │   │   └── IIAService.cs
    │   │
    │   ├── Auth/
    │   │   ├── AuthService.cs
    │   │   ├── PermissionService.cs
    │   │   └── TokenService.cs
    │   │
    │   ├── Public/
    │   │   └── PublicService.cs
    │   │
    │   ├── Portal/
    │   │   ├── SocioService.cs
    │   │   ├── TurnoService.cs
    │   │   ├── RutinaService.cs
    │   │   ├── MaquinaService.cs
    │   │   ├── EjercicioService.cs
    │   │   ├── EquipamientoService.cs
    │   │   ├── HorarioService.cs
    │   │   ├── EntrenadorService.cs
    │   │   └── ConfiguracionService.cs
    │   │
    │   ├── Security/
    │   │   ├── UsuarioService.cs
    │   │   ├── GrupoService.cs
    │   │   └── PermisoService.cs
    │   │
    │   ├── Infrastructure/
    │   │   ├── EmailService.cs
    │   │   └── IAService.cs
    │   │
    │   └── Base/
    │       └── BaseService.cs
    │
    ├── Models/
    │   ├── SaaS/
    │   │   ├── Gym.cs
    │   │   ├── Lead.cs
    │   │   └── ContactMessage.cs
    │   │
    │   ├── Security/
    │   │   ├── Usuario.cs
    │   │   ├── Grupo.cs
    │   │   ├── Permiso.cs
    │   │   ├── UsuarioGrupo.cs
    │   │   ├── GrupoPermiso.cs
    │   │   ├── RefreshToken.cs
    │   │   └── PasswordResetToken.cs
    │   │
    │   ├── Portal/
    │   │   ├── Socio.cs
    │   │   ├── Turno.cs
    │   │   ├── Rutina.cs
    │   │   ├── RutinaEjercicio.cs
    │   │   ├── Maquina.cs
    │   │   ├── Ejercicio.cs
    │   │   ├── Equipamiento.cs
    │   │   ├── Horario.cs
    │   │   ├── Entrenador.cs
    │   │   └── Configuracion.cs
    │   │
    │   └── Base/
    │       └── BaseEntity.cs
    │
    ├── DTOs/
    │   ├── Auth/
    │   │   ├── LoginRequestDto.cs
    │   │   ├── LoginResponseDto.cs
    │   │   ├── RefreshTokenRequestDto.cs
    │   │   ├── RefreshTokenResponseDto.cs
    │   │   ├── ForgotPasswordRequestDto.cs
    │   │   ├── ResetPasswordRequestDto.cs
    │   │   └── ChangePasswordRequestDto.cs
    │   │
    │   ├── Public/
    │   │   ├── LeadRequestDto.cs
    │   │   ├── ContactRequestDto.cs
    │   │   └── GymSearchResponseDto.cs
    │   │
    │   ├── Socios/
    │   │   ├── SocioRequestDto.cs
    │   │   ├── SocioResponseDto.cs
    │   │   ├── SocioUpdateDto.cs
    │   │   └── SocioListDto.cs
    │   │
    │   ├── Turnos/
    │   │   ├── TurnoRequestDto.cs
    │   │   ├── TurnoResponseDto.cs
    │   │   ├── TurnoListDto.cs
    │   │   └── TurnoCancelDto.cs
    │   │
    │   ├── Rutinas/
    │   │   ├── RutinaRequestDto.cs
    │   │   ├── RutinaResponseDto.cs
    │   │   ├── RutinaUpdateDto.cs
    │   │   ├── RutinaListDto.cs
    │   │   └── RutinaEjercicioDto.cs
    │   │
    │   ├── Gimnasio/
    │   │   ├── MaquinaDto.cs
    │   │   ├── EjercicioDto.cs
    │   │   ├── EquipamientoDto.cs
    │   │   ├── HorarioDto.cs
    │   │   ├── EntrenadorDto.cs
    │   │   └── ConfiguracionDto.cs
    │   │
    │   ├── Seguridad/
    │   │   ├── UsuarioRequestDto.cs
    │   │   ├── UsuarioResponseDto.cs
    │   │   ├── UsuarioUpdateDto.cs
    │   │   ├── UsuarioListDto.cs
    │   │   ├── GrupoRequestDto.cs
    │   │   ├── GrupoResponseDto.cs
    │   │   ├── GrupoUpdateDto.cs
    │   │   ├── GrupoListDto.cs
    │   │   ├── PermisoDto.cs
    │   │   └── AsignarGruposDto.cs
    │   │
    │   ├── IA/
    │   │   ├── IARequestDto.cs
    │   │   └── IAResponseDto.cs
    │   │
    │   └── Common/
    │       ├── PagedResultDto.cs
    │       ├── ErrorResponseDto.cs
    │       └── SuccessResponseDto.cs
    │
    ├── Validators/
    │   ├── Auth/
    │   │   ├── LoginRequestValidator.cs
    │   │   ├── ForgotPasswordRequestValidator.cs
    │   │   └── ResetPasswordRequestValidator.cs
    │   │
    │   ├── Public/
    │   │   ├── LeadRequestValidator.cs
    │   │   └── ContactRequestValidator.cs
    │   │
    │   ├── Socios/
    │   │   ├── SocioRequestValidator.cs
    │   │   └── SocioUpdateValidator.cs
    │   │
    │   ├── Turnos/
    │   │   └── TurnoRequestValidator.cs
    │   │
    │   ├── Rutinas/
    │   │   └── RutinaRequestValidator.cs
    │   │
    │   └── Seguridad/
    │       ├── UsuarioRequestValidator.cs
    │       └── GrupoRequestValidator.cs
    │
    ├── Data/
    │   ├── ApplicationDbContext.cs
    │   ├── Configurations/
    │   │   ├── GymConfiguration.cs
    │   │   ├── UsuarioConfiguration.cs
    │   │   ├── GrupoConfiguration.cs
    │   │   ├── PermisoConfiguration.cs
    │   │   ├── SocioConfiguration.cs
    │   │   ├── TurnoConfiguration.cs
    │   │   └── ... (configuraciones EF Core por entidad)
    │   │
    │   └── Seeders/
    │       ├── PermisosSeeder.cs
    │       └── GruposSeeder.cs
    │
    ├── Migrations/
    │   └── (archivos generados por EF Core)
    │
    ├── Middleware/
    │   ├── TenantResolverMiddleware.cs
    │   ├── ExceptionHandlingMiddleware.cs
    │   └── RequestLoggingMiddleware.cs
    │
    ├── Filters/
    │   ├── PermissionAuthorizationFilter.cs
    │   └── ValidateModelStateFilter.cs
    │
    ├── Attributes/
    │   └── RequirePermissionAttribute.cs
    │
    ├── Extensions/
    │   ├── ServiceCollectionExtensions.cs
    │   ├── ApplicationBuilderExtensions.cs
    │   └── ClaimsPrincipalExtensions.cs
    │
    ├── Helpers/
    │   ├── PasswordHasher.cs
    │   ├── TokenGenerator.cs
    │   └── DateTimeHelper.cs
    │
    └── Constants/
        ├── Permissions.cs
        ├── Roles.cs
        └── AppConstants.cs
```

### Descripción de carpetas Backend

#### **Controllers/**

Exponen endpoints HTTP. Delgados, sin lógica de negocio.

**Ejemplo de estructura:**

```csharp
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SociosController : ControllerBase
{
    private readonly ISocioService _socioService;

    public SociosController(ISocioService socioService)
    {
        _socioService = socioService;
    }

    [HttpGet]
    [RequirePermission("SOCIOS_VER")]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        // Delegar a service
    }
}
```

#### **Services/**

Lógica de negocio. Subcarpetas por módulo funcional.

- **Interfaces/**: Contratos de servicios
- **Auth/**: Autenticación, tokens, permisos
- **Public/**: Leads, contacto (sin autenticación)
- **Portal/**: Módulos del portal (socios, turnos, rutinas, gimnasio)
- **Security/**: Gestión de usuarios, grupos, permisos
- **Infrastructure/**: Servicios transversales (email, IA)
- **Base/**: Clases base (si aplica)

#### **Models/**

Entidades del dominio (POCOs). Organización por contexto.

- **SaaS/**: Gyms, Leads, ContactMessages
- **Security/**: Usuario, Grupo, Permiso, tokens
- **Portal/**: Socios, Turnos, Rutinas, Máquinas, etc.
- **Base/**: Entidad base con propiedades comunes (Id, CreatedAt, UpdatedAt)

**Ejemplo de BaseEntity:**

```csharp
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

**Ejemplo de entidad con GymId:**

```csharp
public class Socio : BaseEntity
{
    public int GymId { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Email { get; set; }
    // ...

    public virtual Gym Gym { get; set; }
}
```

#### **DTOs/**

Contratos de entrada/salida. Organizados por módulo.

- Sufijos: `RequestDto`, `ResponseDto`, `UpdateDto`, `ListDto`
- Separación clara entre request y response
- DTOs específicos por operación

#### **Validators/**

Validadores FluentValidation para DTOs.

**Ejemplo:**

```csharp
public class SocioRequestValidator : AbstractValidator<SocioRequestDto>
{
    public SocioRequestValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).EmailAddress();
        // ...
    }
}
```

#### **Data/**

Contexto de EF Core, configuraciones y seeders.

- **ApplicationDbContext.cs**: DbContext principal
- **Configurations/**: Fluent API configurations (alternativa a Data Annotations)
- **Seeders/**: Datos iniciales (permisos, grupos predeterminados)

**Ejemplo de DbContext:**

```csharp
public class ApplicationDbContext : DbContext
{
    private readonly int? _tenantId; // GymId actual del contexto

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantResolver tenantResolver)
        : base(options)
    {
        _tenantId = tenantResolver.GetCurrentTenantId();
    }

    public DbSet<Gym> Gyms { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Socio> Socios { get; set; }
    // ...

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplicar configuraciones
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Query Filters para multi-tenancy
        modelBuilder.Entity<Socio>().HasQueryFilter(e => e.GymId == _tenantId);
        modelBuilder.Entity<Usuario>().HasQueryFilter(e => e.GymId == _tenantId);
        // ... aplicar a todas las entidades con GymId
    }
}
```

#### **Migrations/**

Migraciones generadas por EF Core.

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

#### **Middleware/**

Componentes del pipeline de procesamiento.

- **TenantResolverMiddleware.cs**: Extrae GymId del JWT y lo almacena en contexto
- **ExceptionHandlingMiddleware.cs**: Manejo global de excepciones
- **RequestLoggingMiddleware.cs**: Logging de requests

#### **Filters/**

Filtros de acción para autorización y validación.

- **PermissionAuthorizationFilter.cs**: Verifica permisos dinámicos por usuario
- **ValidateModelStateFilter.cs**: Valida ModelState automáticamente

#### **Attributes/**

Atributos personalizados.

**Ejemplo:**

```csharp
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class RequirePermissionAttribute : Attribute
{
    public string Permission { get; }

    public RequirePermissionAttribute(string permission)
    {
        Permission = permission;
    }
}
```

#### **Extensions/**

Métodos de extensión para configuración.

**Ejemplo:**

```csharp
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ISocioService, SocioService>();
        services.AddScoped<ITurnoService, TurnoService>();
        // ...
        return services;
    }
}
```

#### **Helpers/**

Utilidades transversales.

- **PasswordHasher.cs**: Hasheo de contraseñas (bcrypt)
- **TokenGenerator.cs**: Generación de tokens seguros
- **DateTimeHelper.cs**: Manejo de fechas/zonas horarias

#### **Constants/**

Constantes de la aplicación.

**Ejemplo:**

```csharp
public static class Permissions
{
    // Socios
    public const string SOCIOS_VER = "SOCIOS_VER";
    public const string SOCIOS_CREAR = "SOCIOS_CREAR";
    public const string SOCIOS_EDITAR = "SOCIOS_EDITAR";
    public const string SOCIOS_ELIMINAR = "SOCIOS_ELIMINAR";

    // Turnos
    public const string TURNOS_VER = "TURNOS_VER";
    public const string TURNOS_CREAR = "TURNOS_CREAR";
    // ...
}

public static class Roles
{
    public const string ADMIN_GYM = "ADMIN_GYM";
    public const string ADMIN_SEGURIDAD = "ADMIN_SEGURIDAD";
    public const string ENTRENADOR = "ENTRENADOR";
    public const string ASISTENTE = "ASISTENTE";
    public const string SOCIO = "SOCIO";
}
```

---

## Frontend (React)

### Estructura completa del proyecto React

```
frontend/
└── mindfit-web/
    ├── package.json
    ├── package-lock.json
    ├── .gitignore
    ├── .env.development
    ├── .env.production
    ├── vite.config.js (o webpack.config.js si usas CRA)
    ├── index.html
    ├── tsconfig.json (si usas TypeScript)
    │
    ├── public/
    │   ├── favicon.ico
    │   ├── logo.png
    │   └── assets/
    │       └── images/
    │
    └── src/
        ├── main.jsx (o index.js)
        ├── App.jsx
        ├── App.css
        │
        ├── routes/
        │   ├── index.jsx
        │   ├── PublicRoutes.jsx
        │   ├── PrivateRoutes.jsx
        │   └── ProtectedRoute.jsx
        │
        ├── layouts/
        │   ├── PublicLayout.jsx
        │   ├── PortalLayout.jsx
        │   └── AuthLayout.jsx
        │
        ├── pages/
        │   ├── public/
        │   │   ├── HomePage.jsx
        │   │   ├── FuncionalidadesPage.jsx
        │   │   ├── PreciosPage.jsx
        │   │   ├── TestimoniosPage.jsx
        │   │   ├── BlogPage.jsx
        │   │   ├── ContactoPage.jsx
        │   │   └── SolicitarDemoPage.jsx
        │   │
        │   ├── auth/
        │   │   ├── LoginPage.jsx
        │   │   ├── ForgotPasswordPage.jsx
        │   │   └── ResetPasswordPage.jsx
        │   │
        │   └── portal/
        │       ├── DashboardPage.jsx
        │       │
        │       ├── socios/
        │       │   ├── SociosPage.jsx
        │       │   ├── SocioDetailPage.jsx
        │       │   ├── SocioCreatePage.jsx
        │       │   └── SocioEditPage.jsx
        │       │
        │       ├── turnos/
        │       │   ├── TurnosPage.jsx
        │       │   ├── TurnoCreatePage.jsx
        │       │   └── TurnoCalendarPage.jsx
        │       │
        │       ├── rutinas/
        │       │   ├── RutinasPage.jsx
        │       │   ├── RutinaDetailPage.jsx
        │       │   ├── RutinaCreatePage.jsx
        │       │   └── RutinaEditPage.jsx
        │       │
        │       ├── gimnasio/
        │       │   ├── MaquinasPage.jsx
        │       │   ├── EjerciciosPage.jsx
        │       │   ├── EquipamientoPage.jsx
        │       │   ├── HorariosPage.jsx
        │       │   ├── EntrenadoresPage.jsx
        │       │   └── ConfiguracionesPage.jsx
        │       │
        │       ├── seguridad/
        │       │   ├── UsuariosPage.jsx
        │       │   ├── UsuarioCreatePage.jsx
        │       │   ├── UsuarioEditPage.jsx
        │       │   ├── GruposPage.jsx
        │       │   ├── GrupoCreatePage.jsx
        │       │   ├── GrupoEditPage.jsx
        │       │   └── PermisosPage.jsx
        │       │
        │       └── ia/
        │           └── IAAssistantPage.jsx
        │
        ├── components/
        │   ├── public/
        │   │   ├── Header.jsx
        │   │   ├── Footer.jsx
        │   │   ├── Hero.jsx
        │   │   ├── Features.jsx
        │   │   ├── Pricing.jsx
        │   │   ├── Testimonials.jsx
        │   │   ├── ContactForm.jsx
        │   │   └── DemoRequestForm.jsx
        │   │
        │   ├── portal/
        │   │   ├── Sidebar.jsx
        │   │   ├── Navbar.jsx
        │   │   ├── Dashboard/
        │   │   │   ├── StatsCard.jsx
        │   │   │   └── RecentActivity.jsx
        │   │   │
        │   │   ├── Socios/
        │   │   │   ├── SocioTable.jsx
        │   │   │   ├── SocioForm.jsx
        │   │   │   └── SocioCard.jsx
        │   │   │
        │   │   ├── Turnos/
        │   │   │   ├── TurnoTable.jsx
        │   │   │   ├── TurnoForm.jsx
        │   │   │   └── Calendar.jsx
        │   │   │
        │   │   ├── Rutinas/
        │   │   │   ├── RutinaTable.jsx
        │   │   │   ├── RutinaForm.jsx
        │   │   │   └── EjercicioSelector.jsx
        │   │   │
        │   │   ├── Seguridad/
        │   │   │   ├── UsuarioTable.jsx
        │   │   │   ├── UsuarioForm.jsx
        │   │   │   ├── GrupoTable.jsx
        │   │   │   └── GrupoForm.jsx
        │   │   │
        │   │   └── IA/
        │   │       └── ChatAssistant.jsx
        │   │
        │   └── common/
        │       ├── Button.jsx
        │       ├── Input.jsx
        │       ├── Select.jsx
        │       ├── Modal.jsx
        │       ├── Table.jsx
        │       ├── Pagination.jsx
        │       ├── Loader.jsx
        │       ├── ErrorMessage.jsx
        │       ├── SuccessMessage.jsx
        │       └── ConfirmDialog.jsx
        │
        ├── context/
        │   ├── AuthContext.jsx
        │   ├── TenantContext.jsx
        │   └── PermissionContext.jsx
        │
        ├── hooks/
        │   ├── useAuth.js
        │   ├── usePermissions.js
        │   ├── useFetch.js
        │   ├── useForm.js
        │   └── useDebounce.js
        │
        ├── services/
        │   ├── api.js (configuración axios)
        │   ├── authService.js
        │   ├── publicService.js
        │   ├── sociosService.js
        │   ├── turnosService.js
        │   ├── rutinasService.js
        │   ├── gimnasioService.js
        │   ├── seguridadService.js
        │   └── iaService.js
        │
        ├── utils/
        │   ├── validators.js
        │   ├── formatters.js
        │   ├── dateHelpers.js
        │   └── constants.js
        │
        ├── styles/
        │   ├── global.css
        │   ├── variables.css
        │   └── themes/
        │
        └── assets/
            ├── images/
            ├── icons/
            └── fonts/
```

### Descripción de carpetas Frontend

#### **routes/**

Configuración de rutas de React Router.

**Ejemplo index.jsx:**

```jsx
import { BrowserRouter, Routes, Route } from "react-router-dom";
import PublicRoutes from "./PublicRoutes";
import PrivateRoutes from "./PrivateRoutes";

export default function AppRouter() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/*" element={<PublicRoutes />} />
        <Route path="/portal/*" element={<PrivateRoutes />} />
      </Routes>
    </BrowserRouter>
  );
}
```

#### **layouts/**

Plantillas de página.

- **PublicLayout**: Header + contenido + Footer (sitio público)
- **PortalLayout**: Sidebar + Navbar + contenido (portal autenticado)
- **AuthLayout**: Pantalla de login centrada

#### **pages/**

Componentes de página completa.

- **public/**: Páginas del sitio público (marketing)
- **auth/**: Login, recuperar contraseña
- **portal/**: Módulos del portal organizados por funcionalidad

#### **components/**

Componentes reutilizables.

- **public/**: Componentes del sitio público
- **portal/**: Componentes específicos del portal por módulo
- **common/**: Componentes genéricos (Button, Input, Modal, Table, etc.)

#### **context/**

Context API para estado global.

**AuthContext:**

```jsx
const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [accessToken, setAccessToken] = useState(null); // En memoria

  const login = async (gymId, username, password) => {
    const response = await authService.login(gymId, username, password);
    setAccessToken(response.accessToken);
    setUser(response.user);
  };

  const logout = async () => {
    await authService.logout();
    setAccessToken(null);
    setUser(null);
  };

  return (
    <AuthContext.Provider value={{ user, accessToken, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
```

#### **hooks/**

Custom hooks.

**useAuth:**

```jsx
export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within AuthProvider");
  }
  return context;
};
```

**usePermissions:**

```jsx
export const usePermissions = () => {
  const { user } = useAuth();

  const hasPermission = (permission) => {
    return user?.permissions?.includes(permission) || false;
  };

  return { hasPermission };
};
```

#### **services/**

Servicios de comunicación con la API.

**api.js:**

```jsx
import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  withCredentials: true, // Para enviar cookies (refresh token)
});

// Interceptor para agregar access token
api.interceptors.request.use((config) => {
  const token = getAccessToken(); // De memoria/contexto
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Interceptor para auto-refresh
api.interceptors.response.use(
  (response) => response,
  async (error) => {
    if (error.response?.status === 401 && !error.config._retry) {
      error.config._retry = true;
      try {
        const { accessToken } = await authService.refresh();
        setAccessToken(accessToken);
        error.config.headers.Authorization = `Bearer ${accessToken}`;
        return api(error.config);
      } catch (refreshError) {
        // Refresh falló, logout
        logout();
        window.location.href = "/login";
      }
    }
    return Promise.reject(error);
  }
);

export default api;
```

#### **utils/**

Utilidades y helpers.

- **validators.js**: Validaciones de formularios
- **formatters.js**: Formateo de fechas, números, etc.
- **dateHelpers.js**: Manejo de fechas
- **constants.js**: Constantes del frontend

#### **styles/**

Estilos globales y temas.

---

## Convenciones y estándares

### Nomenclatura

#### Backend (C#)

- **Clases**: PascalCase (`SocioService`, `TurnoController`)
- **Métodos**: PascalCase (`GetAll`, `CreateSocio`)
- **Propiedades**: PascalCase (`GymId`, `FirstName`)
- **Variables locales**: camelCase (`socioDto`, `currentUser`)
- **Interfaces**: Prefijo `I` (`IAuthService`, `IEmailService`)
- **DTOs**: Sufijo `Dto` (`SocioRequestDto`, `LoginResponseDto`)
- **Constantes**: UPPER_SNAKE_CASE (`SOCIOS_VER`, `TURNOS_CREAR`)

#### Frontend (React)

- **Componentes**: PascalCase (`SocioTable`, `LoginPage`)
- **Archivos**: camelCase o PascalCase según contenido (`authService.js`, `LoginPage.jsx`)
- **Variables/funciones**: camelCase (`currentUser`, `handleSubmit`)
- **Constantes**: UPPER_SNAKE_CASE (`API_BASE_URL`)
- **Hooks**: Prefijo `use` (`useAuth`, `usePermissions`)

### Organización de código

#### Backend

- Un archivo por clase
- Carpetas por contexto funcional
- Services agrupados por módulo
- DTOs separados por operación (Request/Response/Update)

#### Frontend

- Un componente por archivo
- Carpetas por módulo funcional
- Componentes pequeños y reutilizables
- Lógica separada de UI (hooks, services)

### Comentarios y documentación

#### Backend

```csharp
/// <summary>
/// Crea un nuevo socio en el gimnasio actual.
/// </summary>
/// <param name="dto">Datos del socio a crear</param>
/// <param name="gymId">ID del gimnasio (tenant)</param>
/// <returns>Socio creado con su ID asignado</returns>
public async Task<SocioResponseDto> CreateSocio(SocioRequestDto dto, int gymId)
{
    // Implementación
}
```

#### Frontend

```jsx
/**
 * Tabla de socios con paginación y filtros.
 * @param {Array} socios - Lista de socios a mostrar
 * @param {Function} onEdit - Callback al editar un socio
 * @param {Function} onDelete - Callback al eliminar un socio
 */
export const SocioTable = ({ socios, onEdit, onDelete }) => {
  // Implementación
};
```

### Git

#### Estructura de branches

- `main`: producción
- `develop`: desarrollo
- `feature/nombre-feature`: nuevas funcionalidades
- `bugfix/nombre-bug`: correcciones

#### Commits

Formato: `tipo(módulo): descripción`

Tipos:

- `feat`: nueva funcionalidad
- `fix`: corrección de bug
- `docs`: documentación
- `refactor`: refactorización
- `test`: tests
- `chore`: tareas de mantenimiento

Ejemplos:

- `feat(socios): agregar listado con paginación`
- `fix(auth): corregir renovación de token expirado`
- `docs(readme): actualizar instrucciones de instalación`

---

## Próximos pasos

1. ✅ Estructura de proyectos definida
2. ⏳ Modelo de datos detallado
3. ⏳ Especificación de endpoints
4. ⏳ Casos de uso
5. ⏳ Plan de implementación

---

**Última actualización:** 01/01/2026
