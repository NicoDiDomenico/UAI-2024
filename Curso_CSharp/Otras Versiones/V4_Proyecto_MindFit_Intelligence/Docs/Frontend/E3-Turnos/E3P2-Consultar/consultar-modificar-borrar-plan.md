# Etapa 3 Turnos - Parte 2 Consultar

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend

## 2. Tarea Principal

Implementar en el frontend el modal de **Consultar Socio**, accesible desde la ruta:

`/socios/:idUsuario/consultar`

El objetivo de esta pantalla es permitir visualizar, editar y administrar un socio existente.

La pantalla debe reutilizar, en la medida de lo posible, el mismo formulario utilizado para Alta de Socio, precargando la información obtenida desde backend.

### Información Personal

Mostrar todos los datos del socio en modo lectura inicialmente:

- Nombre
- Apellido
- Fecha de nacimiento
- Género
- Tipo y número de documento
- Ciudad
- Dirección
- Teléfono
- Email
- Obra Social
- Días de asistencia

Cada campo editable debe disponer de un botón de edición individual (ícono lápiz).

Al editar un campo:

- El control correspondiente pasa a modo edición.
- El usuario puede modificar el valor.
- Los cambios se persisten al confirmar.

### Información de Facturación

Mostrar la información de la cuota vigente:

- Plan actual
- Estado del socio
- Fecha de vencimiento de cuota

Los estados posibles incluyen:

- Nuevo
- Activo
- Suspendido
- Eliminado

La UI debe adaptar las acciones visibles según el estado actual del socio.

Ejemplos:

- Nuevo: mostrar Renovar Cuota.
- Suspendido: mostrar Renovar Cuota.
- Eliminado: mostrar Renovar Cuota y Borrar Socio Definitivamente.

### Renovación de Cuota

Mostrar un botón **Renovar Cuota**.

Al presionarlo:

- Debe mostrarse una sección expandida dentro del bloque Facturación.
- Deben aparecer:
  - Selector de Plan
    - Mensual
    - Anual
  - Campo Monto
  - Opción para cancelar la renovación iniciada

Al confirmar:

- Debe enviarse la información de renovación al backend.
- Si el socio se encontraba en estado Suspendido o Eliminado:
  - Debe volver a visualizarse normalmente en el listado de socios.
  - Debe eliminarse cualquier indicación visual utilizada para marcarlo como eliminado o suspendido.

### Eliminación Definitiva

Cuando el socio se encuentre en estado Eliminado:

- Mostrar el botón:
  - Borrar Socio Definitivamente

Al ejecutarlo:

- Solicitar confirmación explícita al usuario.
- El endpoint debe ejecutarse utilizando el idUsuario del socio actualmente consultado.
- Invocar el endpoint:

  DELETE /api/Usuario/socio/{idUsuario}

- Eliminar físicamente el socio de la base de datos.
- Actualizar el listado de socios una vez completada la operación.
- Mostrar mensajes de éxito o error según corresponda.
- Respetar la policy EliminarUsuarioSocioDefinitivamente.

### Perfil IA

Mostrar una pestaña o sección denominada IA.

La sección debe visualizar la información del Perfil IA asociada al socio:

- Objetivo principal
- Nivel de experiencia
- Ejercicios preferidos
- Ejercicios a evitar
- Disponibilidad horaria
- Motivación personal

Los datos deben cargarse utilizando la información recibida desde UsuarioDto.
Los campos del Perfil IA deben ser editables y persistirse junto con el resto de la información del socio al confirmar.

### Acciones Disponibles

La pantalla debe incluir:

- Confirmar
- Cambiar Contraseña
  - Al presionar el botón **Cambiar Contraseña**, deben mostrarse dos campos adicionales:
    - Contraseña actual
    - Nueva contraseña
  - Los campos deben permanecer ocultos hasta que el usuario presione el botón **Cambiar Contraseña**.
  - Junto a esos campos debe mostrarse un botón **Confirmar Cambio**.
  - Al presionar **Confirmar Cambio**, el frontend debe construir el `ChangePasswordRequestDto` con los valores ingresados.
  - Debe invocar el endpoint `POST /api/Auth/socio/change-password`.
  - Debe enviar:
    - `currentPassword`
    - `newPassword`
  - Debe respetar la policy `CambiarContrasenaSocio`.
  - Debe mostrar loading, éxito o error según la respuesta del backend.
- Renovar Cuota
- Borrar Socio Definitivamente (solo cuando corresponda)

La visibilidad y habilitación de cada acción debe respetar los permisos del usuario autenticado.

### Requisitos de UX

- Mostrar indicadores de carga durante las llamadas al backend.
- Mostrar mensajes claros de éxito y error.
- Evitar pérdida accidental de cambios.
- Mantener consistencia visual con el formulario de Alta de Socio.
- Reutilizar componentes, hooks y tipos existentes cuando sea posible.

### Flujo inicial de carga

Al ingresar al modal, el frontend debe ejecutar internamente este flujo:

1. Llamar a:
   [Authorize] GET /api/Usuario/{idUsuario}  RES: UsuarioDto? --> Front:

- Reutilizar el formulario de Alta de Socio.
- Precargar los datos obtenidos desde UsuarioDto.
- Mostrar la información de Facturación según el estado actual del socio.
- Mostrar la información de Perfil IA obtenida desde el backend
- Si se renueva la cuota y estaba “Suspendido” o “Eliminado” hay que volverlo a mostrar en el Grid y sacarle el color rojo de la fuente.
  json RES (UsuarioDto):
  {
  "idUsuario": 0,
  "username": "string",
  "fechaRegistro": "2026-06-05T18:01:33.229Z",
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
  "fechaNacimiento": "2026-06-05T18:01:33.229Z"
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
  "fechaNacimiento": "2026-06-05T18:01:33.229Z",
  "obraSocial": "string",
  "estadoSocio": "Nuevo",
  "fechaInicioActividades": "2026-06-05T18:01:33.229Z",
  "fechaNotificacion": "2026-06-05T18:01:33.229Z",
  "respuestaNotificacion": true,
  "pregunta": "string",
  "respuesta": "string",
  "rutinas": [
  {
  "idRutina": 0,
  "idPersonaSocio": 0,
  "idDia": 0,
  "fechaModificacion": "2026-06-05T18:01:33.229Z",
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
  "fechaFabricacion": "2026-06-05T18:01:33.229Z",
  "fechaCompra": "2026-06-05T18:01:33.229Z",
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
  "fechaFabricacion": "2026-06-05T18:01:33.229Z",
  "fechaCompra": "2026-06-05T18:01:33.229Z",
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
  "fechaFabricacion": "2026-06-05T18:01:33.229Z",
  "fechaCompra": "2026-06-05T18:01:33.229Z",
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
  "fechaInicioPeriodo": "2026-06-05T18:01:33.229Z",
  "fechaFinPeriodo": "2026-06-05T18:01:33.229Z",
  "monto": 0,
  "estadoCuota": "Vigente",
  "fechaPago": "2026-06-05T18:01:33.229Z"
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

2. Llamar a:
   PUT /api/Usuario/socio/{idUsuario} --> REQ: UsuarioUpdateDto, RES: UsuarioDto?  Sistema: Registra usuario --> Policy: EditarUsuarioSocio.
   json REQ (UsuarioUpdateDto):
   {
   "username": "string",
   "tipoPersona": "string",
   "personaResponsable": {
   "nombre": "string",
   "apellido": "string",
   "email": "string",
   "telefono": "string",
   "direccion": "string",
   "ciudad": "string",
   "tipoDocumento": "string",
   "nroDocumento": "string",
   "genero": "Masculino",
   "fechaNacimiento": "2026-06-05T17:57:00.477Z"
   },
   "personaSocio": {
   "nombre": "string",
   "apellido": "string",
   "email": "string",
   "telefono": "string",
   "direccion": "string",
   "ciudad": "string",
   "tipoDocumento": "string",
   "nroDocumento": "string",
   "genero": "Masculino",
   "fechaNacimiento": "2026-06-05T17:57:00.477Z",
   "obraSocial": "string",
   "fechaNotificacion": "2026-06-05T17:57:00.477Z",
   "respuestaNotificacion": true,
   "pregunta": "string",
   "respuesta": "string",
   "diasActivosIds": [
   0
   ],
   "perfilIA": {
   "objetivoPrincipal": "string",
   "nivelExperiencia": "string",
   "ejerciciosPreferidos": "string",
   "ejerciciosAEvitar": "string",
   "disponibilidadHoraria": "string",
   "motivacionPersonal": "string"
   },
   "cuota": {
   "renueva": true,
   "plan": "Mensual",
   "monto": 0
   }
   },
   "idGrupos": [
   0
   ]
   }

   json REQ (UsuarioDto):
   {
   "idUsuario": 0,
   "username": "string",
   "fechaRegistro": "2026-06-05T18:01:33.229Z",
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
   "fechaNacimiento": "2026-06-05T18:01:33.229Z"
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
   "fechaNacimiento": "2026-06-05T18:01:33.229Z",
   "obraSocial": "string",
   "estadoSocio": "Nuevo",
   "fechaInicioActividades": "2026-06-05T18:01:33.229Z",
   "fechaNotificacion": "2026-06-05T18:01:33.229Z",
   "respuestaNotificacion": true,
   "pregunta": "string",
   "respuesta": "string",
   "rutinas": [
   {
   "idRutina": 0,
   "idPersonaSocio": 0,
   "idDia": 0,
   "fechaModificacion": "2026-06-05T18:01:33.229Z",
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
   "fechaFabricacion": "2026-06-05T18:01:33.229Z",
   "fechaCompra": "2026-06-05T18:01:33.229Z",
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
   "fechaFabricacion": "2026-06-05T18:01:33.229Z",
   "fechaCompra": "2026-06-05T18:01:33.229Z",
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
   "fechaFabricacion": "2026-06-05T18:01:33.229Z",
   "fechaCompra": "2026-06-05T18:01:33.229Z",
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
   "fechaInicioPeriodo": "2026-06-05T18:01:33.229Z",
   "fechaFinPeriodo": "2026-06-05T18:01:33.229Z",
   "monto": 0,
   "estadoCuota": "Vigente",
   "fechaPago": "2026-06-05T18:01:33.229Z"
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

3. Para confirmar el cambio de contraseña llamar a:
   POST /api/Auth/socio/change-password
   REQ: ChangePasswordRequestDto
   Policy: CambiarContrasenaSocio

Front:

- Usar este endpoint para cambiar la contraseña del socio seleccionado desde el sistema.
- Solicitar contraseña actual y nueva contraseña.
- Mostrar loading, éxito y error.
- No enviar el formulario completo del socio para cambiar contraseña.

json REQ (ChangePasswordRequestDto):
{
"currentPassword": "string",
"newPassword": "string"
}

4. Para eliminar definitivamente a un socio llamar a:
   DELETE /api/Usuario/socio/{idUsuario}
   RES: UsuarioDto
   Policy: EliminarUsuarioSocioDefinitivamente

Front:

- Utilizado por el botón Borrar Socio Definitivamente.
- Debe solicitar confirmación antes de ejecutar la acción.
- Realiza la baja física del socio.
- El socio deja de existir en el sistema.
- Actualizar el listado una vez completada la operación.

## 3. Contexto

- AGENTS.MD
- frontend-skill.md
- UsuarioController.cs
- UsuarioDto.cs
- ChangePasswordRequestDto.cs
- AuthController.cs
- UsuarioUpdateDto.cs
- Se adjuntan 4 imagenes: 3 son del Socio consultado pero cada una tiene diferentes estados de socio, y la otra corresponde a la seccion de Perfil IA.

### Botones de Acción

#### Boton Confirmar y Botones con un lapiz que indica edicion y que estan lado de cada campo de texto

- EDITAR_USUARIO_SOCIO

#### Boton Cambiar Contraseña

- CAMBIAR_CONTRASENA_SOCIO

#### Boton Borrar Socio Definitivamente

- ELIMINAR_USUARIO_SOCIO_DEFINITIVAMENTE

## 5. Formato de Salida

Además de implementar el código solicitado, generar: IMPLEMENTATION_LOG_consultar-modificar-borrar-plan.md

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
