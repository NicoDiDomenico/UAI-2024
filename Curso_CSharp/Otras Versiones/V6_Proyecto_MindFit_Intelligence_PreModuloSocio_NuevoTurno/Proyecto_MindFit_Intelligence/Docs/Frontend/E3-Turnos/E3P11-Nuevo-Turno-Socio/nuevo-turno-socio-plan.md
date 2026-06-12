# Etapa 3 Turnos - Parte 11 Nuevo Turno Socio

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend.

## 2. Tarea Principal

Implementar en el frontend el flujo de **Nuevo Turno** para el usuario logueado con rol **Socio**, disparado desde el botón:

```txt
Nuevo Turno
```

ubicado en:

```txt
Frontend/src/pages/socio/SocioInicioPage.tsx
```

Actualmente la pantalla del Socio muestra sus turnos y tiene el botón **Nuevo Turno** preparado. Se debe reemplazar el comportamiento placeholder por un modal real que permita registrar un nuevo turno para el Socio autenticado.

El flujo debe reutilizar, cuando sea posible, la implementación existente del alta de turno del lado asistente:

```txt
Frontend/src/components/socios/NuevoTurnoModal.tsx
Frontend/src/components/socios/GestionTurnosModal.tsx
Frontend/src/services/turnosService.ts
Frontend/src/types/turno.ts
Frontend/src/utils/apiError.ts
Frontend/src/App.css
```

El objetivo es mantener consistencia visual y funcional entre:

```txt
Nuevo Turno desde módulo Asistente
Nuevo Turno desde módulo Socio
```

pero usando el endpoint específico del Socio.

---

## 3. Endpoint de registro

El registro del turno desde el Socio debe usar:

```txt
POST /api/Turno/socio/registrar
```

REQ:

```txt
TurnoInsertDto
```

Policy backend:

```txt
SoloSocio
```

Endpoint backend:

```csharp
[Authorize(Policy = "SoloSocio")]
[HttpPost("socio/registrar")]
public async Task<ActionResult<TurnoDto>> SocioRegistraTurno(TurnoInsertDto turnoInsertDto)
{
    var claimIdUsuario = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (!int.TryParse(claimIdUsuario, out var idUsuarioSocio))
        return Unauthorized();

    turnoInsertDto.IdUsuarioSocio = idUsuarioSocio;

    if (!await _turnoService.ValidateAsync(turnoInsertDto))
        return Conflict(_turnoService.Errors);

    var turno = await _turnoService.RegistrarTurno(turnoInsertDto);
    if (turno is null)
        return Conflict(_turnoService.Errors);

    return Ok(turno);
}
```

Punto importante:

- El frontend no debe obtener ni enviar el `idUsuarioSocio` como dato confiable desde la pantalla.
- El backend toma el Socio desde el JWT y sobrescribe `turnoInsertDto.IdUsuarioSocio`.
- Antes de implementar, revisar `TurnoInsertDto.cs`.
- Si `IdUsuarioSocio` es nullable, se puede enviar `null`.
- Si `IdUsuarioSocio` es `int` no nullable, no enviar `null`; enviar el valor default aceptado por el contrato, por ejemplo `0`, sabiendo que backend lo reemplaza desde el JWT.
- No usar `session.datosPersonales.id` para armar `IdUsuarioSocio`, salvo que el backend/DTO lo requiera estrictamente para model binding. La fuente de verdad para el socio en este endpoint es el JWT.

---

## 4. Flujo del modal

Al hacer clic en **Nuevo Turno** desde `SocioInicioPage.tsx`, abrir un modal visualmente consistente con el modal existente de alta de turno del asistente.

El modal debe contener:

- Campo **Fecha Turno** con Date Picker.
- Selector **Rango Horario**.
- Grilla/listado de entrenadores disponibles.
- Botón principal **Registrar Turno**.
- Botón para cerrar/volver.

Al abrir el modal:

1. El Date Picker debe inicializarse con la fecha actual del sistema.
2. Con esa fecha inicial, consultar disponibilidad:

```txt
GET /api/DiaRangoHorario/grilla-por-dia?fecha=yyyy-mm-dd
```

3. El endpoint está protegido con `[Authorize]`.
4. La fecha enviada debe tener formato:

```txt
yyyy-mm-dd
```

5. Con la respuesta, construir el selector de rangos horarios y la grilla de entrenadores.

---

## 5. Carga del selector Rango Horario

El selector **Rango Horario** debe cargarse a partir de los elementos recibidos en:

```txt
GrillaDiaRangoHorarioDto
```

Cada opción debe representar un rango horario disponible.

Texto visible:

```txt
HoraDesde - HoraHasta
```

Ejemplo:

```txt
16:00 - 17:00
```

Cada opción debe quedar asociada internamente a:

```txt
IdDiaRangoHorario
```

Solo deben mostrarse rangos horarios con:

```txt
Activo = true
```

Si no hay rangos activos para la fecha seleccionada, mostrar un mensaje claro:

```txt
No hay rangos horarios disponibles para la fecha seleccionada.
```

---

## 6. Carga de la grilla de entrenadores

Cuando el usuario seleccione un rango horario, mostrar en una grilla los entrenadores asociados a ese rango.

Los entrenadores se obtienen desde:

```txt
GrillaDiaRangoHorarioDto.Responsables
```

La grilla debe tener las columnas:

```txt
Selección
Entrenador
Disponibilidad
```

Detalle:

- **Selección**: radio button para seleccionar un único entrenador.
- **Entrenador**: mostrar `Nombre Apellido`.
- **Disponibilidad**: mostrar `CupoActual/CupoMaximo`.

La disponibilidad pertenece al rango horario seleccionado, no al entrenador individual.

Por lo tanto, para todos los responsables del mismo rango se debe mostrar el mismo valor:

```txt
CupoActual/CupoMaximo
```

Si no hay entrenadores disponibles para el rango seleccionado, mostrar:

```txt
No hay entrenadores disponibles para este día y horario.
```

---

## 7. Comportamiento esperado

El usuario Socio debe poder:

1. Elegir una fecha.
2. Seleccionar un rango horario disponible.
3. Seleccionar un entrenador.
4. Presionar **Registrar Turno**.

Al cambiar la fecha:

- volver a consultar `GET /api/DiaRangoHorario/grilla-por-dia`
- actualizar el selector de rangos horarios
- limpiar el rango horario seleccionado anteriormente
- limpiar el entrenador seleccionado anteriormente
- limpiar errores de registro anteriores
- actualizar la grilla de entrenadores

Al cambiar el rango horario:

- actualizar la grilla de entrenadores
- limpiar el entrenador seleccionado anteriormente
- limpiar errores de registro anteriores

---

## 8. Registro del turno

Al presionar **Registrar Turno**, llamar a:

```txt
POST /api/Turno/socio/registrar
```

El payload debe respetar `TurnoInsertDto`.

Campos esperados del DTO:

```txt
IdUsuarioSocio
IdUsuarioResponsable
Fecha
IdDiaRangoHorario
```

Reglas para armar el DTO desde el frontend:

- `IdUsuarioResponsable`: se obtiene del entrenador seleccionado.
- `Fecha`: se obtiene del Date Picker.
- `IdDiaRangoHorario`: se obtiene del rango horario seleccionado.
- `IdUsuarioSocio`: no debe tomarse de una selección de socio, porque en este flujo el socio es el usuario autenticado.
- El backend resuelve el Socio desde el JWT.

Para `IdUsuarioSocio`:

- revisar primero el tipo real en `TurnoInsertDto.cs`
- si el contrato permite `null`, enviar `null`
- si el contrato requiere número, enviar un valor default compatible, por ejemplo `0`
- no usar el endpoint del asistente para registrar el turno del Socio

No usar:

```txt
POST /api/Turno/asistente/registrar
```

Usar solamente:

```txt
POST /api/Turno/socio/registrar
```

---

## 9. Validaciones del modal

Antes de registrar el turno, validar desde el frontend que:

- exista fecha seleccionada
- exista rango horario seleccionado
- exista entrenador seleccionado
- el rango horario seleccionado tenga cupo disponible:

```txt
CupoActual < CupoMaximo
```

No validar socio seleccionado, porque en este flujo el Socio se obtiene desde el JWT en backend.

Si falta algún dato requerido, no llamar al endpoint y mostrar un mensaje claro dentro del modal.

---

## 10. Estados visuales y manejo de errores

El modal debe manejar:

- loading al cargar disponibilidad
- loading al registrar el turno
- error si no se puede cargar disponibilidad
- error si no se puede registrar el turno
- mensaje de éxito cuando el turno se registra correctamente
- bloqueo del botón **Registrar Turno** mientras se procesa la solicitud para evitar doble registro

Si el backend devuelve errores reales, mostrarlos usando los helpers existentes de errores, por ejemplo:

```txt
getRegistrarTurnoErrorMessage
```

Si el backend devuelve `409 Conflict` con errores de negocio, mostrar el mensaje real cuando esté disponible.

---

## 11. Comportamiento luego del registro exitoso

Cuando el turno se registre correctamente:

- mostrar un mensaje de éxito dentro del modal o cerrar el modal según el patrón actual del proyecto
- refrescar la grilla de turnos del Socio llamando nuevamente a:

```txt
GET /api/Turno/socio
```

- limpiar la selección de turno actual en `SocioInicioPage`
- dejar la pantalla consistente, sin datos obsoletos

Preferencia para mantener consistencia con el flujo existente del asistente:

- si `NuevoTurnoModal` del asistente actualmente cierra automáticamente al registrar correctamente, mantener el mismo patrón para el Socio
- si se mantiene abierto, mostrar éxito y limpiar selección de entrenador

---

## 12. Contexto

- AGENTS.md
- frontend-skill.md
- Docs/Frontend/E3-Turnos/E3P8-Nuevo-Turno/nuevo-turno-plan.md
- Docs/Frontend/E3-Turnos/E3P8-Nuevo-Turno/IMPLEMENTATION_LOG_nuevo-turno-plan.md
- Docs/Frontend/E3-Turnos/E3P9-Modulo-Socios/modulo-socios.md
- Docs/Frontend/E3-Turnos/E3P9-Modulo-Socios/IMPLEMENTATION_LOG_modulo-socios.md
- Frontend/src/pages/socio/SocioInicioPage.tsx
- Frontend/src/components/socios/NuevoTurnoModal.tsx
- Frontend/src/components/socios/GestionTurnosModal.tsx
- Frontend/src/services/turnosService.ts
- Frontend/src/types/turno.ts
- Frontend/src/utils/apiError.ts
- Frontend/src/App.css
- DiaRangoHorarioController.cs
- GrillaDiaRangoHorarioDto.cs
- GrillaDiaRangoHorarioResponsableDto.cs
- TurnoController.cs
- TurnoInsertDto.cs
- TurnoDto.cs

---

## 13. Reglas y Restricciones

- Trabajar únicamente en `/Frontend`.
- No modificar backend.
- No cambiar rutas, nombres de endpoints ni contratos del backend.
- No usar el endpoint del asistente para registrar turnos del Socio.
- Usar exactamente:

```txt
POST /api/Turno/socio/registrar
```

- Centralizar la llamada HTTP en:

```txt
Frontend/src/services/turnosService.ts
```

- Usar el `apiClient` existente.

- Respetar el interceptor existente para enviar automáticamente:
  - `Authorization: Bearer {accessToken}`
  - `X-Gym-Id: {idGym}`

- No hardcodear URLs absolutas.

- No agregar dependencias nuevas salvo que sea estrictamente necesario.

- Reutilizar componentes, modales, helpers, tipos y estilos existentes siempre que sea posible.

- Mantener consistencia visual con `NuevoTurnoModal`.

- No usar permisos frontend como `AGREGAR_TURNO` para mostrar u ocultar el botón en la pantalla del Socio.

- La autorización del endpoint la resuelve el backend con la policy `SoloSocio`.

- No usar el nombre de la policy backend como permiso frontend.

- No enviar `idUsuarioSocio` como dato obtenido de una selección de la UI.

- No modificar el flujo de `/dashboard`.

- No modificar el flujo del asistente.

- No romper:
  - `POST /api/Turno/asistente/registrar`
  - `GET /api/Turno/asistente/{idUsuarioSocio}`
  - `GET /api/Turno/socio`
  - `PATCH /api/Turno/socio/cancelar/{idTurno}`

---

## 14. Aclaraciones finales para implementación

### 1. Reutilización del modal existente

Actualmente existe:

```txt
Frontend/src/components/socios/NuevoTurnoModal.tsx
```

Ese modal fue creado para el flujo del asistente y recibe un socio seleccionado.

Para el flujo del Socio, revisar si conviene:

- reutilizar `NuevoTurnoModal` agregando una variante `mode`
- crear un nuevo componente específico, por ejemplo:

```txt
Frontend/src/components/socio/NuevoTurnoSocioModal.tsx
```

- extraer un componente compartido para evitar duplicación

Preferencia:

- si los cambios son pequeños, reutilizar lógica existente
- si reutilizar obliga a llenar props falsas de socio o vuelve confuso el componente, crear un modal específico para Socio
- no sobrediseñar

### 2. Service de registro del Socio

En `turnosService.ts`, agregar una función nueva separada:

```ts
async registrarTurnoSocio(request: TurnoInsertRequest) {
  const response = await apiClient.post<TurnoHistorialItem>('/Turno/socio/registrar', request)
  return response.data
}
```

No modificar ni romper:

```ts
registrarTurnoAsistente(request);
```

### 3. Tipo del request

Revisar el tipo existente:

```txt
Frontend/src/types/turno.ts
```

Si `TurnoInsertRequest` actualmente exige:

```ts
idUsuarioSocio: number;
```

evaluar una de estas opciones:

- permitir `idUsuarioSocio: number | null`
- crear un tipo específico para el flujo Socio
- armar el payload con `idUsuarioSocio: 0` si `TurnoInsertDto.cs` define `IdUsuarioSocio` como `int` no nullable

La decisión final debe basarse en `TurnoInsertDto.cs`.

No cambiar el tipo de forma que rompa el flujo del asistente.

### 4. Fecha

Usar el mismo criterio que ya usa `NuevoTurnoModal`.

El endpoint de disponibilidad usa:

```txt
yyyy-mm-dd
```

El endpoint de registro debe enviar la fecha en un formato compatible con el backend `DateTime`.

Mantener el formato que ya funciona en el flujo del asistente, salvo que `TurnoInsertDto.cs` indique otra cosa.

### 5. Registro exitoso

Luego del registro exitoso:

- cerrar el modal si ese es el patrón actual de `NuevoTurnoModal`
- refrescar la grilla del Socio con `GET /api/Turno/socio`
- limpiar selección actual de turno
- mostrar la grilla actualizada

### 6. Nuevo Turno en pantalla Socio

El botón **Nuevo Turno** en `SocioInicioPage.tsx` debe dejar de mostrar `Próximamente...`.

Debe abrir el modal real de registro de turno para Socio.

---

## 15. Formato de Salida

Además de implementar el código solicitado, generar:

```txt
IMPLEMENTATION_LOG_nuevo-turno-socio-plan.md
```

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

---

## 16. Verificación esperada

Al finalizar, verificar:

- `npm run build` ejecuta correctamente.
- Al entrar como Socio a `/socio/inicio`, el botón **Nuevo Turno** abre el modal real.
- Al abrir el modal, se consulta:

```txt
GET /api/DiaRangoHorario/grilla-por-dia?fecha=yyyy-mm-dd
```

- Al cambiar la fecha, se vuelve a consultar disponibilidad.
- Solo se muestran rangos activos.
- Al seleccionar rango, se muestran los entrenadores disponibles.
- Si no hay entrenadores, se muestra:

```txt
No hay entrenadores disponibles para este día y horario.
```

- Si no hay cupo disponible, no se llama al endpoint de registro.
- Al registrar, se llama a:

```txt
POST /api/Turno/socio/registrar
```

- No se llama a:

```txt
POST /api/Turno/asistente/registrar
```

- La request incluye automáticamente:
  - `Authorization`
  - `X-Gym-Id`

- No se depende de un socio seleccionado en UI.

- Si el registro es exitoso, se refresca la grilla del Socio con:

```txt
GET /api/Turno/socio
```
