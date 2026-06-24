# Log de implementación - Etapa 1

Fecha: 2026-06-20

## Alcance realizado

Se implementó únicamente la Etapa 1 de `PLAN-automatizacion.md`.

Esta etapa registra en `MindFitMaster`:

- el gimnasio inactivo;
- el usuario master;
- la persona responsable master.

Todavía no se crean bases tenant, no se ejecutan migraciones tenant y no se modificó el frontend.

## Cambios realizados

### Normalización y nombre de base

- El nombre del gimnasio elimina espacios al principio y al final.
- Los espacios internos consecutivos se convierten en un único espacio.
- Se valida el límite de 150 caracteres definido por el modelo.
- El nombre sugerido para la futura base tenant elimina diacríticos y caracteres no alfanuméricos.
- Se agrega un sufijo aleatorio de ocho caracteres para evitar colisiones entre nombres de bases.

Ejemplo conceptual:

```text
Nombre gimnasio: Gimnasio Águila
Nombre base: MindFit_GimnasioAguila_a1b2c3d4
```

### Unicidad

- Se agregó una consulta previa para detectar nombres de gimnasio existentes.
- Se agregó una consulta previa para detectar usernames master existentes.
- Se agregó el índice único `IX_Gym_NombreGym` en `MindFitMaster.Gym`.
- Los errores SQL de unicidad `2601` y `2627` se traducen a una respuesta HTTP `409 Conflict`.
- Otros errores de persistencia no se presentan como duplicados y conservan la respuesta HTTP `500`.

### Transacción master

- `GymRepository` y `UsuarioMasterRepository` continúan usando la misma instancia scoped de `MindFitMasterContext`.
- El alta de `Gym`, `UsuarioMaster` y `PersonaResponsableMaster` se ejecuta dentro de una transacción explícita.
- Si falla el segundo guardado, se revierte también la inserción previa del gimnasio.
- El gimnasio continúa creándose con `Activo = false`.

### Respuesta del endpoint

- `POST /api/Gyms/onboarding` conserva su request y su respuesta exitosa.
- Los conflictos de nombre de gimnasio o username devuelven `409 Conflict` con un mensaje comprensible.

## Migración master

Se generó:

```text
Migrations/Master/20260620214349_UniqueGymName.cs
```

La migración solamente ejecuta:

```sql
CREATE UNIQUE INDEX [IX_Gym_NombreGym] ON [Gym] ([NombreGym]);
```

La migración fue aplicada correctamente a `MindFitMaster`.

Verificaciones posteriores:

- `IX_Gym_NombreGym` existe.
- El índice tiene `is_unique = 1`.
- `__EFMigrationsHistory` contiene `20260620214349_UniqueGymName` con EF `10.0.3`.
- La tabla `Gym` conserva sus cuatro registros originales.
- EF informa que no existen cambios pendientes entre el modelo master y su snapshot.

## Pruebas realizadas

### Datos previos

Antes de crear el índice se comprobó que no existían:

- nombres de gimnasio duplicados;
- usernames master duplicados.

### Compilación

Comando:

```text
dotnet build --no-restore
```

Resultado final:

```text
0 errores
0 advertencias
```

### Conflicto por nombre normalizado

Se envió un onboarding usando:

```text
"  Mundial   Gym  "
```

El nombre se normalizó a `Mundial Gym`, detectó el registro existente y respondió:

```text
HTTP 409
```

### Rollback de la transacción

Se forzó un error de escritura del usuario después de iniciar el alta de un gimnasio temporal.

Resultado:

```text
HTTP 500
Total de gimnasios: 4
Filas del gimnasio temporal: 0
Filas del usuario temporal: 0
```

Esto confirma que el gimnasio no queda guardado parcialmente cuando falla el usuario master.

## Incidentes controlados durante la implementación

- El primer `database update --no-build` fue bloqueado por EF porque el ensamblado todavía no incluía la migración recién creada. No se modificó la base en ese intento. Se recompiló y luego la migración se aplicó correctamente.
- Un servidor local de prueba quedó ejecutándose después de una sesión de herramienta y bloqueó temporalmente el ejecutable. Se detuvo únicamente ese proceso y la compilación final terminó correctamente.

## Advertencias pendientes

- La herramienta global `dotnet-ef` es `8.0.11`, mientras el proyecto utiliza EF Core `10.0.3`. La migración se generó, aplicó y validó correctamente, pero la versión deberá alinearse antes de probar las migraciones tenant de la Etapa 2.
- El archivo del plan y este log todavía aparecen sin seguimiento en Git.

## Archivos modificados

- `Controllers/GymsController.cs`
- `Models/Master/MindFitMasterContext.cs`
- `Repository/GymRepository.cs`
- `Repository/UsuarioMasterRepository.cs`
- `Repository/Interfaces/IGymRepository.cs`
- `Repository/Interfaces/IUsuarioMasterRepository.cs`
- `Services/GymPublicoService.cs`
- `Migrations/Master/MindFitMasterContextModelSnapshot.cs`

## Archivos creados

- `Services/GymOnboardingConflictException.cs`
- `Migrations/Master/20260620214349_UniqueGymName.cs`
- `Migrations/Master/20260620214349_UniqueGymName.Designer.cs`
- `Docs/Backend/automatizacion-bd/LOG-PLAN-automatizacion-E1.md`

## Fuera de alcance

Quedó expresamente fuera de esta etapa:

- validar la existencia física de la futura base tenant;
- crear la base tenant;
- ejecutar `MigrateAsync()` sobre la base tenant;
- cargar el seed estructural;
- crear el administrador tenant;
- activar el gimnasio;
- modificar el mensaje del frontend.
