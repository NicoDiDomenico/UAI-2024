# Implementation Log - usuarios-plan

## Archivos creados

- `Frontend/src/types/usuario.ts`
- `Frontend/src/services/usuariosService.ts`
- `Frontend/src/pages/UsuariosPage.tsx`
- `Docs/Frontend/E4-Gimnasio/E4P2-Usuarios/IMPLEMENTATION_LOG_usuarios-plan.md`

## Archivos modificados

- `Frontend/src/routes/AppRouter.tsx`
- `Frontend/src/App.css`

## Decisiones importantes

- Se reemplazo el placeholder de `/gimnasio/usuarios` por `UsuariosPage`.
- La pantalla se implemento como workspace de dos columnas: formulario/acciones a la izquierda y grilla de responsables a la derecha.
- Se mantuvo un unico formulario visual con dos modos: creacion y edicion.
- En creacion se muestra `Password` y `RepetirPassword`; en edicion general no se muestran campos de password.
- El cambio de contrasena queda en una seccion separada y solo aparece en modo edicion con permiso correspondiente.
- Los permisos no son editables manualmente; se calculan visualmente a partir de los grupos seleccionados.

## Integracion frontend/backend

- Se centralizaron endpoints del modulo en `usuariosService`.
- Se reutiliza `apiClient`, por lo que `Authorization`, `X-Gym-Id` y `VITE_API_BASE_URL` siguen centralizados.
- Endpoints integrados:
  - `GET /Usuario/grilla-responsable`
  - `GET /Usuario/{idUsuario}`
  - `POST /Usuario/responsable/register`
  - `PUT /Usuario/responsable/{idUsuario}`
  - `DELETE /Usuario/responsable/{idUsuario}`
  - `POST /Auth/responsables/{idUsuario}/change-password`
  - `GET /Grupo`
  - `GET /Formulario`

## Grupos, formularios y permisos

- `GET /Grupo` carga los chips seleccionables.
- Se excluye cualquier grupo cuyo `nombre` sea `SOCIO`, comparando sin distinguir mayusculas/minusculas.
- `GET /Formulario` carga todos los formularios y todos sus permisos.
- Los acordeones muestran siempre todos los formularios y permisos.
- Un permiso se ve activo si su codigo existe en al menos uno de los grupos seleccionados.
- El tooltip de cada permiso indica los grupos seleccionados que lo contienen, o que no esta incluido en los grupos seleccionados.
- Al crear o editar se envia solo `idGrupos`; no se envian permisos individuales.

## Validaciones implementadas

- Campos obligatorios: username, nombre, apellido, email, tipo documento y nro documento.
- Email con formato basico.
- En creacion: password obligatorio, minimo 8 caracteres y repeticion coincidente.
- Seleccion obligatoria de al menos un grupo.
- Cambio de contrasena: contrasena actual, nueva contrasena y repeticion obligatorias; nueva contrasena y repeticion deben coincidir.
- No se permite doble submit durante creacion, edicion, cambio de contrasena o eliminacion.

## Estados, loading y errores

- Carga inicial de responsables, grupos y formularios.
- Carga de detalle del responsable seleccionado.
- Loading de creacion/edicion.
- Loading de cambio de contrasena.
- Loading dentro del modal de eliminacion.
- Errores priorizan mensajes reales del backend usando `getApiErrorMessage`.
- Estados vacios para grilla, grupos y formularios.

## Verificacion

- `npm.cmd run build` ejecutado correctamente en `Frontend`.
- `npm run build` no se pudo ejecutar directamente por la policy de PowerShell sobre `npm.ps1`; se uso `npm.cmd run build`.
- Luego de los arreglos de `arreglos-modificaciones.md`, se volvio a ejecutar `npm.cmd run build` correctamente.

## Arreglos posteriores

- Se agregaron campos decoy y atributos `autoComplete="new-password"` en el formulario de responsables para evitar que el navegador cargue credenciales del usuario logueado cuando no hay responsable seleccionado.
- El boton `Nuevo` se movio desde el encabezado del formulario hacia la parte inferior izquierda del panel de grilla.
- La grilla de responsables dejo de forzar `min-width` y scroll horizontal; ahora usa `table-layout: fixed`, ajuste de ancho por columnas y corte de texto con `overflow-wrap`.
- La grilla excluye al usuario actualmente autenticado usando `session.datosPersonales.id`, sin modificar la respuesta del backend ni crear endpoints nuevos.
- Se agrego filtro frontend por columna para `Username`, `NombreCompleto`, `Email` y `NombreGrupo`; la busqueda es case-insensitive y `NombreGrupo` busca dentro de la lista de roles.
- Si el responsable seleccionado deja de estar visible por filtro o por exclusion del usuario autenticado, se limpia la seleccion y el formulario vuelve a modo creacion.
- Luego de agregar exclusion de usuario autenticado y filtros por columna, se volvio a ejecutar `npm.cmd run build` correctamente.

## TODOs o limitaciones

- No se agregaron dependencias nuevas.
- No se modifico backend.
- La verificacion visual debe hacerse con backend y sesion real para validar datos, permisos y certificados HTTPS locales.
