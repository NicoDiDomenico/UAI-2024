# Basico Conjuntos / Sets
'''
- Los elementos de un set son únicos - No puede haber elementos duplicados.
- Son desordenados - No mantienen el orden de cuadno son declarados.
- Sus elementos deben ser inmutables
'''

utensillos = {'tenedor', 'cocina', 'cuchillo', 'cuchillo'}
platos = {'plato', 'bol', 'taza', 'cocina'}

utensillos.add('cucharita')

# Notar que no se imprimen como se declararon
for x in utensillos:
    print(x)

utensillos.remove('cucharita')

print()
for x in utensillos:
    print(x)

# Elimina un elemeto al azar
print()
utensillos.pop()
for x in utensillos:
    print(x)

'''
# Elimina todos los elementos
print()
utensillos.clear()
for x in utensillos:
    print(x)
'''

# Agrego platos a utensillos
print()
utensillos.update(platos)
for x in utensillos:
    print(x)

# Muestra los elementos que sean diferentes al del conjunto platos
print()
print(utensillos.difference(platos))

# Muestra el elemento repetido
print()
print(utensillos.intersection(platos))