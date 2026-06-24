# Implementation Log - Maquinas

## Archivos creados o modificados

- `Frontend/src/types/maquina.ts`: tipos TypeScript para `MaquinaDto`, `MaquinaInsertDto` y `MaquinaUpdateDto`.
- `Frontend/src/services/maquinasService.ts`: servicio centralizado para `GET`, `POST`, `PUT` y `DELETE` de maquinas usando `apiClient`.
- `Frontend/src/pages/MaquinasPage.tsx`: pantalla operativa para listar, crear, editar, seleccionar y eliminar maquinas.
- `Frontend/src/routes/AppRouter.tsx`: reemplazo del placeholder de `/gimnasio/maquinas` por `MaquinasPage`.
- `Frontend/src/App.css`: estilos especificos para formulario, switch y grilla de maquinas, reutilizando la base visual de Equipamientos.

## Decisiones importantes

- Se trabajo solo dentro de `/Frontend`, sin modificar backend.
- Se reutilizo el patron de `EquipamientosPage` para mantener consistencia en permisos, estados, seleccion, modal y mensajes.
- No se consume `GET /api/Maquina/{id}` porque `GET /api/Maquina` trae los datos necesarios para cargar el formulario.
- Los permisos frontend usados son `CREAR_MAQUINA`, `EDITAR_MAQUINA` y `ELIMINAR_MAQUINA`; no se comparan las policies backend.

## Integracion frontend/backend

- El servicio usa rutas relativas del `apiClient`: `/Maquina` y `/Maquina/{id}`.
- `apiClient` conserva la responsabilidad de agregar `Authorization` y `X-Gym-Id`.
- Los bodies de alta y modificacion envian solo campos del DTO: `nombreMaquina`, `fechaFabricacion`, `fechaCompra`, `costoAdquisicion`, `pesoMaximoLingotera` y `esElectrica`.
- No se envia `idMaquina` ni `idGym` en el body.

## Validaciones implementadas

- Nombre obligatorio y maximo 100 caracteres.
- Fecha de fabricacion obligatoria y no futura.
- Fecha de compra obligatoria, no futura y no anterior a la fecha de fabricacion.
- Costo de adquisicion mayor a 0.
- Peso maximo de lingotera opcional; si se completa, debe ser mayor a 0.

## Estados y errores

- La pantalla maneja carga inicial, error de carga, guardado, eliminacion, mensajes de exito y errores de backend.
- La eliminacion usa modal visual de confirmacion y no `window.confirm`.
- Al crear o editar, la grilla se actualiza localmente y el registro queda seleccionado.
- Al eliminar, la grilla remueve el registro y el formulario vuelve a modo alta.

## TODOs o limitaciones

- No se agregaron tests automatizados especificos porque el proyecto no tiene una suite frontend dedicada para estas pantallas.
- Si en una etapa futura se centralizan componentes de ABM, `EquipamientosPage` y `MaquinasPage` podrian compartir componentes de formulario, grilla y modal.
