// Async code
/* 
Cuando un cliente accede a http://localhost:3000/post, se ejecuta la función asincrónica asociada.
axios.get hace una solicitud HTTP a https://jsonplaceholder.typicode.com/posts, una API de prueba que devuelve datos JSON.
await se asegura de que el código espere a recibir la respuesta antes de continuar.
La respuesta (que contiene datos) se registra en la consola y luego se envía de vuelta al cliente con res.send.
*/

const express = require('express'); // Importa el módulo Express para crear el servidor.
const axios = require('axios'); // Importa el módulo Axios para hacer solicitudes HTTP.

/* voy a usar nodemon --> para hacerlo andar poner: npm run dev */
const app = express(); // Crea una instancia de una aplicación Express.

// https://jsonplaceholder.typicode.com/
app.get('/post', async function(req, res) { // Define una ruta GET en '/post' y usa una función asincrónica.
    const response = await axios.get('https://jsonplaceholder.typicode.com/posts'); 
    // Hace una solicitud GET a la URL indicada y espera la respuesta.
    
    console.log(response.data[7]); // Muestra los datos recibidos en la consola.
    res.send(response.data); // Envía los datos recibidos al cliente que hizo la solicitud.
});

app.listen(3000, function(){
    console.log('Server on port 3000'); // Hace que el servidor escuche en el puerto 3000.
});
