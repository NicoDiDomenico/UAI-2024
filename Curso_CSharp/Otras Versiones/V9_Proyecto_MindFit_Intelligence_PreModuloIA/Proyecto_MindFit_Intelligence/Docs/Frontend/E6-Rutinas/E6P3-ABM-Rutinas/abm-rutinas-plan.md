# Etapa 6 Rutinas - Parte 3 - Alta Baja Modificacion de Rutinas

---

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend.

## 2. Tarea Principal

Implementar en el frontend los endpoints de los botones "Guardar" y "Eliminar" en el modulo **Gestionar Rutinas**, accesible desde `/rutinas`.

### Flujo de Endpoints

Para Agregar/Modificar Rutina:

1. PUT api/Rutina/{idRutina}/bloques --> REQ: RutinaBloquesUpdateDto, RES: RutinaDto --> Front: se actualizan los paneles de Calentamiento, Entrenamiento, Estiramiento --> Policy: EditarRutina.

Eliminar Rutina:

2. PATCH api/Rutina/{idRutina}/estado --> REQ: RutinaEstadoUpdateDto --> Front: Boton "Eliminar" desactiva la rutina activa actualmente consultada enviando `{ "activo": false }`, ademas hacer un modal de confirmacion para aegurar si realmente quiere desactivar la rutina actual --> Policy: EliminarRutina.

Importante:

- En esta etapa **no se implementa Activar rutina**.
- Motivo: el endpoint de consulta `GET api/Rutina/socios/{idUsuarioSocio}/rutinas?idDia=X` devuelve solamente rutinas activas. Si una rutina fue desactivada, el frontend recibe `404` y no obtiene `idRutina`, por lo que no tiene un identificador confiable para volver a activar esa rutina.
- Por lo tanto, el boton **Eliminar** solo debe mostrarse cuando hay una `RutinaDto` activa cargada en pantalla y el usuario tiene `ELIMINAR_RUTINA`.
- Luego de eliminar/desactivar, el frontend debe refrescar la consulta del socio/dia actual y mostrar el estado sin rutina activa.

## 3. Contexto

Leer antes de implementar:

- AGENTS.md
- frontend-skill.md
- Program.cs
- DiaController.cs
- DiaDto.cs
- RutinaController.cs
- RutinaDto.cs
- CalentamientoDto.cs
- EntrenamientoDto.cs
- EstiramientoDto.cs
- EjercicioDto.cs
- GrupoMuscularDto.cs
- TipoEjercicioDto.cs
- MaquinaDto.cs
- EquipamientoDto.cs
- TipoDeEjercicio.cs
- Musculo.cs
- menu-rutina.png (imagen de prototipo de referencia para armar el diseño actual)
- EjercicioController.cs
- GrupoMuscularController.cs
- RutinaBloquesUpdateDto.cs
- CalentamientoInsertDto.cs
- EntrenamientoInsertDto.cs
- EstiramientoInsertDto.cs

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
  - Mostrar **Guardar** si tiene `EDITAR_RUTINA`.
  - Botón **+** mostrarlo solo si el usuario tiene EDITAR_RUTINA.
  - Mostrar botón **Eliminar** si tiene `ELIMINAR_RUTINA`.
  - Mostrar botón **Historial** si tiene `VER_HISTORIAL_RUTINA`.
- Los permisos frontend deben compararse contra los permisos guardados en `localStorage`, usando la lógica/helper existente en el proyecto.
- No usar las policies backend como códigos de permiso frontend.

## 5. Formato de Salida

Además de implementar el código solicitado, generar:

`IMPLEMENTATION_LOG_abm-rutinas-plan.md`

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
