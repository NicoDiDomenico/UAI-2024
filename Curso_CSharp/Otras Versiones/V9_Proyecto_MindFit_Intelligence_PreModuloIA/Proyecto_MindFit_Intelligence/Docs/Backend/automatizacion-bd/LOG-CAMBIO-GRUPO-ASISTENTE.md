# Cambio de grupo Responsable a Asistente

Fecha: 2026-06-20

## Alcance

Se modifico el seed utilizado durante el onboarding de nuevas bases tenant.

El grupo de permisos `Responsable` fue reemplazado por:

```text
Asistente
```

El cambio se limita al grupo de autorizacion. No se renombraron:

- la entidad `PersonaResponsable`;
- los DTOs de responsables;
- los endpoints de usuarios responsables;
- las relaciones de turnos con responsables;
- las politicas cuyos nombres describen operaciones sobre responsables.

Esos conceptos representan el tipo de persona y no el grupo de permisos.

## Permisos de Asistente

El grupo `Asistente` se crea con exactamente estos ocho permisos:

```text
AGREGAR_TURNO
CANCELAR_TURNO
VALIDAR_INGRESO
CREAR_USUARIO_SOCIO
EDITAR_USUARIO_SOCIO
ELIMINAR_USUARIO_SOCIO
ELIMINAR_USUARIO_SOCIO_DEFINITIVAMENTE
CAMBIAR_CONTRASENA_SOCIO
```

## Matriz resultante

Las bases tenant creadas desde este cambio contienen:

```text
Admin=30
Asistente=8
Entrenador=4
Socio=0
```

El total esperado es:

```text
Grupos=4
GrupoPermisos=42
```

## Idempotencia

El seed mantiene su comportamiento idempotente. Si se ejecuta nuevamente sobre una base creada con esta version:

- no agrega grupos duplicados;
- no agrega permisos duplicados;
- no agrega relaciones duplicadas.

## Verificacion

Se creo un gimnasio mediante el onboarding completo:

```text
Nombre: Assistant Seed Validation 20260620223141
IdGym: 12
Base: MindFit_AssistantSeedValidation20260620223141_70b51e14
```

La inspeccion SQL confirmo:

```text
Admin=30
Asistente=8
Entrenador=4
Socio=0
Grupo Responsable presente: false
GrupoPermisos totales: 42
```

Tambien se comprobaron individualmente los ocho codigos asignados a `Asistente`.

El seed se ejecuto dos veces mas sobre esa misma base. Ambas ejecuciones informaron:

```text
Entidades agregadas: 0
Relaciones agregadas: 0
```

## Compilacion

La compilacion normal estaba bloqueada por una instancia del backend abierta por el usuario (`PID 21172`). No se detuvo ese proceso.

Se compilo el mismo proyecto hacia una carpeta temporal independiente.

Resultado:

```text
0 errores
0 advertencias
```

## Archivos modificados

- `Backend/MindFit_Intelligence_Backend/Services/TenantInitialDataSeeder.cs`
- `Docs/Backend/automatizacion-bd/PLAN-automatizacion.md`
- `Docs/Backend/automatizacion-bd/LOG-PLAN-automatizacion-E3.md`

## Archivos creados

- `Docs/Backend/automatizacion-bd/LOG-CAMBIO-GRUPO-ASISTENTE.md`

## Bases existentes

Este cambio se aplica automaticamente a las bases creadas desde ahora.

Las bases tenant que ya existian no fueron modificadas. Cambiar sus grupos requiere una migracion de datos separada porque puede haber usuarios asignados al grupo `Responsable`; hacerlo automaticamente podria alterar autorizaciones existentes.
