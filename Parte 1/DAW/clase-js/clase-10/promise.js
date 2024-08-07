/* 
Promesas:
Las promesas son objetos que representan la eventual finalización (o falla) de una tarea asincrónica.

Una promesa puede estar en uno de los tres estados:

-Pendiente (pending): Estado inicial de una promesa antes de que se cumpla o se rechace.
-Cumplida (fulfilled): Estado en el que la promesa se cumple exitosamente y devuelve un valor resultado.
-Rechazada (rejected): Estado en el que la promesa falla y devuelve un motivo de error.

Las promesas vienen incluidas en el lenguaje JavaScript y se basan en la clase Promise.

En el constructor de Promise, se proporciona una función que a su vez toma dos argumentos: resolve y reject. Estos son métodos que se utilizan para cambiar el estado de la promesa.

-resolve: Se utiliza para cambiar el estado de la promesa a resuelta (fulfilled) y retornar un valor de resultado.
-reject: Se utiliza para cambiar el estado de la promesa a rechazada (rejected) y retornar un motivo de rechazo, generalmente representado por una instancia de Error o una cadena de texto.

También se pueden combinar promesas utilizando Promise.all() o Promise.race() para manejar varias promesas simultáneamente.
    if/else

Manejo de promesas
.then() y .catch()

- .then se utiliza con Promesas en JavaScript para manejar el resultado exitoso de una operación asíncrona.
- Después de una promesa, puedes encadenar .then para ejecutar código cuando la promesa se resuelva correctamente.
- El método .catch se utiliza para manejar cualquier error que ocurra durante la ejecución de la promesa.
- Cuando se produce un error dentro del .then anterior, el control se transfiere al bloque .catch más cercano.
*/

// Sin promesas - asincronia sin problema
var datos = ['Iron man','Spiderman','Avengers: Endgame'];

var getDatos = function(){
    return datos
};
console.log(getDatos());

// Sin promesas - asincronia con problema
/* El problema con el segundo fragmento de código es que la función setTimeout es asíncrona. Esto significa que la función getDatos no espera a que setTimeout complete su ejecución antes de devolver el control. Por lo tanto, getDatos devuelve undefined antes de que la función pasada a setTimeout se ejecute y devuelva los datos. */
var getDatos = function(){
    // Simulamos una peticion al servidor
    setTimeout(function(){
        return datos;
    },1000)
};
console.log(getDatos());

// Con promesas - solucionando el problema de asincronia
var getDatos = function(){
    // Simulamos una peticion al servidor
    return new Promise(function(resolve,reject){
        setTimeout(function(){
            resolve(datos); // Esta línea indica que la Promesa se resuelve exitosamente con el valor datos.
        },1000)
    });
};
getDatos().then(function(datos){
    console.log(datos);
})

// Otro ejemplo de promesa:
var datos = ['Iron man', 'Spiderman', 'Avengers: Endgame'];

var getDatos = function() {
    return new Promise((resolve, reject) => {
        setTimeout(function() {
            // Simulamos una operación que puede fallar o tener éxito aleatoriamente
            let errorOccurred = Math.random() < 0.5; // 50% de probabilidad de error

            if (errorOccurred) {
                reject('Error: No se pudieron obtener los datos');
            } else {
                resolve(datos);
            }
        }, 1000);
    });
};

getDatos()
    .then(function(datos){console.log(datos)}) // Manejo de éxito
            // LA FUNCION dentro de then hace de callback
    .catch(function(error){console.error('Error:', error)}); // Manejo de error

// Entendiendo mejor .then y .catch
/* 
Capturando Errores con reject:
Cuando se crea una promesa, puedes usar reject para rechazar la promesa y pasar un error a la cadena de promesas. Este rechazo es capturado por .catch().
*/
let promesa = new Promise((resolve, reject) => {
    setTimeout(() => {
        reject("Operation failed");
    }, 1000);
});

promesa()
    .then(result => {
        console.log(result); // Este no se ejecutará
    })
    .catch(error => {
        console.error("Error caught:", error); // "Error caught: Operation failed"
    });
/* 
En este caso:

La promesa se rechaza después de 1 segundo con el mensaje "Operation failed".
El .catch() captura este rechazo y maneja el error.
*/

/* 
Capturando Errores Lanzados en .then():
Los errores también pueden ser lanzados dentro de un .then() mediante throw. Estos errores serán capturados por el siguiente .catch() en la cadena.
*/
let promesa2 = new Promise((resolve, reject) => {
    setTimeout(() => {
        resolve("Operation successful");
    }, 1000);
});

promesa2()
    .then(result => {
        console.log(result); // "Operation successful"
        throw new Error("Something went wrong in then");
    })
    .catch(error => {
        console.error("Error caught:", error.message); // "Error caught: Something went wrong in then"
    });

/* 
En este caso:

La promesa se resuelve después de 1 segundo con el mensaje "Operation successful".
El primer .then() maneja el resultado, imprime el mensaje y luego lanza un error.
El .catch() captura este error lanzado en el .then() y lo maneja.
*/

/* 
Combinando Ambos Casos:
Vamos a combinar ambos casos para ver cómo .catch() maneja errores provenientes tanto del rechazo de la promesa como de los errores lanzados en un .then(). 
*/
let promesa3 = new Promise((resolve, reject) => {
    setTimeout(() => {
        let success = Math.random() > 0.5; // Simula éxito o fracaso al azar
        if (success) {
            resolve("Operation was successful");
        } else {
            reject("Operation failed");
        }
    }, 1000);
});

promesa3()
    .then(result => {
        console.log(result); // "Operation was successful" (si es exitoso)
        return result * 2; // Este es un ejemplo, podría ser cualquier operación
    })
    .then(result => {
        console.log(result); // Resultado de la operación anterior
        // Lanzar un error intencionalmente
        throw new Error("Something went wrong in then");
    })
    .catch(error => {
        console.error("Error caught:", error); // Captura tanto el rechazo como el error lanzado en `.then()`
    });

/* 
En este ejemplo:

La promesa tiene un 50% de probabilidad de ser resuelta o rechazada.
Si es resuelta, se maneja en el primer .then(), y luego se lanza un error en el segundo .then().
Si es rechazada, el .catch() captura el error del rechazo de la promesa.
Si un error es lanzado en cualquier .then(), el .catch() también lo captura.

Resumen
reject en la promesa inicial: Rechaza la promesa y pasa el error a la cadena de promesas. El .catch() lo captura.
throw en un .then(): Lanza un error dentro de un .then(). El .catch() más cercano en la cadena captura este error.
El .catch() al final de la cadena de promesas captura cualquier error, ya sea desde un rechazo inicial con reject o un error lanzado en un .then().
Esta capacidad de .catch() de manejar ambos tipos de errores hace que el manejo de errores sea más sencillo y centralizado.
*/