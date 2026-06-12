# Etapa 3 Turnos - Parte 6 Gestionar Turno

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend

## 2. Tarea Principal

Posterior al hacer cick en el elemento "<button class="ghost-button socios-action" type="button">Turnos</button>", se debe crear un un modal en el que se muestre una grilla en la que se carguen el historial de turnos del socio selecionado a partir del siguinte endpoint:
[Authorize] GET api/Turno/asistente/{idUsuarioSocio}
RES: IEnumerable<TurnoDto>
JSON RES (TurnoDto):
[
{
"idTurno": 0,
"fechaAlta": "2026-06-08T17:16:29.887Z",
"estadoTurno": "EnCurso",
"horaDesde": "string",
"horaHasta": "string",
"nombreDia": "string",
"nombreResponsable": "string",
"apellidoResponsable": "string"
}
]
El Usuario seleciona un turno de la grilla cargada (se obtiene IdTurno) para cancelarlo (Boton "Cancelar") o agrega uno nuevo (Boton "Nuevo Turno).

## 3. Contexto

- AGENTS.MD
- frontend-skill.md
- IMPLEMENTATION_LOG_turno-socios-plan
- TurnoController.cs
- TurnoDto.cs
- EstadoTurno.cs
- UsuarioController.cs
- SocioGridDto.cs

## 4. Reglas y Restricciones (Constraints / Guardrails)

- EstadoTurno tiene un estado que es "EnCurso", al mostrarlo en la grilla escribirlo con espacio en blanco de esta forma "En Curso", pero para mandarlo en una reques colocarlo como vino de la response del backend, o sea escrito asi: "EnCurso".
- Para la visibildiad de los botones segun permisos del usuario logueado:

### Boton de Navegacion

#### Boton "Nuevo Turno"

Permiso necesario: AGREGAR_TURNO.

### Boton de Accion

#### Boton "Cancelar Turno"

Permiso necesario: CANCELAR_TURNO.

- Los botones de "Nuevo Turno" y "Cancelar Turno", se les implementará las funcionaldiades de visibilidad segun permisos que el usuario tiene (estos permisos estan en el localStorage), es decir la autorizacion en el frontend. Al hacer clic en cualquiera de ambos botones se abrira un Modal que diga "proximamente...", asi dejo sus correspondientes implementaciones para otra etapa.
- La grilla tiene que tener las siguientes columnas:
  Fecha Turno --> public DateTime FechaAlta { get; set; }
  Hora Desde --> public TimeSpan HoraDesde { get; set; }
  Hora Hasta --> public TimeSpan HoraHasta { get; set; }
  Estado Turno --> public EstadoTurno EstadoTurno { get; set; }
  Entrenador --> public string NombreResponsable { get; set; } = null!; + public string ApellidoResponsable { get; set; } = null!;
- Arriba de la grilla y a la izquierda habra un titulo que diga "Historial de turnos".
- Arriba de la grilla y a la derecha estaran 2 datos del usuario: NombreCompleto y NroDocumento. Ambos datos provienen del endpoint de la etapa anterior `GET /api/Usuario/grilla-socio`.
- 404 = lista vacía --> Para historial de turnos tiene sentido mostrar “El socio no tiene turnos registrados”.
- Nuevo Turno NO requiere selección. Solo necesita el socio seleccionado. Cancelar Turno SÍ requiere seleccionar un turno, porque necesita idTurno.
- Aclaración sobre navegación del botón Turnos:

* Actualmente el botón Turnos navega a `/socios/:idUsuario/turnos`
* Se debe mantener esa ruta.
* Pero en lugar de mostrar una página placeholder de "Turnos del socio", esa ruta debe renderizar un modal visualmente consistente con el modal de Agregar socio.
* El modal debe abrirse sobre la pantalla de socios, con el fondo oscurecido, y debe poder cerrarse con la X o con un botón Volver/Cerrar, regresando a `/socios`.

- Aclaraciones Fecha Turno:

* La columna visual **Fecha Turno** debe mostrar el valor recibido en `fechaAlta`.
* Aunque el nombre técnico del backend sea `fechaAlta`, en la UI debe mantenerse el encabezado **Fecha Turno**.

- Aclaraciones Hora Desde y Hora Hasta:

* Los campos `horaDesde` y `horaHasta` deben mostrarse tal como vienen desde el backend.
* No aplicar conversión horaria ni recalcular horarios en frontend.
* Solo aplicar un formateo visual simple si fuera necesario para quitar segundos o mejorar legibilidad, sin modificar el valor original usado internamente.

- Aclaraciones Placeholder de acciones:

* El botón **Nuevo Turno** abre el placeholder `Próximamente...` sin requerir selección de fila.

* El botón **Cancelar Turno** abre el placeholder `Próximamente...` únicamente si existe un turno seleccionado.

* Si no hay turno seleccionado, el botón **Cancelar Turno** debe estar deshabilitado.

## 5. Formato de Salida

Además de implementar el código solicitado, generar: IMPLEMENTATION_LOG_gestion-turnos-plan.md

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
