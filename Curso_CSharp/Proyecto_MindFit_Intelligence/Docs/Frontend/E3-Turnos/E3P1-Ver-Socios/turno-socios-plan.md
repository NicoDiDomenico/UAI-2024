# Etapa 3 Turnos - Parte 1 Ver Socios

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend

## 2. Tarea Principal

Implementar en el frontend la pantalla **Ver Socios**, accesible desde la ruta `/socios`.

La pantalla debe mostrar:

- Un grid/listado de socios.
- Un checkbox o filtro para **Mostrar Socios Eliminados**.
- Cuatro botones principales:
  - **Agregar**
  - **Consultar**
  - **Eliminar**
  - **Turnos**

### Flujo inicial de carga

Al ingresar a la pantalla `/socios`, el frontend debe ejecutar internamente este flujo:

1. Llamar a:

   `PUT /api/Cuota/actualizar-vencidas`

   Objetivo: permitir que el backend actualice cuotas vencidas y cambie el estado del socio según corresponda.

2. Luego llamar a:

   `PATCH /api/Usuario/procesar-eliminaciones-pendientes`

   Objetivo: permitir que el backend procese automáticamente los socios suspendidos que deban pasar a estado `Eliminado` según la regla RN07.

3. Finalmente llamar a:

   `GET /api/Usuario/grilla-socio`

   Objetivo: obtener la lista de socios para mostrar en el grid.

El frontend no debe implementar la lógica de negocio de vencimientos, suspensión o eliminación automática. Solo debe invocar los endpoints correspondientes y reflejar el resultado en pantalla.

### Grid de socios

El grid debe construirse usando la respuesta de:

`GET /api/Usuario/grilla-socio`

Tipo de respuesta:

`List<SocioGridDto>`

Campos esperados:

```json
[
  {
    "idUsuario": 0,
    "username": "string",
    "fechaRegistro": "2026-06-05T15:44:41.459Z",
    "nombreCompleto": "string",
    "email": "string",
    "estadoSocio": "Nuevo",
    "plan": "Mensual",
    "fechaFinPeriodo": "2026-06-05T15:44:41.459Z"
  }
]
```

El grid debe mostrar como mínimo:

- nombreCompleto
- estadoSocio
- fechaFinPeriodo
  Los demás campos del DTO pueden quedar disponibles internamente para futuras funcionalidades.

El usuario debe poder seleccionar un socio del grid.

Al seleccionar un socio:

- Guardar internamente el `idUsuario` seleccionado.
- Habilitar los botones:
  - **Consultar**
  - **Eliminar**
  - **Turnos**

Si no hay socio seleccionado, esos botones deben permanecer deshabilitados.

### Filtro Mostrar Socios Eliminados

La pantalla debe permitir ocultar o mostrar socios con estado `Eliminado`.

- Si el checkbox **Mostrar Socios Eliminados** está deshabilitado, no mostrar socios con estado `Eliminado`.
- Si está habilitado, mostrar también los socios eliminados.
- Los registros correspondientes a socios con estado `Eliminado` y `Suspendido` deben visualizarse con texto de color rojo en el grid.

### Filtro de búsqueda

La pantalla debe incluir un filtro similar al mostrado en la referencia visual.

El filtro estará compuesto por:

- Un selector de campo.
- Un campo de texto para ingresar el valor a buscar.

Campos disponibles para filtrar:

- Socio (`nombreCompleto`)
- Estado (`estadoSocio`)
- Fecha Vencimiento Cuota (`fechaFinPeriodo`)

Comportamiento:

- El filtrado debe realizarse sobre los datos ya cargados en el grid.
- No realizar llamadas adicionales al backend.
- La búsqueda debe actualizar el grid mostrando únicamente los registros que coincidan con el criterio seleccionado.
- Si el valor de búsqueda está vacío, mostrar nuevamente todos los registros.
- La implementación puede realizarse completamente del lado cliente.

### Botones

#### Agregar

Debe estar disponible según permiso y servir como punto de entrada para crear un nuevo socio.

#### Consultar

Debe estar disponible según permisos. Debe requerir un socio seleccionado y usar el `idUsuario` del socio seleccionado.

#### Eliminar

Debe estar disponible según permiso. Debe requerir un socio seleccionado y usar el `idUsuario` del socio seleccionado.

#### Turnos

Debe estar disponible según permisos. Debe requerir un socio seleccionado y usar el `idUsuario` del socio seleccionado para acceder a la gestión/consulta de turnos del socio.

## 3. Contexto

- AGENTS.MD
- frontend-skill.md
- CuotaController.cs
- UsuarioController.cs
- ProcesarEliminacionesDto.cs
- SocioGridDto.cs
- Usar la captura adjunta como referencia de layout general.

## 4. Reglas y Restricciones

- “disponible según permiso” significa ocultar o deshabilitar el botón. Ejemplo: Si el usuario no tiene el permiso requerido, el botón no debe mostrarse.
- El componente actual que se está implementando se va a renderizar posterior a cuando se hace clic en el siguiente elemento: "<a class="navigation-link" href="/socios" data-discover="true"><span class="navigation-link__number">02</span><span class="navigation-link__content"><strong>Ver socios</strong><small>Accede a socios y gestiona sus turnos.</small></span><span class="navigation-link__arrow" aria-hidden="true">&gt;</span></a>".
- En esta etapa los botones Agregar, Consultar, Eliminar y Turnos deben navegar únicamente a vistas placeholder mostrando el título de la sección y el mensaje "Próximamente". No implementar formularios, llamadas a APIs, lógica de negocio, CRUD ni gestión de turnos. La funcionalidad real será desarrollada en etapas posteriores.

### Botones de Navegación

#### Boton Consultar

- EDITAR_USUARIO_SOCIO
- CAMBIAR_CONTRASENA_SOCIO
- ELIMINAR_USUARIO_SOCIO_DEFINITIVAMENTE

#### Boton Turnos

- AGREGAR_TURNO
- CANCELAR_TURNO

### Botones de Acción

#### Boton Agregar

- CREAR_USUARIO_SOCIO

#### Boton Eliminar

- ELIMINAR_USUARIO_SOCIO

## 5. Formato de Salida

Además de implementar el código solicitado, generar: IMPLEMENTATION_LOG_turno-socios-plan.md

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
