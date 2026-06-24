# IMPLEMENTATION LOG - Validar ingreso

## Archivos creados o modificados

- `Frontend/src/layouts/AppLayout.tsx`
- `Frontend/src/pages/InicioPage.tsx`
- `Frontend/src/hooks/useInicioData.ts`
- `Frontend/src/services/turnosService.ts`
- `Frontend/src/types/turno.ts`
- `Frontend/src/utils/apiError.ts`
- `Frontend/src/App.css`
- `Docs/Frontend/E3-Turnos/E3P5-Validar-Ingreso/IMPLEMENTATION_LOG_validar-ingreso-plan.md`

## Ajuste post-validacion - 2026-06-17

- Se agrego un callback opcional `onValidarIngresoSuccess` en `AppLayout`.
- Cuando `POST /Turno/validar-ingreso` responde exitosamente, el layout ejecuta ese callback despues de mostrar el mensaje de exito.
- `useInicioData` ahora expone `refreshTurnos`, reutilizando la carga real de `turnosService.getInicioGridByDate(fecha)`.
- `InicioPage` pasa `refreshTurnos` al layout para que la grilla de turnos del inicio vuelva a consultar los datos luego de una validacion exitosa.
- Si la recarga de la grilla falla, no se pisa el resultado exitoso del modal; el error queda contenido en el estado propio de la grilla.
- Se corrigio el control de montaje de `useInicioData` para que vuelva a marcarse activo al ejecutar los efectos. Esto evita que React en modo desarrollo deje la grilla en `Cargando turnos del dia...` por el doble ciclo de efectos.

## Decisiones importantes

- Se implemento el flujo dentro de `AppLayout` para que este disponible en todas las pantallas privadas que usan el mismo header.
- El boton se muestra solo cuando la sesion contiene el permiso frontend exacto `VALIDAR_INGRESO`.
- Se reutilizaron el `apiClient` centralizado, las clases base de modal ya existentes y el helper global de errores para mantener coherencia con el frontend actual.
- Para el refresco post-validacion se prefirio un callback opcional desde la pantalla que posee la grilla, evitando acoplar `AppLayout` al estado interno de `InicioPage`.

## Integracion frontend/backend

- Se agrego al servicio de turnos el llamado `POST /Turno/validar-ingreso`.
- El request usa el DTO real del backend: `{ dniSocio: string }`.
- La respuesta exitosa espera `{ message: string }` y se muestra en el modal.
- El header `X-Gym-Id` y el token `Authorization` siguen saliendo del interceptor global de Axios ya existente.

## Validaciones implementadas

- El campo DNI es obligatorio.
- El input permite solo digitos.
- Antes de enviar se aplica `trim()`.
- El boton `Confirmar` queda deshabilitado si el campo queda vacio.

## Estados, loading y errores

- Mientras el request esta en curso, el modal bloquea cierre y muestra `Confirmando...`.
- Si la validacion es exitosa, el modal permanece abierto, limpia el DNI y deja listo el formulario para otra validacion.
- Si falla, se muestra el mensaje real del backend cuando existe.
- El helper global `getApiErrorMessage` ahora soporta tambien el formato `{ message: ["error 1", "error 2"] }`.

## TODOs o limitaciones

- No se agrego cierre por tecla `Escape` ni por click en backdrop porque el plan no lo pedia.
- La verificacion final depende del build del frontend y de probar el endpoint contra el backend real con un usuario que tenga `VALIDAR_INGRESO`.
