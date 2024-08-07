// EVENT LISTENERS - probando distintos eventos
// Podemos traer el elemento en una variable como haciamos
var boton = document.getElementById("saludoBtn");
console.log(boton);

boton.addEventListener("click", function(){
    console.log("Hola");
});

// O directamente usar el ID del elemento en cuestión - menos código!
saludoBtn.addEventListener("click", function(){
    console.log("Le diste al clic izquierdo!!");
});

saludoBtn.addEventListener("contextmenu", function(){
    console.log("Le diste al clic derecho!!");
});

/* saludoBtn.addEventListener("mouseover", function(){
    console.log("Me pasaste el punte del mouse por encima!!");
}); */ 

// Mas eventos: https://www.w3schools.com/jsref/dom_obj_event.asp

// BORRAR EEVENTO - Para esto tenermos que trabajar con la funcion o handler separado del evento, sino no funciona
// removeEventListener: Remueve del objeto un detector de evento previamente registrado con EventTarget.addEventListener

saludoBtn.addEventListener("mouseover", pasando);
function pasando (){
    console.log("Me pasaste el punte del mouse por encima!!");
};

// ahora procedemos a eliminarlo...
saludoBtn.removeEventListener("mouseover", pasando);
function pasando (){
    console.log("Me pasaste el punte del mouse por encima!!");
};

// Probando + eventos:
// Por defecto cuando ejecutamos un evento, este mismo pasa como parámetro en la funcion que le especifiquemos, el uso de esta puede sernos muy útil..
saludoBtn.addEventListener("click", function(event){
    console.log(event); // Cómo resutlado tenemos un objeto con varias propiedades que podemos usar. Este objeto es el mismisimo evento
    console.log(event.target.innerHTML);
});

userInput.addEventListener("keypress", (event)=>{
    if(event.key === "Enter" || event.keCode === 13){
        console.log(event); // este objeto tiene propiedades distintas, ya que corresponde a un diferrente evento
        console.log(event.target.value); // y notar ademas que al ser un objeto cuentas con sus métodos. Con el método .target.value tenemos el valor que ingresamos en el input
    }
});