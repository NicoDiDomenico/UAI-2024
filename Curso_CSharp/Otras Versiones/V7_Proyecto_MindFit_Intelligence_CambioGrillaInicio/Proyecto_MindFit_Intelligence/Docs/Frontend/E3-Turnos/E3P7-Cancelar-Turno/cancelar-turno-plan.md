# Etapa 3 Turnos - Parte 7 Cancelar Turno

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend

## 2. Tarea Principal

Implementar la funcionalidad real del botón Cancelar Turno en la pantalla de Gestionar Turno.

Actualmente el botón ya existe en la interfaz. Se debe completar el flujo para que, al presionarlo, el frontend permita confirmar la acción y luego cancele el turno seleccionado consumiendo el endpoint del backend:

PATCH /api/Turno/asistente/cancelar/{idTurno}

Policy backend: CancelarTurno

Comportamiento requerido:

1. Al hacer clic en Cancelar Turno, mostrar un mensaje/modal de confirmación con el texto:

- ¿Confirma que desea cancelar este turno?

2. Si el usuario cancela la confirmación, no ejecutar ninguna llamada al backend.
3. Si el usuario confirma:

- ejecutar PATCH /api/Turno/asistente/cancelar/{idTurno}
- usar el idTurno del turno seleccionado
- mostrar estado de loading mientras se procesa
- evitar doble submit mientras la operación está en curso

4. Si la cancelación es exitosa:

- mostrar un mensaje de éxito claro
- refrescar la información/listado del turno o actualizar el estado visual correspondiente
- dejar la pantalla consistente, sin datos obsoletos

5. Si la cancelación falla:

- mostrar el mensaje real devuelto por el backend cuando esté disponible
- si no hay mensaje específico, mostrar un mensaje genérico claro
- no romper la pantalla ni borrar información del turno

## 3. Contexto

- AGENTS.MD
- frontend-skill.md
- IMPLEMENTATION_LOG_gestion-turnos-plan.md
- TurnoController.cs

## 4. Reglas y Restricciones (Constraints / Guardrails)

- Trabajar únicamente en /Frontend.
- No modificar backend.
- No cambiar rutas, nombres de endpoints ni contratos del backend.
- Centralizar la llamada HTTP en un service existente o nuevo dentro de src/services.
- Usar Axios ya configurado en el proyecto.
- Respetar el interceptor existente para:

* Authorization: Bearer
* X-Gym-Id

- No hardcodear URLs absolutas.
- No agregar dependencias nuevas salvo que sea estrictamente necesario.
- Reutilizar componentes, modales, helpers y estilos existentes siempre que sea posible.
- Mantener consistencia visual con el frontend actual.
- Validar que exista idTurno antes de llamar al endpoint.
- El botón Cancelar Turno debe mostrarse/habilitarse solamente si el usuario tiene el permiso frontend correspondiente.
- El permiso frontend debe compararse contra los permisos guardados en localStorage, usando la misma lógica/helper de permisos que ya utiliza el frontend.
- Usar como código de permiso frontend: CANCELAR_TURNO
- No usar el nombre de la policy backend CancelarTurno como permiso frontend.
- La policy solo documenta la autorización del endpoint.
- Manejar estados de:

* confirmación
* loading
* éxito
* error

- Evitar duplicar lógica existente.
- Mantener los cambios simples, incrementales y mantenibles.
- Al finalizar, verificar TypeScript y corregir errores evidentes.
- después de cancelar con éxito, refresca la grilla de turnos del socio, limpia la selección actual y cierra el modal de confirmación dejando visible el modal de gestión.

### Validación de estado antes de cancelar

Antes de ejecutar:

PATCH /api/Turno/asistente/cancelar/{idTurno}

el frontend debe validar el estado del turno seleccionado.

Estados posibles:

- EnCurso = 1
- Cancelado = 2
- Finalizado = 3

Solo se permite cancelar turnos cuyo estado sea:

- EnCurso

Si el turno se encuentra en:

- Cancelado
- Finalizado

el frontend no debe invocar el endpoint.

En esos casos mostrar un mensaje apropiado al usuario:

- "El turno ya fue cancelado."
- "No es posible cancelar un turno finalizado."

La validación debe realizarse antes de mostrar la confirmación y antes de ejecutar la llamada HTTP.

El endpoint únicamente debe invocarse cuando el estado actual sea EnCurso.

## 5. Formato de Salida

Además de implementar el código solicitado, generar: IMPLEMENTATION_LOG_cancelar-turno-plan.md

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
