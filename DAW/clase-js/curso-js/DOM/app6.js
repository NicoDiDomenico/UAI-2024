// Lo que aprendimos hasta ahora:
var parrafoCuatro = document.createElement("p");
parrafoCuatro.textContent = "Párrafo 4"
parrafoCuatro.setAttribute("class", "parrafos");
parrafoCuatro.setAttribute("id", "parrafo4");

var clasePadre = document.querySelector(".padre");

clasePadre.appendChild(parrafoCuatro);

// Eliminar --> Tenemos 3 métodos
// removeChild()
clasePadre.removeChild(parrafoCuatro);

// remove() --> Se aplica directamente al nodo
//clasePadre.remove(); /* borraria todo */

var parrafoTres = document.querySelector("#parrafo3");
parrafoTres.remove();

// ReplaceChild
var parrafoUno = document.querySelector("#parrafo1");
var parrafoDos = document.querySelector("#parrafo2");

clasePadre.replaceChild(parrafoDos, parrafoUno);/* Al parrafo 2 lo mando al parrafo 1 sustituyendolo. */

// EVENT LISTENERS --> Lo veremos mejor en app7.js
parrafoDos.addEventListener('click', () => {
    parrafoDos.innerHTML = 'AUCH!';
});

