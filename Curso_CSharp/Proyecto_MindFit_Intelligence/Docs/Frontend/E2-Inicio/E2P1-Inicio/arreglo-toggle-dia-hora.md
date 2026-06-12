Necesito ajustar la mejora que hiciste en `TurnosGrid`.

Cambios requeridos:

1. Selector Día/Hora

Actualmente el `turnos-view-toggle` cambia a Día solo si hago click en "Dia" y cambia a Hora solo si hago click en "Hora".

Quiero que funcione como un switch real:

- si la vista actual es `Día`, al hacer click en cualquier parte del selector debe cambiar a `Hora`
- si la vista actual es `Hora`, al hacer click en cualquier parte del selector debe cambiar a `Día`

Es decir, el usuario no debería tener que acertar exactamente sobre el texto/opción.

2. Texto con acento

Cambiar todos los textos visibles de `Dia` a `Día`.

Debe verse:

- `Día`
- `Hora`

3. Contador de registros

El contador:

<span class="section-count">2 registrados</span>

debe reflejar la cantidad de registros visibles según la vista seleccionada.

Reglas:

- En vista `Día`, mostrar la cantidad total de turnos del día.
- En vista `Hora`, mostrar la cantidad de turnos filtrados para la hora actual.

Ejemplos:

- Si vista `Día` tiene 2 turnos → `2 registrados`
- Si vista `Hora` tiene 0 turnos → `0 registrados`
- Si vista `Hora` tiene 1 turno → `1 registrado`

También cuidar singular/plural:

- `1 registrado`
- `0 registrados`
- `2 registrados`

No modificar backend.
No crear nuevos endpoints.
No cambiar la lógica de carga de datos.
Solo ajustar el comportamiento del selector, el texto visible y el contador.
Actualizar `IMPLEMENTATION_LOG_inicio-plan.md` al finalizar.
