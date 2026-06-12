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
