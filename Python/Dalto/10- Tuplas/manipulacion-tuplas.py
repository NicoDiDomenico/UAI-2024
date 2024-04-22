# Basico Tuplas
'''
Están ordenadas y no pueden modificarse
'''
estudiantes = ('Alex', 22, 'M')

# Count en tuplas
print(estudiantes.count('Alex'))
# Este metodo lo vimos en cadenas
print(estudiantes[0].count('A'))

print('\nM se encuentra en la posicion:')
print(estudiantes.index('M'))
print()

for e in estudiantes:
    print(e)

if 'Alex' in estudiantes:
    print('Alex está aquí')




