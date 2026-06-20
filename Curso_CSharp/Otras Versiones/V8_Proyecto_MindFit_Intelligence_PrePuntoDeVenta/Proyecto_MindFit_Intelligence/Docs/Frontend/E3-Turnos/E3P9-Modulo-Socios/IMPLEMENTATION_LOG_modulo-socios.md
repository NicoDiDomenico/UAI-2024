# Implementation Log - Modulo Socios

## Primera Implementacion

Fecha: 2026-06-10

## Alcance implementado

Se adapto el frontend al nuevo contrato de respuesta de `POST /api/Auth/login`, incorporando `datosPersonales` a los tipos de autenticacion, a la sesion en memoria y a la persistencia en `localStorage`.

No se implemento redireccion por rol, no se creo `SocioInicioPage.tsx` y no se trabajo sobre la grilla de turnos del socio.

## Archivos modificados

- `Frontend/src/types/auth.ts`
- `Frontend/src/utils/authStorage.ts`
- `Frontend/src/contexts/AuthContext.tsx`

## Decisiones importantes

- Se agrego el tipo `DatosPersonales` alineado al DTO backend:
  - `id`
  - `nombre`
  - `apellido`
  - `rol`
- `TokenResponse` ahora requiere `datosPersonales`, porque el nuevo contrato de login lo devuelve como parte de la response.
- `AuthSession` permite `datosPersonales: null` al hidratar desde `localStorage`, para mantener compatibilidad con sesiones viejas que todavia no tengan la nueva clave guardada.
- Se agrego la clave namespaced `mindfit.datosPersonales`.
- No se guarda `username` ni `password`.

## Integracion frontend/backend

`AuthContext` ahora conserva `response.datosPersonales` al construir la sesion luego de un login exitoso.

`authStorage.ts` ahora:

- guarda `datosPersonales` al iniciar sesion
- lee `datosPersonales` al hidratar la sesion
- elimina `datosPersonales` al cerrar sesion

## Validaciones implementadas

Al hidratar desde `localStorage`, `datosPersonales` se parsea con validaciones basicas:

- debe ser un objeto
- `id` debe ser numerico
- `nombre` y `apellido` se conservan si son string; si no, quedan en `null`
- `rol` se conserva como `string[]` filtrando valores no string; si no hay array, queda en `null`

Si la clave existe pero contiene JSON invalido, se limpia la sesion para evitar hidratar un estado corrupto.

## Estados, loading y errores

No aplica en esta primera implementacion. No se agregaron pantallas ni llamadas nuevas con estados de carga.

## Axios, interceptors, context y hooks

No se modifico `apiClient` ni la logica de interceptors.

La sesion expuesta por `AuthContext` queda preparada para que futuras implementaciones consulten `session.datosPersonales`.

## TODOs

- Implementar en una etapa posterior la redireccion por rol usando `session.datosPersonales.rol`.
- Implementar en una etapa posterior la pantalla real del socio y su historial de turnos.

## Segunda Implementacion

Fecha: 2026-06-10

## Alcance implementado

Se implemento el redireccionamiento post-login segun `session.datosPersonales.rol`.

Cuando la sesion contiene el rol `Socio`, el usuario autenticado es enviado a `/socio/inicio`. Para el resto de los perfiles se mantiene el comportamiento actual hacia `/dashboard`, conservando `InicioPage` como pantalla renderizada en esa ruta.

No se implemento la grilla de turnos del socio y no se modifico backend.

## Archivos modificados o creados

- `Frontend/src/utils/authRoles.ts`
- `Frontend/src/pages/socio/SocioInicioPage.tsx`
- `Frontend/src/pages/auth/LoginPage.tsx`
- `Frontend/src/routes/AppRouter.tsx`
- `Docs/Frontend/E3-Turnos/E3P9-Modulo-Socios/IMPLEMENTATION_LOG_modulo-socios.md`

## Decisiones importantes

- Se centralizo la deteccion del rol Socio en `authRoles.ts` con `isSocioRole`.
- Se agrego `getAuthenticatedHomePath` para evitar duplicar la decision de ruta entre `LoginPage` y `FallbackRoute`.
- La comparacion del rol se hace contra `session.datosPersonales.rol`, sin usar permisos.
- `SocioInicioPage.tsx` se creo solo como placeholder inicial para la ruta `/socio/inicio`.

## Integracion frontend/backend

La implementacion consume unicamente los datos de sesion ya incorporados en la primera parte. No se agregaron llamadas nuevas a la API.

## Estados, loading y errores

No aplica en esta segunda implementacion. Solo se agregaron redirecciones y una pantalla placeholder.

## Axios, interceptors, context y hooks

No se modifico `apiClient`, interceptors ni hooks de autenticacion.

## TODOs

- Reemplazar el placeholder de `SocioInicioPage.tsx` por la pantalla real del socio en la tercera implementacion.
- Implementar luego el historial/listado de turnos del socio usando `GET /api/Turno/socio`.

## Tercera Implementacion

Fecha: 2026-06-10

## Alcance implementado

Se reemplazo el placeholder de `SocioInicioPage.tsx` por la pantalla inicial real del Socio, cargando el historial de turnos del usuario autenticado desde `GET /api/Turno/socio`.

La pantalla no envia `idUsuarioSocio`; la identidad del socio queda resuelta por el backend desde el JWT.

No se implementaron todavia los endpoints reales de nuevo turno ni cancelar turno.

## Archivos modificados o creados

- `Frontend/src/pages/socio/SocioInicioPage.tsx`
- `Frontend/src/components/turnos/TurnosHistorialGrid.tsx`
- `Frontend/src/components/socios/GestionTurnosModal.tsx`
- `Frontend/src/services/turnosService.ts`
- `Frontend/src/App.css`
- `Docs/Frontend/E3-Turnos/E3P9-Modulo-Socios/IMPLEMENTATION_LOG_modulo-socios.md`

## Que se reutilizo

- Se reutilizo `TurnoHistorialItem` desde `Frontend/src/types/turno.ts`.
- Se reutilizo `getSocioTurnosErrorMessage` desde `Frontend/src/utils/apiError.ts`.
- Se reutilizaron estilos existentes de la grilla de gestion de turnos:
  - `turnos-table`
  - `gestionar-turnos-table`
  - `gestionar-turnos-row`
  - `gestionar-turnos-row--selected`
  - `socios-radio`
  - `turno-state`

## Que se creo

- Se creo `TurnosHistorialGrid.tsx` para compartir la grilla entre el modal del asistente y la pantalla inicial del socio.
- Se agrego `turnosService.getTurnosSocioLogueado()` para consumir `GET /Turno/socio`.
- Se agregaron estilos minimos para el layout de `SocioInicioPage`.

## Integracion frontend/backend

`getTurnosSocioLogueado()` usa el `apiClient` existente, por lo que mantiene centralizados:

- `Authorization: Bearer {accessToken}`
- `X-Gym-Id: {idGym}`

La funcion interpreta `404 Not Found` como lista vacia, igual que el historial del asistente.

## Validaciones y estados implementados

`SocioInicioPage` ahora maneja:

- loading inicial
- error de carga
- estado vacio con el mensaje `No tenés turnos registrados.`
- grilla con seleccion de turno
- almacenamiento interno de `selectedTurnoId`
- boton `Nuevo Turno` siempre visible
- boton `Cancelar Turno` deshabilitado hasta seleccionar un turno

## TODOs

- Conectar `Nuevo Turno` con el flujo real cuando se implemente el endpoint correspondiente.
- Conectar `Cancelar Turno` con el endpoint real cuando se indique.
- Revisar si el resumen de cancelacion del modal asistente tambien conviene moverlo a helper compartido en una limpieza futura.

## Correccion 1 - Header Socio

Fecha: 2026-06-10

## Alcance implementado

Se corrigio el destino del link del logo/header `workspace-brand` para respetar el rol del usuario autenticado.

Si `session.datosPersonales.rol` contiene `Socio`, el logo navega a `/socio/inicio`. Para el resto de los perfiles sigue navegando a `/dashboard`.

## Archivos modificados

- `Frontend/src/layouts/AppLayout.tsx`
- `Docs/Frontend/E3-Turnos/E3P9-Modulo-Socios/IMPLEMENTATION_LOG_modulo-socios.md`

## Decisiones importantes

- Se reutilizo `getAuthenticatedHomePath(session)` desde `Frontend/src/utils/authRoles.ts`.
- No se duplico la comparacion del rol `Socio`.
- No se modifico el comportamiento de `/dashboard` ni de `/socio/inicio`.
- No se modifico backend.

## Correccion 2 - Botones Socio

Fecha: 2026-06-10

## Alcance implementado

Se movieron los botones `Nuevo Turno` y `Cancelar Turno` de la cabecera de `SocioInicioPage` a una zona debajo de la grilla/listado de turnos.

## Archivos modificados

- `Frontend/src/pages/socio/SocioInicioPage.tsx`
- `Frontend/src/App.css`
- `Docs/Frontend/E3-Turnos/E3P9-Modulo-Socios/IMPLEMENTATION_LOG_modulo-socios.md`

## Decisiones importantes

- Se mantuvo el orden visual: `Nuevo Turno` y luego `Cancelar Turno`.
- Se mantuvo la logica existente de habilitado/deshabilitado.
- No se modifico la carga de turnos, endpoints ni backend.

## Correccion 3 - Turnos Vencidos en Portal Socio

Fecha: 2026-06-11

## Alcance implementado

Se alineo `SocioInicioPage` con la gestion de turnos del modulo de socios para procesar turnos vencidos antes de cargar el historial del socio autenticado.

Ahora, al entrar a `/socio/inicio` y tambien al refrescar el listado luego de acciones como cancelar o registrar un turno, el frontend ejecuta primero:

- `PATCH /api/Turno/procesar-turnos-vencidos`
- `GET /api/Turno/socio`

Con esto, los turnos cuya fecha ya quedo por detras de la fecha actual pueden reflejar `EstadoTurno = Vencido` tambien en el portal del socio, no solo en `/socios/:idUsuario/turnos`.

## Archivos modificados

- `Frontend/src/pages/socio/SocioInicioPage.tsx`
- `Docs/Frontend/E3-Turnos/E3P9-Modulo-Socios/IMPLEMENTATION_LOG_modulo-socios.md`

## Decisiones importantes

- Se reutilizo `turnosService.procesarTurnosVencidos()` ya existente.
- Se mantuvo la secuencia de carga consistente con `GestionTurnosModal`.
- No se agregaron endpoints nuevos ni cambios en backend.

## Correccion 4 - Modal Compartido de Cancelacion en Portal Socio

Fecha: 2026-06-11

## Alcance implementado

Se reemplazaron en `/socio/inicio` las alertas inline asociadas al flujo de cancelacion de turnos por el mismo patron modal usado en `/socios/:idUsuario/turnos`.

Ahora, cuando el socio intenta cancelar un turno:

- la confirmacion se hace en modal
- los rechazos por estado, por ejemplo `No es posible cancelar un turno vencido.`, se muestran en modal
- el resultado exitoso tambien queda resuelto dentro del modal

Ademas, se extrajo un componente compartido para evitar duplicar la logica entre ambas pantallas.

## Archivos modificados o creados

- `Frontend/src/components/turnos/CancelarTurnoModal.tsx`
- `Frontend/src/components/socios/GestionTurnosModal.tsx`
- `Frontend/src/pages/socio/SocioInicioPage.tsx`
- `Docs/Frontend/E3-Turnos/E3P9-Modulo-Socios/IMPLEMENTATION_LOG_modulo-socios.md`

## Decisiones importantes

- `SocioInicioPage` ya no usa `form-alert` inline para errores de cancelacion.
- La validacion por estado del turno se mantiene en frontend, pero ahora se devuelve al modal como parte del mismo flujo de confirmacion.
- Se reutilizo un unico modal para asistente y socio, manteniendo consistente la experiencia entre `/socios/:idUsuario/turnos` y `/socio/inicio`.

## Correccion 5 - Consistencia del Modal de Cancelacion en Gestion de Turnos

Fecha: 2026-06-11

## Alcance implementado

Se ajusto la ruta `/socios/:idUsuario/turnos` para que el flujo de cancelacion replique el comportamiento de `/socio/inicio` en dos casos puntuales:

- un turno con estado `Cancelado` ya no puede completar una cancelacion exitosa en el modal
- un rechazo backend `409 Conflict` ya no muestra el mensaje crudo `Request failed with status code 409`

Ahora la gestion de turnos del asistente valida el `estadoTurno` antes de invocar el endpoint y, cuando el error viene de Axios, el modal usa el mapper de `apiError.ts` en lugar del texto generico de la libreria.

## Archivos modificados

- `Frontend/src/components/turnos/CancelarTurnoModal.tsx`
- `Frontend/src/components/socios/GestionTurnosModal.tsx`
- `Docs/Frontend/E3-Turnos/E3P9-Modulo-Socios/IMPLEMENTATION_LOG_modulo-socios.md`

## Decisiones importantes

- Se unifico la validacion previa de cancelacion entre socio y asistente.
- Se priorizo el mensaje normalizado de `getCancelarTurnoErrorMessage()` para errores Axios.
- No se modificaron endpoints ni contratos backend.
