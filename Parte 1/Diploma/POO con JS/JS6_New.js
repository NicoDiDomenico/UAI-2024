var objetoPadre = {
    name1: 'Soy padre',
    ObjetoHijo: function(){
        this.name2 = 'Soy un objeto hijo'
    }
}
/* Puedo tener constructores dentro de un ibjeto literal */
var padre = new objetoPadre.ObjetoHijo();
console.log(objetoPadre);
console.log(objetoPadre.name1);
console.log(objetoPadre.name2);   
console.log(padre.name2)

/* Notar como al no tener new el this del constructor utiliza como objeto al objeto que lo contiene que será objetoPadre */
var padre = objetoPadre.ObjetoHijo();
console.log(objetoPadre);
console.log(objetoPadre.name1);
console.log(objetoPadre.name2);   
console.log(padre.name2)