# Etapa 3 Turnos - Parte 8 Nuevo Turno

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend

## 2. Tarea Principal

Implementar en el frontend el flujo de **Nuevo Turno** para un socio, disparado desde el botón:

`Nuevo Turno`

Este botón ya existe en la pantalla anterior de gestión/consulta de turnos. Al hacer clic, debe abrirse un **modal** que permita registrar un nuevo turno para el socio seleccionado.

El modal debe seguir una interfaz similar al prototipo adjunto y contener:

- Un campo **Fecha Turno** con Date Picker.
- Un selector **Rango Horario**.
- Una grilla/listado de entrenadores disponibles para la fecha y rango horario seleccionados.
- Un botón principal **Registrar Turno**.

---

### Flujo inicial del modal

Al abrir el modal:

1. El Date Picker debe inicializarse con la fecha actual del sistema.
2. Con esa fecha inicial, el frontend debe consultar la disponibilidad llamando al endpoint:

   `GET api/DiaRangoHorario/grilla-por-dia?fecha=yyyy-mm-dd`

   Endpoint protegido con `[Authorize]`.

   RES:

   `IEnumerable<GrillaDiaRangoHorarioDto>`

3. La fecha enviada debe tener formato:

   `yyyy-mm-dd`

4. Con la respuesta del endpoint, el frontend debe construir el contenido del modal.

---

### Carga del selector Rango Horario

El dropdown/select **Rango Horario** debe cargarse a partir de los elementos recibidos en:

`GrillaDiaRangoHorarioDto`

Cada opción del select debe representar un rango horario disponible.

El texto visible de cada opción debe tener el formato:

`HoraDesde - HoraHasta`

Por ejemplo:

`16:00 - 17:00`

Cada opción debe quedar asociada internamente a su correspondiente:

`IdDiaRangoHorario`

Este dato será necesario luego para armar el `TurnoInsertDto`.

---

### Carga de la grilla de entrenadores

Cuando el usuario seleccione un rango horario, el frontend debe mostrar en una grilla los entrenadores asociados a ese rango.

Los entrenadores se obtienen desde la propiedad:

`Responsables`

del `GrillaDiaRangoHorarioDto` seleccionado.

Esa propiedad contiene una lista de:

`GrillaDiaRangoHorarioResponsableDto`

La grilla debe tener las siguientes columnas:

1. **Selección**
   - Radio button para seleccionar un único entrenador.

2. **Entrenador**
   - Mostrar la combinación de:
     - `Nombre`
     - `Apellido`

   Formato sugerido:

   `Nombre Apellido`

3. **Disponibilidad**
   - Mostrar la relación entre:
     - `CupoActual`
     - `CupoMaximo`

   Formato:

   `CupoActual/CupoMaximo`

   Ejemplo:

   `01/37`

La disponibilidad pertenece al rango horario seleccionado, no al entrenador individual. Por lo tanto, para todos los responsables de ese mismo rango se debe mostrar el mismo valor de disponibilidad correspondiente al `GrillaDiaRangoHorarioDto` seleccionado.

---

### Comportamiento esperado

El usuario debe poder:

1. Elegir una fecha desde el Date Picker.
2. Seleccionar un rango horario disponible.
3. Seleccionar un entrenador disponible dentro de ese rango.
4. Presionar **Registrar Turno**.

Al cambiar la fecha:

- debe volver a consultarse el endpoint `GET api/DiaRangoHorario/grilla-por-dia`
- debe actualizarse el selector de rangos horarios
- debe limpiarse el rango horario seleccionado anteriormente
- debe limpiarse el entrenador seleccionado anteriormente
- debe actualizarse la grilla de entrenadores según la nueva fecha

Al cambiar el rango horario:

- debe actualizarse la grilla de entrenadores
- debe limpiarse el entrenador seleccionado anteriormente

---

### Registro del turno

Al presionar **Registrar Turno**, el frontend debe llamar al endpoint:

`POST api/Turno/asistente/registrar`

REQ:

`TurnoInsertDto`

Policy backend:

`AgregarTurno`

El DTO debe armarse usando:

- el `IdUsuario` del socio seleccionado en la pantalla/formulario anterior
- el `IdDiaRangoHorario` del rango horario seleccionado
- el `IdUsuarioResponsable` del entrenador seleccionado
- la `Fecha` seleccionada en el Date Picker

No inventar campos del DTO. Antes de implementar, revisar el modelo real `TurnoInsertDto` en el backend y mapear los campos exactamente según ese contrato.

---

### Validaciones del modal

Antes de registrar el turno, validar desde el frontend que:

- exista un socio seleccionado
- exista una fecha seleccionada
- exista un rango horario seleccionado
- exista un entrenador seleccionado
- el rango horario seleccionado tenga cupo disponible, es decir, que `CupoActual < CupoMaximo`.

Si falta alguno de esos datos, no llamar al endpoint de registro y mostrar un mensaje claro dentro del modal.

---

### Estados visuales y manejo de errores

El modal debe manejar:

- loading al cargar la grilla de disponibilidad
- loading al registrar el turno
- error si no se puede cargar la disponibilidad
- error si no se puede registrar el turno
- mensaje de éxito cuando el turno se registra correctamente
- bloqueo del botón **Registrar Turno** mientras se está enviando la solicitud, para evitar doble registro

Luego de registrar correctamente el turno:

- mostrar un mensaje de éxito dentro del modal
- refrescar la información necesaria de la pantalla anterior si corresponde
- mantener la interfaz en un estado consistente

## 3. Contexto

- AGENTS.md
- frontend-skill.md
- IMPLEMENTATION_LOG_gestion-turnos-plan.md
- DiaRangoHorarioController.cs
- GrillaDiaRangoHorarioDto.cs
- GrillaDiaRangoHorarioResponsableDto.cs
- TurnoController.cs
- TurnoInsertDto.cs
- registrar-turno.png

## 4. Reglas y Restricciones (Constraints / Guardrails)

- Botón de acción "Registrar Turno" requiere el permiso frontend: `AGREGAR_TURNO`
- No modificar backend. El backend es fuente de verdad para endpoints, DTOs y reglas de negocio.
- Si la grilla/listado de entrenadores está vacía colocar un texto que diga "No hay entrenadores disponibles para este día y horario."
- Antes de implementar el registro, revisar TurnoInsertDto.cs y mapear exactamente los campos reales del DTO. No inventar nombres de propiedades.

## 5. Formato de Salida

Además de implementar el código solicitado, generar: IMPLEMENTATION_LOG_nuevo-turno-plan.md

El archivo debe crearse en la misma carpeta/ruta donde se encuentra este plan .md.

El log debe documentar brevemente:

- qué archivos fueron creados o modificados
- qué decisiones importantes se tomaron
- cómo se resolvió la integración frontend/backend
- validaciones implementadas
- manejo de estados/loading/errors
- configuración relevante de Axios/interceptors/context/hooks si aplica
- posibles TODOs o limitaciones detectadas

El objetivo es que futuras iteraciones de IA puedan entender rápidamente qué fue implementado y por qué.

## 6. Aclaraciones finales para implementación

Para evitar interpretaciones durante la implementación, aplicar las siguientes decisiones:

1. Rangos horarios inactivos

Si algún elemento de `GrillaDiaRangoHorarioDto` viene con:

`Activo = false`

no debe mostrarse como opción disponible en el selector **Rango Horario**.

El usuario solo debe poder seleccionar rangos horarios activos.

2. Formato de fecha para registrar el turno

Para el endpoint:

`POST api/Turno/asistente/registrar`

el campo `Fecha` del `TurnoInsertDto` debe enviarse usando la fecha seleccionada en el Date Picker.

Como en backend el tipo es `DateTime`, enviar un valor ISO válido normalizado al inicio del día:

`yyyy-mm-ddT00:00:00`

Ejemplo:

`2026-06-09T00:00:00`

3. Campos reales del TurnoInsertDto

El DTO de registro debe armarse respetando los campos reales del backend:

- `IdUsuarioSocio`
- `IdUsuarioResponsable`
- `Fecha`
- `IdDiaRangoHorario`

No usar nombres alternativos como `IdUsuario` si el DTO real espera `IdUsuarioSocio`.

4. Disponibilidad

La disponibilidad se calcula a nivel de rango horario, no a nivel de entrenador.

Por lo tanto:

- `CupoActual`
- `CupoMaximo`

se toman del `GrillaDiaRangoHorarioDto` seleccionado.

Para todos los responsables de ese mismo rango horario se debe mostrar la misma disponibilidad:

`CupoActual/CupoMaximo`

5. Comportamiento luego del registro exitoso

Cuando el turno se registre correctamente:

- mantener el modal abierto
- mostrar un mensaje de éxito dentro del modal
- bloquear dobles envíos mientras se procesa la solicitud
- limpiar la selección de entrenador
- refrescar la información de la pantalla anterior si corresponde

No cerrar automáticamente el modal salvo que el diseño existente del proyecto ya tenga ese patrón para operaciones similares.
