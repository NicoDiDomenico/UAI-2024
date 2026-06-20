Quiero implementar un footer responsive para las rutas públicas:

- `/`
- `/funcionalidades`
- `/precios`
- `/testimonios`
- `/blog`
- `/contacto`

Usá como referencia visual la imagen adjunta:

`footer-referencia.jpg`

La imagen es solo referencia de estilo y estructura. No copiar los textos originales.

Reemplazos de contenido:

- Cambiar el título `Mystery Code` por `MindFit Intelligence`

- Cambiar el párrafo central por:

  `MindFit Intelligence es una plataforma para gimnasios que integra gestión, turnos, socios y herramientas inteligentes para mejorar la experiencia de entrenamiento como nunca antes se ha visto.`

- Cambiar los botones de navegación por:
  - `Inicio` → `/`
  - `Funcionalidades` → `/funcionalidades`
  - `Precios` → `/precios`
  - `Testimonios` → `/testimonios`
  - `Blog` → `/blog`
  - `Contacto` → `/contacto`

- Cambiar el texto inferior por:

  `© 2026 MindFit Intelligence. Todos los derechos reservados.`

Diseño esperado:

- Footer oscuro, simple y moderno.
- Título centrado.
- Descripción breve centrada.
- Íconos/redes sociales centrados debajo del texto.
- Links de navegación en forma de botones/píldoras, similares a la referencia.
- Texto legal inferior separado visualmente.
- Diseño responsive para desktop y mobile.
- Mantener consistencia visual con la landing pública de MindFit.

Reglas:

- Trabajar solo dentro de `/Frontend`.
- No modificar `/Backend`.
- No consumir endpoints.
- No hardcodear URLs completas.
- Usar `Link` de `react-router-dom` para la navegación interna.
- Crear un componente reutilizable, por ejemplo `PublicFooter.tsx`.
- Usar estilos encapsulados para evitar afectar otras pantallas.
- Integrar el footer únicamente en las páginas públicas indicadas.
- Crear un log que registre los implementado en la misma ruta que este .md
