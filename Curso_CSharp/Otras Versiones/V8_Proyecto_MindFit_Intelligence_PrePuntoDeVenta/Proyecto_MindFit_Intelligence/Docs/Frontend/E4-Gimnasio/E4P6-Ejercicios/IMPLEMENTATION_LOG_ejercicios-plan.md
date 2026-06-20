# Implementation Log - Ejercicios

## Archivos creados o modificados

- `Frontend/src/types/ejercicio.ts`: tipos TypeScript para `EjercicioDto`, `EjercicioInsertDto`, `EjercicioUpdateDto`, `GrupoMuscularDto`, `TipoEjercicioDto` y enums serializados como string.
- `Frontend/src/services/ejerciciosService.ts`: servicio centralizado para `Ejercicio`, `GrupoMuscular`, `TipoEjercicio`, `Maquina` y `Equipamiento`.
- `Frontend/src/pages/EjerciciosPage.tsx`: pantalla operativa para listar, crear, editar y eliminar ejercicios.
- `Frontend/src/routes/AppRouter.tsx`: reemplazo del placeholder de `/gimnasio/ejercicios`.
- `Frontend/src/App.css`: estilos específicos del modulo, reutilizando la base visual de Equipamientos/Maquinas.

## Decisiones importantes

- Se mantuvo el layout operativo existente: formulario a la izquierda y grilla a la derecha.
- Maquina y equipamiento se modelaron como selectores independientes porque el plan indica que pueden coexistir.
- No se agregaron dependencias nuevas.
- No se tocaron archivos del backend.

## Integracion frontend/backend

- `GET /Ejercicio` carga la grilla.
- `GET /GrupoMuscular`, `GET /TipoEjercicio`, `GET /Maquina` y `GET /Equipamiento` cargan los selectores.
- `POST /Ejercicio`, `PUT /Ejercicio/{id}` y `DELETE /Ejercicio/{id}` resuelven alta, modificacion y baja.
- La instancia Axios existente agrega `Authorization` y `X-Gym-Id`.

## Validaciones implementadas

- `descEjercicio` obligatorio.
- `descEjercicio` maximo 200 caracteres.
- `idGrupoMuscular` obligatorio.
- `idTipoEjercicio` obligatorio.
- `idMaquina` e `idEquipamiento` se envian como `null` cuando no se seleccionan.

## Estados, loading y errores

- Loading inicial para ejercicios y catalogos.
- Mensaje de error si falla la carga inicial.
- Estados de guardado y eliminacion para bloquear acciones duplicadas.
- Mensajes de exito y error usando el helper `getApiErrorMessage`.
- Modal visual para confirmar eliminacion, sin `window.confirm`.

## Permisos

- Crear: `CREAR_EJERCICIO`.
- Guardar edicion: `EDITAR_EJERCICIO`.
- Eliminar: `ELIMINAR_EJERCICIO`.
- Se comparan contra `session.permisos`, hidratados desde el almacenamiento existente.

## TODOs o limitaciones

- La pantalla no implementa mapa anatomico porque el plan actualizado pide acciones desde grilla.
- El detalle de maquina/equipamiento se muestra como resumen simple con datos ya cargados; no se usan endpoints de detalle por id.
