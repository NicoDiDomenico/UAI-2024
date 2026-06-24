# Implementation Log - Consultar Rutinas

## Archivos creados o modificados

- `Frontend/src/types/rutina.ts`
  - Se agregaron los DTOs usados por la consulta de rutinas: `DiaDto`, `RutinaDto`, bloques de calentamiento/entrenamiento/estiramiento, `EjercicioDto`, `GrupoMuscularDto`, `TipoEjercicioDto`, `MaquinaDto` y `EquipamientoDto`.
- `Frontend/src/services/rutinasService.ts`
  - Se sumaron llamadas centralizadas a:
    - `GET /Dia/dias`
    - `GET /Rutina/socios/{idUsuarioSocio}/rutinas?idDia=X`
    - `GET /GrupoMuscular`
    - `GET /Ejercicio?idGrupoMuscular=X`
- `Frontend/src/pages/GestionRutinasPage.tsx`
  - Se reemplazo el panel "Proximamente" por la seccion de consulta y edicion en memoria de rutinas.
  - Se agregaron botones de dia, carga automatica del dia actual, tres paneles editables y acciones segun permisos.
- `Frontend/src/App.css`
  - Se agregaron estilos para tabs de dias, paneles de rutina, filas editables, acciones y modal visual.

## Decisiones importantes

- Las lineas que vienen desde `RutinaDto` y las lineas creadas con `+` se manejan como el mismo tipo de borrador editable en memoria.
- Cambiar, quitar o agregar lineas no persiste datos en backend en esta etapa.
- Si se recarga la pagina o se vuelve a consultar la rutina, el borrador local se descarta y vuelve a mostrarse la respuesta actual del backend.
- El boton `+` solo se muestra con `EDITAR_RUTINA`.
- El campo `orden` no es editable para el usuario. Se usa internamente para ordenar y numerar las lineas.
- Al cargar, agregar o quitar lineas se normaliza el orden de cada panel para mantener una secuencia ascendente de uno en uno.
- El boton `+` queda en el cuerpo del panel: debajo del mensaje vacio si no hay lineas, o debajo de la ultima linea cuando ya existen registros.
- `Guardar`, `Eliminar` e `Historial` se muestran segun permisos, pero abren un modal informativo porque la funcionalidad queda para la Parte 3 o una etapa posterior.
- No se implementaron `Reporte` ni `Limpiar`.

## Integracion frontend/backend

- La pantalla sigue usando el `apiClient` centralizado, por lo que conserva los interceptores existentes para `Authorization` y `X-Gym-Id`.
- Al seleccionar un socio:
  - Se cargan los dias con `GET /Dia/dias`.
  - Se elige automaticamente el dia actual comparando `NombreDia` con el dia del sistema.
  - Si no hay match, se usa el primer dia recibido como fallback.
  - Se consulta la rutina con `GET /Rutina/socios/{idUsuarioSocio}/rutinas?idDia=X`.
- Para dropdowns:
  - Las lineas existentes toman grupo muscular y ejercicio desde el `EjercicioDto` embebido en la rutina.
  - Las lineas nuevas o modificadas usan `GET /GrupoMuscular` y luego `GET /Ejercicio?idGrupoMuscular=X`.

## Validaciones y estados

- El grupo muscular se marca como obligatorio en filas editables cuando esta vacio.
- El dropdown de ejercicios queda deshabilitado hasta elegir grupo muscular.
- Cambiar el grupo muscular limpia el ejercicio seleccionado.
- La numeracion visual `Linea N` toma el `orden` interno ya normalizado, no un campo editable.
- Se manejan estados de loading/error para rangos, entrenadores, socios, dias, grupos musculares y rutina.
- El `404` de rutina se muestra como estado esperado: "El socio no asiste este dia".
- Las respuestas tardias se ignoran con request ids para evitar estados viejos al cambiar seleccion rapidamente.

## TODOs / limitaciones

- Implementar persistencia real con `PUT /Rutina/{idRutina}/bloques`.
- Implementar baja/estado de rutina cuando corresponda.
- Implementar historial de rutinas.
- Agregar validacion completa previa a Guardar en la Parte 3.
- Mostrar errores especificos si falla la carga de ejercicios por grupo muscular.
