# Correccion 1

Necesito corregir una navegación del header luego de implementar el módulo de Socio.

Problema detectado:

Cuando estoy logueado como Socio y estoy en:

```txt
http://localhost:5173/socio/inicio
```

el elemento del header:

```html
<a class="workspace-brand" aria-label="Ir a Inicio" href="/dashboard">
  <span class="workspace-brand__mark">MF</span>
  <span>
    <strong>MindFit</strong>
    <small>Intelligence</small>
  </span>
</a>
```

sigue apuntando a:

```txt
/dashboard
```

Entonces, al hacer clic en el logo/marca, me lleva incorrectamente a:

```txt
http://localhost:5173/dashboard
```

Comportamiento esperado:

- Si el usuario autenticado tiene rol `"Socio"` en `session.datosPersonales.rol`, el link del header debe apuntar a:

```txt
/socio/inicio
```

- Si el usuario autenticado no tiene rol `"Socio"`, el link debe seguir apuntando a:

```txt
/dashboard
```

Archivos a revisar:

```txt
Frontend/src/layouts/AppLayout.tsx
Frontend/src/hooks/useAuth.ts
Frontend/src/utils/authRoles.ts
```

Si el link `workspace-brand` no está en `AppLayout.tsx`, buscar en el frontend dónde se renderiza esa clase y aplicar la corrección ahí.

Usar la misma lógica/helper de rol Socio que ya se implementó para el redireccionamiento post-login, por ejemplo `isSocioRole(...)`, para no duplicar la comparación del rol.

No modificar backend.

No cambiar el comportamiento de `/dashboard`.

No cambiar el comportamiento de `/socio/inicio`.

Solo corregir el destino del link del header según el rol del usuario autenticado.

Al finalizar:

- Ejecutar `npm run build`.
- Actualizar `IMPLEMENTATION_LOG_modulo-socios.md` indicando esta corrección.
- Indicar qué archivo fue modificado.

# Correccion 2

Necesito corregir la ubicación visual de los botones en la pantalla del Socio.

Pantalla afectada:

```txt
Frontend/src/pages/socio/SocioInicioPage.tsx
```

Actualmente los botones:

```html
<div class="gestionar-turnos-modal__actions socio-inicio-actions">
  <button class="submit-button consultar-footer__save" type="button">
    Nuevo Turno
  </button>
  <button
    class="ghost-button consultar-footer__close gestionar-turnos-modal__cancel"
    type="button"
    disabled
  >
    Cancelar Turno
  </button>
</div>
```

se están mostrando arriba de la grilla, al mismo nivel visual que el título/subtítulo.

Comportamiento esperado:

- Los botones `Nuevo Turno` y `Cancelar Turno` deben mostrarse debajo de la grilla de turnos.
- Mantener el orden:
  - `Nuevo Turno`
  - `Cancelar Turno`

- Mantener la lógica actual:
  - `Nuevo Turno` habilitado sin necesidad de seleccionar turno.
  - `Cancelar Turno` deshabilitado si no hay turno seleccionado.
  - `Cancelar Turno` habilitado cuando hay turno seleccionado.

- No modificar la lógica de carga de turnos.
- No modificar endpoints.
- No modificar backend.
- Mantener estilos consistentes con la pantalla actual.

Archivos a revisar:

```txt
Frontend/src/pages/socio/SocioInicioPage.tsx
Frontend/src/App.css
```

La corrección debería ser principalmente de estructura JSX y, solo si hace falta, de CSS.

Al finalizar:

- Ejecutar `npm run build`.
- Actualizar `IMPLEMENTATION_LOG_modulo-socios.md` indicando esta corrección visual.
