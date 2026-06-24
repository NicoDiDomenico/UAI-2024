# Implementation Log - Historial de Rutinas

## Archivos creados o modificados

- `Frontend/src/types/rutina.ts`
  - Se agregaron los tipos de historial:
    - `RutinaHistorialResumenDto`
    - `RutinaHistorialDetalleDto`
    - `RutinaHistorialCalentamientoDto`
    - `RutinaHistorialEntrenamientoDto`
    - `RutinaHistorialEstiramientoDto`
- `Frontend/src/services/rutinasService.ts`
  - Se agregaron endpoints para:
    - `GET /Rutina/{idRutina}/historial`
    - `GET /Rutina/{idRutina}/historial/{idRutinaHistorial}`
    - `POST /Rutina/{idRutina}/historial/{idRutinaHistorial}/restaurar`
    - `GET /Ejercicio` para resolver nombres de ejercicios desde `idEjercicio`.
- `Frontend/src/pages/GestionRutinasPage.tsx`
  - El boton **Historial** abre un modal real de versiones historicas.
  - Se agrego seleccion unica con radio buttons para las versiones.
  - Se agrego detalle de version historica en modo lectura.
  - Se agrego restauracion de version historica con permiso `RECUPERAR_RUTINA`.
  - Se habilito navegacion por dias dentro del modal de historial.
  - Se ajusto el titulo del modal para mostrar el nombre y apellido del socio.
  - Se quitaron los indicadores visuales `Activa` / `Inactiva` del listado y detalle del historial.
- `Frontend/src/App.css`
  - Se agregaron estilos para el modal amplio de historial, listado de versiones y paneles de lectura.

## Decisiones importantes

- El historial se implemento como modal separado del modal de mensajes/confirmacion usado por Guardar y Eliminar.
- El detalle historico se muestra en modo lectura: no hay dropdowns, inputs, boton `+` ni quitar linea.
- Aunque el backend devuelve `activoSnapshot`, la UI ya no lo muestra para mantener el modal mas limpio.
- El backend devuelve bloques historicos con `idEjercicio`, no con `EjercicioDto`. Para mostrar nombres se carga el catalogo completo de ejercicios y se resuelve `idEjercicio -> descEjercicio` en frontend.
- Si un ejercicio historico no existe en el catalogo actual, se muestra `Ejercicio #ID` como fallback.
- Al restaurar, se usa la `RutinaDto` devuelta por el backend para reconstruir el borrador actual, sin recargar toda la pagina.
- Cambiar de dia dentro del modal no cambia la rutina principal. Solo busca la rutina activa del socio para ese dia, obtiene su `idRutina` y carga su historial.
- Si el dia elegido dentro del modal no tiene rutina activa, se muestra un estado vacio: "No hay rutina activa para este dia."

## Integracion frontend/backend

- `GET /Rutina/{idRutina}/historial`
  - Carga versiones historicas resumidas con `idRutinaHistorial`, `version`, `fechaSnapshot` y `activoSnapshot`.
- `GET /Rutina/{idRutina}/historial/{idRutinaHistorial}`
  - Carga los tres bloques historicos para una version puntual.
- `POST /Rutina/{idRutina}/historial/{idRutinaHistorial}/restaurar`
  - Restaura la version seleccionada y devuelve `RutinaDto`.
- `GET /Rutina/socios/{idUsuarioSocio}/rutinas?idDia=X`
  - Se reutiliza dentro del modal para buscar la rutina activa del socio cuando se cambia de dia.
- `GET /Ejercicio`
  - Se usa para mostrar nombres de ejercicios en el detalle historico.
- Todas las llamadas usan `apiClient`, por lo que conservan `Authorization` y `X-Gym-Id`.

## Validaciones implementadas

- El boton **Historial** solo aparece cuando ya hay rutina activa cargada y el usuario tiene `VER_HISTORIAL_RUTINA`.
- El boton **Restaurar** solo aparece si hay detalle seleccionado y el usuario tiene `RECUPERAR_RUTINA`.
- La seleccion de historial es unica mediante radio buttons.

## Manejo de estados/loading/errors

- Se muestran estados de carga para listado de versiones, detalle y restauracion.
- Se muestran estados de carga al cambiar de dia dentro del modal.
- Se muestran errores del backend en el modal sin usar `window.confirm`.
- Si no hay versiones historicas, el modal muestra un estado vacio.
- Si no hay rutina activa para el dia elegido en el modal, no se intenta cargar historial porque no hay `idRutina`.
- Al restaurar correctamente, se cierra el modal y se informa el resultado con el modal visual existente.

## TODOs / limitaciones

- El plan menciona `historial-rutina.png`, pero no se encontro el archivo en la carpeta de la etapa al implementar.
- Si en el futuro el backend incluye `EjercicioDto` dentro del historial, se podria evitar la carga extra de `GET /Ejercicio`.
