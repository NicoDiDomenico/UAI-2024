# ARGS(argumentos) - Es un parámetro que empaquetará todos los argumentos en una tupla
# Es muy útil para que una funcion pueda aceptar una cantidad de variables de argumentos
'''
# forma no óptima de sumar valores
def suma(lista):
    rta = 0
    for l in lista:
        rta += l
    return rta

rta = suma([1,2,3]) 
print (f'La suma es {rta}')
'''
# forma óptima usando el operador * como argumento (*args)
def suma(*numeros):
    rta = sum(numeros)
    return rta
'''
# Otra forma de hacer el def
def suma(*args):
    suma = 0
    cosas = list(args) # La tupla la convertimos en lista -> asi la podemos editar
    cosas[3] = 0
    for i in cosas:
        suma += i
    return suma
'''
rta = suma(1,2,3)
print(f'La suma es {rta}')
'''
# En el ej. anterior se podria haber usado una lista y listo, acá vamos a ver un ej. + útil: 
# Concatenar listas
lista1 = [1, 2, 3]
lista2 = [4, 5, 6]

lista_combinada = [*lista1, *lista2]
print(lista_combinada)  # Salida: [1, 2, 3, 4, 5, 6]
# * no devuelve una lista, solamente números... si usamos type() notar que devolveria un error.
'''