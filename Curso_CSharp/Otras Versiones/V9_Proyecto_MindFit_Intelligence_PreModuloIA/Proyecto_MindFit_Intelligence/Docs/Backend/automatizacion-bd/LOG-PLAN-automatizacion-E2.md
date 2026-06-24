# Log de implementación - Etapa 2

Fecha: 2026-06-20

## Alcance realizado

Se implementó únicamente la Etapa 2 de `PLAN-automatizacion.md`.

Esta etapa agrega la infraestructura para:

- validar una connection string tenant;
- confirmar que la base destino no exista;
- crear la base mediante las migraciones existentes;
- verificar conexión, historial y migraciones pendientes.

El servicio todavía no se invoca desde `POST /api/Gyms/onboarding`. Por lo tanto, el onboarding actual continúa registrando únicamente los datos master de la Etapa 1.

## Herramienta EF

Se confirmó que la herramienta disponible es:

```text
dotnet-ef 10.0.3
```

Coincide con los paquetes EF Core `10.0.3` utilizados por el proyecto. La advertencia de versión registrada durante la Etapa 1 ya no está presente.

## Servicio agregado

Se creó:

```text
Services/TenantProvisioningService.cs
```

El servicio se registró como scoped en `Program.cs`, pero todavía no forma parte del flujo de onboarding.

Su operación pública es:

```text
CrearYAplicarMigracionesAsync(connectionString, cancellationToken)
```

## Validación de la connection string

El servicio utiliza `SqlConnectionStringBuilder` y exige:

- una connection string no vacía y con formato válido;
- un servidor definido;
- una base de datos definida;
- un nombre que comience con `MindFit_`;
- un máximo de 128 caracteres;
- únicamente caracteres alfanuméricos y guion bajo;
- coincidencia de servidor y base con `TenantTemplate`.

No se registra la connection string completa ni sus credenciales en logs.

## Protección de bases existentes

Antes de ejecutar migraciones:

1. Se construye una conexión al catálogo `master` del mismo servidor.
2. Se ejecuta `SELECT DB_ID(@databaseName)` usando un parámetro SQL.
3. Si SQL Server devuelve un identificador, el servicio lanza una excepción y no ejecuta `MigrateAsync()`.

El servicio no contiene ni utiliza:

- `EnsureCreated`;
- `EnsureDeleted`;
- `DROP DATABASE`;
- `TRUNCATE`;
- eliminación general de registros.

## Migración y verificación

Cuando la base no existe:

1. Se crea un `DbContextOptionsBuilder<MindFitIntelligenceContext>` independiente del tenant de la petición.
2. Se configura `CommandTimeout = 120` segundos únicamente para este contexto.
3. Se ejecuta `Database.MigrateAsync()`.
4. Se comprueba `CanConnectAsync()`.
5. Se consultan las migraciones aplicadas.
6. Se comprueba que no queden migraciones pendientes.

Los logs identifican solamente el nombre seguro de la base tenant.

## Prueba desde una base vacía

Se creó una base de validación con nombre único:

```text
MindFit_E2Validation_20260620_a7f3c921
```

Antes de crearla se confirmó:

```text
DB_ID = NULL
```

Se aplicó la cadena completa de migraciones tenant mediante EF Core 10.0.3.

Resultado:

```text
Migraciones aplicadas: 18
Tablas de usuario creadas: 31
Última migración: 20260421190807_Formulario
Cambios de modelo pendientes: ninguno
```

La base se conserva deliberadamente para inspección. No fue eliminada automáticamente y no contiene seed, usuarios ni datos operativos.

## Compilación

Comando:

```text
dotnet build --no-restore
```

Resultado:

```text
0 errores
3 advertencias de nulabilidad preexistentes en Repository/UsuarioRepository.cs
```

Las advertencias se encuentran fuera del alcance de esta etapa.

## Incidentes controlados

- El primer script de comprobación de `DB_ID` interpretó incorrectamente la salida de `sqlcmd`. La prueba se canceló antes de crear la base o ejecutar migraciones. Se repitió la consulta de forma directa y confirmó `NULL`.
- `Program.cs` utiliza Windows-1252. Para editarlo se realizó una conversión temporal y luego se restauró su codificación original. El diff final de ese archivo contiene únicamente el registro de `TenantProvisioningService`.

## Archivos modificados

- `Backend/MindFit_Intelligence_Backend/Program.cs`

## Archivos creados

- `Backend/MindFit_Intelligence_Backend/Services/TenantProvisioningService.cs`
- `Docs/Backend/automatizacion-bd/LOG-PLAN-automatizacion-E2.md`

## Fuera de alcance

Quedó expresamente fuera de esta etapa:

- invocar el aprovisionador desde onboarding;
- cargar grupos, permisos, formularios o catálogos;
- crear los 24 rangos horarios;
- crear las 168 combinaciones `DiaRangoHorario`;
- crear el administrador tenant;
- activar el gimnasio;
- modificar el frontend;
- eliminar automáticamente la base de validación.

## Pendiente para las etapas siguientes

- Implementar el seed estructural idempotente.
- Integrar seed y administrador dentro de una transacción tenant.
- Conectar el aprovisionamiento completo al onboarding únicamente cuando todas las etapas previas estén verificadas.
- Probar mediante el flujo integrado que una base existente sea rechazada antes de `MigrateAsync()`.
