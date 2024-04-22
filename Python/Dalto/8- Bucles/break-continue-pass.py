nombre = ''

# Cortar bucle
while True:
    nombre = input('Ingrese nombre: ')
    if nombre != '':
        break

print()
# Saltar sentencia
numero = "123-456-789"
for i in numero:
    if i == '-':
        continue # Salta al siguiente item sin ejecutar el código debajo
        print(' esto no se va a ejecutar ')
    else:
        print(i, end='')

print()
#
for i in range(1,15):
    if i == 13:
        pass # No hace nada, simplemente pasa al siguiente bloque de código
        print(' No imprimo 13 ')
    else:
        print(i)