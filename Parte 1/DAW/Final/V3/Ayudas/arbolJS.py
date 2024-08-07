from graphviz import Digraph

# Crear un diagrama dirigido
dot = Digraph(comment='Flujo de Ejecución de Funciones')

# Definir nodos para las funciones principales
dot.node('startGame', 'startGame')
dot.node('resetSecondaryPanel', 'resetSecondaryPanel')
dot.node('resetCurrentWord', 'resetCurrentWord')
dot.node('initializeBoard', 'initializeBoard')
dot.node('handleTimer', 'handleTimer')
dot.node('showScore', 'showScore')
dot.node('saveGameData', 'saveGameData')
dot.node('sendWord', 'sendWord')
dot.node('showGameErrorMessage', 'showGameErrorMessage')
dot.node('handleSubmitWord', 'handleSubmitWord')
dot.node('addWordToFound', 'addWordToFound')
dot.node('resetCellsStyle', 'resetCellsStyle')
dot.node('messagePoints', 'messagePoints')
dot.node('handleError', 'handleError')
dot.node('handleCellClick', 'handleCellClick')
dot.node('getAdjacentCells', 'getAdjacentCells')
dot.node('isAdjacent', 'isAdjacent')

# Añadir las relaciones entre nodos
dot.edge('startGame', 'resetSecondaryPanel')
dot.edge('startGame', 'resetCurrentWord')
dot.edge('startGame', 'initializeBoard')
dot.edge('startGame', 'handleTimer', label='setInterval')
dot.edge('handleTimer', 'showScore', label='if remainingTime === 0')
dot.edge('handleTimer', 'saveGameData', label='if remainingTime === 0')
dot.edge('sendWord', 'showGameErrorMessage', label='if currentWord.length < 3')
dot.edge('sendWord', 'showGameErrorMessage', label='if foundWords.includes(currentWord)')
dot.edge('sendWord', 'handleSubmitWord', label='if response.ok')
dot.edge('sendWord', 'handleError', label='catch error')
dot.edge('sendWord', 'resetCurrentWord', label='finally')
dot.edge('sendWord', 'resetCellsStyle', label='finally')
dot.edge('handleSubmitWord', 'messagePoints', label='if valid')
dot.edge('handleSubmitWord', 'addWordToFound', label='if valid')
dot.edge('handleSubmitWord', 'resetCurrentWord')
dot.edge('handleCellClick', 'isAdjacent', label='if selectedCells.length > 0')
dot.edge('handleCellClick', 'getAdjacentCells', label='if selectedCells.length > 0')

# Mostrar el diagrama
dot.render('/mnt/data/flujo_de_ejecucion', format='png', view=True)

dot.source
