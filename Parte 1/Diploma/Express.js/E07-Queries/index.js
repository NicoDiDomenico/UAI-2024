/* 
Parámetros de consulta (Query Parameters)
    Ejemplo: https://www.ejemplo.com/widgets?color=azul&sort=reciente
    Uso: Los parámetros de consulta se utilizan para enviar datos adicionales a través de una URL en la forma de pares clave-valor. Estos parámetros comienzan después de un signo de interrogación ? y cada par clave-valor se separa con un símbolo &.
    Contexto: Son ideales para filtrar, ordenar o especificar detalles opcionales en una petición. No forman parte de la ruta propiamente dicha, sino que son opcionales y pueden ser utilizados en cualquier combinación.
*/

const express = require('express');

const app = express();

// Este código define una ruta en una aplicación Express.js que maneja una solicitud de búsqueda. Utiliza un query parameter (req.query.q) para verificar si el valor de q en la URL es igual a 'javascript books'. Si coincide, responde con 'lista de libros de javascript'. Si no coincide, responde con 'pagina normal'.
app.get('/search', function(req, res){
    if (req.query.q === 'javascript books') { /* Los espacios en la URL se reemplazan por %20 */
      res.send('lista de libros de javascript')
    } else {
      res.send('Página normal')
    }
});  
// Nota: Se pueden combinar queries con params.

app.listen(3000, function(){
    console.log('Server on port 3000');  // Muestra en la consola que el servidor está corriendo en el puerto 3000
});
