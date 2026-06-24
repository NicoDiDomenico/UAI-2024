# Implementation Log - ABM Rutinas

## Archivos creados o modificados

- `Frontend/src/types/rutina.ts`
  - Se agregaron los tipos de request para `RutinaBloquesUpdateDto`, `CalentamientoInsertDto`, `EntrenamientoInsertDto`, `EstiramientoInsertDto` y `RutinaEstadoUpdateDto`.
  - Se agrego el tipo `RutinaEstadoUpdateResponse` para la respuesta del cambio de estado.
- `Frontend/src/services/rutinasService.ts`
  - Se agrego `guardarBloquesRutina(idRutina, payload)` para `PUT /Rutina/{idRutina}/bloques`.
  - Se agrego `cambiarEstadoRutina(idRutina, payload)` para `PATCH /Rutina/{idRutina}/estado`.
- `Frontend/src/pages/GestionRutinasPage.tsx`
  - El boton **Guardar** ahora persiste los tres bloques de la rutina.
  - El boton **Eliminar** abre un modal visual de confirmacion y desactiva la rutina activa.
  - Se mantienen los permisos frontend `EDITAR_RUTINA` y `ELIMINAR_RUTINA` para mostrar acciones.
- `Frontend/src/App.css`
  - Se agregaron estados deshabilitados para acciones y estilos para el modal de confirmacion.

## Decisiones importantes

- Guardar usa estrategia replace-all, igual que el backend: envia todos los calentamientos, entrenamientos y estiramientos del borrador actual.
- El borrador editable se reconstruye con la `RutinaDto` devuelta por el backend despues de guardar.
- Eliminar no borra fisicamente: envia `{ "activo": false }` al endpoint de estado.
- No se implemento **Activar rutina** porque el endpoint de consulta solo devuelve rutinas activas. Si una rutina esta desactivada, el frontend recibe `404` y no tiene `idRutina` para reactivarla.
- Historial sigue como accion pendiente porque no pertenece a esta etapa.

## Integracion frontend/backend

- `PUT /Rutina/{idRutina}/bloques`
  - `calentamientos`: `idEjercicio`, `duracion`, `orden`, `observaciones`.
  - `entrenamientos`: `idEjercicio`, `series`, `repeticiones`, `pesoAsignado`, `tiempoDescansoSegundos`, `orden`, `observaciones`.
  - `estiramientos`: `idEjercicio`, `duracion`, `orden`, `observaciones`.
- `PATCH /Rutina/{idRutina}/estado`
  - Se envia `{ activo: false }` para desactivar la rutina activa actualmente consultada.
- Se sigue usando `apiClient`, por lo que aplican los interceptores existentes de `Authorization` y `X-Gym-Id`.

## Validaciones implementadas

- Antes de guardar, cada linea debe tener un ejercicio seleccionado.
- Calentamiento y Estiramiento requieren `duracion` mayor a cero.
- Entrenamiento requiere `series` y `repeticiones` mayores a cero.
- `orden` no se edita manualmente: se envia el orden interno normalizado de cada panel.
- `observaciones` vacias se envian como `null`.

## Manejo de estados/loading/errors

- El boton **Guardar** muestra `Guardando...` mientras se ejecuta el PUT.
- El boton **Eliminar** muestra `Eliminando...` y usa modal de confirmacion antes de llamar al PATCH.
- Los errores de backend se muestran en un modal visual, sin `window.confirm`.
- Luego de desactivar una rutina, se refresca la consulta del socio/dia y se muestra el estado sin rutina activa.

## TODOs / limitaciones

- Implementar Historial en una etapa posterior.
- Implementar Activar solo si el backend expone una forma confiable de consultar rutinas inactivas o recuperar su `idRutina`.
- Si se agregan validaciones backend mas especificas, reflejarlas en mensajes de frontend.
