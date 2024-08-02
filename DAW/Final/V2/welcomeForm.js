"use strict";

var d = document;

var welcomeForm = d.querySelector(".welcomeForm"); /* Para el evento */
var boggleGame = d.querySelector(".boggleGame"); /* Para la tabla */

var nameError = d.getElementById("nameError"); /* Para validar */
var nameInput = d.getElementById("nameInput"); /* Para validar */

var gameTime = d.getElementById("game-time"); /* Traigo el <select> del Formulario de bienvenida  */

var rankingButton = d.getElementById("rankingButton"); /* Para el evento */
var rankingButtonMobile = d.getElementById("rankingButtonMobile"); /* Para el evento */

var listaJuegos = JSON.parse(localStorage.getItem("savegame") || "[]");

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
    welcomeForm.classList.add("hidden");
    boggleGame.classList.remove("hidden");
    startGame();
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
  var tabla = document.createElement("table"); /* creo el elemento <table> </table> */
  var thead = tabla.createTHead(); /* creo en la tabla <thead></thead> */
  var tbody = tabla.createTBody(); /* creo en la tabla <tbody></tbody> */
  /* 
    <table>
      <thead></thead>
      <tbody></tbody>
    </table>
  */
  tabla.id = "rankingTable"; /* Asignación de un ID a la Tabla */
  /* 
    <table id="rankingTable">
      <thead></thead>
      <tbody></tbody>
    </table>
  */
  var cabeceras = ["Usuario", "Fecha", "Puntaje", "Tiempo"];
  var filaCabecera = thead.insertRow(); /* En <thead></thead> inserto <tr></tr> (fila)*/
  /* 
    <table id="rankingTable">
      <thead>
          <tr></tr>
      </thead>
      <tbody></tbody>
    </table>
  */

  cabeceras.forEach((cabecera) => { /* Creación de las Celdas de Encabezado */
    var th = document.createElement("th");
    th.textContent = cabecera;
    filaCabecera.appendChild(th);
  });
  /* 
    <table id="rankingTable">
      <thead>
        <tr>
          <th>Usuario</th>
          <th>Fecha</th>
          <th>Puntaje</th>
          <th>Tiempo</th>
        </tr>
      </thead>
      <tbody></tbody>
    </table>
  */
  listaJuegos.forEach((juego) => {
    var fila = tbody.insertRow();
    var celdaUsuario = fila.insertCell(0);
    var celdaFecha = fila.insertCell(1);
    var celdaPuntaje = fila.insertCell(2);
    var celdaTiempo = fila.insertCell(3);

    celdaUsuario.textContent = juego.username;
    celdaFecha.textContent = juego.date;
    celdaPuntaje.textContent = juego.score;
    celdaTiempo.textContent =
      juego.time == 1 ? juego.time + " minuto" : juego.time + " minutos";
  });
  /*
    Estructura de la tabla después de añadir las filas de datos:
    <table id="rankingTable">
      <thead>
        <tr>
          <th>Usuario</th>
          <th>Fecha</th>
          <th>Puntaje</th>
          <th>Tiempo</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>Juan</td>
          <td>2024-07-31</td>
          <td>100</td>
          <td>5 minutos</td>
        </tr>
        <tr>
          <td>María</td>
          <td>2024-07-30</td>
          <td>150</td>
          <td>3 minutos</td>
        </tr>
        <tr>
          <td>Pedro</td>
          <td>2024-07-29</td>
          <td>200</td>
          <td>4 minutos</td>
        </tr>
      </tbody>
    </table>
  */
  return tabla;
}

rankingButton.addEventListener("click", showRanking);
rankingButtonMobile.addEventListener("click", showRanking);
