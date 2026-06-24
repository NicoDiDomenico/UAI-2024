# Etapa 4 Gestionar Gimnasio - Parte 6 - Ejercicios

---

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend.

## 2. Tarea Principal

Implementar en el frontend el modulo **Ejercicios**, accesible desde `/gimnasio/ejercicios`, reemplazando el placeholder actual por una pantalla operativa para listar, crear, editar y eliminar ejercicios.

### Flujo de Endpoints

1.  a. [Authorize] GET api/Ejercicio → RES: IEnumerable<EjercicioDto> → Front: Para el grid de ejercicios → Usuario: Selecciona un ejercicio y se carga en el formulario con su detalle así con el IdEjercicio hacer la Baja o Modificación.
    public class EjercicioDto
    {
    public int IdEjercicio { get; set; }
    public required string DescEjercicio { get; set; }
    public GrupoMuscularDto GrupoMuscular { get; set; } = default!;
    public TipoEjercicioDto TipoEjercicio { get; set; } = default!;
    public MaquinaDto? Maquina { get; set; }
    public EquipamientoDto? Equipamiento { get; set; }
    }
    public class GrupoMuscularDto
    {
    public int IdGrupoMuscular { get; set; }
    public required Musculo NombreMusculo { get; set; }
    public string? IdMapaAnatomico { get; set; }
    }
    public enum Musculo
    {
    // Mapeados exactamente con los IDs y nombres de tu tabla GrupoMuscular
    Pecho = 1,
    Espalda = 2,
    Cuadriceps = 3,
    Biceps = 4,
    Triceps = 5,
    Gluteos = 6,
    Abdomen = 7,
    Hombros = 8,
    Gemelos = 9,
    Antebrazos = 10,
    Lumbares = 11,
    Isquiotibiales = 12
    }
    b. [Authorize] GET api/GrupoMuscular → IEnumerable<GrupoMuscularDto> → Front: Para el Selector (Dropdown) con los NombreMusculo en el formulario de Ejercicio  Usuario: Selecciona un GrupoMuscular y se obtiene IdGrupoMuscular para asignarlo a EjercicioInsertDto y EjercicioUpdateDto.
    c. [Authorize] GET api/TipoEjercicio → IEnumerable<TipoEjercicioDto> → Front: Para el Selector (Dropdown) con los NombreTipo en el formulario de Ejercicio  Usuario: Selecciona un TipoEjercicio y se obtiene IdTipoEjercicio para asignarlo a EjercicioInsertDto y EjercicioUpdateDto.
    public class TipoEjercicioDto
    {
    public int IdTipoEjercicio { get; set; }
    public required TipoDeEjercicio NombreTipo { get; set; }
    }
    public enum TipoDeEjercicio
    {
    Calentamiento = 1,
    Entrenamiento = 2,
    Estiramiento = 3
    }
    d. [Authorize] GET api/Equipamiento → RES: IEnumerable<EquipamientoDto> → Front: Para el Selector (Dropdown) con los nombreEquipo en el formulario de Ejercicio → Usuario: Selecciona un Equipamiento y se obtiene IdEquipamiento para asignarlo a EjercicioInsertDto y EjercicioUpdateDto.
    public class EquipamientoDto
    {
    public int IdEquipamiento { get; set; }
    public required string NombreEquipo { get; set; }
    public decimal CostoAdquisicion { get; set; }
    public decimal? PesoFijoKg { get; set; }
    }
    e. [Authorize] GET api/Maquina → RES: IEnumerable<MaquinaDto> → Front: Para el Selector (Dropdown) con los nombreMaquina en el formulario de Ejercicio → Usuario: Selecciona una máquinas y se obtiene IdMáquinas para asignarlo a EjercicioInsertDto y EjercicioUpdateDto.
    public class MaquinaDto
    {
    public int IdMaquina { get; set; }
    public required string NombreMaquina { get; set; }
    public DateTime FechaFabricacion { get; set; }
    public DateTime FechaCompra { get; set; }
    public decimal CostoAdquisicion { get; set; }
    public decimal? PesoMaximoLingotera { get; set; }
    public bool EsElectrica { get; set; }
    }

2.  POST api/Ejercicio → REQ: EjercicioInsertDto, RES: EjercicioDto → Front: Formulario de creación de ejercicio → Usuario: Completa y confirma el formulario para el Alta del ejercicio; el nuevo registro aparece en el grid y queda queda seleccionado → Policy: CrearEjercicio.
    public class EjercicioInsertDto
    {
    public required string DescEjercicio { get; set; }
    public int IdGrupoMuscular { get; set; }
    public int IdTipoEjercicio { get; set; }
    public int? IdMaquina { get; set; }
    public int? IdEquipamiento { get; set; }
    }
3.  PUT api/Ejercicio/{id} → REQ: EjercicioUpdateDto, RES: EjercicioDto → Front: Formulario de edición de ejercicio → Usuario: Modifica campos y guarda; el grid se actualiza y el elemento queda seleccionado → Policy: EditarEjercicio.
    public class EjercicioUpdateDto
    {
    public required string DescEjercicio { get; set; }
    public int IdGrupoMuscular { get; set; }
    public int IdTipoEjercicio { get; set; }
    public int? IdMaquina { get; set; }
    public int? IdEquipamiento { get; set; }
    }
4.  DELETE api/Ejercicio/{id} → RES: EjercicioDto → Front: Acción de eliminación desde el grid (confirmación) → Usuario: Confirma la eliminación; el ejercicio se elimina del grid y se limpia/cierra el formulario → Policy: EliminarEjercicio.

---

## 3. Contexto

Leer antes de implementar:

- AGENTS.md
- frontend-skill.md
- Program.cs
- EjercicioUpdateDto.cs
- EjercicioInsertDto.cs
- EjercicioDto.cs
- GrupoMuscularDto.cs
- TipoEjercicioDto.cs
- TipoDeEjercicio.cs
- Musculo.cs
- EquipamientoDto.cs
- MaquinaDto.cs
- EjercicioController.cs
- GrupoMuscularController.cs
- TipoEjercicioController.cs
- EquipamientoController.cs
- MaquinaController.cs

---

## 4. Reglas y Restricciones (Constraints / Guardrails)

- No mostrar, cargar ni enviar datos del usuario autenticado en el formulario actual de Ejercicios.
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
  - En modo alta, mostrar **Crear** si tiene `CREAR_EJERCICIO`.
  - En modo edición, mostrar **Guardar** si tiene `EDITAR_EJERCICIO`.
  - Botón **Eliminar**: `ELIMINAR_EJERCICIO`.

- Los permisos frontend deben compararse contra los permisos guardados en `localStorage`, usando la lógica/helper existente en el proyecto.
- No usar las policies backend como códigos de permiso frontend.
- Un ejercicio puede tener máquina y equipamiento al mismo tiempo.

---

## 5. Formato de Salida

Además de implementar el código solicitado, generar:

`IMPLEMENTATION_LOG_ejercicios-plan.md`

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
