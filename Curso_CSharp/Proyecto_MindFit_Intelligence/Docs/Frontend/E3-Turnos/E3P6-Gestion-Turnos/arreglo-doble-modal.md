Hay un bug.

Al navegar a:

`/socios/:idUsuario/turnos`

se abre correctamente el modal de Gestión de Turnos, pero también se abre el modal de Consultar Socio por detrás.

Revisar la lógica de renderizado de modales en `SociosPage.tsx`.

El modal de Consultar Socio debe abrirse únicamente cuando la ruta sea:

`/socios/:idUsuario/consultar`

y no cuando la ruta sea:

`/socios/:idUsuario/turnos`

Corregir la detección de rutas para que cada modal se renderice solamente en su ruta correspondiente.
