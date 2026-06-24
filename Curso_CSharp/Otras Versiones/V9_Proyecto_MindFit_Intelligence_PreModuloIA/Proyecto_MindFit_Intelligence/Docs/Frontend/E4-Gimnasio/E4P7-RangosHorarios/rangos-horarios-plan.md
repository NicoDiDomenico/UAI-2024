# Etapa 4 Gestionar Gimnasio - Parte 7 - Rangos Horarios

---

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend.

## 2. Tarea Principal

Implementar en el frontend el modulo **Rangos Horarios**, accesible desde `/gimnasio/rangos-horarios`, reemplazando el placeholder actual por una pantalla operativa para asignar/quitar entrenadores y activar/desactivar/editar cupo del rango.

### Flujo de Endpoints

1.  [Authorize] GET api/DiaRangoHorario/grilla → RES: IEnumerable<GrillaDiaRangoHorarioDto> → Front: Se arman 2 grillas y 1 Form. una grilla arriba tiene una navegación de botones, estos botones tienen los días de la semana, en la Grilla se carga los Rango Horarios del día indicado en el botón (por defecto, el día actual). La otra grilla se encontrara a la derecha de la anterior y tendra los Entrenadores que ya fueron asignados en ese rango horario para ese dia (No mostrar Observaciones en la columna de la grilla solo Nombre + Apellido). El Form estará a la izquierda de la primera grilla y en este se cargaran los datos de la fila selecionada de la primera grilla a traves de un radio button que tendra la grilla → Usuario: selecciona un rango horario para asignarle Entrenador/es, Desactivar/Activar el rango horario.
    public class GrillaDiaRangoHorarioDto
    {
    public int IdDiaRangoHorario { get; set; }
    public int CupoActual { get; set; }
    public int CupoMaximo { get; set; }
    public bool Activo { get; set; }
    public TimeSpan HoraDesde { get; set; }
    public TimeSpan HoraHasta { get; set; }
    public string NombreDia { get; set; } = null!;
    public List<GrillaDiaRangoHorarioResponsableDto> Responsables { get; set; } = new List<GrillaDiaRangoHorarioResponsableDto>();

    }
    public class GrillaDiaRangoHorarioResponsableDto
    {
    public int IdUsuarioResponsable { get; set; }
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string? Observaciones { get; set; } // No se mostrara en la grilla del front
    }

2.  [Authorize] GET api/PersonaResponsable/entrenadores → RES: IEnumerable<EntrenadorDto> → Front: se arma un select box/dropdown en el form que contiene una lista de entrenadores con Nombre + Apellido obtenido de EntrenadorDto para asignar su IdUsuario a DiaRangoHorarioResponsableInsertDto.
    public class EntrenadorDto
    {
    public int IdUsuario { get; set; }
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    }

3.  POST api/DiaRangoHorario/asignar-responsable → REQ: DiaRangoHorarioResponsableInsertDto → Front: Botón Guardar que al hacerle clic se asigna un entrenador a un dia rango horario específico a partir de lo selecioado en la grilla y completado en el form→ Policy: ModificarDiaRangoHorario.
    public class DiaRangoHorarioResponsableInsertDto
    {
    public int IdDiaRangoHorario { get; set; }
    public int IdUsuarioResponsable { get; set; } // En este caso, el "responsable" es un "entrenador".
    public string? Observaciones { get; set; } // mandarlo nulo por defecto ya que por ahora en el front no pienso tocar las observaciones.
    }

4.  PATCH api/DiaRangoHorario/cambiar-estado/{IdDiaRangoHorario} → REQ: DiaRangoHorarioUpdateDto → Front: En el form va a haber un Switch button para activar o desactivar un rango horario y otro campo para establecer un cupo máximo, cuando se hace clic en "Guardar" se usa este endpoint para mandar DiaRangoHorarioUpdateDto → Policy: ModificarDiaRangoHorario.
    public class DiaRangoHorarioUpdateDto
    {
    public bool Activo { get; set; }
    public int CupoMaximo { get; set; }
    }
5.  DELETE api/DiaRangoHorario/quitar-responsable → REQ: DiaRangoHorarioResponsableDeleteDto → Front: Boton de eliminación en el grid de entrenadores asignados a rangos horarios en dia para quitar un específico → Policy: QuitarEntrenadorDiaRangoHorario.
    public class DiaRangoHorarioResponsableDeleteDto
    {
    public int IdDiaRangoHorario { get; set; }
    public int IdUsuarioResponsable { get; set; }
    }

## 3. Contexto

Leer antes de implementar:

- AGENTS.md
- frontend-skill.md
- Program.cs
- DiaRangoHorarioController.cs
- PersonaResponsableController.cs
- GrillaDiaRangoHorarioDto.cs
- GrillaDiaRangoHorarioResponsableDto.cs
- EntrenadorDto.cs
- DiaRangoHorarioResponsableInsertDto.cs
- DiaRangoHorarioUpdateDto.cs
- DiaRangoHorarioResponsableDeleteDto.cs

## 4. Reglas y Restricciones (Constraints / Guardrails)

- No mostrar, cargar ni enviar datos del usuario autenticado en el formulario actual de Rangos Horarios.
- Trabajar solo en el frontend.
- No modificar backend.
- No cambiar endpoints.
- No cambiar nombres de DTOs del backend.
- No hardcodear URLs completas.
- No inventar campos que no existan en los DTOs.
- No usar `window.confirm`; usar modal visual.
- No enviar enums como número; enviarlos como string.
- Mantener consistencia visual con el resto de pantallas ya implementadas.
- Permisos para botones de acción:
  - En el formulario de edición, mostrar **Guardar** si tiene `MODIFICAR_DIA_RH`.
  - Botón **Eliminar** al lado de cada registro de la 2da grilla: `QUITAR_ENTRENADOR_DIA_RH`.

- Los permisos frontend deben compararse contra los permisos guardados en `localStorage`, usando la lógica/helper existente en el proyecto.
- No usar las policies backend como códigos de permiso frontend.
- Para botones de días usar GET /DiaRangoHorario/grilla y filtrar por nombreDia en frontend
- El Boton Guardar ejecuta 2 endpoint: el 3. y el 4. El PATCH siempre se puede ejecutar si hay cambios de estado o cupo; el POST solo si hay un entrenador seleccionado.
- Filtrar del dropdown los entrenadores ya asignados al rango seleccionado
- Validar solo si cupoMaximo es >= 1
- Dice “día actual”. Como se filtra por nombreDia desde /grilla, tomaría el día actual del navegador y lo mapearía a Lunes, Martes, etc.
- Las respuestas erroneas del backend despues de hacer clic en "Guardar" manejarlas con un Modal que muestre la lista de errores

---

## 5. Formato de Salida

Además de implementar el código solicitado, generar:

`IMPLEMENTATION_LOG_rangos-horarios-plan.md`

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
