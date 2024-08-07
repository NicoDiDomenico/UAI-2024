'use strict'

//Elementos del DOM
var time = d.getElementById("time"); /* el span que que va a tener el valor del tiempo */
var gameTime = d.getElementById("game-time"); /* traigo el <select> */

var currentWordDom = d.getElementById("current-word"); /* traigo el <span> para la palabra que se va a formar cuando seleccionemos letras  */
var gameErrorMessage = d.getElementById("game-error"); /* traigo el <span> para agregarle el error segun los distintos casos */

/* var cell = d.querySelectorAll(".cell"); */ /* Para mi está de mas */
var pointsMessage = d.getElementById("points-message");

var pointsDom = d.getElementById("points");
var foundWordsContainerDom = d.getElementById("found-words-container"); /* traigo el <ul> */

var sendWordButton = d.getElementById("send-word"); /* Para evento al hacer clic en enviar palabra  */
var clearWordButton = d.getElementById("clear-word"); /* Para evento al hacer clic en limpiar palabra */

// Variables

var selectedCells = [];
var allCells = [];

var vowels = ["A", "E", "I", "O", "U"]; //Vocales
var consonants = ["B", "C","D","F","G","H","J","K","L","M","N","P","Q","R","S","T","V","W","X","Y","Z",]; //Constantes

////Contador del tiempo restante de un juego
var remainingTime;
////Almacena la palabra actual que el jugador está formando seleccionando celdas.
var currentWord = "";
////Variable para ir almacenando el puntaje de un juego
var totalScore;
////ID del intervalo para cada segundo del timer
var intervalID;
////Variable para guardar las palabras encontradas
var foundWords;

//Funcion que empieza el juego
function startGame() {
  time.classList.remove("text-red"); /* cuando se reinicia el juego (por el modal) queda en rojo el contenido del span, entonces hay que removerle la clase y asi quitar el css*/
  remainingTime = Number(gameTime.value) * 60;
  time.textContent = remainingTime;
  /* totalScore = 0; */ /* lo mandé a resetSecondaryPanel()  */
  foundWords = []

  resetSecondaryPanel() /* reseteo el puntaje y las palabras encontardas */
  resetCurrentWord(); /* reseteo la palabra seleccionada en el tablero cuando inicia o reinicio el juego */
  initializeBoard(); /* no solo inicializa el tablero sino que es lo que me permite seleccionar celdas y colorearlas a traves del evento de cada una */

  intervalID = setInterval(handleTimer, 1000); /* setInterval es una función que llama a otra función o ejecuta un fragmento de código repetidamente, con un retardo fijo entre cada llamada (1000=1 seg). */
}

//Funcion para manejar el temporizador
function handleTimer() {
  if (remainingTime === 0) {
    clearInterval(intervalID);
    showScore(); /* me lleva al modal que devuelve una promesa que se resuelve al interacturar con el modal */
    saveGameData(); // Guardo datos de la partida cuando termine --> servirá para el ranking
  }
  /* A los 10 seg pongo el span time en rojo  */
  if (remainingTime === 10) {
    time.classList.add("text-red");
  }
  time.textContent = remainingTime;
  remainingTime--; /* esto hace que se reste el tiempo */
}

//Funcion para guardar en localStorage los datos del jugador y una partida
function saveGameData() {
  var savegame = JSON.parse(localStorage.getItem("savegame") || "[]");
  savegame.push({
    username: nameInput.value, /* nameInput está en welcomeForm.js */
    score: totalScore,
    date: new Date().toLocaleString(),
    time: gameTime.value,
  });
  // new Date() crea un nuevo objeto de fecha que contiene la fecha y hora actuales.
  // .toLocaleString() es un método del objeto Date que convierte la fecha y hora en una cadena de texto usando la configuración regional específica del entorno en el que se ejecuta el código
  localStorage.setItem("savegame", JSON.stringify(savegame));
}

//Funcion que valida la palabra ingresada en una api
async function sendWord() {
  try { // Hacer algo que puede fallar
    sendWordButton.disabled = true; /* no lo termino de entender */
    //Muestro el mensaje de error de palabra menor a 3 caracteres por un ratito y lo saco
    if (currentWord.length < 3) {
      showGameErrorMessage("La palabra debe contener mas de 3 caracteres");
    } else if (foundWords.indexOf(currentWord) !== -1) { /* este if tiene sentido por addWordToFound() que te agrega la palabra al arreglo foundWords*/
      showGameErrorMessage("La palabra ya ha sido ingresada");
    } else {
      gameErrorMessage.classList.add("hidden");
      var res = await fetch("https://api.dictionaryapi.dev/api/v2/entries/en/" + currentWord); /* await espera a que la promesa de fetch se resuelva y asigna el resultado (un objeto Response) a la variable response. Response es exclusivo de API fetch --> esto permite hacer solicitudes HTTP. Notar que la URL tiene la currentWord, de esta manera se valida si la palabra que s eforma en el span es o no una palabra valida en ingles. */
      handleSubmitWord(res.ok); /* res.ok devuelve boolean, True si la solicitud a la URL especificada fue exitosa */
    }
  } catch (error) { // Manejar errores 
    handleError("Error al verificar la palabra"); // Esto se muestra si no funca la api ¿como? ni idea mostruo
  } finally { // Hacer algo siempre, ya sea que hubo error o no
    resetCurrentWord() /* reseteo la palabra seleccionada en el tablero cuando la palabra tiene < de dos letars o cuando la palabra ya la ingresaste */
    /* resetCellsStyle() */ /* es al pedo porque está en resetCurrentWord() */
  }
}

//Funcion que se encarga de realizar la logica correspondiente segun si la palabra ingresa fue correcta o incorrecta
function handleSubmitWord(isValid) {
  if (isValid) {
    if (currentWord.length === 3 || currentWord.length === 4) {
      totalScore = totalScore + 1;
      messagePoints(1);
    }
    if (currentWord.length === 5) {
      totalScore = totalScore + 2;
      messagePoints(2);
    }
    if (currentWord.length === 6) {
      totalScore = totalScore + 3;
      messagePoints(3);
    }
    if (currentWord.length === 7) {
      totalScore = totalScore + 5;
      messagePoints(5);
    }
    if (currentWord.length > 7) {
      totalScore = totalScore + 11;
      messagePoints(11);
    }
    showGameErrorMessage("Correcto!");
    addWordToFound(); /* esto tiene conexion con foundWords */
  } else {
    showGameErrorMessage("Incorrecto!")
    totalScore = totalScore - 1;
    messagePoints(-1);
  }
  pointsDom.textContent = totalScore;
  resetCurrentWord(); /* reseteo la palabra seleccionada en el tablero cuando la palabra tiene < de dos letars o cuando la palabra ya la ingresaste */
}

//Funcion general para mostrar modal de error
function handleError(msjError) {
  Swal.fire({
    position: "top",
    icon: "error",
    title: "Error!",
    text: msjError, // importante!
    width: 300,
    padding: "12",
    timer: "750", // tiempo que dura el modal
    showConfirmButton: false, // lo escondo total se va solo el modal
  });
}

//Funcion que muestra el puntaje del jugador una vez que finaliza el temporizador
function showScore() {
  Swal.fire({
    title: "Finalizó el juego",
    text: "Su puntaje: " + totalScore,
    icon: "info",
    confirmButtonColor: "#3085d6",
    confirmButtonText: "Jugar de nuevo",
    cancelButtonText: "Salir", 
    showCancelButton: true,
    cancelButtonColor: "#d33",
  }).then(function(res) { // SweetAlert2 devuelve una promesa que se resuelve cuando el usuario interactúa con la ventana emergente.
    if (res.isConfirmed) {// .isConfirmed Esa propiedad es específica del objeto de resultado de las promesas devueltas por SweetAlert2.
      startGame();
    } else {
      window.location.replace("/");  // redirige al usuario a la página principal ("/").
    }
  });
}

//Funcion para resetar la palabra ingresada
/* 
Casos que la funcion es llamada:
  - Inicio del Juego (startGame()): Se llama resetCurrentWord() para asegurarse de que la palabra actual esté vacía y que las celdas seleccionadas y los estilos de las celdas se reinicien antes de que comience una nueva partida.
  - Después de Enviar una Palabra (sendWord()): Independientemente de si la palabra es válida o no, resetCurrentWord() se llama para reiniciar la palabra actual y las celdas seleccionadas después de intentar enviar una palabra.
  - Al Hacer Clic en el Botón de Limpiar Palabra (clearWordButton.addEventListener("click", resetCurrentWord)): Cuando el usuario hace clic en el botón "Limpiar palabra", se llama resetCurrentWord() para vaciar la palabra actual y reiniciar las celdas seleccionadas.
  - ... (falta creo)
  */
function resetCurrentWord() {
  currentWord = ""; /* Es una variable global que almacena la palabra actual que el jugador está formando seleccionando celdas. */
  selectedCells = [] /*  Es una variable global que almacena un array de las celdas actualmente seleccionadas por el jugador. */
  currentWordDom.textContent = currentWord;
  /* gameErrorMessage.classList.add("hidden"); */ /* Agregarlo da error */
  resetCellsStyle()
}

//Funcion que muestra el puntaje segun una palabra ingresada
function messagePoints(points) {
  if (points > 0) {
    pointsMessage.textContent = "+" + points + " puntos!";
    pointsMessage.style.color = "#00798C"; /* "Verde" */
    setTimeout(function() {
      pointsMessage.textContent = "";
    }, 1200);
  } else {
    pointsMessage.textContent = points + " puntos!";
    pointsMessage.style.color = "#d1495b"; /* "Rojo" */
    setTimeout(function() {
      pointsMessage.textContent = "";
    }, 1200);
  }
}

//Funcion que muestra error del juego abajo del tablero
function showGameErrorMessage(msg) {
  gameErrorMessage.classList.remove("hidden");
  gameErrorMessage.textContent = msg;
  if (msg === "Correcto!"){
    gameErrorMessage.style.color = "#00798C"; /* "Verde" */ 
  }
  setTimeout(function() {
    gameErrorMessage.classList.add("hidden");
    gameErrorMessage.textContent = "";
    gameErrorMessage.style.removeProperty('color'); /* esto lo agregue para quitar lo verde que puse antes y se respete el css con rojo */
  }, 1500);
  return;
}

//Funcion que agrega la palabra al arreglo de palabras encontradas y tambien se muestra en el dom
function addWordToFound() {
  foundWords.push(currentWord); /* foundWord es un arreglo para comparar en el try de sendWord si la palabra ya la ingresé antes*/
  /* Ahora agrego la palabra al dom del contenedor found-words*/
  var liFoundWordElement = d.createElement("li");
  liFoundWordElement.textContent = currentWord;
  foundWordsContainerDom.appendChild(liFoundWordElement);
}

//Funcion que inicializa el tablero
function initializeBoard() {
  /* selectedCells = []; */ /* LO HACE resetCurrentWord() */
  allCells = []; 
  /* resetCellsStyle(); */ /* LO HACE resetCurrentWord() */

  
  // Selecciona 6 vocales aleatorias
  var selectedVowels = [];
  for (var i = 0; i < 6; i++) {
    selectedVowels.push(vowels[Math.floor(Math.random() * vowels.length)]);
    //                         Math.floor --> redeondea para abajo
    //                                    Math.random() --> Devuelve un numero real entre 0 y 1 (sin incluir)
    //                                                    vowels.length --> Me devuevle el tamaño del arreglo vowels
  }

  // Selecciona exactamente 10 consonantes aleatorias
  var selectedConsonants = [];
  for (var i = 0; i < 10; i++) {
    selectedConsonants.push(consonants[Math.floor(Math.random() * consonants.length)]);
    // misma logica que antes pero ahroa con las consonantes
  }

  // Concateno las vocales y consonantes para luego mezclarlas todas aleatoriamente en boardLetters 
  var boardLetters = selectedVowels.concat(selectedConsonants);
  boardLetters = boardLetters.sort(function() { /* En sort se usa una funcion de comparacion que devuelve un valor +,0,- que determinara en que orden colocar los los valores */
    return Math.random() - 0.5; /* Math.random() - 0.5 hace que obtenga valores entre [-0.5, 5) y no [0, 1) */
  });
  // Ahora las coloco en el tablero
  for (var i = 1; i <= 16; i++) {
    var cell = d.getElementById("cell-" + i);
    cell.textContent = boardLetters[i - 1];
    cell.addEventListener("click", handleCellClick);
    allCells.push(cell); /* tambien las coloco en este arreglo para mas adelante hacer valdiaciones */
  }
}  

// Maneja el click en una celda del tablero
function handleCellClick(event) {
  var cell = event.target; // obtiene el elemento que fue clicado. En este caso, es una celda del tablero. Recordar event es un objeto que pasa por el handler, y como objeto tiene propeidades propias como .tarjet o .type 
  console.log(event.target); // Notar como traigo la etiqueta <button>...</button>, esto es gracias al .target
  
  // Verificar si la celda ya está seleccionada
  if (selectedCells.indexOf(cell) !== -1) { /* si ya ingrese la letra salgo del handler */
    return;
  } // si es verde no continuo el codigo

  // Verificar si la celda clicada es adyacente a la última celda seleccionada --> 2)
  if (selectedCells.length > 0) { 
    var lastCell = selectedCells[selectedCells.length - 1];
    // 1)
    if(cell.classList.contains("able-to-select")){
      lastCell.classList.remove("last-selected"); // le quita el outline
    }
    // 2) 
    if (!isAdjacent(lastCell, cell)) {
      return; // CREO que si es azul o verde retorno (es decir no es naranja)...
    }// ...pero si es naranja continuo el codigo
  } 
  // hasta acá sé que cell es adyacente y no está seleccionada (no es verde)

  // Restablece el color original de todas las celdas adyacentes no seleccionadas
  allCells.forEach(function (adjacentCell) { // array.forEach(function(elementFromArray, index, array) {...}
    if (selectedCells.indexOf(adjacentCell) === -1) {
      adjacentCell.classList.remove("able-to-select");
    }
  });
  // de acá salgo con el tablero sin color A EXEPCION de las selecionadas (las verdes)
  // y con la cell adyacente y no seleccionada

  // A cell la definio como seleccionada
  cell.classList.add("selected");
  cell.classList.add("last-selected")
  selectedCells.push(cell);
  
  // Voy definiendo dinamicamente currentWord y el contenido del dom en que se encuentra
  currentWord += cell.textContent;
  currentWordDom.textContent = currentWord;

  // Obtiene las celdas adyacentes y marca las seleccionables
  // Obtengo las celdas adyacentes a la cliqueeada
  var adjacentCells = getAdjacentCells(cell);
  //  Las "pinto" de naranja --> able-to-select
  adjacentCells.forEach(function (adjacentCell) {
    if (selectedCells.indexOf(adjacentCell) === -1) {
      adjacentCell.classList.add("able-to-select");
    }
  });
}

// Obtiene las celdas adyacentes a una celda dada (la del evento)
function getAdjacentCells(cell) {
  var index = allCells.indexOf(cell);
  var row = Math.floor(index / 4);
  var col = index % 4;

  // adjacentCells es un array vacío que llenaremos con las celdas adyacentes.
  var adjacentCells = [];

  // Verifica las celdas adyacentes en la matriz 4x4
  /* 
  Math.max(num1, num2) --> devuelve el maximo (establezco la minima fila/columna para iterar)
  Math.min(num1, num2) --> devuelve el minimo (establezco la maxima fila/columna para iterar)
  */
  for (var i = Math.max(0, row - 1); i <= Math.min(row + 1, 3); i++) { // itero filas
    for (var j = Math.max(0, col - 1); j <= Math.min(col + 1, 3); j++) { // itero columnas
      if (!(i === row && j === col)) { // asegura que no incluimos la celda actual en el array de celdas adyacentes. Solo queremos celdas adyacentes.
        adjacentCells.push(allCells[i * 4 + j]); // si haces las cuentas te da la posicion de la cerlda adyacente
      }
    }
  }

  return adjacentCells;
}

// Verifica si dos celdas son adyacentes en el tablero (si la celda cliqueada es adyacente a la ultima celda selecionada)
function isAdjacent(lastCell, cell) {
  /* 
  Antes hay que entender:
  El tablero 4x4 tiene indices del 0 al 15:
    0  1  2  3
    4  5  6  7
    8  9 10 11
    12 13 14 15
  - allCells = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15]

  Cada celda se puede identificar por su fila y columna. Por ejemplo:
    La celda 0 está en la fila 0, columna 0.
    La celda 5 está en la fila 1, columna 1.
    La celda 14 está en la fila 3, columna 2.

  Para encontrar la fila de una celda con índice i, usamos:
    var row = Math.floor(i / 4);

  Para encontrar la columna de una celda con índice i, usamos:
    var col = i % 4;
  */
  // Obtener los índices de las celdas en el array allCells
  var lastCellIndex = allCells.indexOf(lastCell);
  var cellIndex = allCells.indexOf(cell);

  // Calcular la fila y la columna de lastCell
  var lastCellRow = Math.floor(lastCellIndex / 4);
  var lastCellCol = lastCellIndex % 4;

  // Calcular la fila y la columna de cell
  var cellRow = Math.floor(cellIndex / 4);
  var cellCol = cellIndex % 4;

  // Verificar si las celdas son adyacentes
  /* 
  lastCellRow - cellRow calcula la diferencia entre las filas de las dos celdas.
  
  Math.abs(...) toma el valor absoluto de esa diferencia, eliminando cualquier signo negativo. Esto es importante porque queremos la distancia absoluta entre las filas, sin importar el orden.
  
  <= 1 verifica si esta diferencia absoluta es menor o igual a 1. Si es así, significa que las filas están una al lado de la otra o son la misma fila.
  */
  var rowsAreAdjacent = Math.abs(lastCellRow - cellRow) <= 1;
  var colsAreAdjacent = Math.abs(lastCellCol - cellCol) <= 1;

  return rowsAreAdjacent && colsAreAdjacent; 
}

//Funcion que reinicia todos los estilos de las celdas en caso de jugar de nuevo
function resetCellsStyle() {
  for (var i = 1; i <= 16; i++) {
    var cell = d.getElementById("cell-" + i); /* traigo la celda */
    cell.classList.remove("selected"); /* remuevo esta clase que se agrego antes dinamicamente para darle estilo */
    cell.classList.remove("last-selected"); /*                           ||                                      */
    cell.classList.remove("able-to-select"); /*                          ||                                      */
    cell.disabled = false; /* habilito la celda */
  }
}

//Funcionar para reiniciar el puntaje y las palabras encontradas de la partida anterior
function resetSecondaryPanel() {
  totalScore = 0;
  pointsDom.textContent = totalScore;
  //Elimino todos las palabras encontradas del dom
  /* while(foundWordsContainerDom.firstChild) {
    foundWordsContainerDom.firstChild.remove()
  } */ foundWordsContainerDom.innerHTML = '';
}

//Eventos
sendWordButton.addEventListener("click",sendWord); /* Envio la palabra para hacer todas las valdiaciones y cambios durante el jeugo */
clearWordButton.addEventListener("click", resetCurrentWord); /* acá limpio cuando apreto limpiar */
