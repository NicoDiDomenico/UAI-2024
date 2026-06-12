# IMPLEMENTATION LOG - Gestion turnos

## Archivos creados o modificados

- `Frontend/src/components/socios/GestionTurnosModal.tsx`
- `Frontend/src/pages/SociosPage.tsx`
- `Frontend/src/routes/AppRouter.tsx`
- `Frontend/src/services/turnosService.ts`
- `Frontend/src/types/socio.ts`
- `Frontend/src/types/turno.ts`
- `Frontend/src/utils/apiError.ts`
- `Frontend/src/App.css`
- `Docs/Frontend/E3-Turnos/E3P6-Gestion-Turnos/IMPLEMENTATION_LOG_gestion-turnos-plan.md`

## Decisiones importantes

- Se mantuvo la ruta `/socios/:idUsuario/turnos`, pero ahora esa ruta renderiza un modal sobre `SociosPage` en lugar de una pagina placeholder aislada.
- Se reutilizo el sistema visual de modales ya existente para mantener consistencia con `Agregar socio`, `Consultar socio` y `Validar ingreso`.
- El modal de gestion muestra `nombreCompleto` y `nroDocumento` usando los datos ya cargados desde `GET /Usuario/grilla-socio`.
- `Nuevo Turno` y `Cancelar Turno` quedaron como placeholders funcionales para la etapa actual, respetando la visibilidad por permisos y la seleccion requerida solo para cancelar.
- Se ajusto la deteccion de rutas en `SociosPage` para evitar que `/socios/:idUsuario/turnos` renderice tambien el modal de `Consultar socio`; ahora cada modal se monta solo en su sufijo correspondiente.
- En una iteracion posterior de UI se elimino el bloque visual redundante de `Turno seleccionado` y se diferencio `Cancelar Turno` con tratamiento rojo para reforzar que es una accion sensible.
- En otro ajuste de UI se reforzo el estado deshabilitado de `Cancelar Turno` con un estilo grisado explicito cuando todavia no hay una fila seleccionada.

## Integracion frontend/backend

- Se agrego el consumo de `GET /Turno/asistente/{idUsuarioSocio}` en `turnosService`.
- La respuesta se tipa como `TurnoHistorialItem[]` siguiendo `TurnoDto`.
- Un `404` del historial se interpreta como lista vacia, mostrando el mensaje `El socio no tiene turnos registrados.`
- El valor `estadoTurno: "EnCurso"` se muestra visualmente como `En Curso`, sin alterar el valor real recibido del backend.
- `horaDesde` y `horaHasta` se muestran tal como vienen del backend, aplicando solo un formateo visual simple para dejar `HH:mm` cuando corresponde.

## Validaciones implementadas

- El boton principal `Turnos` en la grilla de socios sigue requiriendo un socio seleccionado.
- Dentro del modal, `Nuevo Turno` se muestra solo con permiso `AGREGAR_TURNO`.
- Dentro del modal, `Cancelar Turno` se muestra solo con permiso `CANCELAR_TURNO`.
- `Cancelar Turno` queda deshabilitado hasta que exista un turno seleccionado en la grilla.

## Estados, loading y errores

- Mientras se carga el historial se muestra `Cargando historial de turnos...`.
- Si el endpoint devuelve un error distinto de `404`, se muestra un mensaje amigable usando `getSocioTurnosErrorMessage`.
- Cuando no hay datos y no hay error, el modal informa que el socio no tiene turnos registrados.
- Se agrego una capa secundaria de modal para los placeholders `Proximamente...` de `Nuevo Turno` y `Cancelar Turno`.

## Configuracion relevante

- No se modifico la configuracion global de Axios ni los interceptores existentes.
- La autenticacion `Authorization` y el header `X-Gym-Id` siguen saliendo del `apiClient` centralizado ya implementado.

## Verificacion

- Se ejecuto `cmd /c npm run build` en `Frontend`.
- El build finalizo correctamente con `tsc -b && vite build`.

## TODOs o limitaciones

- En esta etapa no se implementaron los flujos reales de registrar ni cancelar turnos; ambos quedan preparados como placeholder.
- El modal depende de que `GET /Usuario/grilla-socio` ya incluya `nroDocumento` en la respuesta para mostrarlo en el encabezado derecho.
- No se agrego cierre por tecla `Escape` ni cierre por click en el backdrop porque el plan no lo exigia.
