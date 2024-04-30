## Curso Dalto
# 54
# Introducción
# Definiendo lambda - Se crean utilizando la palabra clave lambda y no requieren un nombre. Son expresiones anónimas 
# que pueden tener cualquier número de parámetros y una única expresión.
# Ejemplos de datos anónimos:
#   'asdasdas'
#   [True, 42719, 'Nashe']

## Usando Lambda


# 1 - Multiplicando *2
# En una función normal: 
def multiplicar_por_dos(x):
    return x*2

# Con Lambda:
multiplicar_por_dos = lambda x : x*2
#                     lambda parametros: expresion
print(multiplicar_por_dos(5))

# 2 - Retornando números pares:
# En una función normal: 
numeros = [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20]
def num_pares(x):
    if x%2 == 0:
        return True

lista_numeros_pares = list(filter(num_pares,numeros))
#    La función filter toma dos argumentos: la función de filtrado (num_pares) y la secuencia a filtrar (numeros). 
# Filtra los elementos de la secuencia para los cuales la función de filtrado devuelve True. En este caso, filtra 
# los números pares de la lista original. Luego convierto el objeto filter devuelto en una lista con list().
# - Es una función de orden superior
# - Permite utilizar un paradigma de programación funcional
print(lista_numeros_pares) 

# Con Lambda:
lista_numeros_pares = list(filter(lambda x : x%2 == 0,numeros))
print(lista_numeros_pares)
#    La expresión lambda está verificando si x es par (x % 2 == 0). Si es par, la expresión lambda devuelve True, 
# lo que indica a la función filter que incluya ese número en la lista resultante. Si el número es impar, 
# la expresión lambda devuelve False, y el número es excluido.
#    En resumen, aunque no estás utilizando la palabra clave True o False explícitamente, la expresión lambda 
# está devolviendo valores que se comportan como booleanos en el contexto de la función filter.

## Curso de Pildorasinformáticas

#    Todo lo que podemos hacer con una expresión Lambda se puede hacer con una función normal pero no todo lo 
# que se puede hacer con una función normal lo podemos hacer con una expresión lambda

def area_triangulo(base,altura):
    return base*altura/2

# Lambda nos va a permitir simplificar la sintaxis de la función anterior en el caso de ser sencilla...

area_triángulo = lambda b,a : (b*a)/2
print(area_triángulo(2,4))