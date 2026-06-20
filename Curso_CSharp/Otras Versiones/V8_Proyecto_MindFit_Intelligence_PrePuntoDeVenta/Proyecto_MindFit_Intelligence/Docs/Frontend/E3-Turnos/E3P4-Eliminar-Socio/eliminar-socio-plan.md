# Etapa 3 Turnos - Parte 4 Eliminar Socio

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend

## 2. Tarea Principal

Implementar la baja lógica de un socio desde la pantalla **Ver Socios**.

En la grilla de socios, cuando el usuario seleccione un socio y haga clic en el botón **Eliminar**, el frontend debe ejecutar el endpoint:

`PATCH /api/Usuario/socio/{idUsuario}/baja`

Respuesta esperada:
Code: 200 OK
json RES (UsuarioDto):
{
"idUsuario": 0,
"username": "string",
"fechaRegistro": "2026-06-06T17:37:57.430Z",
"tipoPersona": "string",
"personaResponsable": {
"idUsuario": 0,
"nombre": "string",
"apellido": "string",
"email": "string",
"telefono": "string",
"direccion": "string",
"ciudad": "string",
"tipoDocumento": "string",
"nroDocumento": "string",
"genero": "Masculino",
"fechaNacimiento": "2026-06-06T17:37:57.430Z"
},
"personaSocio": {
"idUsuario": 0,
"nombre": "string",
"apellido": "string",
"email": "string",
"telefono": "string",
"direccion": "string",
"ciudad": "string",
"tipoDocumento": "string",
"nroDocumento": "string",
"genero": "Masculino",
"fechaNacimiento": "2026-06-06T17:37:57.430Z",
"obraSocial": "string",
"estadoSocio": "Nuevo",
"fechaInicioActividades": "2026-06-06T17:37:57.430Z",
"fechaNotificacion": "2026-06-06T17:37:57.430Z",
"respuestaNotificacion": true,
"pregunta": "string",
"respuesta": "string",
"rutinas": [
{
"idRutina": 0,
"idPersonaSocio": 0,
"idDia": 0,
"fechaModificacion": "2026-06-06T17:37:57.431Z",
"activo": true,
"calentamientos": [
{
"idCalentamiento": 0,
"idRutina": 0,
"ejercicio": {
"idEjercicio": 0,
"descEjercicio": "string",
"grupoMuscular": {
"idGrupoMuscular": 0,
"nombreMusculo": "Pecho",
"idMapaAnatomico": "string"
},
"tipoEjercicio": {
"idTipoEjercicio": 0,
"nombreTipo": "Calentamiento"
},
"maquina": {
"idMaquina": 0,
"nombreMaquina": "string",
"fechaFabricacion": "2026-06-06T17:37:57.431Z",
"fechaCompra": "2026-06-06T17:37:57.431Z",
"costoAdquisicion": 0,
"pesoMaximoLingotera": 0,
"esElectrica": true
},
"equipamiento": {
"idEquipamiento": 0,
"nombreEquipo": "string",
"costoAdquisicion": 0,
"pesoFijoKg": 0
}
},
"duracion": 0,
"orden": 0,
"observaciones": "string"
}
],
"entrenamientos": [
{
"idEntrenamiento": 0,
"idRutina": 0,
"ejercicio": {
"idEjercicio": 0,
"descEjercicio": "string",
"grupoMuscular": {
"idGrupoMuscular": 0,
"nombreMusculo": "Pecho",
"idMapaAnatomico": "string"
},
"tipoEjercicio": {
"idTipoEjercicio": 0,
"nombreTipo": "Calentamiento"
},
"maquina": {
"idMaquina": 0,
"nombreMaquina": "string",
"fechaFabricacion": "2026-06-06T17:37:57.431Z",
"fechaCompra": "2026-06-06T17:37:57.431Z",
"costoAdquisicion": 0,
"pesoMaximoLingotera": 0,
"esElectrica": true
},
"equipamiento": {
"idEquipamiento": 0,
"nombreEquipo": "string",
"costoAdquisicion": 0,
"pesoFijoKg": 0
}
},
"series": 0,
"repeticiones": 0,
"pesoAsignado": 0,
"tiempoDescansoSegundos": 0,
"orden": 0,
"observaciones": "string"
}
],
"estiramientos": [
{
"idEstiramiento": 0,
"idRutina": 0,
"ejercicio": {
"idEjercicio": 0,
"descEjercicio": "string",
"grupoMuscular": {
"idGrupoMuscular": 0,
"nombreMusculo": "Pecho",
"idMapaAnatomico": "string"
},
"tipoEjercicio": {
"idTipoEjercicio": 0,
"nombreTipo": "Calentamiento"
},
"maquina": {
"idMaquina": 0,
"nombreMaquina": "string",
"fechaFabricacion": "2026-06-06T17:37:57.431Z",
"fechaCompra": "2026-06-06T17:37:57.431Z",
"costoAdquisicion": 0,
"pesoMaximoLingotera": 0,
"esElectrica": true
},
"equipamiento": {
"idEquipamiento": 0,
"nombreEquipo": "string",
"costoAdquisicion": 0,
"pesoFijoKg": 0
}
},
"duracion": 0,
"orden": 0,
"observaciones": "string"
}
]
}
],
"cuotas": [
{
"idCuota": 0,
"idUsuario": 0,
"plan": "Mensual",
"fechaInicioPeriodo": "2026-06-06T17:37:57.431Z",
"fechaFinPeriodo": "2026-06-06T17:37:57.431Z",
"monto": 0,
"estadoCuota": "Vigente",
"fechaPago": "2026-06-06T17:37:57.431Z"
}
],
"perfilIA": {
"objetivoPrincipal": "string",
"nivelExperiencia": "string",
"ejerciciosPreferidos": "string",
"ejerciciosAEvitar": "string",
"disponibilidadHoraria": "string",
"motivacionPersonal": "string"
}
},
"grupos": [
{
"idGrupo": 0,
"nombre": "string",
"descripcion": "string",
"permisos": [
{
"idPermiso": 0,
"codigo": "string",
"descripcion": "string"
}
]
}
]
}

El objetivo es que el socio seleccionado cambie su estado a **Eliminado**.

### Comportamiento esperado

- El botón **Eliminar** debe actuar sobre el socio seleccionado en el grid.
- Antes de ejecutar la baja lógica, mostrar una confirmación al usuario.
- Si el usuario confirma, llamar al endpoint:

  `PATCH /api/Usuario/socio/{idUsuario}/baja`

- Si la operación es exitosa:
  - Mostrar un mensaje de éxito.
  - Refrescar o actualizar la grilla de socios.
  - Si el checkbox **Mostrar Socios Eliminados** está deshabilitado, el socio eliminado debe ocultarse del grid.
  - Si el checkbox **Mostrar Socios Eliminados** está habilitado, el socio puede seguir visible pero con estado **Eliminado**.
- Si ocurre un error:
  - Mostrar un mensaje de error.
  - No modificar visualmente la grilla de forma optimista.

### Integración requerida

Agregar en `sociosService.ts` un método específico para esta acción, por ejemplo:

`darDeBajaSocio(idUsuario: number): Promise<UsuarioDto>`

Este método debe usar la instancia centralizada de Axios existente.

### Aclaración importante

Esta acción corresponde a una **baja lógica**.

No usar:

- `DELETE /api/Usuario/socio/{idUsuario}`
- endpoint de borrado definitivo
- permiso `ELIMINAR_USUARIO_SOCIO_DEFINITIVAMENTE`

El endpoint correcto para el botón **Eliminar** de la grilla es:

`PATCH /api/Usuario/socio/{idUsuario}/baja`

## 3. Contexto

- AGENTS.MD
- frontend-skill.md
- `Frontend/src/pages/SociosPage.tsx`
- `Frontend/src/services/sociosService.ts`
- `Frontend/src/types/socio.ts`
- `Frontend/src/App.css`
- UsuarioController.cs
- UsuarioDto.cs

## 4. Reglas y Restricciones (Constraints / Guardrails)

### Boton Accion Eliminar

Permiso necesario: ELIMINAR_USUARIO_SOCIO

## 5. Formato de Salida

Además de implementar el código solicitado, generar: IMPLEMENTATION_LOG_eliminar-socio-plan.md

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
