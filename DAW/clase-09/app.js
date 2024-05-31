console.log(document.title) /* Me treae todo el HTML del VSC */

document.title = "To do list" /* lo cambio dinamicamente al title del HTML */

/* GetElementById */
var title = document.getElementById("title") /* obtengo el elemento con ese id, mirar el tipo de dato --> es un elemento HTML lo que traigo */
console.log(title)

console.log(title.textContent) /* me muestra el texto y no el HTML, entonces lo puedo manipular como string */
title.textContent = "LISTA DE QUEHACERES" /* usa literalmente ese texto */
title.innerHTML = "<span>LISTA DE QUEHACERES</span>" /* reeplaza las etiquetas en el HTML */
title.style.backgroundColor = "red" /* cambio el estilo de mi CSS/HTML */

/* GetElementsByClassName */
var tasks = Document.getElementByClassName("task");
console.log(tasks); /* devuelve un html collection y no un array */
console.log(tasks[0]); /* a fines practicos si lo usamos como un array */ 
console.log(tasks[1].textContent = "hola"); /* tambien lo podemos manipular */

for (var index = 0; index < tasks.length; index++){
    var element = tasks[index];
    element.style.color="red" /* NO ENTENDI QUE DIJO ACA, ALGO DE DEBUGUEAR */
}

/* GetElementsByTagName */
var tasks = Document.getElementByTagName("li"); /* traigo todos los li */
console.log(tasks); 
console.log(tasks[1]); 


for (var index = 0; index < tasks.length; index++){
    var element = tasks[index];
    element.style.color="red" 
}

/* GetElementsByName */
console.log(document.getElementByName("newTask"))
var input = document.getElementsByName("newTask")

/* querySelector y querySelectorAll */
var element = document.querySelector("title") /* querySelector trae el primer elemento que encuentre, el All trae todo */
var element = document.querySelector("#taskList li")
console.log(element)

/* CHILD */
var list = document.querySelector("ul");
console.log(list.chilNodes) /* Este no se usa */
console.log(list.children) /* Este si */
console.log(list.firsChild)
console.log(list.firsElementChild)
console.log(list.lastChild)
console.log(list.lastElementChild)

/* SIBLING */
var list = document.querySelector("li");
console.log(list.nextSibling)
console.log(list.nextElementSibling)
console.log(list.previusSibling)
console.log(list.previusElementSibling)

/* PERENT */
var list = document.querySelector("li");
console.log(list.parentElement)
console.log(list.parentNode)

/* metodos para agregar o eliminar elementos */
var newTask = document.createElement("li")
console.log(newTask.className)
newTask.className = "task"
console.log(newTask)
newTask.textContent = "Lavar los platos"

var list = document.querySelector("ul")
list.appendChild(newTask)
list.removeChild(document.querySelector("#firstTask"))
list.replaceChild(newTask, document.querySelector("#firstTask"))
list.insertAdjacentElement("afterbegin", newTask)
/* 
beforebegin
<ul>
afterbegin
beforend
</ul>
afterend
*/

/* LISTENERS */
var button = document.getElementById("button")

/* button.addEventListener("click", function(){
    console.log("CLICK")
}) */
button.addEventListener("click", clickButton); /* en vez de clic puedo poner DoubleClic (no se bien como se escribia)*/


function clickButton(e) {
    console.log(e.target) /* muestra el elemento al que le hacemos clic */
}

var input = document.getElementById("newTaskInput");

input.addEventListener("focus", onEvent); /* puedo usar "blur", "keypress", "r" */ /* Esto lo vamos a suar mucho en el formulario de tarea */

function onEvent(e) {
    console.log(e.target.value) /* vemos el valor de nuestro elemento objetivo */
    document.querySelector("li").textContent = e.target.value
}