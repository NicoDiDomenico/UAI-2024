# Implementation Log - Nuevo Turno Socio

## Implementacion

Fecha: 2026-06-10

## Alcance implementado

Se implemento el flujo real de `Nuevo Turno` para el Socio autenticado desde `Frontend/src/pages/socio/SocioInicioPage.tsx`.

El boton `Nuevo Turno` deja de mostrar el placeholder y ahora abre un modal real que permite:

- seleccionar fecha
- cargar disponibilidad por dia
- seleccionar rango horario activo
- seleccionar entrenador
- registrar el turno con el endpoint del Socio

## Archivos creados o modificados

- `Frontend/src/components/socio/NuevoTurnoSocioModal.tsx`
- `Frontend/src/pages/socio/SocioInicioPage.tsx`
- `Frontend/src/services/turnosService.ts`
- `Docs/Frontend/E3-Turnos/E3P11-Nuevo-Turno-Socio/IMPLEMENTATION_LOG_nuevo-turno-socio-plan.md`

## Decisiones importantes

- Se creo `NuevoTurnoSocioModal.tsx` para evitar pasar props falsas de socio al modal existente del asistente.
- Se reutilizaron tipos, helpers, service y estilos existentes del flujo de nuevo turno del asistente.
- Se agrego `turnosService.registrarTurnoSocio(request)` sin modificar `registrarTurnoAsistente`.
- `TurnoInsertDto.cs` define `IdUsuarioSocio` como `int` no nullable, por eso el frontend envia `idUsuarioSocio: 0`.
- El backend sobrescribe `IdUsuarioSocio` desde el JWT en `POST /Turno/socio/registrar`.
- No se usa `session.datosPersonales.id` para armar el request.

## Integracion frontend/backend

Disponibilidad:

- `GET /DiaRangoHorario/grilla-por-dia?fecha=yyyy-mm-dd`

Registro:

- `POST /Turno/socio/registrar`

No se usa:

- `POST /Turno/asistente/registrar`

El `apiClient` existente mantiene automaticamente:

- `Authorization: Bearer {accessToken}`
- `X-Gym-Id: {idGym}`

## Validaciones implementadas

Antes de registrar se valida:

- fecha seleccionada
- rango horario seleccionado
- entrenador seleccionado
- cupo disponible con `cupoActual < cupoMaximo`

No se valida socio seleccionado porque el Socio se obtiene desde el JWT.

## Manejo de estados, loading y errores

- Loading al cargar disponibilidad.
- Loading al registrar turno.
- Bloqueo del boton `Registrar Turno` durante el submit.
- Error de carga de disponibilidad con `getDisponibilidadTurnoErrorMessage`.
- Error de registro con `getRegistrarTurnoErrorMessage`.
- Al registrar correctamente:
  - cierra el modal
  - limpia la seleccion de turno actual
  - refresca la grilla con `GET /Turno/socio`

## TODOs

- Evaluar mas adelante si conviene extraer un componente compartido entre `NuevoTurnoModal` y `NuevoTurnoSocioModal` si ambos flujos siguen evolucionando en paralelo.
