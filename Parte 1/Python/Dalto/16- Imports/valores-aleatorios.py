import random

# random.randint(a, b): Genera un número entero aleatorio en el rango inclusivo entre a y b. Es decir, puede generar números enteros desde a hasta b, ambos inclusive. Por ejemplo, random.randint(1, 10) podría generar cualquier número entero aleatorio entre 1 y 10, incluidos 1 y 10.
x = random.randint(1, 6)
print(x)

# random.random(): Genera un número decimal aleatorio en el rango semiabierto [0.0, 1.0). Es decir, puede generar números decimales aleatorios entre 0.0 (incluido) y 1.0 (excluido). Cada vez que llamas a random.random(), obtienes un número diferente en este rango. Por ejemplo, podrías obtener 0.4567, 0.8321, 0.1245, etc.
y = random.random()
print(y)

# random.choice(): Se utiliza para seleccionar aleatoriamente un elemento de una secuencia (como una lista, tupla o cadena).
mi_lista = ['Piedra', 'Papel', 'Tijera']
z = random.choice(mi_lista)
print(mi_lista)
print(z)

# random.shuffle(): Se utiliza para mezclar aleatoriamente los elementos de una secuencia mutable, como una lista. Modifica la secuencia en su lugar y no devuelve ningún valor explícito.
cartas = ['1', '2', '3', '4', '5', '6', '7', '8', '9', 'J', 'Q', 'K', 'A']
random.shuffle(cartas)
print(cartas)
