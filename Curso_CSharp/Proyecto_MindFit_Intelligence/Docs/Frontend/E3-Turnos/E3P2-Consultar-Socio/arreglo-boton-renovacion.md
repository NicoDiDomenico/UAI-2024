# Ajuste Facturación - Renovar Cuota

## 1. Rol y Persona

Ponte en el rol de un desarrollador frontend.

---

## 2. Tarea Principal

Ajustar el comportamiento visual de la sección **Facturación** dentro del modal **Consultar Socio**.

Actualmente, al presionar el botón **Renovar Cuota**, se muestra correctamente la sección de renovación, pero el botón **Renovar Cuota** continúa visible.

El comportamiento esperado es que el botón **Renovar Cuota** y la sección de renovación sean mutuamente excluyentes.

### Estado Inicial

Al ingresar a la pestaña Facturación:

- Mostrar botón **Renovar Cuota**.
- Ocultar la sección de renovación.

### Al presionar Renovar Cuota

Cuando el usuario presiona:

```html
<button class="submit-button consultar-button--green" type="button">
  Renovar Cuota
</button>
```

el frontend debe:

- Ocultar el botón **Renovar Cuota**.
- Mostrar la sección de renovación.
- Mantener visibles los campos:
  - Plan
  - Monto

- Mostrar el botón:
  - Cancelar renovación

### Al presionar Cancelar renovación

Cuando el usuario presiona:

```html
<button class="ghost-button consultar-renewal__cancel" type="button">
  Cancelar renovación
</button>
```

el frontend debe:

- Ocultar la sección de renovación.
- Volver a mostrar el botón **Renovar Cuota**.
- Limpiar los valores ingresados para la renovación si corresponde.
- Restablecer el estado local de renovación.

### Reglas Importantes

- No modificar endpoints existentes.
- No modificar DTOs existentes.
- No modificar lógica backend.
- No alterar el flujo de Confirmar Cambios.
- La renovación debe continuar enviándose únicamente cuando el usuario presiona Confirmar Cambios.
- Mantener la validación existente:
  - Si se renueva cuota, debe existir un plan seleccionado.
  - Si se renueva cuota, el monto debe ser mayor a cero.

- Mantener compatibilidad con la implementación actual del modal Consultar Socio.

---

## 3. Contexto

Según la implementación actual:

- `Frontend/src/components/socios/ConsultarSocioModal.tsx`
- `Frontend/src/App.css`

La lógica de renovación ya existe y actualmente:

- Permite seleccionar Plan.
- Permite ingresar Monto.
- Permite cancelar la renovación.
- Construye correctamente:

```json
{
  "renueva": true,
  "plan": "Mensual",
  "monto": 0
}
```

El ajuste solicitado es únicamente visual y de estado local.

---

## 4. Formato de Salida

Además de implementar el cambio solicitado:

- Actualizar `IMPLEMENTATION_LOG_consultar-modificar-borrar-plan.md`.

Agregar una breve descripción indicando:

- Qué archivo fue modificado.
- Cómo se resolvió la visibilidad de Renovar Cuota.
- Cómo se resolvió la cancelación de la renovación.
- Confirmación de que el build continúa funcionando correctamente.

---

## 5. Verificación

Ejecutar:

```bash
npm run build
```

Verificar:

- Sin errores TypeScript.
- Sin errores de compilación Vite.
- Renovar Cuota oculto cuando la renovación está activa.
- Renovar Cuota visible nuevamente al cancelar la renovación.
- Confirmar Cambios continúa funcionando correctamente.
