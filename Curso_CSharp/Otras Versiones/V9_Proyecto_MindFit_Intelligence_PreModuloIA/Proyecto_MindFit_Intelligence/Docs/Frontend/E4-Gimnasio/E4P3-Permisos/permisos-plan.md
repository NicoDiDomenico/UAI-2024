# Etapa 4 Gestionar Gimnasio - Parte 3 - Permisos

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend.

## 2. Tarea Principal

Implementar en el frontend el módulo **Gestión de Roles y Permisos**, accesible desde el botón de navegación **Permisos** dentro del menú de **Gestionar Gimnasio**.

La ruta objetivo es:

`/gimnasio/permisos`

Actualmente esa ruta muestra un componente tipo **Próximamente**. Se debe reemplazar ese placeholder por una pantalla real de administración de grupos/roles y sus permisos.

El objetivo del módulo es permitir que un usuario autorizado pueda:

- Ver los grupos existentes.
- Crear un nuevo grupo.
- Seleccionar un grupo existente para editarlo.
- Modificar nombre, descripción y permisos asociados.
- Eliminar un grupo existente mediante modal de confirmación.
- Asociar permisos a cada grupo usando checkboxes.

---

### Pantalla esperada

La pantalla debe estar organizada como un módulo operativo, consistente con el resto del frontend.

#### 1. Encabezado del módulo

Mostrar un encabezado con:

- Título: `Roles y permisos`
- Texto breve indicando que desde esta sección se administran los grupos internos y sus permisos.
- Acción principal para iniciar el alta de un nuevo grupo, visible solo si el usuario tiene permiso frontend `CREAR_GRUPO`.

#### 2. Grilla/Listado de grupos

Cargar los grupos desde:

`GET api/Grupo`

Mostrar los grupos devueltos por el backend en una grilla/listado.

Mostrar como mínimo:

- Nombre
- Descripción
- Cantidad de permisos asociados

Permitir seleccionar un grupo.

Al seleccionar un grupo:

- Cargar sus datos en el formulario.
- Pasar el formulario a modo edición.
- Marcar automáticamente los permisos que ya tenga asociados.

#### 3. Formulario de alta/edición

El mismo formulario puede reutilizarse para crear y editar.

Campos del formulario:

- `Nombre`
- `Descripcion`
- Lista de permisos seleccionables mediante checkboxes.

En modo alta:

- El formulario debe iniciar vacío.
- Los permisos deben iniciar desmarcados.
- Al confirmar, llamar a `POST api/Grupo`.
- Si la creación es exitosa, agregar/actualizar el registro en la grilla.
- El grupo creado debe quedar seleccionado en modo edición.

En modo edición:

- El formulario debe completarse con los datos del `GrupoDto` seleccionado.
- Los permisos asociados al grupo deben aparecer marcados.
- Al confirmar, llamar a `PUT api/Grupo/{id}`.
- Si la edición es exitosa, actualizar la grilla.
- El grupo editado debe continuar seleccionado.

#### 4. Sección de permisos

Cargar la lista completa de permisos desde:

`GET api/Permiso`

Renderizar cada permiso como un checkbox.

Cada checkbox debe mostrar:

- `Codigo`
- `Descripcion`, si existe.

El valor enviado al backend debe ser una lista de IDs de permisos:

```ts
IdPermisos: number[]
```

Al crear o editar un grupo, construir `IdPermisos` usando los `IdPermiso` de los checkboxes seleccionados.

#### 5. Eliminación de grupo

En modo edición, mostrar botón **Eliminar** solo si el usuario tiene permiso frontend `ELIMINAR_GRUPO`.

No usar `window.confirm`.

Al hacer clic en **Eliminar**:

- Abrir un modal visual de confirmación.
- Indicar claramente qué grupo se va a eliminar.
- Mostrar botones `Cancelar` y `Confirmar eliminación`.

Al confirmar:

- Llamar a `DELETE api/Grupo/{id}`.
- Mostrar loading mientras se ejecuta la operación.

Si la eliminación es exitosa:

- Quitar el grupo de la grilla.
- Limpiar el formulario.
- Volver al modo alta o dejar la pantalla sin selección, según encaje mejor con la estructura existente.

---

### Flujo de endpoints

#### 1. Obtener permisos disponibles

`GET api/Permiso`

RES: `IEnumerable<PermisoDto>`

```csharp
public class PermisoDto
{
    public int IdPermiso { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
}
```

Uso en frontend:

- Cargar la lista de permisos al ingresar al módulo.
- Renderizar los permisos como checkboxes.
- Usar `IdPermiso` para construir `IdPermisos` al crear o editar un grupo.

---

#### 2. Obtener grupos existentes

`GET api/Grupo`

RES: `IEnumerable<GrupoDto>`

```csharp
public class GrupoDto
{
    public int IdGrupo { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public List<PermisoDto> Permisos { get; set; } = new();
}
```

Uso en frontend:

- Cargar la grilla de grupos.
- Permitir seleccionar un grupo para edición.
- Usar `IdGrupo` para modificar o eliminar.
- Usar `Permisos` para marcar los checkboxes correspondientes en modo edición.

---

#### 3. Crear grupo

`POST api/Grupo`

REQ: `GrupoInsertDto`
RES: `GrupoDto`
Policy backend: `CrearGrupo`

```csharp
public class GrupoInsertDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public List<int> IdPermisos { get; set; } = new();
}
```

Uso en frontend:

- Se ejecuta desde el formulario en modo alta.
- El usuario completa nombre, descripción y selecciona permisos.
- Enviar únicamente los campos definidos por `GrupoInsertDto`.
- Si la creación es exitosa, actualizar la grilla y seleccionar el grupo creado.

---

#### 4. Editar grupo

`PUT api/Grupo/{id}`

REQ: `GrupoUpdateDto`
RES: `GrupoDto`
Policy backend: `EditarGrupo`

```csharp
public class GrupoUpdateDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public List<int> IdPermisos { get; set; } = new();
}
```

Uso en frontend:

- Se ejecuta desde el formulario en modo edición.
- El `{id}` debe ser el `IdGrupo` del grupo seleccionado.
- El usuario puede modificar nombre, descripción y permisos.
- Enviar únicamente los campos definidos por `GrupoUpdateDto`.
- Si la edición es exitosa, actualizar el registro en la grilla.

---

#### 5. Eliminar grupo

`DELETE api/Grupo/{id}`

RES: `GrupoDto`
Policy backend: `EliminarGrupo`

Uso en frontend:

- Se ejecuta desde el botón **Eliminar** del formulario en modo edición.
- Antes de llamar al endpoint, mostrar modal de confirmación.
- El `{id}` debe ser el `IdGrupo` del grupo seleccionado.
- Si la eliminación es exitosa, quitar el grupo de la grilla y limpiar el formulario.

---

### Permisos frontend para acciones

Los botones de acción deben mostrarse u ocultarse usando los permisos guardados en `localStorage` y la lógica/helper existente del proyecto.

No usar las policies backend como códigos de permiso frontend.

Mapeo requerido:

- Botón **Crear**: `CREAR_GRUPO`
- Botón **Guardar**: `EDITAR_GRUPO`
- Botón **Eliminar**: `ELIMINAR_GRUPO`

Comportamiento esperado:

- Si el usuario no tiene `CREAR_GRUPO`, no mostrar la acción de alta.
- Si el usuario no tiene `EDITAR_GRUPO`, no mostrar el botón de guardar cambios en modo edición.
- Si el usuario no tiene `ELIMINAR_GRUPO`, no mostrar el botón eliminar.
- Aunque un botón no se muestre, mantener las llamadas protegidas por backend como fuente final de autorización.

---

### Estados y validaciones mínimas

Implementar manejo claro de:

- Carga inicial de grupos.
- Carga inicial de permisos.
- Error al cargar grupos o permisos.
- Loading al crear.
- Loading al editar.
- Loading al eliminar.
- Mensajes de éxito.
- Mensajes de error del backend cuando estén disponibles.

Validaciones mínimas del formulario:

- `Nombre` obligatorio.
- `Descripcion` obligatoria si el backend la requiere como string no vacío.
- `IdPermisos` puede enviarse como lista vacía solo si el backend lo permite; si no está claro, validar que se seleccione al menos un permiso.

Usar el helper existente para obtener mensajes reales del backend cuando corresponda, manteniendo consistencia con otros módulos ya implementados.

---

## 3. Contexto

Leer antes de implementar:

- AGENTS.md
- frontend-skill.md
- PermisoDto.cs
- GrupoInsertDto.cs
- GrupoDto.cs
- GrupoUpdateDto.cs

---

## 4. Reglas y Restricciones (Constraints / Guardrails)

- No colocar que ningun datos del usuario autenticado en algun campo del formulario
- Trabajar solo en el frontend.
- No modificar backend.
- No cambiar endpoints.
- No cambiar nombres de DTOs del backend.
- No hardcodear URLs completas.
- No inventar campos que no existan en los DTOs.
- No usar `window.confirm`; usar modal visual.
- No mostrar ni enviar campos de socio.
- No enviar enums como número; enviarlos como string.
- Mantener consistencia visual con el resto de pantallas ya implementadas.
- Permisos para botones de acción:
  - En modo alta, mostrar **Crear** si tiene `CREAR_GRUPO`.
  - En modo edición, mostrar **Guardar** si tiene `EDITAR_GRUPO`.
  - Botón **Eliminar**: `ELIMINAR_GRUPO`.

- Los permisos frontend deben compararse contra los permisos guardados en `localStorage`, usando la lógica/helper existente en el proyecto.
- No usar las policies backend como códigos de permiso frontend.

---

## 5. Formato de Salida

Además de implementar el código solicitado, generar:

`IMPLEMENTATION_LOG_permisos-plan`

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
