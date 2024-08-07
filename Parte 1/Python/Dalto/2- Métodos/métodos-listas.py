# LIST - crea una lista
print('LIST:')
lista = list() # Es útil para crear listas vacia

# lista = list(['Hola', 1])  # No tiene mucho sentido porque haces como la linea 7 y listo
print(lista)
lista = ['Hola', 1]
print(f'La lista es: {lista}\n') 

# LEN - cuenta la cantidad de elementos en una lista 
print('LEN:')
cantidad_elementos = len(lista)
print(f'Cantidad elementos del arreglo: {cantidad_elementos}\n')


# MÉTODOS PARA AGREGAR ELEMENTOS

# APPEND - agrega un elemento a la lista 
print('APPEND:')
lista.append(2) # notar que no requiere definir una variable
print(f'lista actualizada: {lista}\n')
# Datazo: Podemos agregar tuplas en listas

# INSERT - agrega un elemento a la lista en el indice especificado  
print('INSERT:')
lista.insert(2,'Posicion 2')
print(f'Agregamos un elemento en la 2da posición: {lista}\n')

# EXTEND - agrega varios elementos a la lista (una lista dentro de otra lista)
print('EXTEND:')
lista.extend([' ', 5, 4, 3, 2, 1])
print(f'Le agregagmos un conjunto de valores a la lista: {lista}\n')


# MÉTODOS PARA ELIMINAR ELEMENTOS

# POP - elimina un elemento de una lista, pide indice y devuelve valor 
print('POP:')
lista.pop(0)
print(f'Elimino primer elemento: {lista}')
lista.pop(-1) # -1: último, -2: penúltimo, ...
print(f'Elimino último elemento: {lista}\n') # muy útil cuando no se conoce la poscion de los últimos elementos

# REMOVE - remueve un elemento de una lista, pide valor 
print('REMOVE:')
lista.remove('Posicion 2')
print(f'Elimino Posicion 2: {lista}\n')

# CLEAR - elimina todos los elementos de la lista 
print('CLEAR:')
lista.clear()
print(f'Elimino todos los elementos: {lista}\n')

# SORT - ordena una lista de forma ascendente
print('SORT:')
lista.extend([0, 2, 5, 1, 8, 97, 7 , 27, 10, 9])
print(f'Dada esta lista: {lista}')
lista.sort() # No funciona si la lista tiene cadenas de texto
#lista.sort(key=lambda x: (isinstance(x, bool), x)) # Esto me soluciona el problema del True=1 y False=0
print(f'La ordenamos de menor a mayor: {lista}\n')
# Si la lista está compuesta por tuplas tenemos que hacer uso del key:
# Ej.1:
# Lista de tuplas
print()
lista_tuplas = [(3, 1), (1, 2), (4, 0), (2, 3)]

# Ordenar por el primer elemento de cada tupla
lista_tuplas.sort(key=lambda x: x[0]) # Si queremos ordenar por el segundo elemento usamos x[1]

# Imprimir la lista ordenada
print(lista_tuplas)
print()

# Ej.2:
print()
def obtener_segundo_elemento(tupla):
    return tupla[1]

# Lista de tuplas
lista_tuplas = [(3, 1), (1, 2), (4, 0), (2, 3)]

# Ordenar por el segundo elemento de cada tupla utilizando la función definida
lista_tuplas.sort(key=obtener_segundo_elemento)

# Imprimir la lista ordenada
print(lista_tuplas)
print()

# REVERSE - invierte los elementos de una lista 
print('REVERSE:')
lista.reverse()
print(f'La ordenamos de mayor a menor : {lista}\n')
# OTRA FORMA
#lista.sort(reverse = True)
