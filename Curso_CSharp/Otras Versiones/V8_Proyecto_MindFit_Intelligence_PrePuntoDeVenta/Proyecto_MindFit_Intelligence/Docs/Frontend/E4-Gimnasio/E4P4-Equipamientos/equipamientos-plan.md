# Etapa 4 Gestionar Gimnasio - Parte 4 - Equipamientos

---

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend.

## 2. Tarea Principal

Implementar en el frontend el módulo **Equipamientos**, accesible desde la ruta:

`/gimnasio/equipamientos`

El objetivo es reemplazar el placeholder actual por una pantalla operativa de administración que permita **listar, crear, editar y eliminar equipamientos**, utilizando exclusivamente los DTOs y endpoints ya existentes en el backend.

La implementación debe mantenerse dentro del frontend y respetar la arquitectura actual del proyecto: tipos TypeScript, servicios centralizados, `apiClient`, permisos existentes y consistencia visual con las pantallas ya implementadas del módulo **Gestionar Gimnasio**.

---

### 2.1. Listado de equipamientos

Al ingresar a `/gimnasio/equipamientos`, el frontend debe cargar la grilla consumiendo:

`GET /api/Equipamiento`

**Response:**

`EquipamientoDto[]`

```csharp
public class EquipamientoDto
{
    public int IdEquipamiento { get; set; }
    public required string NombreEquipo { get; set; }
    public decimal CostoAdquisicion { get; set; }
    public decimal? PesoFijoKg { get; set; }
}
```

La grilla debe mostrar los equipamientos existentes con, al menos, los siguientes datos:

- Nombre del equipo.
- Costo de adquisición.
- Peso fijo en kg, cuando corresponda.

La pantalla debe permitir seleccionar un equipamiento de la grilla.
Al seleccionar una fila, sus datos deben cargarse en el formulario para edición o eliminación.

No es necesario consumir:

`GET /api/Equipamiento/{id}`

El listado ya trae todos los datos requeridos para operar sobre el equipamiento seleccionado.

---

### 2.2. Alta de equipamiento

Para crear un equipamiento, consumir:

`POST /api/Equipamiento`

**Request:**

`EquipamientoInsertDto`

**Response:**

`EquipamientoDto`

```csharp
public class EquipamientoInsertDto
{
    public required string NombreEquipo { get; set; }
    public decimal CostoAdquisicion { get; set; }
    public decimal? PesoFijoKg { get; set; }
}
```

El formulario de alta debe enviar únicamente:

- `nombreEquipo`
- `costoAdquisicion`
- `pesoFijoKg`

No enviar `idEquipamiento` ni `idGym` en el body.

Al crear correctamente:

- El nuevo equipamiento debe aparecer en la grilla.
- El listado debe actualizarse.
- El equipamiento creado debe quedar visible y debe quedar seleccionado para edición.

El botón de creación debe mostrarse únicamente si el usuario tiene el permiso frontend:

`CREAR_EQUIPAMIENTO`

---

### 2.3. Edición de equipamiento

Para editar un equipamiento seleccionado, consumir:

`PUT /api/Equipamiento/{id}`

**Request:**

`EquipamientoUpdateDto`

**Response:**

`EquipamientoDto`

```csharp
public class EquipamientoUpdateDto
{
    public required string NombreEquipo { get; set; }
    public decimal CostoAdquisicion { get; set; }
    public decimal? PesoFijoKg { get; set; }
}
```

El formulario de edición debe enviar únicamente:

- `nombreEquipo`
- `costoAdquisicion`
- `pesoFijoKg`

No enviar `idEquipamiento` ni `idGym` en el body.

Al guardar una edición correctamente:

- La grilla debe actualizarse.
- El equipamiento modificado debe quedar seleccionado.
- El formulario debe reflejar los datos actualizados.

El botón de guardado debe mostrarse únicamente si el usuario tiene el permiso frontend:

`EDITAR_EQUIPAMIENTO`

---

### 2.4. Eliminación de equipamiento

Para eliminar un equipamiento seleccionado, consumir:

`DELETE /api/Equipamiento/{id}`

**Response:**

`EquipamientoDto`

La eliminación debe realizarse mediante un **modal visual de confirmación**, consistente con el estilo actual del frontend.

No usar:

`window.confirm`

El flujo esperado es:

1. El usuario selecciona un equipamiento de la grilla.
2. Hace clic en **Eliminar**.
3. Se abre un modal de confirmación.
4. El modal informa qué equipamiento se va a eliminar.
5. Al confirmar, se llama al endpoint de eliminación.
6. Si la eliminación es exitosa:
   - El equipamiento se quita de la grilla.
   - El formulario vuelve a modo alta y queda limpio.
   - La selección actual se limpia.

El botón de eliminación debe mostrarse únicamente si el usuario tiene el permiso frontend:

`ELIMINAR_EQUIPAMIENTO`

---

### 2.5. Validaciones frontend mínimas

Antes de enviar datos al backend, validar:

- `nombreEquipo` es obligatorio.
- `nombreEquipo` no puede superar los 100 caracteres.
- `costoAdquisicion` debe ser mayor a 0.
- `pesoFijoKg` es opcional.
- Si `pesoFijoKg` se completa, debe ser mayor a 0.

Las validaciones deben mostrarse de forma clara en la pantalla, evitando enviar requests inválidos al backend.

---

### 2.6. Integración frontend esperada

Crear los tipos TypeScript necesarios para:

- `EquipamientoDto`
- `EquipamientoInsertDto`
- `EquipamientoUpdateDto`

Crear un servicio centralizado en:

`Frontend/src/services`

El servicio debe usar el `apiClient` existente, para aprovechar los interceptores actuales de:

- `Authorization: Bearer`
- `X-Gym-Id`

No hardcodear URLs completas.
Usar rutas relativas como:

- `/api/Equipamiento`
- `/api/Equipamiento/{id}`
  El endpoint backend efectivo es /api/Equipamiento, pero en el servicio frontend usar el patrón actual de apiClient, es decir /Equipamiento y /Equipamiento/{id}.

La lógica de permisos debe reutilizar el mecanismo existente del proyecto, leyendo los permisos guardados en sesión/localStorage.

---

### 2.7. Criterios de implementación

La implementación debe:

- Trabajar únicamente dentro de `/Frontend`.
- No modificar el backend.
- No cambiar endpoints, DTOs ni contratos existentes.
- Mantener consistencia visual con el módulo **Gestionar Gimnasio**.
- Reutilizar estilos, componentes o patrones ya existentes cuando sea posible.
- Mostrar mensajes claros de carga, éxito y error.
- Mostrar los errores reales del backend cuando estén disponibles.
- Dejar documentado lo realizado en el archivo de log correspondiente al plan.

---

## 3. Contexto

Leer antes de implementar:

- AGENTS.md
- frontend-skill.md
- EquipamientoController.cs
- EquipamientoDto.cs
- EquipamientoInsertDto.cs
- EquipamientoUpdateDto.cs

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
  - En modo alta, mostrar **Crear** si tiene `CREAR_EQUIPAMIENTO`.
  - En modo edición, mostrar **Guardar** si tiene `EDITAR_EQUIPAMIENTO`.
  - Botón **Eliminar**: `ELIMINAR_EQUIPAMIENTO`.

- Los permisos frontend deben compararse contra los permisos guardados en `localStorage`, usando la lógica/helper existente en el proyecto.
- No usar las policies backend como códigos de permiso frontend.

---

## 5. Formato de Salida

Además de implementar el código solicitado, generar:

`IMPLEMENTATION_LOG_equipamientos-plan.md`

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
