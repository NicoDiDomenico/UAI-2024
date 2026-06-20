# Etapa 3 Turnos - Parte 8 Nuevo Turno

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend

## 2. Tarea Principal

### Primera Implementación

Implementar los ajustes necesarios en el frontend para adaptar el login al nuevo contrato de respuesta de `POST /api/Auth/login`.

Antes, `TokenResponseDto` devolvía:

```csharp
AccessToken
RefreshToken
Permisos
```

Ahora también devuelve:

```csharp
DatosPersonales
```

con la siguiente estructura:

```csharp
public class DatosPersonalesDto
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public List<string>? Rol { get; set; }
}
```

Se requiere que el frontend:

- Actualice los tipos TypeScript relacionados al login/sesión para incluir `datosPersonales`.
- Persista `datosPersonales` en `localStorage` junto con los datos actuales de sesión.
- Use una clave namespaced consistente con las existentes, por ejemplo:

```txt
mindfit.datosPersonales
```

- Actualice el helper centralizado de storage, `authStorage.ts`, para:
  - guardar `datosPersonales` al iniciar sesión
  - leer `datosPersonales` al hidratar la sesión
  - eliminar `datosPersonales` al cerrar sesión

- Actualice `AuthSession`, `TokenResponse` y cualquier uso relacionado en `AuthContext` o hooks de autenticación.
- Mantenga la lógica actual de `accessToken`, `refreshToken`, `idGym` y `permisos` sin romper compatibilidad.
- No guardar username ni password en `localStorage`.
- No modificar backend.

Este ajuste debe ser previo o paralelo al desarrollo de Nuevo Turno, porque futuras pantallas pueden necesitar conocer el usuario logueado, su id, nombre, apellido o rol.

### Segunda Implementación

Implementar el redireccionamiento post-login según el rol del usuario autenticado.

Actualmente, luego de un login exitoso, `LoginPage.tsx` ejecuta:

```tsx
navigate("/dashboard", { replace: true });
```

Además, cuando un usuario autenticado entra a `/login`, `LoginPage.tsx` redirige directamente a:

```tsx
<Navigate to="/dashboard" replace />
```

Y el fallback de rutas en `AppRouter.tsx` también redirige a `/dashboard` cuando existe una sesión autenticada:

```tsx
return <Navigate to={isAuthenticated ? "/dashboard" : "/login"} replace />;
```

Se requiere modificar ese comportamiento para que dependa del rol del usuario autenticado.

A partir del nuevo contrato de `POST /api/Auth/login`, la response incluye `datosPersonales.rol`, por ejemplo:

```json
{
  "accessToken": "...",
  "refreshToken": "...",
  "permisos": [],
  "datosPersonales": {
    "id": 8,
    "nombre": "Paula",
    "apellido": "Pareto",
    "rol": ["Socio"]
  }
}
```

Cuando `datosPersonales.rol` contenga el valor `"Socio"`:

- redirigir al usuario a la nueva ruta:

```txt
/socio/inicio
```

- no enviarlo a `/dashboard`
- crear una pantalla inicial para socios:

```txt
Frontend/src/pages/socio/SocioInicioPage.tsx
```

- registrar la nueva ruta protegida en:

```txt
Frontend/src/routes/AppRouter.tsx
```

La ruta debe quedar dentro de `ProtectedRoute`:

```tsx
<Route path="/socio/inicio" element={<SocioInicioPage />} />
```

La pantalla `SocioInicioPage` debe ser un placeholder simple, por ejemplo:

```txt
Inicio Socio
Próximamente vas a poder consultar tus turnos, rutinas y datos personales.
```

Cuando el usuario no tenga el rol `"Socio"`:

- mantener el comportamiento actual
- redirigirlo a `/dashboard`
- conservar `/dashboard` renderizando `InicioPage`
- no modificar el flujo actual para responsables, asistentes, administradores u otros perfiles

También se debe actualizar:

```txt
Frontend/src/pages/auth/LoginPage.tsx
```

para que:

- si el usuario ya está autenticado y su sesión tiene rol `"Socio"`, redirija a `/socio/inicio`
- si el usuario ya está autenticado y no tiene rol `"Socio"`, redirija a `/dashboard`
- después de un login exitoso, use la sesión retornada por `login(...)` para decidir si navega a `/socio/inicio` o a `/dashboard`

También se debe actualizar:

```txt
Frontend/src/routes/AppRouter.tsx
```

para que `FallbackRoute` use la sesión autenticada y respete el rol:

- si no hay sesión autenticada, redirigir a `/login`
- si hay sesión autenticada con rol `"Socio"`, redirigir a `/socio/inicio`
- si hay sesión autenticada sin rol `"Socio"`, redirigir a `/dashboard`

La validación de rol debe hacerse desde:

```txt
session.datosPersonales.rol
```

usando los datos persistidos e hidratados desde `localStorage`.

Se recomienda crear un helper simple para evitar duplicar la comparación del rol, por ejemplo en:

```txt
Frontend/src/utils/authRoles.ts
```

con una función similar a:

```ts
export function isSocioRole(roles?: string[] | null) {
  return roles?.some((rol) => rol.toLowerCase() === "socio") ?? false;
}
```

No usar los permisos para detectar si es Socio, porque en este caso el criterio correcto es el rol devuelto por el backend dentro de `DatosPersonalesDto`.

No modificar backend.

### Tercera Implementación

Implementar la pantalla inicial real del Socio en:

```txt
Frontend/src/pages/socio/SocioInicioPage.tsx
```

Aclaración importante: si este archivo todavía no existe al momento de implementar esta sección, debe ser creado. Si ya fue creado en la Segunda Implementación como placeholder, debe reemplazarse su contenido por la funcionalidad real descripta en esta sección.

La pantalla debe cargar y mostrar en un grid el historial/listado de turnos del socio autenticado, consumiendo el endpoint:

```txt
GET /api/Turno/socio
```

Endpoint backend:

```csharp
[Authorize]
[HttpGet("socio")]
public async Task<ActionResult<IEnumerable<TurnoDto>>> SocioGetTurnos()
{
    var claimIdUsuario = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (!int.TryParse(claimIdUsuario, out var idUsuarioSocio))
        return Unauthorized();

    var turnos = await _turnoService.GetTurnosByIdUsuarioSocio(idUsuarioSocio);

    return (turnos is null) ? NotFound() : Ok(turnos);
}
```

Características importantes del endpoint:

- El frontend no debe enviar `idUsuarioSocio` por query params ni por body.
- El backend obtiene el id del socio logueado desde el JWT.
- El endpoint requiere usuario autenticado.
- El `apiClient` existente debe enviar automáticamente:
  - `Authorization: Bearer {accessToken}`
  - `X-Gym-Id: {idGym}`

Response esperada:

```csharp
public class TurnoDto
{
    public int IdTurno { get; set; }
    public DateTime FechaAlta { get; set; }
    public EstadoTurno EstadoTurno { get; set; }
    public TimeSpan HoraDesde { get; set; }
    public TimeSpan HoraHasta { get; set; }
    public string NombreDia { get; set; } = null!;
    public string NombreResponsable { get; set; } = null!;
    public string ApellidoResponsable { get; set; } = null!;
}
```

La implementación debe mantener consistencia con la grilla ya creada para la gestión de turnos desde el lado asistente.

Revisar como referencia directa la implementación existente en:

```txt
Frontend/src/components/socios/GestionTurnosModal.tsx
Frontend/src/services/turnosService.ts
Frontend/src/types/turno.ts
Frontend/src/utils/apiError.ts
Frontend/src/App.css
```

En la etapa anterior de gestión de turnos del asistente ya se implementó un historial similar usando:

```txt
GET /api/Turno/asistente/{idUsuarioSocio}
```

Ahora se debe implementar la variante para el socio logueado usando:

```txt
GET /api/Turno/socio
```

Si existe lógica, tipos, helpers de formato o estilos reutilizables de `GestionTurnosModal.tsx`, se deben reutilizar o extraer a componentes/helpers compartidos para evitar duplicación.

Si se decide extraer una grilla compartida, crear un nuevo componente, por ejemplo:

```txt
Frontend/src/components/turnos/TurnosHistorialGrid.tsx
```

Este archivo no existe actualmente; debe crearse solo si realmente ayuda a reutilizar la grilla entre:

```txt
Frontend/src/components/socios/GestionTurnosModal.tsx
Frontend/src/pages/socio/SocioInicioPage.tsx
```

El objetivo es que ambas grillas se vean y se comporten de forma consistente.

Reutilizar el tipo existente:

```txt
Frontend/src/types/turno.ts
```

Si ya existe `TurnoHistorialItem`, usar ese tipo en lugar de crear un tipo duplicado.

Agregar en:

```txt
Frontend/src/services/turnosService.ts
```

una nueva función específica para consumir el endpoint del socio logueado.

Actualmente existe una función para el flujo del asistente, por ejemplo:

```ts
getSocioTurnos(idUsuarioSocio: number)
```

Esa función consume:

```txt
GET /api/Turno/asistente/{idUsuarioSocio}
```

No modificar ni romper esa función.

Agregar una nueva función separada para el flujo del Socio, por ejemplo:

```ts
getTurnosSocioLogueado(): Promise<TurnoHistorialItem[]>
```

Esta nueva función debe consumir:

```txt
GET /api/Turno/socio
```

sin recibir `idUsuarioSocio` como parámetro.

Si se considera necesario para aislar loading/error/data, crear un hook nuevo:

```txt
Frontend/src/hooks/useSocioTurnos.ts
```

Este archivo no existe actualmente; debe crearse solo si mejora la organización de la pantalla.

La pantalla `SocioInicioPage` debe:

- cargar los turnos al montar la pantalla
- mostrar estado de loading mientras se consulta el endpoint
- mostrar mensaje de error si falla la carga
- interpretar `404 Not Found` como lista vacía si el backend lo usa para indicar que el socio no tiene turnos
- mostrar el mensaje `No tenés turnos registrados.` cuando no haya datos
- mostrar los turnos en un grid/listado responsive y consistente con el historial de turnos del asistente
- permitir seleccionar un turno del grid

El grid debe mostrar las mismas columnas usadas en la gestión de turnos del asistente:

```txt
Fecha Turno
Hora Desde
Hora Hasta
Estado Turno
Entrenador
```

Mapeo visual:

- `fechaAlta` en la columna **Fecha Turno**
- `horaDesde` en la columna **Hora Desde**
- `horaHasta` en la columna **Hora Hasta**
- `estadoTurno` en la columna **Estado Turno**
- `nombreResponsable` + `apellidoResponsable` en la columna **Entrenador**

Aclaraciones de formato:

- La columna visual **Fecha Turno** debe mostrar el valor recibido en `fechaAlta`.
- Aunque el nombre técnico del backend sea `fechaAlta`, en la UI debe mantenerse el encabezado **Fecha Turno**.
- Los campos `horaDesde` y `horaHasta` deben mostrarse tal como vienen desde el backend.
- No aplicar conversión horaria ni recalcular horarios en frontend.
- Solo aplicar un formateo visual simple si ya existe en la implementación anterior, por ejemplo para dejar `HH:mm`.
- Si `estadoTurno` llega como `"EnCurso"`, mostrarlo visualmente como `"En Curso"`.
- No modificar internamente el valor real recibido del backend.

Al seleccionar un turno:

- guardar internamente el `idTurno` seleccionado
- resaltar visualmente la fila seleccionada
- habilitar futuras acciones sobre ese turno, especialmente cancelación

Agregar en la pantalla dos acciones principales:

```txt
Nuevo Turno
Cancelar Turno
```

Comportamiento inicial requerido:

- `Nuevo Turno` debe quedar visible y preparado para la siguiente implementación.
- `Nuevo Turno` no requiere selección de turno.
- `Cancelar Turno` debe estar deshabilitado si no hay un turno seleccionado.
- Al seleccionar un turno, `Cancelar Turno` debe quedar habilitado.
- En esta tercera implementación no implementar todavía el endpoint de cancelar turno.
- En esta tercera implementación no implementar todavía el endpoint de nuevo turno.
- Si se necesita una acción temporal, ambos botones pueden abrir un mensaje o modal simple con el texto `Próximamente...`, manteniendo consistencia con la implementación anterior de `GestionTurnosModal`.

Sobre permisos:

- En esta pantalla del Socio no usar permisos para mostrar u ocultar los botones.
- El acceso a esta pantalla ya depende de autenticación y del redireccionamiento por rol `"Socio"` implementado previamente.
- La detección del rol Socio se resuelve desde `session.datosPersonales.rol`.
- No reutilizar la lógica de permisos `AGREGAR_TURNO` o `CANCELAR_TURNO` aplicada al asistente, salvo que más adelante se indique explícitamente.

La implementación debe mantener el frontend simple:

- no modificar backend
- no enviar datos que el backend resuelve desde el JWT
- no duplicar lógica existente de Axios
- no agregar dependencias nuevas
- reutilizar tipos, estilos, helpers o componentes existentes cuando sea razonable
- mantener `/dashboard` renderizando `InicioPage` para usuarios no Socio
- mantener el diseño limpio, moderno y consistente con el resto del módulo

## 3. Contexto

- AGENTS.md
- frontend-skill.md
- Docs/Frontend/Etapa-1-Auth/Paso-1-Login/implementation-plan.md
- IMPLEMENTATION_LOG_inicio-plan.md
- Frontend/src/routes/AppRouter.tsx
- Frontend/src/pages/auth/LoginPage.tsx
- Frontend/src/contexts/AuthContext.tsx
- Frontend/src/components/socios/GestionTurnosModal.tsx
- Frontend/src/services/turnosService.ts
- Frontend/src/types/turno.ts
- Frontend/src/utils/apiError.ts
- Frontend/src/App.css
- AuthController.cs
- TokenResponseDto.cs
- DatosPersonalesDto.cs
- TurnoController.cs
- TurnoDto.cs
- EstadoTurno.cs

## 4. Reglas y Restricciones (Constraints / Guardrails)

- Para la grilla de turnos del Socio, revisar primero la implementación previa de `GestionTurnosModal.tsx`. Si existen tipos, helpers, estilos o lógica reutilizable, reutilizarlos o extraerlos a componentes compartidos antes de duplicar código.
- En la Tercera Implementación no implementar todavía los endpoints de nuevo turno ni cancelar turno. Solo preparar los botones y la selección de fila.
- `SocioInicioPage.tsx`, `TurnosHistorialGrid.tsx` y `useSocioTurnos.ts` pueden no existir todavía. Si se mencionan en el plan, deben tratarse como archivos a crear cuando corresponda, no como archivos existentes obligatorios.

## 5. Formato de Salida

Además de implementar el código solicitado, generar: IMPLEMENTATION_LOG_modulo-socios.md

El archivo debe crearse en la misma carpeta/ruta donde se encuentra este plan .md.

El log debe documentar brevemente:

- qué archivos fueron creados o modificados
- qué decisiones importantes se tomaron
- cómo se resolvió la integración frontend/backend
- validaciones implementadas
- manejo de estados/loading/errors
- configuración relevante de Axios/interceptors/context/hooks si aplica
- posibles TODOs o limitaciones detectadas

El objetivo es que futuras iteraciones de IA puedan entender rápidamente qué fue implementado y por qué.
