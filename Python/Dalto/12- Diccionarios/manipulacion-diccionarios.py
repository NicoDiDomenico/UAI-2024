# Basico Diccionarios
'''
- Un diccionario es una coleccion modificable y no ordenada de pares unicos clave valor
- Son rápidos porque usan el concepto de hash y nos permite acceder rapidamente a un valor
'''
capitales = {
    'EE.UU': 'Whashington D.C',
    'Argentina': 'Buenos Aires',
    'Chile': 'Santiago de Chile',
    'Brasil': 'Brasilia',
    #'Cursos': ['Python', 'C++'],
    #'años': 22
}

print(capitales['Chile'])

# Saber si una clave está en el dicccionario:
print()
print(capitales.get('Alemania')) # Retorna None
print(capitales.get('Argentina')) # Retorna el valor si lo encuentra

# O otra forma es conociendo todas las claves
print()
print(capitales.keys())

# También se pueden obtener los valores
print()
print(capitales.values())

# Traer todo el diccionario
print()
print(capitales.items())

# Tambien podemos traer el diccionario con bucle for
print()
for key, value in capitales.items():
    print(key, value)

# Agregamos una nueva key con su valor
print()
capitales.update({'Alemania': 'Berlin'})

# Otra forma de mostrar
print()
for key in capitales:
    print(key, capitales[key])

# Eliminar valor
capitales.pop('EE.UU')

# Limpiar el diciconario
#capitales.clear()