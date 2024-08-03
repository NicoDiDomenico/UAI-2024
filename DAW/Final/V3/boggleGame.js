'use strict'

//Elementos del DOM
var time = d.getElementById("time"); /* el span que que va a tener el valor del tiempo */
var gameTime = d.getElementById("game-time"); /* traigo el <select> */

var currentWordDom = d.getElementById("current-word"); /* traigo el <span> para la palabra que se va a formar cuando seleccionemos letras  */
var gameErrorMessage = d.getElementById("game-error"); /* traigo el <span> para agregarle el error segun los distintos casos */

var cell = d.querySelectorAll(".cell");
var pointsMessage = d.getElementById("points-message");

var pointsDom = d.getElementById("points");
var foundWordsContainerDom = d.getElementById("found-words-container"); /* traigo el <ul> */

var sendWordButton = d.getElementById("send-word"); /* Para evento al hacer clic en enviar palabra  */
var clearWordButton = d.getElementById("clear-word"); /* Para evento al hacer clic en limpiar palabra */

var selectedCells = [];
var allCells = [];

//Constantes
var vowels = ["A", "E", "I", "O", "U"];
var consonants = [
  "B",
  "C",
  "D",
  "F",
  "G",
  "H",
  "J",
  "K",
  "L",
  "M",
  "N",
  "P",
  "Q",
  "R",
  "S",
  "T",
  "V",
  "W",
  "X",
  "Y",
  "Z",
];

// Declaracion de variables
////Contador del tiempo restante de un juego
var remainingTime;
////Almacena la palabra actual que el jugador está formando seleccionando celdas.
var currentWord = "";
////Variable para ir almacenando el puntaje de un juego
var totalScore;
////Intervalo para cada segundo del timer
var timer;
////Variable para guardar las palabras encontradas
var foundWords;

//Funcion que empieza el juego
function startGame() {
  time.classList.remove("text-red"); /* cuando se reinicia el juego (ver modal) queda en rojo el contenido del span, entonces hay que removerle la clase y asi quitar el css*/
  remainingTime = Number(gameTime.value) * 60;
  time.textContent = remainingTime;
  totalScore = 0;
  foundWords = []

  resetSecondaryPanel() /* reseteo el puntaje y las palabras encontardas */
  resetCurrentWord(); /* reseteo la palabra seleccionada en el tablero cuando inicia o reinicio el juego */
  initializeBoard();

  timer = setInterval(handleTimer, 1000);
}

//Funcion para manejar el temporizador
function handleTimer() {
  if (remainingTime === 0) {
    clearInterval(timer);
    showScore();
    saveGameData();
  }
  /* A los 10 seg pongo el span time en rojo  */
  if (remainingTime === 10) {
    time.classList.add("text-red");
  }
  time.textContent = remainingTime;
  remainingTime--;
}

//Funcion para guardar en localStorage los datos del jugador y una partida
function saveGameData() {
  var savegame = JSON.parse(localStorage.getItem("savegame") || "[]");
  savegame.push({
    username: nameInput.value,
    score: totalScore,
    date: new Date().toLocaleString(),
    time: gameTime.value,
  });
  var formatedSavegame = JSON.stringify(savegame);
  localStorage.setItem("savegame", formatedSavegame);
}

//Funcion que valida la palabra ingresada en una api
async function sendWord() {
  try { // Hacer algo que puede fallar
    sendWordButton.disabled = true; /* no lo termino de entender */
    //Muestro el mensaje de error de palabra menor a 3 caracteres por un ratito y lo saco
    if (currentWord.length < 3) {
      showGameErrorMessage("La palabra debe contener mas de 3 caracteres");
    } else if (foundWords.includes(currentWord)) { /* este if tiene sentido por addWordToFound() que te agrega la palabra al arreglo foundWords*/
      showGameErrorMessage("La palabra ya ha sido ingresada");
    } else {
      gameErrorMessage.classList.add("hidden");
      var response = await fetch(`https://api.dictionaryapi.dev/api/v2/entries/en/${currentWord}`); /* await espera a que la promesa de fetch se resuelva y asigna el resultado (un objeto Response) a la variable response. Response es exclusivo de API fetch --> esto permite hacer solicitudes HTTP. Notar que la URL tiene la currentWord, de esta manera se valida si la palabra que s eforma en el span es o no una palabra valida en ingles. */
      handleSubmitWord(response.ok); /* res.ok devuelve boolean, True si la solicitud a la URL especificada fue exitosa */
    }
  } catch (error) { // Manejar errores 
    handleError("Error al verificar la palabra"); // Esto se muestra si no funca la api ¿como? ni idea mostruo
  } finally { // Hacer algo siempre, ya sea que hubo error o no
    sendWordButton.disabled = false;
    resetCurrentWord() /* reseteo la palabra seleccionada en el tablero cuando la palabra tiene < de dos letars o cuando la palabra ya la ingresaste */
    resetCellsStyle()
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
    addWordToFound(); /* esto tiene conexion con foundWords */
  } else {
    totalScore = totalScore - 1;
    messagePoints(-1, "#d1495b");
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
    text: msjError,
    width: 300,
    padding: "12",
    timer: "750",
    showConfirmButton: false,
  });
}

//Funcion que muestra el puntaje del jugador una vez que finaliza el temporizador
function showScore() {
  Swal.fire({
    title: "Finalizó el juego",
    text: `Su puntaje: ${totalScore}`,
    icon: "info",
    confirmButtonColor: "#3085d6",
    confirmButtonText: "Jugar de nuevo",
    showCancelButton: true,
    cancelButtonColor: "#d33",
  }).then((result) => {
    if (result.isConfirmed) {
      startGame();
    } else {
      window.location.replace("/");
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
  /* gameErrorMessage.classList.add("hidden"); */ /* solo se que si lo saco anda */
  resetCellsStyle()
}

//Funcion que muestra el puntaje segun una palabra ingresada
function messagePoints(points, color = "#00798C") {
  if (points > 0) {
    pointsMessage.textContent = `+${points} puntos!`;
    pointsMessage.style.color = color;
    setTimeout(() => {
      pointsMessage.textContent = "";
    }, 1200);
  } else {
    pointsMessage.textContent = `${points} puntos!`;
    pointsMessage.style.color = color;
    setTimeout(() => {
      pointsMessage.textContent = "";
    }, 1200);
  }
}

//Funcion que muestra error del juego abajo del tablero
function showGameErrorMessage(msg) {
  gameErrorMessage.classList.remove("hidden");
  gameErrorMessage.textContent = msg;
  setTimeout(() => {
    gameErrorMessage.classList.add("hidden");
    gameErrorMessage.textContent = "";
  }, 1500);
  return;
}

//Funcion que agrega la palabra encontrada a la lista de palabras encontradas y tambien lo muestra en el dom
function addWordToFound() {
  foundWords.push(currentWord);
  var liFoundWordElement = d.createElement("li");
  liFoundWordElement.textContent = currentWord;
  foundWordsContainerDom.appendChild(liFoundWordElement);
}

//Funcion que inicializa el tablero
function initializeBoard() {
  selectedCells = [];
  allCells = [];
  resetCellsStyle();

  // Selecciona 6 vocales aleatorias
  var selectedVowels = [];
  for (var i = 0; i < 6; i++) {
    selectedVowels.push(vowels[Math.floor(Math.random() * vowels.length)]);
  }

  // Selecciona exactamente 10 consonantes aleatorias
  var selectedConsonants = [];
  for (var i = 0; i < 10; i++) {
    selectedConsonants.push(
      consonants[Math.floor(Math.random() * consonants.length)]
    );
  }

  // Combina y mezcla las letras
  var boardLetters = selectedVowels.concat(selectedConsonants);
  boardLetters = boardLetters.sort(() => Math.random() - 0.5);

  for (let i = 1; i <= 16; i++) {
    var cell = d.getElementById(`cell-${i}`);
    cell.textContent = boardLetters[i - 1];
    cell.addEventListener("click", handleCellClick);
    allCells.push(cell);
  }
}

// Maneja el click en una celda del tablero
function handleCellClick(event) {
  var cell = event.target;

  if (selectedCells.includes(cell)) {
    return;
  }

  if (selectedCells.length > 0) {
    var lastCell = selectedCells[selectedCells.length - 1];
    lastCell.classList.remove("last-selected");
    if (!isAdjacent(lastCell, cell)) {
      return;
    }
  }

  // Restablece el color original de todas las celdas adyacentes no seleccionadas
  allCells.forEach(function (adjacentCell) {
    if (!selectedCells.includes(adjacentCell)) {
      adjacentCell.classList.remove("able-to-select");
    }
  });

  // Añade las clases 'selected'
  cell.classList.add("selected");
  cell.classList.add("last-selected")
  selectedCells.push(cell);
  currentWord += cell.textContent;
  currentWordDom.textContent = currentWord;

  // Obtiene las celdas adyacentes y marca las seleccionables
  var adjacentCells = getAdjacentCells(cell);
  adjacentCells.forEach(function (adjacentCell) {
    if (!selectedCells.includes(adjacentCell)) {
      adjacentCell.classList.add("able-to-select");
    }
  });
}

// Obtiene las celdas adyacentes a una celda dada
function getAdjacentCells(cell) {
  var index = allCells.indexOf(cell);
  var row = Math.floor(index / 4);
  var col = index % 4;
  var adjacentCells = [];

  // Verifica las celdas adyacentes en la matriz 4x4
  for (var i = Math.max(0, row - 1); i <= Math.min(row + 1, 3); i++) {
    for (var j = Math.max(0, col - 1); j <= Math.min(col + 1, 3); j++) {
      if (!(i === row && j === col)) {
        // No incluir la celda actual
        adjacentCells.push(allCells[i * 4 + j]);
      }
    }
  }

  return adjacentCells;
}

// Verifica si dos celdas son adyacentes en el tablero
function isAdjacent(cell1, cell2) {
  var index1 = allCells.indexOf(cell1);
  var index2 = allCells.indexOf(cell2);
  var row1 = Math.floor(index1 / 4);
  var col1 = index1 % 4;
  var row2 = Math.floor(index2 / 4);
  var col2 = index2 % 4;
  return Math.abs(row1 - row2) <= 1 && Math.abs(col1 - col2) <= 1;
}

//Funcion que reinicia todos los estilos de las celdas en caso de jugar de nuevo
function resetCellsStyle() {
  for (let i = 1; i <= 16; i++) {
    var cell = d.getElementById(`cell-${i}`);
    cell.classList.remove("selected");
    cell.classList.remove("last-selected");
    cell.classList.remove("able-to-select");
    cell.disabled = false;
  }
}

//Funcionar para reiniciar el puntaje y las palabras encontradas de la partida anterior
function resetSecondaryPanel() {
  pointsDom.textContent = totalScore
  //Elimino todos las palabras encontradas del dom
  /* while(foundWordsContainerDom.firstChild) {
    foundWordsContainerDom.firstChild.remove()
  } */ foundWordsContainerDom.innerHTML = '';
}

//Eventos
sendWordButton.addEventListener("click",sendWord);
clearWordButton.addEventListener("click", resetCurrentWord); /* acá limpio cuando apreto limpiar */
