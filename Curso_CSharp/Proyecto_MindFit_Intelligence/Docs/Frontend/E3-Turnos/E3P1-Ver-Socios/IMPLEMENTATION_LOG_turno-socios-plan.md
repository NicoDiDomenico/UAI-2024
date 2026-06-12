# Implementation Log - Ver Socios

## Archivos creados o modificados

- `Frontend/src/types/socio.ts`
- `Frontend/src/services/sociosService.ts`
- `Frontend/src/services/turnosService.ts`
- `Frontend/src/pages/SociosPage.tsx`
- `Frontend/src/components/socios/GestionTurnosModal.tsx`
- `Frontend/src/utils/date.ts`
- `Frontend/src/utils/apiError.ts`
- `Frontend/src/routes/AppRouter.tsx`
- `Frontend/src/App.css`

## Decisiones importantes

- Se reemplazo el placeholder de `/socios` por una pagina operativa con carga real de backend.
- Se mantuvo la arquitectura actual del frontend usando `types`, `services`, `pages` y utilidades compartidas.
- Los botones `Agregar`, `Consultar`, `Eliminar` y `Turnos` navegan a vistas placeholder, tal como pide esta etapa.
- La visibilidad de botones se resolvio con permisos del `AuthContext`, reutilizando la sesion ya persistida en localStorage.
- Ajuste posterior: antes de cargar el historial de turnos del socio, el frontend ejecuta el procesamiento automatico de turnos vencidos para que la UI siempre refleje estados actualizados.

## Integracion frontend/backend

- En el montaje de la pagina se ejecuta este flujo:
  1. `PUT /Cuota/actualizar-vencidas`
  2. `PATCH /Usuario/procesar-eliminaciones-pendientes`
  3. `GET /Usuario/grilla-socio`
- Las llamadas quedaron centralizadas en `sociosService`.
- Se reutilizo el `apiClient` existente, por lo que `Authorization` y `X-Gym-Id` siguen saliendo desde interceptors.
- En la apertura de la vista/modal de turnos del socio se ejecuta este flujo:
  1. `PATCH /Turno/procesar-turnos-vencidos`
  2. `GET /Turno/asistente/{idUsuarioSocio}`
- La llamada de preprocesamiento quedo centralizada en `turnosService`.

## Validaciones implementadas

- `Consultar`, `Eliminar` y `Turnos` solo se habilitan cuando hay un socio seleccionado.
- El filtro por texto trabaja sobre los datos ya cargados, sin nuevas llamadas al backend.
- El checkbox `Mostrar socios eliminados` controla la visibilidad de registros con estado `Eliminado`.
- Si el socio seleccionado deja de estar visible por filtros, la seleccion se limpia automaticamente.
- Antes de mostrar turnos no se replica logica de negocio en frontend: solo se invoca el endpoint del backend y luego se continua con la carga normal.

## Loading, estados y errores

- Mientras corre el flujo inicial se muestra un estado de carga.
- Si alguna llamada falla, se muestra un mensaje de error amigable usando `getSociosErrorMessage`.
- Si no hay coincidencias con los filtros, la tabla muestra un estado vacio claro.
- Ajuste posterior: el campo de filtro `Valor` paso a usar una estrategia mas agresiva contra autocompletado del navegador:
  `name` no asociado a credenciales, `autoComplete="one-time-code"`, `readOnly` hasta foco real y campos ocultos senuelo para desacoplarlo del usuario autenticado y de formularios de contrasena abiertos previamente.
- Ajuste posterior: si falla `procesar-turnos-vencidos`, el modal de turnos muestra el error con la misma estrategia de mensajes ya usada para el historial de turnos y no intenta inventar recuperaciones locales.

## Configuracion relevante

- No hizo falta cambiar Axios ni el contexto de autenticacion.
- Se agregaron utilidades de fecha para:
  - mostrar `fechaFinPeriodo` en formato `dd/mm/aaaa`
  - permitir filtrado por fecha ya formateada

## TODOs o limitaciones

- Los placeholders hijos aun no consumen `idUsuario`; solo representan la navegacion esperada para futuras etapas.
- La logica de permisos se resolvio con los codigos definidos en el plan. Si luego el backend expone metadata especifica para estos botones, convendra centralizarla.
- El procesamiento automatico de turnos vencidos quedo aplicado en la visualizacion de turnos del socio dentro del modulo Ver Socios. Si en futuras etapas hay otras pantallas de turnos independientes, convendra reutilizar esta misma pre-carga alli tambien.
