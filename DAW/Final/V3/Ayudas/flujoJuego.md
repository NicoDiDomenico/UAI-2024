startGame
├── resetSecondaryPanel <!-- borro el puntaje y las palabras encontradas de la partida anterior -->
├── resetCurrentWord <!-- borro la palabra que se va armando al seleccionar celdas  -->
│   ├── resetCellsStyle <!-- borro el estilo de las celdas seleccionadas -->
├── initializeBoard
│   ├── resetCellsStyle <!--  -->
│   ├── handleCellClick (para cada celda)
│       ├── isAdjacent (si hay celdas seleccionadas)
│       ├── getAdjacentCells (si hay celdas seleccionadas)
├── handleTimer (cada segundo)
    ├── if remainingTime === 0
    │   ├── showScore
            ├──startGame
    │   ├── saveGameData
    ├── if remainingTime === 10
        ├── Cambia el color de texto a rojo

sendWord <!--  -->
├── showGameErrorMessage (si currentWord.length < 3) <!--  -->
├── showGameErrorMessage (si foundWords.includes(currentWord)) <!--  -->
├── if response.ok
│   ├── handleSubmitWord(true) <!--  -->
│       ├── messagePoints (si válido/no válido) <!--  -->
        ├── showGameErrorMessage (si válido/no válido) <!--  -->
│       ├── addWordToFound (si válido) 
│       ├── resetCurrentWord <!--  -->
│           ├── resetCellsStyle <!--  -->
├── catch error
│   ├── handleError <!-- a un modal -->
├── finally
    ├── resetCurrentWord <!--  -->
        ├── resetCellsStyle <!--  -->

sendWordButton.addEventListener("click",sendWord);
clearWordButton.addEventListener("click", resetCurrentWord);
