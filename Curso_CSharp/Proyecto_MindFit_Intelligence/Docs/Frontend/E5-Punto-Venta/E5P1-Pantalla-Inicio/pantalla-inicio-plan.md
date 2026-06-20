# Etapa 5 Punto de Venta - Parte 1 - Pantalla de Inicio

## 1. El Rol y la Persona (System / Role Context)

Actúa como un Desarrollador Frontend Senior experto en React, TypeScript y UI/UX, con amplia experiencia en la integración fluida de diseños de Figma mediante herramientas MCP. Tu actitud es metódica, analítica y orientada a la calidad del código. Escribes código limpio, escalable y respetas de manera estricta la arquitectura y los patrones existentes en el proyecto en el que te integras.

## 2. La Tarea Principal (Core Task)

Implementar la pantalla pública inicial (landing page) de MindFit en la ruta raíz `/`.
Tu objetivo final es traducir fielmente el diseño del frame de Figma asignado a código frontend, crear la interfaz visual completa y configurar el botón `Acceso Clientes` para que ejecute una navegación interna directa hacia la ruta `/login`.

## 3. El Contexto (Background)

Esta pantalla corresponde a la primera vista pública de la plataforma MindFit, destinada a usuarios no autenticados que ingresan al sistema.
El diseño base que debes inspeccionar mediante tu conexión MCP se encuentra en: `https://www.figma.com/design/fZNLHzSfSR2k2CllMxkNyz/MindFit?node-id=1-2&t=vQsTWn7uFvcx6vZs-4`

Archivos de conocimiento base obligatorios a revisar antes de iniciar:

- `AGENTS.md` y `frontend-skill.md`
- `Frontend/src/routes/AppRouter.tsx` (Comprender el manejo del `FallbackRoute`)
- `Frontend/src/App.css` y `Frontend/src/index.css`
- Estructura del directorio `Frontend/src/pages`

El diseño busca transmitir una estética moderna, limpia, calma y orientada a la salud. La landing page funcionará de manera aislada del flujo de sesión: no requiere autenticación, no carga datos del usuario autenticado, y debe coexistir perfectamente con el ecosistema actual de rutas y el sistema de login.

## 4. Reglas y Restricciones (Constraints / Guardrails)

- **Límites del entorno:** Trabajar única y exclusivamente dentro del directorio `/Frontend`. No modificar `/Backend`.
- **Red y Datos:** No inventar endpoints, no consumir servicios backend desde esta pantalla, ni crear o modificar DTOs, lógica de Axios o el `authStorage`.
- **Ruteo:** La ruta `/` debe declararse fuera de `ProtectedRoute` y ANTES del fallback `path="*"`. No cambiar el comportamiento de las rutas existentes ni la lógica de `FallbackRoute`; solo agregar la nueva ruta `/`.
- **Navegación:** No utilizar `window.location.href`. Utilizar exclusivamente `<Link to="/login">` (React Router) o en su defecto `navigate('/login')`.
- **Estilos:** Añadir estilos en `Frontend/src/App.css`, encapsulándolos (por ejemplo, bajo una clase contenedora `.landing-page`) para evitar colisiones con otras pantallas de MindFit.
- **Assets:** Extraer assets del diseño mediante Figma MCP si es posible. Si el frame no expone assets descargables, no detener la tarea; implementar una versión visual equivalente. Prohibido hardcodear URLs externas temporales de Figma.
- **Validación:** Antes de dar la tarea por concluida, ejecutar desde `/Frontend`: `npm run build`.

## 5. Formato de Salida (Output Format)

**Archivos esperados:**

- Crear `Frontend/src/pages/LandingPage.tsx`
- Modificar `Frontend/src/routes/AppRouter.tsx`
- Modificar `Frontend/src/App.css`
- Crear `Docs/Frontend/E5-Punto-Venta/E5P1-Pantalla-Inicio/IMPLEMENTATION_LOG_pantalla-inicio-plan.md`

Genera e implementa el código solicitado. Inmediatamente después, genera el archivo de documentación `IMPLEMENTATION_LOG_pantalla-inicio-plan.md` en la ruta especificada en los archivos esperados.

El contenido de este archivo Markdown debe estructurarse con viñetas y debe incluir al menos:

- Lista de archivos creados y/o modificados.
- Resumen de las decisiones de diseño e integración del MCP de Figma.
- Explicación de cómo se resolvió la navegación a `/login`.
- Resultado de las validaciones ejecutadas, indicando si fueron exitosas o si fallaron y por qué.
- Posibles TODOs técnicos o limitaciones detectadas durante la implementación.
