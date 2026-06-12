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

Muestra todos los turnos devueltos por el endpoint para el día actual.

El título debe mantenerse como:

- AGENDA OPERATIVA
- Turnos del día

El contador debe mostrar la cantidad de registros visibles.

Ejemplo:

- `2 registrados`

### Vista Hora

Al seleccionar `Hora`, la grilla debe mostrar únicamente los turnos correspondientes a la hora actual.

Regla:

- Si la hora actual es `12:30`, mostrar los turnos cuya hora sea `12:00`.
- Si la hora actual es `14:10`, mostrar los turnos cuya hora sea `14:00`.

Usar la propiedad `hora` del DTO para comparar.

El contador debe actualizarse según la cantidad filtrada.

Agregar un texto sutil de ayuda, por ejemplo:

`Turnos para la hora actual: 12:00`

Si no hay turnos para la hora actual, mostrar un estado vacío claro.

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
