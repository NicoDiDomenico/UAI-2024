# Definimos el diccionario
diccionario = {
    'nombre': 'Nicolás ',
    'apellido': 'Di Domenico',
    'suscriptores': 1
}
print(f'\nBiblioteca: {diccionario}\n')
# Keys - devuelve las claves  
print('KEYS:')
claves = diccionario.keys()
print(f'Las Keys de mi dicionario son: {claves}\n') #también nos sirven para iterar

# Get - devuelve el valor de una clave 
print('GET:')
claves = diccionario.get('nombre')
print(f'El value de la llave elegida es: {claves}\n')

# Clear - elimina todos los elementos 
print('CLEAR:')
diccionario.clear()
print(f'Limpiamos el diccionario: {diccionario}\n')

# Pop - elimina un elemento 
diccionario = {
    'nombre': 'Nicolás ',
    'apellido': 'Di Domenico',
    'suscriptores': 1
}
print('POP:')
diccionario.pop('suscriptores') # puedo poner varias keys a eliminar
print(f'Al quitar suscriptores nos queda: {diccionario}\n')

# Items - para iterar el diccionario
print('ITEMS:')
diccionario_iterable = diccionario.items() # el diccionario asi como está no se puede iterar => usamos items()
print(f'Ahora el diccionario se puede iterar (recorrer): {diccionario_iterable}\n')
# Hay que aprender a usar bucles para usar items


