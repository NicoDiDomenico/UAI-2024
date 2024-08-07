// Entendinedo las promesas:
// 1. Crear una Promesa
let miPromesa = new Promise((resolve, reject) => {
    // 2. Definir la Operación Asincrónica
    // Aquí puedes usar cualquier lógica asincrónica, como una solicitud a un servidor
    setTimeout(() => {
        // Simula una condición para éxito o error
        let exito = true; // Cambia esto para simular éxito o error
        if (exito) {
            // 3. Llamar a `resolve` en caso de éxito
            resolve("¡Operación exitosa!");
        } else {
            // 3. Llamar a `reject` en caso de error
            reject("Ocurrió un error.");
        }
    }, 2000); // Simula un retraso de 2 segundos
});

// 4. Consumir la Promesa
miPromesa
    .then((mensaje) => {
        console.log(mensaje); // Esto se ejecuta si la promesa se cumple
    })
    .catch((error) => {
        console.error(error); // Esto se ejecuta si la promesa se rechaza
    });

// Aprendiendo callbacks:
function hacerAlgo(callback) {
    setTimeout(() => {
        console.log("Haciendo algo...");
        callback(); // Llamamos al callback después de que la operación se completa
    }, 1000);
}

function hacerAlgoMas(callback) {
    setTimeout(() => {
        console.log("Haciendo algo más...");
        callback(); // Llamamos al callback después de que la operación se completa
    }, 1000);
}

function hacerUnaUltimaCosa(callback) {
    setTimeout(() => {
        console.log("Haciendo una última cosa...");
        callback(); // Llamamos al callback después de que la operación se completa
    }, 1000);
}

function finalizar() {
    console.log("Todo terminado");
}

// Encadenamos las funciones usando callbacks
hacerAlgo(() => {
    hacerAlgoMas(() => {
        hacerUnaUltimaCosa(() => {
            finalizar();
        });
    });
});

// Aplicandole promesas al ejemplo anterior:
function hacerAlgo() {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            console.log("Haciendo algo...");
            resolve("Paso 1 completado"); // Llamamos a resolve para indicar éxito
        }, 1000);
    });
}

function hacerAlgoMas() {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            console.log("Haciendo algo más...");
            resolve("Paso 2 completado"); // Llamamos a resolve para indicar éxito
        }, 1000);
    });
}

function hacerUnaUltimaCosa() {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            console.log("Haciendo una última cosa...");
            resolve("Paso 3 completado"); // Llamamos a resolve para indicar éxito
        }, 1000);
    });
}

// Encadenamos las promesas para ejecutar las funciones en secuencia
hacerAlgo()
    .then((mensaje) => {
        console.log(mensaje);
        return hacerAlgoMas();
    })
    .then((mensaje) => {
        console.log(mensaje);
        return hacerUnaUltimaCosa();
    })
    .then((mensaje) => {
        console.log(mensaje);
        console.log("Todo terminado");
    })
    .catch((error) => {
        console.error("Ocurrió un error:", error);
    });

// Aplicando Async Await:
async function ejecutarTareas() {
    try {
        const mensaje1 = await hacerAlgo();
        console.log(mensaje1);

        const mensaje2 = await hacerAlgoMas();
        console.log(mensaje2);

        const mensaje3 = await hacerUnaUltimaCosa();
        console.log(mensaje3);

        console.log("Todo terminado");
    } catch (error) {
        console.error("Ocurrió un error:", error);
    }
}

// Llamar a la función `async`
ejecutarTareas();

// fetch
// Realizar una solicitud HTTP GET a una URL
fetch('https://api.example.com/data')
    .then(response => {
        if (!response.ok) {
            throw new Error('Error en la solicitud: ' + response.statusText);
        }
        return response.json(); // Convertir la respuesta a JSON
    })
    .then(data => {
        console.log(data); // Manejar los datos obtenidos
    })
    .catch(error => {
        console.error('Ocurrió un error:', error); // Manejar cualquier error
    });


// Uso de fetch con async y await
async function obtenerDatos() {
    try {
        const response = await fetch('https://api.example.com/data');
        if (!response.ok) {
            throw new Error('Error en la solicitud: ' + response.statusText);
        }
        const data = await response.json(); // Esperar a que la respuesta se convierta a JSON
        console.log(data); // Manejar los datos obtenidos
    } catch (error) { // El bloque catch captura cualquier error que ocurra dentro del bloque try y lo maneja. 
        console.error('Ocurrió un error:', error); // Manejar cualquier error
    }
}

// Llamar a la función asíncrona
obtenerDatos();
