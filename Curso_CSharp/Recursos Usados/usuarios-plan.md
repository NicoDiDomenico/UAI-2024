# Etapa 4 Turnos - Parte 2 Usuarios

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend

## 2. Tarea Principal

Implementar en el frontend el módulo **Gestión de Usuarios Responsables**, accesible desde el botón de navegación **Usuario/Usuarios** dentro del menú de **Gestionar Gimnasio** correspondiente a la ruta "http://localhost:5173/gimnasio/usuarios", reemplazar el componente actual que dice "Proximamente" por este nuevo.

La pantalla debe permitir administrar usuarios responsables del gimnasio, excluyendo usuarios de tipo Socio.

Debe incluir:

- Una grilla/listado de usuarios responsables.
- Un formulario para crear un nuevo responsable.
- Un formulario para consultar y editar un responsable existente.
- Una sección para cambiar contraseña.
- Una acción para eliminar definitivamente un responsable.
- Asignación visual de grupos.
- Visualización de formularios y permisos mediante acordeones.

### Flujo general de la pantalla

Al ingresar al módulo de usuarios, el frontend debe cargar internamente:

1. La grilla de responsables usando:

   `GET api/Usuario/grilla-responsable`

   RES: `List<ResponsableGridDto>`

2. Los grupos disponibles usando:

   `GET api/Grupo`

   RES: `IEnumerable<GrupoDto>`

   Estos grupos deben mostrarse como chips seleccionables, pero deben excluir el grupo **SOCIO**.
   El módulo es solo para usuarios responsables.

3. Los formularios y permisos usando:

   `GET api/Formulario`

   RES: `IEnumerable<FormularioDto>`

   Esta información se debe usar para armar acordeones por formulario, mostrando sus permisos y marcando visualmente cuáles quedan activos según los grupos seleccionados.

### Grilla de responsables

- La pantalla debe mostrar una grilla, con los usuarios responsables obtenidos desde `GET api/Usuario/grilla-responsable`, con las siguientes columnas:
  Nombre de Usuario | Nombre y Apellido | Email | Roles
  Estas columnas se podran encontrar en cada `ResponsableGridDto` de la lista que viene en la respuesta de `GET api/Usuario/grilla-responsable`:
  Nombre de Usuario --> Username
  Nombre y Apellido --> NombreCompleto
  Email --> Email
  Roles (En forma de Chips agrupados en una sola celda)--> List<string> NombreGrupo
- Desde esta grilla, el usuario debe poder seleccionar un responsable a traves de un radio button.Al seleccionar un responsable, se debe obtener su detalle completo usando `GET api/Usuario/{idUsuario}` con RES: `UsuarioDto?` y asi con esa respuesta se debe cargar el formulario de Usuario Responsable en modo edición.

### Alta de usuario responsable

Debe existir una acción **Crear** para registrar un nuevo usuario responsable.

El alta debe usar:

`POST api/Usuario/responsable/register`

REQ: `UsuarioInsertDto`
RES: `UsuarioDto`
Policy backend: `CrearUsuarioResponsable`
Permiso frontend requerido: `CREAR_USUARIO_RESPONSABLE`

El formulario debe incluir los campos necesarios de datos personales del responsable según el DTO de la REQ: `UsuarioInsertDto`.

También debe permitir asignar grupos mediante chips seleccionables.

Al crear correctamente:

- Mostrar mensaje de éxito.
- Limpiar o reiniciar el formulario según el patrón actual del proyecto.
- Recargar la grilla de responsables.
- Dejar visible el nuevo responsable creado en la lista.

### Edición de usuario responsable

Cuando se seleccione un usuario existente desde la grilla, el formulario debe pasar a modo edición.

La acción **Guardar** debe actualizar el usuario usando:

`PUT api/Usuario/responsable/{idUsuario}`

REQ: `UsuarioUpdateDto`
RES: `UsuarioDto?`
Policy backend: `EditarUsuarioResponsable`
Permiso frontend requerido: `EDITAR_USUARIO_RESPONSABLE`

El formulario debe incluir los campos necesarios de datos personales del responsable según el DTO de la REQ: `UsuarioUpdateDto`.

También debe permitir cargar los grupos en los chips seleccionables.

Al guardar correctamente:

- Mostrar mensaje de éxito.
- Actualizar la grilla de usuarios responsables.
- Mantener seleccionado o recargar el usuario actualizado de forma consistente con el resto del frontend.

### Cambio de contraseña

Agregar una sección dentro del formulario para cambiar la contraseña del usuario selecionado de la grilla de responsables.

Debe tener dos campos:

- Contraseña actual.
- Nueva contraseña.

Al hacer clic en **Cambiar Contraseña**, se debe llamar a:

`POST api/Auth/responsables/{idUsuario:int}/change-password `

REQ: `ChangePasswordRequestDto`

Permiso frontend requerido para mostrar/habilitar la acción:

`CAMBIAR_CONTRASENA_RESPONSABLE`

Comportamiento esperado:

- Validar que ambos campos estén completos.
- Mostrar loading mientras se ejecuta la petición.
- Mostrar mensaje de éxito o error real devuelto por backend.
- Limpiar los campos de contraseña si la operación fue exitosa.

### Eliminación definitiva de responsable

Agregar acción **Eliminar** para borrar definitivamente un usuario responsable seleccionado.

Debe usar:

`DELETE api/Usuario/responsable/{idUsuario}`

RES: `UsuarioDto`
Policy backend: `EliminarUsuarioResponsableDefinitivamente`
Permiso frontend requerido: `ELIMINAR_USUARIO_RESPONSABLE_DEFINITIVAMENTE`

Comportamiento requerido:

1. El botón debe estar disponible solo cuando haya un responsable seleccionado.
2. No usar `window.confirm`.
3. Mostrar un modal visual de confirmación.
4. El modal debe indicar claramente qué responsable se va a eliminar.
5. Al confirmar:
   - Ejecutar el DELETE.
   - Mostrar loading dentro del modal.

6. Si la eliminación es exitosa:
   - Mostrar mensaje de éxito.
   - Cerrar o dejar resuelto el modal según el patrón existente del proyecto.
   - Quitar el responsable eliminado de la grilla.
   - Limpiar el formulario seleccionado.

7. Si falla:
   - Mostrar el mensaje real devuelto por el backend cuando esté disponible.

### Chips de grupos

Los grupos obtenidos desde:

`GET api/Grupo`

deben mostrarse como chips seleccionables en la parte superior o en una sección clara del formulario.

Ejemplo visual esperado:

`[ Grupo: Admin ] [ Grupo: Instructor ]`

Reglas:

- No mostrar el grupo **SOCIO**.
- Permitir seleccionar uno o varios grupos si el DTO/backend lo permite.
- La selección de grupos debe impactar visualmente en los permisos mostrados dentro de los acordeones.
- No inventar estructura de DTO: revisar los tipos reales antes de mapear IDs de grupos en el request.

### Acordeones de formularios y permisos

Con la respuesta de:

`GET api/Formulario`

se deben construir acordeones por formulario.

Ejemplo esperado:

- 📁 Formulario: Rutinas `[3 activos]`
  - ✅ Ver Rutinas
  - ✅ Crear Rutina
  - ✅ Asignar Ejercicios
  - ⚪ Eliminar Historial

- 📁 Formulario: Pagos `[1 activo]`
  - ✅ Consultar Saldo
  - ⚪ Registrar Pago
  - ⚪ Anular Factura

- 📁 Formulario: Configuración `[0 activos]`

Reglas visuales:

- Los permisos activos deben verse resaltados en verde, azul o el color de acento ya usado por el proyecto.
- Los permisos no activos deben verse grisados/desactivados.
- Cada acordeón debe mostrar la cantidad de permisos activos para ese formulario.
- Si un formulario no tiene permisos activos, puede aparecer cerrado por defecto.
- Al apoyar el mouse sobre un permiso, mostrar una leyenda/tooltip indicando a qué grupo o grupos pertenece ese permiso.
- No permitir editar permisos manualmente desde esta pantalla salvo que el backend/DTO lo soporte explícitamente. La visualización de permisos debe depender de los grupos seleccionados.

### Estados de pantalla

Implementar estados claros para:

- Carga inicial de grilla.
- Carga de grupos.
- Carga de formularios/permisos.
- Carga del detalle del responsable seleccionado.
- Creación.
- Edición.
- Cambio de contraseña.
- Eliminación.
- Errores de backend.
- Estados vacíos, por ejemplo cuando no haya responsables cargados.

### Permisos frontend

Los botones de acción deben mostrarse o habilitarse según los permisos guardados en `localStorage`, siguiendo la misma lógica/helper de permisos existente en el proyecto.

Mapeo requerido:

- Botón **Crear**: `CREAR_USUARIO_RESPONSABLE`
- Botón **Guardar**: `EDITAR_USUARIO_RESPONSABLE`
- Botón **Cambiar Contraseña**: `CAMBIAR_CONTRASENA_RESPONSABLE`
- Botón **Eliminar**: `ELIMINAR_USUARIO_RESPONSABLE_DEFINITIVAMENTE`

No usar los nombres de policies backend como permisos frontend.
Las policies documentan autorización del backend, pero el frontend debe comparar contra los códigos de permisos.

### Integración frontend/backend

Crear o extender los servicios necesarios dentro de `src/services`.

Crear o extender los tipos necesarios dentro de `src/types`.

Endpoints involucrados:

- `POST api/Usuario/responsable/register`
- `GET api/Usuario/grilla-responsable`
- `GET api/Usuario/{idUsuario}`
- `PUT api/Usuario/responsable/{idUsuario}`
- `DELETE api/Usuario/responsable/{idUsuario}`
- `POST api/Auth/responsables/{idUsuario:int}/change-password`
- `GET api/Grupo`
- `GET api/Formulario`

Usar la configuración Axios existente, incluyendo:

- `Authorization: Bearer`
- `X-Gym-Id`
- base URL desde variable de entorno

No hardcodear URLs completas.

### Validaciones mínimas

Implementar validaciones frontend básicas antes de llamar al backend:

- Campos obligatorios del usuario responsable.
- Formato básico de email si aplica.
- Selección de al menos un grupo si el backend lo requiere.
- Contraseña actual y nueva contraseña requeridas para cambio de contraseña.
- No permitir guardar o eliminar si no hay usuario seleccionado.
- No permitir doble submit mientras una operación está en curso.

Los mensajes de error deben priorizar el mensaje real devuelto por el backend.

## 3. Contexto

- AGENTS.md
- frontend-skill.md
- UsuarioController.cs
- ResponsableGridDto.cs
- GrupoController.cs
- GrupoDto.cs
- PermisoDto.cs
- FormularioController.cs
- FormularioDto.cs
- UsuarioDto.cs
- PersonaResponsableDto.cs
- Genero.cs
- UsuarioInsertDto.cs
- PersonaResponsableInsertDto.cs
- UsuarioUpdateDto.cs
- PersonaResponsableUpdateDto.cs
- AuthController.cs
- ChangePasswordRequestDto.cs
- Program.cs

## 4. Reglas y Restricciones (Constraints / Guardrails)

- Permisos para botones de accion:

* En modo alta, mostrar Crear si tiene CREAR_USUARIO_RESPONSABLE.
* En modo edición, mostrar Guardar si tiene EDITAR_USUARIO_RESPONSABLE.
* Botón Cambiar Contraseña: CAMBIAR_CONTRASENA_RESPONSABLE
* Botón Eliminar: ELIMINAR_USUARIO_RESPONSABLE_DEFINITIVAMENTE

## 5. Formato de Salida

Además de implementar el código solicitado, generar: IMPLEMENTATION_LOG_usuarios-plan.md

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
