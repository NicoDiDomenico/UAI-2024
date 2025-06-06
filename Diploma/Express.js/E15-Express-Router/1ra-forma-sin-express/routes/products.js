function ProductsRoutes(app){
    // Arreglo que simulará mi base de datos. Se va a guardar en la ram, cuando haga Ctrl + S se borrará.
    let productos = [
        {
            id: 1,
            name: 'laptop',
            price: 3000
        }
        // voy a agregar + objetos a traves de thunder client
    ];

    // Routes -> Hacemos routing
    app.get('/products',function(req, res){
        console.log(req.body); // Notar como no se muestra en .json, esto es gracias al app.use(express.json());
        res.json(productos); // res.json() convierte automáticamente el objeto JavaScript o arreglo que le pases en una cadena JSON y lo de envia. Es un res.send() pero que solamente traduce a json.
    });

    app.post('/products',function(req, res){
        console.log(req.body);
        // Directamente:
        /* productos.push({id: productos.length+1,...req.body}); */
        // Pasandolo a una variable:
        var unProducto = {id: productos.length+1,...req.body}
        productos.push(unProducto);
        res.send('Creando productos');
        console.log('Producto creado: ' + unProducto.name);
    });

    app.put("/products/:id", function(req, res){
        let newData = req.body // ya que ahora no van a pasar un objeto completo, sino algunos pares clave/ valor para modificiar en el objeto con la id que se encuentra en el req
        let productFound = productos.find( // devuelve el primer objeto que cumpla con la condicion
            function(product) {
                return product.id === parseInt(req.params.id)}
        );

        if (!productFound)
        return res.status(404).json({
            message: "Product not found",
        });

        productos = productos.map(function(p){
            return p.id === parseInt(req.params.id) ? {...p, ...newData} : p /* basicamente hicimos un if ternario que si se cumple la condicion se copia lo que habia en p pero modificando p con la newData, y sino se devuevle solo p como está */
        });

        res.json({
            message: "Product updated successfully"
        });
    });

    app.delete("/products/:id", function(req, res) {
        // Buscar el producto en la lista usando el ID proporcionado en la URL
        var productFound = productos.find(function(product) {
            return product.id === parseInt(req.params.id);
        });

        // Si el producto no se encuentra, devolver un estado 404 con un mensaje claro
        if (!productFound) {
            return res.status(404).json({ 
                message: "Product not found" // El producto con el ID dado no existe
            });
        }

        // Si el producto se encuentra, eliminarlo de la lista
        productos = productos.filter(function(product) { // El método filter es una función de los arreglos en JavaScript que se utiliza para crear un nuevo arreglo con todos los elementos que cumplen una condición especificada en la función de callback que se le pasa.
            return product.id !== parseInt(req.params.id); // entonces los objetos que pasaran al nuevo arreglo son los que tengan id distinta a la del params
        });

        // Responder con un estado 204 (No Content) para indicar que la operación fue exitosa
        res.sendStatus(204);
        console.log(productos);
    });

    app.get('/products/:id',function(req, res){
        console.log(req.params.id);
        let productFound = productos.find(function (prod) {
            return prod.id === parseInt(req.params.id); // necesito convertir en entero el req.params.id, recordar que todo lo que viaje en un a url está en string
        });
        if (!productFound) {
            return res.status(404).json({ // notar como puedo editar el codigo de estado, y asi aclarar que es un 404.
            message: "Product not found"
            });
        } 
        res.json(productFound);
    });
}

module.exports = ProductsRoutes;