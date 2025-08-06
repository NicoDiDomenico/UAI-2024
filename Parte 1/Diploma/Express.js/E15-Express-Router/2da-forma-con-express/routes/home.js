const express = require('express');

// Modularizando rutas en Express:
router = express.Router();
/* 
1) Uso de express.Router():
    Aquí, en lugar de usar directamente app = express(), estás utilizando app = express.Router(). Esto es más modular y permite encapsular las rutas en un "router" separado que luego puedes montar en la aplicación principal. express.Router() devuelve una instancia de middleware que se comporta como una mini aplicación Express.

2) Exportación del Router:
    En lugar de exportar una función (HomeRoutes) como en los ejemplos anteriores, estás exportando directamente el router (app). Esto significa que el código exportado ya tiene todas las rutas configuradas y se puede montar directamente en la aplicación principal.

3) Importación y Uso en index.js:
    En index.js, cuando importas HomeRoutes, estás importando el router ya configurado y luego lo montas en la aplicación principal con app.use(HomeRoutes);. Esto hace que todas las rutas definidas en home.js estén disponibles en tu aplicación.
*/

router.get('/about', function(req, res){
    res.send('about page');
});

router.get("/dashboard", (req, res) => {
    res.sendFile("Dashboard page");
});

module.exports = router