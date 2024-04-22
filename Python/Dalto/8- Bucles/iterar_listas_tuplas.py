# For
animales = ('Perro', 'Gato', 'Loro', 'Ratón') # Tupla
numeros = [1, 2, 3, 4] # Lista

print('---------------')
i = 1
for animal in animales:
    print(f'Animal {i}: {animal}')
    i += 1    
print('---------------')
i = 1
for numero in numeros:
    print(f'Numero {i}: {numero*2}')
    i += 1 

print('---------------')

# Doble for
for n in numeros:
    for a in animales:
        print(f'[{n}]: {a}')

print('---------------')

# For en simultaneo
for n,a in zip(numeros,animales): # Notar que funciona igual al primer for
    print(f'Animal {n}: {a}')
print('---------------')
# Otro uso
for n,a in zip(numeros,animales): 
    print(f'Numero {n}')
    print(f'Animal {a}')

print('---------------')

# For sin tener que crear listas/tuplas de números
for i in range(0,9): # Esta forma me sirve para definir un mínimo
    print(f'iteracion n° {i}')
print('---------------')
# Otra forma
for i in range(9):
    print(f'iteracion n° {i}')
print('---------------')

# Utilidad
# forma no óptima
for i in range(len(animales)): #La función len() en Python se utiliza para obtener la longitud de un objeto
    print(f'[{i+1}]:{animales[i]}')

print('---------------')

# forma óptima 
print('La mejor forma de usar for es con enumerate():')
for i in enumerate(animales): # enumerate devuelve una tupla por c/ iteracion
    print(i)
print('- Notar que devuelve una tupla con su respectivo índice, esto nos será muy útil.')
print('')

# forma óptima 1
frutas = ['manzana', 'banana', 'cereza']
print('Ejemplo 1:')
for i in enumerate(frutas): 
    indice = i[0]
    valor = i[1]
    print(f'Índice: {indice}, Fruta: {valor}') 

# forma óptima 2
print('\nEjemplo 2:')
for indice, valor in enumerate(animales):
    print(f'Índice: {indice}, Animal: {valor}')
    
print('---------------')

# Usando else en for
ropa = list(['Remera', 'Pantalón', 'Zapatilla'])
print('Usando else en for:')
for i, v in enumerate(ropa):
    print(f'Índice: {i}, Animal: {v}')
else: 
    print('Fin del bucle')
    
print('---------------')