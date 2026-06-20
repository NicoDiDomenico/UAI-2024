# IMPLEMENTATION_LOG_permisos-plan

## Archivos creados

- `Frontend/src/pages/PermisosPage.tsx`
- `Frontend/src/services/permisosService.ts`
- `Frontend/src/types/permiso.ts`
- `Docs/Frontend/E4-Gimnasio/E4P3-Permisos/IMPLEMENTATION_LOG_permisos-plan.md`

## Archivos modificados

- `Frontend/src/routes/AppRouter.tsx`
- `Frontend/src/App.css`
- `Frontend/src/pages/PermisosPage.tsx`

## Decisiones principales

- Se reemplazo el placeholder de `/gimnasio/permisos` por una pantalla operativa real.
- Se mantuvo el patron visual de `UsuariosPage`: encabezado, panel de grilla, panel de formulario, alertas y modal visual.
- Los permisos de botones se leen desde `useAuth().session.permisos`, igual que en otros modulos.
- No se modifico backend ni se inventaron campos fuera de los DTOs documentados.
- Ajuste UI posterior: el formulario de alta/edicion quedo a la izquierda y el listado de grupos a la derecha.
- Ajuste UI posterior: el boton `Nuevo grupo` se movio desde el encabezado del modulo hasta debajo del listado de grupos.
- Ajuste UI posterior: la lista de permisos seleccionables paso de una lista plana a una agrupacion por `Formulario`.
- Los permisos existentes en `GET /Permiso` que no aparezcan en `GET /Formulario` se muestran en un grupo adicional `Sin formulario`.
- Ajuste UI posterior: se agrego un filtro visual sobre la grilla de grupos seleccionables de la derecha, siguiendo el patron de filtros usado en `Usuarios`.
- Ajuste UI posterior: se corrigio el espaciado del filtro de grupos para que respete el padding y la alineacion del filtro de `Usuarios`.

## Integracion frontend/backend

- `GET /Permiso` carga la lista completa de permisos para checkboxes.
- `GET /Grupo` carga la grilla/listado de grupos.
- `GET /Formulario` se usa para agrupar visualmente los permisos por formulario a partir de los codigos devueltos por backend.
- `POST /Grupo` crea grupos con payload `{ nombre, descripcion, idPermisos }`.
- `PUT /Grupo/{idGrupo}` edita grupos con el mismo payload.
- `DELETE /Grupo/{idGrupo}` elimina el grupo seleccionado.
- Todas las llamadas pasan por `apiClient`, por lo que heredan `Authorization` y `X-Gym-Id` desde el interceptor centralizado.

## Validaciones implementadas

- `Nombre` obligatorio.
- `Descripcion` obligatoria.
- Al menos un permiso seleccionado antes de crear o editar.
- El payload enviado usa solamente los campos de `GrupoInsertDto` / `GrupoUpdateDto`.

## Estados, loading y errores

- Carga inicial de grupos y permisos con mensaje de loading.
- Carga inicial de formularios para construir la agrupacion de permisos.
- Error inicial separado de errores de formulario para no desmontar la pantalla por validaciones.
- Loading de guardado para crear/editar.
- Loading de eliminacion dentro del modal.
- Mensajes de exito para creacion, edicion y eliminacion.
- Mensajes de error del backend mediante `getApiErrorMessage`.
- Ajuste correctivo posterior en edicion: el frontend dejo de depender de `updatedGrupo.permisos` devuelto por `PUT /Grupo/{id}` para reconstruir el estado local del formulario.
- El filtro de la grilla de grupos permite buscar por `Nombre`, `Descripcion` o cantidad de `Permisos`.

## Permisos frontend

- Crear: `CREAR_GRUPO`.
- Guardar en edicion: `EDITAR_GRUPO`.
- Eliminar: `ELIMINAR_GRUPO`.

## TODOs / limitaciones detectadas

- En `GrupoController.cs`, el atributo del endpoint PUT figura como `[HttpPut("/{id}")]`. El plan documenta `PUT api/Grupo/{id}` y el frontend consume esa ruta. Si el backend responde 404 al editar, revisar ese atributo en backend.
- El backend devuelve `NotFound("No hay grupos cargados")` cuando no hay grupos; actualmente se muestra como error inicial, respetando el mensaje real del backend.
- Durante `Update`, el backend persiste correctamente el grupo pero puede devolver una coleccion `Permisos` no hidratada desde la entidad recien mapeada. Para evitar un falso error en frontend, la UI ahora recompone el grupo actualizado usando `selectedPermissionIds` y el catalogo de permisos ya cargado.

## Verificacion

- `npm.cmd run build` ejecutado correctamente en `Frontend`.
- `npx.cmd eslint src/pages/PermisosPage.tsx src/services/permisosService.ts src/types/permiso.ts src/routes/AppRouter.tsx` ejecutado correctamente.
- Vite respondio con HTTP 200 en `http://[::1]:5173/gimnasio/permisos`.
- `npm.cmd run lint` global falla por una regla existente en `Frontend/src/pages/UsuariosPage.tsx` (`react-hooks/set-state-in-effect`), fuera del modulo implementado.
- Tras los ajustes de UI, `npm.cmd run build` volvio a ejecutarse correctamente.
- Tras los ajustes de UI, `npx.cmd eslint src/pages/PermisosPage.tsx src/routes/AppRouter.tsx src/services/permisosService.ts src/types/permiso.ts` volvio a ejecutarse correctamente.
- Tras el ajuste del flujo de edicion de grupos, `npm.cmd run build` volvio a ejecutarse correctamente.
- Tras el ajuste del flujo de edicion de grupos, `npx.cmd eslint src/pages/PermisosPage.tsx src/routes/AppRouter.tsx src/services/permisosService.ts src/types/permiso.ts` volvio a ejecutarse correctamente.
- Tras la agrupacion de permisos por formulario, `npm.cmd run build` volvio a ejecutarse correctamente.
- Tras la agrupacion de permisos por formulario, `npx.cmd eslint src/pages/PermisosPage.tsx src/routes/AppRouter.tsx src/services/permisosService.ts src/services/formulariosService.ts src/types/permiso.ts src/types/formulario.ts` volvio a ejecutarse correctamente.
- Tras reubicar el filtro a la grilla de grupos, `npm.cmd run build` volvio a ejecutarse correctamente.
- Tras reubicar el filtro a la grilla de grupos, `npx.cmd eslint src/pages/PermisosPage.tsx src/routes/AppRouter.tsx src/services/permisosService.ts src/services/formulariosService.ts src/types/permiso.ts src/types/formulario.ts` volvio a ejecutarse correctamente.
- Tras corregir el espaciado visual del filtro de grupos, `npm.cmd run build` volvio a ejecutarse correctamente.
- Tras corregir el espaciado visual del filtro de grupos, `npx.cmd eslint src/pages/PermisosPage.tsx src/routes/AppRouter.tsx src/services/permisosService.ts src/services/formulariosService.ts src/types/permiso.ts src/types/formulario.ts` volvio a ejecutarse correctamente.
