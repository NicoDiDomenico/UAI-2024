Quiero ajustar la sección de Inicio correspondiente a **Agenda Operativa / Turnos del día**.

Leé primero:

- AGENTS.md
- frontend-skill.md
- inicio-plan.md
- IMPLEMENTATION_LOG_inicio-plan.md

También revisá la implementación actual de la pantalla de Inicio y la grilla de turnos.

Objetivo:

Rediseñar la sección `Turnos del día` y agregar un selector tipo cápsula con dos vistas:

- `Día`
- `Hora`

Referencias visuales adjuntas:

- `grid_original.png`: estado actual de la grilla.
- `imagen_boton.png`: referencia visual exacta para el selector Día/Hora.

Backend existente:

GET api/Turno/inicio/grilla-fecha?fecha={yyyy-mm-dd}

El endpoint ya devuelve los turnos del día actual usando `TurnoDetalleDto`:

- idTurno
- nombreDia
- fecha
- cupos
- hora
- entrenador
- socio
- estadoTurno

No crear endpoints nuevos.

No modificar backend.

## Comportamiento requerido

### Vista Día

Debe ser la vista por defecto.

IMPORTANTE:

La vista Día debe reutilizar la grilla actualmente implementada en la pantalla de Inicio (ver referencia `grid_original.png`).

La estructura actual de la tabla:

- Hora
- Socio
- Entrenador
- Cupos
- Estado

debe mantenerse como base.

No reemplazar la grilla por otro componente distinto.

No crear una segunda grilla para la vista Día.

La nueva funcionalidad debe construirse sobre la implementación existente, aplicando únicamente mejoras visuales y de legibilidad.

La vista Día debe seguir mostrando todos los turnos devueltos por:

GET /api/Turno/inicio/grilla-fecha?fecha={yyyy-mm-dd}

### Vista Hora

La vista Hora debe usar una grilla específica y más compacta, distinta a la grilla de Día.

Motivo:

En esta vista todos los registros corresponden a la misma hora actual y al mismo bloque/cupo, por lo tanto no tiene sentido repetir las columnas `Hora` y `Cupos` en cada fila.

La vista Hora debe mostrar arriba de la grilla un resumen visible con:

- Hora actual filtrada, por ejemplo: `Hora: 12:00`
- Cupos del bloque, por ejemplo: `Cupos: 0/20`

Luego, la grilla debe mostrar solo las columnas relevantes para los turnos filtrados:

- Socio
- Entrenador
- Estado

Si hay varios turnos para la misma hora, deben listarse como filas separadas.

Si no hay turnos para la hora actual, mostrar un estado vacío claro.

No llamar nuevamente al backend para la vista Hora. Usar los datos ya cargados para el día y filtrarlos en frontend.

## Selector Día/Hora

Agregar un selector tipo cápsula similar a `imagen_boton.png`.

Requisitos visuales:

- Forma de cápsula.
- Borde turquesa/verde agua.
- Opción inactiva con fondo turquesa/verde (#78B2AC) agua y texto blanco.
- Opción activa con fondo blanco y texto turquesa/verde agua.
- Ubicarlo en la cabecera de la sección, idealmente entre el título y el contador.
- Mantener buena alineación responsive.

## Mejoras visuales de la grilla

Mejorar la grilla actual sin romper la estructura existente.

Priorizar:

- mejor legibilidad
- mejor contraste en encabezados
- alineación limpia de columnas
- nombres propios fáciles de leer
- diseño consistente con el estilo actual de MindFit Intelligence

Mejorar los chips de estado:

- `Cancelado`: chip con color suave de error/cancelación.
- `Finalizado`: chip con color suave de éxito/finalización.
- Otros estados deben tener un estilo neutro.

Opcional:

- Agregar una barra de progreso sutil debajo de `cupos`, interpretando valores como `1/20`.
- Si el formato de `cupos` no se puede interpretar, mostrar solo el texto.

## Criterios de Implementación

### Hora actual

Para la vista `Hora`, usar la hora del sistema del navegador.

La hora debe normalizarse al bloque horario en punto.

Ejemplos:

- Si la hora actual es `12:00`, filtrar por `12:00`.
- Si la hora actual es `12:30`, filtrar por `12:00`.
- Si la hora actual es `14:59`, filtrar por `14:00`.

Comparar contra la propiedad `hora` del DTO.

No buscar coincidencia exacta con minutos actuales si no son `00`.

### Cupos en Vista Hora

En la vista `Hora`, el resumen superior debe mostrar el cupo del bloque horario filtrado.

Si todos los turnos filtrados tienen el mismo valor de `cupos`, mostrar ese valor.

Si hubiera valores diferentes de `cupos` para la misma hora, mostrar el primero disponible y no bloquear la UI.

### Selector Día/Hora

Respetar la referencia visual `imagen_boton.png`.

En este diseño:

- la opción visualmente seleccionada debe quedar con fondo blanco y texto turquesa/verde agua
- la opción no seleccionada debe quedar con fondo turquesa/verde agua y texto blanco

Aunque este comportamiento sea distinto a otros tabs tradicionales, debe respetarse porque es el diseño definido para este proyecto.

### Textos de estado vacío

Para la vista `Hora`, si no hay turnos en la hora actual, mostrar un mensaje claro, por ejemplo:

`No hay turnos registrados para la hora actual.`

Para la vista `Día`, mantener el estado vacío actual si ya existe.

## Restricciones

- No modificar backend.
- No crear endpoints nuevos.
- No duplicar llamadas API si ya se cargan los turnos del día.
- No inventar campos del DTO.
- Mantener TypeScript estricto.
- Reutilizar componentes, servicios o hooks existentes si ya existen.
- Evitar cambios masivos fuera de la sección de Inicio.
- Mantener estados de loading, error y vacío.
- No implementar lógica relacionada con turnos futuros fuera de esta mejora.

## Al finalizar

Actualizar `IMPLEMENTATION_LOG_inicio-plan.md` documentando:

- archivos modificados
- nueva vista Día/Hora
- lógica de filtrado por hora actual
- mejoras visuales realizadas
- manejo de estados vacío/loading/error
- TODOs o limitaciones detectadas
