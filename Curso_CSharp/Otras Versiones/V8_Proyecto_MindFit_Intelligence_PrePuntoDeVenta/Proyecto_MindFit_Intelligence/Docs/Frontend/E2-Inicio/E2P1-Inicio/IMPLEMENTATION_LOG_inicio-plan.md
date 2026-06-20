# Implementation Log - Etapa 2 Inicio

## Archivos creados

- `Frontend/src/pages/InicioPage.tsx`
- `Frontend/src/pages/PlaceholderPage.tsx`
- `Frontend/src/layouts/AppLayout.tsx`
- `Frontend/src/components/inicio/NavigationPanel.tsx`
- `Frontend/src/components/inicio/TurnosGrid.tsx`
- `Frontend/src/hooks/useInicioData.ts`
- `Frontend/src/services/formulariosService.ts`
- `Frontend/src/services/turnosService.ts`
- `Frontend/src/types/formulario.ts`
- `Frontend/src/types/turno.ts`
- `Frontend/src/utils/date.ts`
- `Frontend/src/utils/navigationPermissions.ts`

## Archivos modificados

- `Frontend/src/routes/AppRouter.tsx`
- `Frontend/src/utils/apiError.ts`
- `Frontend/src/App.css`

## Decisiones tecnicas

- Se mantuvo `/dashboard` como ruta de entrada posterior al login para no alterar el flujo de autenticacion existente, pero ahora renderiza `InicioPage`.
- Se creo `AppLayout` para reutilizar encabezado, identificacion del gym activo y cierre de sesion en Inicio y futuros modulos.
- La navegacion se centralizo en `navigationPermissions.ts`. Cada acceso identifica formularios compatibles por los permisos asociados indicados en el plan y solo se muestra si uno de esos permisos existe tanto en `GET /Formulario` como en la sesion actual.
- No se agregaron dependencias ni se modifico backend.
- Los placeholders reutilizan un unico componente y no incluyen logica de negocio ni llamadas HTTP.

## Integracion frontend/backend

- `formulariosService.getAll()` consume `GET /Formulario` mediante el `apiClient` centralizado.
- `turnosService.getInicioGridByDate(fecha)` consume `GET /Turno/inicio/grilla-fecha?fecha=yyyy-mm-dd`.
- Los tipos `Formulario` y `TurnoDetalle` replican las propiedades camelCase de `FormularioDto` y `TurnoDetalleDto`.
- El interceptor existente continua agregando `Authorization` y `X-Gym-Id` desde la sesion persistida.
- La fecha se arma con componentes locales del navegador para evitar desplazamientos de dia por conversion UTC.

## Estados y validaciones

- Catalogo de formularios y grilla de turnos cargan de forma independiente para que un error no bloquee toda la pantalla.
- Cada recurso tiene estado de loading y mensaje de error propio.
- El `404` del endpoint de turnos se interpreta como estado vacio porque el backend lo usa cuando no hay turnos para la fecha.
- Si no hay turnos se muestra un mensaje vacio.
- Si no hay accesos compatibles se informa que no existen secciones habilitadas.
- La grilla muestra hora, socio, entrenador, cupos y estado, y cambia a formato apilado en pantallas pequenas.

## Rutas placeholder

- `/rutinas`
- `/socios`
- `/gimnasio`

Todas las rutas nuevas permanecen dentro de `ProtectedRoute`.

## Verificacion

- `npm.cmd run build`: exitoso.
- `npm.cmd run lint`: exitoso.

## TODOs y limitaciones

- El backend no documenta nombres concretos de formularios para los tres accesos principales. La asociacion se resuelve con los codigos de permiso del plan y el catalogo real devuelto por `GET /Formulario`, sin inventar nombres.
- Las rutas placeholder estan protegidas por autenticacion, pero todavia no bloquean acceso directo por permiso; esta etapa solo exige visibilidad permission-based de los botones.
- `DashboardPage.tsx` se conserva sin uso para evitar eliminar archivos sin aprobacion. Puede retirarse en una futura limpieza autorizada.

## Actualizacion - Mejora grilla Turnos del dia

### Archivos modificados

- `Frontend/src/components/inicio/TurnosGrid.tsx`
- `Frontend/src/App.css`
- `Docs/Frontend/E2-Inicio/E2P1-Inicio/IMPLEMENTATION_LOG_inicio-plan.md`

### Vista Dia/Hora

- Se agrego un selector tipo capsula `Dia/Hora` en la cabecera de `Turnos del dia`.
- La vista `Dia` queda seleccionada por defecto y reutiliza la grilla existente con las columnas `Hora`, `Socio`, `Entrenador`, `Cupos` y `Estado`.
- La vista `Hora` usa una tabla compacta con las columnas `Socio`, `Entrenador` y `Estado`, mas un resumen superior con `Hora` y `Cupos`.

### Filtrado por hora actual

- La vista `Hora` usa la hora del sistema del navegador y la normaliza al bloque en punto `HH:00`.
- Los registros se filtran en frontend comparando ese bloque contra `turno.hora`, sin hacer una nueva llamada al backend.
- Si hay turnos para la hora actual, el resumen muestra el primer valor disponible de `cupos`, manteniendo la UI operativa aunque hubiera valores distintos para el mismo bloque.

### Mejoras visuales

- Se ajusto la cabecera de la seccion para alinear titulo, selector y contador de forma responsive.
- Se mejoro contraste y legibilidad de la tabla de turnos.
- Se agregaron chips de estado diferenciados: `Cancelado` con color suave de error, `Finalizado` con color suave de exito y estados restantes con estilo neutro.
- Se agrego una barra de progreso sutil para `cupos` cuando el formato se puede interpretar como `actual/total`.

### Estados

- La vista `Dia` mantiene loading, error y estado vacio existente.
- La vista `Hora` mantiene loading y error compartidos con la carga diaria, y agrega el estado vacio `No hay turnos registrados para la hora actual.`.

### TODOs y limitaciones

- La vista `Hora` depende del reloj del navegador y actualiza el bloque horario cada minuto mientras el componente esta montado.
- Si `turno.hora` llegara con un formato no horario, se conserva el valor original para evitar bloquear la UI, pero no matchearia contra el bloque `HH:00`.

## Actualizacion - Ajuste toggle Dia/Hora

### Archivos modificados

- `Frontend/src/components/inicio/TurnosGrid.tsx`
- `Frontend/src/App.css`
- `Docs/Frontend/E2-Inicio/E2P1-Inicio/IMPLEMENTATION_LOG_inicio-plan.md`

### Cambios realizados

- El selector `Dia/Hora` ahora funciona como un switch real: cualquier click dentro de la capsula alterna entre `Dia` y `Hora`.
- Se corrigieron los textos visibles de `Dia` a `Día`, incluyendo el titulo y el estado de carga de la seccion.
- El contador de registros ahora refleja la cantidad visible segun la vista activa.
- Se agrego singular/plural para el contador: `1 registrado` y `0/2 registrados`.
