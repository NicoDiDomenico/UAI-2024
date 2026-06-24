# Etapa 4 Gestionar Gimnasio - Parte 5 - Maquinas

---

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend.

## 2. Tarea Principal

Implementar en el frontend el modulo **Maquinas**, accesible desde `/gimnasio/maquinas`, reemplazando el placeholder actual por una pantalla operativa para listar, crear, editar y eliminar maquinas.

La implementacion debe trabajar solo dentro de `/Frontend`, consumir los endpoints existentes del backend y seguir el patron ya usado en **Equipamientos**: tipos TypeScript, servicio centralizado, `apiClient`, permisos frontend, modal visual de confirmacion, estados de carga/error/exito y consistencia visual con Gestionar Gimnasio.

### Key Changes

- Crear tipos TypeScript para `MaquinaDto`, `MaquinaInsertDto` y `MaquinaUpdateDto` con estos campos:
  - `idMaquina: number` solo en response.
  - `nombreMaquina: string`
  - `fechaFabricacion: string`
  - `fechaCompra: string`
  - `costoAdquisicion: number`
  - `pesoMaximoLingotera: number | null`
  - `esElectrica: boolean`
- Crear `maquinasService` en `Frontend/src/services`, usando `apiClient` y rutas relativas:
  - `GET /Maquina`
  - `POST /Maquina`
  - `PUT /Maquina/{idMaquina}`
  - `DELETE /Maquina/{idMaquina}`
- Crear `MaquinasPage` y conectar `/gimnasio/maquinas` en el router.
- Reutilizar el modelo visual y de interaccion de `EquipamientosPage`, adaptando textos, columnas, filtros y formulario a maquinas.
- No consumir `GET /api/Maquina/{id}` porque el listado trae todos los datos necesarios para seleccionar, editar y eliminar.

### Screen Behavior

- Al entrar a `/gimnasio/maquinas`, cargar la grilla con `GET api/Maquina`.
- La grilla debe mostrar al menos:
  - Nombre de maquina.
  - Fecha de fabricacion.
  - Fecha de compra.
  - Costo de adquisicion.
  - Peso maximo de lingotera, si corresponde.
  - Si es electrica.
- Al seleccionar una fila, cargar sus datos en el formulario y guardar `idMaquina` como seleccion actual.
- En modo alta, enviar `POST api/Maquina` con `MaquinaInsertDto`; al crear, actualizar la grilla, dejar el nuevo registro seleccionado y cargarlo en el formulario.
- En modo edicion, enviar `PUT api/Maquina/{id}` con `MaquinaUpdateDto`; al guardar, actualizar la grilla y mantener seleccionado el registro modificado.
- Para eliminar, abrir un modal visual de confirmacion, sin `window.confirm`; al confirmar, llamar `DELETE api/Maquina/{id}`, quitar la maquina de la grilla y limpiar el formulario.

### Permissions

- Mostrar boton **Crear** solo con `CREAR_MAQUINA`.
- Mostrar boton **Guardar** solo con `EDITAR_MAQUINA`.
- Mostrar boton **Eliminar** solo con `ELIMINAR_MAQUINA`.
- Comparar contra permisos guardados en sesion/localStorage usando la logica/helper existente.
- No usar las policies backend (`CrearMaquina`, `EditarMaquina`, `EliminarMaquina`) como codigos frontend.

### Validation And API Rules

- Validar antes de enviar:
  - `nombreMaquina` obligatorio.
  - `nombreMaquina` maximo 100 caracteres.
  - `fechaFabricacion` no puede ser futura.
  - `fechaCompra` no puede ser futura.
  - `fechaCompra` no puede ser anterior a `fechaFabricacion`.
  - `costoAdquisicion` debe ser mayor a 0.
  - `pesoMaximoLingotera` es opcional.
  - Si `pesoMaximoLingotera` se completa, debe ser mayor a 0.
- Enviar fechas como strings compatibles con inputs `date` y serializacion JSON del backend.
- No enviar `idMaquina` ni `idGym` en los bodies.
- No inventar campos ni cambiar nombres de DTOs.
- Mostrar errores reales del backend cuando esten disponibles usando el helper de errores existente.

### Test Plan

- Verificar que `/gimnasio/maquinas` deja de mostrar el placeholder y carga la pantalla real.
- Verificar listado con datos existentes y estado vacio.
- Verificar seleccion de fila y carga correcta del formulario.
- Verificar alta exitosa, actualizacion de grilla y seleccion del nuevo registro.
- Verificar edicion exitosa y permanencia del registro seleccionado.
- Verificar eliminacion con modal, remocion de grilla y limpieza del formulario.
- Verificar validaciones frontend para nombre, fechas, costo y peso.
- Verificar visibilidad de botones segun permisos `CREAR_MAQUINA`, `EDITAR_MAQUINA`, `ELIMINAR_MAQUINA`.
- Ejecutar TypeScript/build del frontend si el entorno lo permite.

### Assumptions

- El backend mantiene camelCase en JSON.
- El `apiClient` ya agrega `Authorization` y `X-Gym-Id`, por lo que no se debe duplicar logica de headers.
- La implementacion debe dejar un `IMPLEMENTATION_LOG_maquinas-plan.md` en la misma carpeta del plan, documentando archivos tocados, decisiones, integracion, validaciones, estados y limitaciones.

---

## 3. Contexto

Leer antes de implementar:

- AGENTS.md
- frontend-skill.md
- MaquinaController.cs
- MaquinaDto.cs
- MaquinaInsertDto.cs
- MaquinaUpdateDto.cs

---

## 4. Reglas y Restricciones (Constraints / Guardrails)

- No mostrar, cargar ni enviar datos del usuario autenticado en el formulario de Máquinas.
- Trabajar solo en el frontend.
- No modificar backend.
- No cambiar endpoints.
- No cambiar nombres de DTOs del backend.
- No hardcodear URLs completas.
- No inventar campos que no existan en los DTOs.
- No usar `window.confirm`; usar modal visual.
- No mostrar ni enviar campos de socio.
- No enviar enums como número; enviarlos como string.
- Mantener consistencia visual con el resto de pantallas ya implementadas.
- Permisos para botones de acción:
  - En modo alta, mostrar **Crear** si tiene `CREAR_MAQUINA`.
  - En modo edición, mostrar **Guardar** si tiene `EDITAR_MAQUINA`.
  - Botón **Eliminar**: `ELIMINAR_MAQUINA`.

- Los permisos frontend deben compararse contra los permisos guardados en `localStorage`, usando la lógica/helper existente en el proyecto.
- No usar las policies backend como códigos de permiso frontend.

---

## 5. Formato de Salida

Además de implementar el código solicitado, generar:

`IMPLEMENTATION_LOG_maquinas-plan.md`

El archivo debe crearse en la misma carpeta/ruta donde se encuentra este plan `.md`.

El log debe documentar brevemente:

- qué archivos fueron creados o modificados
- qué decisiones importantes se tomaron
- cómo se resolvió la integración frontend/backend
- validaciones implementadas
- manejo de estados/loading/errors
- configuración relevante de Axios/interceptors/context/hooks si aplica
- posibles TODOs o limitaciones detectadas

El objetivo es que futuras iteraciones de IA puedan entender rápidamente qué fue implementado y por qué.
