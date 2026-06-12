# Etapa 3 Turnos - Parte 10 Cancelar Turno desde Modulo Socio

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend

## 2. Tarea Principal

### Primera Implementación - Cancelar Turno Socio

Implementar la funcionalidad real del botón **Cancelar Turno** en la pantalla del Socio:

```txt
Frontend/src/pages/socio/SocioInicioPage.tsx
```

Actualmente la pantalla del Socio muestra una grilla con sus turnos y permite seleccionar un turno. También existe el botón **Cancelar Turno**, pero todavía no ejecuta la cancelación real.

Se debe completar el flujo para que, al presionar **Cancelar Turno**, el frontend permita confirmar la acción y luego cancele el turno seleccionado consumiendo el endpoint del backend:

```txt
PATCH /api/Turno/socio/cancelar/{idTurno}
```

Policy backend:

```txt
SoloSocio
```

Endpoint backend:

```csharp
[Authorize(Policy = "SoloSocio")]
[HttpPatch("socio/cancelar/{idTurno}")]
public async Task<ActionResult> SocioCancelarTurno(int idTurno)
{
    var resultado = await _turnoService.CancelarTurno(idTurno);

    if (!resultado)
    {
        if (_turnoService.Errors.Any())
            return Conflict(new { message = _turnoService.Errors });

        return NotFound($"No se encontró el turno con ID: {idTurno}");
    }

    return NoContent();
}
```

Comportamiento requerido:

1. El usuario Socio selecciona un turno de la grilla.
2. El sistema obtiene internamente el `idTurno` del turno seleccionado.
3. El botón **Cancelar Turno** debe permanecer deshabilitado si no hay un turno seleccionado.
4. Al hacer clic en **Cancelar Turno**, mostrar un modal o mensaje de confirmación con el texto:

```txt
¿Confirma que desea cancelar este turno?
```

5. Si el usuario cancela la confirmación:
   - no ejecutar ninguna llamada al backend
   - mantener la pantalla y la selección sin cambios

6. Si el usuario confirma:
   - ejecutar:

```txt
PATCH /api/Turno/socio/cancelar/{idTurno}
```

- usar el `idTurno` del turno seleccionado
- mostrar estado de loading mientras se procesa
- evitar doble submit mientras la operación está en curso

7. Si la cancelación es exitosa:
   - el backend responde `204 No Content`
   - mostrar un mensaje de éxito claro, por ejemplo:

```txt
El turno fue cancelado correctamente.
```

- cerrar el modal de confirmación
- limpiar la selección actual
- refrescar la grilla de turnos del Socio llamando nuevamente a:

```txt
GET /api/Turno/socio
```

- dejar la pantalla consistente, sin datos obsoletos

8. Si la cancelación falla:
   - mostrar el mensaje real devuelto por el backend cuando esté disponible
   - si no hay mensaje específico, mostrar un mensaje genérico claro
   - no romper la pantalla
   - no borrar la grilla
   - no limpiar la selección salvo que sea necesario por consistencia

## 3. Contexto

- AGENTS.md
- frontend-skill.md
- Docs/Frontend/E3-Turnos/E3P9-Modulo-Socios/modulo-socios.md
- Docs/Frontend/E3-Turnos/E3P9-Modulo-Socios/IMPLEMENTATION_LOG_modulo-socios.md
- Docs/Frontend/E3-Turnos/E3P7-Cancelar-Turno/cancelar-turno-plan.md
- Docs/Frontend/E3-Turnos/E3P7-Cancelar-Turno/IMPLEMENTATION_LOG_cancelar-turno-plan.md
- Frontend/src/pages/socio/SocioInicioPage.tsx
- Frontend/src/services/turnosService.ts
- Frontend/src/types/turno.ts
- Frontend/src/utils/apiError.ts
- Frontend/src/App.css
- TurnoController.cs
- TurnoDto.cs
- EstadoTurno.cs

## 4. Reglas y Restricciones (Constraints / Guardrails)

- Trabajar únicamente en `/Frontend`.
- No modificar backend.
- No cambiar rutas, nombres de endpoints ni contratos del backend.
- Usar exactamente el endpoint:

```txt
PATCH /api/Turno/socio/cancelar/{idTurno}
```

- No usar el endpoint del asistente:

```txt
PATCH /api/Turno/asistente/cancelar/{idTurno}
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

- Reutilizar componentes, modales, helpers y estilos existentes siempre que sea posible.

- Mantener consistencia visual con el flujo ya implementado para cancelar turnos desde el lado asistente.

- Revisar como referencia:

```txt
Frontend/src/components/socios/GestionTurnosModal.tsx
```

- Evitar duplicar lógica si ya existe una estructura reutilizable de confirmación, loading, éxito o error.
- Validar que exista `idTurno` antes de llamar al endpoint.
- El botón **Cancelar Turno** debe estar deshabilitado si no hay un turno seleccionado.
- El botón **Cancelar Turno** debe bloquearse mientras la cancelación está en curso para evitar doble submit.
- En la pantalla del Socio no usar permisos frontend como `CANCELAR_TURNO` para mostrar u ocultar el botón.
- La autorización del endpoint la resuelve el backend con la policy `SoloSocio`.
- No usar el nombre de la policy backend como permiso frontend.
- No enviar `idUsuarioSocio`; el backend identifica al Socio desde el JWT.
- No modificar el flujo de `/dashboard`.
- No modificar el flujo del asistente.
- No romper el endpoint existente del asistente:

```txt
GET /api/Turno/asistente/{idUsuarioSocio}
PATCH /api/Turno/asistente/cancelar/{idTurno}
```

### Validación de estado antes de cancelar

Antes de ejecutar:

```txt
PATCH /api/Turno/socio/cancelar/{idTurno}
```

el frontend debe validar el estado del turno seleccionado.

Estados posibles:

```txt
EnCurso = 1
Cancelado = 2
Finalizado = 3
Vencido = 4, si existe en el frontend/backend actual
```

Solo se permite cancelar turnos cuyo estado sea:

```txt
EnCurso
```

Si el turno se encuentra en:

```txt
Cancelado
Finalizado
Vencido
```

el frontend no debe invocar el endpoint.

En esos casos mostrar un mensaje apropiado al usuario:

```txt
El turno ya fue cancelado.
No es posible cancelar un turno finalizado.
No es posible cancelar un turno vencido.
```

La validación debe realizarse antes de mostrar la confirmación y antes de ejecutar la llamada HTTP.

El endpoint únicamente debe invocarse cuando el estado actual sea `EnCurso`.

### Manejo de errores backend

El backend puede responder:

```txt
204 No Content
```

cuando la cancelación fue exitosa.

También puede responder:

```txt
409 Conflict
```

con un body similar a:

```json
{
  "message": [
    "No se puede cancelar el turno con menos de 3 horas de antelación."
  ]
}
```

También puede responder:

```txt
404 Not Found
```

con un mensaje como:

```txt
No se encontró el turno con ID: {idTurno}
```

El frontend debe intentar mostrar el mensaje real del backend cuando esté disponible.

Si el backend devuelve `message` como array, mostrarlo de forma legible para el usuario.

Si no hay mensaje específico, usar un fallback:

```txt
No pudimos cancelar el turno seleccionado. Intentá nuevamente en unos minutos.
```

## 5. Formato de Salida

Además de implementar el código solicitado, generar:

```txt
IMPLEMENTATION_LOG_agregar-cancelar-turno-socio.md
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

## 6. Verificación esperada

Al finalizar, verificar:

- `npm run build` ejecuta correctamente.
- El botón **Cancelar Turno** está deshabilitado si no hay turno seleccionado.
- Al seleccionar un turno `EnCurso`, el botón **Cancelar Turno** queda habilitado.
- Al presionar **Cancelar Turno**, se muestra la confirmación:

```txt
¿Confirma que desea cancelar este turno?
```

- Si el usuario cancela, no se ejecuta ningún request.
- Si el usuario confirma, se ejecuta:

```txt
PATCH /api/Turno/socio/cancelar/{idTurno}
```

- La request incluye automáticamente:
  - `Authorization`
  - `X-Gym-Id`

- No se envía `idUsuarioSocio`.

- Si el backend responde `204`, se muestra éxito, se limpia la selección y se refresca la grilla con:

```txt
GET /api/Turno/socio
```

- Si el backend responde `409`, se muestra el mensaje real del backend.
- Si el turno está `Cancelado`, `Finalizado` o `Vencido`, no se llama al endpoint.

### Aclaraciones finales para implementación

#### 1. Contrato de `estadoTurno`

En el frontend actual, `TurnoHistorialItem.estadoTurno` está tipado como `string` en:

```txt
Frontend/src/types/turno.ts
```

Además, el backend configura `JsonStringEnumConverter` en `Program.cs`, por lo que los enums se serializan hacia el frontend como texto.

Por lo tanto, para esta implementación asumir que `estadoTurno` llega desde el backend como `string`.

Valores esperados:

```txt
EnCurso
Cancelado
Finalizado
Vencido
```

El enum backend actual es:

```csharp
public enum EstadoTurno
{
    EnCurso = 1,
    Cancelado = 2,
    Finalizado = 3,
    Vencido = 4,
}
```

Solo se puede cancelar un turno cuando:

```txt
estadoTurno === "EnCurso"
```

Cualquier otro estado debe bloquear la cancelación antes de abrir el modal de confirmación y antes de llamar al endpoint.

No cambiar el tipo `TurnoHistorialItem.estadoTurno` a `number`.

#### 2. Estado `Vencido`

El estado `Vencido` existe en el backend y debe tratarse como no cancelable.

Si el turno seleccionado tiene estado:

```txt
Vencido
```

el frontend no debe invocar:

```txt
PATCH /api/Turno/socio/cancelar/{idTurno}
```

Debe mostrar el mensaje:

```txt
No es posible cancelar un turno vencido.
```

#### 3. Mensajes por estado no cancelable

Cuando el usuario presione **Cancelar Turno** y el turno seleccionado no sea cancelable por estado, mostrar el mensaje en la misma pantalla `SocioInicioPage`, usando una alerta visual existente.

Usar el estilo:

```tsx
<p className="form-alert form-alert--error">...</p>
```

No abrir el modal de confirmación en estos casos.

Mensajes requeridos:

```txt
Cancelado -> El turno ya fue cancelado.
Finalizado -> No es posible cancelar un turno finalizado.
Vencido -> No es posible cancelar un turno vencido.
```

Si el estado no es reconocido:

```txt
No es posible cancelar el turno seleccionado por su estado actual.
```

En todos estos casos:

- no llamar al endpoint
- no limpiar la selección
- no refrescar la grilla
- limpiar cualquier mensaje de éxito anterior si existiera

#### 4. Ubicación de la validación de estado

Implementar la validación de estado en `SocioInicioPage.tsx` o en un helper pequeño si ayuda a mantener el archivo legible.

No colocar esta validación en `turnosService.ts`, porque `turnosService.ts` debe encargarse solo de llamadas HTTP.

Sugerencia de helper local o reutilizable:

```ts
function getCancelBlockedMessage(estadoTurno: string) {
  if (estadoTurno === "Cancelado") {
    return "El turno ya fue cancelado.";
  }

  if (estadoTurno === "Finalizado") {
    return "No es posible cancelar un turno finalizado.";
  }

  if (estadoTurno === "Vencido") {
    return "No es posible cancelar un turno vencido.";
  }

  if (estadoTurno !== "EnCurso") {
    return "No es posible cancelar el turno seleccionado por su estado actual.";
  }

  return null;
}
```

#### 5. Service para cancelar turno del Socio

En `Frontend/src/services/turnosService.ts` ya existen funciones para:

```txt
PATCH /Turno/asistente/cancelar/{idTurno}
GET /Turno/asistente/{idUsuarioSocio}
GET /Turno/socio
```

No modificar ni romper esas funciones.

Agregar una nueva función separada para el flujo del Socio:

```ts
async cancelarTurnoSocio(idTurno: number) {
  await apiClient.patch(`/Turno/socio/cancelar/${idTurno}`)
}
```

Esta función no debe recibir `idUsuarioSocio`.

El backend identifica al socio desde el JWT.

#### 6. Modal de confirmación

La implementación del asistente ya tiene un modal de confirmación dentro de:

```txt
Frontend/src/components/socios/GestionTurnosModal.tsx
```

Ese modal no está separado como componente compartido.

Para esta implementación:

- usar la misma estructura visual y clases CSS del modal de confirmación del asistente
- se puede crear un modal local dentro de `SocioInicioPage.tsx`
- extraer un componente compartido solo si reduce duplicación clara y no implica rediseñar el flujo

Mantener el texto de confirmación:

```txt
¿Confirma que desea cancelar este turno?
```

Mantener mensajes consistentes con el asistente:

```txt
Confirmar cancelacion
Confirma la accion
Cancelando turno...
Confirmar cancelacion
El turno fue cancelado correctamente.
```

#### 7. Manejo de éxito

Si el backend responde:

```txt
204 No Content
```

entonces:

- mostrar mensaje de éxito:

```txt
El turno fue cancelado correctamente.
```

- cerrar el modal de confirmación
- limpiar la selección actual
- refrescar la grilla llamando nuevamente a:

```txt
GET /api/Turno/socio
```

En frontend esa recarga debe usar la función existente:

```ts
turnosService.getTurnosSocioLogueado();
```

#### 8. Manejo de errores backend

El backend puede responder:

```txt
409 Conflict
```

con un body similar a:

```json
{
  "message": [
    "No se puede cancelar el turno con menos de 3 horas de antelación."
  ]
}
```

El frontend ya tiene `getCancelarTurnoErrorMessage()` en:

```txt
Frontend/src/utils/apiError.ts
```

Ese helper ya soporta `message` como string o array.

Por lo tanto, usar `getCancelarTurnoErrorMessage()` para mostrar errores del backend.

Reglas:

- `409 Conflict`: mostrar el mensaje real del backend y conservar la selección
- error genérico/red/500: mostrar fallback y conservar la selección
- `404 Not Found`: mostrar el mensaje real si está disponible, cerrar el modal de confirmación, limpiar la selección y refrescar la grilla con `GET /api/Turno/socio`

El objetivo del `404` es reconciliar la pantalla con el backend si el turno ya no existe.

#### 9. Textos y acentos

El frontend actual mezcla algunos textos con acentos y otros sin acentos.

Para esta implementación, mantener consistencia con el archivo donde se está trabajando.

En `SocioInicioPage.tsx` ya existe texto con acento:

```txt
No tenés turnos registrados.
Próximamente...
```

Por lo tanto, se pueden usar acentos en los mensajes nuevos de esa pantalla.

No cambiar textos existentes que no estén relacionados con esta implementación.
