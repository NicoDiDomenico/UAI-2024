const express = require('express');

const app = express();

////// Request - Lo que el cliente puede hacer
//// Aplicando CRUD (Create, Read, Update, Delete)
// Create (Crear) → POST
app.post('/products',function(req,res){
    res.send('creando productos'); // Todavia no vamos a indagar en endio de datos
});

// Read (Leer) → GET
app.get('/products',function(req,res){
    //// Antes yo puedo ejecutar lógica, que es la razón de por qué creamos código de servidor.
    // validar datos
    // consulta a base de datos
    // procesar datos
    res.send('lista de productos'); // Todavia no vamos a indagar en envio de datos
});

// Update (Actualizar) → PUT
app.put('/products',function(req,res){
    res.send('Actualizando UN producto'); // Todavia no vamos a indagar en endio de datos
});

// Update (Actualizar) → PATCH
app.patch('/products',function(req,res){
    res.send('Actualizando una parte del producto'); // Todavia no vamos a indagar en endio de datos
});

// Delete (Eliminar) → DELETE
app.delete('/products',function(req,res){
    res.send('Eliminando UN producto'); // Todavia no vamos a indagar en endio de datos
});

app.listen(3000, function(){
    console.log('Server on port 3000');
});