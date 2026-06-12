# Implementation Log - Cancelar Turno Socio

## Primera Implementacion - Cancelar Turno Socio

Fecha: 2026-06-10

## Alcance implementado

Se implemento la cancelacion real del turno desde `Frontend/src/pages/socio/SocioInicioPage.tsx` para usuarios Socio.

El flujo ahora:

- valida el estado del turno antes de abrir confirmacion
- abre un modal de confirmacion si el turno esta en `EnCurso`
- ejecuta `PATCH /Turno/socio/cancelar/{idTurno}`
- refresca la grilla con `GET /Turno/socio`

No se implemento el flujo real de `Nuevo Turno`.

## Archivos creados o modificados

- `Frontend/src/pages/socio/SocioInicioPage.tsx`
- `Frontend/src/services/turnosService.ts`
- `Docs/Frontend/E3-Turnos/E3P10-Cancelar-Turno-Socio/IMPLEMENTATION_LOG_agregar-cancelar-turno-socio.md`

## Decisiones importantes

- Se agrego `turnosService.cancelarTurnoSocio(idTurno)` sin tocar los endpoints del asistente.
- La validacion de estado se resolvio en `SocioInicioPage.tsx`, no en el service.
- Se mantuvo `estadoTurno` como `string`.
- Solo se permite cancelar cuando `estadoTurno === "EnCurso"`.
- Para `Cancelado`, `Finalizado`, `Vencido` o un estado desconocido, no se llama al endpoint.
- Se reutilizo la estructura visual del modal de confirmacion ya usada en el flujo del asistente.

## Integracion frontend/backend

- Cancelacion del Socio:
  - `PATCH /Turno/socio/cancelar/{idTurno}`
- Recarga de grilla:
  - `GET /Turno/socio`

La llamada se realiza con el `apiClient` existente, por lo que mantiene:

- `Authorization: Bearer {accessToken}`
- `X-Gym-Id: {idGym}`

No se envia `idUsuarioSocio`.

## Validaciones implementadas

- El boton `Cancelar Turno` permanece deshabilitado sin seleccion.
- Antes de abrir confirmacion se valida el estado del turno seleccionado.
- Mensajes bloqueantes implementados:
  - `Cancelado` -> `El turno ya fue cancelado.`
  - `Finalizado` -> `No es posible cancelar un turno finalizado.`
  - `Vencido` -> `No es posible cancelar un turno vencido.`
  - estado desconocido -> `No es posible cancelar el turno seleccionado por su estado actual.`

## Manejo de estados, loading y errores

- Loading de grilla mantenido en la pantalla.
- Loading de submit dentro del modal de confirmacion.
- Doble submit bloqueado mientras la cancelacion esta en curso.
- Exito:
  - muestra `El turno fue cancelado correctamente.`
  - cierra modal
  - limpia seleccion
  - refresca grilla
- `409 Conflict`:
  - muestra el mensaje real usando `getCancelarTurnoErrorMessage()`
  - conserva seleccion
- `404 Not Found`:
  - muestra el mensaje real cuando esta disponible
  - cierra modal
  - limpia seleccion
  - refresca grilla para reconciliar estado

## TODOs

- Implementar luego el flujo real de `Nuevo Turno`.
- Si mas adelante se necesita reutilizacion adicional, evaluar extraer el modal de confirmacion a un componente compartido.

## Ajustes posteriores

Fecha: 2026-06-10

## Cambios aplicados

- El mensaje de exito `El turno fue cancelado correctamente.` ahora se oculta automaticamente despues de 4 segundos en `SocioInicioPage.tsx`.
- La grilla compartida de turnos ahora ordena los registros por `fechaAlta` en orden descendente, mostrando primero la fecha mas reciente.

## Archivos modificados

- `Frontend/src/pages/socio/SocioInicioPage.tsx`
- `Frontend/src/components/turnos/TurnosHistorialGrid.tsx`
- `Docs/Frontend/E3-Turnos/E3P10-Cancelar-Turno-Socio/IMPLEMENTATION_LOG_agregar-cancelar-turno-socio.md`

## Decisiones importantes

- El autohide del aviso se limito al mensaje de exito de cancelacion para no interferir con otros avisos de la pantalla.
- El orden descendente se implemento dentro de `TurnosHistorialGrid.tsx` para que impacte en todos los lugares donde se reutiliza el componente.
