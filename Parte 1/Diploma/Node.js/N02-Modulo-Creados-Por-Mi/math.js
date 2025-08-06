Math = {};

function add(A, B){
    return A + B;
}

function substract(A, B){
    return A - B;
}

function multiply(A, B){
    return A * B;
}

function divide(A, B){
    if (B === 0){
        console.log('No se puede dividir por 0');
    } else {
        return A / B;
    }
}

// Para que se importe este módulo primero se tiene que exportar las funciones del mismo
/* Se exportarán como objetos */
/* exports.add = add; 
exports.substract = substract; 
exports.multiply = multiply; 
exports.divide = divide;  */

// Podemos ya tener las funciones convertidas en objetos entonces las pasamos con module.exports
// AL objeto Math(vacio) le agrego como propiedad las funciones
Math.add = add
Math.substract = substract;
Math.multiply = multiply; 
Math.divide = divide; 

module.exports = Math;

// Tambien puedo exportar directamente una sola funcion (hay que comentar las funciones anteriores en el index para que ande)
/* module.exports = function soyUnaFuncion(){
    console.log('Hola!!, me importe como funcion!');
} */

    /* 
    Como conclusion:
    - exports. permite exportar solamente objetos.
    - module.exports permite exportar cualquier cosa, variables, funciones, objetos, etc.
    */