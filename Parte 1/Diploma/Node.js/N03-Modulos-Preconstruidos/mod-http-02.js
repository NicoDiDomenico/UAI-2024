// También podemos llamar las funciones desde una constante --> Forma mas ordenada!
const http = require('http');

const handlerServer = function handlerServer(req,res){ 
    res.writeHead(200, {'content-type': 'text/html'}); 
    res.write('<h1>Hola mundo desde node.js</h1>');
    res.end;
};
const server = http.createServer(handlerServer);

server.listen(3000, function(){
    console.log('Server on en puerto 3000');
});