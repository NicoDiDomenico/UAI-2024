# Implementation Log - Equipamientos

## Archivos creados o modificados

- Creado `Frontend/src/types/equipamiento.ts` con los tipos `EquipamientoDto`, `EquipamientoInsertDto` y `EquipamientoUpdateDto`.
- Creado `Frontend/src/services/equipamientosService.ts` con operaciones `getAll`, `create`, `update` y `delete`.
- Creado `Frontend/src/pages/EquipamientosPage.tsx` con grilla, formulario, validaciones, permisos y modal de eliminacion.
- Modificado `Frontend/src/routes/AppRouter.tsx` para reemplazar el placeholder de `/gimnasio/equipamientos`.
- Modificado `Frontend/src/App.css` para estilos y comportamiento responsive de la pantalla.

## Decisiones importantes

- El servicio frontend usa `/Equipamiento` y `/Equipamiento/{id}` porque `apiClient` ya configura `VITE_API_BASE_URL`, normalmente `/api`.
- No se consume `GET /api/Equipamiento/{id}`: la grilla trae todos los campos necesarios para cargar el formulario.
- Al crear o editar, el equipamiento devuelto por el backend queda seleccionado y el formulario refleja esa respuesta.
- Al eliminar, se limpia la seleccion y el formulario vuelve a modo alta.

## Integracion frontend/backend

- La integracion queda centralizada en `equipamientosService`, usando el `apiClient` existente.
- `Authorization` y `X-Gym-Id` quedan delegados al interceptor ya configurado.
- Los payloads envian solo `nombreEquipo`, `costoAdquisicion` y `pesoFijoKg`.
- No se envia `idEquipamiento` ni `idGym` en el body.

## Validaciones implementadas

- `nombreEquipo` obligatorio.
- `nombreEquipo` maximo 100 caracteres.
- `costoAdquisicion` mayor a 0.
- `pesoFijoKg` opcional.
- Si `pesoFijoKg` se completa, debe ser mayor a 0.

## Estados, errores y permisos

- Se manejan estados de carga inicial, guardado, eliminacion, exito y error.
- Los errores usan `getApiErrorMessage` para mostrar mensajes reales del backend cuando esten disponibles.
- Boton **Crear**: `CREAR_EQUIPAMIENTO`.
- Boton **Guardar**: `EDITAR_EQUIPAMIENTO`.
- Boton **Eliminar**: `ELIMINAR_EQUIPAMIENTO`.
- La eliminacion usa modal visual y no `window.confirm`.

## Verificacion

- `npm.cmd run build` ejecutado correctamente en `Frontend`.
- Vite emitio un warning de chunk mayor a 500 kB, sin bloquear el build.
- Se intento abrir la app con el navegador interno, pero la instancia `iab` no esta disponible en esta sesion.
- Se intento iniciar Vite en `http://127.0.0.1:5173/`; Vite reporto `ready`, pero la comprobacion HTTP posterior no pudo conectar al servidor.

## TODOs o limitaciones

- No se agregaron tests automatizados porque el proyecto no tiene una suite frontend especifica para estas pantallas.
- La verificacion real de requests requiere backend corriendo y sesion con permisos de equipamiento.
- La verificacion visual queda pendiente por la disponibilidad del navegador interno/local server en la sesion actual.
