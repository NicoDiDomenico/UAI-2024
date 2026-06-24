# Log de implementación - Etapa 3

Fecha: 2026-06-20

> Actualizacion posterior: el grupo `Responsable` documentado en esta etapa fue reemplazado por `Asistente`, con ocho permisos de gestion de turnos y socios. Ver `LOG-CAMBIO-GRUPO-ASISTENTE.md`. Los resultados restantes de esta prueba historica se conservan sin cambios.

## Alcance realizado

Se implementó únicamente la Etapa 3 de `PLAN-automatizacion.md`.

Esta etapa agrega un seed estructural e idempotente para una base tenant ya creada y migrada.

Todavía no se crea el administrador tenant, no se activa el gimnasio y el onboarding no ejecuta el aprovisionamiento.

## Fuente de los datos

Los valores se extrajeron en modo de solo lectura de la base tenant actual `MindFitIntelligence` y se contrastaron con:

- las políticas de autorización de `Program.cs`;
- los modelos y enums;
- las relaciones `GrupoPermiso` y `FormularioPermiso`;
- los catálogos utilizados por los servicios actuales.

El seed no consulta ni clona la base actual durante el onboarding. Los valores confirmados quedaron escritos explícitamente en código.

## Servicio agregado

Se creó:

```text
Services/TenantInitialDataSeeder.cs
```

Su operación pública es:

```text
SeedAsync(MindFitIntelligenceContext context, CancellationToken cancellationToken)
```

El seeder recibe un contexto tenant ya migrado. No crea ni elimina bases y no abre una transacción propia, para que posteriormente pueda compartir una transacción con la creación del administrador.

El servicio se registró como scoped en `Program.cs`, pero todavía no se invoca desde onboarding.

## Datos estructurales incorporados

### Grupos y permisos

- 4 grupos: `Admin`, `Responsable`, `Socio` y `Entrenador`.
- 30 permisos con sus códigos y descripciones actuales.
- 36 relaciones `GrupoPermiso`.

Distribución confirmada:

- `Admin`: 30 permisos.
- `Responsable`: 2 permisos.
- `Entrenador`: 4 permisos.
- `Socio`: 0 permisos directos.

No se incluyó el grupo de prueba `Invitado`.

### Formularios

- 8 formularios.
- 30 relaciones `FormularioPermiso`.

### Catálogos

- 7 días de la semana.
- 12 grupos musculares con sus identificadores del mapa anatómico.
- 3 tipos de ejercicio.
- 24 rangos horarios de una hora.
- 168 combinaciones `DiaRangoHorario`.

El rango final quedó registrado correctamente como:

```text
23:00 - 00:00
```

Todas las combinaciones `DiaRangoHorario` comienzan con:

```text
Activo = false
CupoMaximo = 0
```

## Idempotencia

El seed resuelve registros mediante claves naturales:

- `Grupo.Nombre`.
- `Permiso.Codigo`.
- `Formulario.NombreFormulario`.
- `Dia.NombreDia`.
- enum `Musculo`.
- enum `TipoDeEjercicio`.
- combinación `RangoHorario.HoraDesde` y `HoraHasta`.
- claves compuestas de las tablas de relaciones.

No copia IDs de la base actual.

Solo inserta registros o relaciones faltantes. No elimina ni reemplaza información existente.

## Utilidad de verificación

Se agregó:

```text
Backend/Tools/TenantSeedVerifier.cs
```

La utilidad:

- recibe la connection string mediante un argumento;
- no contiene credenciales persistidas;
- ejecuta el seed dos veces;
- imprime los conteos finales;
- no crea ni elimina bases.

No forma parte del proyecto web ni se expone como endpoint.

## Prueba sobre la base de validación

Se utilizó la base creada en la Etapa 2:

```text
MindFit_E2Validation_20260620_a7f3c921
```

Primera ejecución:

```text
Entidades agregadas: 88
Relaciones agregadas: 234
```

Desglose:

```text
4 grupos
30 permisos
8 formularios
7 días
12 grupos musculares
3 tipos de ejercicio
24 rangos horarios
36 relaciones grupo-permiso
30 relaciones formulario-permiso
168 relaciones día-rango
```

Segunda ejecución, usando el mismo contexto:

```text
Entidades agregadas: 0
Relaciones agregadas: 0
```

Nueva ejecución en un proceso independiente:

```text
Entidades agregadas: 0
Relaciones agregadas: 0
```

Esto confirma que la idempotencia no depende del seguimiento de entidades de un mismo `DbContext`.

## Verificación SQL directa

Conteos finales:

```text
Grupos: 4
Permisos: 30
GrupoPermisos: 36
Formularios: 8
FormularioPermisos: 30
Días: 7
GruposMusculares: 12
TiposEjercicio: 3
RangosHorarios: 24
DiaRangosHorarios: 168
Permisos de Admin: 30
DiaRangoHorario inactivos con cupo cero: 168
Rango 23:00 - 00:00: 1
```

Las tablas operativas permanecen vacías:

```text
Usuarios: 0
Socios: 0
Turnos: 0
Cuotas: 0
Rutinas: 0
Ejercicios: 0
Máquinas: 0
Equipamientos: 0
```

La base de validación E2 ahora contiene las migraciones y exclusivamente el seed estructural de esta etapa.

## Compilación

Comando:

```text
dotnet build --no-restore
```

Resultado final:

```text
0 errores
0 advertencias
```

## Incidente controlado

La primera ejecución de la utilidad de archivo intentó usar NativeAOT, modalidad incompatible con la creación dinámica del modelo EF. El proceso falló antes de ejecutar consultas o insertar datos. Se agregó `PublishAot=false` a la utilidad y la prueba posterior terminó correctamente.

## Archivos modificados

- `Backend/MindFit_Intelligence_Backend/Program.cs`

## Archivos creados

- `Backend/MindFit_Intelligence_Backend/Services/TenantInitialDataSeeder.cs`
- `Backend/Tools/TenantSeedVerifier.cs`
- `Docs/Backend/automatizacion-bd/LOG-PLAN-automatizacion-E3.md`

## Fuera de alcance

Quedó expresamente fuera de esta etapa:

- crear el administrador tenant;
- hashear y guardar su contraseña tenant;
- relacionar el primer usuario con `Admin`;
- integrar seed y administrador dentro de una transacción común;
- invocar migraciones o seed desde onboarding;
- activar el gimnasio;
- modificar el frontend;
- eliminar automáticamente la base de validación.

## Pendiente para las etapas siguientes

- Crear el primer `Usuario` y `PersonaResponsable` tenant.
- Asociar el usuario al grupo `Admin`.
- Ejecutar seed y administrador dentro de una transacción tenant.
- Conectar el flujo completo al onboarding solamente después de verificar esas operaciones.
