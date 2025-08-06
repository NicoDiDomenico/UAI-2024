// Creando lista de módulos de mi proyecto ("dependencies") a través del siguiente comando de node.js: npm init -> Se completa y se genera un package.json con meta info de mi proyecto. El servidor leerá este archivo para instalar todos los módulos indicados en "dependencies". Por lo tanto, ya no hace falta reinstalar manualmente el módulo colors que se instaló previamente con 'npm install colors' en el cmd.

// Nosotros subiremos este código index.js y el package.json a un servidor como AWS y este ejecutará npm install en el path alojado, y ahí se instalará el módulo colors. Para probar esto borrar la carpeta node_modules y ejecutar npm install en el cmd simulando que somos el servidor con el codigo que ya subimos.

const http = require('http');
const colors = require('colors'); 

const handlerServer = function handlerServer(req,res){ 
    res.writeHead(200, {'content-type': 'text/html'}); 
    res.write('<h1>Hola mundo desde node.js</h1>');
    res.end;
};
const server = http.createServer(handlerServer);

server.listen(3000, function(){
    console.log('Server on en puerto 3000'.rainbow);
});

/* 
### Explicación detallada:

1. **`npm init`**: 
   - Es un comando que se ejecuta en la terminal para iniciar un nuevo proyecto de Node.js.
   - Cuando ejecutas `npm init`, se te hace una serie de preguntas en la terminal sobre tu proyecto, como el nombre del proyecto, la versión, la descripción, el punto de entrada (archivo principal), etc.

2. **Meta información del proyecto**:
   - Toda la información que proporcionas cuando ejecutas `npm init` se guarda en un archivo llamado `package.json`.
   - Este archivo contiene **meta información** sobre tu proyecto, como su nombre, versión, autor, dependencias (los paquetes que tu proyecto necesita), scripts, etc.

3. **Lista de módulos**:
   - Cuando el comentario menciona "lista de módulos", se refiere a que el `package.json` también contiene una lista de los paquetes o módulos que has instalado en tu proyecto utilizando `npm`.
   - Estos módulos se enumeran en una sección llamada `dependencies` dentro del archivo `package.json`.

### ¿Por qué es importante?

- El archivo `package.json`(archivo de configuración) es esencial para cualquier proyecto de Node.js porque permite:
  - **Mantener un registro de las dependencias**: Todos los paquetes que instalas se listan aquí, lo que facilita compartir el proyecto con otros o trasladarlo a otro entorno.
  - **Ejecutar scripts**: Puedes definir comandos personalizados para automatizar tareas (como correr pruebas, compilar código, etc.).
  - **Información del proyecto**: Ayuda a documentar las configuraciones clave y detalles del proyecto.

### Ejemplo de un `package.json` básico:
```json
    {
    "name": "mi-proyecto",
    "version": "1.0.0",
    "description": "Este es un proyecto de ejemplo",
    "main": "index.js",
    "scripts": {
        "start": "node index.js"
    },
    "author": "Tu Nombre",
    "license": "ISC",
    "dependencies": {
        "express": "^4.17.1"
    }
    }
```

- **`dependencies`**: Aquí se listan los módulos que has instalado, como `express` en este caso.
- **`scripts`**: Aquí puedes definir comandos personalizados, como `"start": "node index.js"` para iniciar tu aplicación.

Este archivo es fundamental para que otros desarrolladores puedan instalar rápidamente todas las dependencias de tu proyecto usando el comando `npm install`.
*/