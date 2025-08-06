/* Los métodos son propiedades con funciones en el objeto */
function habilidadFuncion(){
    return 'Patadas rápidas'
}

var objetoConMetodo1 = {
    nombre: 'Juan',
    poder: 'volar',
    habilidad1: habilidadFuncion, // Se lo asigno como variable
    habilidad2: habilidadFuncion(), // Se lo asigno como función
    habilidad3: function funcionLocal(){
        return 'Piñas rápidas'
    },
    habilidad4: function(){
        return 'Cabezasos rápidos'
    },
    habilidad5(){
        return 'Resistencia muscular dea'
    }
}

// Ojo con los ()
console.log(objetoConMetodo1.habilidad1());
console.log(objetoConMetodo1.habilidad2);

// Otra forma
console.log(objetoConMetodo1.habilidad3());

// Mejor forma
console.log(objetoConMetodo1.habilidad4());

// Forma mas nueva
console.log(objetoConMetodo1.habilidad5());