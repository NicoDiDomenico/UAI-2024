# Etapa 6 Rutinas - Parte 1 - Gestionar Rutinas

---

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend.

## 2. Tarea Principal

Implementar en el frontend el modulo **Gestionar Rutinas**, accesible desde `/rutinas`, reemplazando el placeholder actual.

### Flujo de Endpoints

1. [Authorize] GET api/RangoHorario --> Obtengo IEnumerable<RangoHorarioDto> --> Front: Dropdown de horaDesde-horaHasta --> Obtengo un idRangoHorario.
2. [Authorize] GET api/Rutina/entrenadores/{idRangoHorario} --> Obtengo List<EntrenadorDto> --> Front: armar DataGrid con checkbox para cada Nombre y Apellido --> Obtengo un IdUsuario (Entrenador)
3. [Authorize] GET api/Rutina/entrenadores/{idUsuarioResponsable}/socios/{idRangoHorario} --> Obtengo IEnumerable<SocioTurnoDto> --> Front: armar DataGrid con checkbox para cada Nombre y Apellido --> Selecciono un IdUsuario (Socio)

## 3. Contexto

Leer antes de implementar:

- AGENTS.md
- frontend-skill.md
- Program.cs
- RangoHorarioController.cs
- RangoHorarioDto.cs
- RutinaController.cs
- EntrenadorDto.cs
- SocioTurnoDto.cs
- menu-rutina.png (imagen de prototipo de referencia para armar el diseño actual)

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
- Permisos para botones de acción:
  - Mostrar **Guardar** si tiene `EDITAR_RUTINA`.
  - Mostrar botón **Eliminar** si tiene `ELIMINAR_RUTINA`.
  - Mostrar botón **Historial** si tiene `VER_HISTORIAL_RUTINA`.
- Los permisos frontend deben compararse contra los permisos guardados en `localStorage`, usando la lógica/helper existente en el proyecto.
- No usar las policies backend como códigos de permiso frontend.
- Las respuestas erroneas del backend despues de hacer clic en "Guardar" manejarlas con un Modal que muestre la lista de errores
- esta etapa incluye solamente la selección de horario, entrenador y socio
- usar radio button para cada grilla
- Todavia no hace falta generar las pestañas de dias desde GET /api/Dia/dias, eso se hará en la siguiente parte.
- El alcance es hasta esos grid con los endpoint que te pasé, no quier crear rutinas todavia ni agregar botones "+" o armar Reporte y Limpiar, cunado se seleccione un socio mostrar un panel que diga "proximamente..." donde ahi se implementara las partes siguientes de esta estapa.

---

## 5. Formato de Salida

Además de implementar el código solicitado, generar:

`IMPLEMENTATION_LOG_gestion-rutinas-plan.md`

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
