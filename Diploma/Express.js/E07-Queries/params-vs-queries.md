Cuando digo "parte esencial" en el contexto de parámetros de ruta (route parameters), me refiero a que los datos son fundamentales para identificar o acceder al recurso específico que se está solicitando o manipulando en la URL. 

Aquí tienes una explicación más detallada:

### Parámetros de Ruta como Parte Esencial

1. **Identificación del Recurso:**
   - Los parámetros de ruta se utilizan cuando necesitas identificar un recurso único en tu aplicación. Este recurso es esencial para la URL porque la URL no tendría sentido sin este parámetro.
   - **Ejemplo:**
     - Supongamos que tienes un sitio de usuarios y quieres acceder al perfil de un usuario específico. En este caso, el ID del usuario es fundamental para la URL:
       - `/users/123`
       - Aquí, `123` es el ID del usuario. Sin el ID, no podrías saber qué perfil de usuario quieres mostrar.

2. **Jerarquía de la URL:**
   - Los parámetros de ruta reflejan la estructura jerárquica de los recursos.
   - **Ejemplo:**
     - `/companies/456/employees/789`
     - En este caso, `456` es el ID de una empresa y `789` es el ID de un empleado de esa empresa. La URL representa una jerarquía donde primero identificas la empresa y luego un empleado dentro de esa empresa.

3. **Acceso Directo a Recursos:**
   - Estos parámetros son "esenciales" porque la URL está diseñada para llevarte directamente a ese recurso. Es como una "dirección" que te lleva a un lugar específico.
   - **Ejemplo:**
     - `/orders/555/details`
     - Aquí, `555` es el ID de una orden específica, y sin él, la URL no tendría sentido porque no sabrías de qué orden se trata.

### Comparación con Query Parameters

En contraste, **los parámetros de consulta (query parameters)** no son "esenciales" en el mismo sentido porque se utilizan para modificar o filtrar la forma en que se accede a un recurso, pero no son necesarios para identificar el recurso en sí.

- **Ejemplo con Query Parameters:**
  - `/search?q=javascript`
  - Aquí, `q=javascript` es un parámetro de consulta que filtra los resultados de búsqueda, pero la página de búsqueda podría funcionar sin él (mostraría resultados genéricos o ningún resultado).
  - Otro ejemplo: `/products?category=electronics&sort=price_desc`
    - Aquí, `category=electronics` y `sort=price_desc` modifican la lista de productos que se muestran, pero no son esenciales para acceder a la página de productos en sí.

### Ejemplos Prácticos

- **Parámetros de Ruta (Esenciales):**
  - `/articles/10` (donde `10` es el ID del artículo que quieres ver)
  - `/users/123/friends` (donde `123` es el ID del usuario cuyos amigos quieres ver)
  
- **Parámetros de Consulta (No Esenciales):**
  - `/articles?tag=javascript` (donde `tag=javascript` filtra los artículos que se muestran por una etiqueta específica)
  - `/products?sort=price_asc` (donde `sort=price_asc` ordena los productos mostrados por precio ascendente)

En resumen, los **parámetros de ruta** son esenciales cuando son necesarios para identificar de manera única un recurso en la URL, mientras que los **parámetros de consulta** son opcionales y se utilizan para modificar o filtrar la información que se está solicitando.