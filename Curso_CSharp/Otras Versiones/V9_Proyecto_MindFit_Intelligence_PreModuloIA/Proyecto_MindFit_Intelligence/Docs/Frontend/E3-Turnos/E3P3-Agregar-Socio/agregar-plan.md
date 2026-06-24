# Etapa 3 Turnos - Parte 3 Agregar Socio

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend

## 2. Tarea Principal

## Resumen

Implementar en el frontend el flujo de **Alta de Socio** disparado desde el botón **Agregar** de la pantalla `/socios`.

Este caso de uso **no reutiliza el endpoint de modificación** usado en `consultar-modificar-borrar-plan.md`. Para crear un nuevo socio se debe usar:

`POST /api/Usuario/socio/register`

REQ: `UsuarioInsertDto`  
RES: `UsuarioDto`  
Policy: `CrearUsuarioSocio`  
Permiso frontend del botón **Agregar**: `CREAR_USUARIO_SOCIO`

El sistema asigna internamente el grupo de Socio (`IdGrupo = 3`), por lo que el flujo de alta debe construirse alrededor de ese contrato y no del caso de edición.

## Cambios de Implementación

- Reutilizar, en la medida de lo posible, la base visual y de campos del formulario de socio ya existente, pero separando claramente el modo **alta** del modo **consulta/edición**.
- El botón **Agregar** de `SociosPage` debe permanecer visible solo si el usuario autenticado posee `CREAR_USUARIO_SOCIO`.
- Al presionar **Agregar**, navegar a la pantalla/modal de creación de socio, sin `idUsuario` y sin precarga desde `GET /api/Usuario/{idUsuario}`.
- Ejecutar al cargar el formulario:
  - `GET /api/Dia/dias` para construir los checkboxes de días de asistencia.
- Al presionar **Guardar**, construir `UsuarioInsertDto` respetando exactamente el contrato backend:
  - `username`
  - `password`
  - `tipoPersona: "Socio"`
  - `personaResponsable: null` o ausente según el cliente HTTP serialice
  - `personaSocio` con:
    - `nombre`
    - `apellido`
    - `email`
    - `telefono`
    - `direccion`
    - `ciudad`
    - `tipoDocumento`
    - `nroDocumento`
    - `genero`
    - `fechaNacimiento`
    - `obraSocial`
    - `fechaNotificacion`
    - `respuestaNotificacion`
    - `pregunta`
    - `respuesta`
    - `diasActivosIds`
    - `cuota`
    - `perfilIA`
  - `idGrupos`
- En `idGrupos`, enviar el grupo de Socio para que el alta quede asociada al rol correcto:
  - valor esperado: `[3]`
- En `personaSocio.cuota`, enviar:
  - `plan`
  - `monto`
- En `personaSocio.perfilIA`, enviar los campos del perfil IA solo según `PerfilIAInsertDto`, sin inventar propiedades.
- Luego de un alta exitosa:
  - mostrar mensaje de éxito
  - cerrar modal o redirigir fuera del alta
  - refrescar la grilla de socios
  - dejar visible inmediatamente al nuevo socio en `/socios`
- Manejar errores de validación y conflictos devolviendo mensajes claros desde backend, sin asumir una forma distinta del payload de error ya usada por el proyecto.

## Reglas de UI y Comportamiento

- El formulario de alta debe incluir como mínimo:
  - datos personales del socio
  - credenciales de acceso: `username` y `password`
  - días de asistencia
  - datos de cuota inicial
  - sección de Perfil IA
- El flujo de alta no debe mostrar acciones exclusivas del caso de consulta/modificación:
  - no mostrar `Confirmar`
  - no mostrar `Cambiar Contraseña`
  - no mostrar `Renovar Cuota`
  - no mostrar `Borrar Socio Definitivamente`
- La acción principal del alta debe ser **Guardar**.
- Mostrar loading durante:
  - carga de días
  - envío del alta
- Evitar submit duplicado deshabilitando el botón mientras la petición esté en curso.
- Mantener consistencia visual con el módulo de socios ya implementado.

## APIs, Tipos y Servicios

- Centralizar la llamada de alta en `Frontend/src/services`.
- Agregar o ajustar tipos frontend para reflejar:
  - `UsuarioInsertDto`
  - `PersonaSocioInsertDto`
  - `CuotaInsertDto`
  - `PerfilIAInsertDto`
- No reutilizar directamente `UsuarioUpdateDto` para el alta.
- Mantener el uso del cliente Axios centralizado con:
  - `Authorization: Bearer {accessToken}`
  - `X-Gym-Id: {idGym}`
- Si el formulario de alta se monta antes de persistir cambios, usar estado local separado del detalle `UsuarioDto` de consulta para no mezclar creación con edición.

## Casos de Prueba

- Usuario con permiso `CREAR_USUARIO_SOCIO` ve el botón **Agregar**.
- Usuario sin ese permiso no ve el botón **Agregar**.
- Al entrar al alta se cargan correctamente los días desde `GET /api/Dia/dias`.
- Se puede seleccionar y deseleccionar días, y el submit envía `diasActivosIds` con los `idDia` elegidos.
- El submit exitoso a `POST /api/Usuario/socio/register` crea el socio y lo deja visible en la grilla.
- El formulario muestra errores backend si faltan campos obligatorios o si hay conflictos de validación.
- El alta envía `tipoPersona: "Socio"` e `idGrupos: [3]`.
- El flujo no intenta llamar endpoints de consulta ni de edición durante la creación.

## Supuestos y Defaults

- Se toma como default que el grupo Socio debe enviarse desde frontend como `idGrupos: [3]`, aunque el backend lo relacione conceptualmente con “Socio”.
- Se asume que el endpoint acepta el mismo esquema camelCase usado por el resto del frontend.
- Se asume que `personaResponsable` no participa en el alta de socio.
- Se asume que el botón de acción visible en la UI seguirá llamándose **Agregar** en la grilla y **Guardar** dentro del formulario.

## 3. Contexto

- AGENTS.MD
- frontend-skill.md
- UsuarioController.cs
- UsuarioInsertDto.cs
- PersonaSocioInsertDto.cs
- CuotaInsertDto.cs
- PerfilIAInsertDto.cs
- UsuarioDto.cs
- DiaController.cs
- DiaDto.cs

## 4. Reglas y Restricciones (Constraints / Guardrails)

### Boton Accion Agregar

Permiso necesario: CREAR_USUARIO_SOCIO

## 5. Formato de Salida

Además de implementar el código solicitado, generar: IMPLEMENTATION_LOG_agregar-plan.md

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
