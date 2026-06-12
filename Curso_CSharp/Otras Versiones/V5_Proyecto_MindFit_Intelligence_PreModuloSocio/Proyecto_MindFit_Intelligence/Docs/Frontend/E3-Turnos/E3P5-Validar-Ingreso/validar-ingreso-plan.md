# Etapa 3 Turnos - Parte 5 Validar Ingreso de Socio

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend

## 2. Tarea Principal

Implementar en el frontend el flujo **Validar Ingreso del Socio**.

El flujo debe estar disponible desde un botón superior visible en el layout principal del sistema, para poder validar el ingreso de un socio desde cualquier pantalla privada.

Usar el endpoint:

`POST /api/Turno/validar-ingreso`

REQ: `ValidarIngresoDto`  
Policy backend: `ValidarIngreso`

El frontend debe leer el DTO real desde el backend/Swagger antes de implementar y no inventar campos.

Agregar un botón superior de **Validar Ingreso** en el layout privado del sistema.

Al hacer clic:

1. Abrir un modal visual consistente con el estilo actual del frontend.
2. El modal debe solicitar el **DNI del socio**.
3. El DNI ingresado debe enviarse en el body del endpoint usando `ValidarIngresoDto`.
4. Mientras se ejecuta la validación, mostrar estado de carga dentro del modal.
5. Si el backend responde exitosamente, mostrar el mensaje de éxito devuelto por el backend o un mensaje claro de ingreso validado.
6. Si el backend responde error, mostrar el mensaje real devuelto por el backend siempre que exista.
7. No usar `window.alert` ni `window.confirm`.

El botón **Validar Ingreso** solo debe mostrarse si el usuario tiene el permiso correspondiente:

`VALIDAR_INGRESO`

Si el código exacto del permiso difiere en el backend o en los formularios, usar el valor real existente en el proyecto.

Agregar el botón **Validar Ingreso** dentro del header principal existente del sistema.

El header actual tiene una estructura similar a:

`<header class="workspace-header">...</header>`

El botón debe ubicarse visualmente en el centro del header, entre:

- la marca/logo de MindFit ubicada a la izquierda
- la información de sesión y botón `Cerrar sesión` ubicada a la derecha

No crear una pantalla nueva ni agregar este botón dentro de páginas específicas.

La funcionalidad debe estar disponible en todas las pantallas que usen este mismo header/layout privado.

Si el proyecto tiene un componente de layout como `WorkspaceLayout`, `PrivateLayout`, `DashboardLayout`, `AppLayout` o similar, implementar ahí el botón y el modal.

El modal debe incluir:

- título: `Validar ingreso`
- campo: `DNI del socio`
- botón: `Cancelar`
- botón principal: `Confirmar`
- estado de loading al confirmar
- área para mostrar mensaje de éxito o error

Validaciones frontend mínimas:

- El DNI es obligatorio.
- No permitir confirmar si el campo está vacío.
- Mantener el formulario simple.

## 3. Contexto

- AGENTS.MD
- frontend-skill.md
- TurnoController.cs
- ValidarIngresoDto.cs

## 4. Reglas y Restricciones (Constraints / Guardrails)

### Boton Accion Validar ingreso

Permiso necesario: VALIDAR_INGRESO

### Ubicación del botón

Buscar en el frontend el componente que renderiza el header principal privado del sistema.

Referencia HTML actual:

`<header class="workspace-header">...</header>`

Agregar dentro de ese header un botón central:

`Validar Ingreso`

El botón debe quedar visualmente entre:

- la marca/logo de MindFit ubicada a la izquierda
- la información de sesión y botón `Cerrar sesión` ubicada a la derecha

No implementar esta acción dentro de páginas específicas.

La acción debe estar disponible automáticamente en todas las pantallas que usen ese header.

### Reglas funcionales

- No crear una pantalla nueva.
- No agregar el botón manualmente en cada página.
- Implementar el botón y el modal en el layout/header privado existente.
- Buscar el componente por la clase `workspace-header` o por nombres similares como `WorkspaceLayout`, `PrivateLayout`, `DashboardLayout` o `AppLayout`.
- Reutilizar servicios, hooks, helpers de permisos y cliente Axios existentes.
- Mostrar mensajes reales del backend cuando existan.
- No usar `window.alert`.
- No usar `window.confirm`.

### Aclaraciones finales para implementación

1. Permiso frontend

El botón **Validar Ingreso** debe mostrarse únicamente si el usuario tiene el permiso:

`VALIDAR_INGRESO`

Este permiso debe compararse contra los permisos guardados en `localStorage`, usando la misma lógica/helper de permisos que ya utiliza el frontend para mostrar u ocultar otras acciones.

No usar la policy backend `ValidarIngreso` como código de permiso frontend.  
La policy solo documenta la autorización del endpoint.

2. Comportamiento del modal después del éxito

Cuando la validación sea exitosa:

- mantener el modal abierto
- mostrar el mensaje de éxito
- limpiar el campo DNI
- dejar el formulario listo para validar otro socio

No cerrar automáticamente el modal.

3. Regla de entrada del DNI

El body real es:

`{ dniSocio: string }`

Reglas frontend:

- aplicar `trim()` antes de enviar
- el campo es obligatorio
- permitir solo dígitos en el input
- no enviar si queda vacío luego del `trim()`

4. Mensajes de error del backend

El frontend debe soportar errores con estas formas:

- `{ message: "texto de error" }`
- `{ message: ["error 1", "error 2"] }`
- `["error 1", "error 2"]`
- `"texto de error"`

Si ya existe un helper global para parsear errores, se puede extender sin romper otros flujos.

Si no conviene tocar el helper global, implementar un parser específico para este flujo y documentarlo en el log.

## 5. Formato de Salida

Además de implementar el código solicitado, generar: IMPLEMENTATION_LOG_validar-ingreso-plan.md

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
