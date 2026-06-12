# Etapa 4 Parte 1 Gestionar Gimnasio - Menú Principal

## 1. El Rol y la Persona

Ponte en el rol de un desarrollador frontend senior especializado en React, TypeScript, Vite, React Router y Axios.

Tu tarea es trabajar sobre el frontend de **MindFit Intelligence**, respetando la arquitectura existente del proyecto y manteniendo un estilo de código simple, claro y mantenible.

Actuá con criterio incremental:

- analizá primero la implementación actual
- reutilizá componentes, hooks, helpers y servicios existentes cuando sea posible
- evitá duplicar lógica
- extraé lógica común cuando sea necesario
- no hagas refactors grandes innecesarios
- no modifiques backend

Antes de implementar, leé:

- `AGENTS.md`
- `frontend-skill.md`
- `src/routes/AppRouter.tsx`
- `src/layouts/AppLayout.tsx`
- `src/pages/PlaceholderPage.tsx`
- `src/utils/navigationPermissions.ts`
- `src/hooks/useInicioData.ts`
- `src/services/formulariosService.ts`
- `src/utils/authStorage.ts`
- la implementación actual del botón `Gestionar gimnasio` en Inicio/Dashboard

---

## 2. La Tarea Principal

Implementar el menú principal del módulo **Gestionar Gimnasio** en la ruta:

```txt
/gimnasio
```

Actualmente esa ruta usa:

```tsx
<Route
  path="/gimnasio"
  element={<PlaceholderPage title="Gestionar gimnasio" />}
/>
```

Ese placeholder debe reemplazarse por una pantalla real de menú, visualmente similar al prototipo adjunto.

El menú debe mostrar botones horizontales de navegación para las siguientes secciones:

- Usuarios
- Permisos
- Equipamientos
- Máquinas
- Ejercicios
- Rangos Horarios

No agregar el botón **Negocio**.

Cada botón visible debe navegar a su ruta correspondiente, aunque por ahora la pantalla destino muestre un placeholder consistente.

---

## 3. El Contexto

El módulo **Gestionar Gimnasio** representa el menú administrativo del gimnasio.

El usuario llega a este módulo desde el botón de navegación principal:

```html
<a class="navigation-link" href="/gimnasio" data-discover="true">
  <span class="navigation-link__number">03</span>
  <span class="navigation-link__content">
    <strong>Gestionar gimnasio</strong>
    <small>Configura equipo, personal y operacion.</small>
  </span>
  <span class="navigation-link__arrow" aria-hidden="true">&gt;</span>
</a>
```

Hoy el frontend ya tiene una lógica de visibilidad para la navegación principal en:

```txt
src/utils/navigationPermissions.ts
```

Pero esa lógica está pensada para estos items principales:

```ts
key: "rutinas" | "socios" | "gimnasio";
```

Por eso, para este módulo no se debe copiar esa lógica de forma rígida. Se debe reutilizar la idea de comparación de permisos y, si hace falta, extraer una función más genérica que sirva tanto para la navegación principal como para submenús internos.

La pantalla debe integrarse visualmente con el layout actual. No inventar datos visuales que hoy no existen. Por ejemplo, `AppLayout.tsx` actualmente muestra marca, botón `Validar Ingreso` si corresponde, `Gym {id}` y `Cerrar sesion`. No agregar nombre de usuario ni rol salvo que ya estén disponibles y sea simple hacerlo sin romper el layout.

---

## 4. Reglas y Restricciones

### 4.1 Restricciones generales

- Trabajar únicamente en `/Frontend`.
- No modificar backend.
- No cambiar endpoints.
- No cambiar DTOs.
- No cambiar contratos de autenticación.
- No borrar archivos sin necesidad.
- No agregar dependencias nuevas salvo que sea estrictamente necesario.
- Mantener la estructura actual del proyecto.
- Usar componentes funcionales y TypeScript.
- Mantener nombres claros y simples.
- Evitar sobreingeniería.

---

### 4.2 Diseño visual

La nueva pantalla debe respetar el estilo actual del frontend y el lineamiento visual de MindFit Intelligence:

- moderno
- limpio
- operativo
- confiable
- orientado a gestión de gimnasio
- sin saturar de colores
- sin dashboards genéricos innecesarios

La barra horizontal debe parecer un menú de acceso rápido del módulo, similar al prototipo adjunto.

No hace falta que sea una copia exacta del prototipo, pero debe respetar su idea visual:

- botones alineados horizontalmente en desktop
- íconos arriba
- texto debajo
- buen espaciado
- fondo claro
- lectura simple

En mobile o pantallas angostas, el menú puede resolverse con scroll horizontal para evitar romper el layout.

Los íconos pueden implementarse con SVG inline, caracteres visuales simples o estilos locales. No agregar una librería de íconos solo para esta tarea.

Mantener consistencia con los textos actuales del proyecto. Si el frontend viene usando textos sin tildes por consistencia, respetar ese criterio.

---

### 4.3 Botones del menú horizontal

El menú debe incluir estas opciones:

#### Usuarios

Ruta:

```txt
/gimnasio/usuarios
```

Permisos asociados:

```txt
CREAR_USUARIO_RESPONSABLE
EDITAR_USUARIO_RESPONSABLE
ELIMINAR_USUARIO_RESPONSABLE_DEFINITIVAMENTE
```

---

#### Permisos

Ruta:

```txt
/gimnasio/permisos
```

Permisos asociados:

```txt
CREAR_GRUPO
EDITAR_GRUPO
ELIMINAR_GRUPO
```

---

#### Equipamientos

Ruta:

```txt
/gimnasio/equipamientos
```

Permisos asociados:

```txt
CREAR_EQUIPAMIENTO
EDITAR_EQUIPAMIENTO
ELIMINAR_EQUIPAMIENTO
```

---

#### Máquinas

Ruta:

```txt
/gimnasio/maquinas
```

Permisos asociados:

```txt
CREAR_MAQUINA
EDITAR_MAQUINA
ELIMINAR_MAQUINA
```

---

#### Ejercicios

Ruta:

```txt
/gimnasio/ejercicios
```

Permisos asociados:

```txt
CREAR_EJERCICIO
EDITAR_EJERCICIO
ELIMINAR_EJERCICIO
```

---

#### Rangos Horarios

Ruta:

```txt
/gimnasio/rangos-horarios
```

Permisos asociados:

```txt
MODIFICAR_DIA_RH
QUITAR_ENTRENADOR_DIA_RH
```

---

### 4.4 No agregar botón Negocio

Aunque el prototipo muestre un botón llamado **Negocio**, no debe agregarse en esta implementación.

De todas formas, la estructura del menú debe quedar preparada para poder agregar nuevas opciones en el futuro sin romper nada.

Para eso, crear una configuración reutilizable del menú, por ejemplo:

```ts
const gimnasioMenuItems = [
  {
    key: "usuarios",
    label: "Usuarios",
    path: "/gimnasio/usuarios",
    permissionCodes: [
      "CREAR_USUARIO_RESPONSABLE",
      "EDITAR_USUARIO_RESPONSABLE",
      "ELIMINAR_USUARIO_RESPONSABLE_DEFINITIVAMENTE",
    ],
  },
];
```

Adaptar el código final a la estructura real del proyecto.

---

### 4.5 Visibilidad por permisos

Los botones del menú horizontal deben tratarse como **botones de navegación**, no como botones de acción.

Recordatorio de la regla del proyecto:

#### Botones de Navegación

Endpoint:

```txt
GET /api/Formulario
```

Servicio existente:

```txt
src/services/formulariosService.ts
```

Comportamiento esperado:

- el frontend trae el catálogo de formularios con sus permisos asociados
- cada botón de navegación tiene un conjunto de permisos configurados en frontend
- el sistema compara los permisos del usuario obtenidos desde la sesión hidratada de `useAuth`
- el botón se muestra si existe al menos un permiso en común entre:
  - permisos del usuario
  - permisos configurados para ese botón
  - permisos presentes en algún formulario del catálogo backend

No mapear la visibilidad por nombre de formulario.

La implementación actual de `navigationPermissions.ts` resuelve la visibilidad por intersección de permisos. Mantener ese criterio.

Es decir, para cada item:

1. tomar sus `permissionCodes`
2. buscar en `formularios` permisos compatibles con esos códigos
3. verificar si el usuario tiene al menos uno de esos permisos compatibles
4. mostrar el item solo si la comparación da true

#### Botones de Acción

No aplicar lógica de botón de acción para este menú.

Los botones de acción comparan contra un permiso específico, pero este menú corresponde a navegación interna del módulo.

---

### 4.6 Reutilización y extracción de lógica

Hoy existe:

```txt
src/utils/navigationPermissions.ts
```

pero su tipo actual está limitado a:

```ts
key: "rutinas" | "socios" | "gimnasio";
```

Para este desarrollo, no duplicar la lógica copiándola en otro archivo de forma aislada.

Se debe hacer una de estas opciones:

#### Opción recomendada

Extraer una función genérica reutilizable, por ejemplo:

```ts
export interface PermissionNavigationItem<TKey extends string = string> {
  key: TKey;
  label: string;
  path: string;
  permissionCodes: readonly string[];
}

export function getVisiblePermissionNavigationItems<
  TItem extends PermissionNavigationItem,
>(
  items: readonly TItem[],
  userPermissions: readonly string[],
  formularios: readonly Formulario[],
): TItem[] {
  // misma lógica de intersección que hoy usa getVisibleNavigationItems
}
```

Luego:

- `navigationItems` sigue existiendo para Inicio/Dashboard
- `gimnasioMenuItems` usa la misma función genérica
- no se rompe la navegación actual

#### Opción alternativa

Crear un helper nuevo con lógica genérica y dejar `navigationPermissions.ts` como wrapper de compatibilidad.

Lo importante es no dejar dos lógicas de permisos inconsistentes.

---

### 4.7 Carga de formularios

No reutilizar `useInicioData` completo para `/gimnasio`, porque ese hook mezcla:

- carga de formularios
- carga de turnos del dashboard

Para `/gimnasio`, reutilizar el servicio existente:

```txt
src/services/formulariosService.ts
```

Crear un hook específico si hace falta, por ejemplo:

```txt
src/hooks/useGimnasioMenu.ts
```

Ese hook debería encargarse solo de:

- cargar formularios
- leer permisos de sesión desde `useAuth` o desde la fuente ya usada por el proyecto
- calcular opciones visibles
- exponer loading/error/items visibles

---

### 4.8 Estados de pantalla

La pantalla debe contemplar estos estados:

#### Cargando opciones

Mientras se determina qué opciones puede ver el usuario:

```txt
Cargando opciones del modulo...
```

#### Error de carga

Si falla la carga de formularios, mostrar un mensaje claro y consistente con el resto del frontend.

Se puede reutilizar `getApiErrorMessage` o el helper de errores existente si corresponde.

Usar como fallback:

````txt
No pudimos cargar las opciones del modulo. Intenta nuevamente.

#### Sin permisos

Si el usuario no tiene permisos para ninguna opción del módulo:

```txt
No tenes permisos disponibles para gestionar opciones del gimnasio.
````

También mostrar una acción para volver a Inicio:

```txt
Volver a Inicio
```

#### Con opciones disponibles

Mostrar solamente los botones habilitados por permisos.

---

### 4.9 Rutas internas iniciales

Crear o preparar estas rutas:

```txt
/gimnasio/usuarios
/gimnasio/permisos
/gimnasio/equipamientos
/gimnasio/maquinas
/gimnasio/ejercicios
/gimnasio/rangos-horarios
```

Por ahora, si esas pantallas todavía no están implementadas, pueden mostrar un placeholder consistente:

```txt
Modulo en preparacion
```

Pero no dejar los botones sin navegación.

El placeholder de estas rutas internas puede reutilizar `PlaceholderPage` si eso mantiene consistencia, o un placeholder propio dentro del layout de gimnasio si resulta más claro.

---

### 4.10 Archivos sugeridos

La implementación puede crear o modificar archivos similares a estos, según la estructura real del proyecto:

```txt
src/pages/GimnasioPage.tsx
src/components/gimnasio/GimnasioMenu.tsx
src/components/gimnasio/GimnasioMenuItem.tsx
src/components/gimnasio/gimnasioMenuConfig.ts
src/hooks/useGimnasioMenu.ts
src/utils/navigationPermissions.ts
src/routes/AppRouter.tsx
```

No es obligatorio usar exactamente esos nombres si el proyecto ya tiene otra convención.

---

### 4.11 Criterios de aceptación

La implementación se considera correcta cuando:

- al hacer clic en **Gestionar gimnasio**, se navega a `/gimnasio`
- `/gimnasio` ya no muestra el placeholder anterior
- se muestra un menú horizontal similar al prototipo adjunto
- aparecen solamente las opciones permitidas según permisos del usuario
- no aparece el botón **Negocio**
- cada botón visible navega a su ruta correspondiente
- las rutas internas muestran al menos un placeholder consistente
- en mobile el menú no rompe el layout
- la barra queda preparada para agregar nuevas opciones futuras desde una config
- no se modifica backend
- no se rompen rutas existentes
- no se duplica lógica de permisos
- no se reutiliza `useInicioData` completo para este módulo
- no se agrega una librería de íconos innecesaria
- no quedan errores de TypeScript
- el diseño se mantiene coherente con el resto del frontend

---

## 5. Formato de Salida

Al finalizar la implementación, crear o actualizar el archivo:

```txt
IMPLEMENTATION_LOG_menu-gimnasio-plan.md
```

El log debe incluir:

```md
# Implementation Log - Gestionar Gimnasio Menu Principal

## Archivos creados

- ...

## Archivos modificados

- ...

## Cambios realizados

- ...

## Rutas agregadas o ajustadas

- ...

## Manejo de permisos

- ...

## Decisiones técnicas

- ...

## Pendientes o limitaciones

- ...
```

Además, responder con un resumen breve indicando:

- qué se implementó
- qué archivos se tocaron
- cómo se resolvió la visibilidad por permisos
- si quedó algún pendiente
