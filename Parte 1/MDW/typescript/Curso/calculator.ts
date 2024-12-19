/* const operations = ['multiply', 'add', 'divide']; */ // Gracias a ts ya no necesito esto, uso |
/* En ts se puede crear un tipo, asicomo existe el tipo string, Int, acá creo uno con type: */
type operation = 'multiply'| 'add' |'divide'

const calculator = (a: number, b: number, op: operation) => {
  /* if (!operations.includes(op)) {
    console.log('This operation is not defined');
  } */

  if (op === 'multiply') return a * b;
  if (op === 'add') return a + b;

  if (op === 'divide') {
    if (b === 0) return 'can\'t divide by 0! sorry!';
    return a / b;
  }
}

console.log( calculator(1, 3, 'add'));
console.log( calculator(1, 3, "multiply"));


