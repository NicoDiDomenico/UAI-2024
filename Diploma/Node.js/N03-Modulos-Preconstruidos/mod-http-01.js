//// Una de las tareas principales de node.js es crear servidores, y para eso vamos necesitar un modulo.
// Módulo http - el protocolo http consiste en poder recibir peticiones de los clientes y poder dar respuestas desde el servidor.
const http = require('http');

// Método para crear un servidor --> Acá hice un .txt que explica este apartado
http.createServer(function(req,res){ /* request(req) y response(res) hacen referencia a la peticion del cliente y a la respuesta del servidor respectivamente*/
    res.writeHead(200, {'content-type': 'text/html'}); // https://es.wikipedia.org/wiki/Anexo:C%C3%B3digos_de_estado_HTTP
    /* 
    res.writeHead(200, {...});: Este método envía un encabezado de respuesta HTTP al cliente. Aquí estás especificando dos cosas:
        200: Es el código de estado HTTP. El código 200 significa que la solicitud fue exitosa y que la respuesta está OK.
        {'Content-Type': 'text/html'}: Este es un encabezado que indica el tipo de contenido que se está enviando en la respuesta. Aquí, estás indicando que el contenido que sigue es HTML (text/html). Esto le dice al navegador cómo debe interpretar el contenido que recibe.
    */
    res.write('<h1>Hola mundo desde node.js</h1>');
    res.end;
}).listen(3000); /* tengo que hacerle saber a mi servidor que puerto tiene que escuchar las solicitudes del cliente (mi navegador en este caso) */

// con doble ctrl + C en el CMD cerramos el servidor