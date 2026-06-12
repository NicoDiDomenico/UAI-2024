## Ajuste requerido - Modal de baja lógica y mensajes reales del backend

Modificar la acción **Eliminar** de la pantalla `Ver Socios`.

Actualmente, cuando el endpoint falla, el frontend muestra un mensaje genérico como:

"No pudimos cargar la lista de socios. Revisa la conexión con el backend e intenta nuevamente."

Pero el backend puede devolver errores útiles, por ejemplo:

HTTP 409

```json
["No se puede eliminar el socio con la cuota al día."]
```

Se debe mostrar ese mensaje real al usuario.

---

## Comportamiento requerido

Al hacer clic en **Eliminar**:

1. No usar `window.confirm`.

2. Abrir un modal visual consistente con el diseño actual del frontend.

3. El modal debe mostrar:
   - título: `Confirmar eliminación`
   - nombre del socio seleccionado
   - descripción de la acción que se va a realizar
   - botón `Cancelar`
   - botón `Confirmar eliminación`

4. Al confirmar:
   - llamar a `PATCH /api/Usuario/socio/{idUsuario}/baja`
   - mostrar loading dentro del modal mientras se ejecuta la solicitud
   - evitar múltiples clics durante la ejecución

5. Si la baja es exitosa:
   - mantener el modal abierto
   - mostrar mensaje de éxito dentro del modal
   - actualizar la grilla
   - permitir cerrar el modal mediante un botón `Cerrar`

6. Si la baja falla:
   - mantener el modal abierto
   - mostrar dentro del modal el mensaje real devuelto por el backend
   - no modificar la grilla
   - permitir volver a intentar o cerrar el modal

---

## Manejo de errores requerido

Crear o ajustar una función utilitaria para extraer mensajes de error de Axios.

Debe soportar respuestas como:

```json
["No se puede eliminar el socio con la cuota al día."]
```

```json
{
  "message": "Mensaje de error"
}
```

```json
{
  "errors": ["Error 1", "Error 2"]
}
```

```json
{
  "title": "Error de validación"
}
```

La función debe priorizar siempre el mensaje devuelto por el backend.

Solo mostrar mensajes genéricos cuando no exista ningún mensaje útil en la respuesta.

---

## Importante

- No reutilizar el mensaje de error utilizado para la carga de socios.
- No cerrar automáticamente el modal cuando ocurre un error.
- No usar `alert`.
- No usar `window.confirm`.
- Mantener una experiencia consistente con los demás modales del sistema.

---

## Resultado esperado

- El botón **Eliminar** abre un modal visual.
- El usuario confirma la acción desde el modal.
- Los errores del backend se muestran exactamente como fueron devueltos cuando sea posible.
- El éxito también se muestra dentro del modal.
- La experiencia completa ocurre dentro del modal sin diálogos nativos del navegador.
- Ejecutar `npm run build`.
- Actualizar `IMPLEMENTATION_LOG_eliminar-socio-plan.md`.
