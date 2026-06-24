Implementar una sección Blog accesible desde `/blog`.

La sección no será un blog manual. Debe mostrar automáticamente noticias relacionadas con fitness, entrenamiento, nutrición y bienestar consumiendo uno o más feeds RSS públicos.

Requisitos generales:

- Trabajar únicamente en el frontend.
- No modificar backend.
- Crear una página Blog con diseño consistente con MindFit.
- Mostrar noticias en formato de tarjetas.
- Cada tarjeta debe mostrar:
  - imagen (si existe)
  - título
  - resumen
  - fecha
  - fuente
  - botón "Leer más"
- El botón debe abrir la noticia original en una nueva pestaña.
- Implementar caché simple en localStorage para evitar consultar los RSS en cada visita.
- El servicio debe normalizar y combinar noticias de múltiples fuentes RSS.
- El diseño debe ser responsive.

Crear los tipos, servicios, componentes y rutas que consideres necesarios siguiendo la arquitectura existente del proyecto.

Dejar implementado los cambios en IMPLEMENTATION_LOG_blog-plan.md
