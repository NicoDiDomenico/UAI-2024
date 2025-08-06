// Polimorfismo - capacidad de algunos objetos para poder manipular distintos tipos de datos de manera uniforme, se busca reducir el acoplamiento
// Sobrecarga - métodos que pueden tomar parámetros con diferentes tipos de datos.

// Con igual cantidad de parámetros:
function countItems(x) {
    return x.toString().length;
  }
  
  console.log(countItems(10000));
  console.log(countItems('Hola mundo'));
  
  // Con distinta cantidad de parámetros:
  function sum(x = 0, y = 0, z = 0) { /* Esta es la forma para que los parametros tomen determinado valor por defector en caso d eque no se ingresen */
    return x + y + z;
  }
  
  console.log(sum(10, 20));
  console.log(sum(10, 20, 30));
  