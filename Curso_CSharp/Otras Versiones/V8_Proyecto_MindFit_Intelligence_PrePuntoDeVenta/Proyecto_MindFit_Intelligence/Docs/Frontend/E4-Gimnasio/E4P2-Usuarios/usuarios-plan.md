# Etapa 4 Gestionar Gimnasio - Parte 2 Usuarios

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend.

## 2. Tarea Principal

Implementar en el frontend el módulo **Gestión de Usuarios Responsables**, accesible desde el botón de navegación **Usuarios** dentro del menú de **Gestionar Gimnasio**.

La ruta objetivo es:

`http://localhost:5173/gimnasio/usuarios`

Actualmente esa sección muestra un componente tipo **Próximamente**. Reemplazar ese placeholder por la pantalla real del módulo.

La pantalla debe permitir administrar usuarios responsables del gimnasio, excluyendo usuarios de tipo Socio.

Debe incluir:

- A la derecha una grilla/listado de usuarios responsables.
- A la izquierda una seccion que tiene:
  - Un formulario para crear un nuevo responsable o un formulario para consultar y editar un responsable existente.
  - Una sección para cambiar contraseña del responsable seleccionado.
  - Una acción para eliminar definitivamente un responsable.
  - Asignación visual de grupos mediante chips seleccionables.
  - Visualización de formularios y permisos mediante acordeones.

---

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

---

### Grilla de responsables

La pantalla debe mostrar una grilla con los usuarios responsables obtenidos desde:

`GET api/Usuario/grilla-responsable`

La grilla debe tener las siguientes columnas:

| Columna visible   | Campo del DTO              |
| ----------------- | -------------------------- |
| Nombre de Usuario | `Username`                 |
| Nombre y Apellido | `NombreCompleto`           |
| Email             | `Email`                    |
| Roles             | `List<string> NombreGrupo` |

Reglas:

- La columna **Roles** debe mostrar los grupos en forma de chips agrupados en una sola celda.

- Desde esta grilla, el usuario debe poder seleccionar un responsable mediante un radio button.

- Al seleccionar un responsable, se debe obtener su detalle completo usando:

  `GET api/Usuario/{idUsuario}`

  RES: `UsuarioDto?`

- Con esa respuesta se debe cargar el formulario de Usuario Responsable en **modo edición**.

---

### Formulario de Usuario Responsable

El módulo debe manejar un único formulario visual de **Usuario Responsable**, pero con dos modos de uso:

- **Modo creación**
- **Modo edición**

El formulario siempre corresponde a usuarios de tipo **Responsable**, por lo tanto:

- `TipoPersona` debe enviarse siempre como `"Responsable"`.
- No se debe usar `PersonaSocio`.
- No se deben mostrar ni enviar campos de socio.
- No se debe permitir asignar el grupo **SOCIO**.

---

## Modo creación

El modo creación se usa cuando no hay ningún responsable seleccionado en la grilla o cuando el usuario presiona la acción para iniciar un nuevo registro.

La acción principal del formulario debe ser **Crear**.

Endpoint:

`POST api/Usuario/responsable/register`

REQ: `UsuarioInsertDto`
RES: `UsuarioDto`
Policy backend: `CrearUsuarioResponsable`
Permiso frontend requerido: `CREAR_USUARIO_RESPONSABLE`

### Campos visibles del formulario en creación

El formulario de creación debe incluir estos campos:

#### Datos de acceso

- `Username`
- `Password`

#### Datos personales del responsable

- `Nombre`
- `Apellido`
- `Email`
- `Telefono`
- `Direccion`
- `Ciudad`
- `TipoDocumento`
- `NroDocumento`
- `Genero`
- `FechaNacimiento`

#### Grupos

- Chips seleccionables de grupos responsables.
- Cada chip representa un `GrupoDto` cuyo nombre de etiqueta se usara el atributo `Nombre` del DTO.
- Al seleccionar grupos, se debe armar la lista `IdGrupos`.

### Armado del UsuarioInsertDto

Al presionar **Crear**, el frontend debe armar el request de esta forma:

```ts
const payload: UsuarioInsertDto = {
  username: form.username,
  password: form.password,
  tipoPersona: "Responsable",
  personaResponsable: {
    nombre: form.nombre,
    apellido: form.apellido,
    email: form.email,
    telefono: form.telefono || null,
    direccion: form.direccion || null,
    ciudad: form.ciudad || null,
    tipoDocumento: form.tipoDocumento,
    nroDocumento: form.nroDocumento,
    genero: form.genero || null,
    fechaNacimiento: form.fechaNacimiento || null,
  },
  personaSocio: null,
  idGrupos: selectedGroupIds,
};
```

### Reglas específicas de creación

- `Password` solo debe aparecer en modo creación.
- `Password` es obligatorio para crear.
- En creación debe existir un campo `RepetirPassword` para confirmar la contraseña antes de enviar.
- `Password` y `RepetirPassword` deben coincidir antes de llamar al endpoint.
- `Username` es obligatorio.
- `Nombre`, `Apellido`, `Email`, `TipoDocumento` y `NroDocumento` son obligatorios.
- `IdGrupos` debe contener los IDs de los chips seleccionados.
- Debe seleccionarse al menos un grupo, validar que haya al menos un chip seleccionado antes de llamar al endpoint.
- Al crear correctamente:
  - Mostrar mensaje de éxito.
  - Recargar la grilla de responsables.
  - Limpiar el formulario o dejarlo listo para una nueva creación.
  - Limpiar la selección de responsable de la grilla.

---

## Modo edición

El modo edición se activa cuando el usuario selecciona un responsable desde la grilla.

Al seleccionar un responsable, se debe llamar a:

`GET api/Usuario/{idUsuario}`

RES: `UsuarioDto?`

La respuesta `UsuarioDto` debe usarse para cargar el formulario.

La acción principal del formulario debe ser **Guardar**.

Endpoint:

`PUT api/Usuario/responsable/{idUsuario}`

REQ: `UsuarioUpdateDto`
RES: `UsuarioDto?`
Policy backend: `EditarUsuarioResponsable`
Permiso frontend requerido: `EDITAR_USUARIO_RESPONSABLE`

### Campos visibles del formulario en edición

El formulario de edición debe incluir estos campos:

#### Datos de acceso

- `Username`

No debe mostrarse el campo `Password` en modo edición.
El cambio de contraseña se maneja en una sección separada.

#### Datos personales del responsable

Estos campos deben cargarse desde `UsuarioDto.PersonaResponsable`:

- `Nombre`
- `Apellido`
- `Email`
- `Telefono`
- `Direccion`
- `Ciudad`
- `TipoDocumento`
- `NroDocumento`
- `Genero`
- `FechaNacimiento`

#### Grupos

Los grupos deben cargarse desde `UsuarioDto.Grupos`.

- Cada grupo recibido en `UsuarioDto.Grupos` debe marcarse como chip seleccionado.
- Al guardar, se debe enviar en `IdGrupos` la lista actualizada de IDs seleccionados.

### Mapeo desde UsuarioDto hacia el formulario

Cuando el backend devuelve `UsuarioDto`, cargar el estado del formulario así:

```ts
const formState = {
  username: usuario.username,
  nombre: usuario.personaResponsable?.nombre ?? "",
  apellido: usuario.personaResponsable?.apellido ?? "",
  email: usuario.personaResponsable?.email ?? "",
  telefono: usuario.personaResponsable?.telefono ?? "",
  direccion: usuario.personaResponsable?.direccion ?? "",
  ciudad: usuario.personaResponsable?.ciudad ?? "",
  tipoDocumento: usuario.personaResponsable?.tipoDocumento ?? "",
  nroDocumento: usuario.personaResponsable?.nroDocumento ?? "",
  genero: usuario.personaResponsable?.genero ?? "",
  fechaNacimiento: usuario.personaResponsable?.fechaNacimiento
    ? usuario.personaResponsable.fechaNacimiento.slice(0, 10)
    : "",
};

const selectedGroupIds = usuario.grupos.map((grupo) => grupo.idGrupo);
```

### Armado del UsuarioUpdateDto

Al presionar **Guardar**, el frontend debe armar el request de esta forma:

```ts
const payload: UsuarioUpdateDto = {
  username: form.username,
  tipoPersona: "Responsable",
  personaResponsable: {
    nombre: form.nombre,
    apellido: form.apellido,
    email: form.email,
    telefono: form.telefono || null,
    direccion: form.direccion || null,
    ciudad: form.ciudad || null,
    tipoDocumento: form.tipoDocumento,
    nroDocumento: form.nroDocumento,
    genero: form.genero || null,
    fechaNacimiento: form.fechaNacimiento || null,
  },
  personaSocio: null,
  idGrupos: selectedGroupIds,
};
```

### Reglas específicas de edición

- `Password` no debe formar parte de `UsuarioUpdateDto`.
- `IdUsuario` no se envía en el body; se usa en la URL.
- `TipoPersona` debe enviarse como `"Responsable"`.
- `PersonaSocio` debe ir como `null` o no enviarse, según cómo se definan los tipos en el frontend.
- Los campos personales deben salir de `PersonaResponsable`.
- Los grupos seleccionados deben transformarse a `List<int>` usando `IdGrupo`.
- Al guardar correctamente:
  - Mostrar mensaje de éxito.
  - Recargar la grilla de responsables.
  - Mantener seleccionado el responsable editado o volver a cargar su detalle actualizado.

---

### Campo Género

El campo `Genero` debe representarse como un select.

El backend tiene configurado `JsonStringEnumConverter`, por lo que los enums se serializan y deserializan como texto.

Por este motivo, el frontend debe manejar y enviar `Genero` como string, respetando exactamente los nombres definidos en backend.

Opciones:

- `Masculino`
- `Femenino`
- `Otro`
- `NoEspecifica`

El valor debe enviarse como string o `null`.

No enviar:

- `1`
- `2`
- `3`
- `4`

Enviar:

```ts
genero: form.genero || null;
```

Ejemplo de opciones para el select:

```ts
const generoOptions = [
  { value: "Masculino", label: "Masculino" },
  { value: "Femenino", label: "Femenino" },
  { value: "Otro", label: "Otro" },
  { value: "NoEspecifica", label: "No especifica" },
];
```

Al cargar desde `UsuarioDto`, usar directamente el string recibido:

```ts
genero: usuario.personaResponsable?.genero ?? "";
```

---

### Campo FechaNacimiento

El campo `FechaNacimiento` debe representarse como input de tipo fecha.

Reglas:

- En el formulario se debe manejar en formato `yyyy-MM-dd`.
- Al cargar desde `UsuarioDto`, convertir el `DateTime?` recibido a `yyyy-MM-dd` para que el input lo pueda mostrar.
- Al enviar al backend, mandar el valor seleccionado o `null` si está vacío.

---

### Diferencias obligatorias entre creación y edición

| Aspecto          | Creación                                | Edición                                         |
| ---------------- | --------------------------------------- | ----------------------------------------------- |
| Endpoint         | `POST api/Usuario/responsable/register` | `PUT api/Usuario/responsable/{idUsuario}`       |
| DTO              | `UsuarioInsertDto`                      | `UsuarioUpdateDto`                              |
| Acción principal | Crear                                   | Guardar                                         |
| Password         | Se muestra y se envía                   | No se muestra y no se envía                     |
| IdUsuario        | No se usa                               | Se usa solo en la URL                           |
| Datos personales | Se cargan desde inputs vacíos           | Se cargan desde `UsuarioDto.PersonaResponsable` |
| Grupos           | Se seleccionan desde cero               | Se cargan desde `UsuarioDto.Grupos`             |
| TipoPersona      | `"Responsable"`                         | `"Responsable"`                                 |
| PersonaSocio     | `null` / no usado                       | `null` / no usado                               |

---

### Cambio de contraseña

Agregar una sección dentro del formulario para cambiar la contraseña del responsable seleccionado de la grilla de responsables. NO confundir con la contraseña del usuario logueado.

Debe tener dos campos:

- Contraseña actual.
- Nueva contraseña.
- Repetir nueva contraseña.

Al hacer clic en **Cambiar Contraseña**, se debe llamar a:

`POST api/Auth/responsables/{idUsuario:int}/change-password`

REQ: `ChangePasswordRequestDto`

Permiso frontend requerido para mostrar/habilitar la acción:

`CAMBIAR_CONTRASENA_RESPONSABLE`

Comportamiento esperado:

- La sección solo debe estar disponible en modo edición, cuando haya un responsable seleccionado.
- Validar que ambos campos estén completos.
- Validar que `Nueva contraseña` y `Repetir nueva contraseña` coincidan antes de llamar al endpoint.
- Mostrar loading mientras se ejecuta la petición.
- Mostrar mensaje de éxito o error real devuelto por backend.
- Limpiar los campos de contraseña si la operación fue exitosa.

---

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

---

### Chips de grupos

Los grupos obtenidos desde:

`GET api/Grupo`

deben mostrarse como chips seleccionables en la parte superior o en una sección clara del formulario.

Ejemplo visual esperado:

`[ Grupo: Admin ] [ Grupo: Instructor ]`

Reglas:

- No mostrar el grupo **SOCIO**.
- Permitir seleccionar uno o varios grupos.
- La selección de grupos debe impactar visualmente en los permisos mostrados dentro de los acordeones.
- Para enviar al backend, transformar los grupos seleccionados a `List<int>` usando `IdGrupo`.
- No inventar estructura de DTO: revisar los tipos reales antes de mapear IDs de grupos en el request.

---

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
- No permitir editar permisos manualmente desde esta pantalla salvo que el backend/DTO lo soporte explícitamente.
- La visualización de permisos debe depender de los grupos seleccionados.

---

### Cálculo de permisos activos

Para calcular si un permiso está activo:

1. Tomar los grupos seleccionados en los chips.
2. Leer los permisos de cada grupo seleccionado desde `GrupoDto.Permisos`.
3. Comparar esos permisos contra los permisos del formulario.
4. Si el permiso del formulario existe dentro de algún grupo seleccionado, mostrarlo como activo.
5. Si no existe en ningún grupo seleccionado, mostrarlo grisado.

La leyenda/tooltip de cada permiso debe indicar a qué grupos seleccionados pertenece ese permiso.

Ejemplo:

`Este permiso pertenece a: Admin, Instructor`

Si no pertenece a ningún grupo seleccionado:

`Este permiso no está incluido en los grupos seleccionados`

---

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

---

### Permisos frontend

Los botones de acción deben mostrarse o habilitarse según los permisos guardados en `localStorage`, siguiendo la misma lógica/helper de permisos existente en el proyecto.

Mapeo requerido:

- Botón **Crear**: `CREAR_USUARIO_RESPONSABLE`
- Botón **Guardar**: `EDITAR_USUARIO_RESPONSABLE`
- Botón **Cambiar Contraseña**: `CAMBIAR_CONTRASENA_RESPONSABLE`
- Botón **Eliminar**: `ELIMINAR_USUARIO_RESPONSABLE_DEFINITIVAMENTE`

No usar los nombres de policies backend como permisos frontend.
Las policies documentan autorización del backend, pero el frontend debe comparar contra los códigos de permisos.

---

### Integración frontend/backend

Crear o extender los servicios necesarios dentro de:

`src/services`

Crear o extender los tipos necesarios dentro de:

`src/types`

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

---

### Manejo de enums

El backend tiene configurado `JsonStringEnumConverter`, por lo que los enums se serializan y deserializan como texto.

Por este motivo, el frontend debe manejar y enviar los enums como strings, respetando exactamente los nombres definidos en backend.

Ejemplo para `Genero`:

- Correcto: `"Masculino"`
- Correcto: `"Femenino"`
- Correcto: `"Otro"`
- Correcto: `"NoEspecifica"`
- Incorrecto: `1`
- Incorrecto: `2`
- Incorrecto: `3`
- Incorrecto: `4`

---

### Validaciones mínimas

Implementar validaciones frontend básicas antes de llamar al backend:

- Campos obligatorios del usuario responsable.
- Formato básico de email si aplica.
- Selección obligatoria de al menos un grupo.
- Contraseña actual y nueva contraseña requeridas para cambio de contraseña.
- No permitir guardar o eliminar si no hay usuario seleccionado.
- No permitir cambiar contraseña si no hay usuario seleccionado.
- No permitir doble submit mientras una operación está en curso.
- No permitir asignar el grupo **SOCIO**.
- No enviar datos de `PersonaSocio`.

Los mensajes de error deben priorizar el mensaje real devuelto por el backend.

---

## 3. Contexto

Leer antes de implementar:

- AGENTS.md
- frontend-skill.md
- usuarios-plan.md
- Implementación actual del menú **Gestionar Gimnasio**
- Implementación actual de la ruta `/gimnasio/usuarios`
- Implementación actual de helpers de permisos
- Implementación actual de Axios/interceptors
- Implementación actual de manejo de errores de backend

También revisar, si existen en el proyecto:

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

---

## 4. Reglas y Restricciones (Constraints / Guardrails)

- Trabajar solo en el frontend.
- No modificar backend.
- No cambiar endpoints.
- No cambiar nombres de DTOs del backend.
- No hardcodear URLs completas.
- No inventar campos que no existan en los DTOs.
- No usar `window.confirm`; usar modal visual.
- No mostrar ni enviar campos de socio.
- No permitir seleccionar el grupo **SOCIO**.
- No enviar enums como número; enviarlos como string.
- Mantener consistencia visual con el resto de pantallas ya implementadas.
- En modo creación, mostrar `Password` y `RepetirPassword`.
- En modo edición de datos generales, no mostrar `Password` ni `RepetirPassword`.
- En la sección separada de cambio de contraseña, mostrar `Contraseña actual`, `Nueva contraseña` y `Repetir nueva contraseña`.
- Los acordeones deben mostrar siempre todos los formularios y todos los permisos devueltos por GET api/Formulario. La selección de grupos solo determina si cada permiso se ve activo o inactivo; no debe ocultar formularios ni permisos.

Permisos para botones de acción:

- En modo alta, mostrar **Crear** si tiene `CREAR_USUARIO_RESPONSABLE`.
- En modo edición, mostrar **Guardar** si tiene `EDITAR_USUARIO_RESPONSABLE`.
- Botón **Cambiar Contraseña**: `CAMBIAR_CONTRASENA_RESPONSABLE`.
- Botón **Eliminar**: `ELIMINAR_USUARIO_RESPONSABLE_DEFINITIVAMENTE`.

Los permisos frontend deben compararse contra los permisos guardados en `localStorage`, usando la lógica/helper existente en el proyecto.

No usar las policies backend como códigos de permiso frontend.

---

## 5. Formato de Salida

Además de implementar el código solicitado, generar:

`IMPLEMENTATION_LOG_usuarios-plan.md`

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
