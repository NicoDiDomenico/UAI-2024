# Plan simplificado: automatización de base tenant

## Objetivo

Extender `POST /api/Gyms/onboarding` para:

1. Registrar el gimnasio y usuario master.
2. Crear la base tenant.
3. Aplicar las migraciones existentes.
4. Cargar datos estructurales.
5. Crear el primer administrador tenant.
6. Activar el gimnasio.
7. Permitir el login inmediatamente.

## Reglas de seguridad obligatorias

- El aprovisionamiento solo puede ejecutarse sobre un nombre de base nuevo.
- Si la base ya existe físicamente en SQL Server, el proceso debe cancelarse antes de ejecutar `MigrateAsync()`.
- La connection string guardada en `Gym` debe validarse con `SqlConnectionStringBuilder` y debe coincidir con el servidor y nombre de base generados por el backend.
- El código de aprovisionamiento no utilizará `EnsureDeleted`, `DROP DATABASE`, `TRUNCATE` ni eliminaciones generales de datos.
- Ningún error debe provocar la eliminación automática de una base existente o parcialmente creada.
- La contraseña plana no se incluirá en logs, mensajes de error ni datos persistidos.
- Antes de conectar el flujo al onboarding real, las migraciones completas deben probarse sobre una base temporal nueva.

## Etapa 1: registro maestro

Mantener el flujo actual:

- Construir la connection string desde `TenantTemplate`.
- Crear `Gym` con `Activo = false`.
- Guardar `UsuarioMaster` y `PersonaResponsableMaster`.
- Conservar el `idGym` para la respuesta.
- Normalizar el nombre de la base y agregar un sufijo único para evitar colisiones.
- Rechazar nombres de gimnasio duplicados.

El guardado de `Gym`, `UsuarioMaster` y `PersonaResponsableMaster` se realizará dentro de una transacción local de `MindFitMasterContext`. Ambos repositorios ya reciben la misma instancia scoped del contexto durante la petición.

Para impedir dos gimnasios con el mismo nombre incluso si llegan dos onboardings simultáneos:

- normalizar el nombre antes de guardarlo;
- agregar un índice único sobre `Gym.NombreGym`;
- crear la migración correspondiente de `MindFitMasterContext`;
- comprobar previamente que los datos master existentes no contengan nombres duplicados;
- devolver un conflicto comprensible si SQL Server rechaza el duplicado.

No agregar estados ni fechas nuevas a `Gym`.

## Etapa 2: creación de la base tenant

Agregar una clase concreta y pequeña `TenantProvisioningService`.

Responsabilidades:

1. Recibir la connection string y los datos del administrador.
2. Crear un `DbContextOptionsBuilder<MindFitIntelligenceContext>` con esa conexión.
3. Instanciar un contexto independiente del `TenantContext` de la petición.
4. Validar la connection string con `SqlConnectionStringBuilder`.
5. Conectarse a la base `master` del mismo servidor y consultar `DB_ID(@nombreBase)`.
6. Cancelar el proceso si `DB_ID` indica que la base ya existe.
7. Ejecutar `Database.MigrateAsync()` únicamente cuando la base no exista.
8. Verificar `CanConnectAsync()`, la existencia de `__EFMigrationsHistory` y que no queden migraciones pendientes.

No usar `EnsureCreated` ni clonar una base existente.

Tampoco usar `EnsureDeleted`, `DROP DATABASE` o `TRUNCATE`. Las operaciones `DropColumn` y `DropTable` que ya existen dentro de migraciones históricas solo se ejecutarán sobre la base nueva y en el orden definido por EF.

La cuenta SQL de `TenantTemplate` deberá tener permisos para crear bases y ejecutar migraciones.

El contexto de aprovisionamiento tendrá un `CommandTimeout` específico y razonable para migraciones, sin modificar el timeout de los contextos utilizados por el resto de la aplicación. El tiempo definitivo se validará con la prueba real; como punto de partida se utilizarán 120 segundos por comando SQL.

## Etapa 3: datos iniciales estructurales

Agregar una clase `TenantInitialDataSeeder` con los datos que necesita toda base tenant.

### Seguridad y navegación

Cargar:

- Grupos `Admin`, `Asistente`, `Socio` y `Entrenador`.
- Los 30 permisos actuales.
- Relaciones `GrupoPermiso`.
- Los ocho formularios actuales.
- Relaciones `FormularioPermiso`.

El grupo `Admin` tendrá los 30 permisos. `Asistente` tendrá los ocho permisos del formulario de turnos y socios. Se conservarán las relaciones confirmadas para `Entrenador`. `Socio` no tiene permisos directos actualmente.

No incluir el grupo `Invitado`, porque en la base actual está identificado como una prueba.

Los datos se obtendrán mediante una extracción única y de solo lectura de la base tenant actual, contrastada con las políticas y modelos del backend. Quedarán escritos explícitamente en código: el onboarding no consultará ni clonará la base actual para generar cada tenant.

### Catálogos generales

Cargar:

- Los siete días de la semana.
- Los 12 grupos musculares.
- Los tres tipos de ejercicio: `Calentamiento`, `Entrenamiento` y `Estiramiento`.
- Los 24 rangos horarios de una hora.

Los rangos serán:

```text
00:00 - 01:00
01:00 - 02:00
...
22:00 - 23:00
23:00 - 00:00
```

`RangoHorario` es estructural porque no existe ningún endpoint para crear sus registros y `GET /api/RangoHorario` solo permite consultarlos.

### Combinaciones día-horario

Crear también los 168 registros de `DiaRangoHorario`:

```text
7 días × 24 rangos horarios = 168 registros
```

Cada combinación comenzará con:

```text
Activo = false
CupoMaximo = 0
```

Esto es necesario porque:

- La grilla consulta directamente `DiaRangoHorario`.
- El endpoint `cambiar-estado` solo modifica registros existentes.
- No existe un endpoint para crear las combinaciones.
- Cargar únicamente `RangoHorario` dejaría vacía la grilla de configuración.

Los registros son estructurales, mientras que `Activo`, `CupoMaximo` y responsables asignados son configuración particular de cada gimnasio.

### Repetición segura del seed

El seed será idempotente: si se ejecuta más de una vez no duplicará registros ni eliminará datos existentes.

Las búsquedas y relaciones utilizarán claves naturales, no IDs copiados de la base actual:

- `Grupo.Nombre`.
- `Permiso.Codigo`.
- `Formulario.NombreFormulario`.
- `Dia.NombreDia`.
- `RangoHorario` por `HoraDesde` y `HoraHasta`.
- Los valores de los enums de grupos musculares y tipos de ejercicio.

Los IDs necesarios para `GrupoPermiso`, `FormularioPermiso` y `DiaRangoHorario` se obtendrán de las entidades insertadas o consultadas en la nueva base.

### Datos excluidos

No copiar:

- Usuarios o personas existentes.
- Socios.
- Turnos y cupos por fecha.
- Cuotas.
- Rutinas e historiales.
- Asignaciones de responsables a horarios.
- Máquinas y equipamientos.
- Ejercicios existentes.

Los ejercicios actuales no se incorporarán hasta confirmar que deben ser un catálogo compartido y no datos administrables por cada gimnasio.

## Etapa 4: administrador tenant

Después de migrar y ejecutar el seed:

1. Crear un `Usuario` con el username del onboarding.
2. Crear su `PersonaResponsable`.
3. Hashear la contraseña con `PasswordHasher<Usuario>`.
4. Buscar el grupo por nombre `Admin`.
5. Crear la relación `UsuarioGrupo`.
6. Guardar todo en la base tenant.

La contraseña plana solamente existirá durante la petición.

No se registrará el DTO completo ni se interpolará la contraseña en logs o excepciones. El logging de datos sensibles de EF permanecerá desactivado.

El seed y el administrador podrán guardarse dentro de una transacción local tenant. No habrá una transacción distribuida con `MindFitMaster`.

## Etapa 5: activación

Cuando migraciones, seed y administrador terminen correctamente:

1. Actualizar `Gym.Activo = true`.
2. Guardar el cambio en `MindFitMaster`.
3. Devolver la respuesta actual con `mensaje` e `idGym`.

Agregar a `GymRepository` una operación sencilla para actualizar `Activo`.

Si falla cualquier paso:

- Registrar la excepción con `ILogger`.
- Mantener `Activo = false`.
- No mostrar el gimnasio en `GET /api/Gyms/activos`.
- No eliminar automáticamente la base parcial.
- Devolver el error normal del onboarding.

Comportamiento según el punto de fallo:

- Si falla la migración, puede quedar una base parcial, pero el gimnasio permanece inactivo.
- Si falla el seed o el administrador, la transacción tenant revierte ambos y el gimnasio permanece inactivo.
- Si falla la activación master, la base tenant queda preparada pero no será resoluble por `TenantResolver`; se registrará el error para corregir la activación manualmente.

## Etapa 6: frontend

Mantener el request y la respuesta actuales.

Modificar únicamente el mensaje de éxito de `GymOnboardingPage` para indicar que:

- El gimnasio ya fue creado y activado.
- El usuario administrador puede iniciar sesión inmediatamente.

## Verificación

Realizar una prueba controlada que compruebe:

1. Alinear la versión de `dotnet-ef` con EF Core 10 usado por el proyecto.
2. Generar el script completo de migraciones.
3. Aplicar todas las migraciones sobre una base temporal con un nombre nuevo y confirmar que funcionan desde cero.
4. Confirmar que el aprovisionador rechaza una base que ya existe sin modificarla.
5. Confirmar que dos onboardings simultáneos con el mismo nombre no crean dos gimnasios master.
6. Registrar el gimnasio en `MindFitMaster` con `Activo = false`.
7. Crear la base tenant.
8. Verificar la connection string, `CanConnectAsync()` y `__EFMigrationsHistory`.
9. Confirmar los grupos, 30 permisos, ocho formularios y sus relaciones.
10. Confirmar los siete días, 24 rangos y 168 combinaciones `DiaRangoHorario`.
11. Confirmar que las combinaciones comienzan inactivas y con cupo cero.
12. Confirmar los 12 grupos musculares y tres tipos de ejercicio.
13. Ejecutar el seed una segunda vez y comprobar que no genera duplicados.
14. Confirmar el responsable asociado a `Admin`.
15. Confirmar el cambio final a `Activo = true`.
16. Confirmar la aparición en `GET /api/Gyms/activos`.
17. Iniciar sesión usando el nuevo `X-Gym-Id`.
18. Ejecutar `dotnet build` y `npm run build`.

La base temporal de validación tendrá un nombre inequívoco y no se eliminará automáticamente desde la aplicación. Cualquier limpieza posterior será una operación manual, separada y revisada.

## Contratos

No cambian:

- `POST /api/Gyms/onboarding`.
- `GET /api/Gyms/activos`.
- `POST /api/Auth/login`.
- DTO de onboarding y modelos tenant.

La respuesta exitosa de onboarding pasa a significar que el gimnasio está listo para iniciar sesión.

El único cambio de modelo previsto es el índice único de `Gym.NombreGym` en master.

## Eliminado del plan anterior

- Estados detallados de aprovisionamiento.
- Fechas de intentos y activación.
- Reintentos automáticos.
- Endpoint administrativo de reintento.
- Workers y colas.
- Códigos de error personalizados.
- Refactorización general del middleware.
- Cambios de secretos, SMTP o seguridad general.
- Infraestructura avanzada de pruebas.
- Seed genérico para futuras versiones.

## Simplificaciones

- `Activo` continúa siendo el único indicador.
- El onboarding será sincrónico.
- Se utilizarán dos clases concretas pequeñas, sin interfaces nuevas.
- Los errores solamente se registrarán en logs.
- Las migraciones existentes se reutilizarán sin modificarlas.
- El seed contendrá únicamente datos estructurales confirmados.
- El seed será repetible y resolverá relaciones por claves naturales.

## Decisiones basadas en el código

- `Database.MigrateAsync()` puede aplicar las migraciones existentes de `MindFitIntelligenceContext`.
- El contexto de aprovisionamiento debe ser independiente porque el gimnasio permanece inactivo durante la creación.
- El grupo administrador real es `Admin`.
- El login necesita un `Usuario` tenant y no utiliza `UsuarioMaster`.
- Los formularios y permisos son necesarios para autorización y navegación.
- `RangoHorario` es un catálogo estructural sin operaciones de creación.
- `DiaRangoHorario` también debe inicializarse porque su servicio solamente consulta y actualiza registros existentes.
- Las 168 combinaciones se crean inactivas para que cada gimnasio configure sus horarios.
- `MigrateAsync()` solo se ejecutará después de confirmar que la base no existe.
- Los nombres duplicados se resolverán mediante una restricción única en la base master, no solamente con una validación en C#.

## Riesgos pendientes

- La cuenta SQL debe poder crear bases de datos.
- El request puede tardar mientras se ejecutan las migraciones; Axios actualmente no tiene un timeout propio, pero cualquier proxy de despliegue deberá permitir el tiempo medido durante la prueba.
- Un fallo puede dejar una base parcial e inactiva.
- Si falla únicamente la activación final, será necesaria una corrección manual porque no habrá endpoint de reintento en esta primera versión.
- Los ejercicios existentes siguen pendientes de clasificación como catálogo común o datos particulares.

## Archivos esperados

- `Docs/Backend/automatizacion-bd/PLAN-automatizacion.md`.
- `Services/GymPublicoService.cs`.
- `Repository/GymRepository.cs`.
- `Repository/Interfaces/IGymRepository.cs`.
- `Models/Master/Gym.cs`.
- `Program.cs`.
- Nueva `Services/TenantProvisioningService.cs`.
- Nueva `Services/TenantInitialDataSeeder.cs`.
- Nueva migración de `MindFitMasterContext` para la unicidad de `NombreGym`.
- `Frontend/src/pages/GymOnboardingPage.tsx`.

No se esperan cambios en controladores, DTOs, entidades tenant ni migraciones tenant existentes.
