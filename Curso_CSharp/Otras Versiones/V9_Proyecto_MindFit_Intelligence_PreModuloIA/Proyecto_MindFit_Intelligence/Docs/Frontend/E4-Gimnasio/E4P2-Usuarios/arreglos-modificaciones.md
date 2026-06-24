- Apenas se carga la ruta "http://localhost:5173/gimnasio/usuarios" en el componente "<input class="field-input" value="responsable">" y en el "<input class="field-input" type="password" value="Password123!">" se está cargando el nombre de usuario y la contraseña del usuario logueado, esto no puede pasar nunca, esos campos deben estar vacios si no se seleciono ningun responsable de la grilla.
- No quiero que la grilla tenga una barra de desplazamiento horizontal, quiero que todas las columnas con sus registros se vean sin necesidad de hacer scroll horizontal.
- El boton "<button class="ghost-button usuarios-new-button" type="button">Nuevo</button>" lo quiero en la parte inferior izquierda de la grilla.
- En la grilla de responsables no debe mostrarse el usuario actualmente logueado en el sistema. El frontend debe obtener el `id` del usuario autenticado desde la sesión/localStorage (`session.datosPersonales.id` o la estructura existente en `AuthContext`) y filtrar la respuesta de `GET api/Usuario/grilla-responsable` excluyendo cualquier responsable cuyo `idUsuario` coincida con ese ID. Esto evita que el usuario logueado pueda seleccionarse, editarse, eliminarse o cambiarse la contraseña desde esta pantalla administrativa.
- ADemas:

### Filtro de búsqueda en grilla de responsables

Agregar filtros para buscar responsables por columna dentro de la grilla.

La grilla debe permitir filtrar por:

- `Username`
- `NombreCompleto`
- `Email`
- `NombreGrupo`

Comportamiento esperado:

- El filtro debe aplicarse en frontend sobre los datos ya obtenidos desde `GET api/Usuario/grilla-responsable`.
- No crear nuevos endpoints backend.
- Debe existir un selector de columna para elegir por qué campo buscar.
- Debe existir un input de texto para ingresar el valor de búsqueda.
- La búsqueda debe ser case-insensitive.
- Para `NombreGrupo`, buscar dentro de la lista `nombreGrupo`.
- Si el filtro no encuentra resultados, mostrar un estado vacío claro.
- El usuario actualmente logueado debe seguir excluido aunque el filtro cambie.
- Si el responsable seleccionado deja de estar visible por el filtro, limpiar la selección y volver el formulario a modo creación.
