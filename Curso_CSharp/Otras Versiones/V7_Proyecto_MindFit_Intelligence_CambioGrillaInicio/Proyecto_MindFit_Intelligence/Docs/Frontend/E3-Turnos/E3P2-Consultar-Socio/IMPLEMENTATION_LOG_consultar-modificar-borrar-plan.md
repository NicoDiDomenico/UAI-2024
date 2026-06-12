# Implementation Log - Consultar / Modificar / Borrar Socio

## Archivos creados o modificados

- Creado: `Frontend/src/components/socios/ConsultarSocioModal.tsx`
- Modificado: `Frontend/src/pages/SociosPage.tsx`
- Modificado: `Frontend/src/routes/AppRouter.tsx`
- Modificado: `Frontend/src/services/sociosService.ts`
- Modificado: `Frontend/src/types/socio.ts`
- Modificado: `Frontend/src/App.css`

## Decisiones importantes

- La ruta `/socios/:idUsuario/consultar` ahora renderiza `SociosPage` y abre el modal por parametro de ruta. Esto mantiene la grilla como contexto visual y funcional.
- El modal centraliza las pestañas `Info. Personal`, `Facturacion`, `Perfil IA` y `Seguridad`.
- Los campos editables inician en modo lectura y habilitan edicion individual mediante boton `Editar`.
- Los cambios de campos, dias y renovacion se mantienen en estado local y se persisten solo con `Confirmar Cambios`.
- La visibilidad de acciones se basa en los permisos persistidos en sesion:
  - `EDITAR_USUARIO_SOCIO`
  - `CAMBIAR_CONTRASENA_SOCIO`
  - `ELIMINAR_USUARIO_SOCIO_DEFINITIVAMENTE`

## Integracion frontend/backend

- Se agregaron metodos en `sociosService` para:
  - `GET /api/Dia/dias`
  - `GET /api/Usuario/{idUsuario}`
  - `PUT /api/Usuario/socio/{idUsuario}`
  - `DELETE /api/Usuario/socio/{idUsuario}`
  - `POST /api/Auth/socio/change-password`
- Se ampliaron los tipos de `socio.ts` con DTOs camelCase derivados de los DTOs backend.
- `diasActivosIds` se construye desde los checkboxes seleccionados.
- La renovacion de cuota se envia dentro de `personaSocio.cuota` con `renueva`, `plan` y `monto`.
- Los grupos existentes se preservan enviando `idGrupos` desde `usuario.grupos`.

## Validaciones implementadas

- Campos minimos antes de confirmar socio:
  - nombre
  - apellido
  - email
  - tipoDocumento
  - nroDocumento
- Al menos un dia de asistencia seleccionado.
- Si se renueva cuota, requiere plan y monto mayor a cero.
- Cambio de contraseña requiere contraseña actual y nueva contraseña de al menos 8 caracteres.
- Cierre del modal con cambios sin confirmar pide confirmacion al usuario.
- Borrado definitivo pide confirmacion explicita.

## Loading, errores y mensajes

- El modal muestra loading inicial mientras carga dias y detalle del socio.
- Confirmar cambios, cambiar contraseña y borrar definitivamente manejan estados de loading independientes.
- Se muestran mensajes de exito y error usando el estilo existente de alertas del frontend.
- Al confirmar cambios o borrar definitivamente se refresca la grilla de socios.

- Ajuste posterior: los campos de `Cambiar Contrasena` usan una estrategia reforzada contra autocompletado con inputs senuelo ocultos, nombres no asociados a credenciales y `readOnly` hasta foco real, para evitar que el navegador inyecte la clave del usuario autenticado.

## Axios / interceptors

- No se modifico `apiClient`.
- Los nuevos endpoints usan la instancia centralizada existente, que ya agrega:
  - `Authorization: Bearer {accessToken}`
  - `X-Gym-Id: {idGym}`

## TODOs o limitaciones detectadas

- El plan indica cambiar contraseña del socio seleccionado, pero el backend actual `POST /api/Auth/socio/change-password` obtiene el `idUsuario` desde el JWT. El frontend invoca el endpoint documentado, aunque la accion efectiva depende del usuario autenticado segun el controlador actual.
- No se modifico backend por las reglas del proyecto.
- Los iconos se resolvieron con texto corto para evitar agregar dependencias nuevas.

## Verificacion

- Ejecutado: `npm.cmd run build`
- Resultado: build TypeScript/Vite exitoso.

## Ajuste posterior - boton Renovar Cuota

- Modificado: `Frontend/src/components/socios/ConsultarSocioModal.tsx`
- Se resolvio la visibilidad haciendo que el boton `Renovar Cuota` solo se renderice cuando `renovacion.active` es `false`.
- La cancelacion de la renovacion ahora reutiliza un reseteo explicito del estado local, limpiando `plan`, `monto` y ocultando la seccion expandida.
- Se ajusto el calculo de cambios pendientes para que una renovacion cancelada no deje el modal marcado artificialmente como modificado.
- Verificado nuevamente con build exitoso.
