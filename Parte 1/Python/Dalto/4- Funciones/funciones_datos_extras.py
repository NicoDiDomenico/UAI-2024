# Parámetros posicionales
def frase(nombre, apellido, adjetivo):
    return f'{apellido}, {nombre} sos muy {adjetivo}.'

rta_frase = frase('Nicolás', 'Di Domenico', 'Ansioso') # coincide la posicion de los parámetros
print(rta_frase)

# Parámetros Forzados (keyword arguments)
def frase(nombre, apellido, adjetivo):
    return f'{apellido}, {nombre} sos muy {adjetivo}.'

rta_frase = frase(adjetivo = 'Ansioso', nombre = 'Nicolás', apellido = 'Di Domenico') # No coincide la posicion de los parámetros
print(rta_frase)

# Utilidad al usar keyword arguments
# mi 3er parámetro se vuelve opcional. 
def frase(nombre, apellido, adjetivo = 'tonto'):
    return f'{apellido}, {nombre} sos muy {adjetivo}.'

rta_frase = frase('Nicolás', 'Di Domenico')  
print(rta_frase)

# mi 3er parámetro es el que ingreso en la función aunque después defina uno en la misma. 
def frase(nombre, apellido, adjetivo = 'tonto'):
    return f'{apellido}, {nombre} sos muy {adjetivo}.'

rta_frase = frase('Nicolás', 'Di Domenico', 'inteligente') # inteligente remplaza tonto
print(rta_frase)