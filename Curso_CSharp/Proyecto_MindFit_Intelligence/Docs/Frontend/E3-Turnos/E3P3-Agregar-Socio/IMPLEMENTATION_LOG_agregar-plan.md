# Implementation Log - Agregar socio

## Archivos modificados

- `Frontend/src/App.css`
- `Frontend/src/routes/AppRouter.tsx`
- `Frontend/src/services/sociosService.ts`
- `Frontend/src/pages/SociosPage.tsx`
- `Frontend/src/types/socio.ts`

## Archivos creados

- `Docs/Frontend/E3-Turnos/E3P3-Agregar-Socio/IMPLEMENTATION_LOG_agregar-plan.md`
- `Frontend/src/pages/AgregarSocioPage.tsx`

## Decisiones importantes

- La ruta `/socios/agregar` dejo de usar el placeholder y ahora renderiza una pantalla real de alta.
- El formulario de alta usa estado propio separado del flujo de consulta/modificacion para no mezclar `UsuarioInsertDto` con `UsuarioUpdateDto`.
- El alta envia siempre `tipoPersona: "Socio"`, `personaResponsable: null` e `idGrupos: [3]`.
- La accion principal dentro del formulario se llama `Guardar`, mientras que el boton de la grilla sigue siendo `Agregar`.
- Ajuste posterior: `/socios/agregar` ahora renderiza `SociosPage` y abre el alta como modal, igual que el flujo de `Consultar`.
- Ajuste posterior: el modal de alta se cierra sin cuadro de confirmacion al usar `x`, `Cancelar` o volver a `/socios`.
- Ajuste posterior: los campos `Usuario` y `Contraseña` usan nombres dinamicos y `autoComplete` para evitar autocompletado con credenciales del usuario autenticado.
- Ajuste posterior: se corrigio el texto visible `Contrasena` a `Contraseña`.

 - Ajuste posterior: se reforzo la defensa contra autocompletado con campos ocultos senuelo y `readOnly` hasta foco real, porque algunos navegadores seguian completando el alta con credenciales del operador.

## Integracion frontend/backend

- Se agrego en `sociosService.ts` el metodo:
  - `registerSocio(dto: UsuarioInsertDto): Promise<UsuarioDto>`
- El metodo usa la instancia centralizada `apiClient`, por lo que reutiliza:
  - `VITE_API_BASE_URL`
  - `Authorization: Bearer ...`
  - `X-Gym-Id`
- Endpoint integrado:
  - `POST /Usuario/socio/register`
- El formulario carga los dias disponibles con:
  - `GET /Dia/dias`

## Validaciones implementadas

- Se valida antes de enviar:
  - usuario
  - contrasena de al menos 8 caracteres
  - nombre
  - apellido
  - email
  - tipo y numero de documento
  - al menos un dia de asistencia
  - plan y monto mayor a cero
- Los errores reales del backend se muestran con `getSociosErrorMessage`.

## Manejo de estados, loading y errores

- El formulario muestra loading al cargar dias.
- El boton `Guardar` se deshabilita mientras se carga dias o mientras se envia el alta.
- El cierre del modal no solicita confirmacion, por pedido funcional posterior.
- Al crear correctamente el socio, se redirige a `/socios`; esa pantalla recarga la grilla y muestra el alta nueva.

## TODOs o limitaciones

- El permiso `CREAR_USUARIO_SOCIO` gobierna la visibilidad del boton `Agregar` y la apertura del modal de alta desde `SociosPage`.
- Si mas adelante el backend expone un endpoint para obtener el id del grupo Socio por metadata, conviene reemplazar el default fijo `[3]`.
