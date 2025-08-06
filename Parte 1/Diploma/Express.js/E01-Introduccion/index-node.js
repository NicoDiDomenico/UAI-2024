// lo que veniamos haciendo en el CMD ahora lo hacemos en la terminal de VSC: F1 --> Escribo terminal --> Selecciono Create new terminal --> escribo node index.js en el path del sistemas de aruchos donde se encuentre y listo!
console.log('Hola pa');

//// Crear servidor puramente con nodejs vs usar framework express
// nodejs --> uso http:
const http = require('http');
const fs = require('fs'); 

const server = http.createServer(function(req,res){
    const readStream = fs.createReadStream('./static/index.html'); // fs.createReadStream es un método de Node.js que te permite leer un archivo poco a poco, en lugar de cargarlo todo de una vez en la memoria. Es útil cuando trabajas con archivos grandes o quieres procesar el contenido mientras se lee, ya sea ,html, .txt, etc.
    readStream.pipe(res); // pipe es un método en Node.js que se utiliza para conectar (o "encadenar") un flujo de lectura (readable stream) con un flujo de escritura (writable stream). En tu ejemplo, estás utilizando pipe para leer un archivo (index.html) y enviar su contenido directamente como respuesta HTTP al cliente.
    /* 
    // Sin pipe antes se hacia esto:
    readStream.on('data', (chunk) => { // chunk seria una porcion de datos del index.html
        res.write(chunk);  // Escribe cada chunk en la respuesta
    });
    
    readStream.on('end', () => {
        res.end();  // Termina la respuesta cuando todos los datos han sido leídos
    }); 
    */
});

server.listen(3000, function(){
    console.log('Server on port 3000');
});