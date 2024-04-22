# Bucle dentro de otro buclea, ya sea while o for
filas = int(input('Ingrese filas:'))
columnas = int(input('Ingrese columnas:'))
simbolo = input('Ingrese simbolo:')

'''
# Ambos son iguales

for i in range(filas):
    for j in range(columnas):
        print(simbolo)
    print()
    
for i in range(filas):
    for j in range(columnas):
        print(simbolo, end='\n')
    print()
'''

for i in range(filas):
    for j in range(columnas):
        print(simbolo, end='') # De esta forma le sacamos ese \n que se forma
    print()