// Para ID
// var elementoPorId = document.getElementById("parrafo1");
var elementoPorId = document.querySelector("#parrafo1")
elementoPorId.textContent = "Usé querySelector :)"

// Para Clases
// var elementosPorClase = document.getElementsByClassName("parrafos");
var elementosPorClase = document.querySelectorAll(".parrafos"); /* si usaba querySelector() traia la priemra clase */
console.log(elementosPorClase);
// es del tipo NodeList el objeto, los nodeList tienen menos métodos, poir eso no se recimienda los querySlector

var elementosPorEtiquetas = document.querySelectorAll('p');
elementosPorEtiquetas[2].textContent = "También sirve para etiquetas"