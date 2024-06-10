// Aprendiendo createElement() appendChild() append()
// CREAR NODOS

// Forma 1
var parrafoCuatro = document.createElement("p");
var textoParrafoCuatro = document.createTextNode("Parrafo 4");

// Forma 2
var parrafoCinco = document.createElement("p");
/* parrafoCinco.innerHTML = "Parrafo 5"; */
parrafoCinco.textContent = "Parrafo 5";

var parrafoCero = document.createElement("p");
parrafoCero.textContent = "Parrafo 0";

var parrafoExtra = document.createElement("p");
parrafoExtra.textContent = "Parrafo Extra";

// Selecciono el elemento padre al que quiero agregar el nuevo parrafo

var elementoPadre = document.querySelector(".padre");

var parrafoUno = document.querySelector("#parrafo1")

// AGREGAR NODOS

// - AppendChild (agrego un solo elemento)
elementoPadre.appendChild(parrafoCuatro)
parrafoCuatro.appendChild(textoParrafoCuatro);

elementoPadre.appendChild(parrafoCinco)
// - Append (me permite agregar varios elementos)
/* elementoPadre.append(parrafoCuatro,parrafoCinco) */

// insertBefore (agrego un elemento antes de otro)

elementoPadre.insertBefore(parrafoCero, parrafoUno)

// insertAdjacentElement() - ..begin y ...end hace referencia a las etiquetas de apertura y cierre de un determinado elemento padre y tiene 4 parámetos:
// - beforebegin
elementoPadre.insertAdjacentElement("beforebegin", parrafoExtra); /* se agrega antes de la etiqueta de apertura de la clase padre */
// - afterbegin
elementoPadre.insertAdjacentElement("afterbegin", parrafoExtra); /* se agrega despues de la etiqueta de apertura de la clase padre */
// - beforeend
elementoPadre.insertAdjacentElement("beforeend", parrafoExtra); /* se agrega antes de la etiqueta de cierre de la clase padre */
// - afterend
elementoPadre.insertAdjacentElement("afterend", parrafoExtra); /* se agrega despues de la etiqueta de cierre de la clase padre */

// ATRIBUTOS
// setAttribute() - con este mé todo le agrego el atibuto a los elementos que cree y agregue a la clase padre
parrafoCero.setAttribute("class", "parrafos");
parrafoCuatro.setAttribute("class", "parrafos");
parrafoCinco.setAttribute("class", "parrafos");
parrafoExtra.setAttribute("class", "parrafos");

parrafoCero.setAttribute("id", "parrafo0");
parrafoCuatro.setAttribute("id", "parrafo4");
parrafoCinco.setAttribute("id", "parrafo5");
parrafoExtra.setAttribute("id", "parrafoExtra");

// Ver https://www.youtube.com/watch?v=XHFg0yzrHhE&list=PLJubkp8BnTJsDgWXWcS1Z0VDV7rAOqbU4&index=26&ab_channel=CodingTube