# Implementation Log - Blog

## Archivos creados

- `Frontend/src/pages/BlogPage.tsx`
- `Frontend/src/services/blogService.ts`
- `Frontend/src/types/blog.ts`
- `Frontend/src/assets/blog-hero.png`
- `Frontend/src/assets/blog.jpg`
- `Frontend/src/assets/blog-digitalizacion.png`
- `Frontend/src/assets/blog-ia.png`
- `Frontend/src/assets/blog-socios.png`
- `Frontend/src/assets/blog-marketing.png`
- `Frontend/src/assets/blog-retencion.png`
- `Frontend/src/assets/blog-tendencias.png`
- `Docs/Frontend/E5-Punto-Venta/E5P5-Blog/IMPLEMENTATION_LOG_blog-plan.md`

## Archivos modificados

- `Frontend/src/pages/BlogPage.tsx`: se reemplazaron los artículos estáticos por noticias obtenidas desde feeds RSS.
- `Frontend/src/App.css`: se conservaron los estilos originales del blog y se agregaron estilos para metadatos, resúmenes, enlaces, carga y error.
- `Frontend/src/routes/AppRouter.tsx`: la ruta pública `/blog` utiliza `BlogPage`.
- `Frontend/.env.example`: se documentó la variable `VITE_RSS_PROXY_URL`.

## Diseño y navegación

- Se mantuvo la composición visual original basada en el frame `2:299` del archivo de Figma de MindFit.
- El banner principal utiliza `blog.jpg`, con bloques de madera que forman la palabra `BLOG`.
- Se conservaron el banner editorial, la grilla responsive, la barra lateral y las animaciones de entrada.
- Se reutilizan `LandingHeader` y `PublicFooter`.
- `Blog` continúa marcado como navegación activa mediante `NavLink`.
- La pantalla sigue siendo pública y no requiere autenticación ni permisos.
- La barra lateral ahora informa las fuentes consultadas en lugar de mostrar categorías estáticas sin comportamiento.

## Integración RSS

- La integración se realiza únicamente desde el frontend.
- No se modificó el backend.
- No se utiliza el `apiClient` autenticado ni se envían `Authorization` o `X-Gym-Id`.
- Se utiliza un servicio RSS-to-JSON configurable mediante `VITE_RSS_PROXY_URL`.
- El valor por defecto y el documentado para desarrollo/demo es:

```env
VITE_RSS_PROXY_URL=https://api.rss2json.com/v1/api.json
```

- El proxy puede configurarse como una URL compatible con el parámetro `rss_url` o como una plantilla que contenga `{url}`.
- Se configuraron estas fuentes:
  - Vitónica: `https://www.vitonica.com/index.xml`
  - Runner's World España: `https://www.runnersworld.com/es/rss/all.xml`
  - Men's Health España: `https://www.menshealth.com/es/rss/all.xml`

## Normalización de noticias

El servicio transforma la respuesta externa al tipo `BlogArticle` con estos campos:

- `id`
- `title`
- `summary`
- `publishedAt`
- `source`
- `url`
- `imageUrl`

Además:

- elimina HTML de títulos y resúmenes mediante `DOMParser`;
- limita los resúmenes extensos;
- acepta únicamente URLs externas HTTP/HTTPS;
- obtiene imágenes desde `thumbnail`, `enclosure` o la primera imagen del contenido;
- elimina noticias duplicadas por URL;
- ordena las noticias por fecha descendente;
- muestra como máximo 12 noticias combinadas;
- utiliza `blog-tendencias.png` cuando una noticia no incluye una imagen válida.

## Caché y tolerancia a fallos

- Las noticias se guardan en `localStorage` con la clave `mindfit.blogArticles.v1`.
- La caché tiene una vigencia de 30 minutos.
- Las fuentes se consultan en paralelo mediante `Promise.allSettled`.
- Si una fuente falla, se muestran las noticias obtenidas desde las demás fuentes.
- Si todas las fuentes fallan y existe caché vencida, se muestran los últimos datos guardados con un aviso.
- Si no existe ninguna fuente disponible ni caché, se muestra un mensaje de error con la acción `Reintentar`.
- Las solicitudes simultáneas se reutilizan para evitar consultas duplicadas durante el montaje de React en desarrollo.
- Los errores de lectura o escritura de `localStorage` no impiden mostrar el blog.

## Estados de interfaz

- Durante la consulta se muestran placeholders animados con la misma estructura de la grilla.
- La carga inicial renderiza únicamente 4 noticias y 4 placeholders.
- La acción `Cargar más noticias` agrega otras 4 tarjetas por vez sin volver a consultar los feeds RSS.
- Las imágenes de las noticias todavía no visibles no se montan ni se descargan hasta mostrar su tarjeta.
- Cada noticia muestra imagen, fuente, fecha, título, resumen y el enlace `Leer más`.
- `Leer más` abre la noticia original en una pestaña nueva con `noopener noreferrer`.
- Las imágenes utilizan carga diferida y reemplazo local cuando la imagen remota falla.
- Las animaciones se desactivan cuando el usuario tiene habilitado `prefers-reduced-motion`.

## Validaciones ejecutadas

- Se verificó que los tres feeds configurados respondieran con HTTP 200.
- Se verificó una respuesta real de RSS2JSON con estado `ok` y noticias normalizadas.
- `npm.cmd run build`: exitoso; TypeScript y Vite compilaron sin errores.
- ESLint específico sobre `BlogPage.tsx`, `blogService.ts` y `blog.ts`: exitoso.
- El lint global conserva errores preexistentes fuera del blog en `useInicioData.ts` y `UsuariosPage.tsx`.
- Vite conserva el warning general por un chunk JavaScript superior a 500 kB; no fue introducido específicamente por esta funcionalidad.
- No se pudo realizar la inspección visual automatizada final porque el navegador integrado no estaba disponible en la sesión.

## Limitaciones

- La solución está orientada a desarrollo y demo, y depende de la disponibilidad y las condiciones del servicio RSS-to-JSON externo.
- Las fuentes pueden modificar su URL, formato o política de publicación sin control de MindFit.
- Las imágenes remotas pueden bloquear hotlinking; en ese caso se utiliza la imagen local de respaldo.
- Para una versión productiva conviene reemplazar el servicio público por un proxy propio o una función del proveedor de hosting.
