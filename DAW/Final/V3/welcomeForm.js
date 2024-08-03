"use strict";

var d = document;

var welcomeForm = d.querySelector(".welcomeForm"); /* Para el evento */
var boggleGame = d.querySelector(".boggleGame"); /* Para la tabla */

var nameError = d.getElementById("nameError"); /* Para validar */
var nameInput = d.getElementById("nameInput"); /* Para validar */

var rankingButton = d.getElementById("rankingButton"); /* Para el evento */
var rankingButtonMobile = d.getElementById("rankingButtonMobile"); /* Para el evento */

var listaJuegos = JSON.parse(localStorage.getItem("savegame") || "[]"); /* devuelve un arreglo de objetos o un arreglo vacio */
/* 
Resumen:
  localStorage es una API para almacenar datos en pares clave-valor en el navegador.
  JSON es un formato de texto ligero para el intercambio de datos.
  JSON.stringify() convierte un objeto JavaScript a una cadena JSON.
  JSON.parse() convierte una cadena JSON a un objeto JavaScript.
  Uso combinado: Para almacenar objetos en localStorage, conviértelos a JSON y luego parsea el JSON al recuperarlos.
*/
//Valida el nombre ingresado, cierra el formulario y abre el juego
var validateAndOpenGame = function (e) {
  e.preventDefault(); /* evito recarga de pagina */

  let valido = true;

  if (nameInput.value.trim() === "") {
    nameError.textContent = "El nombre es obligatorio";
    valido = false;
  } else if (!/^[a-zA-Z0-9 ]+$/.test(nameInput.value)) {
    nameError.textContent =
      "El nombre solo puede contener letras, números y espacios";
    valido = false;
  } else if (nameInput.value.length < 3) {
    nameError.textContent = "El nombre debe tener como minimo 3 caracteres";
    valido = false;
  } else {
    nameError.textContent = "";
  }

  if (valido) {
    // Ocultar el formulario de bienvenida y mostrar el juego
    welcomeForm.classList.add("hidden");
    boggleGame.classList.remove("hidden");
    
     // Iniciar el juego
    startGame(); /* esta función está en  boggleGame.js (1)*/
  }
};

welcomeForm.addEventListener("submit", validateAndOpenGame); 

// Mostrar ranking modal
function showRanking() {
  var tabla = crearTabla();
  Swal.fire({ /* DUDA: como sabias que esas eran las bibliotetas? en la pagina no te dice que uses esas */
    title: "Ranking",       // Título del modal
    html: tabla.outerHTML,  // Contenido HTML del modal: la tabla generada
    width: "600px",         // Ancho del modal
    showCloseButton: true,  // Mostrar el botón de cerrar en el modal --> Para mi tiene que ser false
    focusConfirm: false,    // No enfocar automáticamente el botón de confirmación --> Para mi tiene que ser true
  });
}

function crearTabla() {
  var table = document.createElement("table"); // Creo el elemento <table></table>
  var thead = document.createElement("thead"); // Creo el elemento <thead></thead>
  var tbody = document.createElement("tbody"); // Creo el elemento <tbody></tbody>

  table.id = "rankingTable"; // Asignación de un ID a la tabla
  table.appendChild(thead); // Añado <thead> a <table>
  table.appendChild(tbody); // Añado <tbody> a <table>

  // Estructura inicial:
  // <table id="rankingTable">
  //   <thead></thead>
  //   <tbody></tbody>
  // </table>

  var cabeceras = ["Usuario", "Fecha", "Puntaje", "Tiempo"];

  // Crear fila de cabecera
  var trCabecera = document.createElement("tr");
  thead.appendChild(trCabecera); // Añadir fila de cabecera a <thead>

  cabeceras.forEach(function(cabecera) {
    var th = document.createElement("th");
    th.textContent = cabecera;
    trCabecera.appendChild(th); // Añadir cada <th> a la fila de cabecera
  });

  // Estructura después de añadir cabeceras:
  // <table id="rankingTable">
  //   <thead>
  //     <tr>
  //       <th>Usuario</th>
  //       <th>Fecha</th>
  //       <th>Puntaje</th>
  //       <th>Tiempo</th>
  //     </tr>
  //   </thead>
  //   <tbody></tbody>
  // </table>

  listaJuegos.forEach(function(juego) {
    var trCuerpo = document.createElement("tr"); // Crear fila para cada juego
    tbody.appendChild(trCuerpo); // Añadir fila a <tbody>

    var tdUsuario = document.createElement("td");
    var tdFecha = document.createElement("td");
    var tdPuntaje = document.createElement("td");
    var tdTiempo = document.createElement("td");

    tdUsuario.textContent = juego.username;
    tdFecha.textContent = juego.date;
    tdPuntaje.textContent = juego.score;
    tdTiempo.textContent = juego.time == 1 ? juego.time + " minuto" : juego.time + " minutos";

    trCuerpo.appendChild(tdUsuario); // Añadir cada <td> a la fila
    trCuerpo.appendChild(tdFecha);
    trCuerpo.appendChild(tdPuntaje);
    trCuerpo.appendChild(tdTiempo);
  });

  // Estructura final:
  // <table id="rankingTable">
  //   <thead>
  //     <tr>
  //       <th>Usuario</th>
  //       <th>Fecha</th>
  //       <th>Puntaje</th>
  //       <th>Tiempo</th>
  //     </tr>
  //   </thead>
  //   <tbody>
  //     <tr>
  //       <td>Juan</td>
  //       <td>2024-07-31</td>
  //       <td>100</td>
  //       <td>5 minutos</td>
  //     </tr>
  //     <tr>
  //       <td>María</td>
  //       <td>2024-07-30</td>
  //       <td>150</td>
  //       <td>3 minutos</td>
  //     </tr>
  //     <tr>
  //       <td>Pedro</td>
  //       <td>2024-07-29</td>
  //       <td>200</td>
  //       <td>4 minutos</td>
  //     </tr>
  //   </tbody>
  // </table>

  return table;
}


rankingButton.addEventListener("click", showRanking);
rankingButtonMobile.addEventListener("click", showRanking);
