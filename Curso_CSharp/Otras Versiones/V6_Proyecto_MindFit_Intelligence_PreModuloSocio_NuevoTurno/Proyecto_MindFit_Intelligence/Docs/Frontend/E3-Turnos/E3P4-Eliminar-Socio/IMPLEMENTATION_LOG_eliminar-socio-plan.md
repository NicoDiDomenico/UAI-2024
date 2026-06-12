# Implementation Log - Eliminar socio

## Archivos modificados

- `Frontend/src/pages/SociosPage.tsx`
- `Frontend/src/services/sociosService.ts`
- `Frontend/src/App.css`
- `Frontend/src/utils/apiError.ts`

## Archivos creados

- `Docs/Frontend/E3-Turnos/E3P4-Eliminar-Socio/IMPLEMENTATION_LOG_eliminar-socio-plan.md`
- `Frontend/src/components/socios/EliminarSocioModal.tsx`

## Decisiones importantes

- La baja logica se implemento directamente desde la grilla de `Ver socios`, porque esa pantalla ya concentra:
  - seleccion del socio
  - permiso `ELIMINAR_USUARIO_SOCIO`
  - filtro `Mostrar socios eliminados`
- No se uso la ruta placeholder `/socios/:idUsuario/eliminar` para ejecutar la accion. El boton `Eliminar` ahora abre un modal visual propio y la llamada API ocurre dentro de ese flujo.
- Se eligio actualizar la grilla localmente con la respuesta del backend en lugar de hacer una actualizacion optimista o una recarga completa innecesaria.
- Se guardo una copia estable del socio seleccionado al abrir el modal para evitar que el modal se cierre solo cuando el socio pasa a `Eliminado` y queda oculto por el filtro.

## Integracion frontend/backend

- Se agrego en `sociosService.ts` el metodo:
  - `darDeBajaSocio(idUsuario: number): Promise<UsuarioDto>`
- El metodo usa la instancia centralizada `apiClient`, por lo que reutiliza:
  - `VITE_API_BASE_URL`
  - `Authorization: Bearer ...`
  - `X-Gym-Id`
- Endpoint integrado:
  - `PATCH /Usuario/socio/{idUsuario}/baja`

## Validaciones implementadas

- El boton solo funciona si hay un socio seleccionado.
- Antes de llamar al endpoint se abre un modal de confirmacion consistente con el sistema.
- Mientras la solicitud esta en curso, el modal bloquea multiples confirmaciones y muestra estado `Confirmando eliminacion...`.
- En error no se altera la grilla de forma optimista.

## Manejo de estados, loading y errores

- Se creo `EliminarSocioModal` para encapsular:
  - confirmacion
  - loading
  - exito
  - error real del backend
- La pagina mantiene separado:
  - error de carga de la grilla
  - estado de apertura del modal
  - socio objetivo de la baja
- Ante exito:
  - se actualiza el `estadoSocio` del elemento en la grilla
  - si `Mostrar socios eliminados` esta apagado, el filtro actual lo oculta automaticamente
  - si esta encendido, el socio sigue visible con estado `Eliminado`
  - el modal permanece abierto y muestra mensaje de exito hasta que el usuario pulse `Cerrar`
- Ante error:
  - el modal permanece abierto
  - se usa un mensaje especifico de baja logica en lugar del mensaje de carga de socios
  - no se cambia el estado visual del socio
  - el usuario puede reintentar o cerrar

## Utilidad de errores Axios

- Se ajusto `apiError.ts` para priorizar mensajes reales devueltos por backend.
- Casos soportados:
  - `["Mensaje"]`
  - `{ "message": "Mensaje" }`
  - `{ "errors": ["Error 1", "Error 2"] }`
  - `{ "title": "Error de validacion" }`
- Solo se usa fallback generico cuando la respuesta no trae informacion util.

## Axios / interceptors / contexto

- No fue necesario modificar interceptors ni contexto de auth.
- La accion nueva queda cubierta por la configuracion ya existente en `apiClient.ts`.

## TODOs o limitaciones

- La ruta `/socios/:idUsuario/eliminar` sigue existiendo como placeholder en el router, pero ya no es usada por el boton principal de la grilla.
- Si en una iteracion futura se necesita acceso directo por URL a una pantalla de baja, convendria redefinir esa ruta con un flujo dedicado.
