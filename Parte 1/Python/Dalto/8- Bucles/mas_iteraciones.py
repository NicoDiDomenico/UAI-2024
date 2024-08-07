frutas = ['Naranja', 'Melón', 'Banana', 'Sandía']
print('-----------------')
# Evitando la ejecucion de un bucle usando continue
for fruta in frutas:
    if fruta == 'Melón':
        continue
    print(f'Me gusta la {fruta}')

print('-----------------')

# Evitando que se ejecute el resto de bucles
for fruta in frutas:
    if fruta == 'Melón':
        break # en caso de existir un else: el break no lo ejecuta
    print(f'Me gusta la {fruta}')

print('-----------------')

# Recorrer una cadena de texto
saludo = 'Hola crack'

for s in saludo:
    print(s)
    
print('-----------------')

# For en una sola linea de código
numeros = [3, 6, 8, 9]

# método no óptimo
print(f'duplicando: {numeros}')
print('(No óptimo)')
numeros_duplicados = list()
for numero in numeros:
    numeros_duplicados.append(numero*2) # .append(): agregar un elemento al final de una lista

print(numeros_duplicados)
print()

# método óptimo
print('(Óptimo)')
numeros_duplicados = [x*2 for x in numeros]
print(numeros_duplicados)



