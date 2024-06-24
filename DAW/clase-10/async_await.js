/* 
async / await:

- async/await es una forma más legible y concisa de escribir código asíncrono en JavaScript.
- La palabra clave async se coloca antes de una función para indicar que dicha función retornará una promesa.
- await se utiliza dentro de una función async para esperar a que una promesa se resuelva antes de continuar la ejecución del código.
- Puedes envolver el código que utiliza await dentro de un bloque try/catch para capturar y manejar errores de forma más efectiva.

En general, async/await se considera una sintaxis más moderna y legible para trabajar con código asíncrono en JavaScript. Proporciona una forma más fácil de estructurar y manejar el flujo de código asíncrono. 
*/

var datos = ['Iron man', 'Spiderman', 'Avengers: Endgame'];

var getDatos = function() {
    return new Promise((resolve, reject) => {
        setTimeout(function() {
            let errorOccurred = Math.random() < 0.5;

            if (errorOccurred) {
                reject('Error: No se pudieron obtener los datos');
            } else {
                resolve(datos);
            }
        }, 1000);
    });
};

async function fetchDatos() { // async se coloca antes de una función para indicar que dicha función retornará una promesa
    try {
        const result = await getDatos(); // await se utiliza dentro de una función async para esperar a que una promesa se resuelva antes de continuar la ejecución del código
        console.log('Datos obtenidos:', result); // Manejo de éxito
    } catch (error) {
        console.error('Error:', error); // Manejo de error
    }
}

// Llamamos a la función asíncrona
fetchDatos();
