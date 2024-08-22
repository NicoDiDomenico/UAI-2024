const math = require('./math.js'); // Importo el módulo

console.log(math) /* Notar como se importa un objeto, esto es debido al exports. del módulo math.js o porque manualmente las pase como objetos */

console.log('Suma: ' + math.add(1,2));
console.log('Resta: ' + math.substract(1,2));
console.log('Multiplicacion: ' + math.multiply(1,2));
console.log('Division: ' + math.divide(1,0));

//// IMportante
// importar objeto vs importar funcion:
/* 
Uso después de la Importación:

Función: Después de importar una función, generalmente la invocas con función().
Objeto: Después de importar un objeto, accedes a sus propiedades o métodos con objeto.propiedad o objeto.metodo().
*/