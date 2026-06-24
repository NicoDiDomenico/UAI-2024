# Log de implementacion - Etapa 4

Fecha: 2026-06-20

## Alcance realizado

Se implemento unicamente la Etapa 4 de `PLAN-automatizacion.md`.

Esta etapa agrega la preparacion transaccional de una base tenant ya migrada para:

- ejecutar el seed estructural de la Etapa 3;
- crear el primer `Usuario` tenant;
- crear su `PersonaResponsable`;
- asignarle el grupo `Admin`;
- guardar unicamente el hash de la contrasena.

El servicio todavia no se invoca desde `POST /api/Gyms/onboarding`. El gimnasio tampoco se activa en esta etapa.

## Servicio extendido

Se extendio:

```text
Services/TenantProvisioningService.cs
```

Se agrego la operacion:

```text
InicializarDatosYAdministradorAsync(connectionString, administratorDto, cancellationToken)
```

Antes de modificar datos, la operacion:

1. valida los datos obligatorios y longitudes del administrador;
2. valida la connection string con las protecciones de la Etapa 2;
3. comprueba que la base sea accesible;
4. comprueba que tenga migraciones aplicadas y ninguna pendiente.

No crea la base ni vuelve a ejecutar migraciones. Es una operacion separada que se utilizara despues de `CrearYAplicarMigracionesAsync` al integrar el onboarding.

## Transaccion tenant

El seed y la creacion del administrador se ejecutan dentro de una unica transaccion local de `MindFitIntelligenceContext`.

Si falla cualquiera de esas operaciones:

- se revierte la transaccion;
- no se activa ningun gimnasio;
- se registra el nombre seguro de la base y la excepcion;
- no se elimina la base ni se borran datos existentes.

No se agrego una transaccion distribuida con `MindFitMasterContext`.

## Administrador creado

La implementacion crea:

- un `Usuario` con el username recibido en el onboarding;
- una `PersonaResponsable` con todos los campos disponibles en el DTO actual;
- una relacion `UsuarioGrupo` con el grupo obtenido por el nombre natural `Admin`.

Se rechaza la operacion si:

- el username ya existe en la base tenant;
- no existe el grupo estructural `Admin`;
- faltan datos obligatorios o se superan las longitudes de las columnas.

Los IDs del usuario, persona y grupo no se copian ni se fijan manualmente.

## Contrasena

La contrasena se transforma mediante:

```text
PasswordHasher<Usuario>
```

La contrasena plana:

- no se persiste;
- no se incluye en logs;
- no se incluye en mensajes de error;
- no se incorpora a la herramienta de verificacion.

La verificacion utilizo una variable de entorno temporal, eliminada al finalizar el comando.

## Herramienta de verificacion

Se creo:

```text
Backend/Tools/TenantAdministratorVerifier.cs
```

Es una aplicacion de archivo independiente y no forma parte del ejecutable del backend.

La prueba se ejecuto sobre la base temporal conservada desde la Etapa 2:

```text
MindFit_E2Validation_20260620_a7f3c921
```

Resultado:

```text
UsuarioCreado=e4_admin_validation
PersonaResponsable=1
GrupoAdmin=True
PermisosEfectivos=30
PasswordHashValido=True
RefreshTokenNulo=True
DuplicadoRechazado=True
UsuariosTrasRollback=1
```

El hash fue verificado con `PasswordHasher<Usuario>.VerifyHashedPassword`, el mismo mecanismo utilizado por el flujo de autenticacion.

La segunda ejecucion dentro de la prueba intento crear el mismo username. Fue rechazada, la transaccion se revirtio y el total de usuarios permanecio en uno.

## Seed despues de la prueba

Se ejecuto nuevamente la verificacion idempotente de la Etapa 3.

Resultado:

```text
Grupos=4
Permisos=30
GrupoPermisos=36
Formularios=8
FormularioPermisos=30
Dias=7
GruposMusculares=12
TiposEjercicio=3
RangosHorarios=24
DiaRangosHorarios=168
```

Las dos ejecuciones del seed informaron cero altas, por lo que la prueba de Etapa 4 no duplico ni altero datos estructurales.

## Compilacion

Comando:

```text
dotnet build Backend/MindFit_Intelligence_Backend/MindFit_Intelligence_Backend.csproj --no-restore
```

Resultado:

```text
0 errores
0 advertencias
```

Tambien se revisaron los archivos de esta etapa sin encontrar problemas de formato.

## Archivos modificados

- `Backend/MindFit_Intelligence_Backend/Services/TenantProvisioningService.cs`

## Archivos creados

- `Backend/Tools/TenantAdministratorVerifier.cs`
- `Docs/Backend/automatizacion-bd/LOG-PLAN-automatizacion-E4.md`

## Fuera de alcance

Quedo expresamente fuera de esta etapa:

- invocar migraciones, seed y administrador desde onboarding;
- activar el gimnasio en la base master;
- probar `POST /api/Auth/login` mediante HTTP;
- modificar controladores, DTOs o entidades;
- modificar el frontend;
- eliminar automaticamente la base temporal.

El login HTTP solo podra probarse en el flujo integrado cuando la Etapa 5 active el gimnasio y el resolver tenant pueda encontrarlo mediante `X-Gym-Id`.

## Pendiente para la etapa siguiente

- integrar las operaciones de aprovisionamiento con el onboarding;
- cambiar `Gym.Activo` a `true` solo despues de migraciones, seed y administrador exitosos;
- comprobar que el gimnasio aparezca en `GET /api/Gyms/activos`;
- comprobar el login real con el nuevo `X-Gym-Id`.
