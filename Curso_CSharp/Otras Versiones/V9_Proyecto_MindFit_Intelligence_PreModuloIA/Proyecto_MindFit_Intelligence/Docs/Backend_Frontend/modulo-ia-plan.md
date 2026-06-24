# Asistente IA para Recomendación de Rutinas

## Resumen

Implementar una primera funcionalidad de IA centrada en `/rutinas`: el entrenador selecciona turno, entrenador, socio y día como hoy, pulsa **Sugerir con IA**, agrega una indicación opcional, y recibe una propuesta de calentamiento, entrenamiento y estiramiento. La propuesta solo reemplaza el draft visible en frontend; se guarda en base recién cuando el entrenador pulsa **Guardar**.

Se usará OpenAI desde backend con Responses API, Structured Outputs y `store: false`, enviando solo datos necesarios del socio, perfil IA, rutina actual y catálogo de ejercicios del tenant.

## Cambios Clave

- Extender `PerfilIA` y sus DTOs con datos útiles para recomendaciones:
  - `LesionesLimitaciones`
  - `CondicionesMedicasRelevantes`
  - `IntensidadPreferida`
  - `DuracionSesionMinutos`
  - `NotasEntrenador`
  - `FechaActualizacion`
- Crear migración EF para esos campos y actualizar alta/edición/consulta de socios donde hoy se carga `PerfilIA`.
- Agregar configuración backend:
  - `OpenAI:ApiKey`, desde variable de entorno `OpenAI__ApiKey`
  - `OpenAI:Model`, default `gpt-5.5`
  - `OpenAI:BaseUrl`, default `https://api.openai.com/v1`
- Crear un servicio backend `IRutinaIaService` que:
  - Cargue socio + `PerfilIA`, rutina actual, día y catálogo de ejercicios.
  - Construya un prompt con IDs reales de ejercicios disponibles.
  - Solicite una salida JSON estructurada compatible con `RutinaBloquesUpdateDto`.
  - Valide que todos los `IdEjercicio` existan, pertenezcan al tenant y respeten tipo: calentamiento, entrenamiento o estiramiento.
  - Devuelva error claro si falta API key, no hay ejercicios o la IA devuelve una propuesta inválida.
- Agregar endpoint:
  - `POST /api/Rutina/{idRutina}/ia/recomendacion`
  - Policy: `EditarRutina`
  - Body: `{ instruccionesEntrenador?: string }`
  - Response: `{ resumen, criterios, advertencias, propuesta }`, donde `propuesta` contiene `calentamientos`, `entrenamientos`, `estiramientos`.

## Frontend

- Agregar tipos y método en `rutinasService` para pedir la recomendación.
- En `GestionRutinasPage`, dentro del workspace de rutina:
  - Mostrar botón **Sugerir con IA** solo si el usuario puede editar rutina y hay rutina cargada.
  - Abrir un panel/modal con textarea opcional para foco del día.
  - Al generar, mostrar loading, errores y resumen de la propuesta.
  - Botón **Aplicar propuesta** reemplaza el draft local usando el catálogo de ejercicios ya existente.
  - El botón **Guardar** sigue siendo el único punto que persiste la rutina y dispara historial.
- No implementar chat libre en v1; queda como evolución futura.

## Test Plan

- Backend:
  - `dotnet build`
  - Verificar endpoint sin API key: responde error controlado, sin romper rutinas.
  - Verificar endpoint con socio sin `PerfilIA`: genera usando datos básicos y rutina actual.
  - Verificar validación de IDs/tipos de ejercicios antes de responder al frontend.
  - Verificar que no persiste cambios hasta llamar a `PUT /Rutina/{idRutina}/bloques`.
- Frontend:
  - `npm run build`
  - `npm run lint`
  - Flujo manual en `/rutinas`: seleccionar socio/día, generar IA, aplicar propuesta, editar una fila, guardar.
  - Confirmar que usuarios sin `EDITAR_RUTINA` no ven la acción IA.

## Supuestos

- Se prioriza **Rutinas asistidas** sobre chat general o analítica de adherencia.
- Se usa OpenAI real en backend, no modo demo.
- No se guardan prompts, respuestas ni borradores IA; solo la rutina final aprobada por el entrenador.
- La IA es asistente, no autoridad médica: el backend y la UI deben dejar claro que el entrenador revisa la propuesta antes de guardarla.
- Referencias técnicas usadas: OpenAI recomienda Responses API y Structured Outputs para respuestas JSON validadas: https://developers.openai.com/api/docs y https://developers.openai.com/api/docs/guides/structured-outputs
