# Etapa 6 Rutinas - Parte 4 - Historial de Rutinas

---

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend.

## 2. Tarea Principal

Implementar en el frontend el modal de Restaurar Rutina al hacer clic en el boton Historial perteneciente al modulo **Gestionar Rutinas**, accesible desde `/rutinas`.

### Flujo de Endpoints

1. GET api/Rutina/{idRutina}/historial --> RES: IEnumerable<RutinaHistorialResumenDto> --> Front: DataGrid con radio buttons de versiones históricas guardadas automáticamente al modificar bloques (paneles) en /rutinas --> Obtengo idRutinaHistorial --> Policy: VerHistorialRutina
2. GET api/Rutina/{idRutina}/historial/{idRutinaHistorial} --> RES: RutinaHistorialDetalleDto --> Front: DataGrid con el Detalle de una versión puntual del historial, aca se reutiliza los componentes del elemento "<div class="rutinas-day-tabs" aria-label="Dias de rutina"><button class="" type="button">Lunes</button><button class="is-active" type="button">Martes</button><button class="" type="button">Miércoles</button><button class="" type="button">Jueves</button><button class="" type="button">Viernes</button><button class="" type="button">Sábado</button><button class="" type="button">Domingo</button></div>" y del elemento "<div class="rutinas-blocks"><section class="rutinas-block"><header class="rutinas-block__header"><div><span class="rutinas-step">Calentamiento</span><h3>Calentamiento</h3></div></header><div class="rutinas-block__empty-state"><p class="rutinas-block__empty">No hay calentamiento cargado.</p><div class="rutinas-block__add-after"><button class="rutinas-add-row" type="button" aria-label="Agregar Calentamiento">+</button></div></div></section><section class="rutinas-block"><header class="rutinas-block__header"><div><span class="rutinas-step">Entrenamiento</span><h3>Entrenamiento</h3></div></header><div class="rutinas-block__empty-state"><p class="rutinas-block__empty">No hay entrenamiento cargado.</p><div class="rutinas-block__add-after"><button class="rutinas-add-row" type="button" aria-label="Agregar Entrenamiento">+</button></div></div></section><section class="rutinas-block"><header class="rutinas-block__header"><div><span class="rutinas-step">Estiramiento</span><h3>Estiramiento</h3></div></header><div class="rutinas-block__empty-state"><p class="rutinas-block__empty">No hay estiramiento cargado.</p><div class="rutinas-block__add-after"><button class="rutinas-add-row" type="button" aria-label="Agregar Estiramiento">+</button></div></div></section></div>" --> Policy: VerHistorialRutina
3. POST api/Rutina/{idRutina}/historial/{idRutinaHistorial}/restaurar --> RES: RutinaDto --> Front: Reemplaza la rutina actual por la versión histórica seleccionada --> Policy: RecuperarRutina.

## 3. Contexto

Leer antes de implementar:

- AGENTS.md
- frontend-skill.md
- Program.cs
- RutinaController.cs
- RutinaHistorialDto.cs
- RutinaHistorialResumenDto.cs
- RutinaHistorialDetalleDto.cs
- RutinaHistorialCalentamientoDto.cs
- RutinaHistorialEntrenamientoDto.cs
- RutinaHistorialEstiramientoDto.cs
- RutinaDto.cs
- historial-rutina.png (imagen de prototipo de referencia para armar el diseño actual)

## 4. Reglas y Restricciones (Constraints / Guardrails)

- No mostrar, cargar ni enviar datos del usuario autenticado en la vista creada
- Trabajar solo en el frontend.
- No modificar backend.
- No cambiar endpoints.
- No cambiar nombres de DTOs del backend.
- No hardcodear URLs completas.
- No inventar campos que no existan en los DTOs.
- No usar `window.confirm`; usar modal visual.
- No enviar enums como número; enviarlos como string.
- Mantener consistencia visual con el resto de pantallas ya implementadas.
- Permisos para botones de acción (ALGUNOS YA IMPLEMENTADOS):
  - Mostrar botón **Historial** si tiene `VER_HISTORIAL_RUTINA`.
    - Mostrar boton **Restaurar** si tiene `RECUPERAR_RUTINA`.
- Los permisos frontend deben compararse contra los permisos guardados en `localStorage`, usando la lógica/helper existente en el proyecto.
- No usar las policies backend como códigos de permiso frontend.
- Es coherente reutilizar la estética de rutinas-blocks y los paneles, pero el detalle del historial no debería mostrar controles editables, +, quitar, dropdowns, ni inputs. Debería ser una vista de lectura de la versión histórica, con el botón Restaurar aparte si tiene RECUPERAR_RUTINA.

## 5. Formato de Salida

Además de implementar el código solicitado, generar:

`IMPLEMENTATION_LOG_historial-rutinas-plan.md`

El archivo debe crearse en la misma carpeta/ruta donde se encuentra este plan `.md`.

El log debe documentar brevemente:

- qué archivos fueron creados o modificados
- qué decisiones importantes se tomaron
- cómo se resolvió la integración frontend/backend
- validaciones implementadas
- manejo de estados/loading/errors
- configuración relevante de Axios/interceptors/context/hooks si aplica
- posibles TODOs o limitaciones detectadas

El objetivo es que futuras iteraciones de IA puedan entender rápidamente qué fue implementado y por qué.
