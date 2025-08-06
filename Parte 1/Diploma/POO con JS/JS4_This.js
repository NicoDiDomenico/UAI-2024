var heroe = {
    nombre: 'Juan',
    apellido: 'Peruan',
    poder: 'volar',
    ahorros: 100,
    habilidad(){
        return this // Retorno el mismisimo objeto :O
    },
    nombreCompleto(){
        return this.nombre + ' ' + this.apellido
    },
    sumar(valor){
        this.ahorros += valor;
    },
    restar(valor){
        this.ahorros -= valor;
    }
}

// this hace referencia al mismo objeto que lo contiene
console.log(heroe);
console.log(heroe.habilidad());

console.log(heroe.nombreCompleto());

// Usando parámetros
console.log(heroe);
console.log(heroe.ahorros);
heroe.sumar(100);
console.log(heroe.ahorros);
heroe.sumar(100);
console.log(heroe.ahorros);
heroe.sumar(100);
console.log(heroe.ahorros)
heroe.restar(300);
console.log(heroe.ahorros)