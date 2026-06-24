# Etapa 6 Rutinas - Parte 2 - Consultar Rutinas

---

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend.

## 2. Tarea Principal

Implementar en el frontend la seccion de **Consultar Rutinas** reeemplazando el elemento "<section class="rutinas-coming-soon" aria-live="polite"><span class="rutinas-step">Siguiente etapa</span><h2>Espacio de rutina</h2><p>Completa los tres pasos para preparar el area de trabajo.</p><ol class="rutinas-progress" aria-label="Progreso de seleccion"><li class="">Horario</li><li class="">Entrenador</li><li class="">Socio</li></ol></section>" en el modulo **Gestionar Rutinas** accesible desde `/rutinas`.

### Flujo de Endpoints

1. [Authorize] GET api/Dia/dias --> RES: IEnumerable<DiaDto> --> Front: Botones que representan cada NombreDia con su IdDia vinculado. Colocar todos los NombreDia que traiga el IEnumerable por mas que en la imagen de menu-rutina.png no tenga Domingo --> El usuario al ahcer clic en uno de esos botones con NombreDia el sistema obtiene IdDia para usarlo como query param en el próximo endpoint.
2. [Authorize] GET api/Rutina/socios/{idUsuarioSocio}/rutinas?idDia=X --> RES: RutinaDto --> Front: Armo 3 paneles que contienen lineas de elementos visuales para Calentamiento, Entrenamiento, Estiramiento, y ademas habilito botones de “Guardar”, “Eliminar” e "Historial" --> El sistema carga la RutinaDto obtenida en lineas de los 3 paneles que se armará en el frontend. El Entrenador puede agregar mas lineas en cada panel al apretar el botón “+” al lado de cada uno. Cada linea nueva debe tener lo siguiente:

- Un dropdown con los NombreMusculo que se obtiene de "[Authorize] GET api/GrupoMuscular --> RES: IEnumerable<GrupoMuscularDto>" que va a servir para obtener el IdGrupoMuscular y usarlo en el próximo endpoint. Por lo tanto es obligatorio seleccionar un grupo muscular.
- Otro dropdown para mostrar las DescEjercicio que se obtiene de " [Authorize] GET api/Ejercicio?idGrupoMuscular=1 --> RES: IEnumerable<EjercicioDto>" que va a servir para obtener el IdEjercicio y usarlo en el body del proximo endpoint que se ejecutara cuando se haga clic en "Guardar" (se implementará en la parte 3).
- resto de campos que no sean ids que estan en CalentamientoDto, EntrenamientoDto y EstiramientoDto.

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
  - Botón **+** mostrarlo solo si el usuario tiene EDITAR_RUTINA.
  - Mostrar botón **Eliminar** si tiene `ELIMINAR_RUTINA`.
  - Mostrar botón **Historial** si tiene `VER_HISTORIAL_RUTINA`.
- Los permisos frontend deben compararse contra los permisos guardados en `localStorage`, usando la lógica/helper existente en el proyecto.
- No usar las policies backend como códigos de permiso frontend.
- No se va a implementar la vista ni la funcionalidad del boton "Reporte" en esta etapa, tampoco las funcionalidades de los botones Guardar, Eliminar e Historial pero si se habiliataran su vista segun permisos.
- Por ahora las lineas que se crean en cada panel de Calentamiento/Estiramiento/Entrenamiento son para cargar la rutina del Socio que ya trae, en caso de no traer una de estas 3 mostrar un label en cada panel que informe que no hay Calentamiento o Estiramiento o Entrenamiento segun corresponda.
- Al seleccionar un socio se debe elegir automáticamente la rutina de ese socio en el día actual.
- si no hay rutina mostrar “El socio no asiste este día” pero debajo de los botones de dia, no hace falta mostrar los 3 paneles y repetir en cada uno ese mensaje, dejar ese mensaje grande en lugar de lso paneles.
- si no logras matchear NombreDia con el día actual del sistema, usa como fallback el primer día que venga de GET /api/Dia/dias.
- Aclaracion importante: las lineas que se se cargan porque se traen en la rutina de un socio son igual de editables que las que se agregan manualmente desde el "+", se tienen que poder quitar y editar para agregarle otros ejercicios del dropdown o cambiar otros campos, solo que hasta que no aprete guardar eso no se va a guardar en la bd, es todo en memoria del front asi que por mas que cambie todo lo que se trajo si regarco la pagia debe volver como antes

## 5. Formato de Salida

Además de implementar el código solicitado, generar:

`IMPLEMENTATION_LOG_consultar-rutinas-plan.md`

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
